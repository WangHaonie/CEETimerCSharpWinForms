using System.Threading;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Modules
{
    public class LaunchManager
    {
        public const string AppVersion = "1.8";
        public static void MainThread()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Thread mainCheckUpdate = new Thread(CheckForUpdate.checkForUpdate);
            Thread mainStartGC = new Thread(AutoGC.startGC);

            mainCheckUpdate.Start();
            mainStartGC.Start();

            Application.Run(new CEETimerCSharpWinForms());
        }
    }
}
