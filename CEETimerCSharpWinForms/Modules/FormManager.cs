using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Modules
{
    public static class FormManager
    {
        public static List<Form> Forms = [];

        public static void Add(Form form)
        {
            lock (Forms)
            {
                Forms.Add(form);
            }
        }

        public static void Remove(Form form)
        {
            lock (Forms)
            {
                Forms.Remove(form);
            }
        }

        public static void ShowLastOpenedForm()
        {
            lock (Forms)
            {
                var lastForm = Forms.LastOrDefault();
                lastForm?.Invoke(new Action(() =>
                {
                    var IsTopMost = lastForm.TopMost;
                    var WindowHandle = lastForm.Handle;
                    lastForm.TopMost = true;
                    lastForm.WindowState = FormWindowState.Normal;
                    WindowsAPI.ShowWindowAsync(WindowHandle, 9);
                    WindowsAPI.SetForegroundWindow(WindowHandle);
                    lastForm.TopMost = IsTopMost;
                }));
            }
        }
    }
}
