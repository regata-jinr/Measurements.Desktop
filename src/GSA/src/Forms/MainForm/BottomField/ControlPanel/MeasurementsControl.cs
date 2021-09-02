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
using Regata.Core.DataBase;
using Regata.Core.DataBase.Models;
using Regata.Core.Hardware;
using RCM = Regata.Core.Messages;
using Regata.Core.UI.WinForms.Controls;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Regata.Desktop.WinForms.Measurements
{
    public partial class MainForm
    {
        private ControlsGroupBox controlsMeasControl;
        private CheckedArrayControl<string> CheckedAvailableDetectorArrayControl;
        private DetectorControlPanel _dcp;

        private TableLayoutPanel MeasurementsStartPanel;
        private Button buttonStop;
        private Button buttonClear;
        private Button buttonPause;
        private Button buttonStart;
        private Button buttonStopSC;
        private Button buttonHalt;

        private void InitializeMeasurementsControls()
        {
            try
            {
                buttonStop  = new Button() { Name = "buttonStop",  Dock = DockStyle.Fill, UseVisualStyleBackColor = true, BackColor = Color.Red    };
                buttonClear = new Button() { Name = "buttonClear", Dock = DockStyle.Fill, UseVisualStyleBackColor = true, BackColor = Color.White  };
                buttonPause = new Button() { Name = "buttonPause", Dock = DockStyle.Fill, UseVisualStyleBackColor = true, BackColor = Color.Yellow };
                buttonStart = new Button() { Name = "buttonStart", Dock = DockStyle.Fill, UseVisualStyleBackColor = true, BackColor = Color.Green  };

                buttonStopSC = new Button() { Name = "buttonStopSC", Dock = DockStyle.Fill, Enabled = false};
                buttonHalt = new Button() { Name = "buttonHalt", Dock = DockStyle.Fill, Enabled = false };

                buttonStop.Click  += ButtonStop_Click;
                buttonStart.Click += ButtonStart_Click;
                buttonClear.Click += ButtonClear_Click;
                buttonPause.Click += ButtonPause_Click;

                buttonHalt.Click += ButtonHalt_Click;
                buttonStopSC.Click += ButtonStopSC_Click;
                MeasurementsStartPanel = new TableLayoutPanel();
                MeasurementsStartPanel.ColumnCount = 2;
                MeasurementsStartPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                MeasurementsStartPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                MeasurementsStartPanel.Name = "MeasurementsStartPanel";
                MeasurementsStartPanel.RowCount = 3;
                MeasurementsStartPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33F));
                MeasurementsStartPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33F));
                MeasurementsStartPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33F));
                MeasurementsStartPanel.Dock = DockStyle.Fill;
                MeasurementsStartPanel.Controls.Add(buttonStopSC, 0, 0);
                MeasurementsStartPanel.Controls.Add(buttonHalt, 1, 0);
                MeasurementsStartPanel.Controls.Add(buttonStop, 0, 1);
                MeasurementsStartPanel.Controls.Add(buttonClear, 1, 1);
                MeasurementsStartPanel.Controls.Add(buttonPause, 0, 2);
                MeasurementsStartPanel.Controls.Add(buttonStart, 1, 2);

                CheckedAvailableDetectorArrayControl = new CheckedArrayControl<string>(new string[0]) { Name = "CheckedAvailableDetectorArrayControl" };

                controlsMeasControl = new ControlsGroupBox(new Control[] { CheckedAvailableDetectorArrayControl, MeasurementsStartPanel }) { Name = "controlsMeasControl" };

                FunctionalLayoutPanel.Controls.Add(controlsMeasControl, 2, 0);

                CheckedAvailableDetectorArrayControl.SelectionChanged += (s,e) => mainForm.MainRDGV.FillDbSetValues("Detector", CheckedAvailableDetectorArrayControl.SelectedItem);
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_INI_MEAS_CONTR) { DetailedText = ex.ToString() });

            }
        }

        private void ButtonStopSC_Click(object sender, EventArgs e)
        {
            if (_detectors == null)
                return;

            _detectors.ForEach(d => CallStop(d.PairedXemoDevice));

        }

        private void ButtonHalt_Click(object sender, EventArgs e)
        {
            if (_detectors == null)
                return;

            _detectors.ForEach(d => CallHalt(d.PairedXemoDevice));
        }

        private bool _isMeasurementsPaused = false;

        private void ButtonPause_Click(object sender, EventArgs e)
        {
            try
            {
                if (_detectors == null) return;

                foreach (var d in _detectors)
                {
                    if (_isMeasurementsPaused)
                        d.Start();
                    else
                    {
                        _isMeasurementsPaused = true;
                        d.Pause();
                    }
                }
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_ACQ_PAUSE)
                {
                    DetailedText = ex.ToString()
                });
            }
        }

        private void ButtonClear_Click(object sender, EventArgs e)
        {
            try
            {
                if (_detectors == null) return;

                foreach (var d in _detectors)
                {
                    d.Clear();
                }
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_ACQ_CLEAR)
                {
                    DetailedText = ex.ToString()
                });
            }
        }

        private void ButtonStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (_detectors == null) return;

                foreach (var d in _detectors)
                {
                    d.Stop();
                    d.Dispose();
                }
                _detectors.Clear();
                buttonStart.Enabled = true;
                _dcp?.Dispose();
                mainForm.ProgressBar.Value = 0;
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_ACQ_STOP) { DetailedText = ex.ToString() });
            }
        }

        private async void ButtonStart_Click(object sender, EventArgs e)
        {
            // TODO: check correcntess of measurements info
            // TODO: detectors availability

            if (!mainForm.MainRDGV.CurrentDbSet.Local.Any()) return;

            buttonStart.Enabled = false;
            mainForm.ProgressBar.Value = mainForm.MainRDGV.CurrentDbSet.Local.Where(m => !string.IsNullOrEmpty(m.FileSpectra)).Count();
            mainForm.ProgressBar.Maximum = mainForm.MainRDGV.RowCount;

            if (_detectors == null || _detectors.Count == 0)
                await InitializeDetectors();


            await Task.WhenAll(_detectors.Select(d => MStartAsync(d)));

            if (_dcp == null || _dcp.IsDisposed)
            {
                _dcp = new DetectorControlPanel(_detectors);
                _dcp.Show();
            }
        }

        private async Task MStartAsync(Detector d)
        {
            try
            {
                var m = GetFirstNotMeasuredForDetector(d.Name);
                if (m == null)
                {
                    //Report.Notify(new RCM.Message(Codes.WARN_UI_WF_ACQ_START_ALL_MEAS));
                    return;
                }

                Irradiation i = null;
                using (var r = new RegataContext())
                {
                    i = r.Irradiations.Where(ir => ir.Id == m.IrradiationId).FirstOrDefault();
                    if (i == null)
                    {
                        Report.Notify(new RCM.Message(Codes.ERR_UI_WF_ACQ_START_IRR_NF));
                        return;
                    }
                }

                await RunSampleChangerCycle(d);

                d.LoadMeasurementInfoToDevice(m, i);

                await RunSampleChangerCycle(d);

                d.Start();
                Report.Notify(new RCM.Message(Codes.SUCC_UI_WF_ACQ_START) { Head = $"{d.Name} complete acq for {d.CurrentMeasurement}" });

            }
            catch (Exception ex)
            {
                Det_HardwareError(d);
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_ACQ_START) { DetailedText = ex.ToString() });
            }
        }

        private async Task RunSampleChangerCycle(Detector d)
        {
            var sc = d.PairedXemoDevice;
            if (!_scFlagMenuItem.Checked || sc == null)
                return;

            if (d.CurrentMeasurement == null)
                return;


            if (!d.CurrentMeasurement.DiskPosition.HasValue)
                return;

            if (sc.IsSampleCaptured)
            { 
                await sc.PutSampleToTheDiskAsync((short)d.CurrentMeasurement.DiskPosition.Value);
                return;
            }

            await sc.TakeSampleFromTheCellAsync((short)d.CurrentMeasurement.DiskPosition.Value);
            var h = d.CurrentMeasurement.Height switch
            {
                > 10f  => Heights.h20,
                > 5f   => Heights.h10,
                > 2.5f => Heights.h5,
                _      => Heights.h2p5 
            };
            await sc.PutSampleAboveDetectorWithHeightAsync(h);

        }

        private void ColorizeRDGVRow(Measurement m, Color clr)
        {
            try
            {
                DataGridViewRow r = mainForm.MainRDGV.Rows.OfType<DataGridViewRow>().Where(r => (int)r.Cells["Id"].Value == m.Id).FirstOrDefault();

                if (r == null) return;

                r.DefaultCellStyle.BackColor = clr;
            }
            catch
            {
                
            }
        }

    } // public partial class MainForm
}     // namespace Regata.Desktop.WinForms.Measurements
