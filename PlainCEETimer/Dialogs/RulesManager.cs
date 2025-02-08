﻿using PlainCEETimer.Controls;
using PlainCEETimer.Modules;
using PlainCEETimer.Modules.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PlainCEETimer.Dialogs
{
    public partial class RulesManager : DialogEx
    {
        public RulesManagerObject[] CustomRules { get; set; }
        public string[] Preferences { get; set; }
        public bool ShowWarning { get; set; }

        private ContextMenu ContextMenuMain;
        private MenuItem ContextDelete;
        private string LastText;
        private readonly Dictionary<int, string> UserUnsavedText = [];

        public RulesManager() : base(DialogExProp.BindButtons | DialogExProp.KeyPreview)
        {
            CompositedStyle = true;
            InitializeComponent();
            InitializeExtra();
        }

        private void InitializeExtra()
        {
            ContextMenuMain = CreateNew
            ([
                ContextDelete = AddItem("删除(&D)", (sender, e) =>
                {
                    if (MessageX.Warn("确认删除所选规则吗？此操作将不可撤销！", Buttons: MessageBoxExButtons.YesNo) == DialogResult.Yes)
                    {
                        foreach (ListViewItem Item in GetSelections())
                        {
                            DeleteItem(Item);
                        }

                        UserChanged();
                    }
                })
            ]);

            ListViewMain.ContextMenu = ContextMenuMain;
            ListViewMain.MouseDown += ListViewMain_MouseUpDown;
            ListViewMain.MouseUp += ListViewMain_MouseUpDown;
            ListViewMain.ColumnWidthChanging += ListViewMain_ColumnWidthChanging;
            ListViewMain.SelectedIndexChanged += ListViewMain_SelectedIndexChanged;

            BindComboData(ComboBoxRuleType,
            [
                new(Placeholders.PH_START, 0),
                new(Placeholders.PH_LEFT, 1),
                new(Placeholders.PH_PAST, 2)
            ]);

            ComboBoxRuleType.SelectedIndexChanged += ComboBoxRuleType_SelectedIndexChanged;
            LabelFore.Click += ColorLabels_Click;
            LabelBack.Click += ColorLabels_Click;
            LinkReset.Click += LinkReset_Click;
            TextBoxCustomText.TextChanged += TextBoxCustomText_TextChanged;
        }

        protected override void OnLoad()
        {
            if (CustomRules != null)
            {
                if (CustomRules.Count() == 0)
                {
                    AddItem("文本测试", "65535天23时59分59秒", "255,255,255", "255,255,255", Placeholders.PH_P1);
                    DeleteAllItems();
                }
                else
                {
                    SuspendListView(() =>
                    {
                        foreach (var Rule in CustomRules)
                        {
                            AddItem(Rule);
                        }
                    });
                }
            }

            var CustomText = TextBoxCustomText.Text;

            if (string.IsNullOrEmpty(CustomText))
            {
                ResetCustomText();
            }
            else if (CustomText != CustomRuleHelper.GetCustomTextDefault(ComboBoxRuleType.SelectedIndex, Preferences))
            {
                SaveUserUnsavedText();
            }

            UpdateColorLabels(Color.Black, Color.White);
        }

        protected override void AdjustUI()
        {
            SetLabelAutoWrap(LabelWarning, GroupBoxWarning);
        }

        private void RulesManager_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && GetSelections().Count != 0)
            {
                ContextDelete.PerformClick();
            }
            else if (e.Control && e.KeyCode == Keys.A)
            {
                SelectAllItems();
            }

            e.Handled = true;
        }

        protected override void OnShown()
        {
            if (ShowWarning)
            {
                MessageX.Warn("检测到未在设置里勾选 自定义文本，在此添加的任何规则将不会生效！");
            }
        }

        private void ListViewMain_MouseUpDown(object sender, MouseEventArgs e)
        {
            var SelectionCount = GetSelections().Count;
            ContextDelete.Enabled = SelectionCount != 0;
            ButtonChange.Enabled = SelectionCount == 1;
        }

        private void ListViewMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            var Selections = GetSelections();

            if (Selections.Count > 0)
            {
                var Selected = Selections[0].SubItems;
                var Tick = CustomRuleHelper.GetExamTick(Selected[1].Text);

                ComboBoxRuleType.SelectedIndex = (int)CustomRuleHelper.GetPhase(Selected[0].Text);
                NUDDays.Value = Tick.Days;
                NUDHours.Value = Tick.Hours;
                NUDMinutes.Value = Tick.Minutes;
                NUDSeconds.Value = Tick.Seconds;
                UpdateColorLabels(ColorHelper.GetColor(Selected[2].Text), ColorHelper.GetColor(Selected[3].Text));
                TextBoxCustomText.Text = Selected[4].Text;
            }
        }

        private void ListViewMain_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.NewWidth = ((ListView)sender).Columns[e.ColumnIndex].Width;
            e.Cancel = true;
        }

        private void ComboBoxRuleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Execute(() =>
            {
                var UserSelected = ComboBoxRuleType.SelectedIndex;

                if (UserUnsavedText.ContainsKey(UserSelected))
                {
                    if (TextBoxCustomText.Text != LastText)
                    {
                        LastText = TextBoxCustomText.Text;
                    }

                    TextBoxCustomText.Text = UserUnsavedText[UserSelected];
                    return;
                }

                ResetCustomText();
            });
        }

        private void ColorLabels_Click(object sender, EventArgs e)
        {
            var LabelSender = (Label)sender;
            var ColorDialogMain = new ColorDialogEx();

            if (ColorDialogMain.ShowDialog(LabelSender.BackColor) == DialogResult.OK)
            {
                LabelSender.BackColor = ColorDialogMain.Color;
            }

            LabelColorPreview.ForeColor = LabelFore.BackColor;
            LabelColorPreview.BackColor = LabelBack.BackColor;

            ColorDialogMain.Dispose();
        }

        private void TextBoxCustomText_TextChanged(object sender, EventArgs e)
        {
            Execute(SaveUserUnsavedText);
        }

        private void LinkReset_Click(object sender, EventArgs e)
        {
            ResetCustomText();
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            var _Fore = LabelFore.BackColor;
            var _Back = LabelBack.BackColor;

            if (!ColorHelper.IsNiceContrast(_Fore, _Back))
            {
                MessageX.Error("选择的颜色相似或对比度较低，将无法看清文字。\n\n请尝试更换其它背景颜色或文字颜色！");
                return;
            }

            if (NUDDays.Value == 0 && NUDHours.Value == 0 && NUDMinutes.Value == 0 && NUDSeconds.Value == 0)
            {
                MessageX.Error("时刻不能为0，请重新设置！");
                return;
            }

            if (!(bool)CustomRuleHelper.CheckCustomText([TextBoxCustomText.Text], out string ErrorMsg, ToBoolean: true) && !string.IsNullOrWhiteSpace(ErrorMsg))
            {
                MessageX.Error(ErrorMsg);
                return;
            }

            var RuleTypeText = CustomRuleHelper.GetRuleTypeText((CountdownPhase)ComboBoxRuleType.SelectedIndex);
            var TickText = $"{NUDDays.Value}天{NUDHours.Value}时{NUDMinutes.Value}分{NUDSeconds.Value}秒";
            ListViewItem Existing = null;

            foreach (var Item in GetAllItems())
            {
                if (Item.SubItems[0].Text == RuleTypeText && Item.SubItems[1].Text == TickText)
                {
                    Existing = Item;
                    break;
                }
            }

            if (Existing != null)
            {
                if (MessageX.Warn("检测到即将添加的规则与现有的某个规则重复！\n\n是否覆盖? (是 则覆盖, 否 则取消添加)", Buttons: MessageBoxExButtons.YesNo)
                    == DialogResult.Yes)
                {
                    EditItem(Existing);
                }

                return;
            }

            AddItem(
                RuleTypeText,
                TickText,
                _Fore.ToRgb(),
                _Back.ToRgb(),
                TextBoxCustomText.Text.RemoveIllegalChars());
        }

        private void ButtonChange_Click(object sender, EventArgs e)
        {
            var Selections = GetSelections();
            if (Selections.Count > 0)
            {
                EditItem(Selections[0]);
            }
        }

        protected override void ButtonA_Click()
        {
            CustomRules = [.. GetAllItems().Select(x => (RulesManagerObject)x.Tag)];
            base.ButtonA_Click();
        }

        private IEnumerable<ListViewItem> GetAllItems()
            => ListViewMain.Items.Cast<ListViewItem>();

        private ListView.SelectedListViewItemCollection GetSelections()
            => ListViewMain.SelectedItems;

        private void UpdateColorLabels(Color Fore, Color Back)
        {
            LabelColorPreview.ForeColor = LabelFore.BackColor = Fore;
            LabelColorPreview.BackColor = LabelBack.BackColor = Back;
        }

        private void SaveUserUnsavedText()
        {
            UserUnsavedText[ComboBoxRuleType.SelectedIndex] = TextBoxCustomText.Text;
            LastText = TextBoxCustomText.Text;
        }

        private void ResetCustomText()
        {
            TextBoxCustomText.Text = CustomRuleHelper.GetCustomTextDefault(ComboBoxRuleType.SelectedIndex, Preferences);
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
            var Items = GetAllItems().ToList();
            Items.Sort(new ListViewItemComparer());

            SuspendListView(() =>
            {
                DeleteAllItems();
                AddItems(Items);
                AutoAdjustColumnWidth();
            });

            UserChanged();
        }

        private void AddItem(string Type, string Tick, string Fore, string Back, string Custom, RulesManagerObject Data = null)
        {
            var Item = new ListViewItem([Type, Tick, Fore, Back, Custom])
            {
                Tag = Data ?? new()
                {
                    Phase = CustomRuleHelper.GetPhase(Type),
                    Tick = CustomRuleHelper.GetExamTick(Tick),
                    Fore = ColorHelper.GetColor(Fore),
                    Back = ColorHelper.GetColor(Back),
                    Text = Custom
                }
            };

            ListViewMain.Items.Add(Item);
            SortItems();
        }

        private void AddItem(RulesManagerObject Rule)
        {
            AddItem(
                CustomRuleHelper.GetRuleTypeText(Rule.Phase),
                CustomRuleHelper.GetExamTickText(Rule.Tick),
                Rule.Fore.ToRgb(),
                Rule.Back.ToRgb(),
                Rule.Text,
                Rule);
        }

        private void AddItems(IEnumerable<ListViewItem> Items)
        {
            ListViewMain.Items.AddRange([.. Items]);
        }

        private void EditItem(ListViewItem Item)
        {
            var SubItems = Item.SubItems;

            var Phase = (CountdownPhase)ComboBoxRuleType.SelectedIndex;
            var Tick = new TimeSpan((int)NUDDays.Value, (int)NUDHours.Value, (int)NUDMinutes.Value, (int)NUDSeconds.Value);
            var Fore = LabelFore.BackColor;
            var Back = LabelBack.BackColor;
            var Text = TextBoxCustomText.Text.RemoveIllegalChars();

            SubItems[0].Text = CustomRuleHelper.GetRuleTypeText(Phase);
            SubItems[1].Text = CustomRuleHelper.GetExamTickText(Tick);
            SubItems[2].Text = Fore.ToRgb();
            SubItems[3].Text = Back.ToRgb();
            SubItems[4].Text = Text;

            Item.Tag = new RulesManagerObject()
            {
                Phase = Phase,
                Tick = Tick,
                Fore = Fore,
                Back = Back,
                Text = Text
            };

            SortItems();
        }

        private void DeleteItem(ListViewItem Item)
        {
            ListViewMain.Items.Remove(Item);
        }

        private void DeleteAllItems()
        {
            ListViewMain.Items.Clear();
        }

        private void AutoAdjustColumnWidth()
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
    }
}
