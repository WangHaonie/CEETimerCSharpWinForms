using CEETimerCSharpWinForms.Controls;
using CEETimerCSharpWinForms.Forms;
using CEETimerCSharpWinForms.Modules;
using System.Drawing;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Dialogs
{
    public partial class MessageBoxEx : DialogEx
    {
        private DialogResult Result;
        private readonly MessageBoxExButtons ButtonsEx;
        private readonly SystemSound DialogSound;
        private readonly bool AutoCloseRequired;

        public MessageBoxEx(SystemSound Sound, MessageBoxExButtons Buttons, bool AutoClose) : base(DialogExProp.KeyPreview)
        {
            InitializeComponent();
            DialogSound = Sound;
            ButtonsEx = Buttons;
            AutoCloseRequired = AutoClose;
        }

        public DialogResult ShowCore(AppForm OwnerForm, string Message, string Title, Bitmap MessageBoxExIcon, FormStartPosition Position)
        {
            LabelMessage.Text = Message;
            Text = Title;
            PicBoxIcon.Image = MessageBoxExIcon;
            StartPosition = (OwnerForm == null && !MainForm.IsNormalStart) ? FormStartPosition.CenterScreen : Position;
            ShowDialog(OwnerForm);
            return Result;
        }

        protected override void AdjustUI()
        {
            EnablePanelAutoSize(AutoSizeMode.GrowAndShrink);
            SetLabelAutoWrap(LabelMessage);
            AdjustPanel();
        }

        protected override void OnLoad()
        {
            switch (ButtonsEx)
            {
                case MessageBoxExButtons.YesNo:
                    ButtonA.Text = "是(&Y)";
                    ButtonB.Text = "否(&N)";
                    break;
                case MessageBoxExButtons.OK:
                    ButtonA.Visible = ButtonA.Enabled = false;
                    ButtonB.Text = "确定(&O)";
                    break;
            }
        }

        protected override void OnShown()
        {
            DialogSound.Play();
            AutoCloseAsync();
        }

        protected override void ButtonA_Click()
        {
            Result = ButtonsEx == MessageBoxExButtons.YesNo ? DialogResult.Yes : DialogResult.None;
            Close();
        }

        protected override void ButtonB_Click()
        {
            Result = ButtonsEx == MessageBoxExButtons.YesNo ? DialogResult.No : DialogResult.OK;
            Close();
        }

        private void MessageBoxEx_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private async void AutoCloseAsync()
        {
            if (AutoCloseRequired)
            {
                await Task.Delay(3000);
                Close();
            }
        }
    }
}
