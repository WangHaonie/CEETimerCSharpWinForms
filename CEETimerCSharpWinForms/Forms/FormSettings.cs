using CEETimerCSharpWinForms.Modules;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Forms
{
    public partial class FormSettings : Form
    {
        private string ExamName;
        private DateTime ExamStartTime = new();
        private DateTime ExamEndTime = new();
        private string isTopMost;
        private bool isTopMostBool;
        private bool isSyncingTime = false;
        private bool isChanged;
        private const string groupBoxTitleOriginal = "重启倒计时";
        private const string label7TextOriginal = "如果你更改了屏幕缩放或者分辨率, 可以点击此按钮来重启倒计时以";
        private const string label8TextOriginal = "确保显示的文字不会变模糊。";
        private const string btnTextOriginal = "点击重启(&R)";
        private const string groupBoxTitle = "关闭倒计时";
        private const string label7Text = "如果你由于某些原因需要临时关闭这个倒计时, 那你现在就可以选择";
        private const string label8Text = "关闭它了。";
        private const string btnText = "点击关闭(&C)";
        public delegate void ConfigChangedHandler(object sender, EventArgs e);
        public ConfigChangedHandler ConfigChanged;

        public FormSettings()
        {
            InitializeComponent();
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            RestoreFunny();
            CheckStartupSetting();
            RefreshSettings();
            isChanged = false;
            FormSettingsApply.Enabled = false;
        }

        private void SettingsChanged(object sender, EventArgs e)
        {
            isChanged = true;
            FormSettingsApply.Enabled = true;
        }

        private void FormSettingsSetExamNameText_TextChanged(object sender, EventArgs e)
        {
            SettingsChanged(sender, e);
            int CharCount = FormSettingsSetExamNameText.Text.RemoveAllBadChars().Length;
            LblCounter.Text = $"{CharCount}/15";
            LblCounter.ForeColor = CharCount > 15 ? System.Drawing.Color.Red : System.Drawing.Color.Black;
        }

        private void BtnRestart_Click(object sender, EventArgs e)
        {
            LaunchManager.Restart();
        }

        private void BtnRestartFunny_Click(object sender, EventArgs e)
        {
            LaunchManager.Shutdown();
        }

        private void BtnRestart_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                BtnRestart.Click -= BtnRestart_Click;
                BtnRestart.Click += BtnRestartFunny_Click;
                groupBox3.Text = groupBoxTitle;
                label7.Text = label7Text;
                label8.Text = label8Text;
                BtnRestart.Text = btnText;
            }
        }

        private async void FormSettingsSyncTimeButton_Click(object sender, EventArgs e)
        {
            isSyncingTime = true;
            FormSettingsSyncTimeButton.Enabled = false;
            BtnRestart.Enabled = false;
            FormSettingsSyncTimeButton.Text = "正在同步中，请稍候...";
            await Task.Run(StartSyncTime);
            isSyncingTime = false;
            FormSettingsSyncTimeButton.Enabled = true;
            BtnRestart.Enabled = true;
            FormSettingsSyncTimeButton.Text = "立即同步(&S)";
        }

        private void FormSettingsApply_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                isChanged = false;
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
            }
            else if (isChanged)
            {
                DialogResult result = MessageBox.Show("检测到设置被更改但没有被保存，是否立即进行保存？", LaunchManager.WarnMsg, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    FormSettingsApply_Click(sender, e);
                }
                else
                {
                    isChanged = false;
                    Close();
                }
            }
        }

        private void FormSettings_FormClosed(object sender, FormClosedEventArgs e)
        {
            RestoreFunny();
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
            ExamName = FormSettingsSetExamNameText.Text.RemoveAllBadChars();

            if (string.IsNullOrWhiteSpace(ExamName) || (ExamName.Length < 2) || (ExamName.Length > 15))
            {
                MessageBox.Show("输入的考试名称有误！\n\n请检查输入的考试名称是否太长或太短！", LaunchManager.ErrMsg, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (StartTimePicker.Value >= EndTimePicker.Value)
            {
                MessageBox.Show("考试开始时间必须在结束时间之前！", LaunchManager.ErrMsg, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            TimeSpan timeSpan = EndTimePicker.Value - StartTimePicker.Value;
            string UniMsg = "";
            string TimeMsg = "";

            if (timeSpan.TotalDays > 4)
            {
                TimeMsg = $"{timeSpan.TotalDays:0.0} 天";
            }
            else if (timeSpan.TotalMinutes < 40 && timeSpan.TotalSeconds > 60)
            {
                TimeMsg = $"{timeSpan.TotalMinutes:0.0} 分钟";
            }
            else if (timeSpan.TotalSeconds < 60)
            {
                TimeMsg = $"{timeSpan.TotalSeconds:0.0} 秒";
            }

            if (!string.IsNullOrEmpty(TimeMsg))
            {
                UniMsg = $"检测到设置的考试时长太长或太短！\n\n当前考试时长: {TimeMsg}。\n\n如果你确定当前设置的是正确的考试时间，请点击确定，否则请点击取消。";
            }

            if (!string.IsNullOrEmpty(UniMsg))
            {
                DialogResult result = MessageBox.Show(UniMsg, LaunchManager.WarnMsg, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Cancel)
                {
                    return false;
                }
            }

            return true;
        }

        private void StartSyncTime()
        {
            ProcessStartInfo processStartInfo = new()
            {
                FileName = @"cmd.exe",
                Arguments = "/c w32tm /config /manualpeerlist:ntp1.aliyun.com /syncfromflags:manual /reliable:YES /update && net stop w32time && net start w32time && sc config w32time start= auto && w32tm /resync && w32tm /resync",
                Verb = "runas",
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            Process syncTimeProcess = Process.Start(processStartInfo);
            syncTimeProcess.WaitForExit();
            MessageBox.Show($"命令执行完成！\n\n返回值为 {syncTimeProcess.ExitCode}\n(0 代表成功，其他值为失败)", LaunchManager.InfoMsg, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                FormSettingsSetExamNameText.Text = ConfigManager.IsValidData(ExamName) ? ExamName : "";
                StartTimePicker.Value = ConfigManager.IsValidData(ExamStartTime) ? ExamStartTime : DateTime.Now;
                EndTimePicker.Value = ConfigManager.IsValidData(ExamEndTime) ? ExamEndTime : DateTime.Now;
            }
            catch
            {

            }
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

        private void RestoreFunny()
        {
            groupBox3.Text = groupBoxTitleOriginal;
            label7.Text = label7TextOriginal;
            label8.Text = label8TextOriginal;
            BtnRestart.Text = btnTextOriginal;
        }

        protected virtual void OnConfigChanged()
        {
            ConfigChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
