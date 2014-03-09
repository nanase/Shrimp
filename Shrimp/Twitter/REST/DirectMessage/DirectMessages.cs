using System;
using System.Collections.Generic;
using System.Threading;
using OAuth;
using Shrimp.Twitter.Status;

namespace Shrimp.Twitter.REST.DirectMessage
{
    class DirectMessages : TwitterWorker, IDisposable
    {
        #region 定義
        private Thread SendDirectMessageResult, GetDirectMessageResult, GetSendDirectMessageResult, DestroyDirectMessageResult;
        private bool isDisposed;
        #endregion

        #region コンストラクタ
        ~DirectMessages()
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
                WaitResult(SendDirectMessageResult);
                WaitResult(GetDirectMessageResult);
                WaitResult(GetSendDirectMessageResult);
                WaitResult(DestroyDirectMessageResult);
                isDisposed = true;
            }
        }
        #endregion

        /// <summary>
        /// ロードシンク
        /// </summary>
        public void SendDirectMessage(TwitterInfo srv, TwitterCompletedProcessDelegate completedDelegate, TwitterErrorProcessDelegate errorProcess, string screen_name, decimal user_id, string text)
        {
            List<OAuthBase.QueryParameter> q = new List<OAuthBase.QueryParameter>();
            if (screen_name != null)
                q.Add(new OAuthBase.QueryParameter("screen_name", screen_name));
            if (user_id != 0)
                q.Add(new OAuthBase.QueryParameter("user_id", "" + user_id + ""));
            q.Add(new OAuthBase.QueryParameter("text", text));
            this.SendDirectMessageResult = base.loadAsync(srv, "POST", workerResult2, completedDelegate, errorProcess, "direct_messages/new.json", q);
        }

        /// <summary>
        /// 自分が送ったダイレクトメッセージを受信する
        /// </summary>
        /// <param name="srv"></param>
        /// <param name="completedDelegate"></param>
        /// <param name="errorProcess"></param>
        /// <param name="screen_name"></param>
        /// <param name="user_id"></param>
        /// <param name="text"></param>
        public void GetSentDirectMessage(TwitterInfo srv, TwitterCompletedProcessDelegate completedDelegate, TwitterErrorProcessDelegate errorProcess, decimal since_id, decimal max_id)
        {
            List<OAuthBase.QueryParameter> q = new List<OAuthBase.QueryParameter>();
            if (since_id > 0)
                q.Add(new OAuthBase.QueryParameter("since_id", since_id.ToString()));
            if (max_id > 0)
                q.Add(new OAuthBase.QueryParameter("max_id", "" + max_id + ""));
            q.Add(new OAuthBase.QueryParameter("count", "200"));
            this.GetSendDirectMessageResult = base.loadAsync(srv, "GET", workerResult, completedDelegate, errorProcess, "direct_messages/sent.json", q);
        }

        /// <summary>
        /// ロードシンク
        /// </summary>
        public void GetDirectMessage(TwitterInfo srv, TwitterCompletedProcessDelegate completedDelegate, TwitterErrorProcessDelegate errorProcess, decimal since_id, decimal max_id)
        {
            List<OAuthBase.QueryParameter> q = new List<OAuthBase.QueryParameter>();
            if (since_id > 0)
                q.Add(new OAuthBase.QueryParameter("since_id", since_id.ToString()));
            if (max_id > 0)
                q.Add(new OAuthBase.QueryParameter("max_id", "" + max_id + ""));
            q.Add(new OAuthBase.QueryParameter("count", "200"));
            this.GetDirectMessageResult = base.loadAsync(srv, "GET", workerResult, completedDelegate, errorProcess, "direct_messages.json", q);
        }

        /// <summary>
        /// ダイレクトメッセージを破棄する
        /// </summary>
        /// <param name="srv"></param>
        /// <param name="completedDelegate"></param>
        /// <param name="errorProcess"></param>
        /// <param name="id"></param>
        public void DestroyDirectMessage(TwitterInfo srv, TwitterCompletedProcessDelegate completedDelegate, TwitterErrorProcessDelegate errorProcess, decimal id)
        {
            List<OAuthBase.QueryParameter> q = new List<OAuthBase.QueryParameter>();
            q.Add(new OAuthBase.QueryParameter("id", "" + id + ""));
            this.DestroyDirectMessageResult = base.loadAsync(srv, "POST", workerResult2, completedDelegate, errorProcess, "direct_messages/destroy.json", q);
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
            List<TwitterDirectMessageStatus> dest = new List<TwitterDirectMessageStatus>();
            foreach (dynamic tmp in data)
            {
                TwitterDirectMessageStatus t = new TwitterDirectMessageStatus(tmp);
                dest.Add(t);
            }
            return dest;
        }

        private object workerResult2(dynamic data)
        {
            return data;
        }
    }
}
