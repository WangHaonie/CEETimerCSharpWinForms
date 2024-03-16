using System;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Modules
{
    public enum MessageLevel
    {
        Info,
        Warning,
        Error
    }

    public class MessageX
    {
        public static void Popup(string Message, MessageLevel Level)
        {
            MessageBoxIcon Icon = MessageBoxIcon.None;
            string Title = "";

            switch (Level)
            {
                case MessageLevel.Info:
                    Icon = MessageBoxIcon.Information;
                    Title = LaunchManager.InfoMsg;
                    break;
                case MessageLevel.Warning:
                    Icon = MessageBoxIcon.Warning;
                    Title = LaunchManager.WarnMsg;
                    break;
                case MessageLevel.Error:
                    Icon = MessageBoxIcon.Error;
                    Title = LaunchManager.ErrMsg;
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
                    Title = LaunchManager.InfoMsg;
                    break;
                case MessageLevel.Warning:
                    Icon = MessageBoxIcon.Warning;
                    Title = LaunchManager.WarnMsg;
                    break;
                case MessageLevel.Error:
                    Icon = MessageBoxIcon.Error;
                    Title = LaunchManager.ErrMsg;
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
                    Title = LaunchManager.InfoMsg;
                    break;
                case MessageLevel.Warning:
                    Icon = MessageBoxIcon.Warning;
                    Title = LaunchManager.WarnMsg;
                    break;
                case MessageLevel.Error:
                    Icon = MessageBoxIcon.Error;
                    Title = LaunchManager.ErrMsg;
                    break;
            }

            return MessageBox.Show(Message, Title, Buttons, Icon);
        }
    }
}
