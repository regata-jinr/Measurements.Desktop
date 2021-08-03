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
using Regata.Core.Settings;
using Regata.Core.UI.WinForms;
using Regata.Core.UI.WinForms.Forms;
using Regata.Core.UI.WinForms.Items;
using System;
using System.Collections.Generic;

namespace Regata.Desktop.WinForms.Measurements
{
    public partial class MainForm : IDisposable
    {
        public RegisterForm<Measurement> mainForm;
        private MeasurementsRegister CurrentMeasurementsRegister;
        private RegataContext _regataContext;
        private EnumItem<MeasurementsType> MeasurementsTypeItems;
        private EnumItem<CanberraDeviceAccessLib.AcquisitionModes> AcquisitionModeItems;


        public MainForm()
        {
            Settings<MeasurementsSettings>.AssemblyName = "Measurements.Desktop";

            _regataContext = new RegataContext();
            mainForm = new RegisterForm<Measurement>() { Name = "GSAMainForm", Text = "GSAMainForm" };
            CurrentMeasurementsRegister = new MeasurementsRegister() { Type = -1, Id = 0 };
            MeasurementsTypeItems = new EnumItem<MeasurementsType>();
            AcquisitionModeItems = new EnumItem<CanberraDeviceAccessLib.AcquisitionModes>();
            _chosenIrradiations = new List<Irradiation>();
            _chosenMeasurements = new List<Measurement>();

            Settings<MeasurementsSettings>.CurrentSettings.LanguageChanged += () => Labels.SetControlsLabels(mainForm);

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
                _regataContext.Dispose();
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
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    } // public partial class MainForm
}     // namespace Regata.Desktop.WinForms.Measurements
