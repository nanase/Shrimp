using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OAuth;
using System.Net;
using System.IO;
using Shrimp.Twitter.Streaming;
using Shrimp.Module;
using Codeplex.Data;
using Shrimp.Twitter.Status;
using Shrimp.Twitter.REST;
using System.IO.Compression;
using System.Drawing;
using Shrimp.Log;
using System.Xml.Serialization;

namespace Shrimp.Twitter
{
    public class TwitterInfo : ICloneable, IEquatable<TwitterInfo>
    {
        #region 定義
        //private OAuthBase oauth = new OAuthBase ();
        public delegate void newTweetEventHandler ( object sender, UserStreamingEventArgs e );
        //public  event newTweetEventHandler newTweetEvent;
        public volatile bool stopStreamingFlag = false;
        #endregion

        #region コンストラクタ
        public TwitterInfo ()
        {
            initialize ();
        }

        public TwitterInfo ( string consumer_key, string consumer_secret )
        {
            this.consumer_key = consumer_key;
            this.consumer_secret = consumer_secret;
            initialize ();
        }


        public TwitterInfo ( string consumer_key, string consumer_secret, string access_token_key, string access_token_secret )
        {
            this.consumer_key = consumer_key;
            this.consumer_secret = consumer_secret;
            this.access_token_key = access_token_key;
            this.access_token_secret = access_token_secret;
            initialize ();
        }

        /// <summary>
        /// イコールかどうかを比較する
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals ( TwitterInfo other )
        {
            if ( other == null ) return false;

            return this.user_id == other.user_id;
        }
        #endregion

        /// <summary>
        /// 初期化
        /// </summary>
        private void initialize ()
        {
            ServicePointManager.Expect100Continue = false;
        }

        /// <summary>
        /// コンシューマーキー
        /// </summary>
        public string consumer_key
        {
            get;
            set;
        }

        /// <summary>
        /// コンシューマーシークレット
        /// </summary>
        public string consumer_secret
        {
            get;
            set;
        }

        /// <summary>
        /// アクセストークンキー
        /// </summary>
        public string access_token_key
        {
            get;
            set;
        }

        /// <summary>
        /// アクセストークンシークレット
        /// </summary>
        public string access_token_secret
        {
            get;
            set;
        }

        /// <summary>
        /// トークンキー
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public string request_token_key
        {
            get;
            set;
        }

        /// <summary>
        /// トークンシークレット
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public string request_token_secret
        {
            get;
            set;
        }

        /// <summary>
        /// アイコンのURL
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public string icon_url
        {
            get;
            set;
        }

        /// <summary>
        /// アイコンのデータ
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public Bitmap icon_data
        {
            get;
            set;
        }

        /// <summary>
        /// TwitterAPIのURLの根幹を取得
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute]
        private string twitterAPIBase
        {
            get { return "https://api.twitter.com/1.1/"; }
        }

        /// <summary>
        /// Streaming API URLを取得
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public static string twitterStreamingAPI
        {
            get { return "https://userstream.twitter.com/1.1/user.json"; }
        }


