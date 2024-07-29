using CEETimerCSharpWinForms.Forms;
using System;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Controls
{
    public abstract class AppForm : Form
    {
        protected override void OnLoad(EventArgs e)
        {
            OnAppFormLoad();
            base.OnLoad(e);
        }

        protected override void OnShown(EventArgs e)
        {
            OnAppFormShown();
            base.OnShown(e);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            OnAppFormClosing(e);
            base.OnFormClosing(e);
        }

        protected override void OnClosed(EventArgs e)
        {
            OnAppFormClosed();
            base.OnClosed(e);
        }

        protected virtual void OnAppFormLoad()
        {
            TopMost = MainForm.UniTopMost;
        }

        protected abstract void OnAppFormShown();

        protected abstract void OnAppFormClosing(FormClosingEventArgs e);

        protected virtual void OnAppFormClosed() { }
    }
}
