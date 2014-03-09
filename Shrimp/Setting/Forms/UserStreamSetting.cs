using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Shrimp.Setting.Forms
{
    public partial class UserStreamSetting : UserControl, ISettingForm, IDisposable
    {
        private Dictionary<string, bool> setting;

        public UserStreamSetting()
        {
            InitializeComponent();
            setting = Setting.UserStream.save();
            SettingReflection();
        }

        public void SettingReflection()
        {
            UserStreamCheckedBox.SetItemChecked(0, setting["isIncludeFollowingsActivity"]);
            UserStreamCheckedBox.SetItemChecked(1, setting["isRepliesAll"]);
        }

        public void SaveReflection()
        {
            Setting.UserStream.load(setting);
        }

        private void UserStreamCheckedBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.Index == 0)
                setting["isIncludeFollowingsActivity"] = (e.NewValue == CheckState.Checked ? true : false);
            if (e.Index == 1)
                setting["isRepliesAll"] = (e.NewValue == CheckState.Checked ? true : false);
            SaveReflection();
        }
    }
}
