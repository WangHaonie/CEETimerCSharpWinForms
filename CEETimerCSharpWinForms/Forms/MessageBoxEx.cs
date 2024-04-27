using CEETimerCSharpWinForms.Modules;
using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Forms
{
    public partial class MessageBoxEx : Form
    {
        private DialogResult DialogResultEx;
        private MessageBoxExButtons ButtonsEx;

        public MessageBoxEx()
        {
            InitializeComponent();
            TopMost = FormMain.IsUniTopMost;
        }

        public DialogResult ShowCore(string Message, string Title, Icon MessageBoxExIcon, SystemSound Sound, MessageBoxExButtons Buttons, FormStartPosition Position)
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
                    ButtonB.Text = "确定";
                    break;
            }

            StartPosition = Position;

            ButtonB.Location = new Point(Width - ButtonB.Width - 15, PanelHead.Height + 10);
            ButtonA.Location = new Point(ButtonB.Location.X - ButtonA.Width - 8, ButtonB.Location.Y);

            ShowDialog();

            return DialogResultEx;
        }

        private void ButtonA_Click(object sender, EventArgs e)
        {
            if (ButtonsEx == MessageBoxExButtons.YesNo) DialogResultEx = DialogResult.Yes;
            Close();
        }

        private void ButtonB_Click(object sender, EventArgs e)
        {
            DialogResultEx = ButtonsEx == MessageBoxExButtons.YesNo ? DialogResult.No : DialogResult.OK;
            Close();
        }
    }
}
