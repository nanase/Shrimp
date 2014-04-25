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
        bool PushTweet(TwitterStatus tweet);
        /// <summary>
        /// スタックに詰まれたツイートを拾う
        /// </summary>
        /// <returns></returns>
        TwitterStatus PopTweet();
        /// <summary>
        /// タイムラインからツイートを削除
        /// </summary>
        void DeleteTimeline(decimal id, TwitterNotifyStatus isNotify);
        /// <summary>
        /// 設定が変更されたら飛んでくる
        /// </summary>
        void OnChangeSetting();
    }
}
