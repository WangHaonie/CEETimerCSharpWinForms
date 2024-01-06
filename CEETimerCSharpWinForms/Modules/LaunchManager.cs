using System;
using System.Diagnostics;
using System.IO;
/*using System.Drawing;
using System.Runtime.InteropServices;*/
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Modules
{
    public class LaunchManager
    {
        public const string AppVersion = "2.2";
        public const string InfoMsg = "提示 - 高考倒计时";
        public const string WarnMsg = "警告 - 高考倒计时";
        public const string ErrMsg = "错误 - 高考倒计时";
        public static Mutex mutex;
        public static readonly string CurrentExecutablePath = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly string CurrentExecutable = Application.ExecutablePath;
        private static readonly string CurrentExecutableName = Path.GetFileName(CurrentExecutable);
        private static readonly string OriginalFileName = "CEETimerCSharpWinForms.exe";
        public static void RestartNow()
        {
            ProcessStartInfo processStartInfo = new()
            {
                FileName = @"cmd.exe",
                Arguments = $"/c taskkill /f /fi \"PID eq {Process.GetCurrentProcess().Id}\" /im {CurrentExecutableName} & start \"\" \"{CurrentExecutable}\"",
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };
            Process.Start(processStartInfo);
        }
        public static void KillMeNow()
        {
            ProcessStartInfo processStartInfo = new()
            {
                FileName = @"cmd.exe",
                Arguments = $"/c taskkill /f /fi \"PID eq {Process.GetCurrentProcess().Id}\" /im {CurrentExecutableName}",
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };
            Process.Start(processStartInfo);
        }
        public static void CheckExecutableName()
        {
            if (!CurrentExecutableName.Equals(OriginalFileName, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show($"为了保证您的使用体验，请不要更改程序文件名！程序将在该对话框关闭后尝试自动恢复到原文件名，若自动恢复失败请手动改回。\n\n当前文件名：{CurrentExecutableName}\n原始文件名：{OriginalFileName}", $"{ErrMsg}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ProcessStartInfo processStartInfo = new()
                {
                    FileName = @"cmd.exe",
                    Arguments = $"/c ren \"{CurrentExecutable}\" {OriginalFileName} & start \"\" \"{CurrentExecutablePath}{OriginalFileName}\"",
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                };
                Process.Start(processStartInfo);
                Environment.Exit(3);
            }
            else
            {
                Task.Run(() => CheckForUpdate.Start(true));
                Application.Run(new CEETimerCSharpWinForms());
            }
        }
        public static void StartProgram()
        {
            mutex = new Mutex(true, "CEETimerCSharpWinForms_MUTEX_61c0097d-3682-421c-84e6-70ca37dc31dd_[A3F8B92E6D14]", out bool isNewProcess);
            if (!isNewProcess)
            {
                Environment.Exit(3);
            }
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                CheckExecutableName();
            }
            finally
            {
                mutex.ReleaseMutex();
                mutex.Dispose();
            }
        }/*
        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
        private const int DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1 = 19;
        private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;
        public static bool UseImmersiveDarkMode(IntPtr handle, bool enabled)
        {
            if (IsWindows10OrGreater(17763))
            {
                var attribute = DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1;
                if (IsWindows10OrGreater(18985))
                {
                    attribute = DWMWA_USE_IMMERSIVE_DARK_MODE;
                }

                int useImmersiveDarkMode = enabled ? 1 : 0;
                return DwmSetWindowAttribute(handle, (int)attribute, ref useImmersiveDarkMode, sizeof(int)) == 0;
            }

            return false;
        }
        private static bool IsWindows10OrGreater(int build = -1)
        {
            return Environment.OSVersion.Version.Major >= 10 && Environment.OSVersion.Version.Build >= build;
        }
        public static void SetDarkControls(Control control)
        {
            control.BackColor = Color.Black;
            control.ForeColor = Color.White;
            control.GetType().GetProperty("BorderColor")?.SetValue(control, Color.Black);
            foreach (Control subControl in control.Controls)
            {
                SetDarkControls(subControl);
            }
        }*/
    }
}
