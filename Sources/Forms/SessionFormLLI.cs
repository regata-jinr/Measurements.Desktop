using System.Windows.Forms;
using System;
using System.Collections.Generic;
using Measurements.Core;
using System.Linq;
using Measurements.UI.Managers;
using System.Data;
using Measurements.Core.Handlers;
using AutoMapper;

namespace Measurements.UI.Desktop.Forms
{
    //LLI
    public partial class SessionForm : Form
    {
        private void SetColumnProperties4LLI()
        {
            SetColumnsProperties(ref SessionFormAdvancedDataGridViewMeasurementsJournal,
                                new string[]
                                { "Id","IrradiationId", "SetKey", "Type" },
                                new Dictionary<string, string>() {
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

        private void AddLLIMeasurementsInfoToMainTable()
        {
            //foreach (DataGridViewRow row in IrradiationJournalADGVSamples.SelectedRows)
            //{
            //    var drvSet = IrradiationJournalADGVSamplesSets.SelectedRows[0];


            //    var newIrr = new IrradiationInfo()
            //    {
            //        CountryCode  = drvSet.Cells["Country_Code"].Value.ToString(),
            //        ClientNumber = drvSet.Cells["Client_Id"].Value.ToString(),
            //        Year         = drvSet.Cells["Year"].Value.ToString(),
            //        SetNumber    = drvSet.Cells["Sample_Set_Id"].Value.ToString(),
            //        SetIndex     = drvSet.Cells["Sample_Set_Index"].Value.ToString(),
            //        SampleNumber = row.Cells["A_Sample_ID"].Value.ToString(),
            //        Type         = _type,
            //        DateTimeStart = _currentJournalDateTime,
            //        Weight       = string.IsNullOrEmpty(row.Cells["P_Weighting_SLI"].Value.ToString()) ? 0 : decimal.Parse(row.Cells["P_Weighting_SLI"].Value.ToString()),
            //        Duration     = Duration,
            //        Channel      = this.Channel,
            //        Container    = ContainerNumber,
            //        Position     = PositionInContainer,
            //        LoadNumber   = _loadNumber,
            //        Assistant    = _user
            //    };
            //    _irradiationList.Add(newIrr);

            //    using (var ic = new InfoContext())
            //    {
            //        ic.Irradiations.Add(newIrr);
            //        ic.SaveChanges();
            //    }
            //}
        }

        //TODO: add spreading samples by container
        private void AddAllMeasurementsInfoToMainTable()
        {
            try
            {
                foreach (DataGridViewRow row in SessionFormAdvancedDataGridViewIrradiatedSamples.SelectedRows)
                {
                    IrradiationInfo currentSample = null;
                    using (var ic = new InfoContext())
                        currentSample = ic.Irradiations.Where(ir => ir.Id == (int)row.Cells["Id"].Value).First();

                    var configuration = new MapperConfiguration(cfg => cfg.AddMaps("MCore"));
                    var mapper = new Mapper(configuration);
                    var newMeasurement = mapper.Map<MeasurementInfo>(currentSample);

                    newMeasurement.DateTimeStart = DateTime.Now.Date;
                    newMeasurement.Duration = Duration;
                    newMeasurement.Detector = Detector;
                    newMeasurement.Assistant = User;

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

    }
}
