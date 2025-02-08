using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PlainCEETimer.Modules
{
    public class ListViewItemComparer : IComparer<ListViewItem>
    {
        public int Compare(ListViewItem x, ListViewItem y)
        {
            int FirstComparison = CompareFirstColumn(x.SubItems[0].Text, y.SubItems[0].Text);

            if (FirstComparison != 0)
            {
                return FirstComparison;
            }

            int SecondComparison = CompareSecondColumn(x.SubItems[1].Text, y.SubItems[1].Text);

            if (x.SubItems[0].Text == Placeholders.PH_PAST)
            {
                return -SecondComparison;
            }

            return SecondComparison;
        }

        private int CompareFirstColumn(string x, string y)
        {
            var Order = new Dictionary<string, int> {
                { Placeholders.PH_START, 1 },
                { Placeholders.PH_LEFT, 2 },
                { Placeholders.PH_PAST, 3 }
            };
            return Order[x].CompareTo(Order[y]);
        }

        private int CompareSecondColumn(string x, string y)
        {
            TimeSpan tsx = CustomRuleHelper.GetExamTick(x);
            TimeSpan tsy = CustomRuleHelper.GetExamTick(y);
            return tsy.CompareTo(tsx);
        }
    }
}
