using CEETimerCSharpWinForms.Forms;
using System.Linq;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Modules
{
    public static class UIHelper
    {
        /// <summary>
        /// 获取当前处于活动状态的屏幕。
        /// </summary>
        /// <returns></returns>
        public static Screen GetCurrentScreen()
        {
            var CurrentForms = FormManager.OpenForms;

            if (CurrentForms.Count() <= 1 && !MainForm.IsNormalStart)
            {
                return Screen.FromPoint(Cursor.Position);
            }

            return Screen.FromControl(CurrentForms.FirstOrDefault());
        }
    }
}