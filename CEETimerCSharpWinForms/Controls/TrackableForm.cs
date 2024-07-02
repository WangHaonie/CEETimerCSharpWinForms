using CEETimerCSharpWinForms.Forms;
using CEETimerCSharpWinForms.Modules;
using System;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Controls
{
    public abstract class TrackableForm : Form
    {
        protected override void OnLoad(EventArgs e)
        {
            OnTrackableFormLoad();
            base.OnLoad(e);
        }

        protected override void OnShown(EventArgs e)
        {
            FormManager.Add(this);
            base.OnShown(e);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            OnTrackableFormClosing(e);
            base.OnFormClosing(e);
        }

        protected override void OnClosed(EventArgs e)
        {
            OnTrackableFormClosed();
            base.OnClosed(e);
        }

        protected virtual void OnTrackableFormLoad()
        {
            TopMost = MainForm.UniTopMost;
        }

        protected abstract void OnTrackableFormClosing(FormClosingEventArgs e);

        protected virtual void OnTrackableFormClosed()
        {
            FormManager.Remove(this);
        }
    }
}
