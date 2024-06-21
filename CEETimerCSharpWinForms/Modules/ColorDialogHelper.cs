using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Modules
{
    public class ColorDialogHelper : IDisposable
    {
        public Color SelectedColor { get; set; }
        public static int[] CustomColorCollection { get; set; }

        private int[] PreviousCustomColors;
        private readonly ColorDialog _ColorDialog;
        private bool Disposed;

        public ColorDialogHelper(Color color)
        {
            _ColorDialog = new()
            {
                AllowFullOpen = true,
                Color = color,
                FullOpen = true,
                CustomColors = CustomColorCollection
            };
        }

        ~ColorDialogHelper()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool Disposing)
        {
            if (!Disposed)
            {
                if (Disposing)
                {
                    _ColorDialog.Dispose();
                }

                Disposed = true;
            }
        }

        public DialogResult ShowDialog()
        {
            PreviousCustomColors = CustomColorCollection;
            var _DialogResult = _ColorDialog.ShowDialog();

            if (_DialogResult == DialogResult.OK)
            {
                CustomColorCollection = _ColorDialog.CustomColors;
                SelectedColor = _ColorDialog.Color;
                SaveCustomColors();
            }

            return _DialogResult;
        }

        private void SaveCustomColors()
        {
            if (CustomColorCollection != null && PreviousCustomColors != null && !CustomColorCollection.SequenceEqual(PreviousCustomColors))
            {
                new ConfigManager().WriteConfig(new()
                {
                    { ConfigItems.KCustomColors, ColorHelper.GetStringFromArgbArray(CustomColorCollection) }
                });
            }
        }
    }
}
