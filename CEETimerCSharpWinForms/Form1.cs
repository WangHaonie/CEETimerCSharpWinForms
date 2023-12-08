using CEETimerCSharpWinForms.Forms;
using System;
using System.Diagnostics;
// 未启用 using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms
{
    public partial class CEETimerCSharpWinForms : Form
    {
        private string ConfigFile = AppDomain.CurrentDomain.BaseDirectory + "CEETimerCSharpWinFormsConfig.db";
        private DateTime targetDateTime = new DateTime();
        private DateTime targetDateTimeEnd = new DateTime();
        private string examName = "";

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

        private System.Windows.Forms.Timer timer;
        private FormSettings formSettings;

        public CEETimerCSharpWinForms()
        {
            InitializeComponent();

            formSettings = new FormSettings();

            this.StartPosition = FormStartPosition.Manual;
            this.Location = new System.Drawing.Point(0, 0);
            this.TopMost = true;

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;

            ReadConfig();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer.Start();
        }

        // 未启用 private float FontSize = 17f;
        private void Timer_Tick(object sender, EventArgs e)
        {
            // 未启用 FormSettings formSettings = new FormSettings(this);
            // 未启用 string FontID = formSettings.FontIDE;
            // 未启用 float FontSize = labelCountdown.Font.Size;
            // 未启用 if (FontID == "57c1228f-bb20-4ef1-ab63-3907b9ec8b63")
            // 未启用 {
            // 未启用 FontSize = 15f;
            // 未启用 }
            // 未启用 else if (FontID == "b994bf0b-48c1-4675-aa5a-b166ab27ffd1")
            // 未启用 {
            // 未启用     FontSize = 16f;
            // 未启用 }
            // 未启用 else if (FontID == "2412781d-654b-4e7e-a698-00bddacfec2f")
            // 未启用 {
            // 未启用     FontSize = 17f;
            // 未启用 }
            // 未启用 else if (FontID == "66d27f44-5311-4abe-8508-e0599e6544db")
            // 未启用 {
            // 未启用     FontSize = 18f;
            // 未启用 }
            // 未启用 else if (FontID == "cd49a9a2-3eaf-4ec8-84dd-fe333f5ea28e")
            // 未启用 {
            // 未启用     FontSize = 19f;
            // 未启用 }

            // 未启用 labelCountdown.Font = new Font(labelCountdown.Font.FontFamily, FontSize, labelCountdown.Font.Style);

            if (!File.Exists(ConfigFile))
            {
                labelCountdown.ForeColor = System.Drawing.Color.Black;
                labelCountdown.Text = $"未检测到考试信息，请右键点击此处到设置里添加相应信息";
            }
            else if (DateTime.Now < TargetDateTime)
            {
                TimeSpan timeLeft = TargetDateTime - DateTime.Now;
                labelCountdown.ForeColor = System.Drawing.Color.Red;
                labelCountdown.Text = $"距离{ExamName}还有{timeLeft.Days}天{timeLeft.Hours:00}时{timeLeft.Minutes:00}分{timeLeft.Seconds:00}秒";
            }
            else if (DateTime.Now >= TargetDateTime && DateTime.Now < TargetDateTimeEnd)
            {
                TimeSpan timeLeftPast = TargetDateTimeEnd - DateTime.Now;
                labelCountdown.ForeColor = System.Drawing.Color.Green;
                labelCountdown.Text = $"{ExamName}正在进行中，距离结束还有{timeLeftPast.Days}天{timeLeftPast.Hours:00}时{timeLeftPast.Minutes:00}分{timeLeftPast.Seconds:00}秒";
            }
            else if (DateTime.Now >= TargetDateTimeEnd)
            {
                TimeSpan timePast = DateTime.Now - TargetDateTimeEnd;
                labelCountdown.ForeColor = System.Drawing.Color.Black;
                labelCountdown.Text = $"倒计时结束，距离{ExamName}已经过去了{timePast.Days}天{timePast.Hours:00}时{timePast.Minutes:00}分{timePast.Seconds:00}秒";
            }
        }

        private void CEETimerCSharpWinForms_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown)
            {
                Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void ContextAbout_Click(object sender, EventArgs e)
        {
            FormAbout formAbout = new FormAbout();
            formAbout.ShowDialog();
        }

        private void ContextSettings_Click(object sender, EventArgs e)
        {
            formSettings.LabelCountdown = labelCountdown;
            formSettings.MainForm = this;

            formSettings.xn = formSettings.n.ToString();
            formSettings.xy = formSettings.y.ToString();
            formSettings.xr = formSettings.r.ToString();
            formSettings.xs = formSettings.s.ToString();
            formSettings.xf = formSettings.f.ToString();
            formSettings.xm = formSettings.m.ToString();

            formSettings.xne = formSettings.ne.ToString();
            formSettings.xye = formSettings.ye.ToString();
            formSettings.xre = formSettings.re.ToString();
            formSettings.xse = formSettings.se.ToString();
            formSettings.xfe = formSettings.fe.ToString();
            formSettings.xme = formSettings.me.ToString();

            if (formSettings.ShowDialog() == DialogResult.OK)
            {
                TargetDateTime = formSettings.TargetDateTime;
                TargetDateTimeEnd = formSettings.TargetDateTimeEnd;
                ExamName = formSettings.ExamName;
            }
        }
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
                }
            }
        }

        public void WriteConfig()
        {
            string DateTimeLine = $"DateTime=({formSettings.n}, {formSettings.y}, {formSettings.r}, {formSettings.s}, {formSettings.f}, {formSettings.m})";
            string DateTimeLineEnd = $"DateTimeEnd=({formSettings.ne}, {formSettings.ye}, {formSettings.re}, {formSettings.se}, {formSettings.fe}, {formSettings.me})";
            string ExamNameLine = $"ExamName=[{formSettings.examName}]";
            string[] lines = new string[] { DateTimeLine, DateTimeLineEnd, ExamNameLine };
            File.WriteAllLines(ConfigFile, lines);
        }

        private void ContextOpenDir_Click(object sender, EventArgs e)
        {
            Process.Start(AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}