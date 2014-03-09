using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shrimp.Setting
{
    class UserStream
    {
        #region コンストラクタ

        internal static void initialize ()
        {
            //  初期設定
            isRepliesAll = false;
            isIncludeFollowingsActivity = false;
            isEnableUserstream = true;
        }

        public static void load ( Dictionary<string, bool> obj )
        {
            if ( obj == null )
                return;
            if ( obj.ContainsKey ( "isRepliesAll" ) )
                isRepliesAll = obj["isRepliesAll"];
            if ( obj.ContainsKey ( "isIncludeFollowingsActivity" ) )
                isIncludeFollowingsActivity = obj["isIncludeFollowingsActivity"];
            if ( obj.ContainsKey ( "isEnableUserstream" ) )
                isEnableUserstream = obj["isEnableUserstream"];
        }

        public static Dictionary<string, bool> save ()
        {
            var dest = new Dictionary<string, bool> ();
            dest["isIncludeFollowingsActivity"] = isIncludeFollowingsActivity;
            dest["isRepliesAll"] = isRepliesAll;
            dest["isEnableUserstream"] = isEnableUserstream;
            return dest;
        }

        #endregion

        /// <summary>
        /// Userstreamを有効にする
        /// </summary>
        public static bool isEnableUserstream
        {
            get;
            set;
        }

        /// <summary>
        /// replies=all
        /// </summary>
        public static bool isRepliesAll
        {
            get;
            set;
        }

        /// <summary>
        /// include_followings_activiy
        /// </summary>
        public static bool isIncludeFollowingsActivity
        {
            get;
            set;
        }
    }
}
