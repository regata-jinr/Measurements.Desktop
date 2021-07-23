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
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Regata.Core.DataBase.Models;
using Regata.Core.DataBase;
using Regata.Core.UI.WinForms.Controls;

namespace Regata.Desktop.WinForms.Measurements
{
    public partial class MainForm
    {

        private ControlsGroupBox buttonsRegForm;
        private Button buttonRemoveSamples;
        private Button buttonCancel;
        private Button buttonAddSamplesToReg;
        private CancellationTokenSource _cancToken;

        private void InitializeRegFormingControls()
        {
            buttonRemoveSamples = new Button() {   AutoSize = false, Dock=DockStyle.Fill, Name = "buttonRemoveSamples" };
            buttonAddSamplesToReg = new Button() { AutoSize = false, Dock=DockStyle.Fill, Name = "buttonAddSamplesToReg" };
            buttonCancel = new Button() {  AutoSize = false, Dock=DockStyle.Fill, Name = "buttonCancel" };
            buttonsRegForm = new ControlsGroupBox(new Button[] { buttonAddSamplesToReg, buttonRemoveSamples, buttonCancel,  });
            buttonsRegForm.groupBoxTitle.Dock = DockStyle.Fill;

            FunctionalLayoutPanel.Controls.Add(buttonsRegForm, 0, 0);

            buttonCancel.Click += ButtonCancel_Click;
            buttonAddSamplesToReg.Click += ButtonAddSamplesToReg_Click;
            buttonRemoveSamples.Click += ButtonRemoveSamples_Click;

        }

        private async void ButtonRemoveSamples_Click(object sender, EventArgs e)
        {
            try
            {
                buttonRemoveSamples.Enabled = false;

                if (mainForm.MainRDGV.SelectedCells.Count <= 0) return;
                _cancToken = new CancellationTokenSource();
                var RemovingTasks = mainForm.MainRDGV.SelectedCells.OfType<DataGridViewCell>().Select(c => c.RowIndex).Where(c => c >= 0).Distinct().Select(c => RemoveRecordAsync((int)mainForm.MainRDGV.Rows[c].Cells["Id"].Value, _cancToken.Token)).ToList();



                while (RemovingTasks.Any())
                {
                    var completedTask = await Task.WhenAny(RemovingTasks);
                    RemovingTasks.Remove(completedTask);
                }


                mainForm.MainRDGV.ClearSelection();
            }
            catch (OperationCanceledException)
            {

            }
            finally
            {
                _cancToken = null;
                buttonRemoveSamples.Enabled = true;
            }

        }

        private async void ButtonAddSamplesToReg_Click(object sender, EventArgs e)
        {

            try
            {
                buttonAddSamplesToReg.Enabled = false;

                if (mainForm.TabsPane.SelectedRowsLastDGV.Count <= 0) return;

                _cancToken = new CancellationTokenSource();

                //var AddingTasks = mainForm.TabsPane.SelectedRowsLastDGV.OfType<DataGridViewRow>().Select(r => (int)r.Cells["Id"].Value).Select(r => AddRecordAsync(r, _cancToken.Token)).ToList();
                //var AddingTasks = mainForm.TabsPane[0, 1].SelectedCells.OfType<DataGridViewCell>().Select(c => c.RowIndex).Where(c => c >= 0).Distinct().Select(c => AddRecordAsync((int)mainForm.TabsPane[0,1].Rows[c].Cells["Id"].Value, _cancToken.Token)).ToList();

                ////await Task.WhenAll(AddingTasks).ConfigureAwait(false);
                //while (AddingTasks.Any())
                //{
                //    var completedTask = await Task.WhenAny(AddingTasks).ConfigureAwait(false);
                //    AddingTasks.Remove(completedTask);
                //}


                foreach (var i in mainForm.TabsPane[0, 1].SelectedCells.OfType<DataGridViewCell>().Select(c => c.RowIndex).Where(c => c >= 0).Distinct())
                    await AddRecordAsync((int)mainForm.TabsPane[0, 1].Rows[i].Cells["Id"].Value, _cancToken.Token);


            }
            catch (OperationCanceledException)
            {

            }
            finally
            {
                _cancToken = null;
                buttonAddSamplesToReg.Enabled = true;
            }


               

        }

        private async void ButtonCancel_Click(object sender, EventArgs e)
        {
            buttonCancel.Enabled = false;
            _cancToken?.Cancel();
            buttonCancel.Enabled = true;

        }

    } //public partial class MainForm
}     // namespace Regata.Desktop.WinForms.Measurements
