using Shrimp.Module.Parts;

namespace Shrimp.ControlParts.User
{
    /// <summary>
    /// 基底クラス
    /// </summary>
    class UserStatusCellSize
    {
        /// <summary>
        /// アイコン
        /// </summary>
        public Cell icon
        {
            get;
            set;
        }

        /// <summary>
        /// スクリーンネーム
        /// </summary>
        public Cell screen_name
        {
            get;
            set;
        }

        /// <summary>
        /// 名前
        /// </summary>
        public Cell name
        {
            get;
            set;
        }

        /// <summary>
        /// bio
        /// </summary>
        public Cell bio
        {
            get;
            set;
        }

        /// <summary>
        /// ツイート数
        /// </summary>
        public Cell tweet_count
        {
            get;
            set;
        }

        /// <summary>
        /// フォロー中の人数
        /// </summary>
        public Cell following_count
        {
            get;
            set;
        }

        /// <summary>
        /// フォロワー数
        /// </summary>
        public Cell follower_count
        {
            get;
            set;
        }

        /// <summary>
        /// ふぁぼりて
        /// </summary>
        public Cell favorites_count
        {
            get;
            set;
        }

        /// <summary>
        /// リスト
        /// </summary>
        public Cell listed_count
        {
            get;
            set;
        }

        /// <summary>
        /// このユーザについて
        /// </summary>
        public Cell AboutUser
        {
            get;
            set;
        }

        /// <summary>
        /// 開始時期
        /// </summary>
        public Cell created_at
        {
            get;
            set;
        }
    }
}
