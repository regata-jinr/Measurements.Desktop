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

        //TODO:  add icons to AutoClosingMessageBox

        public static void Error(string message)
        {
            var result =  AutoClosingMessageBox.Show(message, $"Error! Window will close in 5 sec.",5000, MessageBoxButtons.RetryCancel);

            if (result == DialogResult.Cancel)
            {

            }
            else
            {

            }

        }

        public static void Success(string message) => AutoClosingMessageBox.Show(message, $"Success! Window will close in 3 sec.", 3000, MessageBoxButtons.OK);
        public static void Warning(string message) => AutoClosingMessageBox.Show(message, $"Warning! Window will close in 3 sec.", 3000, MessageBoxButtons.OK);
        public static void Info(string message)    => AutoClosingMessageBox.Show(message, $"Info! Window will close in 3 sec.",    3000, MessageBoxButtons.OK);

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
            if (eventsArgs.Level == ExceptionLevel.Error)
                Error(MessageTemplate(ref eventsArgs));

            if (eventsArgs.Level == ExceptionLevel.Warn)
                Warning(MessageTemplate(ref eventsArgs));

            if (eventsArgs.Level == ExceptionLevel.Info)
                Info(MessageTemplate(ref eventsArgs));
        }
    }
}
