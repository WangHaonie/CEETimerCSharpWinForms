using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Modules
{
    public static class FormManager
    {
        public static List<Form> OpenForms => OpenFormsInternal;
        private static readonly List<Form> OpenFormsInternal = [];

        public static void Add(Form form)
        {
            lock (OpenFormsInternal)
            {
                OpenFormsInternal.Add(form);
            }
        }

        public static void Remove(Form form)
        {
            lock (OpenFormsInternal)
            {
                OpenFormsInternal.Remove(form);
            }
        }

        public static void ShowLastOpenedForm()
        {
            lock (OpenFormsInternal)
            {
                var lastForm = OpenFormsInternal.LastOrDefault();
                lastForm?.Invoke(new Action(lastForm.ReActivate));
            }
        }
    }
}
