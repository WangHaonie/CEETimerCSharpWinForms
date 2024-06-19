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
        public static void ShowLastForm()
        {
            var LastForm = GetOpenForms().LastOrDefault();
            LastForm?.Invoke(new Action(LastForm.ReActivate));
        }

        public static void BindData(ComboBox Target, List<PairItems<string, int>> Data)
        {
            Target.DataSource = Data;
            Target.DisplayMember = "Item1";
            Target.ValueMember = "Item2";
        }

        public static void SetTextBoxMax(TextBox Target, int Max)
        {
            Target.MaxLength = Max;
        }

        public static void SetLabelAutoWrap(Label Target)
        {
            var CurrentScreenWidth = GetCurrentScreen().WorkingArea.Width;
            SetLabelAutoWrapInternal(Target, new(CurrentScreenWidth / 2 + CurrentScreenWidth / 4, 0));
        }

        public static void SetLabelAutoWrap(Label Target, Control Parent)
        {
            SetLabelAutoWrapInternal(Target, new(Parent.Width - Target.Left, 0));
        }

        public static void AlignControls(Button Btn1, Button Btn2, Control Reference)
        {
            Btn2.Location = new(Reference.Location.X + Reference.Width - Btn2.Width, Btn2.Location.Y);
            Btn1.Location = new(Btn2.Location.X - Btn1.Width - 6.WithDpi(Reference), Btn2.Location.Y);
        }

        public static void AlignControls(Control Target, Control Reference, int Tweak = 0)
        {
            Target.Top = Reference.Top + Reference.Height / 2 - Target.Height / 2 + Tweak.WithDpi(Reference);
        }

        public static void AlignControls(Control[] Targets, Control Reference, int Tweak = 0)
        {
            foreach (var Target in Targets)
            {
                AlignControls(Target, Reference, Tweak);
            }
        }

        public static void CompactControlsX(Control Target, Control Reference, int Tweak = 0)
        {
            Target.Left = Reference.Left + Reference.Width + Tweak.WithDpi(Target);
        }

        public static void CompactControlsY(Control Target, Control Reference, int Tweak = 0)
        {
            Target.Top = Reference.Top + Reference.Height + Tweak.WithDpi(Target);
        }

        public static IEnumerable<Form> GetOpenForms()
        {
            return Application.OpenForms.Cast<Form>();
        }

        public static void AdjustOnlyInHighDpi(Action Method)
        {
            if (Extensions.DpiRatio > 1)
            {
                Method.Invoke();
            }
        }

        public static Screen GetCurrentScreen()
        {
            var CurrentForms = GetOpenForms();

            if (CurrentForms.Count() <= 1 && !IsNormalStart(CurrentForms))
            {
                return Screen.FromPoint(Cursor.Position);
            }

            return Screen.FromControl(CurrentForms.FirstOrDefault());
        }

        public static bool IsNormalStart(IEnumerable<Form> Forms)
        {
            return Forms.FirstOrDefault() is FormMain;
        }

        private static void SetLabelAutoWrapInternal(Label Target, Size NewSize)
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
    }
}