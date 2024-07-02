using CEETimerCSharpWinForms.Controls;
using CEETimerCSharpWinForms.Modules;
using System;

namespace CEETimerCSharpWinForms.Dialogs
{
    public partial class CustomTextDialog : DialogEx
    {
        public string[] CustomText { get; set; }

        private string P1TextRaw;
        private string P2TextRaw;
        private string P3TextRaw;

        public CustomTextDialog() : base(true, false)
        {
            InitializeComponent();
        }

        protected override void OnDialogLoad()
        {
            TextBoxP1.Text = CustomText[0];
            TextBoxP2.Text = CustomText[1];
            TextBoxP3.Text = CustomText[2];
            TextBoxP1.TextChanged += (sender, e) => UserChanged();
            TextBoxP2.TextChanged += (sender, e) => UserChanged();
            TextBoxP3.TextChanged += (sender, e) => UserChanged();
        }

        protected override void AdjustUI()
        {
            base.AdjustUI();
            UIHelper.SetLabelAutoWrap(LabelInfo, PanelMain);
            UIHelper.AlignControlsL(ButtonReset, ButtonA, LabelP3);
            UIHelper.AlignControlsX(TextBoxP1, LabelP1);
            UIHelper.AlignControlsX(TextBoxP2, LabelP2);
            UIHelper.AlignControlsX(TextBoxP3, LabelP3);
            UIHelper.AlignControlsX(ButtonReset, ButtonA);
            UIHelper.SetTextBoxMax(TextBoxP1, ConfigPolicy.MaxCustomTextLength);
            UIHelper.SetTextBoxMax(TextBoxP2, ConfigPolicy.MaxCustomTextLength);
            UIHelper.SetTextBoxMax(TextBoxP3, ConfigPolicy.MaxCustomTextLength);
        }

        private void ButtonReset_Click(object sender, EventArgs e)
        {
            UserChanged();

            TextBoxP1.Text = Placeholders.PH_P1;
            TextBoxP2.Text = Placeholders.PH_P2;
            TextBoxP3.Text = Placeholders.PH_P3;
        }

        protected override void OnButtonAClicked()
        {
            P1TextRaw = RemoveInvalid(TextBoxP1.Text);
            P2TextRaw = RemoveInvalid(TextBoxP2.Text);
            P3TextRaw = RemoveInvalid(TextBoxP3.Text);
            string[] tmp = [P1TextRaw, P2TextRaw, P3TextRaw];

            if (!(bool)CustomRuleHelper.CheckCustomText(tmp, out string Error) && !string.IsNullOrWhiteSpace(Error))
            {
                MessageX.Popup(Error, MessageLevel.Error);
                return;
            }

            CustomText = tmp;
            base.OnButtonAClicked();
        }

        private string RemoveInvalid(string s)
        {
            return s.RemoveIllegalChars();
        }
    }
}
