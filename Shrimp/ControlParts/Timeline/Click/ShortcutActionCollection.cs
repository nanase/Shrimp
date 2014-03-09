using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace Shrimp.ControlParts.Timeline.Click
{
    /// <summary>
    /// ショートカットキーをまとめたデータコレクション
    /// </summary>
    [DataContract]
    public class ShortcutActionCollection
    {
        [DataMember]
        private List<ShortcutAction> keys = new List<ShortcutAction> ();

        public ShortcutActionCollection ()
        {
        }

		public void save()
		{
			//	保存する際、コレクト内部のENUMを変換します
			foreach (ShortcutAction key in keys)
			{
				key.save();
			}
		}

		public void load()
		{
			//	読み込む際、コレクト内部のENUMを変換します
			foreach (ShortcutAction key in keys)
			{
				key.load();
			}
		}

		/// <summary>
		/// キーコレクションを取得する
		/// </summary>
		public List<ShortcutAction> Keys
		{
			get { return this.keys; }
		}

        /// <summary>
        /// キーを追加する
        /// </summary>
        /// <param name="key"></param>
        public void Add ( ShortcutAction key )
        {
            keys.Add ( key );
        }

        /// <summary>
        /// キーを削除する
        /// </summary>
        /// <param name="key"></param>
        public void Remove ( ShortcutAction key )
        {
            keys.Remove ( key );
        }

        /// <summary>
        /// キーを削除する
        /// </summary>
        /// <param name="num"></param>
        public void RemoveAt ( int num )
        {
            keys.RemoveAt ( num );
        }

        /// <summary>
        /// ダブルクリックのときのアクションを取得します
        /// </summary>
        /// <returns></returns>
        public Actions DoubleClicked ()
        {
            foreach ( ShortcutAction key in this.keys )
            {
                if ( key.user_action == UserActions.MouseDoubleClick )
                    return key.action;
            }
            return Actions.None;
        }

        /// <summary>
        /// キーが押されたときのアクションを取得します
        /// </summary>
        /// <param name="ctrl">Ctrlキーが押されたかどうか</param>
        /// <param name="shift">Shiftキーが押されたかどうか</param>
        /// <param name="pressKey">押されたキー</param>
        /// <returns>対応するアクション</returns>
        public Actions KeyDown ( bool ctrl, bool shift, Keys pressKey )
        {
            foreach ( ShortcutAction key in this.keys )
            {
                if ( key.user_action == UserActions.KeyboardShortcut &&
                     key.shortcut_key.IsMatchKey ( ctrl, shift, pressKey ) )
                    return key.action;
            }
            return Actions.None;
        }
    }
}
