using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shrimp.Twitter.REST.List;
using Shrimp.Twitter.Status;
using System.Xml.Serialization;

namespace Shrimp.ControlParts.Tabs
{
    public enum TimelineCategories
    {
        None,
        Query,
        HomeTimeline,
        MentionTimeline,
        UserTimeline,
        UserFavoriteTimeline,
        UserInformation,
        SearchTimeline,
        ListTimeline,
        NotifyTimeline,
        BookmarkTimeline,
        DirectMessageTimeline
    }

    /// <summary>
    /// タイムラインの種類です
    /// </summary>
    public class TimelineCategory : ICloneable
    {
        /// <summary>
        /// カテゴリの種類
        /// </summary>
        public TimelineCategories category = TimelineCategories.None;
        /// <summary>
        /// カテゴリの詳細（たとえば、HomeTimelineなら、それを発行したユーザーIDなど)
        /// </summary>
        public object categoryDetail = "";
        /// <summary>
        /// 登録してあるアカウントユーザーすべてを適用する
        /// </summary>
        public bool isAllUserAccept = false;

        /// <summary>
        /// リストタイムラインの時のみ使われます。
        /// リストの情報が格納されています。
        /// </summary>
        public listData ListData;

        /// <summary>
        /// 通知が選択されたときのみ使われます。
        /// 通知の情報が格納されています。
        /// </summary>
        public NotifyFilter notifyFilter;

        public TimelineCategory ()
        {
        }

        public TimelineCategory ( TimelineCategories category, object categoryDetail, bool isAllUserAccept )
        {
            this.category = category;
            this.categoryDetail = categoryDetail;
            this.isAllUserAccept = isAllUserAccept;
            this.notifyFilter = new NotifyFilter ();
        }

        public object Clone ()
        {
            TimelineCategory dest = new TimelineCategory ();
            dest.category = this.category;
            dest.categoryDetail = this.categoryDetail;
            dest.isAllUserAccept = this.isAllUserAccept;
            if ( this.ListData != null )
                dest.ListData = (listData)this.ListData.Clone ();
            if ( this.notifyFilter != null )
                dest.notifyFilter = (NotifyFilter)this.notifyFilter.Clone ();
            return dest;
        }

        /// <summary>
        /// カテゴリ名を文字列で。
        /// </summary>
        [XmlIgnore]
        public string CategoryName
        {
            get
            {
                if ( this.category == TimelineCategories.None )
                    return "なし";
                if ( this.category == TimelineCategories.DirectMessageTimeline )
                    return "ダイレクトメッセージ";
                if ( this.category == TimelineCategories.HomeTimeline )
                    return "ホームタイムライン";
                if ( this.category == TimelineCategories.ListTimeline )
                    return "リストタイムライン";
                if ( this.category == TimelineCategories.MentionTimeline )
                    return "返信";
                if ( this.category == TimelineCategories.NotifyTimeline )
                    return "通知";
                if ( this.category == TimelineCategories.SearchTimeline )
                    return "検索";
                if ( this.category == TimelineCategories.UserTimeline )
                    return "ユーザータイムライン";
                return "なし";
            }
        }
    }
}
