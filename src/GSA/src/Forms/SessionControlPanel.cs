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

using System;
using System.Linq;
using System.Windows.Forms;

namespace Regata.Desktop.WinForms.Measurements
{
    public partial class SessionControlPanel : Form
    {
        public SessionControlPanel(string connectionString)
        {
            try
            {
                InitializeComponent();
                SessionControllerSingleton.InitializeDBConnectionString(connectionString);

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
            try
            {
                var source = new BindingSource();
                source.DataSource = SessionControllerSingleton.AvailableSessions;
                SessionControlPaneldataGridViewSessions.DataSource = null;
                SessionControlPaneldataGridViewSessions.DataSource = source;
            }
            catch (Exception ex)
            {
                MessageBoxTemplates.WrapExceptionToMessageBoxAsync(new Core.Handlers.ExceptionEventsArgs()
                {
                    exception = ex,
                    Level = Core.Handlers.ExceptionLevel.Error
                });
            }
        }

        private void ShowAbout(object sender, EventArgs eventArgs)
        {
            try
            {
                var ab = new About();
                ab.Show();
            }
            catch (Exception ex)
            {
                MessageBoxTemplates.WrapExceptionToMessageBoxAsync(new Core.Handlers.ExceptionEventsArgs()
                {
                    exception = ex,
                    Level = Core.Handlers.ExceptionLevel.Error
                });
            }
        }

        private bool _isExiting = false;
        private void CloseClick(object sender, System.ComponentModel.CancelEventArgs eventArgs)
        {
            try
            {
                if (!_isExiting)
                {
                    if (SessionControllerSingleton.ManagedSessions.Count != 0)
                    {
                        var result = MessageBox.Show($"Панель управления сессиями конртолирует некоторые сессии. Вы уверены, что хотите выйти?{Environment.NewLine}Это повлечет за собой удаление всех данных, которые не сохранены.", "Warning", MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation);
                        if (result == DialogResult.Yes)
                        {
                            for (var i = SessionControllerSingleton.ManagedSessions.Count - 1; i >= 0; --i)
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
            catch (Exception ex)
            {
                MessageBoxTemplates.WrapExceptionToMessageBoxAsync(new Core.Handlers.ExceptionEventsArgs()
                {
                    exception = ex,
                    Level = Core.Handlers.ExceptionLevel.Error
    });
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
