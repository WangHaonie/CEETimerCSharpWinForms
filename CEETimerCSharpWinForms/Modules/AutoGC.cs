using System;
using System.Timers;

namespace CEETimerCSharpWinForms.Modules
{
    public class AutoGC
    {
        public static void startGC()
        {
            Timer newGCTimer = new Timer(30000);
            newGCTimer.Elapsed += TimerElapsed;
            newGCTimer.Start();
        }

        static void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
    }
}
