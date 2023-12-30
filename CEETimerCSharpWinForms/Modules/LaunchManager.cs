using System;
using System.Threading;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Modules
{
    public class LaunchManager
    {
        public const string AppVersion = "2.0";
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
