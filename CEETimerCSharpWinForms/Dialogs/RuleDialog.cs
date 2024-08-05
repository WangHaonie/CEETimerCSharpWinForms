using CEETimerCSharpWinForms.Controls;
using CEETimerCSharpWinForms.Modules;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Dialogs
{
    public partial class RuleDialog : DialogEx
    {
        public int RuleType { get; set; }
        public string ExamTick { get; set; } = "";
        public Color Fore { get; set; } = Color.Black;
        public Color Back { get; set; } = Color.White;
        public string CustomText { get; set; } = string.Empty;
        public string[] CustomTextPreferences { get; set; }

        private string LastText;
        private readonly Dictionary<int, string> UserUnsavedText = [];

        public RuleDialog() : base(true, false)
        {
            InitializeComponent();
        }

        protected override void OnDialogLoad()
        {
            LabelCustomInfo.Text = $"可用的占位符: {Placeholders.PH_PHINFO}";

            BindComboData(ComboBoxRuleType, [new(Placeholders.PH_START, 0), new(Placeholders.PH_LEFT, 1), new(Placeholders.PH_PAST, 2)]);
            ComboBoxRuleType.SelectedIndex = RuleType;
            var Ticks = ExamTick.Split(CustomRuleHelper.TsSeparator);

            if (Ticks.Length > 1)
            {
                NudDays.Value = int.Parse(Ticks[0]);
                NudHours.Value = int.Parse(Ticks[1]);
                NudMinutes.Value = int.Parse(Ticks[2]);
                NudSeconds.Value = int.Parse(Ticks[3]);
            }

            LabelForeColor.BackColor = LabelPreviewColor.ForeColor = Fore;
            LabelBackColor.BackColor = LabelPreviewColor.BackColor = Back;
            TextBoxCustomText.Text = CustomText;

            if (string.IsNullOrEmpty(CustomText))
            {
                ResetCustomText();
            }
            else if (CustomText != CustomRuleHelper.GetCustomTextDefault(ComboBoxRuleType.SelectedIndex, CustomTextPreferences))
            {
                SaveUserUnsavedText();
            }
        }

        protected override void AdjustUI()
        {
            SetLabelAutoWrap(LabelCustomInfo, PanelMain);
            EnablePanelAutoSize(AutoSizeMode.GrowOnly);
            AdjustPanel();
            AlignControlsX(ComboBoxRuleType, LabelChars01);
            AlignControlsR(LabelPreviewColor, LinkReset);

            Adjust(() =>
            {
                AlignControlsR(LinkReset, LabelCustomInfo);
                AlignControlsX([LabelChars02, LabelChars03, LabelChars04, LabelChars05, ComboBoxRuleType, NudDays, NudHours, NudMinutes, NudSeconds], LabelChars01);
                AlignControlsX([TextBoxCustomText, LinkReset], LabelCustomText);
                CompactControlsX(LinkReset, TextBoxCustomText);
                CompactControlsX(TextBoxCustomText, LabelCustomText);
            });
        }

        private void ColorLabels_Click(object sender, EventArgs e)
        {
            var LabelSender = (Label)sender;
            var ColorDialogMain = new ColorDialogEx();

            if (ColorDialogMain.ShowDialog(LabelSender.BackColor) == DialogResult.OK)
            {
                LabelSender.BackColor = ColorDialogMain.Color;
            }

            LabelPreviewColor.ForeColor = LabelForeColor.BackColor;
            LabelPreviewColor.BackColor = LabelBackColor.BackColor;

            ColorDialogMain.Dispose();
        }

        protected override void OnButtonAClicked()
        {
            var _Fore = LabelForeColor.BackColor;
            var _Back = LabelBackColor.BackColor;

            if (!ColorHelper.IsNiceContrast(_Fore, _Back))
            {
                MessageX.Popup("选择的颜色相似或对比度较低，将无法看清文字。\n\n请尝试更换其它背景颜色或文字颜色！", MessageLevel.Error);
                return;
            }

            if (NudDays.Value == 0 && NudHours.Value == 0 && NudMinutes.Value == 0 && NudSeconds.Value == 0)
            {
                MessageX.Popup("时刻不能为0，请重新设置！", MessageLevel.Error);
                return;
            }

            if (!(bool)CustomRuleHelper.CheckCustomText([TextBoxCustomText.Text], out string Error, ToBoolean: true) && !string.IsNullOrWhiteSpace(Error))
            {
                MessageX.Popup(Error, MessageLevel.Error);
                return;
            }

            RuleType = ComboBoxRuleType.SelectedIndex;
            ExamTick = $"{NudDays.Value}天{NudHours.Value}时{NudMinutes.Value}分{NudSeconds.Value}秒";
            Fore = _Fore;
            Back = _Back;
            CustomText = TextBoxCustomText.Text.RemoveIllegalChars();

            base.OnButtonAClicked();
        }

        private void ComboBoxRuleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Execute(() =>
            {
                var UserSelected = ComboBoxRuleType.SelectedIndex;

                if (UserUnsavedText.ContainsKey(UserSelected))
                {
                    if (TextBoxCustomText.Text != LastText)
                    {
                        LastText = TextBoxCustomText.Text;
                    }

                    TextBoxCustomText.Text = UserUnsavedText[UserSelected];
                    return;
                }

                ResetCustomText();
            });
        }

        private void TextBoxCustomText_TextChanged(object sender, EventArgs e)
        {
            Execute(SaveUserUnsavedText);
        }

        private void LinkReset_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ResetCustomText();
        }

        private void ResetCustomText()
        {
            TextBoxCustomText.Text = CustomRuleHelper.GetCustomTextDefault(ComboBoxRuleType.SelectedIndex, CustomTextPreferences);
        }

        private void SaveUserUnsavedText()
        {
            UserUnsavedText[ComboBoxRuleType.SelectedIndex] = TextBoxCustomText.Text;
            LastText = TextBoxCustomText.Text;
        }
    }
}
