using CEETimerCSharpWinForms.Forms;
using CEETimerCSharpWinForms.Modules;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms
{
    public partial class CEETimerCSharpWinForms : Form
    {
        private string ExamName;
        private DateTime ExamStartTime = new DateTime();
        private DateTime ExamEndTime = new DateTime();
        private Timer timer;
        private static FormSettings formSettings;
        private static FormAbout formAbout;
        public CEETimerCSharpWinForms()
        {
            InitializeComponent();
        }
        private void FormSettings_ConfigChanged(object sender, EventArgs e)
        {
            RefreshSettings();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshSettings();
        }
        private void RefreshSettings()
        {
            ExamName = ConfigManager.ReadConfig("ExamName");
            DateTime.TryParseExact(ConfigManager.ReadConfig("ExamStartTime"), "yyyyMMddHHmmss", null, System.Globalization.DateTimeStyles.None, out ExamStartTime);
            DateTime.TryParseExact(ConfigManager.ReadConfig("ExamEndTime"), "yyyyMMddHHmmss", null, System.Globalization.DateTimeStyles.None, out ExamEndTime);
            //MessageBox.Show($"{ExamName}，{ExamStartTime}，{ExamEndTime}");
            if (!ConfigManager.IsValidData(ExamName) || !ConfigManager.IsValidData(ExamStartTime) || !ConfigManager.IsValidData(ExamEndTime))
            {
                labelCountdown.ForeColor = System.Drawing.Color.Black;
                labelCountdown.Text = $"欢迎使用，请右键点击此处到设置里添加考试信息";
            }
            else
            {
                timer = new Timer();
                timer.Interval = 1000;
                timer.Tick += Timer_Tick;
                timer.Start();
            }
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now < ExamStartTime)
            {
                TimeSpan timeLeft = ExamStartTime - DateTime.Now;
                labelCountdown.ForeColor = System.Drawing.Color.Red;
                labelCountdown.Text = $"距离{ExamName}还有{timeLeft.Days}天{timeLeft.Hours:00}时{timeLeft.Minutes:00}分{timeLeft.Seconds:00}秒";
            }
            else if (DateTime.Now >= ExamStartTime && DateTime.Now < ExamEndTime)
            {
                TimeSpan timeLeftPast = ExamEndTime - DateTime.Now;
                labelCountdown.ForeColor = System.Drawing.Color.Green;
                labelCountdown.Text = $"距离{ExamName}结束还有{timeLeftPast.Days}天{timeLeftPast.Hours:00}时{timeLeftPast.Minutes:00}分{timeLeftPast.Seconds:00}秒";
            }
            else if (DateTime.Now >= ExamEndTime)
            {
                TimeSpan timePast = DateTime.Now - ExamEndTime;
                labelCountdown.ForeColor = System.Drawing.Color.Black;
                labelCountdown.Text = $"倒计时结束，距离{ExamName}已经过去了{timePast.Days}天{timePast.Hours:00}时{timePast.Minutes:00}分{timePast.Seconds:00}秒";
            }
        }
        private void CEETimerCSharpWinForms_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown)
            {
                Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
            }
        }
        private void ContextAbout_Click(object sender, EventArgs e)
        {
            if (formAbout == null || formAbout.IsDisposed)
            {
                formAbout = new FormAbout();
            }
            formAbout.WindowState = FormWindowState.Normal;
            formAbout.Show();
            formAbout.Activate();
        }
        private void ContextSettings_Click(object sender, EventArgs e)
        {
            if (formSettings == null || formSettings.IsDisposed)
            {
                formSettings = new FormSettings();
            }
            formSettings.WindowState = FormWindowState.Normal;
            formSettings.ConfigChanged += FormSettings_ConfigChanged;
            formSettings.Show();
            formSettings.Activate();
        }
        private void ContextOpenDir_Click(object sender, EventArgs e)
        {
            Process.Start(AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}