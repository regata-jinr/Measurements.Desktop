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

using Microsoft.EntityFrameworkCore;
using Regata.Core;
using Regata.Core.DataBase.Models;
using Regata.Core.Hardware;
using Regata.Core.Messages;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
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
                if (_detectors != null)
                    _detectors.Clear();
                else
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
            using (var ct = new CancellationTokenSource(TimeSpan.FromMinutes(2)))
            {
                await sc.HomeAsync(ct.Token);
            }
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
            det.CurrentMeasurement.DateTimeStart = det.AcquisitionStartDateTime;
            det.CurrentMeasurement.Height = det.Sample.Height;
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
                //if (Math.Abs((DateTime.Now - det.CurrentMeasurement.DateTimeStart.Value).TotalSeconds) < (det.CurrentMeasurement.Duration - 3)) return;

                if (det.Counts == 0) return;
                if (det.ElapsedRealTime < det.Counts - 10) return;

                //if (Math.Abs(det.Counts - det.ElapsedRealTime) > 15)
                //{
                //    // FIXME: Spectra will not save after pause
                //    //det.Pause();
                //    //det.Start();
                //    return;
                //}

                det.CurrentMeasurement.FileSpectra = await Detector.GenerateSpectraFileNameFromDBAsync(det.Name, det.CurrentMeasurement.Type);
                det.CurrentMeasurement.DeadTime = det.DeadTime;
                det.Save();

                mainForm.ProgressBar.Value++;
                mainForm.MainRDGV.Update(det.CurrentMeasurement);
                mainForm.MainRDGV.SaveChanges();

                await UpdateCurrentReigster();

                ColorizeRDGVRow(det.CurrentMeasurement, Color.LightGreen);

                Report.Notify(new Message(Codes.SUCC_UI_WF_ACQ_DONE) { Head = $"{det.Name} complete acq for {det.CurrentMeasurement}" });

            }
            //catch (DbUpdateConcurrencyException dbce)
            //{
                
            //}
            catch (DbUpdateException dbe)
            {
                Det_HardwareError(det);
                Report.Notify(new Message(Codes.ERR_UI_WF_ACQ_DONE_DB) { DetailedText = dbe.ToString() });
            }
            catch (TaskCanceledException)
            {
                Det_HardwareError(det);
                await CompleteXemoCycle(det.PairedXemoDevice, det.CurrentMeasurement.DiskPosition);
            }
            catch (Exception ex)
            {
                Det_HardwareError(det);
                Report.Notify(new Message(Codes.ERR_UI_WF_ACQ_DONE_UNREG) { DetailedText = string.Join("--", ex.Message, ex?.InnerException?.Message) });
            }
            finally
            {
                if (!mainForm.MainRDGV.CurrentDbSet.Local.Where(m => m.FileSpectra == null && m.Detector == det.Name).Any())
                {
                    await CompleteXemoCycle(det.PairedXemoDevice, det.CurrentMeasurement.DiskPosition);
                }
                else
                {
                    await MStartAsync(det);
                }
                if (!mainForm.MainRDGV.CurrentDbSet.Local.Where(m => m.FileSpectra == null).Any())
                {
                    mainForm.WindowState = mainForm.WindowState;
                    if (_dcp != null)
                        _dcp.WindowState = System.Windows.Forms.FormWindowState.Minimized;
                    buttonStart.Enabled = true;
                    mainForm.WindowState = System.Windows.Forms.FormWindowState.Minimized;
                    mainForm.Show();
                    mainForm.WindowState = System.Windows.Forms.FormWindowState.Normal;
                }
            }
        }


        private async Task CompleteXemoCycle(SampleChanger sc, int? diskPosition)
        {
            if (!diskPosition.HasValue || sc == null)
                return;

            if (sc.IsSampleCaptured)
            {
                using (var ct = new CancellationTokenSource(TimeSpan.FromMinutes(2)))
                {
                    await sc.PutSampleToTheDiskAsync((short)diskPosition.Value, ct.Token);
                }
            }
            using (var ct = new CancellationTokenSource(TimeSpan.FromMinutes(2)))
            {
                await sc.HomeAsync(ct.Token);
            }
        }


        private void Det_StatusChanged(object sender, System.EventArgs e)
        {
        }

    
    } // public partial class MainForm
}     // namespace Regata.Desktop.WinForms.Measurements
