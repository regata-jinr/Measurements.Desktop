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
using Regata.Core.Hardware;
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

        private ToolStripMenuItem _scFlagMenuItem;
        private ToolStripMenuItem _SLIShowAlreadyAdded;
        private ToolStripMenuItem _SLIMenuItem;
        private ToolStripMenuItem _scDropDownMenu;
        private ToolStripMenuItem _showDevCams;


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

                _showDevCams = new ToolStripMenuItem();
                _showDevCams.Name = "showDevCams";
                _showDevCams.Click += (s, e) => SampleChanger.ShowDevicesCams();

                _scDropDownMenu = new ToolStripMenuItem();
                _scDropDownMenu.Name = "scDropDownMenu";
                _scDropDownMenu.DropDownItems.Add(_scFlagMenuItem);
                _scDropDownMenu.DropDownItems.Add(_showDevCams);


                _SLIShowAlreadyAdded= new ToolStripMenuItem();
                _SLIShowAlreadyAdded.CheckOnClick = true;
                _SLIShowAlreadyAdded.Name = "SLIShowAlreadyAdded";
                _SLIShowAlreadyAdded.CheckedChanged += _SLIShowAlreadyAdded_CheckedChanged;
                    
                _SLIMenuItem = new ToolStripMenuItem();
                _SLIMenuItem.Name = "SLIMenuItem";
                _SLIMenuItem.Visible = false;
                _SLIMenuItem.DropDownItems.Add(_SLIShowAlreadyAdded);

                mainForm.MenuStrip.Items.Insert(0,_scDropDownMenu);
                mainForm.MenuStrip.Items.Insert(0, VerbosityItems.EnumMenuItem);
                mainForm.MenuStrip.Items.Insert(0, _SLIMenuItem);
                mainForm.MenuStrip.Items.Insert(0, AcquisitionModeItems.EnumMenuItem);
                mainForm.MenuStrip.Items.Insert(0, MeasurementsTypeItems.EnumMenuItem);

                MeasurementsTypeItems.CheckedChanged += async () =>
                {
                    mainForm.TabsPane[0, 0].DataSource = null;
                    mainForm.TabsPane[1, 0].DataSource = null;
                    mainForm.TabsPane[0, 1].DataSource = null;
                    mainForm.TabsPane[1, 1].DataSource = null;


                    CurrentMeasurementsRegister.Type = (int)MeasurementsTypeItems.CheckedItem;

                    if (MeasurementsTypeItems.CheckedItem == Core.DataBase.Models.MeasurementsType.sli)
                    { 
                        _SLIMenuItem.Visible = true;
                        mainForm.MainRDGV.Columns["DateTimeFinish"].Visible = false;
                        mainForm.MainRDGV.Columns["DiskPosition"].Visible = false;
                    }
                    else
                    {
                        _SLIMenuItem.Visible = false;
                        mainForm.MainRDGV.Columns["DateTimeFinish"].Visible = true;
                        mainForm.MainRDGV.Columns["DiskPosition"].Visible = true;
                        _scFlagMenuItem.Checked = true;
                    }

                    DurationControl.Duration = CurrentMeasurementsRegister.Type switch
                    {
                        0 => TimeSpan.FromSeconds(Settings<MeasurementsSettings>.CurrentSettings.DefaultSLITime),
                        1 => TimeSpan.FromSeconds(Settings<MeasurementsSettings>.CurrentSettings.DefaultLLI1Time),
                        2 => TimeSpan.FromSeconds(Settings<MeasurementsSettings>.CurrentSettings.DefaultLLI2Time),
                        _ => TimeSpan.FromSeconds(0),
                    };

                    var h = Settings<MeasurementsSettings>.CurrentSettings.DefaultSLIHeight switch
                    {
                        <= 2.5f => 2.5f,
                        <= 5    => 5f,
                        <= 10   => 10f,
                        _       => 20f
                    };

                    CheckedHeightArrayControl.SelectItem(h);

                    ClearCurrentRegister();

                    await FillIrradiationRegisters();
                    await FillMeasurementsRegisters();

                    Labels.SetControlsLabels(mainForm);

                    mainForm.buttonAddAllSamples.Enabled = true;
                    mainForm.buttonAddSelectedSamplesToReg.Enabled = true;
                    mainForm.buttonRemoveSelectedSamples.Enabled = true;

                };
            }
            catch (Exception ex)
            {
                Report.Notify(new RCM.Message(Codes.ERR_UI_WF_INI_MENU) { DetailedText = string.Join("--", ex.Message, ex?.InnerException?.Message) });
            }
        }

        private void _SLIShowAlreadyAdded_CheckedChanged(object sender, EventArgs e)
        {
            if (mainForm.MainRDGV == null || mainForm.MainRDGV.DataSource == null) return;

            var added_ids = mainForm.MainRDGV.CurrentDbSet.Local.Select(m => m.IrradiationId).ToList();
            var cti = mainForm.TabsPane.SelectedTabIndex;

            var nameId = cti == 0 ? "Id" : "IrradiationId";

            mainForm.TabsPane[cti, 1].ClearSelection();
            mainForm.TabsPane[cti, 1].CurrentCell = null;

            foreach (DataGridViewRow row in mainForm.TabsPane[cti, 1].Rows)
            {
                if (_SLIShowAlreadyAdded.Checked && added_ids.Contains((int)row.Cells[nameId].Value))
                {
                    row.Visible = false;
                    continue;
                }
                row.Visible = true;
            }
        }

        private async void _scFlagMenuItem_CheckedChanged(object sender, EventArgs e)
        {
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
