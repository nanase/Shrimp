using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Xml.Serialization;
using OAuth;
using Shrimp.Twitter.Status;
using Shrimp.Twitter.Streaming;

namespace Shrimp.Twitter
{
    public class TwitterInfo : ICloneable, IEquatable<TwitterInfo>
    {
        #region 定義
        //private OAuthBase oauth = new OAuthBase ();
        //public  event newTweetEventHandler newTweetEvent;
        private volatile bool stopStreamingFlag = false;

        public delegate void NewTweetEventHandler(object sender, UserStreamingEventArgs e);
        #endregion

        /// <summary>
        /// コンシューマーキー
        /// </summary>
        [XmlElement ( "consumer_key" )]
        public string ConsumerKey { get; set; }

        /// <summary>
        /// コンシューマーシークレット
        /// </summary>
        [XmlElement ( "consumer_secret" )]
        public string ConsumerSecret { get; set; }

        /// <summary>
        /// アクセストークンキー
        /// </summary>
        [XmlElement ( "access_token_key" )]
        public string AccessTokenKey { get; set; }

        /// <summary>
        /// アクセストークンシークレット
        /// </summary>
        [XmlElement ( "access_token_secret" )]
        public string AccessTokenSecret { get; set; }

        /// <summary>
        /// トークンキー
        /// </summary>
        [XmlIgnore]
        public string RequestTokenKey { get; set; }

        /// <summary>
        /// トークンシークレット
        /// </summary>
        [XmlIgnore]
        public string RequestTokenSecret { get; set; }

        /// <summary>
        /// アイコンのURL
        /// </summary>
        [XmlIgnore]
        public string IconUrl { get; set; }

        /// <summary>
        /// アイコンのデータ
        /// </summary>
        [XmlIgnore]
        public Bitmap IconData { get; set; }

        /// <summary>
        /// TwitterAPIのURLの根幹を取得
        /// </summary>
        [XmlIgnore]
        private string twitterAPIBase
        {
            get { return "https://api.twitter.com/1.1/"; }
        }

        /// <summary>
        /// Streaming API URLを取得
        /// </summary>
        [XmlIgnore]
        public static string TwitterStreamingAPI
        {
            get { return "https://userstream.twitter.com/1.1/user.json"; }
        }


        /// <summary>
        /// 自分のユーザータイムラインの内容
        /// </summary>
        [XmlIgnore]
        private List<TwitterStatus> OwnUserTimeline { get; set; }

        /// <summary>
        /// あと何ツイートできるか計算する
        /// </summary>
        [XmlIgnore]
        public int TweetLimit { get { return 0; } }

        /// <summary>
        /// ユーザーID
        /// </summary>
        [XmlElement ( "user_id" )]
        public decimal UserId { get; set; }

        /// <summary>
        /// スクリーンネーム
        /// </summary>
        [XmlElement ( "screen_name" )]
        public string ScreenName { get; set; }

        /// <summary>
        /// ホームタイムラインの直前のID
        /// </summary>
        [XmlIgnore]
        public decimal HomeTimelineSinceID { get; set; }

        /// <summary>
        /// 返信の直前のID
        /// </summary>
        [XmlIgnore]
        public decimal MentionTimelineSinceID { get; set; }

        /// <summary>
        /// ダイレクトメッセージタイムラインの直前のID
        /// </summary>
        [XmlIgnore]
        public decimal DirectMessageReceivedSinceID { get; set; }

        /// <summary>
        /// ダイレクトメッセージタイムラインの直前のID
        /// </summary>
        [XmlIgnore]
        public decimal DirectMessageSendSinceID { get; set; }

        /// <summary>
        /// エビビーム！ビビビビ！
        /// </summary>
        [XmlElement ( "shrimpBeamCount" )]
        public decimal ShrimpBeam { get; set; }

        /// <summary>
        /// エビビームを最後に撃った時間
        /// </summary>
        [XmlElement ( "executeShrimpBeamlatestDate" )]
        public DateTime ShrimpBeamLatestDate { get; set; }

        #region コンストラクタ
        public TwitterInfo ()
        {
        }

        public TwitterInfo(string consumerKey = null,
                           string consumerSecret = null,
                           string accessTokenKey = null,
                           string accessTokenSecret = null)
        {
            this.ConsumerKey = consumerKey;
            this.ConsumerSecret = consumerSecret;
            this.AccessTokenKey = accessTokenKey;
            this.AccessTokenSecret = accessTokenSecret;

            Initialize();
        }
        #endregion

        /// <summary>
        /// 初期化
        /// </summary>
        private void Initialize()
        {
            ServicePointManager.Expect100Continue = false;
        }

        /// <summary>
        /// イコールかどうかを比較する
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(TwitterInfo other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            return this.UserId == other.UserId;
        }

        // コピーを作成するメソッド
        public virtual object Clone()
        {
            TwitterInfo instance = (TwitterInfo)Activator.CreateInstance(GetType());
            instance.ConsumerKey = this.ConsumerKey;
            instance.ConsumerSecret = this.ConsumerSecret;
            instance.AccessTokenKey = this.AccessTokenKey;
            instance.AccessTokenSecret = this.AccessTokenSecret;
            instance.ScreenName = this.ScreenName;
            instance.UserId = this.UserId;
            return instance;
        }

        /// <summary>
        /// 自分のユーザータイムラインをセットする
        /// </summary>
        /// <param name="status"></param>
        public void SetUserTimeline(List<TwitterStatus> status)
        {
            this.OwnUserTimeline = status;
        }

        /// <summary>
        /// 同期的に行うので、どっかでやって
        /// </summary>
        /// <param name="uri"></param>
        public TwitterSocket Get(string url, List<OAuthBase.QueryParameter> param)
        {
            return this.Socket(url, "GET", param);
        }

