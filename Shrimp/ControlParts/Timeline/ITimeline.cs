using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shrimp.Module;
using Shrimp.Twitter.Status;

namespace Shrimp.ControlParts.Timeline
{
    /// <summary>
    /// ITimelineインターフェース
    /// タイムラインのインターフェース
    /// </summary>
    interface ITimeline
    {
        /// <summary>
        /// タイムラインにツイートを送信する
        /// </summary>
        void PushTweet ( TwitterStatus tweet );
        /// <summary>
        /// スタックに詰まれたツイートを拾う
        /// </summary>
        /// <returns></returns>
        TwitterStatus PopTweet ();
        /// <summary>
        /// タイムラインからツイートを削除
        /// </summary>
        void DeleteTimeline(decimal id);
        /// <summary>
        /// 設定が変更されたら飛んでくる
        /// </summary>
        void OnChangeSetting();
    }
}
