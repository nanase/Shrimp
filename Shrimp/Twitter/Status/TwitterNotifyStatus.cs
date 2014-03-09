using System;

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

        public TwitterNotifyStatus(dynamic raw_data)
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
        /// 独自パラメータ。ふぁぼられたらこれがくる
        /// </summary>
        public bool isFaved
        {
            get;
            set;
        }

        /// <summary>
        /// 他の人へのふぁぼ
        /// </summary>
        public bool isFav
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

        public bool isUnFaved
        {
            get;
            set;
        }

        public bool isUnFav
        {
            get;
            set;
        }

        public bool isOwnUnFav
        {
            get;
            set;
        }

        public bool isFollow
        {
            get
            {
                return (this.notify_event == "follow" || this.notify_event == "unfollow");
            }
        }

        public bool isRetweeted
        {
            get
            {
                return (this.notify_event == "retweeted");
            }
        }

        public object Clone()
        {
            var dest = new TwitterNotifyStatus();
            dest.isFav = this.isFav;
            dest.isFaved = this.isFaved;
            dest.isOwnFav = this.isOwnFav;
            dest.isOwnUnFav = this.isOwnUnFav;
            dest.isUnFav = this.isUnFav;
            dest.isUnFaved = this.isUnFaved;
            dest.notify_event = (string)this.notify_event.Clone();
            dest.source = (TwitterUserStatus)this.source.Clone();
            dest.target = (TwitterUserStatus)this.target.Clone();
            dest.target_object = (this.target_object != null ? ((TwitterStatus)this.target_object).Clone() : null);
            return dest;
        }
    }
}
