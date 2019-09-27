using System;
using System.Linq;
using System.Windows.Forms;
using Measurements.Core;
using Measurements.Core.Handlers;
using Measurements.UI.Managers;

namespace Measurements.UI.Desktop.Forms
{
    public partial class SessionControlPanel : Form
    {
        public SessionControlPanel(string connectionString)
        {
            try
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

                SessionControllerSingleton.SessionsInfoListHasChanged += UpdateSessionsTable;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionNotify(this, ex, ExceptionLevel.Error);
            }
        }

        private void UpdateSessionsTable()
        {
            var source = new BindingSource();
            source.DataSource = SessionControllerSingleton.AvailableSessions;
            SessionControlPaneldataGridViewSessions.DataSource = null;
            SessionControlPaneldataGridViewSessions.DataSource = source;
        }

        private void ShowAbout(object sender, EventArgs eventArgs)
        {
            var ab = new About();
            ab.Show();
        }

        private bool _isExiting = false;
        private void CloseClick(object sender, System.ComponentModel.CancelEventArgs eventArgs)
        {
            if (!_isExiting)
            {
                if (SessionControllerSingleton.ManagedSessions.Count != 0)
                {
                    var result = MessageBox.Show($"Панель управления сессиями конртолирует некоторые сессии. Вы уверены, что хотите выйти?{Environment.NewLine}Это повлечет за собой удаление всех данных, которые не сохранены.", "Warning", MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation);
                    if (result == DialogResult.Yes)
                    {
                        for(var i = SessionControllerSingleton.ManagedSessions.Count - 1; i >=0; --i)
                            SessionControllerSingleton.ManagedSessions[i].Dispose();

                        SessionControllerSingleton.ManagedSessions.Clear();
                        _isExiting = true;
                        Application.Exit();
                    }
                    else eventArgs.Cancel = true;
                }
                else
                {
                    _isExiting = true;
                    Dispose();
                    Application.Exit();
                }
            }
        }

        private void SessionControlPanelButtonLoadSession_Click(object sender, EventArgs e)
        {
            try
            {
                if (SessionControlPaneldataGridViewSessions.SelectedRows.Count == 0)
                    MessageBoxTemplates.WarningAsync("Выделите сессию для загрузки!");

                //var dNames = SessionControlPaneldataGridViewSessions.SelectedRows[0].Cells[1].Value.ToString().Split(',');

                //foreach (var dName in dNames)
                //{
                //    if (!SessionControllerSingleton.AvailableDetectors.Any(d => d.Name == dName))
                //    {
                //        MessageBoxTemplates.WarningAsync($"Детектор {dName} недоступен. Вероятно, он контролируется другой сессией.");
                //        return;
                //    }
                //}

                ISession session = SessionControllerSingleton.Load(SessionControlPaneldataGridViewSessions.SelectedRows[0].Cells[0].Value.ToString());
                var sessionForm = new SessionForm(session);
                sessionForm.Show();
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionNotify(this, ex, ExceptionLevel.Error); 
            }
        }

        private void SessionControlPanelButtonCreateSession_Click(object sender, EventArgs e)
        {
            try
            {
                var sessionForm = new SessionForm(new Session());
                sessionForm.Show();
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionNotify(this, ex, ExceptionLevel.Error);
            }
        }
    }
}
