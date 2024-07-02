namespace CEETimerCSharpWinForms.Dialogs
{
    partial class CustomTextDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TextBoxP2 = new System.Windows.Forms.TextBox();
            this.LabelP3 = new System.Windows.Forms.Label();
            this.TextBoxP3 = new System.Windows.Forms.TextBox();
            this.LabelP2 = new System.Windows.Forms.Label();
            this.TextBoxP1 = new System.Windows.Forms.TextBox();
            this.LabelP1 = new System.Windows.Forms.Label();
            this.LabelInfo = new System.Windows.Forms.Label();
            //this.PanelMain = new System.Windows.Forms.Panel();
            //this.ButtonA = new System.Windows.Forms.Button();
            //this.ButtonB = new System.Windows.Forms.Button();
            this.ButtonReset = new System.Windows.Forms.Button();
            this.PanelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // TextBoxP2
            // 
            this.TextBoxP2.Location = new System.Drawing.Point(84, 106);
            this.TextBoxP2.Name = "TextBoxP2";
            this.TextBoxP2.Size = new System.Drawing.Size(220, 23);
            this.TextBoxP2.TabIndex = 12;
            // 
            // LabelP3
            // 
            this.LabelP3.AutoSize = true;
            this.LabelP3.Location = new System.Drawing.Point(6, 138);
            this.LabelP3.Name = "LabelP3";
            this.LabelP3.Size = new System.Drawing.Size(72, 15);
            this.LabelP3.TabIndex = 11;
            this.LabelP3.Text = "考试已结束";
            // 
            // TextBoxP3
            // 
            this.TextBoxP3.Location = new System.Drawing.Point(84, 135);
            this.TextBoxP3.Name = "TextBoxP3";
            this.TextBoxP3.Size = new System.Drawing.Size(220, 23);
            this.TextBoxP3.TabIndex = 11;
            // 
            // LabelP2
            // 
            this.LabelP2.AutoSize = true;
            this.LabelP2.Location = new System.Drawing.Point(6, 109);
            this.LabelP2.Name = "LabelP2";
            this.LabelP2.Size = new System.Drawing.Size(72, 15);
            this.LabelP2.TabIndex = 10;
            this.LabelP2.Text = "考试已开始";
            // 
            // TextBoxP1
            // 
            this.TextBoxP1.Location = new System.Drawing.Point(84, 77);
            this.TextBoxP1.Name = "TextBoxP1";
            this.TextBoxP1.Size = new System.Drawing.Size(220, 23);
            this.TextBoxP1.TabIndex = 10;
            // 
            // LabelP1
            // 
            this.LabelP1.AutoSize = true;
            this.LabelP1.Location = new System.Drawing.Point(6, 80);
            this.LabelP1.Name = "LabelP1";
            this.LabelP1.Size = new System.Drawing.Size(72, 15);
            this.LabelP1.TabIndex = 9;
            this.LabelP1.Text = "考试未开始";
            // 
            // LabelInfo
            // 
            this.LabelInfo.AutoSize = true;
            this.LabelInfo.Location = new System.Drawing.Point(6, 6);
            this.LabelInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelInfo.Name = "LabelInfo";
            this.LabelInfo.Size = new System.Drawing.Size(284, 60);
            this.LabelInfo.TabIndex = 7;
            this.LabelInfo.Text = "自定义倒计时的内容，用占位符表示变量:\n{x}-考试名称 {d}-天 {h}-时 {m}-分 {s}-秒 {rd}-四舍五入的天数 {th}-总小时数 {tm}-总分钟数 {ts}-总秒数。\n比如 \"{x}还有{d}.{h}:{m}:{s}\"。";
            // 
            // PanelMain
            // 
            this.PanelMain.Controls.Add(this.TextBoxP2);
            this.PanelMain.Controls.Add(this.LabelP3);
            this.PanelMain.Controls.Add(this.TextBoxP3);
            this.PanelMain.Controls.Add(this.LabelP2);
            this.PanelMain.Controls.Add(this.TextBoxP1);
            this.PanelMain.Controls.Add(this.LabelP1);
            this.PanelMain.Controls.Add(this.LabelInfo);
            this.PanelMain.Location = new System.Drawing.Point(0, 0);
            this.PanelMain.Margin = new System.Windows.Forms.Padding(4);
            this.PanelMain.Name = "PanelMain";
            this.PanelMain.Size = new System.Drawing.Size(305, 156);
            this.PanelMain.TabIndex = 0;
            // 
            // ButtonA
            // 
            this.ButtonA.Enabled = false;
            this.ButtonA.Location = new System.Drawing.Point(148, 170);
            this.ButtonA.Name = "ButtonA";
            this.ButtonA.Size = new System.Drawing.Size(75, 23);
            this.ButtonA.TabIndex = 8;
            this.ButtonA.Text = "确定(O)";
            this.ButtonA.UseVisualStyleBackColor = true;
            // 
            // ButtonB
            // 
            this.ButtonB.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonB.Location = new System.Drawing.Point(229, 170);
            this.ButtonB.Name = "ButtonB";
            this.ButtonB.Size = new System.Drawing.Size(75, 23);
            this.ButtonB.TabIndex = 9;
            this.ButtonB.Text = "取消(&C)";
            this.ButtonB.UseVisualStyleBackColor = true;
            // 
            // ButtonReset
            // 
            this.ButtonReset.Location = new System.Drawing.Point(2, 172);
            this.ButtonReset.Name = "ButtonReset";
            this.ButtonReset.Size = new System.Drawing.Size(75, 23);
            this.ButtonReset.TabIndex = 10;
            this.ButtonReset.Text = "重置(R)";
            this.ButtonReset.UseVisualStyleBackColor = true;
            this.ButtonReset.Click += new System.EventHandler(this.ButtonReset_Click);
            // 
            // CustomTextDialog
            // 
            this.ClientSize = new System.Drawing.Size(312, 197);
            this.Controls.Add(this.ButtonReset);
            this.Controls.Add(this.PanelMain);
            this.Name = "CustomTextDialog";
            this.Text = "自定义文本 - 高考倒计时";
            this.PanelMain.ResumeLayout(false);
            this.PanelMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        //private System.Windows.Forms.Panel PanelMain;
        //private System.Windows.Forms.Button ButtonA;
        //private System.Windows.Forms.Button ButtonB;
        private System.Windows.Forms.Label LabelInfo;
        private System.Windows.Forms.TextBox TextBoxP2;
        private System.Windows.Forms.Label LabelP3;
        private System.Windows.Forms.TextBox TextBoxP3;
        private System.Windows.Forms.Label LabelP2;
        private System.Windows.Forms.TextBox TextBoxP1;
        private System.Windows.Forms.Label LabelP1;
        private System.Windows.Forms.Button ButtonReset;
    }
}