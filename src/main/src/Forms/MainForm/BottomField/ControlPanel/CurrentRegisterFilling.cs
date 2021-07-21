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

using System.Windows.Forms;
using System;
using System.Linq;
using Regata.Core.DataBase.Models;
using Regata.Core.DataBase;
using Regata.Core.UI.WinForms.Controls;

namespace Regata.Desktop.WinForms.Measurements
{
    public partial class MainForm
    {

        private ControlsGroupBox buttonsRegForm;
        private Button buttonRemoveSample;
        private Button buttonAddAllSamples;
        private Button buttonAddSampleToReg;

        private void InitializeRegFormingControls()
        {
            buttonRemoveSample = new Button() {   AutoSize = false, Dock=DockStyle.Fill, Name = "buttonRemoveSample"  };
            buttonAddAllSamples = new Button() {  AutoSize = false, Dock=DockStyle.Fill, Name = "buttonAddAllSamples" };
            buttonAddSampleToReg = new Button() { AutoSize = false, Dock=DockStyle.Fill, Name = "buttonAddSampleToReg"  };
            buttonsRegForm = new ControlsGroupBox(new Button[] { buttonAddSampleToReg, buttonAddAllSamples, buttonRemoveSample });
            buttonsRegForm.groupBoxTitle.Dock = DockStyle.Fill;

            FunctionalLayoutPanel.Controls.Add(buttonsRegForm, 0, 0);

            buttonAddAllSamples.Click += ButtonAddAllSamples_Click;
            buttonAddSampleToReg.Click += ButtonAddSampleToReg_Click;

        }

        private async void ButtonAddSampleToReg_Click(object sender, EventArgs e)
        {
            var id = mainForm.TabsPane.SelectedRowsLastDGV.OfType<DataGridViewRow>().Select(r => (int)r.Cells["Id"].Value).First();
            await AddRecordAsync(id);

        }

        private async void ButtonAddAllSamples_Click(object sender, EventArgs e)
        {

            var ids = mainForm.TabsPane.SelectedRowsLastDGV.OfType<DataGridViewRow>().Select(r => (int)r.Cells["Id"].Value).ToArray();

            _selectedIrradiations.Where(i => ids.Contains(i.Id));

            //await AddRecordsAsync();
        }




    } //public partial class MainForm
}     // namespace Regata.Desktop.WinForms.Measurements
