using CEETimerCSharpWinForms.Modules.Configuration;
using System;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Dialogs
{
    public partial class ExamInfoManager : Form
    {
        public ExamInfoObject[] ExamInfo { get; set; }
        public int PeriodIndex { get; set; }

        public ExamInfoManager()
        {
            InitializeComponent();
        }

        private void ExamInfoManager_Load(object sender, EventArgs e)
        {

        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {

        }
    }
}
