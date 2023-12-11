using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using CEETimerCSharpWinForms.Modules;

namespace CEETimerCSharpWinForms.Forms
{
    public partial class FormSettings : Form
    {
        // 功能 更改倒计时字体大小 相关代码 private string FontID;
        // 功能 更改倒计时字体大小 相关代码 
        // 功能 更改倒计时字体大小 相关代码 public string FontId
        // 功能 更改倒计时字体大小 相关代码 {
        // 功能 更改倒计时字体大小 相关代码 get { return FontID; }
        // 功能 更改倒计时字体大小 相关代码 set { FontID = value; }
        // 功能 更改倒计时字体大小 相关代码 }

        public int n, y, r, s, f, m;
        public int ne, ye, re, se, fe, me;
        public string examName = "";
        private ToolTip BtnRestartTip;
        private Thread startSyncTime;
        private CEETimerCSharpWinForms mainForm;

        public FormSettings()
        {
            InitializeComponent();
            BtnRestartTip = new ToolTip();
            BtnRestartTip.AutoPopDelay = 30000;
            BtnRestartTip.SetToolTip(BtnRestart, "如果你更改了屏幕分辨率或者缩放，可以点击此按钮来重启倒计时以确保显示的文字不会变模糊");
            BtnRestart.MouseLeave += BtnRestart_MouseLeave;
            //n = 2024;
            //y = 6;
            //r = 7;
            //s = 9;
            //f = 0;
            //m = 0;
            FormSettingsSetCEETextN.Text = n.ToString();
            FormSettingsSetCEETextY.Text = y.ToString();
            FormSettingsSetCEETextR.Text = r.ToString();
            FormSettingsSetCEETextS.Text = s.ToString();
            FormSettingsSetCEETextF.Text = f.ToString();
            FormSettingsSetCEETextM.Text = m.ToString();
            //ne = 2024;
            //ye = 6;
            //re = 8;
            //se = 17;
            //fe = 0;
            //me = 0;
            FormSettingsSetEndTextN.Text = ne.ToString();
            FormSettingsSetEndTextY.Text = ne.ToString();
            FormSettingsSetEndTextR.Text = ne.ToString();
            FormSettingsSetEndTextS.Text = ne.ToString();
            FormSettingsSetEndTextF.Text = ne.ToString();
            FormSettingsSetEndTextM.Text = ne.ToString();
            FormSettingsSetExamNameText.Text = examName;
        }
        private void BtnRestart_MouseLeave(object sender, EventArgs e)
        {
            BtnRestartTip.Hide(BtnRestart);
        }
        private void FormSettings_Load(object sender, EventArgs e)
        {
            mainForm.ReadConfig();
            CheckStartupSetting();
            // 功能 更改倒计时字体大小 相关代码 CheckRBStatus();
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

        // 功能 更改倒计时字体大小 相关代码 private void CheckRBStatus()
        // 功能 更改倒计时字体大小 相关代码 {
        // 功能 更改倒计时字体大小 相关代码     if (FontId == "57c1228f-bb20-4ef1-ab63-3907b9ec8b63")
        // 功能 更改倒计时字体大小 相关代码     {
        // 功能 更改倒计时字体大小 相关代码 FormSettingsRB1.Checked = true;
        // 功能 更改倒计时字体大小 相关代码 }
        // 功能 更改倒计时字体大小 相关代码     else if (FontId == "b994bf0b-48c1-4675-aa5a-b166ab27ffd1")
        // 功能 更改倒计时字体大小 相关代码     {
        // 功能 更改倒计时字体大小 相关代码 FormSettingsRB2.Checked = true;
        // 功能 更改倒计时字体大小 相关代码 }
        // 功能 更改倒计时字体大小 相关代码     else if (FontId == "2412781d-654b-4e7e-a698-00bddacfec2f")
        // 功能 更改倒计时字体大小 相关代码     {
        // 功能 更改倒计时字体大小 相关代码 FormSettingsRB3.Checked = true;
        // 功能 更改倒计时字体大小 相关代码 }
        // 功能 更改倒计时字体大小 相关代码     else if (FontId == "66d27f44-5311-4abe-8508-e0599e6544db")
        // 功能 更改倒计时字体大小 相关代码     {
        // 功能 更改倒计时字体大小 相关代码 FormSettingsRB4.Checked = true;
        // 功能 更改倒计时字体大小 相关代码 }
        // 功能 更改倒计时字体大小 相关代码     else if (FontId == "cd49a9a2-3eaf-4ec8-84dd-fe333f5ea28e")
        // 功能 更改倒计时字体大小 相关代码     {
        // 功能 更改倒计时字体大小 相关代码 FormSettingsRB5.Checked = true;
        // 功能 更改倒计时字体大小 相关代码 }
        // 功能 更改倒计时字体大小 相关代码     else
        // 功能 更改倒计时字体大小 相关代码     {
        // 功能 更改倒计时字体大小 相关代码 FormSettingsRB3.Checked = true;
        // 功能 更改倒计时字体大小 相关代码    }
        // 功能 更改倒计时字体大小 相关代码 }
        private void FormSettingsRB1_CheckedChanged(object sender, EventArgs e)
        {
            // 功能 更改倒计时字体大小 相关代码 if (FormSettingsRB1.Checked)
            // 功能 更改倒计时字体大小 相关代码 {
            // 功能 更改倒计时字体大小 相关代码 mainForm.FontSize = "15.1";
            // 功能 更改倒计时字体大小 相关代码 FontID = "57c1228f-bb20-4ef1-ab63-3907b9ec8b63";
            // 功能 更改倒计时字体大小 相关代码 }
        }
        private void FormSettingsRB2_CheckedChanged(object sender, EventArgs e)
        {
        // 功能 更改倒计时字体大小 相关代码 if (FormSettingsRB2.Checked)
        // 功能 更改倒计时字体大小 相关代码 {
        // 功能 更改倒计时字体大小 相关代码 mainForm.FontSize = "16.15";
        // 功能 更改倒计时字体大小 相关代码 FontID = "b994bf0b-48c1-4675-aa5a-b166ab27ffd1";
        // 功能 更改倒计时字体大小 相关代码 }
        }
        private void FormSettingsRB3_CheckedChanged(object sender, EventArgs e)
        {
            // 功能 更改倒计时字体大小 相关代码 if (FormSettingsRB3.Checked)
            // 功能 更改倒计时字体大小 相关代码 {
            // 功能 更改倒计时字体大小 相关代码 mainForm.FontSize = "17.25";
            // 功能 更改倒计时字体大小 相关代码 FontID = "2412781d-654b-4e7e-a698-00bddacfec2f";
            // 功能 更改倒计时字体大小 相关代码 }
        }
        private void FormSettingsRB4_CheckedChanged(object sender, EventArgs e)
        {
            // 功能 更改倒计时字体大小 相关代码 if (FormSettingsRB4.Checked)
            // 功能 更改倒计时字体大小 相关代码 {
            // 功能 更改倒计时字体大小 相关代码 mainForm.FontSize = "18.35";
            // 功能 更改倒计时字体大小 相关代码 FontID = "66d27f44-5311-4abe-8508-e0599e6544db";
            // 功能 更改倒计时字体大小 相关代码 }
        }
        private void FormSettingsRB5_CheckedChanged(object sender, EventArgs e)
        {
            // 功能 更改倒计时字体大小 相关代码 if (FormSettingsRB5.Checked)
            // 功能 更改倒计时字体大小 相关代码 {
            // 功能 更改倒计时字体大小 相关代码 mainForm.FontSize = "19.45";
            // 功能 更改倒计时字体大小 相关代码 FontID = "cd49a9a2-3eaf-4ec8-84dd-fe333f5ea28e";
            // 功能 更改倒计时字体大小 相关代码 }
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
                LblRestart.Text = "立即关闭倒计时: ";
                BtnRestart.Text = "点击关闭";
                BtnRestartTip.SetToolTip(BtnRestart, "恭喜你发现了惊天大秘密，彳亍，那你关吧~");
            }
        }
        private void FormSettingsSyncTimeButton_Click(object sender, EventArgs e)
        {
            FormSettingsSyncTimeButton.Enabled = false;
            FormSettingsSyncTimeButton.Text = "正在同步中，请稍候...";
            startSyncTime = new Thread(StartSyncTime);
            startSyncTime.Start();
        }
        // 功能 更改倒计时字体大小 相关代码 bool isNormalClose = false;
        private void FormSettingsApply_Click(object sender, EventArgs e)
        {
            // 功能 更改倒计时字体大小 相关代码 isNormalClose = true;

            string input = FormSettingsSetExamNameText.Text;
            string output = new string(input.Trim().Replace(" ", "").Where(c => char.IsLetterOrDigit(c) || (c >= ' ' && c <= byte.MaxValue)).ToArray());

            if (string.IsNullOrWhiteSpace(output) || (output.Length < 2))
            {
                MessageBox.Show("输入的考试名称有误或太短！", "错误 - 高考倒计时", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

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
                DialogResult _result = MessageBox.Show($"你可能输入了错误的考试时间，应该没有持续超过4天的考试吧？\n\n你刚刚设置了一个持续{_dayDistance}天的考试。\n\n如果你确定当前输入的是正确的考试时间，请点击确定；若不是，请点击取消。", "警告 - 高考倒计时", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (_result == DialogResult.Cancel)
                {
                    return;
                }
            }
            examName = output;
            mainForm.WriteConfig();
            DialogResult = DialogResult.OK;
        }
        private void FormSettingsCloseMain_Click(object sender, EventArgs e)
        {
            // 功能 更改倒计时字体大小 相关代码 isNormalClose = false;
            this.Close();
        }
        private void FormSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 功能 更改倒计时字体大小 相关代码 isNormalClose = false;
            if (startSyncTime != null && startSyncTime.IsAlive)
            {
                e.Cancel = true;
                MessageBox.Show("请等待网络时钟同步完毕，然后再关闭此窗口。", "错误：无法关闭窗口 - 高考倒计时", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // 功能 更改倒计时字体大小 相关代码 if (!isNormalClose)
            // 功能 更改倒计时字体大小 相关代码 {
            // 功能 更改倒计时字体大小 相关代码 mainForm.FontSize = "17.25";
            // 功能 更改倒计时字体大小 相关代码 FontID = "2412781d-654b-4e7e-a698-00bddacfec2f";
            // 功能 更改倒计时字体大小 相关代码 }
        }
    }
}
