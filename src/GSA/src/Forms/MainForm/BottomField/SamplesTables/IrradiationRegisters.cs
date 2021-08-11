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
using RCM = Regata.Core.Messages;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Regata.Core.UI.WinForms;
using System.Windows.Forms;

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
            try
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
                    if (RowIsVisible(mainForm.TabsPane[0, 0].Rows[mainForm.TabsPane[0, 0].RowCount - 1]))
                        await FillIrradiationRegisters();
                };

                mainForm.TabsPane[0, 0].ResumeLayout(false);
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_INI_IRR_REGS)
                {
                    DetailedText = ex.ToString()
                });
            }
        }

        private bool RowIsVisible(DataGridViewRow row)
        {
            DataGridView dgv = row.DataGridView;
            int firstVisibleRowIndex = dgv.FirstDisplayedCell.RowIndex;
            int lastVisibleRowIndex = firstVisibleRowIndex + dgv.DisplayedRowCount(false) - 1;
            return row.Index >= firstVisibleRowIndex && row.Index <= lastVisibleRowIndex;
        }

        private async Task FillIrradiationRegisters()
        {
            try
            {
                var type = MeasurementsTypeItems.CheckedItem switch
                    {
                        MeasurementsType.sli => 0,
                        MeasurementsType.bckg => 3,
                        _ => 1
                    };

                using (var r = new RegataContext())
                {
                    mainForm.TabsPane[0, 0].DataSource = await r.Irradiations
                                                    .AsNoTracking()
                                                    .Where(ir => ir.Type == type && ir.DateTimeStart != null)
                                                    .Select(ir => new { ir.LoadNumber, ir.DateTimeStart.Value.Date })
                                                    .Distinct()
                                                    .OrderByDescending(i => i.Date)
                                                    .Take(mainForm.TabsPane[0, 0].RowCount + 20)
                                                    .ToArrayAsync();
                }

                mainForm.TabsPane[0, 0].FirstDisplayedScrollingRowIndex = mainForm.TabsPane[0, 0].RowCount - 20;
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_FILL_IRR_REGS)
                {
                    DetailedText = ex.ToString()
                });
            }
        }

        private async Task FillSelectedIrradiations()
        {
            try
            {
                if (mainForm.TabsPane[0, 0].SelectedCells.Count <= 0) return;

                mainForm.TabsPane[0, 1].DataSource = null;
                _chosenIrradiations.Clear();
                _chosenIrradiations.Capacity = 99;

                var date = mainForm.TabsPane[0, 0].SelectedCells[1].Value as DateTime?;

                if (!date.HasValue) return;

                CurrentMeasurementsRegister.IrradiationDate = date.Value;

                var type = MeasurementsTypeItems.CheckedItem switch
                {
                    MeasurementsType.sli => 0,
                    MeasurementsType.bckg => 3,
                    _ => 1
                };

                using (var r = new RegataContext())
                {
                    var query = r.Irradiations.AsNoTracking()
                                              .Where(
                                                    ir =>
                                                          ir.Type == type &&
                                                          ir.DateTimeStart != null &&
                                                          ir.DateTimeStart.Value.Date == date
                                                     );

                    var _tmpList = MeasurementsTypeItems.CheckedItem switch
                    {
                        MeasurementsType.sli => await query.OrderByDescending(ir => ir.DateTimeStart).ToArrayAsync(),
                                                //await query.OrderBy(ir => ir.Year)
                                                //           .ThenBy(ir => ir.CountryCode)
                                                //           .ThenBy(ir => ir.SetNumber)
                                                //           .ThenBy(ir => ir.SetIndex)
                                                //           .ThenBy(ir => ir.SampleNumber)
                                                //           .ToArrayAsync(),
                                                // for both: lli-1 and lli-2
                        _ => await query.OrderBy(ir => ir.Container)
                                                           .ThenBy(ir => ir.Position)
                                                           .ToArrayAsync()
                    };

                    _chosenIrradiations.AddRange(_tmpList);
                }

                mainForm.TabsPane[0, 1].DataSource = _chosenIrradiations;
                _chosenIrradiations.TrimExcess();
                HideIrradiationsRedundantColumns();
                Labels.SetControlsLabels(mainForm);

            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_FILL_SEL_IRR)
                {
                    DetailedText = ex.ToString()
                });
            }
        }

        private void HideIrradiationsRedundantColumns()
        {
            try
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
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_HIDE_IRR_COLS)
                {
                    DetailedText = ex.ToString()
    });
            }
        }

    } // public partial class MainForm
}     // namespace Regata.Desktop.WinForms.Measurements
