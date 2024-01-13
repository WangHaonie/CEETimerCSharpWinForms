using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
/*using System.Drawing;
using System.Runtime.InteropServices;*/
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Modules
{
    public class LaunchManager
    {
        public const string AppVersion = "2.3";
        public const string InfoMsg = "提示 - 高考倒计时";
        public const string WarnMsg = "警告 - 高考倒计时";
        public const string ErrMsg = "错误 - 高考倒计时";
        public const string AppName = "高考倒计时 by WangHaonie";
        public const string CopyrightInfo = "Copyright (c) 2023-2024 WangHaonie";
        public static Mutex mutex;
        public static readonly string CurrentExecutablePath = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly string CurrentExecutable = Application.ExecutablePath;
        private static readonly string CurrentExecutableName = Path.GetFileName(CurrentExecutable);
        private static readonly string OriginalFileName = "CEETimerCSharpWinForms.exe";
        private static readonly string Dll = $"{CurrentExecutablePath}Newtonsoft.Json.dll";
        private const string DllHash = "E1E27AF7B07EEEDF5CE71A9255F0422816A6FC5849A483C6714E1B472044FA9D";
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
        public static void CheckEnv()
        {
            if (!File.Exists($"{Dll}"))
            {
                MessageBox.Show("由于找不到 Newtonsoft.Json.dll, 无法继续执行代码。重新安装程序可能会解决此问题。", $"{CurrentExecutableName} - 系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(5);
            }
            else if (!BitConverter.ToString(SHA256.Create().ComputeHash(File.OpenRead($"{Dll}"))).Replace("-", "").Equals(DllHash, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show($"{Dll} 没有被指定在 Windows 上运行，或者它包含错误。请尝试使用原始安装介质重新安装程序，或联系你的系统管理员或软件供应商以获取支持。错误状态 Unknown", $"{CurrentExecutableName} - 损坏的映像", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(5);
            }
            else if (!CurrentExecutableName.Equals(OriginalFileName, StringComparison.OrdinalIgnoreCase))
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
                CheckEnv();
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
