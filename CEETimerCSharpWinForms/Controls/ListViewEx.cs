using System;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Controls
{
    public sealed class ListViewEx : ListView
    {
        protected override void OnHandleCreated(EventArgs e)
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();
            base.OnHandleCreated(e);
        }
    }
}
