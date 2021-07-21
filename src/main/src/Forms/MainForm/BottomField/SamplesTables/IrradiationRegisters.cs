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
using Regata.Core.DataBase.Models;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Regata.Desktop.WinForms.Measurements
{
    public partial class MainForm
    {

        private List<Irradiation> _selectedIrradiations;

        private void InitIrradiationsRegisters()
        {
            mainForm.TabsPane[0, 0].MultiSelect = false;

            //mainForm.TabsPane[0, 1].DataSource = _selectedIrradiations;

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
            _selectedIrradiations.Clear();
            _selectedIrradiations.Capacity = 99;
            
            var date = mainForm.TabsPane[0, 0].SelectedCells[1].Value as DateTime?;

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

                _selectedIrradiations.AddRange(_tmpList);

                mainForm.TabsPane[0, 1].DataSource = _selectedIrradiations;

                _selectedIrradiations.TrimExcess();

                //mainForm.TabsPane[0, 1].DataSource = await r.Irradiations
                //                                .AsNoTracking()
                //                                .Where(
                //                                         ir =>
                //                                                ir.Type == (int)MeasurementsTypeItems.CheckedItem &&
                //                                                ir.DateTimeStart != null &&
                //                                                ir.DateTimeStart.Value.Date == date
                //                                      )
                //                                .Select(ir => new
                //                                {
                //                                    ir.CountryCode,
                //                                    ir.ClientNumber,
                //                                    ir.Year,
                //                                    ir.SetNumber,
                //                                    ir.SetIndex,
                //                                    ir.SampleNumber,
                //                                    ir.Id,
                //                                    ir.Container,
                //                                    ir.Position
                //                                }
                //                                       )
                //                                .OrderBy(ir => ir.Container)
                //                                .ThenBy(ir => ir.Position)
                //                                .ToArrayAsync();
            }

        }

    } //public partial class MainForm
}     // namespace Regata.Desktop.WinForms.Measurements
