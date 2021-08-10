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

using Regata.Core;
using Regata.Core.Collections;
using Regata.Core.DataBase;
using Regata.Core.DataBase.Models;
using Regata.Core.Hardware;
using Regata.Core.Settings;
using Regata.Core.UI.WinForms;
using Regata.Core.UI.WinForms.Forms;
using Regata.Core.UI.WinForms.Items;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Regata.Desktop.WinForms.Measurements
{
    public partial class MainForm : IDisposable
    {
        public RegisterForm<Measurement> mainForm;
        private MeasurementsRegister CurrentMeasurementsRegister;
        private EnumItem<MeasurementsType> MeasurementsTypeItems;
        private EnumItem<Status> VerbosityItems;
        private EnumItem<CanberraDeviceAccessLib.AcquisitionModes> AcquisitionModeItems;

        public MainForm()
        {

            mainForm = new RegisterForm<Measurement>() { Name = "GSAMainForm", Text = "GSAMainForm" };

            mainForm.Icon = new Icon("MeasurementsLogoCircle.ico");
            CurrentMeasurementsRegister = new MeasurementsRegister() { Type = -1, Id = 0 };
            MeasurementsTypeItems = new EnumItem<MeasurementsType>();
            VerbosityItems = new EnumItem<Status>();
            AcquisitionModeItems = new EnumItem<CanberraDeviceAccessLib.AcquisitionModes>();
            _chosenIrradiations = new List<Irradiation>();
            _chosenMeasurements = new List<Measurement>();

            Settings<MeasurementsSettings>.CurrentSettings.PropertyChanged += (s,e) => Labels.SetControlsLabels(mainForm);

            Settings<MeasurementsSettings>.CurrentSettings.MainTableSettings = new MeasurementsSettings().MainTableSettings;

            mainForm.MainRDGV.RDGV_Set = Settings<MeasurementsSettings>.CurrentSettings.MainTableSettings;

            // Call event only for warnings and errors
            Settings<MeasurementsSettings>.CurrentSettings.Verbosity = Status.Warning;
            if (Settings<MeasurementsSettings>.CurrentSettings.BackgroundRegistersUpdateTime < 30)
                Settings<MeasurementsSettings>.CurrentSettings.BackgroundRegistersUpdateTime = 30;
            Report.NotificationEvent += Report_NotificationEvent;

            InitMenuStrip();
            InitStatusStrip();
            InitCurrentRegister();
            InitIrradiationsRegisters();
            InitMeasurementsRegisters();
            InitializeFuntionalField();
            InitializeRegFormingControls();
            InitializeMeasurementsParamsControls();
            InitializeMeasurementsControls();

            Labels.SetControlsLabels(mainForm);

            mainForm.Load += MainForm_Load;

            Settings<MeasurementsSettings>.Save();
        }

        private void Report_NotificationEvent(Core.Messages.Message msg)
        {
            PopUpMessage.Show(msg, Settings<MeasurementsSettings>.CurrentSettings.DefaultPopUpMessageTimeout);
        }

        private bool _isDisposed;

        ~MainForm()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (_isDisposed)
                return;

            if (isDisposing)
            {
                // освободить управляемые ресурсы
                mainForm.Dispose();
                _dcp?.Dispose();

                if (_detectors != null)
                {
                    foreach (var d in _detectors)
                    {
                        d.AcquireDone -= Det_AcquireDone;
                        d.AcquireStart -= Det_AcquireStart;
                        d.HardwareError -= Det_HardwareError;
                        d.ParamChange -= Det_ParamChange;
                        d.StatusChanged -= Det_StatusChanged;
                        d.Dispose();
                    }
                }
            }

            // очистить неуправляемые ресурсы

            _isDisposed = true;
            Settings<MeasurementsSettings>.Save();

        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            var tmpArr = new List<string>(8);
            await foreach (var d in Detector.GetAvailableDetectorsAsyncStream())
            {
                if (!string.IsNullOrEmpty(d))
                {
                    CheckedAvailableDetectorArrayControl.Add(d);
                    tmpArr.Add(d);
                }
            }

            _circleDetArray = new CircleArray<string>(tmpArr.OrderBy(d => d).ToArray());

            await BackGroundTask();

        }

        private async Task BackGroundTask()
        {
            while (true)
            {
                await Task.Delay(TimeSpan.FromSeconds(Settings<MeasurementsSettings>.CurrentSettings.BackgroundRegistersUpdateTime));
                await FillSelectedIrradiations();
            }
        }


    } // public partial class MainForm
}     // namespace Regata.Desktop.WinForms.Measurements
