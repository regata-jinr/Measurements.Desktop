using System;
using System.Windows.Forms;

namespace Measurements.UI.Managers
{
    public static class MessageBoxTemplates
    {
        public static void Error(string message)
        {
            MessageBox.Show(message, $"Error in Regata Measurements UI", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
        }
        public static void Success(string message)
        {
            MessageBox.Show(message, $"Action has completed in Regata Measurements UI", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
