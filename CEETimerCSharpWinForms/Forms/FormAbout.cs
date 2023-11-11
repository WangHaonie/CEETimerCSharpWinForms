using System;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Forms
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
        }

        private void FormAbout_Load(object sender, EventArgs e)
        {

        }

        private void FormAboutBottonGH_Click(object sender, EventArgs e)
        {
            string ghurl = "https://github.com/WangHaonie/CEETimerCSharpWinForms";
            System.Diagnostics.Process.Start(ghurl);
        }

        private void FormAboutBottonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormAboutLicenseButton_Click(object sender, EventArgs e)
        {
            FormLicense formLicense = new FormLicense();
            formLicense.Show();
        }
    }
}
