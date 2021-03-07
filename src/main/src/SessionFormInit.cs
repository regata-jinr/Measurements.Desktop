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

using Regata.Core.UI.WinForms.Forms;
using Regata.Core.UI.WinForms.Items;
using Regata.Core.DB.MSSQL.Models;
using Regata.Core.DB.MSSQL.Context;
using System.Linq;
using System;

namespace Regata.Desktop.WinForms.Measurements
{
    public static class SessionFormInit
    {
        public static RegisterForm<Measurement> GetRegisterForm()
        {
            var rf = new RegisterForm<Measurement>();

            var type_items = new EnumItem<MeasurementsType>();

            rf.MenuStrip.Items.Add(type_items.EnumMenuItem);
            rf.StatusStrip.Items.Add(type_items.EnumStatusLabel);


            rf.TabsPane[0, 0].MultiSelect = false;
            rf.TabsPane[1, 0].MultiSelect = false;

            #region Filling list of registers

            type_items.CheckedChanged += () =>
            {
                rf.TabsPane[0, 0].DataSource = null;
                rf.TabsPane[1, 0].DataSource = null;
                rf.TabsPane[0, 1].DataSource = null;
                rf.TabsPane[1, 1].DataSource = null;

                using (var r = new RegataContext())
                {

                    rf.TabsPane[0, 0].DataSource = r.Irradiations
                                                    .Where(ir => ir.Type == (int)type_items.CheckedItem && ir.DateTimeStart != null)
                                                    .Select(ir => new { ir.LoadNumber, ir.DateTimeStart.Value.Date })
                                                    .Distinct()
                                                    .OrderByDescending(i => i.Date)
                                                    .ToArray();

                    rf.TabsPane[1, 0].DataSource = r.MeasurementsRegisters
                                                    .Where(m => m.Type == (int)type_items.CheckedItem && m.DateTimeStart != null)
                                                    .Select(m => new { m.Id, m.LoadNumber, m.IrradiationDate.Value.Date })
                                                    .Distinct()
                                                    .OrderByDescending(i => i.Date)
                                                    .ToArray();
                }
                rf.TabsPane[1, 0].Columns[0].Visible = false;
            };



            #endregion

            #region Getting samples from selected irradiation register

            rf.TabsPane[0, 0].SelectionChanged += (e, s) =>
            {
                if (rf.TabsPane[0, 0].SelectedCells.Count <= 0) return;

                rf.TabsPane[0, 1].DataSource = null;

                var date = rf.TabsPane[0, 0].SelectedCells[1].Value as DateTime?;

                using (var r = new RegataContext())
                {
                    rf.TabsPane[0, 1].DataSource = r.Irradiations
                                                    .Where(
                                                             ir =>
                                                                    ir.Type == (int)type_items.CheckedItem &&
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
                                                        ir.Container,
                                                        ir.Position
                                                    }
                                                           )
                                                    .OrderBy(ir => ir.Container)
                                                    .ThenBy(ir => ir.Position)
                                                    .ToArray();
                }
            };

            #endregion

            #region Getting samples from selected measurement register

            rf.TabsPane[1, 0].SelectionChanged += (e, s) =>
            {
                if (rf.TabsPane[1, 0].SelectedCells.Count <= 0) return;

                rf.TabsPane[1, 1].DataSource = null;

                var mRegId = rf.TabsPane[1, 0].SelectedCells[0].Value as int?;

                using (var r = new RegataContext())
                {
                    rf.TabsPane[1, 1].DataSource = r.Measurements
                                                    .Where(
                                                             m =>
                                                                    m.Type == (int)type_items.CheckedItem &&
                                                                    m.RegId == mRegId.Value
                                                          )
                                                    .Select(m => new
                                                    {
                                                        m.CountryCode,
                                                        m.ClientNumber,
                                                        m.Year,
                                                        m.SetNumber,
                                                        m.SetIndex,
                                                        m.SampleNumber
                                                    }
                                                           )
                                                    .ToArray();
                };
            };

            #endregion

            return rf;

        } // public static RegisterForm<Measurement> GetRegisterForm()

    } //public static class SessionFormInit
}     // namespace Regata.Desktop.WinForms.Measurements
