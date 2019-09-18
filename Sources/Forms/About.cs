using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Measurements.UI.Forms
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
            Text = $"Информация о программе | Regata Measurements UI - {LoginForm.CurrentVersion} | [{Measurements.Core.SessionControllerSingleton.ConnectionStringBuilder.UserID}]";

            linkLabel1.Click += GoToManual;
            linkLabel2.Click += GoToMonitor;

        }

        private void GoToManual(object sender, EventArgs eventArgs)
        {
            System.Diagnostics.Process.Start("https://github.com/regata-jinr/MeasurementsUI/blob/master/README.md");
        }

        private void GoToMonitor(object sender, EventArgs eventArgs)
        {
            System.Diagnostics.Process.Start("http://regata.jinr.ru/monitor/");
        }
    }
}
