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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.IO;
using Regata.Measurements.Managers;

namespace Regata.Desktop.WinForms.Measurements
{
    public partial class Session : IDisposable
    {
        /// <summary>
        /// List of the unique irradiation date that is used for getting samples which were irradiate in this date
        /// </summary>
        public List<DateTime> IrradiationDateList { get; private set; }
        private DateTime _currentIrradiationDate;

        /// <summary>
        /// Assignation this date will form list of samples which available for the spectra acquisition
        /// </summary>
        public DateTime CurrentIrradiationDate
        {
            get { return _currentIrradiationDate; }
            set
            {
                _nLogger.Info($"{value.ToString("dd.MM.yyyy")} has chosen. List of samples will be prepare");
                _currentIrradiationDate = value;
                SetIrradiationsList(_currentIrradiationDate);
            }
        }


        /// <summary>
        /// Formes list of samples that were irradiated in chosen date
        /// </summary>
        /// <param name="date">Date of irradiation samples</param>
        private void SetIrradiationsList(DateTime date)
        {
            _nLogger.Info($"List of samples from irradiations journal will be loaded. Then list of measurements will be prepare");
            try
            {
                if (string.IsNullOrEmpty(Type))
                    throw new ArgumentNullException("Before choosing date of irradiations you should choose type of irradiations");
                IrradiationList.Clear();
                //MeasurementList.Clear();
                IrradiationList.AddRange(AppManager.DbContext.Irradiations.Where(i => i.DateTimeStart.HasValue && i.DateTimeStart.Value.Date == date.Date && i.Type == Type).ToList());

                //var configuration = new MapperConfiguration(cfg => cfg.AddMaps("MeasurementsCore"));
                //var mapper = new Mapper(configuration);

                //foreach (var i in IrradiationList)
                //{
                //    var m = mapper.Map<Measurement>(i);
                //    m.Type = Type;
                //    m.Height = 0;
                //    m.Assistant = AppManager.UserId;
                //    MeasurementList.Add(m);
                //}

                //SpreadSamplesToDetectors();
            }
            catch (ArgumentNullException ane)
            {
                Report.Notify();
            }
            catch (Exception e)
            {
                Report.Notify();
            }
        }

        /// <summary>
        /// Sometimes we have more samples than the disk might contain. In this case
        /// Event of disk overflow should be generate
        /// </summary>
        private void CheckExcessionOfDiskSize()
        {
            //if ((IrradiationList.Count / ManagedDetectors.Count) > SampleChanger.SizeOfDisk)
            //ExcessTheSizeOfDiskEvent?.Invoke(this, EventArgs.Empty);
        }

        //TODO: add event that occures when sample number increase the size of disk, but user should decide break the measurements or not!
        //      also in case of continue of measurements in the end of measurements user should accept that sample were changed on the disk
        /// <summary>
        /// This mode of spreading related with distribution sample to the detectors according to their containers.
        /// There is an algorithm:
        /// 1. Get ordered list of unique container numbers (not necessary one by one (1,3,4,5) also is possible)
        /// 2. Then it starts to asign samples from first container number to first detector, so on till iteration by detectors  is over
        /// 3. When iteration by detectors is over, but container numbers is not, continue assign samples from next container numbers
        ///    to first detector.
        /// </summary>
        private void SpreadSamplesByContainer()
        {
            try
            {
                CheckExcessionOfDiskSize();

                var NumberOfContainers = IrradiationList.Select(ir => ir.Container).Where(ir => ir.HasValue).Distinct().OrderBy(ir => ir.Value).ToArray();

                if (!NumberOfContainers.Any())
                {
                    SpreadSamplesUniform();
                    throw new ArgumentException("Spreading by container could not be use for the measurements because samples don't have information about containers numbers. Uniform option was used, also you can use spreading by the order for this type");
                }

                int i = 0;

                foreach (var conNum in NumberOfContainers)
                {
                    var sampleList = new List<Irradiation>(IrradiationList.Where(ir => ir.Container == conNum).ToList());
                    SpreadSamples[ManagedDetectors[i].Name].AddRange(sampleList);

                    foreach (var s in sampleList)
                        MeasurementList.Where(m => m.IrradiationId == s.Id).First().Detector = ManagedDetectors[i].Name;

                    //System.Threading.Tasks.Parallel.ForEach(sampleList, (s) => { MeasurementList.Where(m => m.IrradiationId == s.Id).First().Detector = ManagedDetectors[i].Name; });

                    i++;
                    if (i >= ManagedDetectors.Count())
                        i = 0;
                }
            }
            catch (ArgumentException ae)
            {
                Handlers.ExceptionHandler.ExceptionNotify(this, ae, Handlers.ExceptionLevel.Warn);
            }
            catch (Exception e)
            {
                Handlers.ExceptionHandler.ExceptionNotify(this, e, Handlers.ExceptionLevel.Error);
            }
        }



