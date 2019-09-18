using System;
using System.Windows.Forms;
using Measurements.Core.Handlers;

namespace Measurements.UI.Managers
{
    public static class MessageBoxTemplates
    {

        static MessageBoxTemplates()
        {
            ExceptionHandler.ExceptionEvent += CoreExceptionHandler;
        }

        public static void Error(string message) => MessageBox.Show(message, $"Error has occurred in Regata Measurements", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
        public static void Success(string message) => MessageBox.Show(message, $"Action has completed in Regata Measurements", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        public static void Warning(string message) => MessageBox.Show(message, $"Warning has occurred in Regata Measurements", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        public static void Info(string message) => MessageBox.Show(message, $"Warning has occurred in Regata Measurements", MessageBoxButtons.OK, MessageBoxIcon.Information);

        public static void CallStaticCtor() { }

        public static void CoreExceptionHandler(object sender, ExceptionEventsArgs eventsArgs)
        {
            if (eventsArgs.Level == ExceptionLevel.error)
                Error($"{eventsArgs.Source} has generated exception in [{eventsArgs.TargetSite}]. The message is '{eventsArgs.Message}'{Environment.NewLine}Stack trace is:'{eventsArgs.StackTrace}'");
            if (eventsArgs.Level == ExceptionLevel.warning)
                Warning($"{eventsArgs.Source} has generated exception in [{eventsArgs.TargetSite}]. The message is '{eventsArgs.Message}'{Environment.NewLine}Stack trace is:'{eventsArgs.StackTrace}'");
            if (eventsArgs.Level == ExceptionLevel.info)
                Info($"{eventsArgs.Source} has generated exception in [{eventsArgs.TargetSite}]. The message is '{eventsArgs.Message}'{Environment.NewLine}Stack trace is:'{eventsArgs.StackTrace}'");



        }
    }
}
