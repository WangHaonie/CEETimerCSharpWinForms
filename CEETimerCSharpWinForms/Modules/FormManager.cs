using CEETimerCSharpWinForms.Controls;
using System.Collections.Generic;
using System.Linq;

namespace CEETimerCSharpWinForms.Modules
{
    public static class FormManager
    {
        public static List<TrackableForm> OpenForms => ShownForms;

        private static readonly List<TrackableForm> ShownForms = [];

        public static void Add(TrackableForm form)
        {
            lock (ShownForms)
            {
                ShownForms.Add(form);
            }
        }

        public static void Remove(TrackableForm form)
        {
            lock (ShownForms)
            {
                ShownForms.Remove(form);
            }
        }

        public static TrackableForm GetFirst()
        {
            lock (ShownForms)
            {
                return ShownForms.First();
            }
        }
    }
}