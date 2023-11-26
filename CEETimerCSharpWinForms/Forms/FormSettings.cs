using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Forms
{
    public partial class FormSettings : Form
    {
        public string en
        {
            get { return FormSettingsSetExamNameText.Text; }
            set { FormSettingsSetExamNameText.Text = value; }
        }
        public string xn
        {
            get { return FormSettingsSetCEETextN.Text; }
            set { FormSettingsSetCEETextN.Text = value; }
        }

        public string xy
        {
            get { return FormSettingsSetCEETextY.Text; }
            set { FormSettingsSetCEETextY.Text = value; }
        }

        public string xr
        {
            get { return FormSettingsSetCEETextR.Text; }
            set { FormSettingsSetCEETextR.Text = value; }
        }

        public string xs
        {
            get { return FormSettingsSetCEETextS.Text; }
            set { FormSettingsSetCEETextS.Text = value; }
        }

        public string xf
        {
            get { return FormSettingsSetCEETextF.Text; }
            set { FormSettingsSetCEETextF.Text = value; }
        }

        public string xm
        {
            get { return FormSettingsSetCEETextM.Text; }
            set { FormSettingsSetCEETextM.Text = value; }
        }

        public string xne
        {
            get { return FormSettingsSetEndTextN.Text; }
            set { FormSettingsSetEndTextN.Text = value; }
        }

        public string xye
        {
            get { return FormSettingsSetEndTextY.Text; }
            set { FormSettingsSetEndTextY.Text = value; }
        }

        public string xre
        {
            get { return FormSettingsSetEndTextR.Text; }
            set { FormSettingsSetEndTextR.Text = value; }
        }

        public string xse
        {
            get { return FormSettingsSetEndTextS.Text; }
            set { FormSettingsSetEndTextS.Text = value; }
        }

        public string xfe
        {
            get { return FormSettingsSetEndTextF.Text; }
            set { FormSettingsSetEndTextF.Text = value; }
        }

        public string xme
        {
            get { return FormSettingsSetEndTextM.Text; }
            set { FormSettingsSetEndTextM.Text = value; }
        }

        public int n, y, r, s, f, m;
        public int ne, ye, re, se, fe, me;
        public string examName = "高考";


        public DateTime TargetDateTime
        {
            get
            {
                return new DateTime(n, y, r, s, f, m);
            }
        }
        public DateTime TargetDateTimeEnd
        {
            get
            {
                return new DateTime(ne, ye, re, se, fe, me);
            }
        }
        public string ExamName
        {
            get
            {
                return examName;
            }
        }
        public FormSettings(CEETimerCSharpWinForms mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;

            n = 2024;
            y = 6;
            r = 7;
            s = 9;
            f = 0;
            m = 0;

            FormSettingsSetCEETextN.Text = n.ToString();
            FormSettingsSetCEETextY.Text = y.ToString();
            FormSettingsSetCEETextR.Text = r.ToString();
            FormSettingsSetCEETextS.Text = s.ToString();
            FormSettingsSetCEETextF.Text = f.ToString();
            FormSettingsSetCEETextM.Text = m.ToString();

            ne = 2024;
            ye = 6;
            re = 8;
            se = 17;
            fe = 0;
            me = 0;

            FormSettingsSetEndTextN.Text = ne.ToString();
            FormSettingsSetEndTextY.Text = ne.ToString();
            FormSettingsSetEndTextR.Text = ne.ToString();
            FormSettingsSetEndTextS.Text = ne.ToString();
            FormSettingsSetEndTextF.Text = ne.ToString();
            FormSettingsSetEndTextM.Text = ne.ToString();

            FormSettingsSetExamNameText.Text = examName;
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            mainForm.ReadConfig();
            CheckStartupSetting();
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
        public bool StartupEnabled
        {
            get { return FormSettingsSetStartupCheck.Checked; }
            set { FormSettingsSetStartupCheck.Checked = value; }
        }
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

        public Label LabelCountdown { get; set; }

        private Thread startSyncTime;
        private void FormSettingsSyncTimeButton_Click(object sender, EventArgs e)
        {
            FormSettingsSyncTimeButton.Enabled = false;
            FormSettingsSyncTimeButton.Text = "正在同步中，请稍候...";
            startSyncTime = new Thread(StartSyncTime);
            startSyncTime.Start();
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

            MessageBox.Show("命令执行完成，当前系统时间已与网络同步", "时间同步成功 - 高考倒计时", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (FormSettingsSyncTimeButton.InvokeRequired)
            {
                FormSettingsSyncTimeButton.Invoke(new Action(() => FormSettingsSyncTimeButton.Enabled = true));
                FormSettingsSyncTimeButton.Invoke(new Action(() => FormSettingsSyncTimeButton.Text = "立即同步"));
            }

        }
        private CEETimerCSharpWinForms mainForm;
        public CEETimerCSharpWinForms MainForm
        {
            get
            {
                return mainForm;
            }
            set
            {
                mainForm = value;
            }
        }

        private void FormSettingsApply_Click(object sender, EventArgs e)
        {
            if (!ValidateInput() || !ValidateEndDate())
            {
                MessageBox.Show("输入的日期时间格式有误！", "错误 - 高考倒计时", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            n = int.Parse(FormSettingsSetCEETextN.Text);
            y = int.Parse(FormSettingsSetCEETextY.Text);
            r = int.Parse(FormSettingsSetCEETextR.Text);
            s = int.Parse(FormSettingsSetCEETextS.Text);
            f = int.Parse(FormSettingsSetCEETextF.Text);
            m = int.Parse(FormSettingsSetCEETextM.Text);

            ne = int.Parse(FormSettingsSetEndTextN.Text);
            ye = int.Parse(FormSettingsSetEndTextY.Text);
            re = int.Parse(FormSettingsSetEndTextR.Text);
            se = int.Parse(FormSettingsSetEndTextS.Text);
            fe = int.Parse(FormSettingsSetEndTextF.Text);
            me = int.Parse(FormSettingsSetEndTextM.Text);

            DateTime _startTime = new DateTime(n, y, r, s, f, m);
            DateTime _endTime = new DateTime(ne, ye, re, se, fe, me);

            if (_startTime >= _endTime)
            {
                MessageBox.Show("考试开始时间必须在结束时间之前！", "错误 - 高考倒计时", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            TimeSpan _timeDistance = _endTime - _startTime;
            int _dayDistance = _timeDistance.Days;

            if (_dayDistance > 4)
            {
                DialogResult _result = MessageBox.Show("你可能输入了错误的考试时间，应该没有持续超过4天的考试吧？如果你确定当前输入的是正确的考试时间，请点击确定；若不是，请点击取消。", "警告 - 高考倒计时", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (_result == DialogResult.Cancel)
                {
                    return;
                }
            }

            examName = FormSettingsSetExamNameText.Text;

            mainForm.WriteConfig();
            DialogResult = DialogResult.OK;
        }

        private bool ValidateInput()
        {
            if (!int.TryParse(FormSettingsSetCEETextN.Text, out int n) || n <= 1 || n >= 3999)
            {
                return false;
            }

            if (!int.TryParse(FormSettingsSetCEETextY.Text, out int y) || y < 1 || y > 12)
            {
                return false;
            }

            if (!int.TryParse(FormSettingsSetCEETextR.Text, out int r) || r < 1 || r > 31)
            {
                return false;
            }

            if (!int.TryParse(FormSettingsSetCEETextS.Text, out int s) || s < 0 || s > 24)
            {
                return false;
            }

            if (!int.TryParse(FormSettingsSetCEETextF.Text, out int f) || f < 0 || f > 60)
            {
                return false;
            }

            if (!int.TryParse(FormSettingsSetCEETextM.Text, out int m) || m < 0 || m > 60)
            {
                return false;
            }

            if (y == 2 && (r > 29 || (r == 29 && !DateTime.IsLeapYear(n))))
            {
                return false;
            }
            if ((y == 4 || y == 6 || y == 9 || y == 11) && r > 30)
            {
                return false;
            }

            return true;
        }

        private bool ValidateEndDate()
        {
            if (!int.TryParse(FormSettingsSetEndTextN.Text, out int ne) || ne <= 1 || ne >= 3999)
            {
                return false;
            }

            if (!int.TryParse(FormSettingsSetEndTextY.Text, out int ye) || ye < 1 || ye > 12)
            {
                return false;
            }

            if (!int.TryParse(FormSettingsSetEndTextR.Text, out int re) || re < 1 || re > 31)
            {
                return false;
            }

            if (!int.TryParse(FormSettingsSetEndTextS.Text, out int se) || se < 0 || se > 24)
            {
                return false;
            }

            if (!int.TryParse(FormSettingsSetEndTextF.Text, out int fe) || fe < 0 || fe > 60)
            {
                return false;
            }

            if (!int.TryParse(FormSettingsSetEndTextM.Text, out int me) || me < 0 || me > 60)
            {
                return false;
            }

            if (ye == 2 && (re > 29 || (re == 29 && !DateTime.IsLeapYear(n))))
            {
                return false;
            }
            if ((ye == 4 || ye == 6 || ye == 9 || ye == 11) && re > 30)
            {
                return false;
            }

            return true;
        }

        private void FormSettingsCloseMain_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (startSyncTime != null && startSyncTime.IsAlive)
            {
                e.Cancel = true;
                MessageBox.Show("请等待网络时钟同步完毕，然后再关闭此窗口。", "错误：无法关闭窗口 - 高考倒计时", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
