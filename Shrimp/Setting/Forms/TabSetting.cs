using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Shrimp.Setting.Forms
{
    public partial class TabSetting : UserControl, ISettingForm, IDisposable
    {
        private Dictionary<string, object> settings;
        private Shrimp.OnChangedTabControlAlignment OnTabAlignChanged;
        private bool RefWait = true;
        public TabSetting(Shrimp.OnChangedTabControlAlignment OnTabAlignChanged)
        {
            InitializeComponent();
            this.OnTabAlignChanged = OnTabAlignChanged;
            settings = Setting.Timeline.save();
            SettingReflection();
            RefWait = false;
        }

        public void SettingReflection()
        {
            this.TabAlignmentSelect.SelectedIndex = (int)this.settings["ShrimpTabAlignment"];
            this.TabAnimationSelect.SelectedIndex = (int)this.settings["TabChangeAnimation"];
            this.TabSettingCheckedListBox.SetItemChecked(0, (bool)this.settings["SelectTabWhenCreatedTab"]);
            this.TabSettingCheckedListBox.SetItemChecked ( 1, (bool)this.settings["isTabMultiline"] );
        }

        public void SaveReflection()
        {
            Setting.Timeline.load(this.settings);
        }

        private void TabAnimationSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RefWait)
                return;
            this.settings["TabChangeAnimation"] = this.TabAnimationSelect.SelectedIndex;
            this.settings["ShrimpTabAlignment"] = this.TabAlignmentSelect.SelectedIndex;

            OnTabAlignChanged.Invoke((TabAlignment)this.TabAlignmentSelect.SelectedIndex);
            this.SaveReflection();
        }

        private void TabSettingCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.Index == 0)
                this.settings["SelectTabWhenCreatedTab"] = (e.NewValue == CheckState.Checked ? true : false);
            if ( e.Index == 1 )
                this.settings["isTabMultiline"] = ( e.NewValue == CheckState.Checked ? true : false );
            this.SaveReflection();
            OnTabAlignChanged.Invoke ( (TabAlignment)this.TabAlignmentSelect.SelectedIndex );
        }
    }
}
