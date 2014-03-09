using Shrimp.Twitter.Status;

namespace Shrimp.Module
{
    /// <summary>
    /// タイムラインの表示で、特殊文字列を生成します
    /// </summary>
    class TimelineUtil
    {
        /// <summary>
        /// リツイートした趣旨を伝えるテキストと、それに見合うオブジェクトを返す
        /// </summary>
        /// <param name="src"></param>
        /// <param name="retweet_text"></param>
        /// <returns></returns>
        public static TwitterStatus GenerateRetweetStatus(TwitterStatus src, out string retweet_text)
        {
            retweet_text = null;
            if (src.retweeted_status != null)
            {
                retweet_text = "" + src.user.name + "さんがリツイートしました";
                return src.retweeted_status;
            }
            else
            {
                return src;
            }
        }

        /// <summary>
        /// ふぁぼられを伝えるテキストと、それに見合うオブジェクトを返す
        /// </summary>
        /// <param name="src"></param>
        /// <param name="fav_text"></param>
        /// <returns></returns>
        public static TwitterStatus GenerateFavedStatus(TwitterStatus src, out string fav_text)
        {
            fav_text = null;
            if (src.NotifyStatus != null && src.NotifyStatus.isFaved)
            {
                fav_text = "" + src.NotifyStatus.source.name + "にお気に入りに追加されました";
                return new TwitterStatus(src.NotifyStatus.target_object);
            }
            else
            {
                return src;
            }
        }

        /// <summary>
        /// 名前を生成して返します
        /// </summary>
        /// <param name="src"></param>
        /// <param name="retweet_text"></param>
        /// <returns></returns>
        public static string GenerateName(TwitterStatus src)
        {
            if (src == null || src.user == null)
                return null;
            return "" + src.user.name + " / @" + src.user.screen_name + "";
        }
    }
}
