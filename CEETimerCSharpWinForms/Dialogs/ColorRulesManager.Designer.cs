namespace CEETimerCSharpWinForms.Dialogs
{
    partial class ColorRulesManager
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
            this.components = new System.ComponentModel.Container();
            this.ContextMenuMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ContextAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.ListViewMain = new CEETimerCSharpWinForms.Controls.ListViewEx();
            this.ColumnRuleType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnExamTick = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnForeColor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnBackColor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LabelInfo = new System.Windows.Forms.Label();
            this.ContextMenuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContextMenuMain
            // 
            this.ContextMenuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ContextAdd,
            this.ToolStripSeparator1,
            this.ContextEdit,
            this.ContextDelete});
            this.ContextMenuMain.Name = "ContextMenuMain";
            this.ContextMenuMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.ContextMenuMain.Size = new System.Drawing.Size(117, 76);
            this.ContextMenuMain.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuMain_Opening);
            // 
            // ContextAdd
            // 
            this.ContextAdd.Name = "ContextAdd";
            this.ContextAdd.Size = new System.Drawing.Size(116, 22);
            this.ContextAdd.Text = "添加(&A)";
            this.ContextAdd.Click += new System.EventHandler(this.ContextAdd_Click);
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(113, 6);
            // 
            // ContextEdit
            // 
            this.ContextEdit.Name = "ContextEdit";
            this.ContextEdit.Size = new System.Drawing.Size(116, 22);
            this.ContextEdit.Text = "编辑(&E)";
            this.ContextEdit.Click += new System.EventHandler(this.ContextEdit_Click);
            // 
            // ContextDelete
            // 
            this.ContextDelete.Name = "ContextDelete";
            this.ContextDelete.Size = new System.Drawing.Size(116, 22);
            this.ContextDelete.Text = "删除(&D)";
            this.ContextDelete.Click += new System.EventHandler(this.ContextDelete_Click);
            // 
            // ListViewMain
            // 
            this.ListViewMain.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnRuleType,
            this.ColumnExamTick,
            this.ColumnForeColor,
            this.ColumnBackColor});
            this.ListViewMain.ContextMenuStrip = this.ContextMenuMain;
            this.ListViewMain.FullRowSelect = true;
            this.ListViewMain.GridLines = true;
            this.ListViewMain.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.ListViewMain.HideSelection = false;
            this.ListViewMain.Location = new System.Drawing.Point(6, 25);
            this.ListViewMain.Name = "ListViewMain";
            this.ListViewMain.Size = new System.Drawing.Size(358, 258);
            this.ListViewMain.TabIndex = 1;
            this.ListViewMain.UseCompatibleStateImageBehavior = false;
            this.ListViewMain.View = System.Windows.Forms.View.Details;
            this.ListViewMain.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.ListViewMain_ColumnWidthChanging);
            this.ListViewMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListViewMain_KeyDown);
            this.ListViewMain.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListViewMain_MouseDoubleClick);
            // 
            // ColumnRuleType
            // 
            this.ColumnRuleType.Text = "类别";
            this.ColumnRuleType.Width = 40;
            // 
            // ColumnExamTick
            // 
            this.ColumnExamTick.Text = "时刻";
            this.ColumnExamTick.Width = 40;
            // 
            // ColumnForeColor
            // 
            this.ColumnForeColor.Text = "文字颜色";
            this.ColumnForeColor.Width = 65;
            // 
            // ColumnBackColor
            // 
            this.ColumnBackColor.Text = "背景颜色";
            this.ColumnBackColor.Width = 65;
            // 
            // ButtonA
            // 
            this.ButtonA.Enabled = false;
            this.ButtonA.Location = new System.Drawing.Point(208, 289);
            this.ButtonA.Name = "ButtonA";
            this.ButtonA.Size = new System.Drawing.Size(75, 23);
            this.ButtonA.TabIndex = 5;
            this.ButtonA.Text = "确定(&O)";
            this.ButtonA.UseVisualStyleBackColor = true;
            // 
            // ButtonB
            // 
            this.ButtonB.Location = new System.Drawing.Point(289, 289);
            this.ButtonB.Name = "ButtonB";
            this.ButtonB.Size = new System.Drawing.Size(75, 23);
            this.ButtonB.TabIndex = 4;
            this.ButtonB.Text = "取消(&C)";
            this.ButtonB.UseVisualStyleBackColor = true;
            // 
            // LabelInfo
            // 
            this.LabelInfo.AutoSize = true;
            this.LabelInfo.Location = new System.Drawing.Point(6, 7);
            this.LabelInfo.Name = "LabelInfo";
            this.LabelInfo.Size = new System.Drawing.Size(332, 15);
            this.LabelInfo.TabIndex = 6;
            this.LabelInfo.Text = "自定义各时刻要显示的颜色，请右键表格进行操作。";
            // 
            // ColorRulesManager
            // 
            this.AcceptButton = this.ButtonA;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.ButtonB;
            this.ClientSize = new System.Drawing.Size(370, 317);
            this.Controls.Add(this.LabelInfo);
            this.Controls.Add(this.ListViewMain);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ColorRulesManager";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "颜色规则管理器 - 高考倒计时";
            this.ContextMenuMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip ContextMenuMain;
        private System.Windows.Forms.ToolStripMenuItem ContextAdd;
        private System.Windows.Forms.ToolStripMenuItem ContextDelete;
        private CEETimerCSharpWinForms.Controls.ListViewEx ListViewMain;
        private System.Windows.Forms.ColumnHeader ColumnRuleType;
        private System.Windows.Forms.ColumnHeader ColumnExamTick;
        private System.Windows.Forms.ColumnHeader ColumnForeColor;
        private System.Windows.Forms.ColumnHeader ColumnBackColor;
        private System.Windows.Forms.Label LabelInfo;
        private System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ContextEdit;
    }
}