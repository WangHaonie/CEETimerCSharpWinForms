using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Controls
{
    [ToolboxItem(true)]
    public class ListViewEx : ListView
    {
        protected override void OnHandleCreated(EventArgs e)
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();

            base.OnHandleCreated(e);
        }
    }
}
