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
using System.Linq;
using System.Windows.Forms;

namespace Regata.Desktop.WinForms.Measurements
{
    public partial class MainForm
    {
        public ControlsGroupBox controlsMeasParams;
        public CheckedArrayControl<float> CheckedHeightArrayControl;
        public Button buttonShowAcqQueue;
        public DurationControl DurationControl;

        private void InitializeMeasurementsParamsControls()
        {
            try
            {
                DurationControl = new DurationControl();
                CheckedHeightArrayControl = new CheckedArrayControl<float>(new float[] { 2.5f, 5f, 10f, 20f }) { Name = "CheckedArrayControlHeights" };
                buttonShowAcqQueue = new Button() { AutoSize = false, Dock = DockStyle.Fill, Name = "buttonShowAcqQueue" };
                controlsMeasParams = new ControlsGroupBox(new Control[] { DurationControl, CheckedHeightArrayControl, buttonShowAcqQueue }) { Name = "controlsMeasParams" };

                FunctionalLayoutPanel.Controls.Add(controlsMeasParams, 1, 0);

                DurationControl.DurationChanged += (s, e) => FillDurationToSelectedRecords();
                CheckedHeightArrayControl.SelectionChanged += () => AssignRecordsMainRDGV("Height", CheckedHeightArrayControl.SelectedItem);

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
            if (fq == null) return;

            fq.Show();
        }

        private void FillDurationToSelectedRecords()
        {
            try
            {
                foreach (var i in mainForm.MainRDGV.SelectedCells.OfType<DataGridViewCell>().Select(c => c.RowIndex).Where(c => c >= 0).Distinct())
                {
                    var m = mainForm.MainRDGV.CurrentDbSet.Where(m => m.Id == (int)mainForm.MainRDGV.Rows[i].Cells["Id"].Value).FirstOrDefault();
                    if (m == null) continue;
                    m.Duration = (int)DurationControl.Duration.TotalSeconds;
                    mainForm.MainRDGV.CurrentDbSet.Update(m);
                }
                mainForm.MainRDGV.SaveChanges();
                mainForm.MainRDGV.Refresh();
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_FILL_DUR)
                {
                    DetailedText = ex.ToString()
                });
            }
        }

    } //public partial class MainForm
}     // namespace Regata.Desktop.WinForms.Measurements
