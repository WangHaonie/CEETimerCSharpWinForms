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
            TopMost = FormMain.IsUniTopMost;
            Controls.AddRange([ButtonA, ButtonB]);
            ResumeLayout(false);
            Load += (sender, e) => OnDialogLoad();
            Shown += (sender, e) => IsDialogLoading = false;
            ButtonA.Click += (sender, e) => OnButtonAClicked();
            ButtonB.Click += (sender, e) => OnButtonBClicked();
            FormClosing += (sender, e) => OnDialogClosing(e);
        }

        protected abstract void OnDialogLoad();

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

        protected void UserChanged()
        {
            if (!IsDialogLoading && !ButtonA.Enabled)
            {
                IsUserChanged = true;
                ButtonA.Enabled = true;
            }
        }

        protected void Execute(Action action)
        {
            if (!IsDialogLoading)
            {
                action.Invoke();
            }
        }

        protected virtual void OnDialogClosing(FormClosingEventArgs e)
        {
            if (IsUserChanged)
            {
                switch (MessageX.Popup("是否保存当前更改？", MessageLevel.Warning, Buttons: MessageBoxExButtons.YesNo))
                {
                    case DialogResult.Yes:
                        e.Cancel = true;
                        OnButtonAClicked();
                        break;
                    case DialogResult.None:
                        e.Cancel = true;
                        break;
                    default:
                        IsUserChanged = false;
                        Close();
                        break;
                }
            }
        }
    }
}
