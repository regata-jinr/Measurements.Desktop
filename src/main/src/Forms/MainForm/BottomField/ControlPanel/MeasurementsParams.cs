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

using Regata.Core.UI.WinForms.Controls;
using Regata.Core.Hardware;
using System.Windows.Forms;

namespace Regata.Desktop.WinForms.Measurements
{
    public partial class MainForm
    {
        public ControlsGroupBox controlsMeasParams;
        public CheckedArrayControl<Detector> CheckedDetectorArrayControl;
        public CheckedArrayControl<float> CheckedHeightArrayControl;
        public Button buttonShowAcqQueue;


    } //public partial class MainForm
}     // namespace Regata.Desktop.WinForms.Measurements
