using CEETimerCSharpWinForms.Controls;
using CEETimerCSharpWinForms.Modules;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Forms
{
    public partial class AboutForm : AppForm
    {
        private bool IsCheckingUpdate = false;

        public AboutForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad()
        {
            LabelInfo.Text = $"{AppLauncher.AppName}\n版本 v{AppLauncher.AppVersion} x64 ({AppLauncher.AppBuildDate})";
            LabelLicense.Text = $"Licensed under the GNU GPL, v3.\n{AppLauncher.CopyrightInfo}";
        }

        private async void PicBoxLogo_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                IsCheckingUpdate = true;
                PicBoxLogo.Enabled = false;

                try
                {
                    var OriginalVersionString = LabelInfo.Text;
                    LabelInfo.Text = $"{AppLauncher.AppName}\n正在检查更新，请稍候...";
                    await Task.Run(() => UpdateChecker.CheckUpdate(false, this));
                    LabelInfo.Text = OriginalVersionString;
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

        protected override void OnClosing(FormClosingEventArgs e)
        {
            e.Cancel = IsCheckingUpdate;
        }
    }
}
