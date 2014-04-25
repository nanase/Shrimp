using System;
using System.Collections.Generic;

namespace Shrimp.Twitter.Status
{
    public class TwitterNotifyStatus : ICloneable
    {
        public TwitterNotifyStatus()
        {
        }

        public TwitterNotifyStatus(TwitterUserStatus source, TwitterUserStatus target, TwitterStatus status)
        {
            //  リツイート
            this.source = source;
            this.target = target;
            this.target_object = status;
            this.notify_event = "retweeted";
        }

        public TwitterNotifyStatus(TwitterInfo srv, dynamic raw_data)
        {
            if (raw_data.IsDefined("target") && raw_data.target != null)
                this.target = new TwitterUserStatus(raw_data.target);
            if (raw_data.IsDefined("source") && raw_data.source != null)
                this.source = new TwitterUserStatus(raw_data.source);
            this.notify_event = (raw_data.IsDefined("event") ? raw_data["event"] : null);
            if (raw_data.IsDefined("target_object") && raw_data.target_object != null)
                this.target_object = raw_data.target_object;
            this.created_at = DateTime.ParseExact(
                                        raw_data.created_at,
                                        "ddd MMM dd HH:mm:ss K yyyy",
                                        System.Globalization.DateTimeFormatInfo.InvariantInfo);
            this.SetParameter ( srv, null );
        }

        /// <summary>
        /// ターゲット
        /// </summary>
        public TwitterUserStatus target
        {
            get;
            set;
        }

        /// <summary>
        /// 元ユーザー
        /// </summary>
        public TwitterUserStatus source
        {
            get;
            set;
        }

        /// <summary>
        /// イベントの種類
        /// </summary>
        public string notify_event
        {
            get;
            set;
        }

        /// <summary>
        /// ターゲットへのオブジェクト(TweetかList)
        /// </summary>
        public object target_object
        {
            get;
            set;
        }

        /// <summary>
        /// 作成日
        /// </summary>
        public DateTime created_at
        {
            get;
            set;
        }

        /// <summary>
        /// パラメータを設定するコマンド
        /// </summary>
        /// <param name="srv"></param>
        public bool SetParameter ( TwitterInfo srv, List<TwitterInfo> srvs )
        {
            //  TwitterInfoを元に、パラメータをセットする
            bool res = false;
            if ( (srv != null && this.source.id == srv.UserId) || (srvs != null &&
                srvs.Find ( (sv)=> sv.UserId == this.source.id ) != null ) )
            {
                //
                if ( this.isFavoriteCategory )
                    res = this.isOwnFav = true;
                if ( this.isUnfavoriteCategory )
                    res = this.isOwnUnFav = true;
                if ( this.isFollowCategory )
                    res = this.isOwnFollow = true;
                if ( this.isUnFollowCategory )
                    res = this.isOwnUnFollow = true;

            }
            else if ( ( srv != null && this.target.id == srv.UserId ) || ( srvs != null &&
              srvs.Find ( ( sv ) => sv.UserId == this.target.id ) != null ) )
            {
                //
                if ( this.isFavoriteCategory )
                    res = this.isFavToMe = true;
                if ( this.isUnfavoriteCategory )
                    res = this.isUnFavToMe = true;
                if ( this.isFollowCategory )
                    res = this.isFollowToMe = true;
                if ( this.isUnFollowCategory )
                    res = this.isUnFollowToMe = true;
            }
            else
            {
                //
                if ( this.isFavoriteCategory )
                    res = this.isFav = true;
                if ( this.isUnfavoriteCategory )
                    res = this.isUnFav = true;
                if ( this.isFollowCategory )
                    res = this.isFollow = true;
                if ( this.isUnFollowCategory )
                    res = this.isUnFollow = true;
            }
            return res;
        }

        /// <summary>
        /// パラメータを複数セットする
        /// </summary>
        /// <param name="srvs"></param>
        public void SetParameters ( List<TwitterInfo> srvs )
        {
            this.SetParameter ( null, srvs );
        }

        /// <summary>
        /// ふぁぼ
        /// </summary>
        public bool isFav
        {
            get;
            set;
        }

