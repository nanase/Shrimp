
using Shrimp.Twitter.Status;
using Shrimp.ControlParts.Toolstrip;
using System.Drawing;
namespace Shrimp.Module
{
    class APIIntroduction
    {
        public static ToolStripStatusLabelText retNotifyIntro ( TwitterNotifyStatus notify )
        {
            if ( notify == null )
                return null;
            if ( notify.isFavToMe )
            {
                var tweet = notify.target_object as TwitterStatus;
                return new ToolStripStatusLabelText ( "@" + notify.source.screen_name + "にお気に入り追加されました 「"+ tweet.DynamicTweet.text +"」", (Bitmap)Setting.ResourceImages.Fav.hover );
            }
            else if ( notify.isUnFavToMe )
            {
                var tweet = notify.target_object as TwitterStatus;
                return new ToolStripStatusLabelText ( "@" + notify.source.screen_name + "にお気に入り削除されました 「" + tweet.DynamicTweet.text + "」", (Bitmap)Setting.ResourceImages.UnFav.hover );
            }
            else if ( notify.isFollowToMe )
            {
                return new ToolStripStatusLabelText ( "@" + notify.source.screen_name + "にフォローされました", (Bitmap)Setting.ResourceImages.UserImage );
            }
            else if ( notify.isRetweetedCategory )
            {
                var tweet = notify.target_object as TwitterStatus;
                return new ToolStripStatusLabelText ( "@" + notify.source.screen_name + "にリツイートされました 「" + tweet.DynamicTweet.text + "」", (Bitmap)Setting.ResourceImages.Retweet.hover );
            }
            return null;
        }

        /// <summary>
        /// TwitterAPIの情報を返す
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static string retTwitterAPIIntro(string uri)
        {
            if (uri.IndexOf("mentions_timeline.json") != -1)
            {
                return "返信を取得";
            }
            if (uri.IndexOf("user_timeline.json") != -1)
            {
                return "ユーザータイムラインを取得";
            }
            if (uri.IndexOf("favorites/list.json") != -1)
            {
                return "お気に入りを取得";
            }
            if (uri.IndexOf("favorites/destroy.json") != -1)
            {
                return "お気に入りを削除";
            }
            if (uri.IndexOf("home_timeline.json") != -1)
            {
                return "タイムラインを取得";
            }
            if (uri.IndexOf("statuses/retweet") != -1)
            {
                return "リツイート";
            }
            if (uri.IndexOf("favorites/create.json") != -1)
            {
                return "お気に入りに登録";
            }
            if (uri.IndexOf("statuses/destroy") != -1)
            {
                return "ツイートを削除";
            }
            if (uri.IndexOf("users/report_spam.json") != -1)
            {
                return "スパム報告";
            }
            if (uri.IndexOf("friendships/create.json") != -1)
            {
                return "フォロー";
            }
            if (uri.IndexOf("blocks/create.json") != -1)
            {
                return "ブロック";
            }
            if (uri.IndexOf("users/show.json") != -1)
            {
                return "ユーザ情報の取得";
            }
            return null;
        }
    }
}
