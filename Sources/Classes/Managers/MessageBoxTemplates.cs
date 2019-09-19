using System;
using System.Text;
using System.Windows.Forms;
using Measurements.Core.Handlers;

namespace Measurements.UI.Managers
{
    public static class MessageBoxTemplates
    {
        static MessageBoxTemplates()
        {
            ExceptionHandler.ExceptionEvent += WrapExceptionToMessageBox;
        }

        //FIXME: now such notification (via message box) paused measurements process. In case of errors
        //       this is correct behaviour, user should decide what he wants to do retry or cancel and change something
        //       but for warnings and successes it should has timeout

        public static void Error(string message)
        {
            var result =  MessageBox.Show(message, $"Error!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

            if (result == DialogResult.Cancel)
            {

            }
            else
            {

            }

        }

        public static void TimeOutMessage(string message, int timeOutSec = 5)
        {

            
        }

        public static void Success(string message) => MessageBox.Show(message, $"Success!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        public static void Warning(string message) => MessageBox.Show(message, $"Warning!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        public static void Info(string message)    => MessageBox.Show(message, $"Info!", MessageBoxButtons.OK, MessageBoxIcon.Information);

        public static void CallStaticCtor() { }

        private static string MessageTemplate(ref ExceptionEventsArgs exceptionEventsArgs)
        {
            var stringBuilder = new StringBuilder();
            var ex = exceptionEventsArgs.exception;
            stringBuilder.Append($"Assembly name: {ex.Data["Assembly"].ToString()}{Environment.NewLine}");
            stringBuilder.Append($"Instanse name: {ex.TargetSite.DeclaringType}{Environment.NewLine}");
            stringBuilder.Append($"Member type:   {ex.TargetSite.MemberType}{Environment.NewLine}");
            stringBuilder.Append($"Member name:   {ex.TargetSite.Name}{Environment.NewLine}");
            stringBuilder.Append($"Message:       {ex.Message}{Environment.NewLine}");
            stringBuilder.Append($"Stack trace:   {ex.StackTrace}");

            return stringBuilder.ToString();
        }

        public static void WrapExceptionToMessageBox(ExceptionEventsArgs eventsArgs)
        {
            if (eventsArgs.Level == ExceptionLevel.error)
                Error(MessageTemplate(ref eventsArgs));

            if (eventsArgs.Level == ExceptionLevel.warning)
                Warning(MessageTemplate(ref eventsArgs));

            if (eventsArgs.Level == ExceptionLevel.info)
                Info(MessageTemplate(ref eventsArgs));
        }
    }
}
