using System;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms
{
    public partial class CEETimerCSharpWinForms : Form
    {
        public DateTime TargetDateTime
        {
            get { return targetDateTime; }
            set { targetDateTime = value; }
        }
        public DateTime TargetDateTimeEnd
        {
            get { return targetDateTimeEnd; }
            set { targetDateTimeEnd = value; }
        }
        public string ExamName
        {
            get { return examName; }
            set { examName = value; }
        }
    }
}
