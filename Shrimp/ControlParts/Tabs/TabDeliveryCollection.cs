using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shrimp.Twitter.Status;
using Shrimp.Twitter.REST.List;
using System.Xml.Serialization;

namespace Shrimp.ControlParts.Tabs
{
    public class TabDeliveryCollection : ICloneable
    {
        #region 定義
        public List<TabDelivery> delivery;
        #endregion

        #region コンストラクター
        public TabDeliveryCollection ()
        {
            this.delivery = new List<TabDelivery> ();
        }

        public TabDeliveryCollection ( int capacity )
        {
            this.delivery = new List<TabDelivery> ( capacity );
        }

        public object Clone ()
        {
            var tmp = new TabDeliveryCollection ();
            foreach ( TabDelivery t in this.delivery )
            {
                tmp.AddDelivery ( (TabDelivery)t.Clone () );
            }
            return tmp;
        }
        #endregion

        /// <summary>
        /// 配達内容全部はいってます
        /// </summary>
        [XmlIgnore]
        public List<TabDelivery> deliveries
        {
            get
            {
                return this.delivery;
            }
        }
        /*
        [XmlElement ( "deliveries" )]
        public TabDelivery[] deliveries_
        {
            get { return this.delivery.ToArray(); }
            set { this.delivery = value.ToList(); }
        }
        */

        /// <summary>
        /// 一番上のカテゴリ
        /// </summary>
        [XmlIgnore]
        public TimelineCategories TopCategory
        {
            get
            {
                if ( this.delivery.Count == 0 )
                    return TimelineCategories.None;
                return this.delivery[0].Category.category;
            }
        }

        /// <summary>
        /// タイムラインが、通常のタイムラインかどうか(UserInformationなら、falseが変える)
        /// </summary>
        [XmlIgnore]
        public bool isTimeline
        {
            get
            {
                return ( TopCategory == TimelineCategories.UserInformation ? false : true );
            }
        }

        /// <summary>
        /// カテゴリが含まれているかどうか走査する
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public bool isContainsCategory ( TimelineCategories category )
        {
            foreach ( TabDelivery t in this.delivery )
            {
                if ( t.Category.category == category )
                    return true;
            }
            return false;
        }

        /// <summary>
        /// リストデータを全部拾ってくる
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public listDataCollection FindListData ( decimal user_id )
        {
            listDataCollection dest = null;
            foreach ( TabDelivery tmp in this.deliveries )
            {
                TimelineCategory cat = tmp.Category;
                if ( cat.category != TimelineCategories.ListTimeline )
                    continue;
                if ( cat.ListData != null && cat.ListData.create_user_id == user_id )
                {
                    if ( dest == null )
                        dest = new listDataCollection ();
                    dest.Addlist ( cat.ListData );
                }
            }
            return dest;
        }

        /// <summary>
        /// コレクション内を検索して、条件にマッチするかどうか調べる
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="isAll"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        public bool isMatch ( decimal user_id, TimelineCategories destCategory, object checkObject )
        {
            if ( ( destCategory != TimelineCategories.ListTimeline && destCategory != TimelineCategories.BookmarkTimeline ) && this.deliveries.FindIndex ( ( tabs ) => tabs.Category.isAllUserAccept ) < 0 )
            {
                var isMatchID = this.deliveries.FindIndex ( ( tabs ) =>
                                tabs.DeliveryFromUsers.FindIndex ( ( tmpID ) => tmpID == user_id ) >= 0 ) >= 0;
                if ( !isMatchID )
                    return false;
            }
            if ( this.deliveries.FindIndex ( ( tabs ) => tabs.Category.category == destCategory ) < 0 )
            {
                return false;
            }

            if ( destCategory == TimelineCategories.ListTimeline )
            {
                foreach ( TabDelivery tmp in this.deliveries )
                {
                    TimelineCategory cat = tmp.Category;
                    if ( cat.ListData != null && cat.ListData.list_id == (decimal)checkObject )
                        return true;
                }
                return false;
            }

            if ( destCategory == TimelineCategories.NotifyTimeline )
            {
                foreach ( TabDelivery tmp in this.deliveries )
                {
                    TimelineCategory cat = tmp.Category;
                    if ( cat.notifyFilter != null && cat.notifyFilter.isHit ( (TwitterNotifyStatus)checkObject ) )
                    {
                        return true;
                    }
                }
                return false;
            }

            return true;
        }
        /// <summary>
        /// 配達条件を増やす。
        /// </summary>
        /// <param name="delivery"></param>
        public void AddDelivery ( TabDelivery delivery )
        {
            this.delivery.Add ( delivery );
        }

        /// <summary>
        /// 配達条件を削除する
        /// </summary>
        /// <param name="i"></param>
        public void RemoveDeliveryAt ( int i )
        {
            this.delivery.RemoveAt ( i );
        }
    }
}
