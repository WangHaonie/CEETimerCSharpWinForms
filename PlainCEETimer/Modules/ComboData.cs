namespace PlainCEETimer.Modules
{
    public class ComboData(string display, int value)
    {
        public string Display { get; set; } = display;
        public int Value { get; set; } = value;
    }
}