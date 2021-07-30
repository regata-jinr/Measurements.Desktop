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

using Regata.Core.Hardware;
using Regata.Core.Settings;
using Regata.Core.UI.WinForms;
using Regata.Core.UI.WinForms.Items;


namespace Regata.Desktop.WinForms.Measurements
{
    public partial class MainForm
    {

        private void InitMenuStrip()
        {
            mainForm.LangItem.CheckedChanged += () => Settings<MeasurementsSettings>.CurrentSettings.CurrentLanguage = mainForm.LangItem.CheckedItem;
            mainForm.LangItem.CheckItem(Settings<MeasurementsSettings>.CurrentSettings.CurrentLanguage);

            AcquisitionModeItems.CheckItem(CanberraDeviceAccessLib.AcquisitionModes.aCountToRealTime);

            AcquisitionModeItems.CheckedChanged += () => AssignRecordsMainRDGV("AcqMode", (int)AcquisitionModeItems.CheckedItem);


            mainForm.MenuStrip.Items.Add(MeasurementsTypeItems.EnumMenuItem);
            mainForm.MenuStrip.Items.Add(AcquisitionModeItems.EnumMenuItem);

            MeasurementsTypeItems.CheckedChanged += async () =>
            {
                mainForm.TabsPane[0, 0].DataSource = null;
                mainForm.TabsPane[1, 0].DataSource = null;
                mainForm.TabsPane[0, 1].DataSource = null;
                mainForm.TabsPane[1, 1].DataSource = null;

                CurrentMeasurementsRegister.Type = (int)MeasurementsTypeItems.CheckedItem;

                DurationControl.Duration = CurrentMeasurementsRegister.Type switch
                {
                    0 => new System.TimeSpan(0, 0, 15, 0),
                    1 => new System.TimeSpan(0, 2, 0, 0),
                    2 => new System.TimeSpan(0, 2, 0, 0),
                    _ => new System.TimeSpan(),
                };

                #region Filling list of registers

                await FillIrradiationRegisters();
                await FillMeasurementsRegisters();

                #endregion

                Labels.SetControlsLabels(mainForm);

            };


        }

    } //public partial class MainForm
}     // namespace Regata.Desktop.WinForms.Measurements
