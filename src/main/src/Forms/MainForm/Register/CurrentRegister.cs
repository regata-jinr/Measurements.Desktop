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
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace Regata.Desktop.WinForms.Measurements
{
    public partial class MainForm
    {
        private List<Measurement> _measurementsList;

        private async Task AddRecordAsync(int id)
        {
            using (var r = new RegataContext())
            {
                var m = new Measurement(r.Irradiations.Where(i => i.Id == id).FirstOrDefault());
                _measurementsList.Add(m);
                await r.SaveChangesAsync();
            }
        }

        private async Task AddRecordAsync(Measurement m)
        {
            using (var r = new RegataContext())
            {
                _measurementsList.Add(m);
                await r.SaveChangesAsync();
            }
        }

        private async Task AddRecordsAsync(IEnumerable<Measurement> ms)
        {
            using (var r = new RegataContext())
            {
                _measurementsList.AddRange(ms);
                await r.SaveChangesAsync();
            }
        }


        private async Task RemoveRecordAsync(Measurement m)
        {
            using (var r = new RegataContext())
            {
                _measurementsList.Remove(m);
                await r.SaveChangesAsync();
            }
        }

        private async Task RemoveRecordsAsync(IEnumerable<Measurement> ms)
        {
            using (var r = new RegataContext())
            {
                _measurementsList.RemoveAll(m => ms.Select(msm => msm.Id).ToArray().Contains(m.Id));
                await r.SaveChangesAsync();
            }
        }

        private async Task ClearCurrentRegisterAsync()
        {
            using (var r = new RegataContext())
            {
                _measurementsList.Clear();
                await r.SaveChangesAsync();
            }
        }

        private async Task RemoveRecordAsync(int id)
        {
            var m = _measurementsList.Where(m => m.Id == id).FirstOrDefault();

            if (m == null) return;

            using (var r = new RegataContext())
            {
                _measurementsList.Remove(m);
                await r.SaveChangesAsync();
            }
        }


        private void InitCurrentRegister()
        {

            _measurementsList = new List<Measurement>();

            using (var r = new RegataContext())
            {
                mainForm.MainRDGV.DataSource = _measurementsList;
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

    } //public partial class MainForm
}     // namespace Regata.Desktop.WinForms.Measurements
