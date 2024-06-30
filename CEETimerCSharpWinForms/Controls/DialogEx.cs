using CEETimerCSharpWinForms.Forms;
using CEETimerCSharpWinForms.Modules;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Controls
{
    public abstract class DialogEx : Form
    {
        protected Panel PanelMain;
        protected Button ButtonB;
        protected Button ButtonA;
        private bool IsDialogLoading;
        private bool IsUserChanged;

        public DialogEx()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            SuspendLayout();

            IsDialogLoading = true;
            PanelMain = new();
            ButtonA = new();
            ButtonB = new();

            AutoScaleDimensions = new(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            DoubleBuffered = true;
            Font = new("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            TopMost = MainForm.IsUniTopMost;
            Controls.AddRange([ButtonA, ButtonB]);

            ButtonA.Click += (sender, e) => OnButtonAClicked();
            ButtonB.Click += (sender, e) => OnButtonBClicked();

            ResumeLayout(false);
        }

        protected override void OnLoad(EventArgs e)
        {
            OnDialogLoad();
            AdjustUI();
            base.OnLoad(e);
        }

        protected override void OnShown(EventArgs e)
        {
            OnDialogShown();
            base.OnShown(e);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            OnDialogClosing(e);
            base.OnFormClosing(e);
        }

        protected abstract void OnDialogLoad();

        protected virtual void OnDialogShown()
        {
            IsDialogLoading = false;
        }

        protected virtual void AdjustUI()
        {
            UIHelper.AlignControlsR(ButtonA, ButtonB, PanelMain);
        }

        protected virtual void OnButtonAClicked()
        {
            IsUserChanged = false;
            DialogResult = DialogResult.OK;
            Close();
        }

        protected virtual void OnButtonBClicked()
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        protected virtual void OnDialogClosing(FormClosingEventArgs e)
        {
            if (IsUserChanged)
            {
                UIHelper.ShowUserChangedWarning("是否保存当前更改？", e, OnButtonAClicked, () =>
                {
                    IsUserChanged = false;
                    Close();
                });
            }
        }

        protected void UserChanged()
        {
            if (!IsDialogLoading && !ButtonA.Enabled)
            {
                IsUserChanged = true;
                ButtonA.Enabled = true;
            }
        }

        protected void Execute(Action Method)
        {
            if (!IsDialogLoading)
            {
                Method();
            }
        }
    }
}
