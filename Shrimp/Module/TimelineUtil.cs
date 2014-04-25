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
                retweet_text = "" + src.user.name + "(@"+ src.user.screen_name +")さんがリツイートしました";
                return src.retweeted_status;
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
