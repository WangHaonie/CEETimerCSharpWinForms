using System.Drawing;

namespace PlainCEETimer.Modules.Configuration
{
    public static class DefaultValues
    {
        public static ColorSetObject[] CountdownDefaultColors
        {
            get
            {
                if (field == null)
                {
                    field = [
                        new(Color.Red, Color.White),
                        new(Color.Green, Color.White),
                        new(Color.Black, Color.White),
                        new(Color.Black, Color.White)
                    ];
                }

                return field;
            }
        }

        public static Font CountdownDefaultFont
        {
            get
            {
                if (field == null)
                {
                    field = new("Microsoft YaHei", 17.25F, FontStyle.Bold, GraphicsUnit.Point);
                }

                return field;
            }
        }
    }
}
