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

using Regata.Core.UI.WinForms.Forms;
using Regata.Core.UI.WinForms;
using Regata.Core.UI.WinForms.Items;
using Regata.Core.DataBase;
using Regata.Core.DataBase.Models;
using Regata.Core.Settings;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;

namespace Regata.Desktop.WinForms.Measurements
{
    public partial class MainForm : IDisposable
    {

        public RegisterForm<Measurement> mainForm;
        private MeasurementsRegister CurrentMeasurementsRegister;
        private RegataContext _regataContext;
        EnumItem<MeasurementsType> MeasurementsTypeItems;

        public MainForm()
        {
            Settings<MeasurementsSettings>.AssemblyName = "Measurements.Desktop";

            _regataContext = new RegataContext();
            mainForm = new RegisterForm<Measurement>();
            CurrentMeasurementsRegister = new MeasurementsRegister() { Type = -1, Id = 0 };
            MeasurementsTypeItems = new EnumItem<MeasurementsType>();
            _chosenIrradiations = new List<Irradiation>();
            _chosenMeasurements = new List<Measurement>();

            Settings<MeasurementsSettings>.CurrentSettings.LanguageChanged += () => Labels.SetControlsLabels(mainForm.Controls);

            InitMenuStrip();
            InitStatusStrip();
            InitCurrentRegister();
            InitIrradiationsRegisters();
            InitMeasurementsRegisters();
            InitializeFuntionalField();
            InitializeRegFormingControls();
            InitializeMeasurementsParamsControls();

            Labels.SetControlsLabels(mainForm.Controls);
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
            }

            // очистить неуправляемые ресурсы

            _isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    } //public partial class MainForm
}     // namespace Regata.Desktop.WinForms.Measurements
