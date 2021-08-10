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

using Microsoft.EntityFrameworkCore;
using Regata.Core;
using Regata.Core.Collections;
using Regata.Core.DataBase;
using Regata.Core.DataBase.Models;
using RCM = Regata.Core.Messages;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Regata.Desktop.WinForms.Measurements
{
    public partial class MainForm
    {
        private CircleArray<string> _circleDetArray;

        private void AddRecord(int IrradiationId)
        {
            try
            {
                Measurement m = null;
                using (var r = new RegataContext())
                {
                    var ir = r.Irradiations.Where(i => i.Id == IrradiationId).FirstOrDefault();
                    if (ir == null) return;

                    m = new Measurement(ir);
                }
                    m.AcqMode = (int)AcquisitionModeItems.CheckedItem;
                    m.RegId = CurrentMeasurementsRegister.Id;
                    m.Duration = (int)DurationControl.Duration.TotalSeconds;

                    m.Detector = MeasurementsTypeItems.CheckedItem switch
                    {
                        MeasurementsType.sli => _circleDetArray?.Current,
                        _ => CheckedAvailableDetectorArrayControl.SelectedItem
                    };

                    _circleDetArray?.MoveForward();

                    m.Height = CheckedHeightArrayControl.SelectedItem;
                    mainForm.MainRDGV.Add(m);
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_ADD_REC)
                {
                    DetailedText = ex.ToString()
                });
            }
        }

        private void RemoveRecord(int id)
        {
            try
            {
                var m = mainForm.MainRDGV.CurrentDbSet.Where(i => i.Id == id).FirstOrDefault();
                if (m == null) return;

                mainForm.MainRDGV.Remove(m);
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_REM_REC)
                {
                    DetailedText = ex.ToString()
                });
            }
        }


        private void ClearCurrentRegister()
        {
            try
            {
                mainForm.MainRDGV.Clear();
                mainForm.MainRDGV.SaveChanges();
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_CLR_CUR_REG)
                {
                    DetailedText = ex.ToString()
                });
            }
        }

        private void InitCurrentRegister()
        {
            try
            {
                CreateNewMeasurementsRegister();
                mainForm.MainRDGV.CurrentDbSet.Where(m => m.Id == 0).Load();
                mainForm.MainRDGV.DataSource = mainForm.MainRDGV.CurrentDbSet.Local.ToBindingList();

                mainForm.MainRDGV.HideColumns();

                mainForm.MainRDGV.SetUpWritableColumns();

                mainForm.Disposed += (s, e) =>
                {
                    try
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
                    }
                    catch (Exception ex)
                    {
                        Report.Notify(new RCM.Message(Codes.ERR_UI_WF_MAIN_FORM_DISP)
                        {
                            DetailedText = ex.ToString()
                        });
                    }
                };
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_INIT_CUR_REG)
                {
                    DetailedText = ex.ToString()
                });
            }
        }


        private async Task UpdateCurrentReigster()
        {
            try
            {
                using (var r = new RegataContext())
                {
                   var ir  =  r.Irradiations.Where(ir => ir.Id == mainForm.MainRDGV.CurrentDbSet.Local.Select(m => m.IrradiationId).Min()).FirstOrDefault();
                    CurrentMeasurementsRegister.IrradiationDate = ir.DateTimeStart.Value.Date;
                    CurrentMeasurementsRegister.LoadNumber = ir.LoadNumber;
                    CurrentMeasurementsRegister.DateTimeStart = mainForm.MainRDGV.CurrentDbSet.Local.Select(m => m.DateTimeStart).Min();
                    CurrentMeasurementsRegister.DateTimeFinish = mainForm.MainRDGV.CurrentDbSet.Local.Select(m => m.DateTimeFinish).Max();
                    CurrentMeasurementsRegister.SamplesCnt = mainForm.MainRDGV.CurrentDbSet.Local.Where(m => m.FileSpectra != null).Count();
                    CurrentMeasurementsRegister.Detectors = string.Join(',', mainForm.MainRDGV.CurrentDbSet.Local.Select(m => m.Detector).Distinct().ToArray());
                    // CurrentMeasurementsRegister.Assistant = mainForm.MainRDGV.CurrentDbSet.Local.Where(m => m.Assistant.HasValue).FirstOrDefault().Assistant;

                    r.MeasurementsRegisters.Update(CurrentMeasurementsRegister);
                    await r.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_UPD_CUR_MEAS_REG)
                {
                    DetailedText = ex.ToString()
                });
            }
        }

        private void CreateNewMeasurementsRegister()
        {
            try
            {
                using (var r = new RegataContext())
                {
                    r.MeasurementsRegisters.Add(CurrentMeasurementsRegister);
                    r.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_CRT_MEAS_REG)
                {
                    DetailedText = ex.ToString()
                });
            }
        }

    } // public partial class MainForm
}     // namespace Regata.Desktop.WinForms.Measurements
