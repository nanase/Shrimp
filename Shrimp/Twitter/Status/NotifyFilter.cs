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
        public bool Retweeted;
        public bool OwnFavorited;
        public bool OwnUnFavorited;
        public bool UserUpdated;

        public NotifyFilter()
        {
            this.Favorited = true;
            this.UnFavorited = true;
            this.Followed = true;
            this.Unfollowed = true;
            this.OwnFavorited = true;
            this.OwnUnFavorited = true;
            this.Retweeted = true;
            this.UserUpdated = true;
        }

        /// <summary>
        /// ステータスと比較して、あたったらtrueを返します
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool isHit(TwitterNotifyStatus status)
        {
            if ( this.OwnFavorited && status.isFavToMe )
                return true;
            if ( this.OwnUnFavorited && status.isUnFavToMe )
                return true;
            if ( this.Retweeted && status.isRetweetedCategory )
                return true;
            if (this.Favorited && status.isFav)
                return true;
            if (this.UnFavorited && status.isUnFav)
                return true;
            if (this.Followed && status.isFollowCategory)
                return true;
            if ( this.Unfollowed && status.isUnFollowCategory)
                return true;
            if ( this.UserUpdated && status.isUpdateProfileCategory )
                return true;

            //if ( !this.Favorited && !this.UnFavorited && !this.Followed && !this.Unfollowed && !this.OwnUnFavorited && !this.OwnFavorited && !this.Retweeted && !this.UserUpdated )
            //    return true;
            return false;
        }

        public object Clone()
        {
            var dest = new NotifyFilter();
            dest.Favorited = this.Favorited;
            dest.UnFavorited = this.UnFavorited;
            dest.Followed = this.Followed;
            dest.Unfollowed = this.Unfollowed;
            dest.OwnFavorited = this.OwnFavorited;
            dest.OwnUnFavorited = this.OwnUnFavorited;
            dest.Retweeted = this.Retweeted;
            dest.UserUpdated = this.UserUpdated;
            return dest;
        }
    }
}
