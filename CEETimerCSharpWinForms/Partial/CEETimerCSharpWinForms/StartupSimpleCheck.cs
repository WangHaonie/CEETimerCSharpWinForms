using System.Windows.Forms;

namespace CEETimerCSharpWinForms
{
    public partial class CEETimerCSharpWinForms : Form
    {
        private bool isValidConfigDate()
        {
            if (!formSettings.ValidateInput() || !formSettings.ValidateEndDate())
            {
                return false;
            }
            return true;
        }
    }
}
