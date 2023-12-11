using System;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Forms
{
    public partial class FormSettings : Form
    {
        public string en
        {
            get { return FormSettingsSetExamNameText.Text; }
            set { FormSettingsSetExamNameText.Text = value; }
        }
        public string xn
        {
            get { return FormSettingsSetCEETextN.Text; }
            set { FormSettingsSetCEETextN.Text = value; }
        }
        public string xy
        {
            get { return FormSettingsSetCEETextY.Text; }
            set { FormSettingsSetCEETextY.Text = value; }
        }
        public string xr
        {
            get { return FormSettingsSetCEETextR.Text; }
            set { FormSettingsSetCEETextR.Text = value; }
        }
        public string xs
        {
            get { return FormSettingsSetCEETextS.Text; }
            set { FormSettingsSetCEETextS.Text = value; }
        }
        public string xf
        {
            get { return FormSettingsSetCEETextF.Text; }
            set { FormSettingsSetCEETextF.Text = value; }
        }
        public string xm
        {
            get { return FormSettingsSetCEETextM.Text; }
            set { FormSettingsSetCEETextM.Text = value; }
        }
        public string xne
        {
            get { return FormSettingsSetEndTextN.Text; }
            set { FormSettingsSetEndTextN.Text = value; }
        }
        public string xye
        {
            get { return FormSettingsSetEndTextY.Text; }
            set { FormSettingsSetEndTextY.Text = value; }
        }
        public string xre
        {
            get { return FormSettingsSetEndTextR.Text; }
            set { FormSettingsSetEndTextR.Text = value; }
        }
        public string xse
        {
            get { return FormSettingsSetEndTextS.Text; }
            set { FormSettingsSetEndTextS.Text = value; }
        }
        public string xfe
        {
            get { return FormSettingsSetEndTextF.Text; }
            set { FormSettingsSetEndTextF.Text = value; }
        }
        public string xme
        {
            get { return FormSettingsSetEndTextM.Text; }
            set { FormSettingsSetEndTextM.Text = value; }
        }
        public bool StartupEnabled
        {
            get { return FormSettingsSetStartupCheck.Checked; }
            set { FormSettingsSetStartupCheck.Checked = value; }
        }
        public CEETimerCSharpWinForms MainForm
        {
            get { return mainForm; }
            set { mainForm = value; }
        }
        public Label LabelCountdown
        {
            get;
            set;
        }
        public DateTime TargetDateTime
        {
            get { return new DateTime(n, y, r, s, f, m); }
        }
        public DateTime TargetDateTimeEnd
        {
            get { return new DateTime(ne, ye, re, se, fe, me); }
        }
        public string ExamName
        {
            get{ return examName; }
        }
    }
}