        /// <summary>
        /// 同期的に行うので、どっかでやって
        /// </summary>
        /// <param name="uri"></param>
        public TwitterSocket Post(string url, List<OAuthBase.QueryParameter> param, TwitterUpdateImage image)
        {
            return this.Socket(url, "POST", param, image);
        }

        /// <summary>
        /// WebSocket
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="method">GET or POST</param>
        /// <param name="param">パラメータ</param>
        /// <returns></returns>
        // FIXME: 適切な名前をつけてね
        private TwitterSocket Socket(string url, string method, List<OAuthBase.QueryParameter> param, TwitterUpdateImage image = null)
        {
            OAuthBase oAuth = new OAuthBase();
            string nonce = oAuth.GenerateNonce();
            string timestamp = oAuth.GenerateTimeStamp();

            Uri uri;

            //OAuthBace.csを用いてsignature生成
            string normalizedUrl, normalizedRequestParameters, sig = "";

            if (url == "oauth/request_token" || url == "oauth/access_token")
            {
                uri = new Uri("https://api.twitter.com/" + url + "");

                if (param != null && param[0].Name == "oauth_verifier")
                {
                    sig = oAuth.GenerateSignature(uri, null, "oob", ConsumerKey, ConsumerSecret, param[1].Value, param[2].Value,
                        method, timestamp, param[0].Value, nonce, out normalizedUrl, out normalizedRequestParameters);
                }
                else
                {
                    sig = oAuth.GenerateSignature(uri, param, "oob", ConsumerKey, ConsumerSecret, null, null,
                        method, timestamp, null, nonce, out normalizedUrl, out normalizedRequestParameters);
                }
            }
            else
            {
                uri = new Uri(twitterAPIBase + url);
                sig = oAuth.GenerateSignature(uri, param, "oob", ConsumerKey, ConsumerSecret, AccessTokenKey, AccessTokenSecret,
                                                        method, timestamp, null, nonce, out normalizedUrl, out normalizedRequestParameters);
            }

            sig = OAuthBase.UrlEncode(sig);
            string raw_data = null;
            Stream st = null;
            StreamReader sr = null;
            HttpStatusCode code = HttpStatusCode.RequestTimeout;
            HttpWebRequest webreq = null;

            try
            {
                webreq = (HttpWebRequest)WebRequest.Create(string.Format("{0}?{1}&oauth_signature={2}", normalizedUrl, normalizedRequestParameters, sig));
                webreq.Method = method;
                webreq.Timeout = 60 * 1000;
                webreq.UserAgent = "Shrimp";
                webreq.ProtocolVersion = HttpVersion.Version11;
                webreq.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                webreq.ServicePoint.ConnectionLimit = 1000;
                webreq.ContentType = "application/x-www-form-urlencoded";

                if (url.IndexOf("statuses/update_with_media") >= 0)
                {
                    //  メディアアップロードらしい。
                    //区切り文字列
                    if (image == null)
                        throw new WebException("imageがありません");

                    var media = image.Data;
                    var filename = image.FileName;
                    var status = image.Status;
                    Encoding enc = Encoding.GetEncoding("utf-8");
                    string boundary = Environment.TickCount.ToString();
                    webreq.ContentType = "multipart/form-data; boundary=" + boundary;

                    //POST送信するデータを作成
                    string postData = "";
                    postData = "--" + boundary + "\r\n" +
                        "Content-Disposition: form-data; name=\"status\"\r\n\r\n" +
                        "" + status + "\r\n" +
                        "--" + boundary + "\r\n" +
                        "Content-Disposition: form-data; name=\"media[]\"; filename=\"" +
                            filename + "\"\r\n" +
                        "Content-Type: application/octet-stream\r\n" +
                        "Content-Transfer-Encoding: binary\r\n\r\n";

                    //バイト型配列に変換
                    byte[] startData = enc.GetBytes(postData);
                    postData = "\r\n--" + boundary + "--\r\n";
                    byte[] endData = enc.GetBytes(postData);

                    //POST送信するデータの長さを指定
                    webreq.ContentLength = startData.Length + endData.Length + media.Length;

                    //データをPOST送信するためのStreamを取得
                    Stream reqStream = webreq.GetRequestStream();

                    //送信するデータを書き込む
                    reqStream.Write(startData, 0, startData.Length);
                    //ファイルの内容を送信
                    reqStream.Write(media, 0, media.Length);

                    reqStream.Write(endData, 0, endData.Length);
                    reqStream.Close();
                }

                HttpWebResponse webres = (HttpWebResponse)webreq.GetResponse();

                //  コードチェック
                if ((code = webres.StatusCode) == HttpStatusCode.OK)
                {
                    st = webres.GetResponseStream();

                    if (webres != null && webres.ContentEncoding.ToLower() == "gzip")
                    {
                        //gzip。
                        GZipStream gzip = new GZipStream(st, CompressionMode.Decompress);
                        sr = new StreamReader(gzip, Encoding.GetEncoding(932));
                        gzip.Close();
                    }
                    else
                    {
                        sr = new StreamReader(st, Encoding.GetEncoding(932));
                    }

                    raw_data = sr.ReadToEnd();

                    if (raw_data == null)
                        raw_data = "";
                }
            }
            catch (Exception e)
            {
                if (e is WebException)
                {
                    var exp = e as WebException;

                    if (exp.Status == WebExceptionStatus.ProtocolError)
                    {
                        HttpWebResponse err = (HttpWebResponse)exp.Response;
                        code = err.StatusCode;
                    }
                }
            }
            finally
            {
                if (sr != null)
                    sr.Close();

                if (st != null)
                    st.Close();
            }
            return new TwitterSocket((webreq != null ? webreq.RequestUri : null), code, raw_data);
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
