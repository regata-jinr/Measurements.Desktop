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
using RCM = Regata.Core.Messages;
using Regata.Core.UI.WinForms.Controls;
using System;
using System.Windows.Forms;

namespace Regata.Desktop.WinForms.Measurements
{
    public partial class MainForm
    {
        public ControlsGroupBox controlsMeasParams;
        public CheckedArrayControl<float?> CheckedHeightArrayControl;
        public Button buttonShowAcqQueue;
        public DurationControl DurationControl;

        private void InitializeMeasurementsParamsControls()
        {
            try
            {
                DurationControl = new DurationControl();
                CheckedHeightArrayControl = new CheckedArrayControl<float?>(new float?[] { 2.5f, 5f, 10f, 20f }) { Name = "CheckedArrayControlHeights" };
                CheckedHeightArrayControl.checkedListBox.ColumnWidth = 70;

                buttonShowAcqQueue = new Button() { AutoSize = false, Dock = DockStyle.Fill, Name = "buttonShowAcqQueue", Enabled = false };
                controlsMeasParams = new ControlsGroupBox(new Control[] { DurationControl, CheckedHeightArrayControl, buttonShowAcqQueue }) { Name = "controlsMeasParams" };
                controlsMeasParams._tableLayoutPanel.RowStyles[0].Height = 37.5F;
                controlsMeasParams._tableLayoutPanel.RowStyles[1].Height = 37.5F;
                controlsMeasParams._tableLayoutPanel.RowStyles[2].Height = 25F;
                mainForm.FunctionalLayoutPanel.Controls.Add(controlsMeasParams, 1, 0);

                DurationControl.DurationChanged += (s, e) => mainForm.MainRDGV.FillDbSetValues("Duration", (int)DurationControl.Duration.TotalSeconds);
                CheckedHeightArrayControl.SelectionChanged += (s,e) => mainForm.MainRDGV.FillDbSetValues("Height", CheckedHeightArrayControl.SelectedItem);

                buttonShowAcqQueue.Click += ButtonShowAcqQueue_Click;
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_INI_MEAS_PARAMS)
                {
                    DetailedText = ex.ToString()
                });
            }
        }

        private void ButtonShowAcqQueue_Click(object sender, System.EventArgs e)
        {
            var fq = GenerateSamplesQueueForm();
            fq?.Show();
        }


    } //public partial class MainForm
}     // namespace Regata.Desktop.WinForms.Measurements
