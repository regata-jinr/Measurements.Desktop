/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2017-2021, REGATA Experiment at FLNP|JINR                  *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 *                                                                         *
 * The REGATA Experiment team license this file to you under the           *
 * GNU GENERAL PUBLIC LICENSE                                              *
 *                                                                         *
 ***************************************************************************/

using Regata.Core.Settings;
using System;
using System.Windows.Forms;

namespace Regata.Desktop.WinForms.Measurements
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Settings<MeasurementsSettings>.AssemblyName = "Measurements.Desktop";

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (var m = new MainForm())
            {
                Application.Run(m.mainForm);
            }
        }
    }
}
