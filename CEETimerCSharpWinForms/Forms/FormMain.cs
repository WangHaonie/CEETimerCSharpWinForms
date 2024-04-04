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
        public static bool IsTopMost { get; private set; }

        private bool IsShowOnly;
        private bool IsDragable;
        private bool IsFeatureMOEnabled;
        private bool IsFeatureVDMEnabled;
        private bool IsMoving;
        private bool IsShowPast;
        private bool IsShowEnd;
        private bool IsReady;
        private bool IsRounding;
        private bool IsPPTService;
        private Color Back1;
        private Color Back2;
        private Color Back3;
        private Color Back4;
        private Color Fore1;
        private Color Fore2;
        private Color Fore3;
        private Color Fore4;
        private int ScreenIndex;
        private int ShowOnlyIndex;
        private DateTime ExamEndTime;
        private DateTime ExamStartTime;
        private Font SelectedFont;
        private FontStyle SelectedFontStyle;
        private Timer TimerCountdown;
        private Timer TimerLocationWatcher;
        private System.Threading.Timer TimerMORunner;
        private System.Threading.Timer TimerVDMRunner;
        private Point LastLocation;
        private readonly FontConverter fontConverter = new();
        private string ExamName;
        private List<Form> Forms;

        public FormMain()
        {
            InitializeComponent();
            InitializeExtra();
            RefreshSettings(null, EventArgs.Empty);
        }

        private void InitializeExtra()
        {
            FormSettings.ConfigChanged += RefreshSettings;
            LableCountdown.TextChanged += LableCountdown_TextChanged;

            TimerCountdown = new Timer()
            {
                Interval = 1000
            };

            TimerCountdown.Tick += StartCountdown;
            TimerCountdown.Start();

            TimerLocationWatcher = new Timer()
            {
                Interval = 3000
            };

            TimerLocationWatcher.Tick += TimerLocationWatcher_Tick;
            TimerLocationWatcher.Start();
            LastLocation = Location;
        }

        private void RefreshSettings(object sender, EventArgs e)
        {
            ConfigManager.MountConfig(true);

            ExamName = ConfigManager.ReadConfig("ExamName");
            TopMost = IsTopMost = !bool.TryParse(ConfigManager.ReadConfig("TopMost"), out bool tmpa) || tmpa;
            IsFeatureVDMEnabled = bool.TryParse(ConfigManager.ReadConfig("FeatureVDM"), out bool tmpb) && tmpb;
            IsFeatureMOEnabled = bool.TryParse(ConfigManager.ReadConfig("FeatureMO"), out bool tmpc) && tmpc;
            IsShowOnly = bool.TryParse(ConfigManager.ReadConfig("ShowOnly"), out bool tmpd) && tmpd;
            IsRounding = bool.TryParse(ConfigManager.ReadConfig("Rounding"), out bool tmpe) && tmpe;
            IsShowPast = bool.TryParse(ConfigManager.ReadConfig("ShowPast"), out bool tmpg) && tmpg;
            IsShowEnd = bool.TryParse(ConfigManager.ReadConfig("ShowEnd"), out bool tmpf) && tmpf;
            IsDragable = bool.TryParse(ConfigManager.ReadConfig("Dragable"), out bool tmph) && tmph;
            IsUniTopMost = bool.TryParse(ConfigManager.ReadConfig("UniTopMost"), out bool tmpi) && tmpi;
            IsPPTService = bool.TryParse(ConfigManager.ReadConfig("PPTService"), out bool tmpj) && tmpj;
            ScreenIndex = int.TryParse(ConfigManager.ReadConfig("Screen"), out int tmpk) ? tmpk : 0;
            ShowOnlyIndex = int.TryParse(ConfigManager.ReadConfig("ShowValue"), out int tmpl) ? tmpl : 0;
            Back1 = ColorHelper.TryParseRGB(ConfigManager.ReadConfig("Back1"), out Color tmpm) ? tmpm : Color.White;
            Back2 = ColorHelper.TryParseRGB(ConfigManager.ReadConfig("Back2"), out Color tmpn) ? tmpn : Color.White;
            Back3 = ColorHelper.TryParseRGB(ConfigManager.ReadConfig("Back3"), out Color tmpo) ? tmpo : Color.White;
            Back4 = ColorHelper.TryParseRGB(ConfigManager.ReadConfig("Back4"), out Color tmpp) ? tmpp : Color.White;
            Fore1 = ColorHelper.TryParseRGB(ConfigManager.ReadConfig("Fore1"), out Color tmpq) ? tmpq : Color.Red;
            Fore2 = ColorHelper.TryParseRGB(ConfigManager.ReadConfig("Fore2"), out Color tmpr) ? tmpr : Color.Green;
            Fore3 = ColorHelper.TryParseRGB(ConfigManager.ReadConfig("Fore3"), out Color tmps) ? tmps : Color.Black;
            Fore4 = ColorHelper.TryParseRGB(ConfigManager.ReadConfig("Fore4"), out Color tmpt) ? tmpt : Color.Black;
            DateTime.TryParseExact(ConfigManager.ReadConfig("ExamStartTime"), "yyyyMMddHHmmss", null, DateTimeStyles.None, out ExamStartTime);
            DateTime.TryParseExact(ConfigManager.ReadConfig("ExamEndTime"), "yyyyMMddHHmmss", null, DateTimeStyles.None, out ExamEndTime);
            int.TryParse(ConfigManager.ReadConfig("PosX"), out int x);
            int.TryParse(ConfigManager.ReadConfig("PosY"), out int y);

            ShowInTaskbar = !TopMost;
            IsShowPast = IsShowPast && IsShowEnd;
            IsRounding = IsRounding && IsShowOnly && ShowOnlyIndex == 0;
            IsUniTopMost = IsUniTopMost && TopMost;
            IsFeatureVDMEnabled = IsFeatureVDMEnabled && LaunchManager.CurrentWindowsVersion >= 10;
            if (ScreenIndex > Screen.AllScreens.Length) ScreenIndex = 0;
            if (ShowOnlyIndex > 3) ShowOnlyIndex = 0;

            if (!ColorHelper.IsNiceContrast(Fore1, Back1))
            {
                Fore1 = Color.Red;
                Back1 = Color.White;
            }
            if (!ColorHelper.IsNiceContrast(Fore2, Back2))
            {
                Fore2 = Color.Green;
                Back2 = Color.White;
            }
            if (!ColorHelper.IsNiceContrast(Fore3, Back3))
            {
                Fore3 = Color.Black;
                Back3 = Color.White;
            }
            if (!ColorHelper.IsNiceContrast(Fore4, Back4))
            {
                Fore4 = Color.Black;
                Back4 = Color.White;
            }

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

            TimerMORunner?.Dispose();
            TimerVDMRunner?.Dispose();

            if (IsFeatureMOEnabled)
                TimerMORunner = new System.Threading.Timer(MemoryManager.OptimizeMemory, null, TimeSpan.Zero, TimeSpan.FromMinutes(5));
            if (IsFeatureVDMEnabled)
                TimerVDMRunner = new System.Threading.Timer(DetectVirtualDesktop, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));

            FormSettings.FeatureMOEnabled = IsFeatureMOEnabled;
            FormSettings.FeatureVDMEnabled = IsFeatureVDMEnabled;
            FormSettings.TopMostChecked = TopMost;
            FormSettings.ExamStartTime = ExamStartTime;
            FormSettings.ExamEndTime = ExamEndTime;
            FormSettings.CountdownFont = LableCountdown.Font;
            FormSettings.CountdownFontStyle = LableCountdown.Font.Style;
            FormSettings.ExamName = ExamName;
            FormSettings.IsShowOnly = IsShowOnly;
            FormSettings.ShowOnlyIndex = ShowOnlyIndex;
            FormSettings.IsShowEnd = IsShowEnd;
            FormSettings.IsShowPast = IsShowPast;
            FormSettings.IsRounding = IsRounding;
            FormSettings.IsDragable = IsDragable;
            FormSettings.IsPPTService = IsPPTService;
            FormSettings.ScreenIndex = ScreenIndex;
            FormSettings.Back1 = Back1;
            FormSettings.Back2 = Back2;
            FormSettings.Back3 = Back3;
            FormSettings.Back4 = Back4;
            FormSettings.Fore1 = Fore1;
            FormSettings.Fore2 = Fore2;
            FormSettings.Fore3 = Fore3;
            FormSettings.Fore4 = Fore4;
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

        private void StartCountdown(object sender, EventArgs e)
        {
            if (IsReady && DateTime.Now < ExamStartTime)
            {
                TimeSpan TimeLeft = ExamStartTime - DateTime.Now;
                BackColor = Back1;
                LableCountdown.ForeColor = Fore1;

                if (IsShowOnly)
                {
                    switch (ShowOnlyIndex)
                    {
                        case 0:
                            LableCountdown.Text = IsRounding ? $"距离{ExamName}还有{TimeLeft.Days + 1}天" : $"距离{ExamName}还有{TimeLeft.Days}天";
                            break;
                        case 1:
                            LableCountdown.Text = $"距离{ExamName}还有{TimeLeft.TotalHours:0}小时";
                            break;
                        case 2:
                            LableCountdown.Text = $"距离{ExamName}还有{TimeLeft.TotalMinutes:0}分钟";
                            break;
                        case 3:
                            LableCountdown.Text = $"距离{ExamName}还有{TimeLeft.TotalSeconds:0}秒";
                            break;
                    }
                }
                else
                {
                    LableCountdown.Text = $"距离{ExamName}还有{TimeLeft.Days}天{TimeLeft.Hours:00}时{TimeLeft.Minutes:00}分{TimeLeft.Seconds:00}秒";
                }
            }
            else if (IsReady && IsShowEnd && DateTime.Now >= ExamStartTime && DateTime.Now < ExamEndTime)
            {
                TimeSpan TimeLeftPast = ExamEndTime - DateTime.Now;
                BackColor = Back2;
                LableCountdown.ForeColor = Fore2;

                if (IsShowOnly)
                {
                    switch (ShowOnlyIndex)
                    {
                        case 0:
                            LableCountdown.Text = IsRounding ? $"距离{ExamName}结束还有{TimeLeftPast.Days + 1}天" : $"距离{ExamName}结束还有{TimeLeftPast.Days}天";
                            break;
                        case 1:
                            LableCountdown.Text = $"距离{ExamName}结束还有{TimeLeftPast.TotalHours:0}小时";
                            break;
                        case 2:
                            LableCountdown.Text = $"距离{ExamName}结束还有{TimeLeftPast.TotalMinutes:0}分钟";
                            break;
                        case 3:
                            LableCountdown.Text = $"距离{ExamName}结束还有{TimeLeftPast.TotalSeconds:0}秒";
                            break;
                    }
                }
                else
                {
                    LableCountdown.Text = $"距离{ExamName}结束还有{TimeLeftPast.Days}天{TimeLeftPast.Hours:00}时{TimeLeftPast.Minutes:00}分{TimeLeftPast.Seconds:00}秒";
                }
            }
            else if (IsReady && IsShowEnd && DateTime.Now >= ExamEndTime && IsShowPast)
            {
                TimeSpan TimePast = DateTime.Now - ExamEndTime;
                BackColor = Back3;
                LableCountdown.ForeColor = Fore3;

                if (IsShowOnly)
                {
                    switch (ShowOnlyIndex)
                    {
                        case 0:
                            LableCountdown.Text = IsRounding ? $"距离{ExamName}已过去了{TimePast.Days + 1}天" : $"距离{ExamName}已过去了{TimePast.Days}天";
                            break;
                        case 1:
                            LableCountdown.Text = $"距离{ExamName}已过去了{TimePast.TotalHours:0}小时";
                            break;
                        case 2:
                            LableCountdown.Text = $"距离{ExamName}已过去了{TimePast.TotalMinutes:0}分钟";
                            break;
                        case 3:
                            LableCountdown.Text = $"距离{ExamName}已过去了{TimePast.TotalSeconds:0}秒";
                            break;
                    }
                }
                else
                {
                    LableCountdown.Text = $"距离{ExamName}已过去了{TimePast.Days}天{TimePast.Hours:00}时{TimePast.Minutes:00}分{TimePast.Seconds:00}秒";
                }
            }
            else
            {
                BackColor = Back4;
                LableCountdown.ForeColor = Fore4;
                LableCountdown.Text = "欢迎使用高考倒计时，请右键点击此处到设置里添加考试信息";
            }
        }

        private void DetectVirtualDesktop(object state)
        {
            #region 来自网络
            /* 

            自动将窗口移动到当前虚拟桌面 (Windows 10 以上) 参考：

            Virtual Desktop Switching in Windows 10 | Microsoft Learn
            https://learn.microsoft.com/en-us/archive/blogs/winsdk/virtual-desktop-switching-in-windows-10

            */

            try
            {
                BeginInvoke(new Action(() =>
                {
                    try
                    {
                        using VirtualDesktopManager vdm = new();
                        using NewWindow nw = new();
                        Forms = GetCurrentForms();
                        foreach (Form form in Forms)
                        {
                            if (!vdm.IsWindowOnCurrentVirtualDesktop(form.Handle))
                            {
                                nw.Show(null);
                                vdm.MoveWindowToDesktop(form.Handle, vdm.GetWindowDesktopId(nw.Handle));
                                form.Activate();
                            }
                        }
                    }
                    catch
                    {
                        // Form that is already visible cannot be displayed as a modal dialog box. Set the form's visible property to false before calling Show.
                    }
                }));
            }
            catch
            {
                // Invoke or BeginInvoke cannot be called on a control until the window handle has been created.
            }
            #endregion
        }

        private void CompatibleWithPPTService()
        {
            if (IsPPTService && TopMost)
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