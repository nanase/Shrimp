using System.Collections.Generic;
using System.Runtime.Serialization;
using Shrimp.Setting.ObjectXML;

namespace Shrimp.Setting
{
    /// <summary>
    /// Setting Class
    /// 設定の全体管理
    /// </summary>
    [DataContract]
    public class ColorOwner
    {
        [DataMember]
        public Dictionary<string, BrushEX> ColorsData;
        /// <summary>
        /// 設定を保存する
        /// </summary>
        public void SaveAll()
        {
            this.ColorsData = Colors.save();
        }

        /// <summary>
        /// 設定を読み込む
        /// </summary>
        public void LoadAll()
        {
            Colors.load(this.ColorsData);
        }
    }
}
