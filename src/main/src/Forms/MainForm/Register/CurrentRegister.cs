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
using Microsoft.EntityFrameworkCore;


namespace Regata.Desktop.WinForms.Measurements
{
    public partial class MainForm
    {

        private void InitCurrentRegister()
        {

            using (var r = new RegataContext())
            {
                mainForm.MainRDGV.DataSource = r.Measurements.Where(m => m.RegId == 0).ToArray();
            }

            mainForm.Disposed += (s, e) =>
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
        }


        private void CreateNewMeasurementsRegister()
        {
            if (CurrentMeasurementsRegister.Type < 0) // || !CurrentMeasurementsRegister.IrradiationDate.HasValue)
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