        /// <summary>
        /// This option allows merely divide all samples to detectors in the same portions.
        /// </summary>
        private void SpreadSamplesUniform()
        {
            try
            {
                CheckExcessionOfDiskSize();

                int i = 0;

                foreach (var sample in IrradiationList)
                {
                    SpreadSamples[ManagedDetectors[i].Name].Add(sample);
                    MeasurementList.Where(m => m.IrradiationId == sample.Id).First().Detector = ManagedDetectors[i].Name;
                    i++;
                    if (i >= ManagedDetectors.Count())
                        i = 0;
                }
            }
            catch (Exception e)
            {
                Handlers.ExceptionHandler.ExceptionNotify(this, e, Handlers.ExceptionLevel.Error);
            }
        }

        /// <summary>
        /// When this option has chosen samples will divide to the detector by the order:
        /// First sample to first detector, second to second, so on...
        /// </summary>
        private void SpreadSamplesByTheOrder()
        {
            try
            {
                CheckExcessionOfDiskSize();

                int i = 0; // number of detector
                int n = 0; // number of sample

                foreach (var sample in IrradiationList)
                {

                    if (i >= ManagedDetectors.Count())
                        throw new IndexOutOfRangeException("Count of samples more then disk can contains");
                    SpreadSamples[ManagedDetectors[i].Name].Add(sample);
                    MeasurementList.Where(m => m.IrradiationId == sample.Id).First().Detector = ManagedDetectors[i].Name;
                    n++;

                    if (n >= SampleChanger.SizeOfDisk)
                    {
                        i++;
                        n = 0;
                    }
                }
            }
            catch (IndexOutOfRangeException ie)
            {
                Handlers.ExceptionHandler.ExceptionNotify(this, ie, Handlers.ExceptionLevel.Warn);
            }
            catch (Exception e)
            {
                Handlers.ExceptionHandler.ExceptionNotify(this, e, Handlers.ExceptionLevel.Error);
            }
        }

        /// <summary>
        /// This method describes the division of samples to detectors according with chosen SpreadedOption with requred checks
        /// <seealso cref="SpreadSamplesByContainer"/>
        /// <seealso cref="SpreadSamplesUniform"/>
        /// <seealso cref="SpreadSamplesByTheOrder"/>
        /// </summary>
        private void SpreadSamplesToDetectors()
        {
            _nLogger.Info($"Spreading samples to detectors has begun");
            try
            {
                if (!ManagedDetectors.Any())
                    throw new ArgumentOutOfRangeException("Session has managed no-one detector");

                if (!IrradiationList.Any())
                    throw new ArgumentOutOfRangeException("Session doesn't contain samples to measure");

                foreach (var dName in ManagedDetectors.Select(d => d.Name).ToArray())
                {
                    if (SpreadSamples[dName].Any())
                        SpreadSamples[dName].Clear();
                }

                if (SpreadOption == SpreadOptions.container)
                    SpreadSamplesByContainer();
                else if (SpreadOption == SpreadOptions.inOrder)
                    SpreadSamplesByTheOrder();
                else if (SpreadOption == SpreadOptions.uniform)
                    SpreadSamplesUniform();
                else
                {
                    SpreadSamplesByContainer();
                    throw new Exception("Type of spreaded options doesn't recognize. Spreaded by container has done.");
                }

                MakeSamplesCurrentOnAllDetectorsByNumber();

                foreach (var d in ManagedDetectors)

                {
                    if (SpreadSamples[d.Name].Count == 0)
                        continue;

                    d.CurrentMeasurement = MeasurementList.Where(cm => cm.IrradiationId == d.CurrentSample.Id).First();

                    _nLogger.Info($"Samples [{(string.Join(",", SpreadSamples[d.Name].OrderBy(ss => $"{ss.SetKey}-{ss.SampleNumber}").Select(ss => $"{ss.SetKey}-{ss.SampleNumber}").ToArray()))}] will measure on the detector {d.Name}");
                }

            }
            catch (ArgumentOutOfRangeException ae)
            {
                Handlers.ExceptionHandler.ExceptionNotify(this, ae, Handlers.ExceptionLevel.Error);
                SessionComplete?.Invoke();
            }
            catch (Exception e)
            {
                Handlers.ExceptionHandler.ExceptionNotify(this, e, Handlers.ExceptionLevel.Error);
            }
        }

    }
}


