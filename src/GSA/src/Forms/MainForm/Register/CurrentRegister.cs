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
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;

namespace Regata.Desktop.WinForms.Measurements
{
    public partial class MainForm
    {

        private void AddRecord(int IrradiationId)
        {
            var ir = _regataContext.Irradiations.Where(i => i.Id == IrradiationId).FirstOrDefault();
            if (ir == null) return;

            var m = new Measurement(ir);
            m.AcqMode = (int)AcquisitionModeItems.CheckedItem;
            m.RegId = CurrentMeasurementsRegister.Id;
            m.Duration = (int)DurationControl.Duration.TotalSeconds;
            m.Detector = CheckedAvailableDetectorArrayControl.SelectedItem;
            m.Height = CheckedHeightArrayControl.SelectedItem;
            _regataContext.Measurements.Add(m);
            _regataContext.SaveChanges();
        }

        private void RemoveRecord(int id)
        {
            var m = _regataContext.Measurements.Where(i => i.Id == id).FirstOrDefault();
            if (m == null) return;
            _regataContext.Measurements.Remove(m);
            _regataContext.SaveChanges();
        }

        private async Task AddRecordAsync(int id, CancellationToken _ct)
        {
            using (var rc = new RegataContext())
            {
                var m = new Measurement(await rc.Irradiations.Where(i => i.Id == id).FirstOrDefaultAsync());
                m.RegId = CurrentMeasurementsRegister.Id;
                await rc.Measurements.AddAsync(m);
                await rc.SaveChangesAsync(_ct);
                _regataContext.Measurements.Where(mm => mm.RegId == CurrentMeasurementsRegister.Id).Load();
            }
        }

        private async Task RemoveRecordAsync(int id, CancellationToken _ct)
        {
            throw new NotImplementedException("Here is the problem related with _regataContext call from different threads.");

            try
            {
                var m = _regataContext.Measurements.Where(m => m.Id == id).FirstOrDefault();

                if (m == null) return;

                _regataContext.Measurements.Remove(m);
                await _regataContext.SaveChangesAsync(_ct);
            }
            catch (Exception e)
            {
                var ee = e;
            }
        }

        private async Task RemoveSelectedRecordsAsync(CancellationToken _ct)
        {
            try
            {
                if (mainForm.MainRDGV.SelectedCells.Count <= 0) return;
                _cancToken = new CancellationTokenSource();

                var RemovingTasks = mainForm.MainRDGV.SelectedCells.OfType<DataGridViewCell>().Select(c => c.RowIndex).Where(c => c >= 0).Distinct().Select(c => RemoveRecordAsync((int)mainForm.MainRDGV.Rows[c].Cells["Id"].Value, _cancToken.Token)).ToList();

                while (RemovingTasks.Any())
                {
                    //await Task.Delay(TimeSpan.FromSeconds(5), _cancToken.Token);
                    var completedTask = await Task.WhenAny(RemovingTasks);
                    if (completedTask.IsCanceled) break;
                    if (!completedTask.IsCompleted)
                        completedTask.Start();
                    if (!completedTask.IsFaulted)
                        RemovingTasks.ToList().Remove(completedTask);
                }

                //mainForm.MainRDGV.ClearSelection();
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception e)
            {
                var ee = e;
            }
            finally
            {
                _cancToken = null;
            }
        }

        private void ClearCurrentRegister()
        {
            _regataContext.Measurements.Local.Clear();
            _regataContext.SaveChanges();
        }

        private async Task ClearCurrentRegisterAsync()
        {
        }

        private void InitCurrentRegister()
        {
            CreateNewMeasurementsRegister();
            _regataContext.Measurements.Where(m => m.Id == 0).Load();
            mainForm.MainRDGV.DataSource = _regataContext.Measurements.Local.ToBindingList();

            CurrentMeasurementsRegister.PropertyChanged += (s,e) => { UpdateCurrentReigster(); };

            //HideMainRDGVRedundantColumns();

            mainForm.Disposed += (s, e) =>
            {
                // running creates measurement register.
                // in case of after disposing the form there are not measurements records for register the last one will be deleted
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
                _regataContext.Dispose();
            };
        }

        private void HideMainRDGVRedundantColumns()
        {
            mainForm.MainRDGV.Columns["Id"].Visible = false;
            mainForm.MainRDGV.Columns["IrradiationId"].Visible = false;
            mainForm.MainRDGV.Columns["RegId"].Visible = false;
        }

        private void UpdateCurrentReigster()
        {
            using (var r = new RegataContext())
            {
                r.MeasurementsRegisters.Update(CurrentMeasurementsRegister);
                r.SaveChanges();
            }
        }

        private void CreateNewMeasurementsRegister()
        {
            using (var r = new RegataContext())
            {
                r.MeasurementsRegisters.Add(CurrentMeasurementsRegister);
                r.SaveChanges();
            }
        }

    } //public partial class MainForm
}     // namespace Regata.Desktop.WinForms.Measurements
