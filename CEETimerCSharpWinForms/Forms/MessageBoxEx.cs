using CEETimerCSharpWinForms.Modules;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Forms
{
    public partial class MessageBoxEx : Form
    {
        public DialogResult DialogResultEx { get; set; }

        private MessageBoxExButtons ButtonsEx;

        public MessageBoxEx()
        {
            InitializeComponent();
            TopMost = FormMain.IsUniTopMost;
        }

        public void ShowCore(string Message, MessageLevel Level, MessageBoxExButtons Buttons, FormStartPosition Position)
        {
            StartPosition = Position;

            var (Sound, Icon, Title) = MessageX.GetStuff(Level);
            ButtonsEx = Buttons;

            switch (Buttons)
            {
                case MessageBoxExButtons.YesNo:
                    ButtonA.Text = "是(&Y)";
                    ButtonB.Text = "否(&N)";
                    break;
                case MessageBoxExButtons.OK:
                    ButtonA.Visible = false;
                    ButtonB.Text = "确定";
                    break;
            }

            Sound.Play();
            PicBoxIcon.Image = Icon.ToBitmap();
            Text = Title;
            LabelMessage.Text = Message;
            ButtonB.Location = new Point(Width - ButtonB.Width - 15, PanelHead.Height + 10);
            ButtonA.Location = new Point(ButtonB.Location.X - ButtonA.Width - 8, ButtonB.Location.Y);
        }

        private void ButtonA_Click(object sender, EventArgs e)
        {
            if (ButtonsEx == MessageBoxExButtons.YesNo) DialogResultEx = DialogResult.Yes;
            Close();
        }

        private void ButtonB_Click(object sender, EventArgs e)
        {
            DialogResultEx = ButtonsEx == MessageBoxExButtons.YesNo ? DialogResult.No : DialogResult.Yes;
            Close();
        }
    }
}
