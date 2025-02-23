﻿using PlainCEETimer.Controls;
using PlainCEETimer.Modules;
using PlainCEETimer.Modules.Configuration;
using System;

namespace PlainCEETimer.Dialogs
{
    public partial class ExamInfoManager : DialogEx
    {
        public ExamInfoObject[] ExamInfo { get; set; }
        public int PeriodIndex { get; set; }
        public bool AutoSwitch { get; set; }

        public ExamInfoManager() : base(DialogExProp.BindButtons | DialogExProp.KeyPreview)
        {
            CompositedStyle = true;
            InitializeComponent();
        }

        protected override void OnLoad()
        {
            LoadUI();
            LoadData();
        }

        private void LoadUI()
        {
            BindComboData(ComboBoxSwitchPeriod,
            [
                new("10 秒", 0),
                new("15 秒", 1),
                new("30 秒", 2),
                new("45 秒", 3),
                new("1 分钟", 4),
                new("2 分钟", 5),
                new("3 分钟", 6),
                new("5 分钟", 7),
                new("10 分钟", 8),
                new("15 分钟", 9),
                new("30 分钟", 10),
                new("45 分钟", 11),
                new("1 小时", 12),
            ]);
        }

        private void LoadData()
        {
            CheckBoxAutoSwitch.Checked = true;
            ComboBoxSwitchPeriod.SelectedIndex = PeriodIndex;
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {

        }

        private void CheckBoxAutoSwitch_CheckedChanged(object sender, EventArgs e)
        {
            ComboBoxSwitchPeriod.Enabled = CheckBoxAutoSwitch.Checked;
        }
    }
}
