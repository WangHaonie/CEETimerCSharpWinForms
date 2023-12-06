using System;
using CEETimerCSharpWinForms.Modules;

namespace CEETimerCSharpWinForms
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            SetProcessDPIAware();
            SetMutex.setMutex();

        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
    }
}