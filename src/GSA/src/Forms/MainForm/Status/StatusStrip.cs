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
using System.Linq;


namespace Regata.Desktop.WinForms.Measurements
{
    public partial class MainForm
    {
        private void InitStatusStrip()
        {
            mainForm.StatusStrip.Items.Add(MeasurementsTypeItems.EnumStatusLabel);
        }
    } //public partial class MainForm
}     // namespace Regata.Desktop.WinForms.Measurements
