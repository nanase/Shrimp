using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Shrimp.Setting.Forms
{
    public partial class ShrimpSetting : UserControl, ISettingForm, IDisposable
    {
        private Dictionary<string, object> setting;

        public ShrimpSetting ()
        {
            InitializeComponent();
            setting = Setting.FormSetting.save();
            SettingReflection();
        }

        public void SettingReflection()
        {
            ShrimpCheckedBox.SetItemChecked ( 0, (bool)setting["isInsertTaskTrayWhenClosing"] );
        }

        public void SaveReflection()
        {
            Setting.FormSetting.load ( setting );
        }

        private void ShrimpCheckedBox_ItemCheck ( object sender, ItemCheckEventArgs e )
        {
            if ( e.Index == 0 )
                setting["isInsertTaskTrayWhenClosing"] = ( e.NewValue == CheckState.Checked ? true : false );
            SaveReflection ();
        }
    }
}
