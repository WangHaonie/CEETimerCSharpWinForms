using CEETimerCSharpWinForms.Controls;
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
        private static readonly Bitmap InfoIcon;
        private static readonly Bitmap WarningIcon;
        private static readonly Bitmap ErrorIcon;

        static MessageX()
        {
            InfoIcon = GetIcon(76);
            WarningIcon = GetIcon(79);
            ErrorIcon = GetIcon(93);
        }

        public static DialogResult Info(string Message, TrackableForm OwnerForm = null, TabControl ParentTabControl = null, TabPage ParentTabPage = null, MessageBoxExButtons Buttons = MessageBoxExButtons.OK, FormStartPosition Position = FormStartPosition.CenterParent, bool AutoClose = false) => Popup(Message, MessageLevel.Info, OwnerForm, ParentTabControl, ParentTabPage, Buttons, Position, AutoClose);

        public static DialogResult Warn(string Message, TrackableForm OwnerForm = null, TabControl ParentTabControl = null, TabPage ParentTabPage = null, MessageBoxExButtons Buttons = MessageBoxExButtons.OK, FormStartPosition Position = FormStartPosition.CenterParent, bool AutoClose = false) => Popup(Message, MessageLevel.Warning, OwnerForm, ParentTabControl, ParentTabPage, Buttons, Position, AutoClose);

        public static DialogResult Error(string Message, TrackableForm OwnerForm = null, TabControl ParentTabControl = null, TabPage ParentTabPage = null, MessageBoxExButtons Buttons = MessageBoxExButtons.OK, FormStartPosition Position = FormStartPosition.CenterParent, bool AutoClose = false) => Popup(Message, MessageLevel.Error, OwnerForm, ParentTabControl, ParentTabPage, Buttons, Position, AutoClose);

        private static DialogResult Popup(string Message, MessageLevel Level, TrackableForm OwnerForm, TabControl ParentTabControl, TabPage ParentTabPage, MessageBoxExButtons Buttons, FormStartPosition Position, bool AutoClose)
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

        private static (string, Bitmap, SystemSound) GetStuff(MessageLevel Level) => Level switch
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

        private static Bitmap GetIcon(int Index)
        {
            NativeInterop.ExtractIconEx("imageres.dll", Index, out IntPtr hIcon, out _, 1);
            return Icon.FromHandle(hIcon).ToBitmap();
        }
    }
}
