using CEETimerCSharpWinForms.Modules;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Forms
{
    public partial class AboutForm : Form
    {
        private bool IsCheckingUpdate = false;

        public AboutForm()
        {
            InitializeComponent();
            TopMost = MainForm.UniTopMost;
            LabelInfo.Text = $"{LaunchManager.AppName}\n版本 v{LaunchManager.AppVersion} x64 ({LaunchManager.AppBuildDate})";
            LabelLicense.Text = $"Licensed under the GNU GPL, v3.\n{LaunchManager.CopyrightInfo}";
        }

        private async void PicBoxLogo_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                IsCheckingUpdate = true;
                PicBoxLogo.Enabled = false;

                try
                {
                    await Task.Run(() => UpdateChecker.CheckUpdate(false, this));
                }
                finally
                {
                    IsCheckingUpdate = false;
                    PicBoxLogo.Enabled = true;
                }
            }
        }

        private void LinkLabels_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Process.Start($"https://github.com/WangHaonie/CEETimerCSharpWinForms{((LinkLabel)sender == LinkFeedback ? "/issues/new/choose" : "")}");
            }
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AboutForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = IsCheckingUpdate;
        }
    }
}
