using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Modules
{
    public static class FormManager
    {
        public static List<Form> ShownForms { get; private set; } = [];

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

        /// <summary>
        /// 向用户显示最后一个打开的窗体。
        /// </summary>
        public static void ShowLastForm()
        {
            lock (ShownForms)
            {
                var lastForm = ShownForms.LastOrDefault();
                lastForm?.BeginInvoke(lastForm.ReActivate);
            }
        }
    }
}