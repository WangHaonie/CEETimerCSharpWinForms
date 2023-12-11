using System;
using System.IO;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms
{
    public partial class CEETimerCSharpWinForms : Form
    {
        public void ReadConfig()
        {
            if (File.Exists(ConfigFile))
            {
                string[] lines = File.ReadAllLines(ConfigFile);
                foreach (string line in lines)
                {
                    if (line.StartsWith("DateTime="))
                    {
                        string DateTimeLine = line.Substring("DateTime=".Length);
                        int DateTimeLineStart = DateTimeLine.IndexOf("(") + 1;
                        int DateTimeLineEnd = DateTimeLine.IndexOf(")");
                        string[] DateTimeLineValue = DateTimeLine.Substring(DateTimeLineStart, DateTimeLineEnd - DateTimeLineStart).Split(',');

                        if (DateTimeLineValue.Length >= 6)
                        {
                            int.TryParse(DateTimeLineValue[0].Trim(), out formSettings.n);
                            int.TryParse(DateTimeLineValue[1].Trim(), out formSettings.y);
                            int.TryParse(DateTimeLineValue[2].Trim(), out formSettings.r);
                            int.TryParse(DateTimeLineValue[3].Trim(), out formSettings.s);
                            int.TryParse(DateTimeLineValue[4].Trim(), out formSettings.f);
                            int.TryParse(DateTimeLineValue[5].Trim(), out formSettings.m);

                            formSettings.xn = formSettings.n.ToString();
                            formSettings.xy = formSettings.y.ToString();
                            formSettings.xr = formSettings.r.ToString();
                            formSettings.xs = formSettings.s.ToString();
                            formSettings.xf = formSettings.f.ToString();
                            formSettings.xm = formSettings.m.ToString();
                            targetDateTime = new DateTime(formSettings.n, formSettings.y, formSettings.r, formSettings.s, formSettings.f, formSettings.m);
                        }
                    }
                    if (line.StartsWith("DateTimeEnd="))
                    {
                        string DateTimeEndLine = line.Substring("DateTimeEnd=".Length);
                        int DateTimeEndLineStart = DateTimeEndLine.IndexOf("(") + 1;
                        int DateTimeEndLineEnd = DateTimeEndLine.IndexOf(")");
                        string[] DateTimeEndLineValue = DateTimeEndLine.Substring(DateTimeEndLineStart, DateTimeEndLineEnd - DateTimeEndLineStart).Split(',');

                        if (DateTimeEndLineValue.Length >= 6)
                        {
                            int.TryParse(DateTimeEndLineValue[0].Trim(), out formSettings.ne);
                            int.TryParse(DateTimeEndLineValue[1].Trim(), out formSettings.ye);
                            int.TryParse(DateTimeEndLineValue[2].Trim(), out formSettings.re);
                            int.TryParse(DateTimeEndLineValue[3].Trim(), out formSettings.se);
                            int.TryParse(DateTimeEndLineValue[4].Trim(), out formSettings.fe);
                            int.TryParse(DateTimeEndLineValue[5].Trim(), out formSettings.me);

                            formSettings.xne = formSettings.ne.ToString();
                            formSettings.xye = formSettings.ye.ToString();
                            formSettings.xre = formSettings.re.ToString();
                            formSettings.xse = formSettings.se.ToString();
                            formSettings.xfe = formSettings.fe.ToString();
                            formSettings.xme = formSettings.me.ToString();
                            targetDateTimeEnd = new DateTime(formSettings.ne, formSettings.ye, formSettings.re, formSettings.se, formSettings.fe, formSettings.me);
                        }
                    }
                    if (line.StartsWith("ExamName="))
                    {
                        string ExamNameLine = line.Substring("ExamName=".Length);
                        int ExamNameLineStart = ExamNameLine.IndexOf('[') + 1;
                        int ExamNameLineEnd = ExamNameLine.LastIndexOf(']');
                        string ExamNameA = ExamNameLine.Substring(ExamNameLineStart, ExamNameLineEnd - ExamNameLineStart);
                        formSettings.en = ExamNameA;
                        examName = ExamNameA;
                    }
                    // 功能 更改倒计时字体大小 相关代码 if (line.StartsWith("FontSize="))
                    // 功能 更改倒计时字体大小 相关代码 {
                    // 功能 更改倒计时字体大小 相关代码 string FontId = line.Substring("FontSize=".Length);
                    // 功能 更改倒计时字体大小 相关代码 int FontIdStart = FontId.IndexOf('[') + 1;
                    // 功能 更改倒计时字体大小 相关代码 int FontIdEnd = FontId.LastIndexOf(']');
                    // 功能 更改倒计时字体大小 相关代码 string FontIdA = FontId.Substring(FontIdStart, FontIdEnd - FontIdStart);
                    // 功能 更改倒计时字体大小 相关代码 formSettings.FontId = FontIdA;
                    //FontId = FontIdA;
                    // 功能 更改倒计时字体大小 相关代码 }
                }
            }
        }

        public void WriteConfig()
        {
            string DateTimeLine = $"DateTime=({formSettings.n}, {formSettings.y}, {formSettings.r}, {formSettings.s}, {formSettings.f}, {formSettings.m})";
            string DateTimeLineEnd = $"DateTimeEnd=({formSettings.ne}, {formSettings.ye}, {formSettings.re}, {formSettings.se}, {formSettings.fe}, {formSettings.me})";
            string ExamNameLine = $"ExamName=[{formSettings.examName}]";
            // 功能 更改倒计时字体大小 相关代码 string FontSizeLine = $"FontSize=[{formSettings.FontId}]";
            string[] lines = new string[] { DateTimeLine, DateTimeLineEnd, ExamNameLine };
            // 功能 更改倒计时字体大小 相关代码 string[] lines = new string[] { DateTimeLine, DateTimeLineEnd, ExamNameLine, FontSizeLine };
            File.WriteAllLines(ConfigFile, lines);
        }
    }
}
