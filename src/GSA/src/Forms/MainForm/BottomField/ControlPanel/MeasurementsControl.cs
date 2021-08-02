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
using Regata.Core.UI.WinForms.Controls;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Regata.Desktop.WinForms.Measurements
{
    public partial class MainForm
    {
        private ControlsGroupBox controlsMeasControl;
        private CheckedArrayControl<string> CheckedAvailableDetectorArrayControl;

        private TableLayoutPanel MeasurementsStartPanel;
        private Button buttonStop;
        private Button buttonClear;
        private Button buttonPause;
        private Button buttonStart;

        private void InitializeMeasurementsControls()
        {
            buttonStop  = new Button() { Name = "buttonStop",   Dock = DockStyle.Fill, UseVisualStyleBackColor = true, BackColor = Color.Red };
            buttonClear = new Button() { Name = "buttonClear",  Dock = DockStyle.Fill, UseVisualStyleBackColor = true, BackColor = Color.White };
            buttonPause = new Button() { Name = "buttonPause",  Dock = DockStyle.Fill, UseVisualStyleBackColor = true, BackColor = Color.Yellow };
            buttonStart = new Button() { Name = "buttonStart",  Dock = DockStyle.Fill, UseVisualStyleBackColor = true, BackColor = Color.Green };

            buttonStop.Click += ButtonStop_Click;
            buttonStart.Click += ButtonStart_Click;
            
            MeasurementsStartPanel = new TableLayoutPanel();
            MeasurementsStartPanel.ColumnCount = 2;
            MeasurementsStartPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            MeasurementsStartPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            MeasurementsStartPanel.Name = "MeasurementsStartPanel";
            MeasurementsStartPanel.RowCount = 2;
            MeasurementsStartPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            MeasurementsStartPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            MeasurementsStartPanel.Dock = DockStyle.Fill;
            MeasurementsStartPanel.Controls.Add(buttonStop, 0, 0);
            MeasurementsStartPanel.Controls.Add(buttonClear, 1, 0);
            MeasurementsStartPanel.Controls.Add(buttonPause, 0, 1);
            MeasurementsStartPanel.Controls.Add(buttonStart, 1, 1);

            CheckedAvailableDetectorArrayControl = new CheckedArrayControl<string>(new string[0]) { Name = "CheckedAvailableDetectorArrayControl" };

            controlsMeasControl = new ControlsGroupBox(new Control[] { CheckedAvailableDetectorArrayControl, MeasurementsStartPanel } ) { Name = "controlsMeasControl" };

            FunctionalLayoutPanel.Controls.Add(controlsMeasControl, 2, 0);

            CheckedAvailableDetectorArrayControl.SelectionChanged += () => AssignRecordsMainRDGV("Detector", CheckedAvailableDetectorArrayControl.SelectedItem);
        }

        private void ButtonStop_Click(object sender, EventArgs e)
        {
            foreach (var d in _detectors)
            {
                d.Stop();
            }
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            // TODO: check correcntess of measurements info
            // TODO: detectors availability
            InitializeDetectors();
            //buttonStart.Enabled = false;

            foreach (var d in _detectors)
            {
                MStart(d.Name);
            }
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            await foreach (var d in Detector.GetAvailableDetectorsAsyncStream())
            {
                if (!string.IsNullOrEmpty(d))
                    CheckedAvailableDetectorArrayControl.Add(d);
            }
        }

        private void MStart(string dName)
        {
            var d = _detectors.Where(det => det.Name == dName).FirstOrDefault();
            var m = GetFirstNotMeasuredForDetector(d.Name);
            if (m == null)
            {
                
                return;
            }
            var i = _regataContext.Irradiations.Where(ir => ir.Id == m.IrradiationId).FirstOrDefault();
            if (i == null) return;

            d.LoadMeasurementInfoToDevice(m, i);
            d.Start();
        }

        private void ColorizeRDGVRow(Measurement m, Color clr)
        {
            DataGridViewRow r = mainForm.MainRDGV.Rows.OfType<DataGridViewRow>().Where(r => (int)r.Cells["Id"].Value == m.Id).FirstOrDefault();

            if (r == null) return;

            r.DefaultCellStyle.BackColor = clr;
        }

    } // public partial class MainForm
}     // namespace Regata.Desktop.WinForms.Measurements
