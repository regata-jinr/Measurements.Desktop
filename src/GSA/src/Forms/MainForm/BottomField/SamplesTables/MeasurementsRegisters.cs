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

namespace Regata.Desktop.WinForms.Measurements
{
    public partial class MainForm
    {
        /// <summary>
        /// List of Measurements from selected measurements register in TabPane
        /// 
        /// </summary>
        private List<Measurement> _chosenMeasurements;


        private void InitMeasurementsRegisters()
        {
            try
            {
                mainForm.TabsPane[1, 0].SuspendLayout();

                mainForm.TabsPane[1, 0].MultiSelect = false;

                mainForm.TabsPane[1, 0].SelectionChanged += async (e, s) =>
                {
                    await FillSelectedMeasurements();
                    if (mainForm.TabsPane[1, 0].SelectedRows.Count > 0 && mainForm.TabsPane[1, 0].SelectedRows[0].Cells[2].Value != null)
                        CurrentMeasurementsRegister.IrradiationDate = (DateTime)mainForm.TabsPane[1, 0].SelectedRows[0].Cells[2].Value;
                };

                mainForm.TabsPane[1, 0].Scroll += async (s, e) =>
                {
                    if (RowIsVisible(mainForm.TabsPane[1, 0].Rows[mainForm.TabsPane[1, 0].RowCount - 1]))
                        await FillMeasurementsRegisters();
                };

                mainForm.TabsPane[1, 0].ResumeLayout(false);
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_INI_MEAS_REGS)
                {
                    DetailedText = ex.ToString()
                });
            }
        }

        private async Task FillMeasurementsRegisters()
        {
            try
            {
                using (var r = new RegataContext())
                {
                    mainForm.TabsPane[1, 0].DataSource = await r.MeasurementsRegisters
                                                    .AsNoTracking()
                                                    .Where(m => m.Type == (int)MeasurementsTypeItems.CheckedItem && m.DateTimeStart.HasValue)
                                                    .Select(m => new { m.Id, m.LoadNumber, m.IrradiationDate })
                                                    .Distinct()
                                                    .OrderByDescending(m => m.Id)
                                                    .Take(mainForm.TabsPane[1, 0].RowCount + 20)
                                                    .ToArrayAsync();
                    

                }


                mainForm.TabsPane[1, 0].FirstDisplayedScrollingRowIndex = mainForm.TabsPane[1, 0].RowCount - 20; ;

                //mainForm.TabsPane[1, 0].Columns[0].Visible = false;
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_FILL_MEAS_REGS)
                {
                    DetailedText = ex.ToString()
                });
            }
        }

        private async Task FillSelectedMeasurements()
        {
            try
            {
                if (mainForm.TabsPane[1, 0].SelectedCells.Count <= 0) return;

                _chosenMeasurements.Clear();
                _chosenMeasurements.Capacity = 499;

                mainForm.TabsPane[1, 1].DataSource = null;

                var mRegId = mainForm.TabsPane[1, 0].SelectedCells[0].Value as int?;
                //var date   = mainForm.TabsPane[1, 0].SelectedCells[2].Value as DateTime?;

                if (!mRegId.HasValue) return;

                using (var r = new RegataContext())
                {

                    CurrentMeasurementsRegister.IrradiationDate = r.MeasurementsRegisters.AsNoTracking().Where(m => m.Id == mRegId.Value).Select(m => m.IrradiationDate).FirstOrDefault();

                    _chosenMeasurements.AddRange(await r.Measurements
                                                    .AsNoTracking()
                                                    .Where(
                                                             m =>
                                                                    m.Type == (int)MeasurementsTypeItems.CheckedItem &&
                                                                    m.RegId == mRegId.Value
                                                          )
                                                    .ToArrayAsync());
                };

                _chosenMeasurements.TrimExcess();
                mainForm.TabsPane[1, 1].DataSource = _chosenMeasurements;
                HideMeasurementsRedundantColumns();
                Labels.SetControlsLabels(mainForm);
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_FILL_SEL_MEAS)
                {
                    DetailedText = ex.ToString()
                });
            }
        }

        private void HideMeasurementsRedundantColumns()
        {
            try
            {
                if (mainForm.TabsPane[1, 1].Columns.Count <= 0) return;

                mainForm.TabsPane[1, 1].Columns["Id"].Visible = false;
                mainForm.TabsPane[1, 1].Columns["IrradiationId"].Visible = false;
                mainForm.TabsPane[1, 1].Columns["RegId"].Visible = false;
                mainForm.TabsPane[1, 1].Columns["Type"].Visible = false;
                mainForm.TabsPane[1, 1].Columns["DateTimeStart"].Visible = false;
                mainForm.TabsPane[1, 1].Columns["Duration"].Visible = false;
                mainForm.TabsPane[1, 1].Columns["DateTimeFinish"].Visible = false;
                mainForm.TabsPane[1, 1].Columns["AcqMode"].Visible = false;
                mainForm.TabsPane[1, 1].Columns["DiskPosition"].Visible = false;
                mainForm.TabsPane[1, 1].Columns["Height"].Visible = false;
                mainForm.TabsPane[1, 1].Columns["DeadTime"].Visible = false;
                mainForm.TabsPane[1, 1].Columns["FileSpectra"].Visible = false;
                mainForm.TabsPane[1, 1].Columns["Detector"].Visible = false;
                mainForm.TabsPane[1, 1].Columns["Assistant"].Visible = false;
                mainForm.TabsPane[1, 1].Columns["Note"].Visible = false;
                mainForm.TabsPane[1, 1].Columns["SetKey"].Visible = false;
                mainForm.TabsPane[1, 1].Columns["SampleKey"].Visible = false;
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_HIDE_MEAS_COLS)
                {
                    DetailedText = ex.ToString()
                });
            }
        }

    } // public partial class MainForm
}     // namespace Regata.Desktop.WinForms.Measurements
