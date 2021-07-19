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

using Regata.Core.DataBase;
using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Regata.Desktop.WinForms.Measurements
{
    public partial class MainForm
    {
        private void InitIrradiationsRegisters()
        {
            mainForm.TabsPane[0, 0].MultiSelect = false;

            #region Getting samples from selected irradiation register

            mainForm.TabsPane[0, 0].SelectionChanged += async (e, s) =>
            {
                await FillSelectedIrradiations();
            };

            #endregion

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
                                                .Take(20)
                                                .ToArrayAsync();
            }
        }

        private async Task FillSelectedIrradiations()
        {
            if (mainForm.TabsPane[0, 0].SelectedCells.Count <= 0) return;

            mainForm.TabsPane[0, 1].DataSource = null;

            var date = mainForm.TabsPane[0, 0].SelectedCells[1].Value as DateTime?;

            CurrentMeasurementsRegister.IrradiationDate = date.Value;

            using (var r = new RegataContext())
            {
                mainForm.TabsPane[0, 1].DataSource = await r.Irradiations
                                                .AsNoTracking()
                                                .Where(
                                                         ir =>
                                                                ir.Type == (int)MeasurementsTypeItems.CheckedItem &&
                                                                ir.DateTimeStart != null &&
                                                                ir.DateTimeStart.Value.Date == date
                                                      )
                                                .Select(ir => new
                                                {
                                                    ir.CountryCode,
                                                    ir.ClientNumber,
                                                    ir.Year,
                                                    ir.SetNumber,
                                                    ir.SetIndex,
                                                    ir.SampleNumber,
                                                    ir.Id,
                                                    ir.Container,
                                                    ir.Position
                                                }
                                                       )
                                                .OrderBy(ir => ir.Container)
                                                .ThenBy(ir => ir.Position)
                                                .ToArrayAsync();
            }
        }

    } //public static class SessionFormInit
}     // namespace Regata.Desktop.WinForms.Measurements
