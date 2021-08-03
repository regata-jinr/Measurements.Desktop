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

using Regata.Core.DataBase.Models;
using Regata.Core.Hardware;
using System.Collections.Generic;
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
            await Detector.RunMvcgAsync();

            _detectors = new List<Detector>(8);
            foreach (var d in _regataContext.Measurements.Local.Select(m => m.Detector).Distinct())
            {
                await Detector.ShowDetectorInMvcgAsync(d);

                var det = new Detector(d);
                det.AcquireDone   += Det_AcquireDone;
                det.AcquireStart  += Det_AcquireStart;
                det.HardwareError += Det_HardwareError;
                det.ParamChange   += Det_ParamChange;
                det.StatusChanged += Det_StatusChanged;
                _detectors.Add(det);
            }
            _detectors.TrimExcess();
        }

        private Measurement GetFirstNotMeasuredForDetector(string detName)
        {
            return _regataContext.Measurements.Local.Where(m => m.Detector == detName && string.IsNullOrEmpty(m.FileSpectra)).FirstOrDefault();
        }

        private void Det_ParamChange(Detector det)
        {
        }

        private void Det_HardwareError(Detector det)
        {
            ColorizeRDGVRow(det.CurrentMeasurement, Color.Red);

        }

        private void Det_AcquireStart(Detector det)
        {
            ColorizeRDGVRow(det.CurrentMeasurement, Color.LightYellow);
        }

        private async void Det_AcquireDone(Detector det)
        {
            // FIXME: A random AcquireDone generated events?
            if (System.Math.Abs(det.ElapsedRealTime - det.PresetRealTime) > 3) return;

            det.CurrentMeasurement.FileSpectra = await Detector.GenerateSpectraFileNameFromDBAsync(det.Name, det.CurrentMeasurement.Type);
            det.Save();
            _regataContext.Update(det.CurrentMeasurement);
            _regataContext.SaveChanges();
            ColorizeRDGVRow(det.CurrentMeasurement, Color.LightGreen);

            if (!_regataContext.Measurements.Local.Where(m => m.FileSpectra == null).Any())
            {
                buttonStart.Enabled = true;
            }
            else
            {
                MStart(det.Name);
            }
        }

        private void Det_StatusChanged(object sender, System.EventArgs e)
        {
        }



    
    } // public partial class MainForm
}     // namespace Regata.Desktop.WinForms.Measurements
