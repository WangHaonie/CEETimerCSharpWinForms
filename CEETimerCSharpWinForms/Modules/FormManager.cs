using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Modules
{
    public static class FormManager
    {
        public static List<Form> Forms = [];

        public static void Add(Form _Form)
        {
            lock (Forms)
            {
                Forms.Add(_Form);
            }
        }

        public static void Remove(Form _Form)
        {
            lock (Forms)
            {
                Forms.Remove(_Form);
            }
        }

        public static void ShowLastOpenedForm()
        {
            lock (Forms)
            {
                var _LastForm = Forms.LastOrDefault();
                _LastForm?.Invoke(new Action(() =>
                {
                    var IsTopMost = _LastForm.TopMost;
                    var WindowHandle = _LastForm.Handle;
                    _LastForm.TopMost = true;
                    _LastForm.WindowState = FormWindowState.Normal;
                    WindowsAPI.ShowWindowAsync(WindowHandle, 9);
                    WindowsAPI.SetForegroundWindow(WindowHandle);
                    _LastForm.TopMost = IsTopMost;
                }));
            }
        }
    }
}
