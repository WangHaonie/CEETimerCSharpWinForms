using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Modules
{
    public class SingleInstanceRunner<T> where T : Form, new()
    {
        private static T Instance;

        public static T GetInstance()
        {
            if (Instance == null || Instance.IsDisposed)
            {
                Instance = new T();
            }

            Instance.WindowState = FormWindowState.Normal;
            Instance.Activate();
            return Instance;
        }
    }
}
