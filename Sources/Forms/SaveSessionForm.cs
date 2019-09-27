using System;
using System.Windows.Forms;
using Measurements.Core;

namespace Measurements.UI.Desktop.Forms
{
    public partial class SaveSessionForm : Form
    {
        public event Action<string, bool> SaveSessionEvent;
        public SaveSessionForm(string name)
        {
            InitializeComponent();
            textBoxSaveSessionName.Text = name;
            Text = $"Сохранение сессии измерений [{name}]| Regata Measurements UI - {LoginForm.CurrentVersion} | [{SessionControllerSingleton.ConnectionStringBuilder.UserID}]";
        }

        private void buttonSaveSessionSave_Click(object sender, System.EventArgs e)
        {
            SaveSessionEvent?.Invoke(textBoxSaveSessionName.Text, checkBoxSaveSessionIsGlobal.Checked);
            Close();
        }
    }
}
