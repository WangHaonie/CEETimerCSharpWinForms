using System.Diagnostics;

namespace CEETimerCSharpWinForms.Modules
{
    public class ProcessHelper
    {
        public static Process RunProcess(string ProcessPath, string Args, bool AdminRequired = false, bool RedirectOutput = false)
        {
            return Process.Start(new ProcessStartInfo
            {
                FileName = ProcessPath,
                Arguments = Args,
                UseShellExecute = AdminRequired,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                RedirectStandardOutput = RedirectOutput,
                Verb = AdminRequired ? "runas" : ""
            });
        }
    }
}
