using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shrimp.ControlParts.Tabs;
using OAuth;
using Shrimp.Twitter.Status;
using System.Threading;

namespace Shrimp.Twitter.REST.Users
{
    class AboutUsers : TwitterWorker, IDisposable
    {
        #region 定義
        private Thread reportSpamResult, UserShowResult;
        private bool isDisposed;
        #endregion

        #region コンストラクタ
        ~AboutUsers ()
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
                WaitResult ( reportSpamResult );
                WaitResult ( UserShowResult );
                isDisposed = true;
            }
        }
        #endregion


        /// <summary>
        /// スパム報告します
        /// </summary>
        public void ReportSpam ( TwitterInfo srv, TwitterCompletedProcessDelegate completedDelegate, TwitterErrorProcessDelegate errorProcess, string screen_name, decimal user_id )
        {
            List<OAuthBase.QueryParameter> q = new List<OAuthBase.QueryParameter> ();
            if ( screen_name != null )
                q.Add ( new OAuthBase.QueryParameter ( "screen_name", screen_name ) );
            if ( user_id != 0 )
                q.Add ( new OAuthBase.QueryParameter ( "user_id", "" + user_id + "" ) );
            this.reportSpamResult = base.loadAsync ( srv, "POST", workerResult, completedDelegate, errorProcess, "users/report_spam.json", q );
        }

        /// <summary>
        /// フォローします
        /// </summary>
        public void FollowUser ( TwitterInfo srv, TwitterCompletedProcessDelegate completedDelegate, TwitterErrorProcessDelegate errorProcess, string screen_name, decimal user_id )
        {
            List<OAuthBase.QueryParameter> q = new List<OAuthBase.QueryParameter> ();
            if ( screen_name != null )
                q.Add ( new OAuthBase.QueryParameter ( "screen_name", screen_name ) );
            if ( user_id != 0 )
                q.Add ( new OAuthBase.QueryParameter ( "user_id", "" + user_id + "" ) );
            this.reportSpamResult = base.loadAsync ( srv, "POST", workerResult, completedDelegate, errorProcess, "friendships/create.json", q );
        }

        /// <summary>
        /// ブロックします
        /// </summary>
        public void BlockUser ( TwitterInfo srv, TwitterCompletedProcessDelegate completedDelegate, TwitterErrorProcessDelegate errorProcess, string screen_name, decimal user_id )
        {
            List<OAuthBase.QueryParameter> q = new List<OAuthBase.QueryParameter> ();
            if ( screen_name != null )
                q.Add ( new OAuthBase.QueryParameter ( "screen_name", screen_name ) );
            if ( user_id != 0 )
                q.Add ( new OAuthBase.QueryParameter ( "user_id", "" + user_id + "" ) );
            this.reportSpamResult = base.loadAsync ( srv, "POST", workerResult, completedDelegate, errorProcess, "blocks/create.json", q );
        }

        /// <summary>
        /// ユーザーデータを取得する
        /// </summary>
        /// <param name="srv"></param>
        /// <param name="tab"></param>
        /// <param name="screen_name"></param>
        /// <param name="user_id"></param>
        public void UserShow ( TwitterInfo srv, TwitterCompletedProcessDelegate completedDelegate, TwitterErrorProcessDelegate errorProcess, string screen_name, decimal user_id )
        {
            List<OAuthBase.QueryParameter> q = new List<OAuthBase.QueryParameter> ();
            if ( screen_name != null )
                q.Add ( new OAuthBase.QueryParameter ( "screen_name", screen_name ) );
            if ( user_id != 0 )
                q.Add ( new OAuthBase.QueryParameter ( "user_id", "" + user_id + "" ) );
            this.UserShowResult = base.loadAsync ( srv, "GET", workerUserShowResult, completedDelegate, errorProcess, "users/show.json", q );
        }

        /// <summary>
        /// 受信したデータを処理する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private object workerResult ( dynamic data )
        {
            return null;
        }

        /// <summary>
        /// 受信したユーザーデータを処理する
        /// </summary>
        /// <param name="user_data"></param>
        /// <returns></returns>
        private object workerUserShowResult ( dynamic user_data )
        {
            if ( user_data != null )
                return new TwitterUserStatus ( user_data );
            return null;
        }
    }
}
