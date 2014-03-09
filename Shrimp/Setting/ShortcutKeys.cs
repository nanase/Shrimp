using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shrimp.ControlParts.Timeline.Click;
using System.Windows.Forms;

namespace Shrimp.Setting
{
    public class ShortcutKeys
    {
        #region 定義

        #endregion

        #region コンストラクタ

        internal static void initialize ()
        {
            //  初期設定
            Shortcuts = new ShortcutActionCollection ();
			Shortcuts.Add 
				( new ShortcutAction ( Actions.Fav, 
					UserActions.KeyboardShortcut, new ShortcutKey ( Keys.S, true, false ) ) );
			Shortcuts.Add
				(new ShortcutAction(Actions.Retweet,
					UserActions.KeyboardShortcut, new ShortcutKey(Keys.R, true, false)));
			Shortcuts.Add
				(new ShortcutAction(Actions.Reply,
					UserActions.KeyboardShortcut, new ShortcutKey(Keys.D, true, false)));
        }

        public static void load ( Dictionary<string, ShortcutActionCollection> obj )
        {
            if ( obj == null )
                return;
			if (obj.ContainsKey("ShortCutKeys"))
			{
				Shortcuts = obj["ShortCutKeys"];
				Shortcuts.load();
			}
        }

        public static Dictionary<string, ShortcutActionCollection> save ()
        {
            var dest = new Dictionary<string, ShortcutActionCollection> ();
			Shortcuts.save();
			dest["ShortCutKeys"] = Shortcuts;
            return dest;
        }

        #endregion
        /// <summary>
        /// ショートカットキーのデータをまとめたもの
        /// </summary>
        public static ShortcutActionCollection Shortcuts
        {
            get;
            set;
        }


    }
}
