using CEETimerCSharpWinForms.Modules;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Forms
{
    public partial class FormAbout : Form
    {
        private ToolTip checkUpdateTip;

        public FormAbout()
        {
            InitializeComponent();

            this.FormAboutLabelVersion.Text = "版本 v" + LaunchManager.AppVersion + " (x64)";

            checkUpdateTip = new ToolTip();
            checkUpdateTip.AutoPopDelay = 10000;
            checkUpdateTip.SetToolTip(FormAboutLabelVersion, "是的你没有看错，点击这里可以检查是否有新版本。");
            FormAboutLabelVersion.MouseLeave += FormAboutLabelVersion_MouseLeave;
        }

        private void FormAboutLabelVersion_MouseLeave(object sender, EventArgs e)
        {
            checkUpdateTip.Hide(FormAboutLabelVersion);
        }

        private void FormAbout_Load(object sender, EventArgs e)
        {
            
        }

        private void FormAboutBottonGH_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/WangHaonie/CEETimerCSharpWinForms");
        }

        private void FormAboutBottonClose_Click(object sender, EventArgs e)
        {
            this.Close();
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
                await Task.Run(() => CheckForUpdate.Start(false));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误 - 高考倒计时", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            }
            finally
            {
                FormAboutLabelVersion.Enabled = true;
            }
        }
    }
}
