using System;
using CEETimerCSharpWinForms.Modules;

namespace CEETimerCSharpWinForms
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            SetMutex.setMutex();
        }
    }
}