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
using RCM = Regata.Core.Messages;
using Regata.Core.Settings;
using Regata.Core.UI.WinForms;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Regata.Desktop.WinForms.Measurements
{
    public partial class MainForm
    {

        ToolStripMenuItem _scFlagMenuItem;
        ToolStripMenuItem _scDropDownMenu;

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

                _scFlagMenuItem = new ToolStripMenuItem();
                _scFlagMenuItem.CheckOnClick = true;
                _scFlagMenuItem.Name = "scFlagMenuItem";
                _scFlagMenuItem.CheckedChanged += _scFlagMenuItem_CheckedChanged;
                

                _scDropDownMenu = new ToolStripMenuItem();
                _scDropDownMenu.Name = "scDropDownMenu";
                _scDropDownMenu.DropDownItems.Add(_scFlagMenuItem);

                mainForm.MenuStrip.Items.Insert(0,_scDropDownMenu);
                mainForm.MenuStrip.Items.Insert(0, VerbosityItems.EnumMenuItem);
                mainForm.MenuStrip.Items.Insert(0, AcquisitionModeItems.EnumMenuItem);
                mainForm.MenuStrip.Items.Insert(0,MeasurementsTypeItems.EnumMenuItem);

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
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_INI_MENU) { DetailedText = string.Join("--", ex.Message, ex?.InnerException?.Message) });
            }
        }

        private async void _scFlagMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (_scFlagMenuItem.Checked)
            {
                buttonHalt.Enabled   = true;
                buttonStopSC.Enabled = true;
            }
            else
            {
                buttonHalt.Enabled   = false;
                buttonStopSC.Enabled = false;
            }

            if (_detectors == null)
                return;
            foreach (var d in _detectors)
            {
                if (!_scFlagMenuItem.Checked)
                    d.DisableXemo();
                else
                    d.EnableXemo();
            }

            if (_scFlagMenuItem.Checked)
                await Task.WhenAll(_detectors.Select(d => CallHomeAsync(d.PairedXemoDevice)));

        }

    } // public partial class MainForm
}     // namespace Regata.Desktop.WinForms.Measurements
