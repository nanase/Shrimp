using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OAuth;
using Shrimp.Twitter.Status;
using System.Threading;

namespace Shrimp.Twitter.REST.Search
{
    class search : TwitterWorker, IDisposable
    {
        #region 定義
        private Thread searchResult;
        private bool isDisposed;
        #endregion

        #region コンストラクタ
        ~search ()
        {
            this.Dispose ();
        }

        /// <summary>
        /// オブジェクト解放
        /// </summary>
        public void Dispose()
        {
            if (!isDisposed)
            {
                WaitResult ( searchResult );
                isDisposed = true;
            }
        }
        #endregion

        /// <summary>
        /// リストを取得
        /// </summary>
        public void searchTweet ( TwitterInfo srv, TwitterCompletedProcessDelegate completedDelegate, TwitterErrorProcessDelegate errorProcess, string query )
        {
            List<OAuthBase.QueryParameter> q = new List<OAuthBase.QueryParameter> ();
            if ( query != null )
                q.Add ( new OAuthBase.QueryParameter ( "q", query ) );
            q.Add ( new OAuthBase.QueryParameter ( "include_entities", "true" ) );
            q.Add ( new OAuthBase.QueryParameter ( "result_type", "mixed" ) );
            q.Add ( new OAuthBase.QueryParameter ( "count", "100" ) );
            this.searchResult = base.loadAsync ( srv, "GET", workerResult, completedDelegate, errorProcess,"search/tweets.json", q );
        }


        /// <summary>
        /// 受信したデータを処理する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private object workerResult ( dynamic data )
        {
            if ( data == null )
                return null;
            var result_status = new List<TwitterStatus> ();
            foreach ( var tweet in data.statuses )
            {
                if ( tweet.IsDefined ( "id" ) )
                    result_status.Add ( new TwitterStatus ( tweet ) );
            }
            return result_status;
        }
    }
}
