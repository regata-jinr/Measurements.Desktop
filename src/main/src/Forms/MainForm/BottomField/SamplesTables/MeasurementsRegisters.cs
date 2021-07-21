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
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Regata.Desktop.WinForms.Measurements
{
    public partial class MainForm
    {
        private List<Measurement> _selectedMeasurements;


        private void InitMeasurementsRegisters()
        {
            mainForm.TabsPane[1, 0].MultiSelect = false;


            #region Getting samples from selected measurement register

            mainForm.TabsPane[1, 0].SelectionChanged += async (e, s) =>
            {
                await FillSelectedMeasurements();
            };

            #endregion

        }

        private async Task FillMeasurementsRegisters()
        {


            using (var r = new RegataContext())
            {
                mainForm.TabsPane[1, 0].DataSource = await r.MeasurementsRegisters
                                                .AsNoTracking()
                                                .Where(m => m.Type == (int)MeasurementsTypeItems.CheckedItem && m.DateTimeStart != null)
                                                .Select(m => new { m.Id, m.LoadNumber, m.IrradiationDate })
                                                .Distinct()
                                                .OrderByDescending(m => m.IrradiationDate)
                                                .Take(20)
                                                .ToArrayAsync();
            }

       

            mainForm.TabsPane[1, 0].Columns[0].Visible = false;
        }

        private async Task FillSelectedMeasurements()
        {
            if (mainForm.TabsPane[1, 0].SelectedCells.Count <= 0) return;


            _selectedMeasurements.Clear();
            _selectedMeasurements.Capacity = 99;

            mainForm.TabsPane[1, 1].DataSource = null;

            var mRegId = mainForm.TabsPane[1, 0].SelectedCells[0].Value as int?;

            using (var r = new RegataContext())
            {

                CurrentMeasurementsRegister.IrradiationDate = r.MeasurementsRegisters.AsNoTracking().Where(m => m.Id == mRegId.Value).Select(m => m.IrradiationDate).FirstOrDefault();

                _selectedMeasurements.AddRange(await r.Measurements
                                                .AsNoTracking()
                                                .Where(
                                                         m =>
                                                                m.Type == (int)MeasurementsTypeItems.CheckedItem &&
                                                                m.RegId == mRegId.Value
                                                      )
                                                .ToArrayAsync());
            };

            //.Select(m => new
            //{
            //    m.CountryCode,
            //    m.ClientNumber,
            //    m.Year,
            //    m.SetNumber,
            //    m.SetIndex,
            //    m.SampleNumber,
            //    m.IrradiationId
            //}
            //)

            _selectedMeasurements.TrimExcess();

            mainForm.TabsPane[1, 1].DataSource = _selectedMeasurements;
        }

        } //public partial class MainForm
}     // namespace Regata.Desktop.WinForms.Measurements
