using System;
using System.Windows.Forms;
using Measurements.UI.Managers;

namespace Measurements.UI.Desktop.Forms
{
    public partial class CountsForm : Form
    {
        public event Action<int> SaveCountsEvent;
        public CountsForm(int counts)
        {
            InitializeComponent();
            var ts = TimeSpan.FromSeconds(counts);
            numericUpDownHours.Value = ts.Hours;
            numericUpDownMinutes.Value = ts.Minutes;
            numericUpDownSeconds.Value = ts.Seconds;
            FormClosing += CountsForm_FormClosing;
        }

        private void CountsForm_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            e.Cancel = true;
            Hide();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            numericUpDownHours.Value = 0;
            numericUpDownMinutes.Value = 0;
            numericUpDownSeconds.Value = 0;
        }

        private void buttonSaveCounts_Click(object sender, EventArgs e)
        {
            int counts = Convert.ToInt32(numericUpDownHours.Value) * 3600 + Convert.ToInt32(numericUpDownMinutes.Value) * 60 + Convert.ToInt32(numericUpDownSeconds.Value);
            if (counts == 0)
            {
                MessageBoxTemplates.ErrorSync("Продолжительность измерений не может быть равна нулю"); 
                return;
            }
            SaveCountsEvent?.Invoke(counts);
        }

        

    }
}
