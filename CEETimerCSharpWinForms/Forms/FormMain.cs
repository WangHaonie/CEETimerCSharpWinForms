using CEETimerCSharpWinForms.Modules;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Forms
{
    public partial class FormMain : Form
    {
        public static bool IsUniTopMost { get; private set; }
        private bool IsDaysOnly;
        private bool IsDragable;
        private bool IsFeatureMOEnabled;
        private bool IsFeatureVDMEnabled;
        private bool IsMoving;
        private bool IsShowPast;
        private bool IsShowEnd;
        private bool IsReady;
        private bool IsRounding;
        private bool IsPPTService;
        private int ScreenIndex;
        private DateTime ExamEndTime;
        private DateTime ExamStartTime;
        private Font SelectedFont;
        private FontStyle SelectedFontStyle;
        private int i;
        private Timer TimerMain;
        private Timer TimerLocationWatcher;
        private Point LastLocation;
        private readonly FontConverter fontConverter = new();
        private string ExamName;
        private VirtualDesktopManager vdm;
        private List<Form> Forms;

        public FormMain()
        {
            InitializeComponent();
            InitializeExtra();
        }

        private void InitializeExtra()
        {
            FormSettings.ConfigChanged += RefreshSettings;
            LableCountdown.TextChanged += LableCountdown_TextChanged;

            TimerMain = new Timer()
            {
                Interval = 1000
            };

            TimerMain.Tick += TimerMain_Tick;
            TimerMain.Start();

            TimerLocationWatcher = new Timer()
            {
                Interval = 3000
            };

            TimerLocationWatcher.Tick += TimerLocationWatcher_Tick;
            TimerLocationWatcher.Start();
            LastLocation = Location;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            if (LaunchManager.CurrentWindowsVersion >= 10)
            {
                vdm = new VirtualDesktopManager();
                IsFeatureVDMEnabled = true;
            }

            RefreshSettings(sender, e);
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
            ScreenIndex = int.TryParse(ConfigManager.ReadConfig("Screen"), out int tmpk) ? tmpk : 0;
            DateTime.TryParseExact(ConfigManager.ReadConfig("ExamStartTime"), "yyyyMMddHHmmss", null, DateTimeStyles.None, out ExamStartTime);
            DateTime.TryParseExact(ConfigManager.ReadConfig("ExamEndTime"), "yyyyMMddHHmmss", null, DateTimeStyles.None, out ExamEndTime);
            int.TryParse(ConfigManager.ReadConfig("PosX"), out int x);
            int.TryParse(ConfigManager.ReadConfig("PosY"), out int y);

            IsShowPast = IsShowPast && IsShowEnd;
            IsRounding = IsRounding && IsDaysOnly;
            IsUniTopMost = IsUniTopMost && TopMost;
            IsFeatureVDMEnabled = IsFeatureVDMEnabled && LaunchManager.CurrentWindowsVersion >= 10;
            if (ScreenIndex > Screen.AllScreens.Length) ScreenIndex = 0;

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

            IsReady = ConfigManager.IsValidData(ExamName) && ConfigManager.IsValidData(ExamStartTime) && ConfigManager.IsValidData(ExamEndTime) && (ExamEndTime > ExamStartTime || !IsShowEnd);

            if (!IsReady)
            {
                LableCountdown.ForeColor = Color.Black;
                LableCountdown.Text = "欢迎使用高考倒计时，请右键点击此处到设置里添加考试信息";
            }

            LocationChanged -= Form_LocationChanged;
            LableCountdown.MouseDown -= Drag_MouseDown;

            if (IsDragable)
            {
                LocationChanged += Form_LocationChanged;
                LableCountdown.MouseDown += Drag_MouseDown;
                Location = new Point(x, y);
                SaveLocation(new Point(Location.X, Location.Y));
            }
            else
            {
                Location = Screen.AllScreens[ScreenIndex - 1 == -1 ? 0 : ScreenIndex - 1].Bounds.Location;
            }

            CompatibleWithPPTService();

            Forms = GetCurrentForms();
            foreach (Form form in Forms)
            {
                if (form == this) continue;
                form.TopMost = IsUniTopMost;
            }
        }

        private void LableCountdown_TextChanged(object sender, EventArgs e)
        {
            KeepOnScreen();
        }

        private void TimerLocationWatcher_Tick(object sender, EventArgs e)
        {
            TimerLocationWatcher.Stop();

            if (LastLocation == Location && IsMoving)
            {
                SaveLocation(Location);
                IsMoving = false;
            }
            else
            {
                TimerLocationWatcher.Start();
            }
        }

        private void TimerMain_Tick(object sender, EventArgs e)
        {
            try
            {
                if (IsFeatureMOEnabled) OptimizeMemory(); else i = 0;
                if (IsReady) StartCountdown();
                if (IsFeatureVDMEnabled) DetectVirtualDesktop();
            }
            catch
            {

            }
        }

        private void Drag_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                WindowsAPI.ReleaseCapture();
                WindowsAPI.SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }

        private void Form_LocationChanged(object sender, EventArgs e)
        {
            KeepOnScreen();
            CompatibleWithPPTService();

            if (IsMoving)
            {
                LastLocation = Location;
            }
            else
            {
                IsMoving = true;
                TimerLocationWatcher.Stop();
                LastLocation = Location;
                TimerLocationWatcher.Start();
            }
        }

        private void ContextMenuSettings_Click(object sender, EventArgs e)
        {
            FormSettings.FeatureMOEnabled = IsFeatureMOEnabled;
            FormSettings.FeatureVDMEnabled = IsFeatureVDMEnabled;
            FormSettings.TopMostChecked = TopMost;
            FormSettings.ExamStartTime = ExamStartTime;
            FormSettings.ExamEndTime = ExamEndTime;
            FormSettings.CountdownFont = LableCountdown.Font;
            FormSettings.CountdownFontStyle = LableCountdown.Font.Style;
            FormSettings.ExamName = ExamName;
            FormSettings.IsDaysOnly = IsDaysOnly;
            FormSettings.IsShowEnd = IsShowEnd;
            FormSettings.IsShowPast = IsShowPast;
            FormSettings.IsRounding = IsRounding;
            FormSettings.IsDragable = IsDragable;
            FormSettings.IsPPTService = IsPPTService;
            FormSettings.ScreenIndex = ScreenIndex;

            SingleInstanceRunner<FormSettings>.GetInstance().Show();
        }

        private void ContextMenuAbout_Click(object sender, EventArgs e)
        {
            SingleInstanceRunner<FormAbout>.GetInstance().Show();
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

        private List<Form> GetCurrentForms()
        {
            return Application.OpenForms.Cast<Form>().ToList();
        }

        private void OptimizeMemory()
        {
            i++;
            if (i % 300 == 0 || i == 5)
            {
                MemoryManager.OptimizeMemory();
            }
        }

        private void StartCountdown()
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

        private void DetectVirtualDesktop()
        {
            #region 来自网络
            /* 

            自动将窗口移动到当前虚拟桌面 (Windows 10 以上) 参考：

            Virtual Desktop Switching in Windows 10 | Microsoft Learn
            https://learn.microsoft.com/en-us/archive/blogs/winsdk/virtual-desktop-switching-in-windows-10

            */

            Forms = GetCurrentForms();
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

        private void CompatibleWithPPTService()
        {
            if (IsPPTService)
            {
                Rectangle ValidArea = Screen.GetWorkingArea(this);

                if (Left == ValidArea.Left && Top == ValidArea.Top)
                {
                    Left = ValidArea.Left + 1;
                }
            }
        }

        private void KeepOnScreen()
        {
            Rectangle ValidArea = Screen.GetWorkingArea(this);

            if (Left < ValidArea.Left)
                Left = ValidArea.Left;
            if (Top < ValidArea.Top)
                Top = ValidArea.Top;
            if (Right > ValidArea.Right)
                Left = ValidArea.Right - Width;
            if (Bottom > ValidArea.Bottom)
                Top = ValidArea.Bottom - Height;
        }

        private void SaveLocation(Point NewLocation)
        {
            ConfigManager.WriteConfig(new Dictionary<string, string>
            {
                { "PosX", $"{NewLocation.X}" },
                { "PosY", $"{NewLocation.Y}" }
            });
        }
    }
}