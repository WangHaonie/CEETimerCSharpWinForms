using CEETimerCSharpWinForms.Modules;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Forms
{
    public partial class FormAbout : Form
    {
        private ToolTip CheckUpdateTip;
        private bool IsCheckingUpdate = false;

        public FormAbout()
        {
            InitializeComponent();
            FormSettings.ConfigChanged += RefreshSettings;
        }

        private void FormAbout_Load(object sender, EventArgs e)
        {
            RefreshSettings(sender, e);
            LableVersion.Text = LaunchManager.AppVersionText;
            LabelAuthor.Text = LaunchManager.CopyrightInfo;

            CheckUpdateTip = new()
            {
                AutoPopDelay = 10000
            };

            CheckUpdateTip.SetToolTip(LableVersion, "是的你没有看错，点击这里可以检查是否有新版本。");
            LableVersion.MouseLeave += LableVersion_MouseLeave;
        }

        private void RefreshSettings(object sender, EventArgs e)
        {
            ConfigManager.SetTopMost(this);
        }

        private void LableVersion_MouseLeave(object sender, EventArgs e)
        {
            CheckUpdateTip.Hide(LableVersion);
        }

        private async void LabelVersion_Click(object sender, EventArgs e)
        {
            IsCheckingUpdate = true;
            LableVersion.Enabled = false;

            try
            {
                await Task.Run(() => SimpleUpdater.CheckUpdate(false, this));
            }
            finally
            {
                IsCheckingUpdate = false;
                LableVersion.Enabled = true;
            }
        }

        private void ButtonGitHub_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/WangHaonie/CEETimerCSharpWinForms");
        }

        private void ButtonLicense_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/WangHaonie/CEETimerCSharpWinForms/blob/main/LICENSE");
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormAbout_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsCheckingUpdate)
            {
                e.Cancel = true;
            }
        }
    }
}
