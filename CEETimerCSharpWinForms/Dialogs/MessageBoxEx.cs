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

        public DialogResult ShowCore(TrackableForm OwnerForm, string Message, string Title, Icon MessageBoxExIcon, FormStartPosition Position)
        {
            LabelMessage.Text = Message;
            Text = Title;
            PicBoxIcon.Image = MessageBoxExIcon.ToBitmap();
            StartPosition = (OwnerForm == null && !MainForm.IsNormalStart) ? FormStartPosition.CenterScreen : Position;
            ShowDialog(OwnerForm);
            return Result;
        }

        protected override void OnDialogLoad()
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

        protected override void AdjustUI()
        {
            SetLabelAutoWrap(LabelMessage);
            AdjustPanel();
        }

        protected override void OnDialogShown()
        {
            DialogSound.Play();
            AutoCloseAsync();
        }

        protected override void OnButtonAClicked()
        {
            Result = ButtonsEx == MessageBoxExButtons.YesNo ? DialogResult.Yes : DialogResult.None;
            Close();
        }

        protected override void OnButtonBClicked()
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
                await Task.Run(async () =>
                {
                    await Task.Delay(3000);

                    if (InvokeRequired)
                    {
                        Invoke(Close);
                    }
                    else
                    {
                        Close();
                    }
                });
            }
        }
    }
}
