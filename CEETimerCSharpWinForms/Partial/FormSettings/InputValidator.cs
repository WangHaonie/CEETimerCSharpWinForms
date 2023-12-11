using System;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Forms
{
    public partial class FormSettings : Form
    {
        public bool ValidateInput()
        {
            if (!int.TryParse(FormSettingsSetCEETextN.Text, out int n) || n <= 1 || n >= 3999)
            {
                return false;
            }
            if (!int.TryParse(FormSettingsSetCEETextY.Text, out int y) || y < 1 || y > 12)
            {
                return false;
            }
            if (!int.TryParse(FormSettingsSetCEETextR.Text, out int r) || r < 1 || r > 31)
            {
                return false;
            }
            if (!int.TryParse(FormSettingsSetCEETextS.Text, out int s) || s < 0 || s > 24)
            {
                return false;
            }
            if (!int.TryParse(FormSettingsSetCEETextF.Text, out int f) || f < 0 || f > 60)
            {
                return false;
            }
            if (!int.TryParse(FormSettingsSetCEETextM.Text, out int m) || m < 0 || m > 60)
            {
                return false;
            }
            if (y == 2 && (r > 29 || (r == 29 && !DateTime.IsLeapYear(n))))
            {
                return false;
            }
            if ((y == 4 || y == 6 || y == 9 || y == 11) && r > 30)
            {
                return false;
            }
            return true;
        }

        public bool ValidateEndDate()
        {
            if (!int.TryParse(FormSettingsSetEndTextN.Text, out int ne) || ne <= 1 || ne >= 3999)
            {
                return false;
            }
            if (!int.TryParse(FormSettingsSetEndTextY.Text, out int ye) || ye < 1 || ye > 12)
            {
                return false;
            }
            if (!int.TryParse(FormSettingsSetEndTextR.Text, out int re) || re < 1 || re > 31)
            {
                return false;
            }
            if (!int.TryParse(FormSettingsSetEndTextS.Text, out int se) || se < 0 || se > 24)
            {
                return false;
            }
            if (!int.TryParse(FormSettingsSetEndTextF.Text, out int fe) || fe < 0 || fe > 60)
            {
                return false;
            }
            if (!int.TryParse(FormSettingsSetEndTextM.Text, out int me) || me < 0 || me > 60)
            {
                return false;
            }
            if (ye == 2 && (re > 29 || (re == 29 && !DateTime.IsLeapYear(ne))))
            {
                return false;
            }
            if ((ye == 4 || ye == 6 || ye == 9 || ye == 11) && re > 30)
            {
                return false;
            }
            return true;
        }
    }
}
