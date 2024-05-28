namespace CEETimerCSharpWinForms.Modules
{
    public class PairItems<TItem1, TItem2>(TItem1 item1, TItem2 item2)
    {
        public TItem1 Item1 { get; set; } = item1;
        public TItem2 Item2 { get; set; } = item2;
    }
}