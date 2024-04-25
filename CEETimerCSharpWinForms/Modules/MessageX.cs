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

    public class MessageX
    {
        public static DialogResult Popup(string Message, MessageLevel Level, Form ParentForm = null, TabControl ParentTabControl = null, TabPage ParentTabPage = null, MessageBoxExButtons Buttons = MessageBoxExButtons.OK, FormStartPosition Position = FormStartPosition.CenterParent)
        {
            return PopupCore(Message, Level, ParentForm, ParentTabControl, ParentTabPage, Buttons, Position);
        }

        public static void Popup(string Message, Exception ex, Form ParentForm = null, TabControl ParentTabControl = null, TabPage ParentTabPage = null, MessageBoxExButtons Buttons = MessageBoxExButtons.OK, FormStartPosition Position = FormStartPosition.CenterParent)
        {
            PopupCore($"{Message}\n\n错误信息: \n{ex.Message}\n\n错误详情: \n{ex}", MessageLevel.Error, ParentForm, ParentTabControl, ParentTabPage, Buttons, Position);
        }

        private static DialogResult PopupCore(string Message, MessageLevel Level, Form ParentForm, TabControl ParentTabControl, TabPage ParentTabPage, MessageBoxExButtons Buttons, FormStartPosition Position)
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

                    using var Mx = new MessageBoxEx();
                    Mx.ShowCore(Message, Level, Buttons, Position);
                    Mx.ShowDialog(ParentForm);
                }));

                return DialogResult.None;
            }
            else
            {
                using var Mx = new MessageBoxEx();
                Mx.ShowCore(Message, Level, Buttons, Position);
                Mx.ShowDialog();
                return Mx.DialogResultEx;
            }
        }

        public static (SystemSound, Icon, string) GetStuff(MessageLevel Level)
        {
            return Level switch
            {
                #region
                /*
                
                获取 imageres.dll 里的 Info、Warning、Error 图标的索引参考：

                Icons in imageres.dll
                https://renenyffenegger.ch/development/Windows/PowerShell/examples/WinAPI/ExtractIconEx/imageres.html


                播放与 MessageBox 同款音效参考：

                c# - Selecting sounds from Windows and playing them - Stack Overflow
                https://stackoverflow.com/a/5194223/21094697

                 */

                MessageLevel.Info => (SystemSounds.Asterisk, GetMessageBoxIcon(76), LaunchManager.InfoMsg),
                MessageLevel.Warning => (SystemSounds.Exclamation, GetMessageBoxIcon(79), LaunchManager.WarnMsg),
                MessageLevel.Error => (SystemSounds.Hand, GetMessageBoxIcon(93), LaunchManager.ErrMsg),
                _ => (null, null, "")

                #endregion
            };
        }

        private static Icon GetMessageBoxIcon(int IconIndex)
        {
            WindowsAPI.ExtractIconEx("imageres.dll", IconIndex, out IntPtr LargeIcon, out IntPtr _, 1);
            return Icon.FromHandle(LargeIcon);
        }
    }
}
