﻿/***************************************************************************
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
using Regata.Core.UI.WinForms;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Regata.Desktop.WinForms.Measurements
{
    public partial class MainForm
    {
        private CircleArray<string> _circleDetArray;

        private void AddRecord(int IrradiationId, string dName = "", int? diskPosition = null)
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
                m.Type = (int)MeasurementsTypeItems.CheckedItem;
                m.DiskPosition = diskPosition;
                m.Detector = MeasurementsTypeItems.CheckedItem switch
                {
                    MeasurementsType.sli => GetDetectorForSLISample(m),
                    _ => string.IsNullOrEmpty(dName) ?  CheckedAvailableDetectorArrayControl.SelectedItem : dName
                };

                m.Height = CheckedHeightArrayControl.SelectedItem;
                mainForm.MainRDGV.Add(m);

                _SLIShowAlreadyAdded_CheckedChanged(null, null);
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_ADD_REC)
                {
                    DetailedText = ex.ToString()
                });
            }
        }


        private void AddRecord(Measurement m)
        {
            try
            {
                m.Id             = 0;
                m.AcqMode        = (int)AcquisitionModeItems.CheckedItem;
                m.RegId          = CurrentMeasurementsRegister.Id;
                m.Duration       = (int)DurationControl.Duration.TotalSeconds;
                m.Type           = (int)MeasurementsTypeItems.CheckedItem;
                m.Height         = CheckedHeightArrayControl.SelectedItem;
                m.DateTimeStart  = null;
                m.DateTimeFinish = null;
                m.FileSpectra    = null;
                m.Note           = null;
                m.DeadTime       = null;

                mainForm.MainRDGV.Add(m);
                _SLIShowAlreadyAdded_CheckedChanged(null, null);

            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_ADD_MEAS_REC)
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

                Labels.SetControlsLabels(mainForm);


            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_INIT_CUR_REG)
                {
                    DetailedText = ex.ToString()
                });
            }
        }

        private void RemoveCurrentRegisterIfEmpty()
        {
            try
            {
                // running creates measurement register.
                // in case of after disposing the form there are not measurements records for register the last one will be deleted
                using (var r = new RegataContext())
                {
                    if (CurrentMeasurementsRegister.SamplesCnt == 0)
                    {
                        r.MeasurementsRegisters.Remove(CurrentMeasurementsRegister);
                    }
                        r.Measurements.RemoveRange(r.Measurements.Where(m => m.RegId == CurrentMeasurementsRegister.Id && m.FileSpectra == null).ToArray());
                        r.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_MAIN_FORM_DISP)
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
                    CurrentMeasurementsRegister.Name = ir.DateTimeStart.Value.Date.ToShortDateString();
                    CurrentMeasurementsRegister.LoadNumber = ir.LoadNumber;
                    CurrentMeasurementsRegister.DateTimeStart = mainForm.MainRDGV.CurrentDbSet.Local.Select(m => m.DateTimeStart).Min();
                    CurrentMeasurementsRegister.DateTimeFinish = mainForm.MainRDGV.CurrentDbSet.Local.Select(m => m.DateTimeFinish).Max();
                    CurrentMeasurementsRegister.SamplesCnt = mainForm.MainRDGV.CurrentDbSet.Local.Where(m => m.FileSpectra != null).Count();
                    CurrentMeasurementsRegister.Detectors = string.Join(',', mainForm.MainRDGV.CurrentDbSet.Local.Select(m => m.Detector).Distinct().ToArray());
                    // TODO: https://github.com/regata-jinr/Measurements.Desktop/issues/46
                    //CurrentMeasurementsRegister.Assistant = GlobalSettings.User;

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
                    // We can not have Measurement register with duplicate name and type. In case of unhandled stop of app it is possible to 
                    // Dispose method of the from will not be run. In this case current register will remain in DB with null value of Name and -1 for type
                    // Here we catch it and remove.
                    var null_register = r.MeasurementsRegisters.Where(m => m.IrradiationDate == null).FirstOrDefault();
                    if (null_register != null)
                        r.MeasurementsRegisters.Remove(null_register);
                    

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


        private string GetDetectorForSLISample(Measurement currM)
        {
            if (currM.SetKey.Contains("s-") || currM.SetKey.Contains("m-"))
                return string.Empty;

            if (!mainForm.MainRDGV.CurrentDbSet.Local.Where(m => m.CountryCode  == currM.CountryCode && 
                                                           m.ClientNumber == currM.ClientNumber && 
                                                           m.Year         == currM.Year &&
                                                           m.SetNumber    == currM.SetNumber &&
                                                           m.SetIndex     == currM.SetIndex
                                                     ).Any()) return string.Empty;

            int curSamNum = 0;
            int.TryParse(currM.SampleNumber, out curSamNum);

            if (curSamNum == 0) return string.Empty;

            var prevM = mainForm.MainRDGV.CurrentDbSet.Local.Where(m => m.CountryCode  == currM.CountryCode &&
                                                                  m.ClientNumber == currM.ClientNumber &&
                                                                  m.Year         == currM.Year &&
                                                                  m.SetNumber    == currM.SetNumber &&
                                                                  m.SetIndex     == currM.SetIndex &&
                                                                  m.SampleNumber == (curSamNum - 1).ToString("d2")
                                                            ).FirstOrDefault();

            if (prevM == null) return string.Empty;

            return prevM.Detector;
        }


    } // public partial class MainForm
}     // namespace Regata.Desktop.WinForms.Measurements
