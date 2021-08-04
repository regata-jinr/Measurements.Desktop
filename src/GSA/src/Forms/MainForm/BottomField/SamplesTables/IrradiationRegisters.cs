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

using Regata.Core.DataBase;
using Regata.Core.DataBase.Models;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Regata.Core.UI.WinForms;

namespace Regata.Desktop.WinForms.Measurements
{
    public partial class MainForm
    {
        /// <summary>
        /// List of Irradiations from selected irradiation register in TabPane
        /// </summary>
        private List<Irradiation> _chosenIrradiations;

        private void InitIrradiationsRegisters()
        {
            mainForm.TabsPane[0, 0].SuspendLayout();
            mainForm.TabsPane[0, 0].MultiSelect = false;

            mainForm.TabsPane[0, 0].SelectionChanged += async (e, s) =>
            {
                await FillSelectedIrradiations();
                if (mainForm.TabsPane[0, 0].SelectedRows.Count > 0)
                    CurrentMeasurementsRegister.IrradiationDate = (DateTime)mainForm.TabsPane[0, 0].SelectedRows[0].Cells[1].Value;
            };


            mainForm.TabsPane[0, 0].Scroll += async (s, e) =>
            {
                if (e.NewValue >= mainForm.TabsPane[0, 0].RowCount - 10)
                    await FillIrradiationRegisters();
            };


            mainForm.TabsPane[0, 0].ResumeLayout(false);

        }

        private async Task FillIrradiationRegisters()
        {
            using (var r = new RegataContext())
            {
                mainForm.TabsPane[0, 0].DataSource = await r.Irradiations
                                                .AsNoTracking()
                                                .Where(ir => ir.Type == (int)MeasurementsTypeItems.CheckedItem && ir.DateTimeStart != null)
                                                .Select(ir => new { ir.LoadNumber, ir.DateTimeStart.Value.Date })
                                                .Distinct()
                                                .OrderByDescending(i => i.Date)
                                                .Take(mainForm.TabsPane[0, 0].RowCount + 20)
                                                .ToArrayAsync();
            }

            mainForm.TabsPane[0, 0].FirstDisplayedScrollingRowIndex = mainForm.TabsPane[0, 0].RowCount - 20;
        }

        private async Task FillSelectedIrradiations()
        {
            if (mainForm.TabsPane[0, 0].SelectedCells.Count <= 0) return;

            mainForm.TabsPane[0, 1].DataSource = null;
            _chosenIrradiations.Clear();
            _chosenIrradiations.Capacity = 99;
            
            var date = mainForm.TabsPane[0, 0].SelectedCells[1].Value as DateTime?;

            if (!date.HasValue) return;

            CurrentMeasurementsRegister.IrradiationDate = date.Value;

            using (var r = new RegataContext())
            {
                var query = r.Irradiations.AsNoTracking()
                                          .Where(
                                                ir =>
                                                      ir.Type == (int)MeasurementsTypeItems.CheckedItem &&
                                                      ir.DateTimeStart != null &&
                                                      ir.DateTimeStart.Value.Date == date
                                                 );

                var _tmpList = MeasurementsTypeItems.CheckedItem switch
                {
                    MeasurementsType.sli => await query.OrderBy(ir => ir.Year)
                                                       .ThenBy(ir => ir.CountryCode)
                                                       .ThenBy(ir => ir.SetNumber)
                                                       .ThenBy(ir => ir.SetIndex)
                                                       .ThenBy(ir => ir.SampleNumber)
                                                       .ToArrayAsync(),
                    // for both: lli-1 and lli-2
                    _                    => await query.OrderBy(ir => ir.Container)
                                                       .ThenBy(ir => ir.Position)
                                                       .ToArrayAsync()
                };

                _chosenIrradiations.AddRange(_tmpList);

                mainForm.TabsPane[0, 1].DataSource = _chosenIrradiations;
                _chosenIrradiations.TrimExcess();
                HideIrradiationsRedundantColumns();
                Labels.SetControlsLabels(mainForm);

            }
        }

        private void HideIrradiationsRedundantColumns()
        {
            if (mainForm.TabsPane[0, 1].Columns.Count <= 0) return;

            mainForm.TabsPane[0, 1].Columns["Id"].Visible = false;
            mainForm.TabsPane[0, 1].Columns["Type"].Visible = false;
            mainForm.TabsPane[0, 1].Columns["DateTimeStart"].Visible = false;
            mainForm.TabsPane[0, 1].Columns["Duration"].Visible = false;
            mainForm.TabsPane[0, 1].Columns["DateTimeFinish"].Visible = false;
            mainForm.TabsPane[0, 1].Columns["Container"].Visible = false;
            mainForm.TabsPane[0, 1].Columns["Position"].Visible = false;
            mainForm.TabsPane[0, 1].Columns["Channel"].Visible = false;
            mainForm.TabsPane[0, 1].Columns["LoadNumber"].Visible = false;
            mainForm.TabsPane[0, 1].Columns["Rehandler"].Visible = false;
            mainForm.TabsPane[0, 1].Columns["Assistant"].Visible = false;
            mainForm.TabsPane[0, 1].Columns["Note"].Visible = false;
            mainForm.TabsPane[0, 1].Columns["SetKey"].Visible = false;
            mainForm.TabsPane[0, 1].Columns["SampleKey"].Visible = false;
        }

    } // public partial class MainForm
}     // namespace Regata.Desktop.WinForms.Measurements