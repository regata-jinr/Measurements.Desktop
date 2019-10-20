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
                                { "Id","IrradiationId", "SetKey", "Type", "SampleKey", "LoadNumber", "IrrJournalDate" },
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
            AddSLIMeasurementsInfoToMainTable();
        }

        private void AddAllMeasurementsInfoToMainTable()
        {
            if (!SelectedIrrJournalDate.HasValue)
            {
                MessageBoxTemplates.WarningAsync("Перед добавлением образцов необходимо выбрать журнал облучений");
                return;
            }
            try
            {
                CheckExcessionOfDiskSize();
                List<IrradiationInfo> IrradiationList = null;
                using (var ic = new InfoContext())
                    IrradiationList = ic.Irradiations.Where(ir => ir.Type == _session.Type && ir.DateTimeStart.HasValue && ir.DateTimeStart.Value.Date == SelectedIrrJournalDate.Value.Date && ir.LoadNumber.Value == SelectedLoadNumber.Value && ir.Container.HasValue).ToList();

                if (!IrradiationList.Any())
                {
                    MessageBoxTemplates.WarningAsync("Программа не может получить список образцов из журнала облучений");
                    return;
                }

                var NumberOfContainers = IrradiationList.Select(ir => ir.Container.Value).Distinct().OrderBy(cn => cn).ToArray();

                if (!NumberOfContainers.Any())
                    MessageBoxTemplates.WarningAsync("Программа не может получить список контейнеров");

                int i = 0;

                _irradiationList.AddRange(IrradiationList);

                foreach (var conNum in NumberOfContainers)
                {
                    var sampleList = new List<IrradiationInfo> (IrradiationList.Where(ir => ir.Container == conNum).ToList());

                    foreach (var currentSample in sampleList)
                    {
                        var configuration = new MapperConfiguration(cfg => cfg.AddMaps("MeasurementsCore"));
                        var mapper = new Mapper(configuration);
                        var newMeasurement = mapper.Map<MeasurementInfo>(currentSample);

                        newMeasurement.IrrJournalDate = MJournalIrrDate;
                        newMeasurement.LoadNumber     = MJournalIrrLoadNumber;
                        newMeasurement.DateTimeStart  = DateTime.Now.Date;
                        newMeasurement.Duration       = Duration;
                        newMeasurement.Height         = HeightGeometry;
                        newMeasurement.Detector       = (_session.ManagedDetectors.OrderBy(md => md.Name).ToArray())[i].Name;
                        newMeasurement.Assistant      = User;

                        _measurementsList.Add(newMeasurement);
                        using (var ic = new InfoContext())
                        {
                            ic.Measurements.Add(newMeasurement);
                            ic.SaveChanges();
                        }
                    }

                    //System.Threading.Tasks.Parallel.ForEach(sampleList, (s) => { MeasurementList.Where(m => m.IrradiationId == s.Id).First().Detector = ManagedDetectors[i].Name; });

                    i++;
                    if (i >= _session.ManagedDetectors.Count())
                        i = 0;
                }
            }
            catch (ArgumentException ae)
            {
                MessageBoxTemplates.WrapExceptionToMessageBox(new ExceptionEventsArgs {exception = ae, Level = ExceptionLevel.Error }); 
            }
            catch (Exception e)
            {
                MessageBoxTemplates.WrapExceptionToMessageBox(new ExceptionEventsArgs {exception = e, Level = ExceptionLevel.Error }); 
            }
        }
        private void CheckExcessionOfDiskSize()
        {
            //if ((IrradiationList.Count / ManagedDetectors.Count) > SampleChanger.SizeOfDisk)
            //ExcessTheSizeOfDiskEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}
