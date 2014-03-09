using System.Collections.Generic;
using System.Runtime.Serialization;
using Shrimp.ControlParts.Timeline.Click;
using Shrimp.Setting.ObjectXML;

namespace Shrimp.Setting
{
    /// <summary>
    /// Setting Class
    /// 設定の全体管理
    /// </summary>
    [DataContract]
    public class SettingOwner
    {
        [DataMember]
        public Dictionary<string, BrushEX> ColorsData;
        [DataMember]
        public Dictionary<string, TabColorManager> TabColorsData;
        [DataMember]
        public Dictionary<string, object> TimelineData, FormSettingData;
        [DataMember]
        public Dictionary<string, bool> SearchData, UserStreamData, UpdateData;
        [DataMember]
        public Dictionary<string, string> FontData;
        [DataMember]
        public Dictionary<string, ShortcutActionCollection> ShortcutData;
        [DataMember]
        public Dictionary<string, int> DataBaseData;

        /// <summary>
        /// 設定を保存する
        /// </summary>
        public void SaveAll()
        {
            this.ColorsData = Colors.save();
            this.TabColorsData = TabColors.save();
            this.TimelineData = Timeline.save();
            this.SearchData = Search.save();
            this.FontData = Fonts.save();
            this.FormSettingData = FormSetting.save();
            this.ShortcutData = ShortcutKeys.save();
            this.UserStreamData = UserStream.save();
            this.DataBaseData = Database.save();
            this.UpdateData = Update.save();
        }

        /// <summary>
        /// 設定を読み込む
        /// </summary>
        public void LoadAll()
        {
            Colors.load(this.ColorsData);
            TabColors.load(this.TabColorsData);
            Timeline.load(this.TimelineData);
            Search.load(this.SearchData);
            Fonts.load(this.FontData);
            FormSetting.load(this.FormSettingData);
            ShortcutKeys.load(this.ShortcutData);
            UserStream.load(this.UserStreamData);
            Database.load(this.DataBaseData);
            Update.load(this.UpdateData);
        }
    }
}
