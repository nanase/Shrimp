using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shrimp.ControlParts.Tabs;
using OAuth;
using System.Threading;
using Shrimp.Module;

namespace Shrimp.Twitter.REST.Tweets
{
    class Statuses : TwitterWorker, IDisposable
    {
        #region 定義
        private Thread UpdateResult, UpdateMediaResult, DeleteResult, RetweetResult, FavoriteResult, UnFavoriteResult;
        private bool isDisposed;
        #endregion

        #region コンストラクタ
        ~Statuses ()
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
                WaitResult ( UpdateResult );
                WaitResult ( UpdateMediaResult );
                WaitResult ( DeleteResult );
                WaitResult ( RetweetResult );
                WaitResult ( FavoriteResult );
                WaitResult ( UnFavoriteResult );
                isDisposed = true;
            }
        }
        #endregion


        /// <summary>
        /// ロードシンク
        /// </summary>
        public void Update ( TwitterInfo srv, TwitterCompletedProcessDelegate completedProcess, TwitterErrorProcessDelegate errorProcess, 
                                string status, decimal in_reply_to_status_id )
        {
            List<OAuthBase.QueryParameter> q = new List<OAuthBase.QueryParameter> ();
            q.Add ( new OAuthBase.QueryParameter ( "status", status ) );
            if ( in_reply_to_status_id != 0 )
                q.Add ( new OAuthBase.QueryParameter ( "in_reply_to_status_id", ""+ in_reply_to_status_id +"" ) );
            this.UpdateResult = base.loadAsync ( srv, "POST", workerResult, completedProcess, errorProcess, "statuses/update.json", q );
        }

        public void UpdateMedia ( TwitterInfo srv, TwitterCompletedProcessDelegate completedProcess, TwitterErrorProcessDelegate errorProcess, 
                                    byte[] data, string fileName, string status, decimal in_reply_to_status_id )
        {
            List<OAuthBase.QueryParameter> q = new List<OAuthBase.QueryParameter> ();
            TwitterUpdateImage image = new TwitterUpdateImage ( fileName, data, status, in_reply_to_status_id );
            this.UpdateMediaResult = base.loadAsync ( srv, "POST", workerResult, completedProcess, errorProcess, "statuses/update_with_media.json", q, image );
        }

        /// <summary>
        /// ツイートを削除する
        /// </summary>
        /// <param name="srv"></param>
        /// <param name="completedProcess"></param>
        /// <param name="id"></param>
        public void Delete ( TwitterInfo srv, TwitterCompletedProcessDelegate completedProcess, TwitterErrorProcessDelegate errorProcess, decimal id )
        {
            this.DeleteResult = base.loadAsync ( srv, "POST", workerResult, completedProcess, errorProcess, "statuses/destroy/"+ id +".json", null );
        }

        /// <summary>
        /// リツイート
        /// </summary>
        /// <param name="srv"></param>
        /// <param name="tab"></param>
        /// <param name="id"></param>
        public void Retweet ( TwitterInfo srv, TwitterCompletedProcessDelegate completedProcess, TwitterErrorProcessDelegate errorProcess, decimal id )
        {
            if ( id <= 0 )
                return;
            this.RetweetResult = base.loadAsync ( srv, "POST", workerResult, completedProcess, errorProcess, "statuses/retweet/" + id + ".json", null );
        }


        /// <summary>
        /// ふぁぼ
        /// </summary>
        /// <param name="srv"></param>
        /// <param name="tab"></param>
        /// <param name="id"></param>
        public void Favorite ( TwitterInfo srv, TwitterCompletedProcessDelegate completedProcess, TwitterErrorProcessDelegate errorProcess, decimal id )
        {
            if ( id <= 0 )
                return;
            List<OAuthBase.QueryParameter> q = new List<OAuthBase.QueryParameter> ();
            q.Add ( new OAuthBase.QueryParameter ( "id", "" + id + "" ) );
            this.FavoriteResult = base.loadAsync ( srv, "POST", workerResult, completedProcess, errorProcess, "favorites/create.json", q );
        }


        /// <summary>
        /// あんふぁぼ
        /// </summary>
        /// <param name="srv"></param>
        /// <param name="tab"></param>
        /// <param name="id"></param>
        public void UnFavorite ( TwitterInfo srv, TwitterCompletedProcessDelegate completedProcess, TwitterErrorProcessDelegate errorProcess, decimal id )
        {
            if ( id <= 0 )
                return;
            List<OAuthBase.QueryParameter> q = new List<OAuthBase.QueryParameter> ();
            q.Add ( new OAuthBase.QueryParameter ( "id", "" + id + "" ) );
            this.UnFavoriteResult = base.loadAsync ( srv, "POST", workerResult, completedProcess, errorProcess, "favorites/destroy.json", q );
        }



        /// <summary>
        /// 受信したデータを処理する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private object workerResult ( dynamic data )
        {
            return data;
        }
    }
}
