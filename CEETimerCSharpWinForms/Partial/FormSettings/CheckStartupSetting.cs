using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Forms
{
    public partial class FormSettings : Form
    {
        private void CheckStartupSetting()
        {
            RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            string regvalue = reg.GetValue("CEETimerCSharpWinForms") as string;

            //if (reg.GetValue("CEETimerCSharpWinForms") != null)
            if (regvalue != null)
            {
                //RegistryValueKind regvaluekind = reg.GetValueKind("CEETimerCSharpWinForms");
                //if (regvalue.Equals(Application.ExecutablePath, StringComparison.OrdinalIgnoreCase) || (regvaluekind != RegistryValueKind.None))

                if (regvalue.Equals(Application.ExecutablePath, StringComparison.OrdinalIgnoreCase))
                {
                    StartupEnabled = true;
                }
                else
                {
                    StartupEnabled = false;
                }
            }
            else
            {
                StartupEnabled = false;
            }
        }
        private void UpdateStartupSetting(bool enableStartup)
        {
            RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (enableStartup)
            {
                reg.SetValue("CEETimerCSharpWinForms", Application.ExecutablePath);
            }
            else
            {
                reg.DeleteValue("CEETimerCSharpWinForms", false);
            }
        }
    }
}
