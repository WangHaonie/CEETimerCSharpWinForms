using CEETimerCSharpWinForms.Modules;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Forms
{
    public partial class FormSettings : Form
    {
        private string ExamName;
        private DateTime ExamStartTime = new DateTime();
        private DateTime ExamEndTime = new DateTime();
        private string isTopMost;
        private bool isTopMostBool;
        private bool isSyncingTime = false;
        public delegate void ConfigChangedHandler(object sender, EventArgs e);
        public ConfigChangedHandler ConfigChanged;
        public FormSettings()
        {
            InitializeComponent();
            /*LaunchManager.UseImmersiveDarkMode(Handle, true);
            LaunchManager.SetDarkControls(this);*/
        }
        private void FormSettings_Load(object sender, EventArgs e)
        {
            CheckStartupSetting();
            RefreshSettings();
        }
        private void FormSettingsSetExamNameText_TextChanged(object sender, EventArgs e)
        {
            int CharCount = FormSettingsSetExamNameText.Text.Trim().Replace(" ", "").Where(c => char.IsLetterOrDigit(c) || (c >= ' ' && c <= byte.MaxValue)).ToArray().Length;
            LblCounter.Text = $"{CharCount}/15";
            LblCounter.ForeColor = CharCount > 15 ? System.Drawing.Color.Red : System.Drawing.Color.Black;
        }
        private void BtnRestart_Click(object sender, EventArgs e)
        {
            LaunchManager.RestartNow();
        }
        private void BtnRestartFunny_Click(object sender, EventArgs e)
        {
            if (isSyncingTime)
            {
                MessageBox.Show("请等待网络时钟同步完毕，然后再关闭此窗口。", $"{LaunchManager.ErrMsg}", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                LaunchManager.KillMeNow();
            }
        }
        private void BtnRestart_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                BtnRestart.Click -= BtnRestart_Click;
                BtnRestart.Click += BtnRestartFunny_Click;
                groupBox3.Text = "关闭倒计时";
                label7.Text = "如果你由于某些原因需要临时关闭这个倒计时, 那你现在就可以选择";
                label8.Text = "关闭它了。";
                BtnRestart.Text = "点击关闭(&C)";
            }
        }
        private async void FormSettingsSyncTimeButton_Click(object sender, EventArgs e)
        {
            isSyncingTime = true;
            FormSettingsSyncTimeButton.Enabled = false;
            FormSettingsSyncTimeButton.Text = "正在同步中，请稍候...";
            await Task.Run(StartSyncTime);
            isSyncingTime = false;
            FormSettingsSyncTimeButton.Enabled = true;
            FormSettingsSyncTimeButton.Text = "立即同步(&S)";
        }
        private void FormSettingsApply_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                SaveSettings();
                OnConfigChanged();
                Close();
            }
        }
        private void FormSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isSyncingTime)
            {
                e.Cancel = true;
                MessageBox.Show("请等待网络时钟同步完毕，然后再关闭此窗口。", $"{LaunchManager.ErrMsg}", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FormSettings_FormClosed(object sender, FormClosedEventArgs e)
        {
            groupBox3.Text = "重启倒计时";
            label7.Text = "如果你更改了屏幕缩放或者分辨率, 可以点击此按钮来重启倒计时以";
            label8.Text = "确保显示的文字不会变模糊。";
            BtnRestart.Text = "点击重启(&R)";
            BtnRestart.Click -= BtnRestartFunny_Click;
            BtnRestart.Click += BtnRestart_Click;
        }
        private void FormSettingsCloseMain_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void FormSettings_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
        public bool ValidateInput()
        {
            #region 来自网络
            /*
            
            移除 ExamName 里不可见的空格 (Unicode 控制字符) 参考：

            c# - Removing hidden characters from within strings - Stack Overflow
            https://stackoverflow.com/a/40888424/21094697

            */
            ExamName = new string(FormSettingsSetExamNameText.Text.Trim().Replace(" ", "").Where(c => char.IsLetterOrDigit(c) || (c >= ' ' && c <= byte.MaxValue)).ToArray());
            #endregion
            if (string.IsNullOrWhiteSpace(ExamName) || (ExamName.Length < 2) || (ExamName.Length > 15))
            {
                MessageBox.Show("输入的考试名称有误！\n\n请检查输入的考试名称是否太短 (<2) 或太长 (>15)！", $"{LaunchManager.ErrMsg}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (StartTimePicker.Value >= EndTimePicker.Value)
            {
                MessageBox.Show("考试开始时间必须在结束时间之前！", $"{LaunchManager.ErrMsg}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            TimeSpan timeSpan = EndTimePicker.Value - StartTimePicker.Value;

            string hint1 = "你可能输入了错误的考试时间，应该没有";
            string hint2 = "你刚刚设置了一个";
            string hint3 = "的考试。\n\n如果你确定当前设置的是正确的考试时间，请点击确定；否则请点击取消。";

            if (timeSpan.TotalDays > 4)
            {
                DialogResult _result = MessageBox.Show($"{hint1}持续超过4天的考试吧？\n\n{hint2}持续{timeSpan.TotalDays}天{hint3}", $"{LaunchManager.WarnMsg}", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (_result == DialogResult.Cancel)
                {
                    return false;
                }
            }
            else if (timeSpan.TotalMinutes < 40 && timeSpan.TotalSeconds > 60)
            {
                DialogResult _result = MessageBox.Show($"{hint1}哪个考试连40分钟都没有吧？\n\n{hint2}只有短短{timeSpan.TotalMinutes}分钟{hint3}", $"{LaunchManager.WarnMsg}", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (_result == DialogResult.Cancel)
                {
                    return false;
                }
            }
            else if (timeSpan.TotalSeconds < 60)
            {
                DialogResult _result = MessageBox.Show($"{hint1}哪个考试连40分钟都没有吧？\n\n{hint2}只有短短{timeSpan.TotalSeconds}秒{hint3}", $"{LaunchManager.WarnMsg}", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (_result == DialogResult.Cancel)
                {
                    return false;
                }
            }
            return true;
        }
        private void StartSyncTime()
        {
            ProcessStartInfo process1Info = new()
            {
                FileName = @"cmd.exe",
                Arguments = "/c w32tm /config /manualpeerlist:ntp1.aliyun.com /syncfromflags:manual /reliable:YES /update & net stop w32time & net start w32time & sc config w32time start= auto & w32tm /resync & w32tm /resync",
                Verb = "runas",
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };
            Process syncTimeProcess = Process.Start(process1Info);
            syncTimeProcess.WaitForExit();
            MessageBox.Show("命令执行完成，当前系统时间已与网络同步", $"{LaunchManager.InfoMsg}", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void CheckStartupSetting()
        {
            RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (reg.GetValue("CEETimerCSharpWinForms") is string regvalue)
            {
                if (regvalue.Equals(Application.ExecutablePath, StringComparison.OrdinalIgnoreCase))
                {
                    FormSettingsSetStartupCheck.Checked = true;
                }
                else
                {
                    FormSettingsSetStartupCheck.Checked = false;
                }
            }
            else
            {
                FormSettingsSetStartupCheck.Checked = false;
            }
        }
        private void RefreshSettings()
        {
            try
            {
                ExamName = ConfigManager.ReadConfig("ExamName");
                isTopMost = ConfigManager.ReadConfig("TopMost");
                if (isTopMost.Equals("true", StringComparison.OrdinalIgnoreCase) || isTopMost.Equals("false", StringComparison.OrdinalIgnoreCase))
                {
                    bool.TryParse(isTopMost, out isTopMostBool);
                    chkSetTopMost.Checked = isTopMostBool;
                }
                else
                {
                    chkSetTopMost.Checked = true;
                }
                DateTime.TryParseExact(ConfigManager.ReadConfig("ExamStartTime"), "yyyyMMddHHmmss", null, System.Globalization.DateTimeStyles.None, out ExamStartTime);
                DateTime.TryParseExact(ConfigManager.ReadConfig("ExamEndTime"), "yyyyMMddHHmmss", null, System.Globalization.DateTimeStyles.None, out ExamEndTime);
                //MessageBox.Show($"{ExamName}，{ExamStartTime}，{ExamEndTime}");
                FormSettingsSetExamNameText.Text = ConfigManager.IsValidData(ExamName) ? ExamName : "";
                StartTimePicker.Value = ConfigManager.IsValidData(ExamStartTime) ? ExamStartTime : DateTime.Now;
                EndTimePicker.Value = ConfigManager.IsValidData(ExamEndTime) ? ExamEndTime : DateTime.Now;
            }
            catch (Exception) { }
        }
        private void SaveSettings()
        {
            RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (FormSettingsSetStartupCheck.Checked)
            {
                reg.SetValue("CEETimerCSharpWinForms", LaunchManager.CurrentExecutable);
            }
            else
            {
                reg.DeleteValue("CEETimerCSharpWinForms", false);
            }
            ConfigManager.WriteConfig("TopMost", chkSetTopMost.Checked.ToString());
            ConfigManager.WriteConfig("ExamName", ExamName);
            ConfigManager.WriteConfig("ExamStartTime", StartTimePicker.Value.ToString("yyyyMMddHHmmss"));
            ConfigManager.WriteConfig("ExamEndTime", EndTimePicker.Value.ToString("yyyyMMddHHmmss"));
        }
        protected virtual void OnConfigChanged()
        {
            ConfigChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
