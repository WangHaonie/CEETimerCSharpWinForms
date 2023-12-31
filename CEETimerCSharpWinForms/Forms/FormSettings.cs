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
        private bool isSyncingTime = false;
        public delegate void ConfigChangedHandler(object sender, EventArgs e);
        public ConfigChangedHandler ConfigChanged;
        public FormSettings()
        {
            InitializeComponent();
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
            // 趁热打铁，用一下刚学的条件运算符简化 if...else...，真香
        }
        private void FormSettingsSetStartupCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (FormSettingsSetStartupCheck.Checked)
            {
                UpdateStartupSetting(true);
            }
            else
            {
                UpdateStartupSetting(false);
            }
        }
        private void BtnRestart_Click(object sender, EventArgs e)
        {
            RestartMyself.RestartNow();
        }
        private void BtnRestartFunny_Click(object sender, EventArgs e)
        {
            RestartMyself.KillMeNow();
        }
        private void BtnRestart_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                BtnRestart.Click -= BtnRestart_Click;
                BtnRestart.Click += BtnRestartFunny_Click;
                groupBox3.Text = "关闭倒计时";
                LblRestart.Text = "如果你由于某些原因需要关闭这个倒计时，那你现在就可以选择";
                label1.Text = "关闭它了。";
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
            LblRestart.Text = "如果你更改了屏幕分辨率或者缩放，可以点击此按钮来重启倒计";
            label1.Text = "时以确保显示的文字不会变模糊。";
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

            if (timeSpan.Days > 4)
            {
                DialogResult _result = MessageBox.Show($"{hint1}持续超过4天的考试吧？\n\n{hint2}持续{timeSpan.Days}天{hint3}", $"{LaunchManager.WarnMsg}", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (_result == DialogResult.Cancel)
                {
                    return false;
                }
            }
            else if (timeSpan.Minutes < 40 && timeSpan.Seconds > 60)
            {
                DialogResult _result = MessageBox.Show($"{hint1}哪个考试连40分钟都没有吧？\n\n呃{hint2}只有短短{timeSpan.Minutes}分钟{hint3}", $"{LaunchManager.WarnMsg}", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (_result == DialogResult.Cancel)
                {
                    return false;
                }
            }
            else if (timeSpan.Seconds < 60)
            {
                DialogResult _result = MessageBox.Show($"{hint1}哪个考试连40分钟都没有吧？\n\n呃{hint2}只有短短{timeSpan.Seconds}秒{hint3}", $"{LaunchManager.WarnMsg}", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (_result == DialogResult.Cancel)
                {
                    return false;
                }
            }
            return true;
        }
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
            MessageBox.Show("命令执行完成，当前系统时间已与网络同步", $"{LaunchManager.InfoMsg}", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void CheckStartupSetting()
        {
            RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            string regvalue = reg.GetValue("CEETimerCSharpWinForms") as string;
            if (regvalue != null)
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
        private void RefreshSettings()
        {
            try
            {
                ExamName = ConfigManager.ReadConfig("ExamName");
                DateTime.TryParseExact(ConfigManager.ReadConfig("ExamStartTime"), "yyyyMMddHHmmss", null, System.Globalization.DateTimeStyles.None, out ExamStartTime);
                DateTime.TryParseExact(ConfigManager.ReadConfig("ExamEndTime"), "yyyyMMddHHmmss", null, System.Globalization.DateTimeStyles.None, out ExamEndTime);
                //MessageBox.Show($"{ExamName}，{ExamStartTime}，{ExamEndTime}");
                if (!ConfigManager.IsValidData(ExamName))
                {
                    FormSettingsSetExamNameText.Text = "";
                }
                else
                {
                    FormSettingsSetExamNameText.Text = ExamName;
                }
                if (!ConfigManager.IsValidData(ExamStartTime))
                {
                    StartTimePicker.Value = DateTime.Now;
                }
                else
                {
                    StartTimePicker.Value = ExamStartTime;
                }
                if (!ConfigManager.IsValidData(ExamEndTime))
                {
                    EndTimePicker.Value = DateTime.Now;
                }
                else
                {
                    EndTimePicker.Value = ExamEndTime;
                }
            }
            catch (Exception) { }
        }
        private void SaveSettings()
        {
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
