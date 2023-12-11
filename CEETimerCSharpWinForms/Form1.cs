using System;
using System.Diagnostics;
using System.Windows.Forms;
using CEETimerCSharpWinForms.Forms;

namespace CEETimerCSharpWinForms
{
    public partial class CEETimerCSharpWinForms : Form
    {
        private string ConfigFile = AppDomain.CurrentDomain.BaseDirectory + "CEETimerCSharpWinFormsConfig.db";
        private DateTime targetDateTime = new DateTime();
        private DateTime targetDateTimeEnd = new DateTime();
        private string examName = "";
        private System.Windows.Forms.Timer timer;
        private FormSettings formSettings;
        // 功能 更改倒计时字体大小 相关代码 string fontSize = "17";
        // 功能 更改倒计时字体大小 相关代码 public string FontSize
        // 功能 更改倒计时字体大小 相关代码 {
        // 功能 更改倒计时字体大小 相关代码 get { return fontSize; }
        // 功能 更改倒计时字体大小 相关代码 set { fontSize = value; }
        // 功能 更改倒计时字体大小 相关代码 }

        public CEETimerCSharpWinForms()
        {
            InitializeComponent();
            formSettings = new FormSettings();
            // 功能 更改倒计时字体大小 相关代码 Label label = new Label();
            // 功能 更改倒计时字体大小 相关代码 string fontSize = labelCountdown.Font.Size.ToString();
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
            FormSettings formSettings = new FormSettings();
            //string FontID = formSettings.FontId;
            //fontSize = "17";
            //if (FontID == "57c1228f-bb20-4ef1-ab63-3907b9ec8b63")
            //{
            //    fontSize = "15";
            //}
            //else if (FontID == "b994bf0b-48c1-4675-aa5a-b166ab27ffd1")
            //{
            //    fontSize = "16";
            //}
            //else if (FontID == "2412781d-654b-4e7e-a698-00bddacfec2f")
            //{
            //    fontSize = "17";
            //}
            //else if (FontID == "66d27f44-5311-4abe-8508-e0599e6544db")
            //{
            //    fontSize = "18";
            //}
            //else if (FontID == "cd49a9a2-3eaf-4ec8-84dd-fe333f5ea28e")
            //{
            //    fontSize = "19";
            //}

            // 功能 更改倒计时字体大小 相关代码 labelCountdown.Font = new Font(labelCountdown.Font.FontFamily, float.Parse(fontSize), labelCountdown.Font.Style);

            if (!isValidConfigDate())
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
        private void ContextOpenDir_Click(object sender, EventArgs e)
        {
            Process.Start(AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}