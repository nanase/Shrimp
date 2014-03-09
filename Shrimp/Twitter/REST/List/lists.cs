using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OAuth;
using Shrimp.Twitter.Status;
using System.Threading;

namespace Shrimp.Twitter.REST.List
{
    class lists : TwitterWorker, IDisposable
    {
        #region 定義
        private Thread listResult, listStatusesResult;
        private bool isDisposed;
        #endregion

        #region コンストラクタ
        ~lists ()
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
                WaitResult(listResult);
                WaitResult(listStatusesResult);
                isDisposed = true;
            }
        }
        #endregion

        /// <summary>
        /// リストを取得
        /// </summary>
        public void list ( TwitterInfo srv, TwitterCompletedProcessDelegate completedDelegate, TwitterErrorProcessDelegate errorProcess, 
                            string screen_name, decimal user_id )
        {
            List<OAuthBase.QueryParameter> q = new List<OAuthBase.QueryParameter> ();
            if ( screen_name != null )
                q.Add ( new OAuthBase.QueryParameter ( "screen_name", screen_name ) );
            if ( user_id != 0 )
                q.Add ( new OAuthBase.QueryParameter ( "user_id", "" + user_id + "" ) );
            this.listResult = base.loadAsync ( srv, "GET", workerlistResult, completedDelegate, errorProcess,  "lists/list.json", q );
        }


        /// <summary>
        /// リストのタイムラインを取得
        /// </summary>
        public void listStatuses ( TwitterInfo srv, TwitterCompletedProcessDelegate completedDelegate, TwitterErrorProcessDelegate errorProcess, decimal list_id, string slug )
        {
            List<OAuthBase.QueryParameter> q = new List<OAuthBase.QueryParameter> ();
            if ( list_id > 0 )
                q.Add ( new OAuthBase.QueryParameter ( "list_id", "" + list_id + "" ) );
            if ( slug != null )
                q.Add ( new OAuthBase.QueryParameter ( "slug", slug ) );
            q.Add ( new OAuthBase.QueryParameter ( "count", "100" ) );
            q.Add ( new OAuthBase.QueryParameter ( "include_entities", "true" ) );
            this.listStatusesResult = base.loadAsync ( srv, "GET", workerResult, completedDelegate, errorProcess, "lists/statuses.json", q );
        }

        /// <summary>
        /// 受信したデータを処理する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private object workerlistResult ( dynamic data )
        {
            if ( data == null )
                return null;
            var result_list = new listDataCollection ();
            foreach ( var tweet in data )
            {
                result_list.Addlist ( new listData ( Decimal.Parse ( tweet.id_str ),
                    tweet.slug, tweet.name, Decimal.Parse ( tweet.user.id_str ) ) );
            }
            return result_list;
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
            foreach ( var tweet in data )
            {
                if ( tweet.IsDefined ( "id" ) )
                    result_status.Add ( new TwitterStatus ( tweet ) );
            }
            return result_status;
        }
    }
}
