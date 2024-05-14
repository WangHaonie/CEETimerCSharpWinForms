using CEETimerCSharpWinForms.Modules;
using System;
using System.Drawing;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Forms
{
    public partial class MessageBoxEx : Form
    {
        private DialogResult _DialogResult;
        private MessageBoxExButtons ButtonsEx;

        public MessageBoxEx()
        {
            InitializeComponent();
            TopMost = FormMain.IsUniTopMost;
        }

        public DialogResult ShowCore(string Message, string Title, Icon MessageBoxExIcon, SystemSound Sound, MessageBoxExButtons Buttons, FormStartPosition Position, bool AutoClose)
        {
            LabelMessage.Text = Message;
            Text = Title;
            PicBoxIcon.Image = MessageBoxExIcon.ToBitmap();
            Sound.Play();
            ButtonsEx = Buttons;

            switch (Buttons)
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

            ButtonB.Location = new Point(Width - ButtonB.Width - 15, PanelHead.Height + 10);
            ButtonA.Location = new Point(ButtonB.Location.X - ButtonA.Width - 8, ButtonB.Location.Y);

            if (AutoClose) AutoCloseAsync();

            ShowDialog();

            return _DialogResult;
        }

        private void ButtonA_Click(object sender, EventArgs e)
        {
            if (ButtonsEx == MessageBoxExButtons.YesNo) _DialogResult = DialogResult.Yes;
            Close();
        }

        private void ButtonB_Click(object sender, EventArgs e)
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
