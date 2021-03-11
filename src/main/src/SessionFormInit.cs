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
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace Regata.Desktop.WinForms.Measurements
{
    public static class SessionFormInit
    {

        public static MeasurementsRegister CurrentMeasurementsRegister;

        public static RegisterForm<Measurement> GetRegisterForm()
        {
            Core.Report.LogDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "test", "MeasurementsDesktop");
            var rf = new RegisterForm<Measurement>();
            CurrentMeasurementsRegister = new MeasurementsRegister() { Type = -1, IrradiationDate = null, Id = 0};

            using (var r = new RegataContext())
            {
                rf.MainRDGV.DataSource = r.Measurements.Where(m => m.RegId == 0).ToArray();
            }

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

                CurrentMeasurementsRegister.Type = (int)type_items.CheckedItem;

                using (var r = new RegataContext())
                {
                    rf.MainRDGV.DataSource = r.Measurements.Where(m => m.RegId == 0).ToArray();

                    rf.TabsPane[0, 0].DataSource = r.Irradiations
                                                    .AsNoTracking()
                                                    .Where(ir => ir.Type == (int)type_items.CheckedItem && ir.DateTimeStart != null)
                                                    .Select(ir => new { ir.LoadNumber, ir.DateTimeStart.Value.Date })
                                                    .Distinct()
                                                    .OrderByDescending(i => i.Date)
                                                    .ToArray();

                    rf.TabsPane[1, 0].DataSource = r.MeasurementsRegisters
                                                    .AsNoTracking()
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

                CurrentMeasurementsRegister.IrradiationDate = date;

                using (var r = new RegataContext())
                {
                    rf.TabsPane[0, 1].DataSource = r.Irradiations
                                                    .AsNoTracking()
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
                                                        ir.Id,
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

                    CurrentMeasurementsRegister.IrradiationDate = r.MeasurementsRegisters.AsNoTracking().Where(m => m.Id == mRegId.Value).Select(m=>m.IrradiationDate).FirstOrDefault();

                    rf.TabsPane[1, 1].DataSource = r.Measurements
                                                    .AsNoTracking()
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
                                                        m.SampleNumber,
                                                        m.IrradiationId
                                                    }
                                                           )
                                                    .ToArray();
                };
            };

            #endregion

            rf.buttonAddAllSamples.Click += (s, e) => 
            {
                var sr = rf.TabsPane.SelectedRowsFirstDGV;
                if (sr.Count <= 0) return;
                var ind = rf.TabsPane.Pages.IndexOf(rf.TabsPane.ActiveTabPage);

                CreateNewMeasurementsRegister();

                using (var r = new RegataContext())
                {
                    var meas = new List<Measurement>();

                    foreach (DataGridViewRow row in rf.TabsPane[ind,1].Rows)
                    {
                        meas.Add(
                                new Measurement()
                                {
                                    RegId         = CurrentMeasurementsRegister.Id,
                                    Type          =  CurrentMeasurementsRegister.Type,
                                    IrradiationId = (int)row.Cells[6].Value,
                                    CountryCode   = row.Cells[0].Value.ToString(),
                                    ClientNumber  = row.Cells[1].Value.ToString(),
                                    Year          = row.Cells[2].Value.ToString(),
                                    SetNumber     = row.Cells[3].Value.ToString(),
                                    SetIndex      = row.Cells[4].Value.ToString(),
                                    SampleNumber  = row.Cells[5].Value.ToString()
                                }
                                );
                    }
                    r.Measurements.AddRange(meas);
                    r.SaveChanges();
                    rf.MainRDGV.DataSource = meas;
                }

            };

            rf.Disposed += (s, e) => 
            {
                // adding samples creates measurement register. in case of after disposing the form there are not measurements records for register the last one will be deleted
                using (var r = new RegataContext())
                {
                    if (
                            !r.Measurements.AsNoTracking().Where(m => m.RegId == CurrentMeasurementsRegister.Id).Any() &&
                            r.MeasurementsRegisters.AsNoTracking().Where(m => m.Id == CurrentMeasurementsRegister.Id).Any()
                       )
                    {
                        r.MeasurementsRegisters.Remove(CurrentMeasurementsRegister);
                        r.SaveChanges();
                    }
                }
            };

            return rf;

        } // public static RegisterForm<Measurement> GetRegisterForm()


        private static void CreateNewMeasurementsRegister()
        {
            if (CurrentMeasurementsRegister.Type < 0 || !CurrentMeasurementsRegister.IrradiationDate.HasValue)
                return;


            using (var r = new RegataContext())
            {
                if (r.MeasurementsRegisters.AsNoTracking().Where(m => m.Id == CurrentMeasurementsRegister.Id).Any())
                    return;

                r.MeasurementsRegisters.Add(CurrentMeasurementsRegister);
                r.SaveChanges();
            }
        }

    } //public static class SessionFormInit
}     // namespace Regata.Desktop.WinForms.Measurements
