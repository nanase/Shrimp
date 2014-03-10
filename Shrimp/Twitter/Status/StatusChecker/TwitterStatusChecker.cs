using System.Collections.Generic;

namespace Shrimp.Twitter.Status.StatusChecker
{
    class TwitterStatusChecker
    {
        /// <summary>
        /// リツイートされたかどうかを判断する
        /// </summary>
        /// <param name="accounts">登録してあるアカウント</param>
        /// <param name="tweet">判断するツイート</param>
        public static void SetIsRetweeted(List<TwitterInfo> accounts, TwitterStatus tweet)
        {
            if (tweet.retweeted_status != null)
            {
                if (accounts.Find((info) => info.UserId == tweet.retweeted_status.user.id) != null)
                {
                    //  リツイートされた
                    var newTweet = (TwitterStatus)tweet.Clone();

                    tweet.retweeted_status = null;
                    tweet.NotifyStatus = new TwitterNotifyStatus(newTweet.user, newTweet.retweeted_status.user, newTweet);

                    tweet.user = (TwitterUserStatus)newTweet.retweeted_status.user.Clone();
                    tweet.text = "【リツイート通知】@" + newTweet.user.screen_name + "にリツイートされました\n" + newTweet.retweeted_status.text + "";
                    tweet.entities = new Twitter.Entities.TwitterEntities(tweet.text);
                }
            }
        }

        /// <summary>
        /// リプライかどうかを判断する
        /// </summary>
        /// <param name="accounts">登録してあるアカウント</param>
        /// <param name="tweet">判断するツイート</param>
        public static void SetIsReply(List<TwitterInfo> accounts, TwitterStatus tweet)
        {
            if (tweet == null)
                return;
            //  リプライかどうか
            var text = tweet.DynamicTweet.text.ToLower();
            TwitterInfo t;
            if ((t = accounts.Find((info) => (text.IndexOf("@" + info.ScreenName.ToLower() + "") >= 0))) != null)
            {
                tweet.DynamicTweet.SetReply(t.UserId);
            }
        }

        /// <summary>
        /// 通知を設定
        /// </summary>
        /// <param name="tweet"></param>
        public static void SetNotify(List<TwitterInfo> accounts, TwitterNotifyStatus tweet)
        {
            if (tweet == null)
                return;
            if (tweet.notify_event == "favorite")
            {
                var sourceUser = tweet.source;
                var targetUser = tweet.target;
                tweet.isFaved = (accounts.FindIndex((t) => t.UserId == targetUser.id) >= 0);
                tweet.isOwnFav = (accounts.FindIndex((t) => t.UserId == sourceUser.id) >= 0);
                tweet.isFav = !(tweet.isFaved | tweet.isOwnFav);
            }

            if (tweet.notify_event == "unfavorite")
            {
                var targetUser = tweet.target;
                var sourceUser = tweet.source;
                tweet.isUnFaved = (accounts.FindIndex((t) => t.UserId == targetUser.id) >= 0);
                tweet.isOwnUnFav = (sourceUser.id == targetUser.id) || (accounts.FindIndex((t) => t.UserId == sourceUser.id) >= 0);
                tweet.isUnFav = !(tweet.isUnFaved | tweet.isOwnUnFav);
            }
        }
    }
}
