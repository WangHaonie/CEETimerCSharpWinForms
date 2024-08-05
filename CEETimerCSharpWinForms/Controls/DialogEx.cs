using CEETimerCSharpWinForms.Modules;
using System.Drawing;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Controls
{
    public abstract class DialogEx : AppForm
    {
        protected Panel PanelMain { get; private set; }
        protected Button ButtonB { get; private set; }
        protected Button ButtonA { get; private set; }

        private bool IsUserChanged;

        private DialogEx()
        {
            InitializeComponent();
        }

        protected DialogEx(DialogExProp Prop) : this()
        {
            SetProperties(Prop);
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

        protected void EnablePanelAutoSize(AutoSizeMode Mode)
        {
            PanelMain.AutoSize = true;
            PanelMain.AutoSizeMode = Mode;
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

        private void SetProperties(DialogExProp Prop)
        {
            if ((Prop & DialogExProp.BindButtons) != 0)
            {
                AcceptButton = ButtonA;
                CancelButton = ButtonB;
            }

            if ((Prop & DialogExProp.KeyPreview) != 0)
            {
                KeyPreview = true;
            }
        }

        private void KeepProperties()
        {
            AutoScaleDimensions = new(96F, 96F);
        }
    }
}
