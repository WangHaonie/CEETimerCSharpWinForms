using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Modules
{
    public class RestartMyself
    {
        private static string CurrentExecutable = Application.ExecutablePath;
        private static string CurrentExecutableName = Path.GetFileName(CurrentExecutable);
        public static void RestartNow()
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = @"cmd.exe";
            processStartInfo.Arguments = @"/c taskkill /f /im " + CurrentExecutableName + " & start \"\" \"" + CurrentExecutable + "\"";
            processStartInfo.CreateNoWindow = true;
            processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            Process.Start(processStartInfo);
        }
        public static void KillMeNow()
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = @"cmd.exe";
            processStartInfo.Arguments = @"/c taskkill /f /im " + CurrentExecutableName;
            processStartInfo.CreateNoWindow = true;
            processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            Process.Start(processStartInfo);
        }
    }
}
