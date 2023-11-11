using System;
using System.Threading;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms
{
    static class Program
    {
        static Mutex mutex;
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            SetProcessDPIAware();

            bool np;
            mutex = new Mutex(true, "CEETimerCSharpWinForms_MUTEX_61c0097d-3682-421c-84e6-70ca37dc31dd", out np);

            if (!np)
            {
                MessageBox.Show("当前已有另一个高考倒计时正在运行。如果你没有看见另一个高考倒计时的界面，那可能程序自身出现了 Bug，请使用命令或任务管理器终止现有进程，再尝试重新开启。", "错误：启动失败 - 高考倒计时", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(3);
            }
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new CEETimerCSharpWinForms());
            }
            finally
            {
                mutex.ReleaseMutex();
                mutex.Dispose();
            }
        }
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
    }
}