        /// <summary>
        /// 自分のユーザータイムラインの内容
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute]
        private List<TwitterStatus> OwnUserTimeline
        {
            get;
            set;
        }

        /// <summary>
        /// あと何ツイートできるか計算する
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public int Tweetlimit
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// ユーザーID
        /// </summary>
        public decimal user_id
        {
            get;
            set;
        }

        /// <summary>
        /// スクリーンネーム
        /// </summary>
        public string screen_name
        {
            get;
            set;
        }

        /// <summary>
        /// ホームタイムラインの直前のID
        /// </summary>
        [XmlIgnoreAttribute]
        public decimal HomeTimelineSinceID
        {
            get;
            set;
        }

        /// <summary>
        /// 返信の直前のID
        /// </summary>
        [XmlIgnoreAttribute]
        public decimal MentionTimelineSinceID
        {
            get;
            set;
        }

        /// <summary>
        /// ダイレクトメッセージタイムラインの直前のID
        /// </summary>
        [XmlIgnoreAttribute]
        public decimal DirectMessageReceivedSinceID
        {
            get;
            set;
        }

        /// <summary>
        /// ダイレクトメッセージタイムラインの直前のID
        /// </summary>
        [XmlIgnoreAttribute]
        public decimal DirectMessageSendSinceID
        {
            get;
            set;
        }

        // コピーを作成するメソッド
        public virtual object Clone ()
        {
            TwitterInfo instance = (TwitterInfo)Activator.CreateInstance ( GetType () );
            instance.consumer_key = this.consumer_key;
            instance.consumer_secret = this.consumer_secret;
            instance.access_token_key = this.access_token_key;
            instance.access_token_secret = this.access_token_secret;
            instance.screen_name = this.screen_name;
            instance.user_id = this.user_id;
            return instance;
        }

        /// <summary>
        /// 自分のユーザータイムラインをセットする
        /// </summary>
        /// <param name="status"></param>
        public void SetUserTimeline ( List<TwitterStatus> status )
        {
            this.OwnUserTimeline = status;
        }

        /// <summary>
        /// WebSocket
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="method">GET or POST</param>
        /// <param name="param">パラメータ</param>
        /// <returns></returns>
        private TwitterSocket socket ( string url, string method, List<OAuthBase.QueryParameter> param, TwitterUpdateImage image = null )
        {
            OAuthBase oAuth = new OAuthBase ();
            string nonce = oAuth.GenerateNonce ();
            string timestamp = oAuth.GenerateTimeStamp ();
            
            Uri uri;
            //OAuthBace.csを用いてsignature生成
            string normalizedUrl, normalizedRequestParameters, sig = "";

            if ( url == "oauth/request_token" || url == "oauth/access_token" )
            {
                uri = new Uri ( "https://api.twitter.com/"+ url +"" );
                if ( param != null && param[0].Name == "oauth_verifier" )
                {
                    sig = oAuth.GenerateSignature ( uri, null, "oob", consumer_key, consumer_secret, param[1].Value, param[2].Value,
                        method, timestamp, param[0].Value, nonce, out normalizedUrl, out normalizedRequestParameters );
                }
                else
                {
                    sig = oAuth.GenerateSignature ( uri, param, "oob", consumer_key, consumer_secret, null, null,
                        method, timestamp, null, nonce, out normalizedUrl, out normalizedRequestParameters );
                }
            }
            else
            {
                uri = new Uri ( twitterAPIBase + url );
                sig = oAuth.GenerateSignature ( uri, param, "oob", consumer_key, consumer_secret, access_token_key, access_token_secret,
                                                        method, timestamp, null, nonce, out normalizedUrl, out normalizedRequestParameters );
            }
            sig = OAuthBase.UrlEncode ( sig );
            string raw_data = null;
            Stream st = null;
            StreamReader sr = null;
            HttpStatusCode code = HttpStatusCode.RequestTimeout; 
            HttpWebRequest webreq = null;
            try
            {
                webreq = (HttpWebRequest)WebRequest.Create ( string.Format ( "{0}?{1}&oauth_signature={2}", normalizedUrl, normalizedRequestParameters, sig ) );
                webreq.Method = method;
                webreq.Timeout = 60 * 1000;
                webreq.UserAgent = "Shrimp";
                webreq.ProtocolVersion = HttpVersion.Version11;
                webreq.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                webreq.ServicePoint.ConnectionLimit = 1000;
                webreq.ContentType = "application/x-www-form-urlencoded";

                if ( url.IndexOf ( "statuses/update_with_media" ) >= 0 )
                {
                    //  メディアアップロードらしい。
                    //区切り文字列
                    if ( image == null )
                        throw new WebException ( "imageがありません" );
                    var media = image.data;
                    var filename = image.filename;
                    var status = image.status;
                    Encoding enc = Encoding.GetEncoding ( "utf-8" );
                    string boundary = Environment.TickCount.ToString ();
                    webreq.ContentType = "multipart/form-data; boundary=" + boundary;

                    //POST送信するデータを作成
                    string postData = "";
                    postData = "--" + boundary + "\r\n" +
                        "Content-Disposition: form-data; name=\"status\"\r\n\r\n" +
                        ""+ status +"\r\n" +
                        "--" + boundary + "\r\n" +
                        "Content-Disposition: form-data; name=\"media[]\"; filename=\"" +
                            filename + "\"\r\n" +
                        "Content-Type: application/octet-stream\r\n" +
                        "Content-Transfer-Encoding: binary\r\n\r\n";
                    //バイト型配列に変換
                    byte[] startData = enc.GetBytes ( postData );
                    postData = "\r\n--" + boundary + "--\r\n";
                    byte[] endData = enc.GetBytes ( postData );

                    //POST送信するデータの長さを指定
                    webreq.ContentLength = startData.Length + endData.Length + media.Length;
                  
                    //データをPOST送信するためのStreamを取得
                    Stream reqStream = webreq.GetRequestStream ();

                    //送信するデータを書き込む
                    reqStream.Write ( startData, 0, startData.Length );
                    //ファイルの内容を送信
                    reqStream.Write ( media, 0, media.Length );

                    reqStream.Write ( endData, 0, endData.Length );
                    reqStream.Close ();
                }
                HttpWebResponse webres = (HttpWebResponse)webreq.GetResponse ();

                //  コードチェック
                if ( ( code = webres.StatusCode ) == HttpStatusCode.OK )
                {
                    st = webres.GetResponseStream ();
                    if ( webres != null && webres.ContentEncoding.ToLower () == "gzip" )
                    {
                        //gzip。
                        GZipStream gzip = new GZipStream ( st, CompressionMode.Decompress );
                        sr = new StreamReader ( gzip, Encoding.GetEncoding ( 932 ) );
                        gzip.Close ();
                    }
                    else
                    {
                        sr = new StreamReader ( st, Encoding.GetEncoding ( 932 ) );
                    }
                    raw_data = sr.ReadToEnd ();
                    if ( raw_data == null )
                        raw_data = "";
                }
            }
            catch ( Exception e )
            {
                if ( e is WebException )
                {
                    var exp = e as WebException;
                    if ( exp.Status == WebExceptionStatus.ProtocolError )
                    {
                        HttpWebResponse err = (HttpWebResponse)exp.Response;
                        code = err.StatusCode;
                    }
                }
            }
            finally
            {
                if ( sr != null )
                    sr.Close ();
                if ( st != null )
                    st.Close ();
            }
            return new TwitterSocket ( ( webreq != null ? webreq.RequestUri : null ), code, raw_data );
        }

        /// <summary>
        /// 同期的に行うので、どっかでやって
        /// </summary>
        /// <param name="uri"></param>
        public TwitterSocket get ( string url, List<OAuthBase.QueryParameter> param )
        {
            return this.socket ( url, "GET", param );
        }

        /// <summary>
        /// 同期的に行うので、どっかでやって
        /// </summary>
        /// <param name="uri"></param>
        public TwitterSocket post ( string url, List<OAuthBase.QueryParameter> param, TwitterUpdateImage image )
        {
            return this.socket ( url, "POST", param, image );
        }

        /*
        /// <summary>
        /// ストリーミングを開始する
        /// 
        /// </summary>
        /// <param name="param"></param>
        public void StartStreaming ( List<OAuthBase.QueryParameter> param, UserStreaming.TweetEventHandler newTweetHandler, UserStreaming.NotifyEventHandler notifyHandler,
                                        UserStreaming.UserStreamingconnectStatusEventHandler connectStatusHandler )
        {
            OAuthBase oAuth = new OAuthBase ();
            string nonce = oAuth.GenerateNonce ();
            string timestamp = oAuth.GenerateTimeStamp ();
            Uri uri;
            uri = new Uri ( twitterStreamingAPI );

            //OAuthBace.csを用いてsignature生成
            string normalizedUrl, normalizedRequestParameters;
            string sig = oAuth.GenerateSignature ( uri, param, "oob", consumer_key, consumer_secret, access_token_key, access_token_secret,
                                                    "GET", timestamp, null, nonce, out normalizedUrl, out normalizedRequestParameters );
            sig = OAuthBase.UrlEncode ( sig );

            Stream st = null;
            StreamReader sr = null;
            HttpStatusCode code = HttpStatusCode.OK;
            HttpWebRequest webreq = (HttpWebRequest)WebRequest.Create ( string.Format ( "{0}?{1}&oauth_signature={2}", normalizedUrl, normalizedRequestParameters, sig ) );
            webreq.Method = "GET";
            webreq.UserAgent = "Shrimp";
            webreq.ProtocolVersion = HttpVersion.Version11;
            webreq.AutomaticDecompression = DecompressionMethods.Deflate;
            webreq.ServicePoint.ConnectionLimit = 1000;
            webreq.Timeout = 30 * 1000;
            webreq.KeepAlive = true;
            webreq.ContentType = "application/x-www-form-urlencoded";

            try
            {
                HttpWebResponse webres = (HttpWebResponse)webreq.GetResponse ();
                st = webres.GetResponseStream ();
                if ( webres != null && webres.ContentEncoding.ToLower () == "gzip" )
                {
                    //gzip。
                    GZipStream gzip = new GZipStream ( st, CompressionMode.Decompress );
                    sr = new StreamReader ( gzip, Encoding.GetEncoding ( 932 ) );
                }
                else
                {
                    sr = new StreamReader ( st, Encoding.GetEncoding ( 932 ) );
                }
                if ( connectStatusHandler != null )
                    connectStatusHandler.Invoke ( this, new TwitterCompletedEventArgs ( HttpStatusCode.Unused, null, null ) );
                bool isFirstTime = false;

                while ( !this.stopStreamingFlag )
                {
                    string t = sr.ReadLine ();
                    if ( !String.IsNullOrEmpty ( t ) && !this.stopStreamingFlag )
                    {
                        if ( !isFirstTime )
                        {
                            isFirstTime = true;
                            if ( connectStatusHandler != null )
                                connectStatusHandler.BeginInvoke ( this, new TwitterCompletedEventArgs ( HttpStatusCode.OK, null, null ), null, null );
                            continue;
                        }
                        var data = DynamicJson.Parse ( t );
                        if ( data.IsDefined ( "id" ) )
                        {
                            if ( newTweetHandler != null )
                                newTweetHandler.BeginInvoke ( this, new TwitterCompletedEventArgs ( HttpStatusCode.OK, new TwitterStatus ( data ), null ), null, null );
                        }
                        else if ( data.IsDefined ( "event" ) )
                        {
                            if ( data["event"] == "favorite" || data["event"] == "unfavorite" ||
                                data["event"] == "follow" || data["event"] == "unfollow" )
                            {
                                if ( notifyHandler != null )
                                    notifyHandler.BeginInvoke ( this, new TwitterCompletedEventArgs ( HttpStatusCode.OK, new TwitterNotifyStatus ( data ), null ), null, null );
                            }
                        }
                        //  深愛
                    }
                }
                Console.WriteLine ( "!?" );
            }
            catch ( Exception e )
            {
                LogControl.AddLogs ( "UserStreamが切断されました: " + e.Message + "" );
            }
            finally
            {
                if ( connectStatusHandler != null )
                    connectStatusHandler.Invoke ( this, new TwitterCompletedEventArgs ( HttpStatusCode.RequestTimeout, null, null ) );
                if ( sr != null )
                    sr.Close ();
                if ( st != null )
                    st.Close ();

                sr = null;
                st = null;

               this.stopStreamingFlag = false;

                
            }

            return;
        }
         * */
    }
}
