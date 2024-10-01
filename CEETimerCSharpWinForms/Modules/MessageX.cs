using CEETimerCSharpWinForms.Dialogs;
using CEETimerCSharpWinForms.Interop;
using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Modules
{
    public static class MessageX
    {
        private static readonly Icon InfoIcon;
        private static readonly Icon WarningIcon;
        private static readonly Icon ErrorIcon;

        static MessageX()
        {
            InfoIcon = GetIcon(76);
            WarningIcon = GetIcon(79);
            ErrorIcon = GetIcon(93);
        }

        /// <summary>
        /// 在指定的窗体或定位到其标签页上或直接显示一个具有 DialogResult 返回值的消息框
        /// </summary>
        /// <param name="Message">消息</param>
        /// <param name="Level">消息等级</param>
        /// <param name="OwnerForm">[可选] 父窗体</param>
        /// <param name="ParentTabControl">[可选] 父窗体的 TabControl</param>
        /// <param name="ParentTabPage">[可选] 父窗体的 TabControl 的标签页</param>
        /// <param name="Buttons">[可选] 要显示的按钮</param>
        /// <param name="Position">[可选] 消息框出现的位置</param>
        /// <param name="AutoClose">[可选] 是否允许消息框在3s后自动关闭</param>
        /// <returns>DialogResult</returns>
        public static DialogResult Popup(string Message, MessageLevel Level, Form OwnerForm = null, TabControl ParentTabControl = null, TabPage ParentTabPage = null, MessageBoxExButtons Buttons = MessageBoxExButtons.OK, FormStartPosition Position = FormStartPosition.CenterParent, bool AutoClose = false)
        {
            var (Title, MessageBoxExIcon, Sound) = GetStuff(Level);
            using var _MessageBoxEx = new MessageBoxEx(Sound, Buttons, AutoClose);

            if (OwnerForm != null)
            {
                if (OwnerForm.InvokeRequired)
                {
                    #region 来自网络
                    /*

                    在 Invoke 方法内部获取到 DialogResult 返回值 参考:

                    c# - Return Ivoke message DialogResult - Stack Overflow
                    https://stackoverflow.com/a/29256646/21094697

                    */
                    return (DialogResult)OwnerForm.Invoke(ShowPopup); // 等效于 Func<DialogResult>
                    #endregion
                }
                else
                {
                    return ShowPopup();
                }
            }
            else
            {
                return ShowPopup();
            }

            DialogResult ShowPopup()
            {
                OwnerForm?.ReActivate();

                if (ParentTabControl != null)
                {
                    ParentTabControl.SelectedTab = ParentTabPage;
                }

                return _MessageBoxEx.ShowCore(OwnerForm, Message, Title, MessageBoxExIcon, Position);
            }
        }

        private static (string, Icon, SystemSound) GetStuff(MessageLevel Level) => Level switch
        {
            #region 来自网络
            /*

            获取 imageres.dll 里的 Info、Warning、Error 图标的索引参考:

            Icons in imageres.dll
            https://renenyffenegger.ch/development/Windows/PowerShell/examples/WinAPI/ExtractIconEx/imageres.html


            播放与 MessageBox 同款音效参考:

            c# - Selecting sounds from Windows and playing them - Stack Overflow
            https://stackoverflow.com/a/5194223/21094697

            */

            MessageLevel.Warning => (AppLauncher.WarnMsg, WarningIcon, SystemSounds.Exclamation),
            MessageLevel.Error => (AppLauncher.ErrMsg, ErrorIcon, SystemSounds.Hand),
            _ => (AppLauncher.InfoMsg, InfoIcon, SystemSounds.Asterisk)

            #endregion
        };

        private static Icon GetIcon(int Index)
        {
            NativeInterop.ExtractIconEx("imageres.dll", Index, out IntPtr hIcon, out _, 1);
            return Icon.FromHandle(hIcon);
        }
    }
}
