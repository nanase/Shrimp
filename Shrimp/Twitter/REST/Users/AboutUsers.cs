using System;
using System.Collections.Generic;
using System.Threading;
using OAuth;
using Shrimp.Twitter.Status;

namespace Shrimp.Twitter.REST.Users
{
    class AboutUsers : TwitterWorker, IDisposable
    {
        #region 定義
        private Thread reportSpamResult, UserShowResult, friendshipsResult, friendResult, followerResult;
        private bool isDisposed;
        #endregion

        #region コンストラクタ
        ~AboutUsers()
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
                WaitResult(reportSpamResult);
                WaitResult(UserShowResult);
                isDisposed = true;
            }
        }
        #endregion


        /// <summary>
        /// スパム報告します
        /// </summary>
        public void ReportSpam(TwitterInfo srv, TwitterCompletedProcessDelegate completedDelegate, TwitterErrorProcessDelegate errorProcess, string screen_name, decimal user_id)
        {
            List<OAuthBase.QueryParameter> q = new List<OAuthBase.QueryParameter>();
            if (screen_name != null)
                q.Add(new OAuthBase.QueryParameter("screen_name", screen_name));
            if (user_id != 0)
                q.Add(new OAuthBase.QueryParameter("user_id", "" + user_id + ""));
            this.reportSpamResult = base.loadAsync(srv, "POST", workerResult, completedDelegate, errorProcess, "users/report_spam.json", q);
        }

        /// <summary>
        /// フォローします
        /// </summary>
        public void FollowUser(TwitterInfo srv, TwitterCompletedProcessDelegate completedDelegate, TwitterErrorProcessDelegate errorProcess, string screen_name, decimal user_id)
        {
            List<OAuthBase.QueryParameter> q = new List<OAuthBase.QueryParameter>();
            if (screen_name != null)
                q.Add(new OAuthBase.QueryParameter("screen_name", screen_name));
            if (user_id != 0)
                q.Add(new OAuthBase.QueryParameter("user_id", "" + user_id + ""));
            this.reportSpamResult = base.loadAsync(srv, "POST", workerResult, completedDelegate, errorProcess, "friendships/create.json", q);
        }

        /// <summary>
        /// ブロックします
        /// </summary>
        public void BlockUser(TwitterInfo srv, TwitterCompletedProcessDelegate completedDelegate, TwitterErrorProcessDelegate errorProcess, string screen_name, decimal user_id)
        {
            List<OAuthBase.QueryParameter> q = new List<OAuthBase.QueryParameter>();
            if (screen_name != null)
                q.Add(new OAuthBase.QueryParameter("screen_name", screen_name));
            if (user_id != 0)
                q.Add(new OAuthBase.QueryParameter("user_id", "" + user_id + ""));
            this.reportSpamResult = base.loadAsync(srv, "POST", workerResult, completedDelegate, errorProcess, "blocks/create.json", q);
        }

        /// <summary>
        /// ユーザーデータを取得する
        /// </summary>
        /// <param name="srv"></param>
        /// <param name="tab"></param>
        /// <param name="screen_name"></param>
        /// <param name="user_id"></param>
        public void UserShow(TwitterInfo srv, TwitterCompletedProcessDelegate completedDelegate, TwitterErrorProcessDelegate errorProcess, string screen_name, decimal user_id)
        {
            List<OAuthBase.QueryParameter> q = new List<OAuthBase.QueryParameter>();
            if (screen_name != null)
                q.Add(new OAuthBase.QueryParameter("screen_name", screen_name));
            if (user_id != 0)
                q.Add(new OAuthBase.QueryParameter("user_id", "" + user_id + ""));
            this.UserShowResult = base.loadAsync(srv, "GET", workerUserShowResult, completedDelegate, errorProcess, "users/show.json", q);
        }

        /// <summary>
        /// フォロー状況を確認する
        /// </summary>
        /// <param name="srv"></param>
        /// <param name="tab"></param>
        /// <param name="screen_name"></param>
        /// <param name="user_id"></param>
        public void FriendsShips ( TwitterInfo srv, TwitterCompletedProcessDelegate completedDelegate, TwitterErrorProcessDelegate errorProcess, string screen_name, decimal user_id )
        {
            List<OAuthBase.QueryParameter> q = new List<OAuthBase.QueryParameter> ();
            if ( screen_name != null )
                q.Add ( new OAuthBase.QueryParameter ( "target_screen_name", screen_name ) );
            if ( user_id != 0 )
                q.Add ( new OAuthBase.QueryParameter ( "target_id", "" + user_id + "" ) );
            this.friendshipsResult = base.loadAsync ( srv, "GET", workerUserShowResult, completedDelegate, errorProcess, "friendships/show.json", q );
        }

        /// <summary>
        /// フォローしているユーザを取得する
        /// </summary>
        /// <param name="srv"></param>
        /// <param name="tab"></param>
        /// <param name="screen_name"></param>
        /// <param name="user_id"></param>
        public Thread FollowUser ( TwitterInfo srv, TwitterCompletedProcessDelegate completedDelegate, TwitterErrorProcessDelegate errorProcess, decimal cursor )
        {
            List<OAuthBase.QueryParameter> q = new List<OAuthBase.QueryParameter> ();
            if ( cursor != 0 )
                q.Add ( new OAuthBase.QueryParameter ( "cursor", ""+ cursor ) );
            q.Add ( new OAuthBase.QueryParameter ( "count", "200" ) );
            this.friendResult = base.loadAsync ( srv, "GET", workerFriendsResult, completedDelegate, errorProcess, "friends/list.json", q );
            return this.friendResult;
        }

        /// <summary>
        /// フォローされているユーザを取得する
        /// </summary>
        /// <param name="srv"></param>
        /// <param name="tab"></param>
        /// <param name="screen_name"></param>
        /// <param name="user_id"></param>
        public Thread FollowerUser ( TwitterInfo srv, TwitterCompletedProcessDelegate completedDelegate, TwitterErrorProcessDelegate errorProcess, decimal cursor )
        {
            List<OAuthBase.QueryParameter> q = new List<OAuthBase.QueryParameter> ();
            if ( cursor != 0 )
                q.Add ( new OAuthBase.QueryParameter ( "cursor", ""+ cursor ) );
            q.Add ( new OAuthBase.QueryParameter ( "count", "200" ) );
            this.followerResult = base.loadAsync ( srv, "GET", workerFriendsResult, completedDelegate, errorProcess, "followers/list.json", q );
            return this.followerResult;
        }

        /// <summary>
        /// プロフィールをアップデートします
        /// </summary>
        /// <param name="srv"></param>
        /// <param name="completedDelegate"></param>
        /// <param name="errorProcess"></param>
        /// <param name="screen_name"></param>
        /// <param name="user_id"></param>
        public void ProfileUpdate ( TwitterInfo srv, TwitterCompletedProcessDelegate completedDelegate, TwitterErrorProcessDelegate errorProcess, string name, string url, string location, string description )
        {
            List<OAuthBase.QueryParameter> q = new List<OAuthBase.QueryParameter> ();
            if ( name != null )
                q.Add ( new OAuthBase.QueryParameter ( "name", name ) );
            if ( url != null )
                q.Add ( new OAuthBase.QueryParameter ( "url", url ) );
            if ( location != null )
                q.Add ( new OAuthBase.QueryParameter ( "location", location ) );
            if ( description != null )
                q.Add ( new OAuthBase.QueryParameter ( "description", description ) );
            this.UserShowResult = base.loadAsync ( srv, "POST", workerUserShowResult, completedDelegate, errorProcess, "account/update_profile.json", q );

        }

        /// <summary>
        /// 受信したデータを処理する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private object workerResult(dynamic data)
        {
            return null;
        }

        /// <summary>
        /// 受信したユーザーデータを処理する
        /// </summary>
        /// <param name="user_data"></param>
        /// <returns></returns>
        private object workerUserShowResult(dynamic user_data)
        {
            if (user_data != null)
                return new TwitterUserStatus(user_data);
            return null;
        }


        /// <summary>
        /// 受信したユーザーデータを処理する
        /// </summary>
        /// <param name="user_data"></param>
        /// <returns></returns>
        private object workerFriendsResult ( dynamic user_data )
        {
            if ( user_data != null )
            {
                var dest = new TwitterFriendshipResult ();
                dest.next_cursor = decimal.Parse ( user_data.next_cursor_str );
                foreach ( dynamic data in user_data.users )
                {
                    dest.Add ( new TwitterUserStatus ( data ) );
                }
                return dest;
            }

            return null;
        }
    }
}
