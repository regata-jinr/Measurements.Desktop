﻿/***************************************************************************
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
using Regata.Core.DataBase;
using Regata.Core.DataBase.Models;
using Regata.Core.UI.WinForms.Controls;
using Regata.Core.UI.WinForms.Forms;
using System;
using System.Collections.Generic;
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
            buttonRemoveSelectedSamples = new Button() { AutoSize = false, Dock = DockStyle.Fill, Name = "buttonRemoveSelectedSamples", Enabled = false };
            buttonAddSelectedSamplesToReg = new Button() { AutoSize = false, Dock = DockStyle.Fill, Name = "buttonAddSelectedSamplesToReg", Enabled = false };
            buttonClearRegister = new Button() { AutoSize = false, Dock = DockStyle.Fill, Name = "buttonClearRegister", Enabled = false };
            buttonAddAllSamples = new Button() { AutoSize = false, Dock = DockStyle.Fill, Name = "buttonAddAllSamples", Enabled = false };
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
            if (MeasurementsTypeItems.CheckedItem == MeasurementsType.sli)
            {

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
            else
            {
                var ln = (int?)mainForm.TabsPane[cti, 0].SelectedCells[0].Value;
                if (!ln.HasValue) return;
                if (_circleDetArray == null || _circleDetArray.Length == 0) return;
                var sf = new ContainersToDetectorsForm(_circleDetArray.ToArray(), ln.Value);
                sf.Show();
                sf.buttonExportToCSV.Visible = false;
                sf.buttonExportToExcel.Visible = false;
                sf.buttonFillMeasurementRegister.Click += (s, e) => { ClearCurrentRegister(); AddAllIrradiationsAndAssignDiskPosition(ln.Value, sf.DetCont); };
            }

        }

        private void AddAllIrradiationsAndAssignDiskPosition(int loadNumber, Dictionary<string, int[]> det_cont)
        {
            using (var r = new RegataContext())
            {
                foreach (var d in det_cont)
                {
                int currPosition = 0;
                    foreach (var ir in r.Irradiations.AsNoTracking().Where(ir => ir.LoadNumber == loadNumber && d.Value.ToList().Contains(ir.Container.Value)).OrderBy(ir => ir.Container).ThenBy(ir => ir.Position))
                    {
                        AddRecord(ir.Id, d.Key, ++currPosition);
                    }
                }
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
