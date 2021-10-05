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
using Regata.Core.DataBase.Models;
using Regata.Core.Hardware;
using Regata.Core.Settings;
using Regata.Core.UI.WinForms;
using Regata.Core.UI.WinForms.Items;
using Regata.Core.UI.WinForms.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Regata.Desktop.WinForms.Measurements
{
    public partial class MainForm : IDisposable
    {
        private Timer _timer;
        public  RegisterForm<Measurement> mainForm;
        private MeasurementsRegister CurrentMeasurementsRegister;
        private EnumItem<MeasurementsType> MeasurementsTypeItems;
        private EnumItem<Status> VerbosityItems;
        private EnumItem<CanberraDeviceAccessLib.AcquisitionModes> AcquisitionModeItems;
        
        public MainForm()
        {
            Settings<MeasurementsSettings>.AssemblyName = "MeasurementsDesktop";

            mainForm = new RegisterForm<Measurement>() { Name = "GSAMainForm", Text = "GSAMainForm" };

            mainForm.Icon = Properties.Resources.MeasurementsLogoCircle2;
            CurrentMeasurementsRegister = new MeasurementsRegister() { Type = -1, Id = 0 };
            MeasurementsTypeItems = new EnumItem<MeasurementsType>();
            VerbosityItems = new EnumItem<Status>();
            AcquisitionModeItems = new EnumItem<CanberraDeviceAccessLib.AcquisitionModes>();
            _chosenIrradiations = new List<Irradiation>();
            _chosenMeasurements = new List<Measurement>();

            Settings<MeasurementsSettings>.CurrentSettings.PropertyChanged += (s, e) =>
            {
                Labels.SetControlsLabels(mainForm);
                // FIXME: dcp npt adopted for new from schema based on regata framework
                //Labels.SetControlsLabels(_dcp);

            };

            mainForm.MainRDGV.RDGV_Set = Settings<MeasurementsSettings>.CurrentSettings.MainTableSettings;

            if (Settings<MeasurementsSettings>.CurrentSettings.BackgroundRegistersUpdateTimeSeconds < 30)
                Settings<MeasurementsSettings>.CurrentSettings.BackgroundRegistersUpdateTimeSeconds = 30;

            Report.NotificationEvent += Report_NotificationEvent;

            InitMenuStrip();
            InitStatusStrip();
            InitCurrentRegister();
            InitIrradiationsRegisters();
            InitMeasurementsRegisters();
            InitializeRegFormingControls();
            InitializeMeasurementsParamsControls();
            InitializeMeasurementsControls();

            Labels.SetControlsLabels(mainForm);

            mainForm.Load += MainForm_Load;

            Settings<MeasurementsSettings>.Save();

            _timer = new Timer();
            _timer.Interval = (int)TimeSpan.FromSeconds(Settings<MeasurementsSettings>.CurrentSettings.BackgroundRegistersUpdateTimeSeconds).TotalMilliseconds;
            _timer.Tick += RefreshRegisters;
            _timer.Start();
        }

        private void Report_NotificationEvent(Core.Messages.Message msg)
        {
            PopUpMessage.Show(msg, Settings<MeasurementsSettings>.CurrentSettings.DefaultPopUpMessageTimeoutSeconds);
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
                RemoveCurrentRegisterIfEmpty();
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
            Labels.SetControlsLabels(mainForm);
            mainForm.MainRDGV.HideColumns();
            //mainForm.MainRDGV.SetUpReadOnlyColumns();
            mainForm.MainRDGV.SetUpWritableColumns();


            mainForm.MainRDGV.Sorted += MainRDGV_Sorted;

            mainForm.MainRDGV.Columns["DateTimeStart"].DefaultCellStyle.Format  = "dd.MM.yyyy HH:mm:ss";
            mainForm.MainRDGV.Columns["DateTimeFinish"].DefaultCellStyle.Format = "dd.MM.yyyy HH:mm:ss";

            var w = mainForm.MainRDGV.Width;
            var typW = 0.05;

            mainForm.MainRDGV.Columns["CountryCode"].Width   = (int)(w * typW);
            mainForm.MainRDGV.Columns["ClientNumber"].Width  = (int)(w * typW);
            mainForm.MainRDGV.Columns["Year"].Width          = (int)(w * typW);
            mainForm.MainRDGV.Columns["SetNumber"].Width     = (int)(w * typW);
            mainForm.MainRDGV.Columns["SetIndex"].Width      = (int)(w * typW);
            mainForm.MainRDGV.Columns["SampleNumber"].Width  = (int)(w * typW);
            mainForm.MainRDGV.Columns["DiskPosition"].Width  = (int)(w * typW);
            mainForm.MainRDGV.Columns["DateTimeStart"].Width = (int)(w * 0.1);
            mainForm.MainRDGV.Columns["Duration"].Width      = (int)(w * 0.1);
            mainForm.MainRDGV.Columns["FileSpectra"].Width   = (int)(w * typW);
            mainForm.MainRDGV.Columns["Height"].Width        = (int)(w * typW);
            mainForm.MainRDGV.Columns["Detector"].Width      = (int)(w * typW);
            mainForm.MainRDGV.Columns["DeadTime"].Width      = (int)(w * typW);

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

            mainForm.buttonClearRegister.Enabled = true;
            buttonShowAcqQueue.Enabled = true;
        }

        private void MainRDGV_Sorted(object sender, EventArgs e)
        {
            ColorizeRDGV();
        }

        private async void RefreshRegisters(object sender, EventArgs e)
        {
                await FillSelectedIrradiations();
        }


    } // public partial class MainForm
}     // namespace Regata.Desktop.WinForms.Measurements
