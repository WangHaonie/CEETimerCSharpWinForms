using System.Collections.Generic;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Modules
{
    public static class UIHelper
    {
        /// <summary>
        /// 为所有的 ComboBox 设置统一的 DataSource、DisplayMember、ValueMember
        /// </summary>
        /// <param name="Target">目标 ComboBox</param>
        /// <param name="Data">数据源</param>
        public static void BindData(ComboBox Target, List<PairItems<string, int>> Data)
        {
            Target.DataSource = Data;
            Target.DisplayMember = "Item1";
            Target.ValueMember = "Item2";
        }

        /// <summary>
        /// 将一对按钮 (通常是 确定、取消) 的右边缘与另一个控件的右边缘对齐
        /// </summary>
        /// <param name="Owner">所在窗体</param>
        /// <param name="Btn1">第一个按钮</param>
        /// <param name="Btn2">第二个按钮</param>
        /// <param name="Reference">参照控件</param>
        public static void AlignControls(Form Owner, Button Btn1, Button Btn2, Control Reference)
        {
            Btn2.Location = new(Reference.Location.X + Reference.Width - Btn2.Width, Btn2.Location.Y);
            Btn1.Location = new(Btn2.Location.X - Btn1.Width - 6.WithDpi(Owner), Btn2.Location.Y);
        }

        /// <summary>
        /// 将一些在高 DPI 下严重错位的控件与正常的控件进行横向对齐
        /// </summary>
        /// <param name="Owner">所在窗体</param>
        /// <param name="Target">目标控件</param>
        /// <param name="Reference">参照控件</param>
        /// <param name="Tweak">[可选] 微调</param>
        public static void AlignControls(Form Owner, Control Target, Control Reference, int Tweak = 0)
        {
            Target.Top = Reference.Top + Reference.Height / 2 - Target.Height / 2 + Tweak.WithDpi(Owner);
        }
    }
}
