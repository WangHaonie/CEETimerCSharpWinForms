using CEETimerCSharpWinForms.Forms;
using CEETimerCSharpWinForms.Modules;
using System;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Controls
{
    public abstract class TrackableForm : AppForm
    {
        protected TrackableForm()
        {
            AppLauncher.TrayMenuShowAllClicked += AppLauncher_TrayMenuShowAllClicked;
            AppLauncher.UniTopMostStateChanged += AppLauncher_UniTopMostStateChanged;
        }

        private void AppLauncher_TrayMenuShowAllClicked(object sender, EventArgs e)
        {
            ValidExecute(this.ReActivate);
        }

        private void AppLauncher_UniTopMostStateChanged(object sender, EventArgs e)
        {
            ValidExecute(() =>
            {
                if (this is MainForm)
                {
                    return;
                }

                TopMost = MainForm.UniTopMost;
            });
        }

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

        private void ValidExecute(Action Method)
        {
            if (!IsDisposed)
            {
                Method();
            }
        }
    }
}
