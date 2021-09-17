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
using Regata.Core.UI.WinForms.Forms;
using System;
using System.Windows.Forms;

namespace Regata.Desktop.WinForms.Measurements
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Settings<MeasurementsSettings>.AssemblyName = "MeasurementsDesktop";

            using (var lf = new LoginForm())
            {
                lf.ConnectionSuccessfull += (sqlcs) => 
                { var m = new MainForm(); m.mainForm.Show(); m.mainForm.FormClosed += (s,e) => lf.Close(); GlobalSettings.User = sqlcs.UserID; };
                Application.Run(lf);
            }
        }
    }
}
