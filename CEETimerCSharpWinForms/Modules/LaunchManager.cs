using System;
using System.Threading;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Modules
{
    public class LaunchManager
    {
        public const string AppVersion = "2.1";
        public const string InfoMsg = "提示 - 高考倒计时";
        public const string WarnMsg = "警告 - 高考倒计时";
        public const string ErrMsg = "错误 - 高考倒计时";
        public static Mutex defaultMutex;
        public static void MainThread()
        {
            bool isNewProcess;
            defaultMutex = new Mutex(true, "CEETimerCSharpWinForms_MUTEX_61c0097d-3682-421c-84e6-70ca37dc31dd_[A3F8B92E6D14]", out isNewProcess);
            if (!isNewProcess)
            {
                Environment.Exit(3);
            }
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Thread mainCheckUpdate = new Thread(CheckForUpdate.checkForUpdate);
                mainCheckUpdate.Start();
                Application.Run(new CEETimerCSharpWinForms());
            }
            finally
            {
                defaultMutex.ReleaseMutex();
                defaultMutex.Dispose();
            }
        }
    }
}
