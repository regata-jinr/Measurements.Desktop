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
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Regata.Desktop.WinForms.Measurements
{
    public partial class MainForm
    {

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

            mainForm.TabsPane[1, 1].DataSource = null;

            var mRegId = mainForm.TabsPane[1, 0].SelectedCells[0].Value as int?;

            using (var r = new RegataContext())
            {

                CurrentMeasurementsRegister.IrradiationDate = r.MeasurementsRegisters.AsNoTracking().Where(m => m.Id == mRegId.Value).Select(m => m.IrradiationDate).FirstOrDefault();

                mainForm.TabsPane[1, 1].DataSource = await r.Measurements
                                                .AsNoTracking()
                                                .Where(
                                                         m =>
                                                                m.Type == (int)MeasurementsTypeItems.CheckedItem &&
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
                                                .ToArrayAsync();
            };
        }

        } //public static class SessionFormInit
}     // namespace Regata.Desktop.WinForms.Measurements
