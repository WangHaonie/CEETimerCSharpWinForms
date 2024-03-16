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
        /// <summary>
        /// 显示一个简单的消息框
        /// </summary>
        /// <param name="Message">要显示的消息</param>
        /// <param name="Level">消息等级</param>
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

        /// <summary>
        /// 在 UI 线程上显示一个简单的消息框
        /// </summary>
        /// <param name="Message">要显示的消息</param>
        /// <param name="Level">消息等级</param>
        /// <param name="WhichForm">父窗体</param>
        public static void Popup(string Message, MessageLevel Level, Form WhichForm)
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

            WhichForm?.Invoke(new Action(() =>
            {
                WhichForm.Activate();
                MessageBox.Show(Message, Title, MessageBoxButtons.OK, Icon);
            }));
        }

        /// <summary>
        /// 显示一个具有 DialogResult 返回值的可以自定义按钮的简单的消息框
        /// </summary>
        /// <param name="Message">要显示的消息</param>
        /// <param name="Level">消息等级</param>
        /// <param name="Buttons">要显示的按钮</param>
        /// <returns></returns>
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

        /// <summary>
        /// 显示一个专门用来显示异常的简单的消息框，默认同时显示错误信息和错误详情
        /// </summary>
        /// <param name="Message">要显示的内容</param>
        /// <param name="ex">异常信息</param>
        public static void Popup(string Message, Exception ex)
        {
            MessageBox.Show($"{Message}\n\n错误信息: \n{ex.Message}\n\n错误详情: \n{ex}", LaunchManager.ErrMsg, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// 在 UI 线程上显示一个专门用来显示异常的简单的消息框，默认同时显示错误信息和错误详情
        /// </summary>
        /// <param name="Message">要显示的内容</param>
        /// <param name="ex">异常信息</param>
        /// <param name="WhichForm">父窗体</param>
        public static void Popup(string Message, Exception ex, Form WhichForm)
        {
            WhichForm?.Invoke(new Action(() =>
            {
                WhichForm.Activate();
                MessageBox.Show($"{Message}\n\n错误信息: \n{ex.Message}\n\n错误详情: \n{ex}", LaunchManager.ErrMsg, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }));
        }
    }
}
