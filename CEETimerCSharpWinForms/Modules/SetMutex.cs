using System;
using System.Threading;

namespace CEETimerCSharpWinForms.Modules
{
    public class SetMutex
    {
        static Mutex defaultMutex;
        public static void setMutex()
        {
            bool isNewProcess;
            defaultMutex = new Mutex(true, "CEETimerCSharpWinForms_MUTEX_61c0097d-3682-421c-84e6-70ca37dc31dd_[A3F8B92E6D14]", out isNewProcess);

            if (!isNewProcess)
            {
                Environment.Exit(3);
            }
            try
            {
                LaunchManager.MainThread();
            }
            finally
            {
                defaultMutex.ReleaseMutex();
                defaultMutex.Dispose();
            }
        }
    }
}
