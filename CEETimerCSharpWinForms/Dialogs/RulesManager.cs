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
    public partial class RulesManager : DialogEx
    {
        public List<TupleEx<int, TimeSpan, TupleEx<Color, Color, string>>> CustomRules { get; set; }
        public string[] Preferences { get; set; }
        public bool ShowWarning { get; set; }

        private bool IsEditMode;
        private IEnumerable<ListViewItem> GetAllItems() => ListViewMain.Items.Cast<ListViewItem>();

        public RulesManager() : base(true, true)
        {
            InitializeComponent();
        }

        protected override void OnDialogLoad()
        {
            if (CustomRules != null)
            {
                if (CustomRules.Count == 0)
                {
                    AddItem("文本测试", "65535天23时59分59秒", "255,255,255", "255,255,255", Placeholders.PH_P1);
                    AdjustColumnWidth();
                    DeleteAllItems();
                }
                else
                {
                    SuspendListView(() =>
                    {
                        foreach (var Rule in CustomRules)
                        {
                            var Part2 = Rule.Item3;
                            AddItem(Rule.Item1, CustomRuleHelper.GetExamTickText(Rule.Item2), Part2.Item1, Part2.Item2, Part2.Item3);
                        }
                    });
                }
            }
        }

        protected override void AdjustUI()
        {
            AlignControlsR(ButtonA, ButtonB, ListViewMain);
            CompactControlsY(ButtonA, ListViewMain, 3);
            CompactControlsY(ButtonB, ListViewMain, 3);
            AlignControlsL(LabelWarning, ButtonA, ListViewMain);
            LabelWarning.Visible = ShowWarning;
        }

        private void ContextAdd_Click(object sender, EventArgs e)
        {
            RuleDialog RuleDialogMain = new();

            if (ShowRuleDialog(RuleDialogMain) == DialogResult.OK)
            {
                AddItem(RuleDialogMain.RuleType, RuleDialogMain.ExamTick, RuleDialogMain.Fore, RuleDialogMain.Back, RuleDialogMain.CustomText);
            }
        }

        private void ContextEdit_Click(object sender, EventArgs e)
        {
            EditCustomRule(ListViewMain.SelectedItems[0]);
        }

        private void ListViewMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && ListViewMain.SelectedItems.Count != 0)
            {
                ContextDelete_Click(sender, e);
            }
            else if (e.Control && e.KeyCode == Keys.A)
            {
                SelectAllItems();
            }

            e.Handled = true;
        }

        private void ContextDelete_Click(object sender, EventArgs e)
        {
            if (MessageX.Popup("确认删除所选规则吗？此操作将不可撤销！", MessageLevel.Warning, Buttons: MessageBoxExButtons.YesNo) == DialogResult.Yes)
            {
                foreach (ListViewItem Item in ListViewMain.SelectedItems)
                {
                    DeleteItem(Item);
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
                EditCustomRule(ListViewMain.GetItemAt(e.X, e.Y));
            }
        }

        protected override void OnButtonAClicked()
        {
            CustomRules = GetAllItems().Select(Item => new TupleEx<int, TimeSpan, TupleEx<Color, Color, string>>
            (
                CustomRuleHelper.GetRuleTypeIndex(Item.SubItems[0].Text),
                CustomRuleHelper.GetExamTick(Item.SubItems[1].Text),
                new(
                    ColorHelper.GetColor(Item.SubItems[2].Text),
                    ColorHelper.GetColor(Item.SubItems[3].Text),
                    Item.SubItems[4].Text
                   )
            )).ToList();

            base.OnButtonAClicked();
        }

        private void AddItem(int RuleTypeIndex, string ExamTick, Color Fore, Color Back, string CustomText, ListViewItem Item = null)
        {
            UserChanged();

            var RuleTypeText = CustomRuleHelper.GetRuleTypeText(RuleTypeIndex);
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

            AddItem(RuleTypeText, ExamTick, _Fore, _Back, CustomText);
            IDontKnowWhatToNameThis();

            void ModifyOrOverrideItem(ListViewItem Item)
            {
                Item.SubItems[0].Text = RuleTypeText;
                Item.SubItems[1].Text = ExamTick;
                Item.SubItems[2].Text = _Fore;
                Item.SubItems[3].Text = _Back;
                Item.SubItems[4].Text = CustomText;
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

        private void EditCustomRule(ListViewItem Item)
        {
            IsEditMode = true;

            RuleDialog RuleDialogMain = new()
            {
                RuleType = CustomRuleHelper.GetRuleTypeIndex(Item.SubItems[0].Text),
                ExamTick = Item.SubItems[1].Text,
                Fore = ColorHelper.TryParseRGB(Item.SubItems[2].Text, out Color color1) ? color1 : Color.White,
                Back = ColorHelper.TryParseRGB(Item.SubItems[3].Text, out Color color2) ? color2 : Color.White,
                CustomText = Item.SubItems[4].Text
            };

            if (ShowRuleDialog(RuleDialogMain) == DialogResult.OK)
            {
                AddItem(RuleDialogMain.RuleType, RuleDialogMain.ExamTick, RuleDialogMain.Fore, RuleDialogMain.Back, RuleDialogMain.CustomText, Item);
                RemoveDuplicates();
            }

            IsEditMode = false;
        }

        private DialogResult ShowRuleDialog(RuleDialog Dialog)
        {
            Dialog.CustomTextPreferences = Preferences;
            return Dialog.ShowDialog();
        }

        private void AdjustColumnWidth()
        {
            foreach (ColumnHeader column in ListViewMain.Columns)
            {
                column.Width = -2;
            }
        }

        private void SuspendListView(Action Operation)
        {
            ListViewMain.BeginUpdate();
            Operation();
            ListViewMain.EndUpdate();
        }

        private void RemoveDuplicates()
        {
            var Uniques = new HashSet<string>();
            var Duplicates = new List<ListViewItem>();

            for (int i = 0; i < GetAllItems().Count(); i++)
            {
                var CurrentItem = ListViewMain.Items[i];
                string CurrentSubItemText = CurrentItem.SubItems[0].Text + CurrentItem.SubItems[1].Text;

                if (!Uniques.Add(CurrentSubItemText))
                {
                    Duplicates.Add(CurrentItem);
                }
            }

            foreach (var Item in Duplicates)
            {
                DeleteItem(Item);
            }
        }

        private void SelectAllItems()
        {
            var All = GetAllItems();

            if (All.Count() != 0)
            {
                foreach (var Item in All)
                {
                    Item.Selected = true;
                }
            }
        }

        private void SortItems()
        {
            List<ListViewItem> Items = GetAllItems().ToList();
            Items.Sort(new ListViewItemComparer());

            SuspendListView(() =>
            {
                DeleteAllItems();
                AddItems(Items);
            });
        }

        private void AddItem(string Column1, string Column2, string Column3, string Column4, string Column5)
        {
            ListViewMain.Items.Add(new ListViewItem([Column1, Column2, Column3, Column4, Column5]));
        }

        private void AddItems(IEnumerable<ListViewItem> Items)
        {
            ListViewMain.Items.AddRange([.. Items]);
        }

        private void DeleteItem(ListViewItem Item)
        {
            ListViewMain.Items.Remove(Item);
        }

        private void DeleteAllItems()
        {
            ListViewMain.Items.Clear();
        }
    }
}
