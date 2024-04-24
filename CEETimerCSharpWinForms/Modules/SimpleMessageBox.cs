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
        /// <summary>
        /// 在指定的窗体或定位到其标签页上或直接显示一个具有 DialogResult 返回值的消息框
        /// </summary>
        /// <param name="Message">消息内容</param>
        /// <param name="Level">消息等级</param>
        /// <param name="ParentForm">[可选] 父窗体</param>
        /// <param name="ParentTabControl">[可选] 父窗体的 TabControl</param>
        /// <param name="ParentTabPage">[可选] 父窗体的标签页</param>
        /// <param name="Buttons">[可选] 要显示的按钮</param>
        /// <returns></returns>
        public static DialogResult Popup(string Message, MessageLevel Level, Form ParentForm = null, TabControl ParentTabControl = null, TabPage ParentTabPage = null, MessageBoxButtons Buttons = MessageBoxButtons.OK)
        {
            var (Icon, Title) = GetIconTitle(Level);
            return PopupCore(Message, Title, Buttons, Icon, ParentForm, ParentTabControl, ParentTabPage);
        }

        /// <summary>
        /// 在指定的窗体或定位到其标签页上或直接显示一个只包含错误的消息框
        /// </summary>
        /// <param name="Message">消息内容</param>
        /// <param name="ex">异常</param>
        /// <param name="ParentForm">[可选] 父窗体</param>
        /// <param name="ParentTabControl">[可选] 父窗体的 TabControl</param>
        /// <param name="ParentTabPage">[可选] 父窗体的标签页</param>
        public static void Popup(string Message, Exception ex, Form ParentForm = null, TabControl ParentTabControl = null, TabPage ParentTabPage = null)
        {
            PopupCore($"{Message}\n\n错误信息: \n{ex.Message}\n\n错误详情: \n{ex}", LaunchManager.ErrMsg, MessageBoxButtons.OK, MessageBoxIcon.Error, ParentForm, ParentTabControl, ParentTabPage);
        }

        private static DialogResult PopupCore(string Message, string Title, MessageBoxButtons Buttons, MessageBoxIcon Icon, Form ParentForm, TabControl ParentTabControl, TabPage ParentTabPage)
        {
            if (ParentForm != null)
            {
                ParentForm.Invoke(new Action(() =>
                {
                    ParentForm.WindowState = FormWindowState.Normal;
                    ParentForm.Activate();

                    if (ParentTabControl != null)
                    {
                        ParentTabControl.SelectedTab = ParentTabPage;
                    }

                    MessageBox.Show(Message, Title, Buttons, Icon);
                }));

                return DialogResult.None;
            }
            else
            {
                return MessageBox.Show(Message, Title, Buttons, Icon);
            }
        }

        private static (MessageBoxIcon, string) GetIconTitle(MessageLevel Level)
        {
            return Level switch
            {
                MessageLevel.Info => (MessageBoxIcon.Information, LaunchManager.InfoMsg),
                MessageLevel.Warning => (MessageBoxIcon.Warning, LaunchManager.WarnMsg),
                MessageLevel.Error => (MessageBoxIcon.Error, LaunchManager.ErrMsg),
                _ => (MessageBoxIcon.None, "")
            };
        }
    }
}
