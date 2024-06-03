using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Modules
{
    public static class FormManager
    {
        public static List<Form> OpenForms { get; private set; } = [];

        public static void Add(Form form)
        {
            lock (OpenForms)
            {
                OpenForms.Add(form);
            }
        }

        public static void Remove(Form form)
        {
            lock (OpenForms)
            {
                OpenForms.Remove(form);
            }
        }

        public static void ShowLastOpenedForm()
        {
            lock (OpenForms)
            {
                var lastForm = OpenForms.LastOrDefault();
                lastForm?.Invoke(new Action(lastForm.ReActivate));
            }
        }
    }
}
