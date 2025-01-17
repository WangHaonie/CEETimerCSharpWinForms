using CEETimerCSharpWinForms.Modules;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Controls
{
    public abstract class TrackableForm : AppForm
    {
        protected sealed override void OnAppFormLoad()
        {
            OnTrackableFormLoad();
        }

        protected sealed override void OnAppFormShown()
        {
            FormManager.Add(this);
        }

        protected sealed override void OnAppFormClosing(FormClosingEventArgs e)
        {
            OnTrackableFormClosing(e);
        }

        protected sealed override void OnAppFormClosed()
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
