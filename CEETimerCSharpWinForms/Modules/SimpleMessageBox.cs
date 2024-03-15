using System;
using System.Windows.Forms;
using static CEETimerCSharpWinForms.LaunchManager;

namespace CEETimerCSharpWinForms
{
    public static class SimpleMessageBox
    {
        public enum MessageLevel
        {
            Info,
            Warning,
            Error
        }

        public static void Popup(string Message, MessageLevel Level)
        {
            MessageBoxIcon Icon = MessageBoxIcon.None;
            string Title = "";

            switch (Level)
            {
                case MessageLevel.Info:
                    Icon = MessageBoxIcon.Information;
                    Title = InfoMsg;
                    break;
                case MessageLevel.Warning:
                    Icon = MessageBoxIcon.Warning;
                    Title = WarnMsg;
                    break;
                case MessageLevel.Error:
                    Icon = MessageBoxIcon.Error;
                    Title = ErrMsg;
                    break;
            }

            MessageBox.Show(Message, Title, MessageBoxButtons.OK, Icon);
        }

        public static void Popup(string Message, MessageLevel Level, Form ThisForm)
        {
            MessageBoxIcon Icon = MessageBoxIcon.None;
            string Title = "";

            switch (Level)
            {
                case MessageLevel.Info:
                    Icon = MessageBoxIcon.Information;
                    Title = InfoMsg;
                    break;
                case MessageLevel.Warning:
                    Icon = MessageBoxIcon.Warning;
                    Title = WarnMsg;
                    break;
                case MessageLevel.Error:
                    Icon = MessageBoxIcon.Error;
                    Title = ErrMsg;
                    break;
            }

            ThisForm?.Invoke(new Action(() =>
            {
                MessageBox.Show(Message, Title, MessageBoxButtons.OK, Icon);
            }));
        }

        public static DialogResult Popup(string Message, MessageLevel Level, MessageBoxButtons Buttons)
        {
            MessageBoxIcon Icon = MessageBoxIcon.None;
            string Title = "";

            switch (Level)
            {
                case MessageLevel.Info:
                    Icon = MessageBoxIcon.Information;
                    Title = InfoMsg;
                    break;
                case MessageLevel.Warning:
                    Icon = MessageBoxIcon.Warning;
                    Title = WarnMsg;
                    break;
                case MessageLevel.Error:
                    Icon = MessageBoxIcon.Error;
                    Title = ErrMsg;
                    break;
            }

            return MessageBox.Show(Message, Title, Buttons, Icon);
        }
    }
}
