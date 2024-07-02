using CEETimerCSharpWinForms.Modules;
using System;

namespace CEETimerCSharpWinForms
{
    internal static class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            LaunchManager.StartProgram(args);
        }
    }
}