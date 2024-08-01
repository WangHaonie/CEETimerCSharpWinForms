using CEETimerCSharpWinForms.Forms;
using System;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Controls
{
    public abstract class AppForm : Form
    {
        /// <summary>
        /// 获取或设置一个值，该值指示窗体是否启用 WS_EX_COMPOSITED 样式以减少闪烁。
        /// </summary>
        protected bool CompositedStyle { get; set; }

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

        #region
        /*
        
        解决窗体因控件较多导致的闪烁问题 参考:

        winform窗体闪烁问题解决 - 就叫我雷人吧 - 博客园
        https://www.cnblogs.com/guosheng/p/7417918.html

         */

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                
                if (CompositedStyle)
                {
                    cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                }

                return cp;
            }
        }
        #endregion

        protected virtual void OnAppFormLoad()
        {
            TopMost = MainForm.UniTopMost;
        }

        protected abstract void OnAppFormShown();

        protected abstract void OnAppFormClosing(FormClosingEventArgs e);

        protected virtual void OnAppFormClosed() { }
    }
}
