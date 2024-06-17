using System.Drawing;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Controls
{
    public abstract class DialogEx : Form
    {
        protected Panel PanelMain;
        protected Button ButtonB;
        protected Button ButtonA;
        protected bool IsDialogLoading { get; private set; }

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
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
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
            e.Cancel = false;
        }
    }
}
