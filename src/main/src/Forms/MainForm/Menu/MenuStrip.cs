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

using Regata.Core.Settings;
using Regata.Core.UI.WinForms;


namespace Regata.Desktop.WinForms.Measurements
{
    public partial class MainForm
    {

        private void InitMenuStrip()
        {
            mainForm.LangItem.CheckedChanged += () => Settings<MeasurementsSettings>.CurrentSettings.CurrentLanguage = mainForm.LangItem.CheckedItem;
            mainForm.LangItem.CheckItem(Settings<MeasurementsSettings>.CurrentSettings.CurrentLanguage);

            mainForm.MenuStrip.Items.Add(MeasurementsTypeItems.EnumMenuItem);

            MeasurementsTypeItems.CheckedChanged += async () =>
            {
                mainForm.TabsPane[0, 0].DataSource = null;
                mainForm.TabsPane[1, 0].DataSource = null;
                mainForm.TabsPane[0, 1].DataSource = null;
                mainForm.TabsPane[1, 1].DataSource = null;

                CurrentMeasurementsRegister.Type = (int)MeasurementsTypeItems.CheckedItem;

                #region Filling list of registers

                await FillIrradiationRegisters();
                await FillMeasurementsRegisters();

                #endregion

                Labels.SetControlsLabels(mainForm.Controls);

            };


        }

    } //public partial class MainForm
}     // namespace Regata.Desktop.WinForms.Measurements
