using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shrimp.Setting
{
    class Update
    {
        #region 定義

        #endregion

        #region コンストラクタ

        /// <summary>
        /// 静的コンストラクタ
        /// </summary>
        public static void initialize ()
        {
            isUpdateEnable = true;
            isIgnoreUpdate = false;
        }

        public static void load ( Dictionary<string, bool> obj )
        {
            if ( obj == null )
                return;
            if ( obj.ContainsKey ( "isUpdateEnable" ) )
                isUpdateEnable = (bool)obj["isUpdateEnable"];
            if ( obj.ContainsKey ( "isIgnoreUpdate" ) )
                isIgnoreUpdate = (bool)obj["isIgnoreUpdate"];
        }

        public static Dictionary<string, bool> save ()
        {
            var dest = new Dictionary<string, bool> ();
            dest["isUpdateEnable"] = isUpdateEnable;
            dest["isIgnoreUpdate"] = isIgnoreUpdate;
            return dest;
        }

        #endregion

        /// <summary>
        /// アップデート通知を有効にするかどうか
        /// </summary>
        public static bool isUpdateEnable
        {
            get;
            set;
        }

        /// <summary>
        /// アップデート通知を無視するかどうか
        /// </summary>
        public static bool isIgnoreUpdate
        {
            get;
            set;
        }
    }
}
