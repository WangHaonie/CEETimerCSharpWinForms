using CEETimerCSharpWinForms.Modules;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Controls
{
    public abstract class TrackableForm : AppForm
    {
        protected override void OnAppFormLoad()
        {
            OnTrackableFormLoad();
        }

        protected override void OnAppFormShown()
        {
            FormManager.Add(this);
        }

        protected override void OnAppFormClosing(FormClosingEventArgs e)
        {
            OnTrackableFormClosing(e);
        }

        protected override void OnAppFormClosed()
        {
            OnTrackableFormClosed();
        }

        protected abstract void OnTrackableFormLoad();

        protected abstract void OnTrackableFormClosing(FormClosingEventArgs e);

        protected virtual void OnTrackableFormClosed()
        {
            FormManager.Remove(this);
        }
    }
}
