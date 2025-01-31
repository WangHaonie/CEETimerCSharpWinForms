using CEETimerCSharpWinForms.Modules;

namespace CEETimerCSharpWinForms.Controls
{
    public class TrackableForm : AppForm
    {
        protected override void OnShown()
        {
            FormManager.Add(this);
        }

        protected override void OnClosed()
        {
            FormManager.Remove(this);
        }
    }
}
