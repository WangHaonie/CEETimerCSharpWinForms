using System;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Modules
{
    public enum MessageLevel
    {
        Info, Warning, Error
    }

    public class MessageX
    {
        public static void Popup(string Message, MessageLevel Level, Form ParentForm = null, TabControl ParentTabControl = null, TabPage ParentTabPage = null)
        {
            var (Icon, Title) = GetIconTitle(Level);
            
            if (ParentForm != null)
            {
                ParentForm.Invoke(new Action(() =>
                {
                    ParentForm.WindowState = FormWindowState.Normal;
                    ParentForm.Activate();
                    ParentTabControl.SelectedTab = ParentTabPage;
                    MessageBox.Show(Message, Title, MessageBoxButtons.OK, Icon);
                }));
            }
            else
            {
                MessageBox.Show(Message, Title, MessageBoxButtons.OK, Icon);
            }
        }

        public static void Popup(string Message, Exception ex, Form ParentForm = null, TabControl ParentTabControl = null, TabPage ParentTabPage = null)
        {
            if (ParentForm != null)
            {
                ParentForm.Invoke(new Action(() =>
                {
                    ParentForm.WindowState = FormWindowState.Normal;
                    ParentForm.Activate();
                    ParentTabControl.SelectedTab = ParentTabPage;
                    MessageBox.Show($"{Message}\n\n错误信息: \n{ex.Message}\n\n错误详情: \n{ex}", LaunchManager.ErrMsg, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }));
            }
            else
            {
                MessageBox.Show($"{Message}\n\n错误信息: \n{ex.Message}\n\n错误详情: \n{ex}", LaunchManager.ErrMsg, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static DialogResult Popup(string Message, MessageLevel Level, MessageBoxButtons Buttons)
        {
            var (Icon, Title) = GetIconTitle(Level);
            return MessageBox.Show(Message, Title, Buttons, Icon);
        }

        private static (MessageBoxIcon, string) GetIconTitle(MessageLevel Level)
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

            return (Icon, Title);
        }
    }
}
