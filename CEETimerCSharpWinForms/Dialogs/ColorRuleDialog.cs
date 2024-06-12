using CEETimerCSharpWinForms.Forms;
using CEETimerCSharpWinForms.Modules;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Dialogs
{
    public partial class ColorRuleDialog : Form
    {
        public int RuleType { get; set; }
        public string RuleTypeText { get; private set; }
        public string ExamTick { get; set; } = "";
        public Color Fore { get; set; } = Color.Black;
        public Color Back { get; set; } = Color.White;

        public ColorRuleDialog()
        {
            InitializeComponent();
            UIHelper.BindData(ComboBoxRuleType, [new(ColorRulesHelper.StartHint, 0), new(ColorRulesHelper.LeftHint, 1), new(ColorRulesHelper.PastHint, 2)]);
            UIHelper.AlignControls(this, ButtonOK, ButtonCancel, PanelMain);
            UIHelper.AlignControls(this, ComboBoxRuleType, LabelChars01);
            TopMost = FormMain.IsUniTopMost;
        }

        private void ColorRuleDialog_Load(object sender, EventArgs e)
        {
            ComboBoxRuleType.SelectedIndex = RuleType;

            var Ticks = ExamTick.Split(ColorRulesHelper.TsSeparator);

            if (Ticks.Length > 1)
            {
                NudDays.Value = int.Parse(Ticks[0]);
                NudHours.Value = int.Parse(Ticks[1]);
                NudMinutes.Value = int.Parse(Ticks[2]);
                NudSeconds.Value = int.Parse(Ticks[3]);
            }

            LabelForeColor.BackColor = LabelPreviewColor.ForeColor = Fore;
            LabelBackColor.BackColor = LabelPreviewColor.BackColor = Back;
        }

        private void ColorLabels_Click(object sender, EventArgs e)
        {
            var LabelSender = (Label)sender;

            ColorDialog ColorDialogMain = new()
            {
                AllowFullOpen = true,
                Color = LabelSender.BackColor,
                FullOpen = true
            };

            if (ColorDialogMain.ShowDialog() == DialogResult.OK)
            {
                LabelSender.BackColor = ColorDialogMain.Color;
            }

            LabelPreviewColor.ForeColor = LabelForeColor.BackColor;
            LabelPreviewColor.BackColor = LabelBackColor.BackColor;

            ColorDialogMain.Dispose();
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            var _Fore = LabelForeColor.BackColor;
            var _Back = LabelBackColor.BackColor;

            if (!ColorHelper.IsNiceContrast(_Fore, _Back))
            {
                MessageX.Popup($"选择的颜色相似或对比度较低，将无法看清文字。\n\n请尝试更换其它背景颜色或文字颜色！", MessageLevel.Error);
                return;
            }

            RuleType = ComboBoxRuleType.SelectedIndex;
            RuleTypeText = ((PairItems<string, int>)ComboBoxRuleType.SelectedItem).Item1;
            ExamTick = $"{NudDays.Value}天{NudHours.Value}时{NudMinutes.Value}分{NudSeconds.Value}秒";
            Fore = _Fore;
            Back = _Back;

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
