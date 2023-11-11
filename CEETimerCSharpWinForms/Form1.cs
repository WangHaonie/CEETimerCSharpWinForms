using CEETimerCSharpWinForms.Forms;
using System;
using System.IO;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms
{
    public partial class CEETimerCSharpWinForms : Form
    {
        private string ConfigFile = "CEETimerCSharpWinFormsConfig.db";
        private DateTime targetDateTime = new DateTime(2024, 6, 7, 9, 0, 0);
        private DateTime targetDateTimeEnd = new DateTime(2024, 6, 8, 17, 0, 0);
        private string examName = "高考";

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
            formSettings = new FormSettings(this);

            this.FormBorderStyle = FormBorderStyle.None;
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

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now < TargetDateTime)
            {
                TimeSpan timeLeft = TargetDateTime - DateTime.Now;
                labelCountdown.Text = $"距离{ExamName}还有{timeLeft.Days}天{timeLeft.Hours:00}时{timeLeft.Minutes:00}分{timeLeft.Seconds:00}秒";
            }
            else if (DateTime.Now >= TargetDateTime && DateTime.Now < TargetDateTimeEnd)
            {
                TimeSpan timeLeftPast = TargetDateTimeEnd - DateTime.Now;
                labelCountdown.Text = $"{ExamName}正在进行中，距离结束还有{timeLeftPast.Days}天{timeLeftPast.Hours:00}时{timeLeftPast.Minutes:00}分{timeLeftPast.Seconds:00}秒";
            }
            else if (DateTime.Now >= TargetDateTimeEnd)
            {
                TimeSpan timePast = DateTime.Now - TargetDateTimeEnd;
                labelCountdown.Text = $"倒计时结束，距离{ExamName}已经过去了{timePast.Days}天{timePast.Hours:00}时{timePast.Minutes:00}分{timePast.Seconds:00}秒";
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void CEETimerCSharpWinForms_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown)
            {
                Environment.Exit(0);
            }
            else
            {
                DialogResult result = MessageBox.Show("考试这么重要，你还想关闭倒计时？\n\n若此窗口真的妨碍到了你，你可以运行以下命令来关闭此窗口，谢谢。\n\ntaskkill /f /im CEETimerCSharpWinForms.exe (结束进程)", "错误：无法关闭此窗口 - 高考倒计时", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        private void CEETimerCSharpWinForms_DpiChanged(object sender, DpiChangedEventArgs e)
        {

        }

        private void ContextAbout_Click(object sender, EventArgs e)
        {
            FormAbout formAbout = new FormAbout();
            formAbout.Show();
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
    }
}