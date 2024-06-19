using CEETimerCSharpWinForms.Controls;
using CEETimerCSharpWinForms.Forms;
using CEETimerCSharpWinForms.Modules;
using System;
using System.Drawing;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Dialogs
{
    public partial class MessageBoxEx : DialogEx
    {
        private DialogResult _DialogResult;
        private readonly MessageBoxExButtons ButtonsEx;
        private readonly SystemSound DialogSound;
        private readonly bool AutoCloseRequired;

        public MessageBoxEx(SystemSound Sound, MessageBoxExButtons Buttons, bool AutoClose)
        {
            InitializeComponent();
            Shown += MessageBoxEx_Shown;
            DialogSound = Sound;
            ButtonsEx = Buttons;
            AutoCloseRequired = AutoClose;
        }

        public DialogResult ShowCore(Form OwnerForm, string Message, string Title, Icon MessageBoxExIcon, FormStartPosition Position)
        {
            UIHelper.SetLabelAutoWrap(LabelMessage);
            LabelMessage.Text = Message;
            Text = Title;
            PicBoxIcon.Image = MessageBoxExIcon.ToBitmap();

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

            StartPosition = Position;

            if (OwnerForm == null && !UIHelper.IsNormalStart(UIHelper.GetOpenForms()))
            {
                StartPosition = FormStartPosition.Manual;
                var CurrentScreen = UIHelper.GetCurrentScreen().WorkingArea;
                Location = new(CurrentScreen.Left + CurrentScreen.Width / 2 - Width / 2, CurrentScreen.Top + CurrentScreen.Height / 2 - Height / 2);
            }

            ButtonB.Location = new(Width - ButtonB.Width - 15.WithDpi(this), PanelMain.Height + 10.WithDpi(this));
            ButtonA.Location = new(ButtonB.Location.X - ButtonA.Width - 6.WithDpi(this), ButtonB.Location.Y);
            ShowDialog(OwnerForm);
            return _DialogResult;
        }

        protected override void OnDialogLoad()
        {
            TopMost = MainForm.IsUniTopMost;
        }

        private void MessageBoxEx_Shown(object sender, EventArgs e)
        {
            DialogSound.Play();
            if (AutoCloseRequired) AutoCloseAsync();
        }

        protected override void OnButtonAClicked()
        {
            if (ButtonsEx == MessageBoxExButtons.YesNo) _DialogResult = DialogResult.Yes;
            Close();
        }

        protected override void OnButtonBClicked()
        {
            _DialogResult = ButtonsEx == MessageBoxExButtons.YesNo ? DialogResult.No : DialogResult.OK;
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
            await Task.Run(async () =>
            {
                await Task.Delay(3000);

                if (InvokeRequired)
                {
                    Invoke(new Action(() => Close()));
                }
                else
                {
                    Close();
                }
            });
        }
    }
}
