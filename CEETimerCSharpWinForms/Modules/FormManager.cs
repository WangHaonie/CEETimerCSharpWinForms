using System.Collections.Generic;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Modules
{
    public static class FormManager
    {
        public static List<Form> OpenForms => ShownForms;

        private static readonly List<Form> ShownForms = [];

        public static void Add(Form form)
        {
            lock (ShownForms)
            {
                ShownForms.Add(form);
            }
        }

        public static void Remove(Form form)
        {
            lock (ShownForms)
            {
                ShownForms.Remove(form);
            }
        }
    }
}