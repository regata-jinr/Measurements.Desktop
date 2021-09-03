/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2021, REGATA Experiment at FLNP|JINR                       *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 *                                                                         *
 * The REGATA Experiment team license this file to you under the           *
 * GNU GENERAL PUBLIC LICENSE                                              *
 *                                                                         *
 ***************************************************************************/

using Regata.Core;
using Regata.Core.DataBase.Models;
using Regata.Core.Hardware;
using Regata.Core.Messages;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Regata.Desktop.WinForms.Measurements
{
    public partial class MainForm
    {
        private List<Detector> _detectors;

        private async Task InitializeDetectors()
        {
            try
            {
                // https://github.com/regata-jinr/Measurements.Desktop/issues/40
                //await Detector.RunMvcgAsync();
                _ = Task.Run(async () => { await Detector.RunMvcgAsync(); });

                _detectors = new List<Detector>(8);
                foreach (var d in mainForm.MainRDGV.CurrentDbSet.Local.Select(m => m.Detector).Distinct().OrderBy(n => n))
                {
                    await Detector.ShowDetectorInMvcgAsync(d);

                    var det = new Detector(d, enableXemo: _scFlagMenuItem.Checked);
                    
                    det.AcquireDone   += Det_AcquireDone;
                    det.AcquireStart  += Det_AcquireStart;
                    det.HardwareError += Det_HardwareError;
                    det.ParamChange   += Det_ParamChange;
                    det.StatusChanged += Det_StatusChanged;
                    _detectors.Add(det);
                }
                _detectors.TrimExcess();

                if (_scFlagMenuItem.Checked)
                    await Task.WhenAll(_detectors.Select(d => CallHomeAsync(d.PairedXemoDevice)));

            }
            catch (Exception ex)
            {
                Report.Notify(new Message(Codes.ERR_UI_WF_INI_DET) { DetailedText = ex.ToString() });
            }
        }

        private async Task CallHomeAsync(SampleChanger sc)
        {
            await sc.HomeAsync();
        }

        private void CallStop(SampleChanger sc)
        {
            sc.Stop();
        }

        private void CallHalt(SampleChanger sc)
        {
            sc.HaltSystem();
        }

        private Measurement GetFirstNotMeasuredForDetector(string detName)
        {
            // FIXME: regataContext.Measurements.Local inversed
            return mainForm.MainRDGV.CurrentDbSet.Local.Where(m => m.Detector == detName &&
                                                                   string.IsNullOrEmpty(m.FileSpectra) &&
                                                                   !m.DateTimeFinish.HasValue
                                                             )
                                                       .OrderBy(m => m.DiskPosition)
                                                       .FirstOrDefault();
        }

        private void Det_ParamChange(Detector det)
        {
        }

        private void Det_HardwareError(Detector det)
        {
            ColorizeRDGVRow(det.CurrentMeasurement, Color.Red);

        }

        private async void Det_AcquireStart(Detector det)
        {
            ColorizeRDGVRow(det.CurrentMeasurement, Color.LightYellow);
            if (_dcp != null)
                await _dcp.SourcesInitialize();
        }

        private async void Det_AcquireDone(Detector det)
        {
            try
            {
                // FIXME: A random AcquireDone generated events?
                if (Math.Abs(det.ElapsedRealTime - det.Counts) > 3) return;

                det.CurrentMeasurement.FileSpectra = await Detector.GenerateSpectraFileNameFromDBAsync(det.Name, det.CurrentMeasurement.Type);
                det.CurrentMeasurement.DeadTime = det.DeadTime;
                det.Save();

                mainForm.ProgressBar.Value++;
                mainForm.MainRDGV.Update(det.CurrentMeasurement);
                mainForm.MainRDGV.SaveChanges();

                await UpdateCurrentReigster();

                ColorizeRDGVRow(det.CurrentMeasurement, Color.LightGreen);

                Report.Notify(new Message(Codes.SUCC_UI_WF_ACQ_DONE) { Head = $"{det.Name} complete acq for {det.CurrentMeasurement}"});

            }
            catch (DbUpdateException dbe)
            {
                Det_HardwareError(det);
                Report.Notify(new Message(Codes.ERR_UI_WF_ACQ_DONE_DB) { DetailedText = dbe.ToString() });
            }
            //catch () 
            // hardware exception
            catch (Exception ex)
            {
                Det_HardwareError(det);
                Report.Notify(new Message(Codes.ERR_UI_WF_ACQ_DONE_UNREG) { DetailedText = string.Join("--", ex.Message, ex?.InnerException.Message) });
            }
            finally
            {
                if (!mainForm.MainRDGV.CurrentDbSet.Local.Where(m => m.FileSpectra == null).Any())
                {
                    buttonStart.Enabled = true;
                }
                else
                {
                    await MStartAsync(det);
                }
            }
        }

        private void Det_StatusChanged(object sender, System.EventArgs e)
        {
        }

    
    } // public partial class MainForm
}     // namespace Regata.Desktop.WinForms.Measurements
