using System;
using System.Collections.Generic;

namespace Shrimp.ControlParts.Tabs
{
    public class TabDelivery : ICloneable
    {
        /// <summary>
        /// タイムラインの種類
        /// </summary>
        public TimelineCategory Category;
        /// <summary>
        /// 配達元ユーザー
        /// </summary>
        public List<decimal> DeliveryFromUsers;
        /// <summary>
        /// ツイートを含める条件式
        /// </summary>
        //public string containsTweet;
        /// <summary>
        /// ツイートを除外する条件式
        /// </summary>
        //public string ignoreTweet;

        public TabDelivery()
        {
        }

        public TabDelivery(TimelineCategory Category, List<decimal> DeliveryFromUsers)
        {
            this.Category = (TimelineCategory)Category.Clone();
            this.DeliveryFromUsers = new List<decimal>();
            if (DeliveryFromUsers != null)
            {
                foreach (decimal i in DeliveryFromUsers)
                {
                    this.DeliveryFromUsers.Add(i);
                }
            }
            //this.containsTweet = ( containsTweet == null ? "" : containsTweet );
            //this.ignoreTweet = ( ignoreTweet == null ? "" : ignoreTweet );
        }

        /// <summary>
        /// コピーをとります
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            var dest = new TabDelivery();
            //dest.ignoreTweet = (string)this.ignoreTweet.Clone ();
            //dest.containsTweet = (string)this.containsTweet.Clone ();
            dest.DeliveryFromUsers = new List<decimal>();
            foreach (decimal i in this.DeliveryFromUsers)
            {
                dest.DeliveryFromUsers.Add(i);
            }
            dest.Category = (TimelineCategory)this.Category.Clone();
            return dest;
        }
    }
}
