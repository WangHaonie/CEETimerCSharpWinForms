using CEETimerCSharpWinForms.Forms;
using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Modules
{
    public enum MessageLevel
    {
        Info, Warning, Error
    }

    public enum MessageBoxExButtons
    {
        OK, YesNo
    }

    public static class MessageX
    {
        /// <summary>
        /// 在指定的窗体或定位到其标签页上或直接显示一个具有 DialogResult 返回值的消息框
        /// </summary>
        /// <param name="Message">消息</param>
        /// <param name="Level">消息等级</param>
        /// <param name="ParentForm">[可选] 父窗体</param>
        /// <param name="ParentTabControl">[可选] 父窗体的 TabControl</param>
        /// <param name="ParentTabPage">[可选] 父窗体的 TabControl 的标签页</param>
        /// <param name="Buttons">[可选] 要显示的按钮</param>
        /// <param name="Position">[可选] 消息框出现的位置</param>
        /// <param name="AutoClose">[可选] 是否允许消息框在3s后自动关闭</param>
        /// <returns>DialogResult</returns>
        public static DialogResult Popup(string Message, MessageLevel Level, Form ParentForm = null, TabControl ParentTabControl = null, TabPage ParentTabPage = null, MessageBoxExButtons Buttons = MessageBoxExButtons.OK, FormStartPosition Position = FormStartPosition.CenterParent, bool AutoClose = false)
            => PopupCore(Message, Level, ParentForm, ParentTabControl, ParentTabPage, Buttons, Position, AutoClose);

        /// <summary>
        /// 在指定的窗体或定位到其标签页上或直接显示一个专门用来显示错误的消息框
        /// </summary>
        /// <param name="Message">消息</param>
        /// <param name="ex">异常</param>
        /// <param name="ParentForm">[可选] 父窗体</param>
        /// <param name="ParentTabControl">[可选] 父窗体的 TabControl</param>
        /// <param name="ParentTabPage">[可选] 父窗体的 TabControl 的标签页</param>
        /// <param name="Buttons">[可选] 要显示的按钮</param>
        /// <param name="Position">[可选] 消息框出现的位置</param>
        /// <param name="AutoClose">[可选] 是否允许消息框在3s后自动关闭</param>
        public static DialogResult Popup(string Message, Exception ex, Form ParentForm = null, TabControl ParentTabControl = null, TabPage ParentTabPage = null, MessageBoxExButtons Buttons = MessageBoxExButtons.OK, FormStartPosition Position = FormStartPosition.CenterParent, bool AutoClose = false)
            => PopupCore($"{Message}\n\n错误信息: \n{ex.Message}\n\n错误详情: \n{ex}", MessageLevel.Error, ParentForm, ParentTabControl, ParentTabPage, Buttons, Position, AutoClose);

        /// <summary>
        /// Popup 的核心方法，用于实现定位到父窗体的标签页和与 MessageBoxEx 交互
        /// </summary>
        /// <param name="Message">消息</param>
        /// <param name="Level">消息等级</param>
        /// <param name="ParentForm">父窗体</param>
        /// <param name="ParentTabControl">父窗体的 TabControl</param>
        /// <param name="ParentTabPage">父窗体的 TabControl 的标签页</param>
        /// <param name="Buttons">要显示的按钮</param>
        /// <param name="Position">消息框出现的位置</param>
        /// <param name="AutoClose">是否允许消息框在3s后自动关闭</param>
        /// <returns>DialogResult</returns>
        private static DialogResult PopupCore(string Message, MessageLevel Level, Form ParentForm, TabControl ParentTabControl, TabPage ParentTabPage, MessageBoxExButtons Buttons, FormStartPosition Position, bool AutoClose)
        {
            var (Title, MessageBoxExIcon, Sound) = GetStuff(Level);
            using var _MessageBoxEx = new MessageBoxEx();

            if (ParentForm != null)
            {
                if (ParentForm.InvokeRequired)
                {
                    #region 来自网络
                    /*

                    在 Invoke 方法内部获取到 DialogResult 返回值 参考：

                    c# - Return Ivoke message DialogResult - Stack Overflow
                    https://stackoverflow.com/a/29256646/21094697

                    */
                    return (DialogResult)ParentForm.Invoke(new Func<DialogResult>(() =>
                    {
                        ParentForm.WindowState = FormWindowState.Normal;
                        ParentForm.Activate();

                        if (ParentTabControl != null)
                        {
                            ParentTabControl.SelectedTab = ParentTabPage;
                        }

                        return _MessageBoxEx.ShowCore(ParentForm, Message, Title, MessageBoxExIcon, Sound, Buttons, Position, AutoClose);
                    }));
                    #endregion
                }
                else
                {
                    ParentForm.WindowState = FormWindowState.Normal;
                    ParentForm.Activate();

                    if (ParentTabControl != null)
                    {
                        ParentTabControl.SelectedTab = ParentTabPage;
                    }

                    return _MessageBoxEx.ShowCore(ParentForm, Message, Title, MessageBoxExIcon, Sound, Buttons, Position, AutoClose);
                }
            }
            else
            {
                return _MessageBoxEx.ShowCore(null, Message, Title, MessageBoxExIcon, Sound, Buttons, Position, AutoClose);
            }
        }

        public static (string, Icon, SystemSound) GetStuff(MessageLevel Level) => Level switch
        {
            #region 来自网络
            /*

            获取 imageres.dll 里的 Info、Warning、Error 图标的索引参考：

            Icons in imageres.dll
            https://renenyffenegger.ch/development/Windows/PowerShell/examples/WinAPI/ExtractIconEx/imageres.html


            播放与 MessageBox 同款音效参考：

            c# - Selecting sounds from Windows and playing them - Stack Overflow
            https://stackoverflow.com/a/5194223/21094697

            */

            MessageLevel.Info => (LaunchManager.InfoMsg, GetMessageBoxIcon(76), SystemSounds.Asterisk),
            MessageLevel.Warning => (LaunchManager.WarnMsg, GetMessageBoxIcon(79), SystemSounds.Exclamation),
            MessageLevel.Error => (LaunchManager.ErrMsg, GetMessageBoxIcon(93), SystemSounds.Hand),
            _ => throw new Exception()

            #endregion
        };

        private static Icon GetMessageBoxIcon(int IconIndex)
        {
            WindowsAPI.ExtractIconEx("imageres.dll", IconIndex, out IntPtr LargeIcon, out _, 1);
            return Icon.FromHandle(LargeIcon);
        }
    }
}