        /// <summary>
        /// あんふぁぼ
        /// </summary>
        public bool isUnFav
        {
            get;
            set;
        }

        /// <summary>
        /// 自分宛のふぁぼ
        /// </summary>
        public bool isFavToMe
        {
            get;
            set;
        }

        /// <summary>
        /// 自分宛のあんふぁぼ
        /// </summary>
        public bool isUnFavToMe
        {
            get;
            set;
        }

        /// <summary>
        /// 自分が発行したふぁぼ
        /// </summary>
        public bool isOwnFav
        {
            get;
            set;
        }

        /// <summary>
        /// 自分で発行したあんふぁぼ
        /// </summary>
        public bool isOwnUnFav
        {
            get;
            set;
        }

        /// <summary>
        /// フォロー
        /// </summary>
        public bool isFollow
        {
            get;
            set;
        }

        /// <summary>
        /// アンフォロー
        /// </summary>
        public bool isUnFollow
        {
            get;
            set;
        }

        /// <summary>
        /// 自分宛のフォロー
        /// </summary>
        public bool isFollowToMe
        {
            get;
            set;
        }

        /// <summary>
        /// 自分宛のアンフォロー
        /// </summary>
        public bool isUnFollowToMe
        {
            get;
            set;
        }

        /// <summary>
        /// 自分のフォロー
        /// </summary>
        public bool isOwnFollow
        {
            get;
            set;
        }

        /// <summary>
        /// 自分のアンフォロー
        /// </summary>
        public bool isOwnUnFollow
        {
            get;
            set;
        }
        /// <summary>
		/// 通知の種類がふぁぼかどうか
		/// </summary>
        public bool isFavoriteCategory
        {
            get
            {
                return (this.notify_event == "favorite");
            }
        }

        /// <summary>
		/// 通知の種類があんふぁぼかどうか
		/// </summary>
        public bool isUnfavoriteCategory
        {
            get
            {
                return (this.notify_event == "unfavorite");
            }
        }

		/// <summary>
		/// 通知の種類がフォロー・アンフォローであるかどうか
		/// </summary>
        public bool isFollowsCategory
        {
            get
            {
                return ( this.isFollowCategory || this.isUnFollowCategory );
            }
        }

        /// <summary>
        /// 通知の種類がフォローかどうか
        /// </summary>
        public bool isFollowCategory
        {
            get
            {
                return (this.notify_event == "follow");
            }
        }

        /// <summary>
        /// 通知の種類がアンフォローかどうか
        /// </summary>
        public bool isUnFollowCategory
        {
            get
            {
                return (this.notify_event == "unfollow");
            }
        }

		/// <summary>
		/// 通知の種類が、リツイートであるかどうか
		/// </summary>
        public bool isRetweetedCategory
        {
            get
            {
                return (this.notify_event == "retweeted");
            }
        }

		/// <summary>
		/// 通知の種類が、プロフィールの更新であるかどうか
		/// </summary>
        public bool isUpdateProfileCategory
        {
            get
            {
                return ( this.notify_event == "user_update" );
            }
        }

        public object Clone()
        {
            var dest = new TwitterNotifyStatus();
            dest.isFav = this.isFav;
            dest.isFavToMe = this.isFavToMe;
            dest.isOwnFav = this.isOwnFav;
            dest.isOwnUnFav = this.isOwnUnFav;
            dest.isUnFav = this.isUnFav;
            dest.isUnFavToMe = this.isUnFavToMe;

            dest.isFollow = this.isFollow;
            dest.isUnFollow = this.isUnFollow;
            dest.isOwnFollow = this.isOwnFollow;
            dest.isOwnUnFollow = this.isOwnUnFollow;
            dest.isFollowToMe = this.isFollowToMe;
            dest.isUnFollowToMe = this.isUnFollowToMe;

            dest.notify_event = (string)this.notify_event.Clone();
            dest.source = (TwitterUserStatus)this.source.Clone();
            dest.target = (TwitterUserStatus)this.target.Clone();
            dest.target_object = (this.target_object != null ? ((TwitterStatus)this.target_object).Clone() : null);
            return dest;
        }
    }
}
