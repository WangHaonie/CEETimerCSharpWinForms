namespace CEETimerCSharpWinForms.Modules
{
    public class TupleEx<TItem1, TItem2>(TItem1 item1, TItem2 item2)
    {
        public TItem1 Item1 { get; set; } = item1;
        public TItem2 Item2 { get; set; } = item2;
    }

    public class TupleEx<TItem1, TItem2, TItem3>(TItem1 item1, TItem2 item2, TItem3 item3)
    {
        public TItem1 Item1 { get; set; } = item1;
        public TItem2 Item2 { get; set; } = item2;
        public TItem3 Item3 { get; set; } = item3;
    }
}