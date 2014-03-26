using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Shrimp.Setting.ObjectXML;

namespace Shrimp.Setting.Forms
{
    public partial class TimelineManagement : UserControl, ISettingForm, IDisposable
    {
        private Dictionary<string, object> tlSetting;
        private Dictionary<string, BrushEX> colors;
        public TimelineManagement()
        {
            InitializeComponent();
            this.tlSetting = Setting.Timeline.save();
            this.colors = Setting.Colors.save();
            this.SettingReflection();
        }

        ~TimelineManagement()
        {
            this.tlSetting = null;
        }

        private void NotifyColorButton_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (this.selectColor.ShowDialog() == DialogResult.OK)
            {
                if (btn.Name == "NotifyBackgroundColorButton")
                {
                    //
                    this.colors["NotifyBackgroundColor"] = new BrushEX(
                        new SolidBrush(Color.FromArgb((int)this.NotifyBackgroundAlphaNumeric.Value, this.selectColor.Color)));
                    if (this.NotifyBackgroundColorPictureBox.Image != null)
                        this.NotifyBackgroundColorPictureBox.Image.Dispose();
                    this.NotifyBackgroundColorPictureBox.Image = SettingUtils.CreateImageColor(this.colors["NotifyBackgroundColor"].Generate);
                }
                else if (btn.Name == "NotifyStringColorButton")
                {
                    this.colors["NotifyStringColor"] = new BrushEX(
                        new SolidBrush(Color.FromArgb(255, this.selectColor.Color)));
                    if (this.NotifyStringColorPictureBox.Image != null)
                        this.NotifyStringColorPictureBox.Image.Dispose();
                    this.NotifyStringColorPictureBox.Image = SettingUtils.CreateImageColor(this.colors["NotifyStringColor"].Generate);
                }
                SaveReflection();
            }
        }

        private void timelineSettingCheckBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.Index == 0)
                tlSetting["isEnableTimeLink"] = (e.NewValue == CheckState.Checked ? true : false);
            if (e.Index == 1)
                tlSetting["isEnableSourceLink"] = (e.NewValue == CheckState.Checked ? true : false);
            if (e.Index == 2)
                tlSetting["isEnableRetweetLink"] = (e.NewValue == CheckState.Checked ? true : false);
            if (e.Index == 3)
                tlSetting["isRetweetBold"] = (e.NewValue == CheckState.Checked ? true : false);
            if (e.Index == 4)
                tlSetting["isNotifyBold"] = (e.NewValue == CheckState.Checked ? true : false);
            if (e.Index == 5)
                tlSetting["isReplyBold"] = (e.NewValue == CheckState.Checked ? true : false);
            if ( e.Index == 6 )
                tlSetting["isEnableAbsoluteTime"] = ( e.NewValue == CheckState.Checked ? true : false );
            SaveReflection();
        }

        private void AnimationCheckBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.Index == 0)
                tlSetting["isEnableInsertAnimation"] = (e.NewValue == CheckState.Checked ? true : false);
            if (e.Index == 1)
                tlSetting["isEnableNotifyAnimation"] = (e.NewValue == CheckState.Checked ? true : false);
            SaveReflection();
        }

        private void NotifyBackgroundAlphaNumeric_ValueChanged(object sender, EventArgs e)
        {
            this.colors["NotifyBackgroundColor"].a = (byte)this.NotifyBackgroundAlphaNumeric.Value;
            if (this.NotifyBackgroundColorPictureBox.Image != null)
                this.NotifyBackgroundColorPictureBox.Image.Dispose();
            this.NotifyBackgroundColorPictureBox.Image = SettingUtils.CreateImageColor(this.colors["NotifyBackgroundColor"].Generate);
            SaveReflection();
        }

        private void fTimelineSettingCheckBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.Index == 0)
                tlSetting["isConfirmRT"] = (e.NewValue == CheckState.Checked ? true : false);
            if (e.Index == 1)
                tlSetting["isConfirmFav"] = (e.NewValue == CheckState.Checked ? true : false);
            if (e.Index == 2)
                tlSetting["isHoverSelectMode"] = (e.NewValue == CheckState.Checked ? true : false);
            SaveReflection();
        }

        public void SettingReflection()
        {
            this.fTimelineSettingCheckBox.SetItemChecked(0, (bool)tlSetting["isConfirmRT"]);
            this.fTimelineSettingCheckBox.SetItemChecked(1, (bool)tlSetting["isConfirmFav"]);
            this.fTimelineSettingCheckBox.SetItemChecked(2, (bool)tlSetting["isHoverSelectMode"]);

            this.timelineSettingCheckBox.SetItemChecked(0, (bool)tlSetting["isEnableTimeLink"]);
            this.timelineSettingCheckBox.SetItemChecked(1, (bool)tlSetting["isEnableSourceLink"]);
            this.timelineSettingCheckBox.SetItemChecked(2, (bool)tlSetting["isEnableRetweetLink"]);
            this.timelineSettingCheckBox.SetItemChecked(3, (bool)tlSetting["isRetweetBold"]);
            this.timelineSettingCheckBox.SetItemChecked(4, (bool)tlSetting["isNotifyBold"]);
            this.timelineSettingCheckBox.SetItemChecked(5, (bool)tlSetting["isReplyBold"]);
            this.timelineSettingCheckBox.SetItemChecked ( 6, (bool)tlSetting["isEnableAbsoluteTime"] );
            

            this.SavingTweetNumNumeric.Value = (((int)tlSetting["SavedTimelineTweetNum"]) < this.SavingTweetNumNumeric.Minimum ? this.SavingTweetNumNumeric.Minimum : ((int)tlSetting["SavedTimelineTweetNum"]));
            this.AnimationCheckBox.SetItemChecked(0, (bool)tlSetting["isEnableInsertAnimation"]);
            this.AnimationCheckBox.SetItemChecked(1, (bool)tlSetting["isEnableNotifyAnimation"]);
            this.NotifyBackgroundColorPictureBox.Image = SettingUtils.CreateImageColor(this.colors["NotifyBackgroundColor"].Generate);
            this.NotifyBackgroundAlphaNumeric.Value = (this.colors["NotifyBackgroundColor"].Alpha);
            this.NotifyStringColorPictureBox.Image = SettingUtils.CreateImageColor(this.colors["NotifyStringColor"].Generate);
        }

        public void SaveReflection()
        {
            Setting.Timeline.load(tlSetting);
            Setting.Colors.load(colors);
        }

        private void SavingTweetNumNumeric_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown obj = sender as NumericUpDown;
            tlSetting["SavedTimelineTweetNum"] = (int)obj.Value;
            SaveReflection();
        }
    }
}
