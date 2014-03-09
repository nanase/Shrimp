using System;

namespace Shrimp.Twitter.Status
{
    /// <summary>
    /// 通知のフィルタ
    /// </summary>
    public class NotifyFilter : ICloneable
    {
        public bool Favorited;
        public bool UnFavorited;
        public bool Followed;
        public bool Unfollowed;

        public NotifyFilter()
        {
            this.Favorited = true;
            this.UnFavorited = true;
            this.Followed = true;
            this.Unfollowed = true;
        }

        /// <summary>
        /// ステータスと比較して、あたったらtrueを返します
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool isHit(TwitterNotifyStatus status)
        {
            if (this.Favorited && status.notify_event == "favorite")
                return true;
            if (this.UnFavorited && status.notify_event == "unfavorite")
                return true;
            if (this.Followed && status.notify_event == "follow")
                return true;
            if (this.Unfollowed && status.notify_event == "unfollow")
                return true;
            if (!this.Favorited && !this.UnFavorited && !this.Followed && !this.Unfollowed)
                return true;
            return false;
        }

        public object Clone()
        {
            var dest = new NotifyFilter();
            dest.Favorited = this.Favorited;
            dest.UnFavorited = this.UnFavorited;
            dest.Followed = this.Followed;
            dest.Unfollowed = this.Unfollowed;
            return dest;
        }
    }
}
