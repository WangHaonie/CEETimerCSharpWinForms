using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Forms
{
    public partial class FormSettings : Form
    {
        private void StartSyncTime()
        {
            ProcessStartInfo process1Info = new ProcessStartInfo();
            process1Info.FileName = @"cmd.exe";
            process1Info.Arguments = "/c w32tm /config /manualpeerlist:ntp1.aliyun.com /syncfromflags:manual /reliable:YES /update & net stop w32time & net start w32time & sc config w32time start= auto & w32tm /resync & w32tm /resync";
            process1Info.Verb = "runas";
            process1Info.CreateNoWindow = true;
            process1Info.WindowStyle = ProcessWindowStyle.Hidden;
            Process process1 = Process.Start(process1Info);
            process1.WaitForExit();
            MessageBox.Show("命令执行完成，当前系统时间已与网络同步", "时间同步成功 - 高考倒计时", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (FormSettingsSyncTimeButton.InvokeRequired)
            {
                FormSettingsSyncTimeButton.Invoke(new Action(() => FormSettingsSyncTimeButton.Enabled = true));
                FormSettingsSyncTimeButton.Invoke(new Action(() => FormSettingsSyncTimeButton.Text = "立即同步"));
            }
        }
    }
}
