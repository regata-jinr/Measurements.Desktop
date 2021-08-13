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
using Regata.Core.Messages;
using Regata.Core.Settings;
using Regata.Core.UI.WinForms;
using System;

namespace Regata.Desktop.WinForms.Measurements
{
    public partial class MainForm
    {

        private void InitMenuStrip()
        {
            try
            {
                mainForm.LangItem.CheckedChanged += () => Settings<MeasurementsSettings>.CurrentSettings.CurrentLanguage = mainForm.LangItem.CheckedItem;
                mainForm.LangItem.CheckItem(Settings<MeasurementsSettings>.CurrentSettings.CurrentLanguage);

                AcquisitionModeItems.CheckItem(Settings<MeasurementsSettings>.CurrentSettings.AcquisitionMode);

                // NOTE: now we can use only two modes aCountToLiveTime or aCountToRealTime

                AcquisitionModeItems.CheckedChanged += () => 
                {
                    var currentMode = AcquisitionModeItems.CheckedItem switch
                    {
                        CanberraDeviceAccessLib.AcquisitionModes.aCountToLiveTime => CanberraDeviceAccessLib.AcquisitionModes.aCountToLiveTime,
                        _ => CanberraDeviceAccessLib.AcquisitionModes.aCountToRealTime
                    };
                    mainForm.MainRDGV.FillDbSetValues("AcqMode", (int)currentMode);
                    Settings<MeasurementsSettings>.CurrentSettings.AcquisitionMode = currentMode;
                    AcquisitionModeItems.CheckItem(currentMode);
                    Labels.SetControlsLabels(mainForm);

                };

                VerbosityItems.CheckItem(Settings<MeasurementsSettings>.CurrentSettings.Verbosity);

                VerbosityItems.CheckedChanged += () =>
                {
                    Settings<MeasurementsSettings>.CurrentSettings.Verbosity = VerbosityItems.CheckedItem;
                    Labels.SetControlsLabels(mainForm);
                };

                mainForm.MenuStrip.Items.Add(MeasurementsTypeItems.EnumMenuItem);
                mainForm.MenuStrip.Items.Add(AcquisitionModeItems.EnumMenuItem);
                mainForm.MenuStrip.Items.Add(VerbosityItems.EnumMenuItem);

                MeasurementsTypeItems.CheckedChanged += async () =>
                {
                    mainForm.TabsPane[0, 0].DataSource = null;
                    mainForm.TabsPane[1, 0].DataSource = null;
                    mainForm.TabsPane[0, 1].DataSource = null;
                    mainForm.TabsPane[1, 1].DataSource = null;

                    CurrentMeasurementsRegister.Type = (int)MeasurementsTypeItems.CheckedItem;

                    DurationControl.Duration = CurrentMeasurementsRegister.Type switch
                    {
                        0 => TimeSpan.FromSeconds(Settings<MeasurementsSettings>.CurrentSettings.DefaultSLITime),
                        1 => TimeSpan.FromSeconds(Settings<MeasurementsSettings>.CurrentSettings.DefaultLLI1Time),
                        2 => TimeSpan.FromSeconds(Settings<MeasurementsSettings>.CurrentSettings.DefaultLLI2Time),
                        _ => TimeSpan.FromSeconds(0),
                    };

                    ClearCurrentRegister();

                    await FillIrradiationRegisters();
                    await FillMeasurementsRegisters();

                    Labels.SetControlsLabels(mainForm);

                    buttonAddAllSamples.Enabled = true;
                    buttonAddSelectedSamplesToReg.Enabled = true;
                    buttonRemoveSelectedSamples.Enabled = true;

                };
            }
            catch (Exception ex)
            {
                Report.Notify(new Message(Codes.ERR_UI_WF_INI_MENU) { DetailedText = string.Join("--", ex.Message, ex?.InnerException?.Message) });
            }
        }

    } //public partial class MainForm
}     // namespace Regata.Desktop.WinForms.Measurements
