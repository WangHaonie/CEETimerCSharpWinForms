using System.Drawing;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Controls
{
    public abstract class DialogEx : AppForm
    {
        protected Panel PanelMain { get; set; }
        protected Button ButtonB { get; set; }
        protected Button ButtonA { get; set; }

        private bool IsUserChanged;

        private DialogEx()
        {
            InitializeComponent();
        }

        protected DialogEx(bool BindButtons, bool ReadKeys) : this()
        {
            if (BindButtons)
            {
                AcceptButton = ButtonA;
                CancelButton = ButtonB;
            }

            if (ReadKeys)
            {
                KeyPreview = true;
            }
        }

        private void InitializeComponent()
        {
            SuspendLayout();

            PanelMain = new();
            ButtonA = new();
            ButtonB = new();

            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            DoubleBuffered = true;
            Font = new("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new(4);
            MaximizeBox = false;
            MinimizeBox = false;
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;

            Controls.AddRange([ButtonA, ButtonB]);

            ButtonA.Click += (sender, e) => OnButtonAClicked();
            ButtonB.Click += (sender, e) => OnButtonBClicked();

            ResumeLayout(false);
        }

        protected sealed override void OnAppFormLoad()
        {
            KeepProperties();
            OnDialogLoad();
        }

        protected sealed override void OnAppFormShown()
        {
            OnDialogShown();
        }

        protected sealed override void OnAppFormClosing(FormClosingEventArgs e)
        {
            OnDialogClosing(e);
        }

        protected abstract void OnDialogLoad();

        protected virtual void OnDialogShown() { }

        protected void AdjustPanel()
        {
            AlignControlsR(ButtonA, ButtonB, PanelMain);
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
                ShowUnsavedWarning("是否保存当前更改？", e, OnButtonAClicked, () =>
                {
                    IsUserChanged = false;
                    Close();
                });
            }
        }

        protected void UserChanged()
        {
            Execute(() =>
            {
                if (!ButtonA.Enabled)
                {
                    IsUserChanged = true;
                    ButtonA.Enabled = true;
                }
            });
        }

        private void KeepProperties() // .NET 貌似会在初始化子类的时候重置基类的某些属性，故该方法可以在重置之后再次应用相关属性
        {
            AutoScaleDimensions = new(96F, 96F);
        }
    }
}
