using CEETimerCSharpWinForms.Modules;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Forms
{
    public partial class FormAbout : Form
    {
        private ToolTip checkUpdateTip;
        private bool isCheckingUpdate = false;
        public FormAbout()
        {
            InitializeComponent();
        }
        private void FormAboutLabelVersion_MouseLeave(object sender, EventArgs e)
        {
            checkUpdateTip.Hide(FormAboutLabelVersion);
        }
        private void FormAbout_Load(object sender, EventArgs e)
        {
            FormAboutLabelVersion.Text = LaunchManager.AppVersionText;
            FormAboutLabelAuthor.Text = LaunchManager.CopyrightInfo;
            checkUpdateTip = new ToolTip
            {
                AutoPopDelay = 10000
            };
            checkUpdateTip.SetToolTip(FormAboutLabelVersion, "是的你没有看错，点击这里可以检查是否有新版本。");
            FormAboutLabelVersion.MouseLeave += FormAboutLabelVersion_MouseLeave;
        }
        private void FormAboutBottonGH_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/WangHaonie/CEETimerCSharpWinForms");
        }
        private void FormAboutBottonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void FormAboutLicenseButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/WangHaonie/CEETimerCSharpWinForms/blob/main/LICENSE");
        }
        private async void FormAboutLabelVersion_Click(object sender, EventArgs e)
        {
            FormAboutLabelVersion.Enabled = false;
            try
            {
                isCheckingUpdate = true;
                await Task.Run(() => CheckForUpdate.Start(false));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, $"{LaunchManager.ErrMsg}", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            }
            finally
            {
                isCheckingUpdate = false;
                FormAboutLabelVersion.Enabled = true;
            }
        }
        private void FormAbout_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isCheckingUpdate)
            {
                e.Cancel = true;
            }
        }
    }
}
