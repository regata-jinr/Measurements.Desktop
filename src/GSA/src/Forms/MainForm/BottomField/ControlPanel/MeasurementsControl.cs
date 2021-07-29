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

using Regata.Core.Hardware;
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
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            await foreach (var d in Detector.GetAvailableDetectorsAsyncStream())
                if (!string.IsNullOrEmpty(d))
                    CheckedAvailableDetectorArrayControl.Add(d);

        }


    } // public partial class MainForm
}     // namespace Regata.Desktop.WinForms.Measurements
