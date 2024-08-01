using CEETimerCSharpWinForms.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Modules
{
    public static class UIHelper
    {
        /// <summary>
        /// 判断程序是否为正常启动 (第一个窗体是否为倒计时)。
        /// </summary>
        /// <param name="Forms">已打开窗体的集合</param>
        public static bool IsNormalStart(List<Form> Forms) => Forms.FirstOrDefault() is MainForm;

        /// <summary>
        /// 获取当前处于活动状态的屏幕。
        /// </summary>
        /// <returns></returns>
        public static Screen GetCurrentScreen()
        {
            var CurrentForms = FormManager.GetOpenForms();

            if (CurrentForms.Count() <= 1 && !IsNormalStart(CurrentForms))
            {
                return Screen.FromPoint(Cursor.Position);
            }

            return Screen.FromControl(CurrentForms.FirstOrDefault());
        }
    }
}