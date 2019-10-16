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


namespace Measurements.UI.Desktop.Forms
{
    // SLI
    public partial class SessionForm : Form
    {

        private void SetColumnProperties4SLI()
        {
            SetColumnsProperties(ref SessionFormAdvancedDataGridViewMeasurementsJournal,
                                new string[]
                                { "Id","IrradiationId", "SetKey", "Type" },
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
                foreach (DataGridViewRow row in SessionFormAdvancedDataGridViewIrradiatedSamples.SelectedRows)
                {
                    IrradiationInfo currentSample = null;
                    using (var ic = new InfoContext())
                        currentSample = ic.Irradiations.Where(ir => ir.Id == (int)row.Cells["Id"].Value).First();

                    var configuration = new MapperConfiguration(cfg => cfg.AddMaps("MeasurementsCore"));
                    var mapper = new Mapper(configuration);
                    var newMeasurement = mapper.Map<MeasurementInfo>(currentSample);

                    newMeasurement.DateTimeStart = DateTime.Now.Date;
                    newMeasurement.Duration      = Duration;
                    newMeasurement.Height        = HeightGeometry;
                    newMeasurement.Detector      = SelectDetectorForSample(newMeasurement.SetKey, newMeasurement.SampleNumber);
                    newMeasurement.Assistant     = User;

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

        private string SelectDetectorForSample(string curSetKey, string curSampleNumber)
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

                return _measurementsList.Where(m => 
                                                m.SetKey == uniqueSets[0].SetKey && (int.Parse(curSampleNumber) - int.Parse(m.SampleNumber)) > 0).
                                         Select(n =>
                                                new { diff = int.Parse(curSampleNumber) - int.Parse(n.SampleNumber), Detector =                            n.Detector }).
                                         MinBy(n => n.diff).First().Detector;
            }
            catch (Exception e)
            {
                MessageBoxTemplates.WarningAsync("Проблема с распределением образцов по детекторам");
                return Detector;
            }


        }

    }
}
