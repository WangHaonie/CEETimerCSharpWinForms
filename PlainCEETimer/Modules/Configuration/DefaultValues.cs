using System.Drawing;

namespace PlainCEETimer.Modules.Configuration
{
    public static class DefaultValues
    {
        public static ColorSetObject[] CountdownDefaultColorsLight
        {
            get
            {
                field ??= [
                    new(Color.Red, Color.White),
                    new(Color.Green, Color.White),
                    new(Color.Black, Color.White),
                    new(Color.Black, Color.White)];

                return field;
            }
        }

        public static ColorSetObject[] CountdownDefaultColorsDark
        {
            get
            {
                field ??= [
                    new(Color.Red, Color.Black),
                    new(Color.Lime, Color.Black),
                    new(Color.White, Color.Black),
                    new(Color.White, Color.Black)];

                return field;
            }
        }

        public static Font CountdownDefaultFont
        {
            get
            {
                field ??= new("Microsoft YaHei", 17.25F, FontStyle.Bold, GraphicsUnit.Point);
                return field;
            }
        }
    }
}
