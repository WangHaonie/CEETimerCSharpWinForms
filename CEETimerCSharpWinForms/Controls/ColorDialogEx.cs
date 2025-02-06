using CEETimerCSharpWinForms.Forms;
using CEETimerCSharpWinForms.Modules.Configuration;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Controls
{
    public class ColorDialogEx : ColorDialog
    {
        public static int[] CustomColorCollection { get; set; }

        private int[] PreviousCustomColors;

        public ColorDialogEx()
        {
            AllowFullOpen = true;
            FullOpen = true;
            CustomColors = CustomColorCollection;
        }

        public DialogResult ShowDialog(Color Default)
        {
            Color = Default;
            PreviousCustomColors = CustomColorCollection;
            var _DialogResult = ShowDialog();

            if (_DialogResult == DialogResult.OK)
            {
                CustomColorCollection = CustomColors;
                SaveCustomColors();
            }

            return _DialogResult;
        }

        private void SaveCustomColors()
        {
            if (CustomColorCollection != null && PreviousCustomColors != null && !CustomColorCollection.SequenceEqual(PreviousCustomColors))
            {
                var ExistingConfig = MainForm.AppConfigPub;
                ExistingConfig.CustomColors = CustomColorCollection;
                new ConfigHandler().Save(ExistingConfig);
            }
        }
    }
}
