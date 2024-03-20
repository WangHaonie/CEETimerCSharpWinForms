using CEETimerCSharpWinForms.Modules;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Forms
{
    public partial class FormMain : Form
    {
        private bool IsDaysOnly;
        private bool IsDragable;
        private bool IsFeatureMOEnabled;
        private bool IsFeatureVDMEnabled;
        private bool IsShowPast;
        private bool IsShowEnd;
        private bool IsReady;
        private bool IsRounding;
        private bool IsUniTopMost;
        private bool IsPPTService;
        private DateTime ExamEndTime;
        private DateTime ExamStartTime;
        private Font SelectedFont;
        private FontStyle SelectedFontStyle;
        private int i;
        private Timer TimerCountdown;
        private readonly FontConverter fontConverter = new();
        private FormSettings formSettings;
        private FormAbout formAbout;
        private string ExamName;
        private VirtualDesktopManager vdm;

        public FormMain()
        {
            InitializeComponent();
            FormSettings.ConfigChanged += RefreshSettings;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            if (LaunchManager.CurrentWindowsVersion >= 10)
            {
                vdm = new VirtualDesktopManager();
                IsFeatureVDMEnabled = true;
            }

            RefreshSettings(sender, e);

            TimerCountdown = new Timer()
            {
                Interval = 1000
            };

            TimerCountdown.Tick += TimerCountdown_Tick;
            TimerCountdown.Start();
        }

        private void RefreshSettings(object sender, EventArgs e)
        {
            ConfigManager.MountConfig(true);

            ExamName = ConfigManager.ReadConfig("ExamName");
            TopMost = !bool.TryParse(ConfigManager.ReadConfig("TopMost"), out bool tmpa) || tmpa;
            ShowInTaskbar = !TopMost;
            IsFeatureVDMEnabled = bool.TryParse(ConfigManager.ReadConfig("FeatureVDM"), out bool tmpb) && tmpb;
            IsFeatureMOEnabled = bool.TryParse(ConfigManager.ReadConfig("FeatureMO"), out bool tmpc) && tmpc;
            IsDaysOnly = bool.TryParse(ConfigManager.ReadConfig("DaysOnly"), out bool tmpd) && tmpd;
            IsRounding = bool.TryParse(ConfigManager.ReadConfig("Rounding"), out bool tmpe) && tmpe;
            IsShowPast = bool.TryParse(ConfigManager.ReadConfig("ShowPast"), out bool tmpg) && tmpg;
            IsShowEnd = bool.TryParse(ConfigManager.ReadConfig("ShowEnd"), out bool tmpf) && tmpf;
            IsDragable = bool.TryParse(ConfigManager.ReadConfig("Dragable"), out bool tmph) && tmph;
            IsUniTopMost = bool.TryParse(ConfigManager.ReadConfig("UniTopMost"), out bool tmpi) && tmpi;
            IsPPTService = bool.TryParse(ConfigManager.ReadConfig("PPTService"), out bool tmpj) && tmpj;
            DateTime.TryParseExact(ConfigManager.ReadConfig("ExamStartTime"), "yyyyMMddHHmmss", null, DateTimeStyles.None, out ExamStartTime);
            DateTime.TryParseExact(ConfigManager.ReadConfig("ExamEndTime"), "yyyyMMddHHmmss", null, DateTimeStyles.None, out ExamEndTime);

            IsShowPast = IsShowPast && !IsShowEnd ? IsShowEnd : IsShowPast;
            IsRounding = IsRounding && !IsDaysOnly ? IsDaysOnly : IsRounding;
            ConfigManager.UniTopMost = IsUniTopMost && TopMost;
            Location = IsPPTService ? new Point(1, 0) : new Point(0, 0);

            if (IsFeatureVDMEnabled && LaunchManager.CurrentWindowsVersion < 10)
            {
                IsFeatureVDMEnabled = false;
            }

            LocationChanged -= Form_LocationChanged;
            LableCountdown.MouseDown -= Drag_MouseDown;

            if (IsDragable)
            {
                LocationChanged += Form_LocationChanged;
                LableCountdown.MouseDown += Drag_MouseDown;
            }

            CompatibleWithPPTService();

            try
            {
                SelectedFont = (Font)fontConverter.ConvertFromString(ConfigManager.ReadConfig("Font"));
                SelectedFontStyle = (FontStyle)Enum.Parse(typeof(FontStyle), ConfigManager.ReadConfig("FontStyle"));

                if (SelectedFont.Size > 24 || SelectedFont.Size < 10)
                {
                    throw new Exception();
                }
            }
            catch
            {
                SelectedFont = (Font)fontConverter.ConvertFromString(LaunchManager.OriginalFontString);
                SelectedFontStyle = FontStyle.Bold;
            }

            LableCountdown.Font = new Font(SelectedFont, SelectedFontStyle);

            ConfigManager.MountConfig(false);

            if (!ConfigManager.IsValidData(ExamName) || !ConfigManager.IsValidData(ExamStartTime) || !ConfigManager.IsValidData(ExamEndTime) || (ExamEndTime <= ExamStartTime && IsShowEnd))
            {
                IsReady = false;
            }
            else
            {
                IsReady = true;
            }

            if (!IsReady)
            {
                LableCountdown.ForeColor = Color.Black;
                LableCountdown.Text = "欢迎使用高考倒计时，请右键点击此处到设置里添加考试信息";
            }
        }

        private void TimerCountdown_Tick(object sender, EventArgs e)
        {
            try
            {
                if (IsReady) { TriggerCountdownStart(); }
                if (IsFeatureMOEnabled) { TriggerMemoryOptimization(); } else { i = 0; }
                if (IsFeatureVDMEnabled) { TriggerVirtualDesktopDetect(); }
            }
            catch
            {

            }
        }

        private void TriggerMemoryOptimization()
        {
            i++;
            if (i % 300 == 0 || i == 5)
            {
                MemoryManager.OptimizeMemory();
            }
        }

        private void TriggerVirtualDesktopDetect()
        {
            #region 来自网络
            /* 

            自动将窗口移动到当前虚拟桌面 (Windows 10 以上) 参考：

            Virtual Desktop Switching in Windows 10 | Microsoft Learn
            https://learn.microsoft.com/en-us/archive/blogs/winsdk/virtual-desktop-switching-in-windows-10

            */

            List<Form> Forms = Application.OpenForms.Cast<Form>().ToList();// 修复报错：Collection was modified; enumeration operation may not execute.

            using NewWindow nw = new();
            foreach (Form form in Forms)
            {
                if (!vdm.IsWindowOnCurrentVirtualDesktop(form.Handle))
                {
                    nw.Show(null);
                    vdm.MoveWindowToDesktop(form.Handle, vdm.GetWindowDesktopId(nw.Handle));
                    form.Activate();
                }
            }
            #endregion
        }

        private void TriggerCountdownStart()
        {
            if (DateTime.Now < ExamStartTime)
            {
                TimeSpan TimeLeft = ExamStartTime - DateTime.Now;
                LableCountdown.ForeColor = Color.Red;

                if (IsDaysOnly)
                {
                    LableCountdown.Text = $"距离{ExamName}还有{TimeLeft.Days}天";

                    if (IsRounding)
                    {
                        LableCountdown.Text = $"距离{ExamName}还有{TimeLeft.Days + 1}天";
                    }
                }
                else
                {
                    LableCountdown.Text = $"距离{ExamName}还有{TimeLeft.Days}天{TimeLeft.Hours:00}时{TimeLeft.Minutes:00}分{TimeLeft.Seconds:00}秒";
                }
            }
            else if (DateTime.Now >= ExamStartTime && DateTime.Now < ExamEndTime && IsShowEnd)
            {
                TimeSpan TimeLeftPast = ExamEndTime - DateTime.Now;
                LableCountdown.ForeColor = Color.Green;

                if (IsDaysOnly)
                {
                    LableCountdown.Text = $"距离{ExamName}结束还有{TimeLeftPast.Days}天";

                    if (IsRounding)
                    {
                        LableCountdown.Text = $"距离{ExamName}结束还有{TimeLeftPast.Days + 1}天";
                    }
                }
                else
                {
                    LableCountdown.Text = $"距离{ExamName}结束还有{TimeLeftPast.Days}天{TimeLeftPast.Hours:00}时{TimeLeftPast.Minutes:00}分{TimeLeftPast.Seconds:00}秒";
                }
            }
            else if (DateTime.Now >= ExamEndTime && IsShowPast)
            {
                TimeSpan TimePast = DateTime.Now - ExamEndTime;
                LableCountdown.ForeColor = Color.Black;

                if (IsDaysOnly)
                {
                    LableCountdown.Text = $"距离{ExamName}已过去了{TimePast.Days}天";

                    if (IsRounding)
                    {
                        LableCountdown.Text = $"距离{ExamName}已过去了{TimePast.Days + 1}天";
                    }
                }
                else
                {
                    LableCountdown.Text = $"距离{ExamName}已过去了{TimePast.Days}天{TimePast.Hours:00}时{TimePast.Minutes:00}分{TimePast.Seconds:00}秒";
                }
            }
            else
            {
                LableCountdown.ForeColor = Color.Black;
                LableCountdown.Text = "欢迎使用高考倒计时，请右键点击此处到设置里添加考试信息";
            }
        }

        private void CompatibleWithPPTService()
        {
            if (IsPPTService && Location == new Point(0, 0))
            {
                Location = new Point(1, 0);
            }
        }

        private void Drag_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void Form_LocationChanged(object sender, EventArgs e)
        {
            Screen CurrentScreen = Screen.FromControl(this);
            Rectangle ScreenBounds = CurrentScreen.WorkingArea;
            Rectangle TaskbarBounds = CurrentScreen.Bounds;

            if (Left < ScreenBounds.Left)
            {
                Left = ScreenBounds.Left;
            }
            else if (Right > ScreenBounds.Right)
            {
                Left = ScreenBounds.Right - Width;
            }

            if (Top < ScreenBounds.Top)
            {
                Top = ScreenBounds.Top;
            }
            else if (Bottom > ScreenBounds.Bottom)
            {
                Top = ScreenBounds.Bottom - Height;
            }

            if (TaskbarBounds.IntersectsWith(Bounds))
            {
                if (Top < TaskbarBounds.Bottom && Bottom > TaskbarBounds.Bottom)
                {
                    Top = TaskbarBounds.Top - Height;
                }
            }

            CompatibleWithPPTService();
        }

        private void ContextMenuSettings_Click(object sender, EventArgs e)
        {
            if (formSettings == null || formSettings.IsDisposed)
            {
                formSettings = new FormSettings();
            }

            formSettings.FeatureMOEnabled = IsFeatureMOEnabled;
            formSettings.FeatureVDMEnabled = IsFeatureVDMEnabled;
            formSettings.TopMostChecked = TopMost;
            formSettings.ExamStartTime = ExamStartTime;
            formSettings.ExamEndTime = ExamEndTime;
            formSettings.CountdownFont = LableCountdown.Font;
            formSettings.CountdownFontStyle = LableCountdown.Font.Style;
            formSettings.ExamName = ExamName;
            formSettings.IsDaysOnly = IsDaysOnly;
            formSettings.IsShowEnd = IsShowEnd;
            formSettings.IsShowPast = IsShowPast;
            formSettings.IsRounding = IsRounding;
            formSettings.IsDragable = IsDragable;
            formSettings.IsPPTService = IsPPTService;

            formSettings.WindowState = FormWindowState.Normal;
            formSettings.Show();
            formSettings.Activate();
        }

        private void ContextMenuAbout_Click(object sender, EventArgs e)
        {
            if (formAbout == null || formAbout.IsDisposed)
            {
                formAbout = new FormAbout();
            }

            formAbout.WindowState = FormWindowState.Normal;
            formAbout.Show();
            formAbout.Activate();
        }

        private void ContextMenuOpenDir_Click(object sender, EventArgs e)
        {
            Process.Start(LaunchManager.CurrentExecutablePath);
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
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

        #region 来自网络
        /*
        
        C# 无边框窗体的拖动 参考：

        C# winform 无边框 窗体的拖动 - 双魂人生 - 博客园
        https://www.cnblogs.com/shuang121/p/3149570.html

         */

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;

        #endregion

    }
}