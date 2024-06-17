using System.Diagnostics;

namespace CEETimerCSharpWinForms.Modules
{
    public static class ProcessHelper
    {
        public static Process RunProcess(string ProcessPath, string Args, bool AdminRequired = false, bool RedirectOutput = false) => Process.Start(new ProcessStartInfo
        {
            FileName = ProcessPath,
            Arguments = Args,
            UseShellExecute = AdminRequired,
            CreateNoWindow = true,
            WindowStyle = ProcessWindowStyle.Hidden,
            RedirectStandardOutput = RedirectOutput,
            Verb = AdminRequired ? "runas" : ""
        });

        public static string GetProcessOutput(Process process)
        {
            process.WaitForExit();
            return process.StandardOutput.ReadToEnd().Trim();
        }
    }
}