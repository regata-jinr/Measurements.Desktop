using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Measurements.Core;

namespace Measurements.UI.Forms
{
    public partial class SessionControlPanel : Form
    {
        
        public SessionControlPanel(string connectionString)
        {
            InitializeComponent();
#if DEBUG
            SessionControllerSingleton.InitializeDBConnectionString(@"Server=RUMLAB\REGATALOCAL;Database=NAA_DB_TEST;Trusted_Connection=True;User Id=bdrum");
#else
            SessionControllerSingleton.InitializeDBConnectionString(connectionString);
#endif

            Text = $"Панель управления сессиями | Regata Measurements UI - {LoginForm.CurrentVersion} | [{SessionControllerSingleton.ConnectionStringBuilder.UserID}]";
            var source = new BindingSource();
            source.DataSource = SessionControllerSingleton.AvailableSessions;

            SessionControlPaneldataGridViewSessions.DataSource = source;
            

        }

        private void ShowAbout(object sender, EventArgs eventArgs)
        {
            var ab = new About();
            ab.Show();
        }
        private void CloseClick(object sender, EventArgs eventArgs)
        {
            Application.Exit();
        }
    }
}
