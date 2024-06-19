using CEETimerCSharpWinForms.Controls;
using CEETimerCSharpWinForms.Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Dialogs
{
    public partial class ColorRulesManager : DialogEx
    {
        public List<PairItems<PairItems<int, TimeSpan>, PairItems<Color, Color>>> ColorRules { get; set; }

        private bool IsEditMode;
        private ListView.ListViewItemCollection GetAllItems() => ListViewMain.Items;

        public ColorRulesManager()
        {
            InitializeComponent();
            Controls.Remove(PanelMain);
        }

        protected override void OnDialogLoad()
        {
            if (ColorRules != null)
            {
                if (ColorRules.Count == 0)
                {
                    ListViewMain.Items.Add(new ListViewItem(["文本测试", "65535天23时59分59秒", "255,255,255", "255,255,255"]));
                    AdjustColumnWidth(); // 触发一次自适应宽度，防止 ListView 为空时所有列在高 DPI 下糊为一坨
                    ListViewMain.Items.Clear();
                }
                else
                {
                    foreach (var Rule in ColorRules)
                    {
                        var Part1 = Rule.Item1;
                        var Part2 = Rule.Item2;
                        AddListViewItem(Part1.Item1, ColorRulesHelper.GetExamTickText(Part1.Item2), Part2.Item1, Part2.Item2);
                    }
                }
            }

            UIHelper.AlignControls(ButtonA, ButtonB, ListViewMain);
            UIHelper.CompactControlsY(ButtonA, ListViewMain, 3);
            UIHelper.CompactControlsY(ButtonB, ListViewMain, 3);
        }

        private void ContextAdd_Click(object sender, EventArgs e)
        {
            ColorRuleDialog _ColorRuleDialog = new();

            if (_ColorRuleDialog.ShowDialog() == DialogResult.OK)
            {
                AddListViewItem(_ColorRuleDialog.RuleType, _ColorRuleDialog.ExamTick, _ColorRuleDialog.Fore, _ColorRuleDialog.Back);
            }
        }

        private void ContextEdit_Click(object sender, EventArgs e)
        {
            EditColorRule(ListViewMain.SelectedItems[0]);
        }

        private void ListViewMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && ListViewMain.SelectedItems.Count != 0)
            {
                ContextDelete_Click(sender, e);
            }
        }

        private void ContextDelete_Click(object sender, EventArgs e)
        {
            if (MessageX.Popup("确认删除所选规则吗？此操作将不可撤销！", MessageLevel.Warning, Buttons: MessageBoxExButtons.YesNo) == DialogResult.Yes)
            {
                foreach (ListViewItem Item in ListViewMain.SelectedItems)
                {
                    ListViewMain.Items.Remove(Item);
                }

                UserChanged();
            }
        }

        private void ContextMenuMain_Opening(object sender, CancelEventArgs e)
        {
            ContextDelete.Enabled = ListViewMain.SelectedItems.Count != 0;
            ContextEdit.Enabled = ListViewMain.SelectedItems.Count == 1;
        }

        private void ListViewMain_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.NewWidth = ((ListView)sender).Columns[e.ColumnIndex].Width;
            e.Cancel = true;
        }

        private void ListViewMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && ListViewMain.HitTest(e.X, e.Y).Item != null && ListViewMain.SelectedItems.Count == 1)
            {
                EditColorRule(ListViewMain.GetItemAt(e.X, e.Y));
            }
        }

        protected override void OnButtonAClicked()
        {
            ColorRules = [];

            foreach (ListViewItem Item in GetAllItems())
            {
                ColorRules.Add(new(new(ColorRulesHelper.GetRuleTypeIndex(Item.SubItems[0].Text), ColorRulesHelper.GetExamTick(Item.SubItems[1].Text)), new(ColorHelper.GetColor(Item.SubItems[2].Text), ColorHelper.GetColor(Item.SubItems[3].Text))));
            }

            base.OnButtonAClicked();
        }

        private void AddListViewItem(int RuleTypeIndex, string ExamTick, Color Fore, Color Back, ListViewItem Item = null)
        {
            UserChanged();

            var RuleTypeText = ColorRulesHelper.GetRuleTypeText(RuleTypeIndex);
            var _Fore = Fore.ToRgb();
            var _Back = Back.ToRgb();

            if (!IsEditMode)
            {
                var Duplicate = GetDuplicate(RuleTypeText, ExamTick);

                if (Duplicate != null)
                {
                    Execute(() =>
                    {
                        if (MessageX.Popup("检测到即将添加的规则与现有的重复，是否覆盖？", MessageLevel.Warning, Buttons: MessageBoxExButtons.YesNo) == DialogResult.Yes)
                        {
                            ModifyOrOverrideItem(Duplicate);
                            return;
                        }
                    });

                    return;
                }
            }

            if (Item != null)
            {
                ModifyOrOverrideItem(Item);
                return;
            }

            ListViewMain.Items.Add(new ListViewItem([$"{RuleTypeText}", $"{ExamTick}", $"{_Fore}", $"{_Back}"]));
            IDontKnowWhatToNameThis();

            void ModifyOrOverrideItem(ListViewItem Item)
            {
                Item.SubItems[0].Text = $"{RuleTypeText}";
                Item.SubItems[1].Text = $"{ExamTick}";
                Item.SubItems[2].Text = $"{_Fore}";
                Item.SubItems[3].Text = $"{_Back}";
                IDontKnowWhatToNameThis();
            }

            void IDontKnowWhatToNameThis()
            {
                AdjustColumnWidth();
                SortItems();
            }
        }

        private ListViewItem GetDuplicate(string Column1Text, string Column2Text)
        {
            foreach (ListViewItem Item in GetAllItems())
            {
                if (Item.SubItems[0].Text == Column1Text && Item.SubItems[1].Text == Column2Text)
                {
                    return Item;
                }
            }

            return null;
        }

        private void RemoveDuplicate()
        {
            List<ListViewItem> ItemsToRemove = [];

            for (int i = 0; i < ListViewMain.Items.Count; i++)
            {
                for (int j = i + 1; j < ListViewMain.Items.Count; j++)
                {
                    if (ListViewMain.Items[i].SubItems[0].Text == ListViewMain.Items[j].SubItems[0].Text &&
                        ListViewMain.Items[i].SubItems[1].Text == ListViewMain.Items[j].SubItems[1].Text)
                    {
                        ItemsToRemove.Add(ListViewMain.Items[i]);
                        break;
                    }
                }
            }

            foreach (var item in ItemsToRemove)
            {
                ListViewMain.Items.Remove(item);
            }
        }

        private void EditColorRule(ListViewItem Item)
        {
            IsEditMode = true;

            ColorRuleDialog _ColorRuleDialog = new()
            {
                RuleType = ColorRulesHelper.GetRuleTypeIndex(Item.SubItems[0].Text),
                ExamTick = Item.SubItems[1].Text,
                Fore = ColorHelper.TryParseRGB(Item.SubItems[2].Text, out Color color1) ? color1 : Color.White,
                Back = ColorHelper.TryParseRGB(Item.SubItems[3].Text, out Color color2) ? color2 : Color.White
            };

            if (_ColorRuleDialog.ShowDialog() == DialogResult.OK)
            {
                AddListViewItem(_ColorRuleDialog.RuleType, _ColorRuleDialog.ExamTick, _ColorRuleDialog.Fore, _ColorRuleDialog.Back, Item);
                RemoveDuplicate();
            }

            IsEditMode = false;
        }

        private void AdjustColumnWidth()
        {
            foreach (ColumnHeader column in ListViewMain.Columns)
            {
                column.Width = -2;
            }
        }

        private void SortItems()
        {
            List<ListViewItem> Items = ListViewMain.Items.Cast<ListViewItem>().ToList();
            Items.Sort(new ListViewItemComparer());
            ListViewMain.BeginUpdate();
            ListViewMain.Items.Clear();
            ListViewMain.Items.AddRange([.. Items]);
            ListViewMain.EndUpdate();
        }
    }
}
