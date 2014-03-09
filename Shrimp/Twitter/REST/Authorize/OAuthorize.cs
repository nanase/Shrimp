using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;

namespace Shrimp.Twitter.REST.Authorize
{
    class OAuthorize : TwitterWorker, IDisposable
    {
        #region 定義
        private Thread AuthorizeResult, RequestTokenResult, AccessTokenResult;
        private bool isDisposed;
        #endregion

        #region コンストラクタ
        ~OAuthorize()
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
                WaitResult(AuthorizeResult);
                WaitResult(RequestTokenResult);
                WaitResult(AccessTokenResult);
                isDisposed = true;
            }
        }
        #endregion

        /// <summary>
        /// 認証
        /// </summary>
        /// <param name="srv"></param>
        public void Authorize ( TwitterInfo srv )
        {
            this.AuthorizeResult = base.loadAsync ( srv, "GET", workerResult, null, null, "oauth/authorize", null );
        }

        /// <summary>
        /// 認証
        /// </summary>
        /// <param name="srv"></param>
        public void RequestToken ( TwitterInfo srv )
        {
            this.RequestTokenResult = base.loadAsync ( srv, "POST", workerResult, null, null, "oauth/request_token", null );
        }

        /// <summary>
        /// 認証
        /// </summary>
        /// <param name="srv"></param>
        public void AccessToken ( TwitterInfo srv, string pincode )
        {
            List<OAuth.OAuthBase.QueryParameter> q = new List<OAuth.OAuthBase.QueryParameter> ();
            q.Add ( new OAuth.OAuthBase.QueryParameter ( "oauth_verifier", pincode ) );
            q.Add ( new OAuth.OAuthBase.QueryParameter ( "request_token", srv.request_token_key ) );
            q.Add ( new OAuth.OAuthBase.QueryParameter ( "request_token_secret", srv.request_token_secret ) );
            this.AccessTokenResult = base.loadAsync ( srv, "POST", workerResult, null, null, "oauth/access_token", q );
        }

        /// <summary>
        /// 受信したデータを処理する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private object workerResult ( dynamic data )
        {
            if ( data.status_code == HttpStatusCode.OK )
            {
                string param = data.rawdata;
                string[] result = new string[4];
                var p = param.Split ( '&' );
                foreach ( string t in p )
                {
                    var tmp = t.Split ( '=' );
                    if ( tmp[0] == "oauth_token" )
                    {
                        result[0] = tmp[1];
                    }
                    if ( tmp[0] == "oauth_token_secret" )
                    {
                        result[1] = tmp[1];
                    }
                    if ( tmp[0] == "user_id" )
                    {
                        result[2] = tmp[1];
                    }
                    if ( tmp[0] == "screen_name" )
                    {
                        result[3] = tmp[1];
                    }
                }
                return result;
            }
            return null;
        }
    }
}
