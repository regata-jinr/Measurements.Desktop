/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2017-2021, REGATA Experiment at FLNP|JINR                  *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 *                                                                         *
 * The REGATA Experiment team license this file to you under the           *
 * GNU GENERAL PUBLIC LICENSE                                              *
 *                                                                         *
 ***************************************************************************/

using System.Windows.Forms;
using System;
using System.Collections.Generic;
using Measurements.Core;
using System.Linq;
using Measurements.UI.Managers;
using System.Data;
using Measurements.Core.Handlers;
using AutoMapper;
using MoreLinq;


namespace Regata.Desktop.WinForms.Measurements;
{
    // SLI
    public partial class SessionForm : Form
    {

        private void SetColumnProperties4SLI()
        {
            SetColumnsProperties(ref SessionFormAdvancedDataGridViewMeasurementsJournal,
                                new string[]
                                { "Id","IrradiationId", "SetKey", "Type", "SampleKey", "LoadNumber", "IrrJournalDate" },
                                new Dictionary<string,      string>() {
                                    { "CountryCode",        "Код страны" },
                                    { "ClientNumber",       "Номер клиента" },
                                    { "Year",               "Год" },
                                    { "SetNumber",          "Номер партии" },
                                    { "SetIndex",           "Индекс партии" },
                                    { "SampleNumber",       "Номер образца" },
                                    { "DateTimeStart",      "Дата и время начала измерения" },
                                    { "Duration",           "Продолжительность измерения" },
                                    { "DateTimeFinish",     "Дата и время конца измерения" },
                                    { "Height",             "Высота" },
                                    { "FileSpectra",        "Файл спектра" },
                                    { "Detector",           "Детектор" },
                                    { "Assistant",          "Оператор" },
                                    { "Note",               "Примечание" } },
                                new string[]
                                { "CountryCode", "ClientNumber", "Year", "SetNumber", "SetIndex", "SampleNumber", "Height", "Duration", "Detector", "Assistant", "FileSpectra" }
                                );

        }



        private void AddSLIMeasurementsInfoToMainTable()
        {
            try
            {
                foreach (DataGridViewRow row in SessionFormAdvancedDataGridViewIrradiatedSamples.SelectedRows.OfType<DataGridViewRow>().Reverse())
                {
                    IrradiationInfo currentSample = null;
                    using (var ic = new InfoContext())
                        currentSample = ic.Irradiations.Where(ir => ir.Id == (int)row.Cells["Id"].Value).First();

                    _irradiationList.Add(currentSample);
                    var configuration = new MapperConfiguration(cfg => cfg.AddMaps("MeasurementsCore"));
                    var mapper = new Mapper(configuration);
                    var newMeasurement = mapper.Map<MeasurementInfo>(currentSample);

                    newMeasurement.IrrJournalDate = MJournalIrrDate;
                    newMeasurement.LoadNumber     = MJournalIrrLoadNumber;
                    newMeasurement.DateTimeStart  = DateTime.Now.Date;
                    newMeasurement.Duration       = Duration;
                    newMeasurement.Height         = HeightGeometry;
                    newMeasurement.Detector       = AssignDetectorForSample(newMeasurement.SetKey, newMeasurement.SampleNumber);
                    newMeasurement.Assistant      = User;

                    _measurementsList.Add(newMeasurement);
                    using (var ic = new InfoContext())
                    {
                        ic.Measurements.Add(newMeasurement);
                        ic.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxTemplates.WrapExceptionToMessageBox(new ExceptionEventsArgs() { exception = ex, Level = ExceptionLevel.Error });
            }
        }

        private void SetSLIVisibilities()
        {
            SessionFormlButtonAddAllToJournal.Visible = false;
        }

        private string AssignDetectorForSample(string curSetKey, string curSampleNumber)
        {
            try
            {
                if (_measurementsList.Count() < _session.ManagedDetectors.Count)
                    return Detector;

                var uniqueSets = _measurementsList.Select(m => new { m.Detector, m.SetKey }).Where(us => us.SetKey == curSetKey).Distinct().ToArray();

                if (uniqueSets.Length == 0)
                    return Detector;

                if (uniqueSets.Length == 1)
                    return uniqueSets.First().Detector;

                var det = _measurementsList.Where(m =>
                                                m.SetKey == uniqueSets[0].SetKey && (int.Parse(curSampleNumber) - int.Parse(m.SampleNumber)) > 0).
                                         Select(n =>
                                                new { diff = int.Parse(curSampleNumber) - int.Parse(n.SampleNumber), Detector = n.Detector }).
                                         MinBy(n => n.diff).First().Detector;

                if (!string.IsNullOrEmpty(det))
                    return det;

                return Detector;
            }
            catch (Exception e)
            {
                //MessageBoxTemplates.WarningAsync("Проблема с распределением образцов по детекторам");
                return Detector;
            }


        }

    }
}
