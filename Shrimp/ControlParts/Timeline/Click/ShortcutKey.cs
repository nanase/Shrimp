using System;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace Shrimp.ControlParts.Timeline.Click
{
    /// <summary>
    /// ショートカットキーを管理する
    /// </summary>
    [DataContract]
    public class ShortcutKey : ICloneable
    {
        [DataMember]
        private int keyInt;
        public Keys key;
        [DataMember]
        public bool ctrl;
        [DataMember]
        public bool shift;

        public ShortcutKey(Keys key, bool ctrl, bool shift)
        {
            this.key = key;
            this.ctrl = ctrl;
            this.shift = shift;
        }

        public void save()
        {
            //	保存する際、Enumをintに変換する
            this.keyInt = (int)key;
        }

        public void load()
        {
            //	読み込む際、Enumへ変換する
            this.key = (Keys)this.keyInt;
        }

        /// <summary>
        /// キーがマッチするかどうか調べる
        /// </summary>
        /// <param name="ctrl">Ctrlキー</param>
        /// <param name="shift">Shiftキー</param>
        /// <param name="key">キー</param>
        /// <returns></returns>
        public bool IsMatchKey(bool ctrl, bool shift, Keys key)
        {
            return (this.key == key && this.ctrl == ctrl && this.shift == shift);
        }

        public object Clone()
        {
            return new ShortcutKey(key, ctrl, shift);
        }

        public string KeyToString()
        {
            var dest = "";
            if (this.ctrl)
                dest += "Ctrl + ";
            if (this.shift)
                dest += "Shift + ";
            dest += this.key.ToString();
            return dest;
        }
    }
}
