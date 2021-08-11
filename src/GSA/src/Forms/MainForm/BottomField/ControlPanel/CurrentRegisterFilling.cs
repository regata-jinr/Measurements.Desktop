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

using Regata.Core.UI.WinForms.Controls;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Regata.Desktop.WinForms.Measurements
{
    public partial class MainForm
    {
        private ControlsGroupBox buttonsRegForm;
        private Button buttonRemoveSelectedSamples;
        private Button buttonAddSelectedSamplesToReg;
        private Button buttonClearRegister;
        private Button buttonAddAllSamples;

        private void InitializeRegFormingControls()
        {
            buttonRemoveSelectedSamples = new Button() { AutoSize = false, Dock = DockStyle.Fill, Name = "buttonRemoveSelectedSamples" };
            buttonAddSelectedSamplesToReg = new Button() { AutoSize = false, Dock = DockStyle.Fill, Name = "buttonAddSelectedSamplesToReg" };
            buttonClearRegister = new Button() { AutoSize = false, Dock = DockStyle.Fill, Name = "buttonClearRegister" };
            buttonAddAllSamples = new Button() { AutoSize = false, Dock = DockStyle.Fill, Name = "buttonAddAllSamples" };
            buttonsRegForm = new ControlsGroupBox(new Button[] { buttonAddSelectedSamplesToReg, buttonAddAllSamples, buttonRemoveSelectedSamples, buttonClearRegister }) { Name = "buttonsRegFormBox" };
            buttonsRegForm.groupBoxTitle.Dock = DockStyle.Fill;

            FunctionalLayoutPanel.Controls.Add(buttonsRegForm, 0, 0);

            buttonAddSelectedSamplesToReg.Click += ButtonAddSelectedSamplesToReg_Click;
            buttonRemoveSelectedSamples.Click += ButtonRemoveSelectedSamples_Click;
            buttonClearRegister.Click += ButtonClearRegister_Click;
            buttonAddAllSamples.Click += ButtonAddAllSamples_Click;
        }

        private void ButtonAddAllSamples_Click(object sender, EventArgs e)
        {
            var cti = mainForm.TabsPane.SelectedTabIndex;

            for (int i = 0; i < mainForm.TabsPane[cti, 1].RowCount; ++i)
            {
                int cellId;
                if (cti == 0)
                    cellId = (int)mainForm.TabsPane[cti, 1].Rows[i].Cells["Id"].Value;
                else
                    cellId = (int)mainForm.TabsPane[cti, 1].Rows[i].Cells["IrradiationId"].Value;

                AddRecord(cellId);
            }
            mainForm.MainRDGV.SaveChanges();

        }

        private void ButtonClearRegister_Click(object sender, EventArgs e)
        {
            _circleDetArray.Reset();
            ClearCurrentRegister();
        }

        private void ButtonRemoveSelectedSamples_Click(object sender, EventArgs e)
        {
            foreach (var i in mainForm.MainRDGV.SelectedCells.OfType<DataGridViewCell>().Select(c => c.RowIndex).Where(c => c >= 0).Distinct())
                RemoveRecord((int)mainForm.MainRDGV.Rows[i].Cells["Id"].Value);

            mainForm.MainRDGV.SaveChanges();
        }

        private void ButtonAddSelectedSamplesToReg_Click(object sender, EventArgs e)
        {
            var cti = mainForm.TabsPane.SelectedTabIndex;

            foreach (var i in mainForm.TabsPane[cti, 1].SelectedCells.OfType<DataGridViewCell>()
                                                                      .Select(c => c.RowIndex)
                                                                      .Where(c => c >= 0)
                                                                      .Distinct()
                                                                      .OrderBy(c => c))
            {
                var cellId = (int)mainForm.TabsPane[cti, 1].Rows[i].Cells["Id"].Value;
                if (cti == 0)
                    cellId = (int)mainForm.TabsPane[cti, 1].Rows[i].Cells["Id"].Value;
                else
                    cellId = (int)mainForm.TabsPane[cti, 1].Rows[i].Cells["IrradiationId"].Value;

                AddRecord(cellId);
            }
            mainForm.MainRDGV.SaveChanges();
        }

    } //public partial class MainForm
}     // namespace Regata.Desktop.WinForms.Measurements
