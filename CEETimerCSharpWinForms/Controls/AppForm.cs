using CEETimerCSharpWinForms.Forms;
using CEETimerCSharpWinForms.Modules;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Controls
{
    public abstract class AppForm : Form
    {
        /// <summary>
        /// 获取或设置一个值，该值指示窗体是否应在加载之前就先开始调整 UI。
        /// </summary>
        protected bool AdjustBeforeLoad { get; set; }

        /// <summary>
        /// 获取或设置一个值，该值指示窗体是否启用 WS_EX_COMPOSITED 样式以减少闪烁。
        /// </summary>
        protected bool CompositedStyle { get; set; }

        private bool IsLoading = true;

        protected AppForm()
        {
            TopMost = MainForm.UniTopMost;
        }

        protected sealed override void OnLoad(EventArgs e)
        {
            if (AdjustBeforeLoad)
            {
                StartAdjustingUI();
            }

            OnAppFormLoad();

            if (!AdjustBeforeLoad)
            {
                StartAdjustingUI();
            }

            base.OnLoad(e);
        }

        protected sealed override void OnShown(EventArgs e)
        {
            IsLoading = false;
            OnAppFormShown();
            base.OnShown(e);
        }

        protected sealed override void OnFormClosing(FormClosingEventArgs e)
        {
            OnAppFormClosing(e);
            base.OnFormClosing(e);
        }

        protected sealed override void OnClosed(EventArgs e)
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

        protected sealed override CreateParams CreateParams
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

        protected virtual void AdjustUI() { }

        protected abstract void OnAppFormLoad();

        protected abstract void OnAppFormShown();

        protected abstract void OnAppFormClosing(FormClosingEventArgs e);

        protected virtual void OnAppFormClosed() { }

        /// <summary>
        /// 仅当窗体加载完成再执行指定的代码。
        /// </summary>
        protected void Execute(Action Method)
        {
            if (!IsLoading)
            {
                Method();
            }
        }

        /// <summary>
        /// 仅当在高 DPI 下执行调整控件的代码。
        /// </summary>
        protected void Adjust(Action Adjustment)
        {
            if (Extensions.DpiRatio > 1)
            {
                Adjustment();
            }
        }

        /// <summary>
        /// 在用户未保存更改并尝试关闭窗体时显示警告。同时防止直接关闭警告时也窗体会随之关闭。
        /// </summary>
        /// <param name="WarningMsg">警告信息</param>
        /// <param name="e">FormClosingEventArgs</param>
        /// <param name="SaveChanges">执行 保存更改 的代码</param>
        /// <param name="IgnoreChanges">执行 忽略更改 的代码</param>
        protected void ShowUnsavedWarning(string WarningMsg, FormClosingEventArgs e, Action SaveChanges, Action IgnoreChanges)
        {
            switch (MessageX.Popup(WarningMsg, MessageLevel.Warning, Buttons: MessageBoxExButtons.YesNo))
            {
                case DialogResult.Yes:
                    e.Cancel = true;
                    SaveChanges();
                    break;
                case DialogResult.No:
                    IgnoreChanges();
                    break;
                default:
                    e.Cancel = true;
                    break;
            }
        }

        /// <summary>
        /// 为 ComboBox 绑定统一类型的 DataSource, DisplayMember 和 ValueMember。
        /// </summary>
        /// <param name="Target">目标 ComboBox 控件</param>
        /// <param name="Data">DataSource</param>
        protected void BindComboData(ComboBox Target, List<TupleEx<string, int>> Data)
        {
            Target.DataSource = Data;
            Target.DisplayMember = "Item1";
            Target.ValueMember = "Item2";
        }

        /// <summary>
        /// 为 TextBox 设置最大文本长度。
        /// </summary>
        /// <param name="Target">目标 TextBox 控件</param>
        /// <param name="Max">最大长度</param>
        protected void SetTextBoxMax(TextBox Target, int Max)
        {
            Target.MaxLength = Max;
        }

        /// <summary>
        /// 以屏幕宽度为参考使 Label 单行内容达到一定长度时自动换行。
        /// </summary>
        /// <param name="Target">目标 Label 控件</param>
        /// <param name="FullWidth">[可选] 默认 false。true 则按屏幕宽度减10px作为最大长度，false 则屏幕宽度的3/4。</param>
        protected void SetLabelAutoWrap(Label Target, bool FullWidth = false)
        {
            var CurrentScreenWidth = UIHelper.GetCurrentScreen().WorkingArea.Width;
            SetLabelAutoWrapCore(Target, new(FullWidth ? CurrentScreenWidth - 10 : (int)(CurrentScreenWidth * 0.75), 0));
        }

        /// <summary>
        /// 以父容器宽度为参考使 Label 单行内容达到一定长度时自动换行。
        /// </summary>
        /// <param name="Target">目标 Label</param>
        /// <param name="Parent">该 Label 所在的容器</param>
        protected void SetLabelAutoWrap(Label Target, Control Parent)
        {
            SetLabelAutoWrapCore(Target, new(Parent.Width - Target.Left, 0));
        }

        /// <summary>
        /// 将一个特殊控件 (通常位于窗体左下角) 与指定控件的左边缘对齐。
        /// </summary>
        /// <param name="Target">目标按钮</param>
        /// <param name="RightButton">右下角的按钮 (通常是确定或取消)</param>
        /// <param name="Reference">指定控件</param>
        protected void AlignControlsL(Control Target, Button RightButton, Control Reference)
        {
            Target.Left = Reference.Left;
            Target.Top = RightButton.Top;
        }

        /// <summary>
        /// 将目标控件与指定的控件的右边缘对齐。
        /// </summary>
        /// <param name="Target">目标控件</param>
        /// <param name="Reference">指定控件</param>
        protected void AlignControlsR(Control Target, Control Reference)
        {
            Target.Left = Reference.Left + Reference.Width - Target.Width;
        }

        /// <summary>
        /// 将一对按钮 (通常是确定和取消) 与某容器类控件的右边缘对齐。
        /// </summary>
        /// <param name="Btn1">按钮1</param>
        /// <param name="Btn2">按钮2</param>
        /// <param name="Container">容器类控件</param>
        protected void AlignControlsR(Button Btn1, Button Btn2, Control Container)
        {
            AlignControlsRCore(Btn1, Btn2, Container, Container.Height + 6.WithDpi(Container));
        }

        /// <summary>
        /// 将一对按钮 (通常是确定和取消) 与指定控件的右边缘对齐。
        /// </summary>
        /// <param name="Btn1">按钮1</param>
        /// <param name="Btn2">按钮2</param>
        /// <param name="Reference">指定控件</param>
        protected void AlignControlsREx(Button Btn1, Button Btn2, Control Reference)
        {
            AlignControlsRCore(Btn1, Btn2, Reference, Btn2.Location.Y);
        }

        /// <summary>
        /// 将目标控件与指定控件在水平方向上对齐。
        /// </summary>
        /// <param name="Target">目标控件</param>
        /// <param name="Reference">指定控件</param>
        /// <param name="Tweak">[可选] 微调</param>
        protected void AlignControlsX(Control Target, Control Reference, int Tweak = 0)
        {
            Target.Top = Reference.Top + Reference.Height / 2 - Target.Height / 2 + Tweak.WithDpi(Reference);
        }

        /// <summary>
        /// 将一堆控件与指定控件在水平方向上对齐。
        /// </summary>
        /// <param name="Targets">控件</param>
        /// <param name="Reference">指定控件</param>
        /// <param name="Tweak">[可选] 微调</param>
        protected void AlignControlsX(Control[] Targets, Control Reference, int Tweak = 0)
        {
            foreach (var Target in Targets)
            {
                AlignControlsX(Target, Reference, Tweak);
            }
        }

        /// <summary>
        /// 使目标控件在水平方向上与指定控件变得更紧凑。
        /// </summary>
        /// <param name="Target">目标控件</param>
        /// <param name="Reference">指定控件</param>
        /// <param name="Tweak">[可选] 微调</param>
        protected void CompactControlsX(Control Target, Control Reference, int Tweak = 0)
        {
            Target.Left = Reference.Left + Reference.Width + Tweak.WithDpi(Target);
        }

        /// <summary>
        /// 使目标控件在垂直方向上与指定控件变得更紧凑。
        /// </summary>
        /// <param name="Target">目标控件</param>
        /// <param name="Reference">指定控件</param>
        /// <param name="Tweak">[可选] 微调</param>
        protected void CompactControlsY(Control Target, Control Reference, int Tweak = 0)
        {
            Target.Top = Reference.Top + Reference.Height + Tweak.WithDpi(Target);
        }

        private void SetLabelAutoWrapCore(Label Target, Size NewSize)
        {
            #region 来自网络
            /*
            
            Label 控件自动换行 参考:

            c# - Word wrap for a label in Windows Forms - Stack Overflow
            https://stackoverflow.com/a/3680595/21094697

            */
            Target.AutoSize = true;
            Target.MaximumSize = NewSize;
            #endregion
        }

        private void AlignControlsRCore(Button Btn1, Button Btn2, Control Main, int yTweak)
        {
            Btn2.Location = new(Main.Location.X + Main.Width - Btn2.Width, yTweak);
            Btn1.Location = new(Btn2.Location.X - Btn1.Width - 6.WithDpi(Main), Btn2.Location.Y);
        }

        private void StartAdjustingUI()
        {
            AdjustUI();
        }
    }
}
