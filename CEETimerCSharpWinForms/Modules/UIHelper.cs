using CEETimerCSharpWinForms.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Modules
{
    public static class UIHelper
    {
        /// <summary>
        /// 向用户显示最后一个打开的窗体。
        /// </summary>
        public static void ShowLastForm()
        {
            var LastForm = GetOpenForms().LastOrDefault();
            LastForm?.Invoke(new Action(LastForm.ReActivate));
        }

        /// <summary>
        /// 用于当用户未保存更改时显示警告。防止直接关闭消息框时也窗体会关闭。
        /// </summary>
        /// <param name="WarningMsg">警告信息</param>
        /// <param name="e">FormClosingEventArgs</param>
        /// <param name="YesExecute">点击 是 执行的操作</param>
        /// <param name="NoExecute">点击 否 执行的操作</param>
        public static void ShowUserChangedWarning(string WarningMsg, FormClosingEventArgs e, Action YesExecute, Action NoExecute)
        {
            switch (MessageX.Popup(WarningMsg, MessageLevel.Warning, Buttons: MessageBoxExButtons.YesNo))
            {
                case DialogResult.Yes:
                    e.Cancel = true;
                    YesExecute();
                    break;
                case DialogResult.None:
                    e.Cancel = true;
                    break;
                default:
                    NoExecute();
                    break;
            }
        }

        /// <summary>
        /// 为 ComboBox 绑定统一类型的 DataSource, DisplayMember、ValueMember。
        /// </summary>
        /// <param name="Target">目标 ComboBox 控件</param>
        /// <param name="Data">DataSource</param>
        public static void BindData(ComboBox Target, List<TupleEx<string, int>> Data)
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
        public static void SetTextBoxMax(TextBox Target, int Max)
        {
            Target.MaxLength = Max;
        }

        /// <summary>
        /// 以屏幕宽度为参考使 Label 单行内容达到一定长度时自动换行。
        /// </summary>
        /// <param name="Target">目标 Label 控件</param>
        /// <param name="FullWidth">[可选] 默认 false。true 则按屏幕宽度减10px作为最大长度，false 则屏幕宽度的3/4。</param>
        public static void SetLabelAutoWrap(Label Target, bool FullWidth = false)
        {
            var CurrentScreenWidth = GetCurrentScreen().WorkingArea.Width;
            SetLabelAutoWrapCore(Target, new(FullWidth ? CurrentScreenWidth - 10 : (int)(CurrentScreenWidth * 0.75), 0));
        }

        /// <summary>
        /// 以父容器宽度为参考使 Label 单行内容达到一定长度时自动换行。
        /// </summary>
        /// <param name="Target">目标 Label</param>
        /// <param name="Parent">该 Label 所在的容器</param>
        public static void SetLabelAutoWrap(Label Target, Control Parent)
        {
            SetLabelAutoWrapCore(Target, new(Parent.Width - Target.Left, 0));
        }

        /// <summary>
        /// 将一个特殊按钮 (通常位于窗体左下角) 与指定控件的左边缘对齐。
        /// </summary>
        /// <param name="Target">目标按钮</param>
        /// <param name="RightButton">右下角的按钮 (通常是确定或取消)</param>
        /// <param name="Reference">指定控件</param>
        public static void AlignControlsL(Button Target, Button RightButton, Control Reference)
        {
            Target.Left = Reference.Left;
            Target.Top = RightButton.Top;
        }

        /// <summary>
        /// 将目标控件与指定的控件的右边缘对齐。
        /// </summary>
        /// <param name="Target">目标控件</param>
        /// <param name="Reference">指定控件</param>
        public static void AlignControlsR(Control Target, Control Reference)
        {
            Target.Left = Reference.Left + Reference.Width - Target.Width;
        }

        /// <summary>
        /// 将一对按钮 (通常是确定和取消) 与某容器类控件的右边缘对齐。
        /// </summary>
        /// <param name="Btn1">按钮1</param>
        /// <param name="Btn2">按钮2</param>
        /// <param name="Container">容器类控件</param>
        public static void AlignControlsR(Button Btn1, Button Btn2, Control Container)
        {
            AlignControlsRCore(Btn1, Btn2, Container, Container.Height + 6.WithDpi(Container));
        }

        /// <summary>
        /// 将一对按钮 (通常是确定和取消) 与指定控件的右边缘对齐。
        /// </summary>
        /// <param name="Btn1">按钮1</param>
        /// <param name="Btn2">按钮2</param>
        /// <param name="Reference">指定控件</param>
        public static void AlignControlsREx(Button Btn1, Button Btn2, Control Reference)
        {
            AlignControlsRCore(Btn1, Btn2, Reference, Btn2.Location.Y);
        }

        /// <summary>
        /// 将目标控件与指定控件在水平方向上对齐。
        /// </summary>
        /// <param name="Target">目标控件</param>
        /// <param name="Reference">指定控件</param>
        /// <param name="Tweak">[可选] 微调</param>
        public static void AlignControlsX(Control Target, Control Reference, int Tweak = 0)
        {
            Target.Top = Reference.Top + Reference.Height / 2 - Target.Height / 2 + Tweak.WithDpi(Reference);
        }

        /// <summary>
        /// 将一堆控件与指定控件在水平方向上对齐。
        /// </summary>
        /// <param name="Targets">控件</param>
        /// <param name="Reference">指定控件</param>
        /// <param name="Tweak">[可选] 微调</param>
        public static void AlignControlsX(Control[] Targets, Control Reference, int Tweak = 0)
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
        public static void CompactControlsX(Control Target, Control Reference, int Tweak = 0)
        {
            Target.Left = Reference.Left + Reference.Width + Tweak.WithDpi(Target);
        }

        /// <summary>
        /// 使目标控件在垂直方向上与指定控件变得更紧凑。
        /// </summary>
        /// <param name="Target">目标控件</param>
        /// <param name="Reference">指定控件</param>
        /// <param name="Tweak">[可选] 微调</param>
        public static void CompactControlsY(Control Target, Control Reference, int Tweak = 0)
        {
            Target.Top = Reference.Top + Reference.Height + Tweak.WithDpi(Target);
        }

        /// <summary>
        /// 获取所有已打开窗体。
        /// </summary>
        public static IEnumerable<Form> GetOpenForms()
        {
            return Application.OpenForms.Cast<Form>();
        }

        /// <summary>
        /// 只允许在高DPI下进行对控件的调整。
        /// </summary>
        /// <param name="Method">要执行的方法</param>
        public static void AdjustOnlyAtHighDpi(Action Method)
        {
            if (Extensions.DpiRatio > 1)
            {
                Method();
            }
        }

        /// <summary>
        /// 获取当前屏幕，决定消息框应该在鼠标所在屏幕还是在倒计时所在屏幕上显示。
        /// </summary>
        public static Screen GetCurrentScreen()
        {
            var CurrentForms = GetOpenForms();

            if (CurrentForms.Count() <= 1 && !IsNormalStart(CurrentForms))
            {
                return Screen.FromPoint(Cursor.Position);
            }

            return Screen.FromControl(CurrentForms.FirstOrDefault());
        }

        /// <summary>
        /// 判断程序是否为正常启动 (第一个窗体是否为倒计时)。
        /// </summary>
        /// <param name="Forms">已打开窗体的集合</param>
        public static bool IsNormalStart(IEnumerable<Form> Forms)
        {
            return Forms.FirstOrDefault() is MainForm;
        }

        public static void SetToolTip(Control Target, string Tip)
        {
            var _ToolTip = new ToolTip() { AutoPopDelay = 1000 };
            _ToolTip.SetToolTip(Target, Tip);
        }

        private static void SetLabelAutoWrapCore(Label Target, Size NewSize)
        {
            #region 来自网络
            /*
            
            Label 控件自动换行 参考：

            c# - Word wrap for a label in Windows Forms - Stack Overflow
            https://stackoverflow.com/a/3680595/21094697

            */
            Target.AutoSize = true;
            Target.MaximumSize = NewSize;
            #endregion
        }

        private static void AlignControlsRCore(Button Btn1, Button Btn2, Control Main, int yTweak)
        {
            Btn2.Location = new(Main.Location.X + Main.Width - Btn2.Width, yTweak);
            Btn1.Location = new(Btn2.Location.X - Btn1.Width - 6.WithDpi(Main), Btn2.Location.Y);
        }
    }
}