using System.Collections.Generic;

namespace Shrimp.Setting
{
    class UserStream
    {
        #region コンストラクタ

        internal static void initialize()
        {
            //  初期設定
            isRepliesAll = false;
            isIncludeFollowingsActivity = false;
            isEnableUserstream = true;
            isMuteWithoutFriends = false;
            isUseGZip = false;
        }

        public static void load(Dictionary<string, bool> obj)
        {
            if (obj == null)
                return;
            if (obj.ContainsKey("isRepliesAll"))
                isRepliesAll = obj["isRepliesAll"];
            if (obj.ContainsKey("isIncludeFollowingsActivity"))
                isIncludeFollowingsActivity = obj["isIncludeFollowingsActivity"];
            if (obj.ContainsKey("isEnableUserstream"))
                isEnableUserstream = obj["isEnableUserstream"];
            if ( obj.ContainsKey ( "isMuteWithoutFriends" ) )
                isMuteWithoutFriends = obj["isMuteWithoutFriends"];
            if ( obj.ContainsKey ( "isUseGZip" ) )
                isUseGZip = obj["isUseGZip"];
        }

        public static Dictionary<string, bool> save()
        {
            var dest = new Dictionary<string, bool>();
            dest["isIncludeFollowingsActivity"] = isIncludeFollowingsActivity;
            dest["isRepliesAll"] = isRepliesAll;
            dest["isEnableUserstream"] = isEnableUserstream;
            dest["isMuteWithoutFriends"] = isMuteWithoutFriends;
            dest["isUseGZip"] = isUseGZip;
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

        /// <summary>
        /// フォロー外のリプライを受信しない
        /// </summary>
        public static bool isMuteWithoutFriends
        {
            get;
            set;
        }

        /// <summary>
        /// GZIPを使うかどうか
        /// </summary>
        public static bool isUseGZip
        {
            get;
            set;
        }
    }
}
