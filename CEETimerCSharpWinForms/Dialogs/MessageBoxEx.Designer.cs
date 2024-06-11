namespace CEETimerCSharpWinForms.Dialogs
{
    partial class MessageBoxEx
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
            this.PanelHead = new System.Windows.Forms.Panel();
            this.LabelMessage = new System.Windows.Forms.Label();
            this.PicBoxIcon = new System.Windows.Forms.PictureBox();
            this.ButtonB = new System.Windows.Forms.Button();
            this.ButtonA = new System.Windows.Forms.Button();
            this.PanelHead.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBoxIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelHead
            // 
            this.PanelHead.AutoSize = true;
            this.PanelHead.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.PanelHead.Controls.Add(this.LabelMessage);
            this.PanelHead.Controls.Add(this.PicBoxIcon);
            this.PanelHead.Location = new System.Drawing.Point(0, 0);
            this.PanelHead.Name = "PanelHead";
            this.PanelHead.Size = new System.Drawing.Size(104, 44);
            this.PanelHead.TabIndex = 0;
            // 
            // LabelMessage
            // 
            this.LabelMessage.AutoSize = true;
            this.LabelMessage.Location = new System.Drawing.Point(48, 9);
            this.LabelMessage.Name = "LabelMessage";
            this.LabelMessage.Size = new System.Drawing.Size(53, 15);
            this.LabelMessage.TabIndex = 2;
            this.LabelMessage.Text = "Message";
            // 
            // PicBoxIcon
            // 
            this.PicBoxIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.PicBoxIcon.Location = new System.Drawing.Point(10, 9);
            this.PicBoxIcon.Name = "PicBoxIcon";
            this.PicBoxIcon.Size = new System.Drawing.Size(32, 32);
            this.PicBoxIcon.TabIndex = 1;
            this.PicBoxIcon.TabStop = false;
            // 
            // ButtonB
            // 
            this.ButtonB.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonB.Location = new System.Drawing.Point(124, 57);
            this.ButtonB.Name = "ButtonB";
            this.ButtonB.Size = new System.Drawing.Size(75, 23);
            this.ButtonB.TabIndex = 2;
            this.ButtonB.Text = "B";
            this.ButtonB.UseVisualStyleBackColor = true;
            this.ButtonB.Click += new System.EventHandler(this.ButtonB_Click);
            // 
            // ButtonA
            // 
            this.ButtonA.Location = new System.Drawing.Point(43, 57);
            this.ButtonA.Name = "ButtonA";
            this.ButtonA.Size = new System.Drawing.Size(75, 23);
            this.ButtonA.TabIndex = 3;
            this.ButtonA.Text = "A";
            this.ButtonA.UseVisualStyleBackColor = true;
            this.ButtonA.Click += new System.EventHandler(this.ButtonA_Click);
            // 
            // MessageBoxEx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(201, 83);
            this.Controls.Add(this.ButtonB);
            this.Controls.Add(this.ButtonA);
            this.Controls.Add(this.PanelHead);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MessageBoxEx";
            this.ShowIcon = false;
            this.Text = "MessageBoxEx";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MessageBoxEx_KeyDown);
            this.PanelHead.ResumeLayout(false);
            this.PanelHead.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBoxIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel PanelHead;
        private System.Windows.Forms.PictureBox PicBoxIcon;
        private System.Windows.Forms.Label LabelMessage;
        private System.Windows.Forms.Button ButtonB;
        private System.Windows.Forms.Button ButtonA;
    }
}