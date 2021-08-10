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
using Regata.Core.UI.WinForms.Controls.Settings;
using System.Collections.Generic;

namespace Regata.Desktop.WinForms.Measurements
{
    public class MeasurementsSettings : ASettings
    {
        public int DefaultSLITime { get; set; } = 900;
        public int DefaultLLI1Time { get; set; } = 7200;
        public int DefaultLLI2Time { get; set; }  = 7200;
        public int BackgroundRegistersUpdateTime { get; set; } = 60;
        public int DefaultPopUpMessageTimeout { get; set; } = 5;
        public CanberraDeviceAccessLib.AcquisitionModes AcquisitionMode { get; set; } = CanberraDeviceAccessLib.AcquisitionModes.aCountToRealTime;

        public RDataGridViewSettings MainTableSettings { get; set; } = new RDataGridViewSettings() 
        { 
            HidedColumns = new List<string>() { "Id", "IrradiationId", "RegId", "Assistant", "AcqMode", "Type", "SetKey", "SampleKey"} 
        };


    } // class MeasurementsSettings : ASettings
}     // namespace Regata.Desktop.WinForms.Measurements
