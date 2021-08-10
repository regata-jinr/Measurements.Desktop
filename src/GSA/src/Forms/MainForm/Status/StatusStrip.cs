﻿/***************************************************************************
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
using System;

namespace Regata.Desktop.WinForms.Measurements
{
    public partial class MainForm
    {
        private void InitStatusStrip()
        {
            try
            {
                mainForm.StatusStrip.Items.Add(MeasurementsTypeItems.EnumStatusLabel);
                mainForm.StatusStrip.Items.Add(AcquisitionModeItems.EnumStatusLabel);
                mainForm.StatusStrip.Items.Add(VerbosityItems.EnumStatusLabel);
            }
            catch (Exception ex)
            {
                Report.Notify(new Message(Codes.ERR_UI_WF_INI_STAT) { DetailedText = ex.ToString() });
            }
        }
    } //public partial class MainForm
}     // namespace Regata.Desktop.WinForms.Measurements
