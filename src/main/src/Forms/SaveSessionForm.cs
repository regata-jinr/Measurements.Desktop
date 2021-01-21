using System;
using System.Windows.Forms;
using Measurements.Core;
using Measurements.UI.Managers;

namespace Measurements.UI.Desktop.Forms
{
    public partial class SaveSessionForm : Form
    {
        public event Action<string, bool> SaveSessionEvent;
        public SaveSessionForm(string name)
        {
            try
            {
                InitializeComponent();
                textBoxSaveSessionName.Text = name;
                Text = $"Сохранение сессии измерений [{name}]| Regata Measurements UI - {LoginForm.CurrentVersion} | [{SessionControllerSingleton.ConnectionStringBuilder.UserID}]";
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

        private void buttonSaveSessionSave_Click(object sender, System.EventArgs e)
        {
            try
            {
                SaveSessionEvent?.Invoke(textBoxSaveSessionName.Text, checkBoxSaveSessionIsGlobal.Checked);
                Close();
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

        private void buttonSaveSessionCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
