using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shrimp.Setting
{
    class BackgroundImage
    {
        #region 定義

        #endregion

        #region コンストラクタ

        /// <summary>
        /// 静的コンストラクタ
        /// </summary>
        public static void initialize ()
        {
            BackgroundImagePath = null;
        }

        public static void load ( Dictionary<string, string> obj )
        {
            if ( obj == null )
                return;
            if ( obj.ContainsKey ( "BackgroundImagePath" ) )
                BackgroundImagePath = (string)( (string)obj["BackgroundImagePath"] ).Clone ();
        }

        public static Dictionary<string, string> save ()
        {
            var dest = new Dictionary<string, string> ();
            dest["BackgroundImagePath"] = BackgroundImagePath;
            return dest;
        }

        #endregion

        /// <summary>
        /// アップデート通知を有効にするかどうか
        /// </summary>
        public static string BackgroundImagePath
        {
            get;
            set;
        }
    }
}
