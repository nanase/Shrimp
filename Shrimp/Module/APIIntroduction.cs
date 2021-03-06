﻿
namespace Shrimp.Module
{
    class APIIntroduction
    {
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
