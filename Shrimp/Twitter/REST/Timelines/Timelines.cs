using System;
using System.Collections.Generic;
using System.Threading;
using OAuth;
using Shrimp.Twitter.Status;

namespace Shrimp.Twitter.REST.Timelines
{
    class Timelines : TwitterWorker, IDisposable
    {
        #region 定義
        private Thread HomeTimelineResult, MentionTimelineResult, UserTimelineResult, FavoriteTimelineResult,
            ShowTweetResult;
        private bool isDisposed;
        #endregion

        #region コンストラクタ
        ~Timelines()
        {
            this.Dispose();
        }

        /// <summary>
        /// オブジェクト解放
        /// </summary>
        public void Dispose()
        {
            if (!isDisposed)
            {
                WaitResult(HomeTimelineResult);
                WaitResult(MentionTimelineResult);
                WaitResult(UserTimelineResult);
                WaitResult(FavoriteTimelineResult);
                WaitResult(ShowTweetResult);
                isDisposed = true;
            }
        }
        #endregion

        /// <summary>
        /// ロードシンク
        /// </summary>
        public void HomeTimeline(TwitterInfo srv, TwitterCompletedProcessDelegate completedProcess, TwitterErrorProcessDelegate errorProcess, decimal since_id)
        {
            List<OAuthBase.QueryParameter> q = new List<OAuthBase.QueryParameter>();
            q.Add(new OAuthBase.QueryParameter("count", "200"));
            q.Add(new OAuthBase.QueryParameter("include_entities", "true"));
            if (since_id > 0)
                q.Add(new OAuthBase.QueryParameter("since_id", since_id.ToString()));
            this.HomeTimelineResult = base.loadAsync(srv, "GET", workerResult, completedProcess, errorProcess, "statuses/home_timeline.json", q);
        }

        /// <summary>
        /// メンション取得
        /// </summary>
        /// <param name="srv"></param>
        /// <param name="tab"></param>
        public void MentionTimeline(TwitterInfo srv, TwitterCompletedProcessDelegate completedProcess, TwitterErrorProcessDelegate errorProcess, decimal since_id)
        {
            List<OAuthBase.QueryParameter> q = new List<OAuthBase.QueryParameter>();
            q.Add(new OAuthBase.QueryParameter("count", "200"));
            q.Add(new OAuthBase.QueryParameter("include_entities", "true"));
            if (since_id > 0)
                q.Add(new OAuthBase.QueryParameter("since_id", since_id.ToString()));
            this.MentionTimelineResult = base.loadAsync(srv, "GET", workerResult, completedProcess, errorProcess, "statuses/mentions_timeline.json", q);
        }

        /// <summary>
        /// ユーザータイムライン
        /// </summary>
        /// <param name="srv"></param>
        /// <param name="tab"></param>
        public void UserTimeline(TwitterInfo srv, TwitterCompletedProcessDelegate completedProcess, TwitterErrorProcessDelegate errorProcess, string screen_name, decimal id)
        {
            List<OAuthBase.QueryParameter> q = new List<OAuthBase.QueryParameter>();
            q.Add(new OAuthBase.QueryParameter("count", "200"));
            q.Add(new OAuthBase.QueryParameter("include_entities", "true"));
            if (screen_name != null)
                q.Add(new OAuthBase.QueryParameter("screen_name", screen_name));
            if (id > 0)
                q.Add(new OAuthBase.QueryParameter("id", "" + id + ""));
            this.UserTimelineResult = base.loadAsync(srv, "GET", workerResult, completedProcess, errorProcess, "statuses/user_timeline.json", q);
        }

        /// <summary>
        /// ふぁぼりてタイムライン
        /// </summary>
        /// <param name="srv"></param>
        /// <param name="tab"></param>
        public void FavoriteTimeline(TwitterInfo srv, TwitterCompletedProcessDelegate completedProcess, TwitterErrorProcessDelegate errorProcess, string screen_name, decimal id)
        {
            List<OAuthBase.QueryParameter> q = new List<OAuthBase.QueryParameter>();
            q.Add(new OAuthBase.QueryParameter("count", "200"));
            q.Add(new OAuthBase.QueryParameter("include_entities", "true"));
            if (screen_name != null)
                q.Add(new OAuthBase.QueryParameter("screen_name", screen_name));
            if (id > 0)
                q.Add(new OAuthBase.QueryParameter("id", "" + id + ""));
            this.FavoriteTimelineResult = base.loadAsync(srv, "GET", workerResult, completedProcess, errorProcess, "favorites/list.json", q);
        }

        /// <summary>
        /// ツイートを取得する
        /// </summary>
        /// <param name="srv"></param>
        /// <param name="completedProcess"></param>
        /// <param name="id"></param>
        public void TweetShow(TwitterInfo srv, TwitterCompletedProcessDelegate completedProcess, TwitterErrorProcessDelegate errorProcess, decimal id)
        {
            List<OAuthBase.QueryParameter> q = new List<OAuthBase.QueryParameter>();
            q.Add(new OAuthBase.QueryParameter("include_entities", "true"));
            if (id > 0)
                q.Add(new OAuthBase.QueryParameter("id", "" + id + ""));
            this.ShowTweetResult = base.loadAsync(srv, "GET", workerResultTweet, completedProcess, errorProcess, "statuses/show.json", q);
        }

        /// <summary>
        /// 受信したデータを処理する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private object workerResult(dynamic data)
        {
            if (data == null)
                return null;
            var result_status = new List<TwitterStatus>();
            foreach (var tweet in data)
            {
                if (tweet.IsDefined("id"))
                    result_status.Add(new TwitterStatus(tweet));
            }
            return result_status;
        }


        /// <summary>
        /// 受信したデータを処理する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private object workerResultTweet(dynamic data)
        {
            if (data == null)
                return null;
            return new TwitterStatus(data);
        }

    }
}
