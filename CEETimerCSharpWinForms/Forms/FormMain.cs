using CEETimerCSharpWinForms.Forms;
using CEETimerCSharpWinForms.Modules;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using VirtualDesktopSwitch;

namespace CEETimerCSharpWinForms
{
    public partial class CEETimerCSharpWinForms : Form
    {
        private string ExamName;
        private DateTime ExamStartTime = new();
        private DateTime ExamEndTime = new();
        private Timer CountdownTimer;
        private int i;
        private string isTopMost;
        private bool isTopMostBool;
        private static FormSettings formSettings;
        private static FormAbout formAbout;
        #region 来自网络
        /* 

        自动将窗口移动到当前虚拟桌面 (Windows 10 以上) 参考：

        Virtual Desktop Switching in Windows 10 | Microsoft Learn
        https://learn.microsoft.com/en-us/archive/blogs/winsdk/virtual-desktop-switching-in-windows-10

        */

        private Timer VDCheckTimer;
        private VirtualDesktopManager vdm;
        private void VDCheckTimer_Tick(object sender, EventArgs e)
        {
            TriggerMemoryOptimization();
            //顺便把触发内存清理的代码搬过来将就用一下，因为全程序只有这一个是自程序运行就运行的 Timer
            try
            {
                foreach (Form form in Application.OpenForms)
                {
                    if (!vdm.IsWindowOnCurrentVirtualDesktop(form.Handle))
                    {
                        using NewWindow nw = new();
                        nw.Show(null);
                        vdm.MoveWindowToDesktop(form.Handle, vdm.GetWindowDesktopId(nw.Handle));
                        form.Activate();
                    }
                }
            }
            catch
            { }
        }
        private void InitializeVdm()
        {
            VDCheckTimer = new Timer
            {
                Enabled = true,
                Interval = 1000
            };
            VDCheckTimer.Tick += VDCheckTimer_Tick;
            vdm = new VirtualDesktopManager();
        }
        #endregion
        public CEETimerCSharpWinForms()
        {
            InitializeComponent();
        }
        private void FormMain_Load(object sender, EventArgs e)
        {
            InitializeVdm();
            RefreshSettings(sender, e);
        }
        private void RefreshSettings(object sender, EventArgs e)
        {
            ExamName = ConfigManager.ReadConfig("ExamName");
            isTopMost = ConfigManager.ReadConfig("TopMost");
            DateTime.TryParseExact(ConfigManager.ReadConfig("ExamStartTime"), "yyyyMMddHHmmss", null, System.Globalization.DateTimeStyles.None, out ExamStartTime);
            DateTime.TryParseExact(ConfigManager.ReadConfig("ExamEndTime"), "yyyyMMddHHmmss", null, System.Globalization.DateTimeStyles.None, out ExamEndTime);
            if (isTopMost.Equals("true", StringComparison.OrdinalIgnoreCase) || isTopMost.Equals("false", StringComparison.OrdinalIgnoreCase))
            {
                bool.TryParse(isTopMost, out isTopMostBool);
                TopMost = isTopMostBool;
            }
            else
            {
                TopMost = true;
            }
            if (!ConfigManager.IsValidData(ExamName) || !ConfigManager.IsValidData(ExamStartTime) || !ConfigManager.IsValidData(ExamEndTime))
            {
                labelCountdown.ForeColor = System.Drawing.Color.Black;
                labelCountdown.Text = $"欢迎使用，请右键点击此处到设置里添加考试信息";
            }
            else
            {
                CountdownTimer = new Timer()
                {
                    Interval = 1000
                };
                CountdownTimer.Tick += Timer_Tick;
                CountdownTimer.Start();
            }
        }
        private void TriggerMemoryOptimization()
        {
            i++;
            if (i % 300 == 0 || i == 5)
            {
                MemoryManager.Optimize();
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
                labelCountdown.Text = $"距离{ExamName}已经过去了{timePast.Days}天{timePast.Hours:00}时{timePast.Minutes:00}分{timePast.Seconds:00}秒";
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
            formSettings.ConfigChanged += RefreshSettings;
            formSettings.Show();
            formSettings.Activate();
        }
        private void ContextOpenDir_Click(object sender, EventArgs e)
        {
            Process.Start(LaunchManager.CurrentExecutablePath);
        }
    }
}