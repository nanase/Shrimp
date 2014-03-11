using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using Codeplex.Data;
using OAuth;
using Shrimp.Log;
using Shrimp.Module.Queue;
using Shrimp.Twitter.REST;
using Shrimp.Twitter.Status;

namespace Shrimp.Twitter.Streaming
{
    public class UserStreaming
    {
        #region 定義

        //  ロード完了時のイベントハンドラ
        public delegate void TweetEventDelegate(object sender, TwitterCompletedEventArgs e);
        public delegate void NotifyEventDelegate(object sender, TwitterCompletedEventArgs e);
        public delegate void UserStreamingconnectStatusEventDelegate(object sender, TwitterCompletedEventArgs e);

        public event TweetEventDelegate completedHandler;
        public event UserStreamingconnectStatusEventDelegate disconnectHandler;
        public event NotifyEventDelegate notifyHandler;

        private delegate void loadWorkerDelegate(List<OAuthBase.QueryParameter> param, UserStreaming.TweetEventDelegate completedHandler, UserStreaming.NotifyEventDelegate notifyHandler,
                                UserStreaming.UserStreamingconnectStatusEventDelegate disconnectHandler, UserStreamThreadData sender);
        private volatile Dictionary<decimal, UserStreamThreadData> workerThreads = new Dictionary<decimal, UserStreamThreadData>();
        private volatile bool _isStartedStreaming = false;
        private Encoding enc = Encoding.GetEncoding(932);

        #endregion

        public UserStreaming()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;
        }

        ~UserStreaming()
        {
            //StopStreamingAll ();
        }

        /// <summary>
        /// ロードシンク
        /// </summary>
        public void loadAsync(TwitterInfo srv, List<OAuthBase.QueryParameter> param)
        {
            if (this.workerThreads.ContainsKey(srv.UserId))
                this.stopStreaming(srv);

            var thread = new Thread(new ParameterizedThreadStart(this.StartStreaming));
            this.workerThreads[srv.UserId] = new UserStreamThreadData(thread, false);
            thread.Start(new object[6] { srv, param, completedHandler, notifyHandler, disconnectHandler, this.workerThreads[srv.UserId] });

            this.isStartedStreaming = true;
        }

        /// <summary>
        /// ストリーミングをストップさせる
        /// </summary>
        /// <param name="srv"></param>
        /// <param name="isJoin"></param>
        public void stopStreaming(TwitterInfo srv, bool isJoin = false)
        {
            if (!this.workerThreads.ContainsKey(srv.UserId))
                return;


            if (this.workerThreads[srv.UserId] != null && this.workerThreads[srv.UserId].Thread.ThreadState == ThreadState.Running)
            {
                //
                //Thread.Sleep ( 1 );
                this.workerThreads[srv.UserId].IsStopFlag = true;
                Thread.Sleep(0);
                this.workerThreads[srv.UserId].Thread.Abort();
                // this.workerThreads[srv.user_id] = null;
                // this.workerThreads.Remove ( srv.user_id );
            }
        }

        public void CheckStopped()
        {
            if (this.workerThreads.All((d) => d.Value.IsFinishedThread == true))
                this.isStartedStreaming = false;
        }

        public void StopStreamingAll()
        {
            /*
            foreach ( KeyValuePair<decimal,Thread> t in this.workerThreads )
            {
                if ( t.Value != null )
                {
                    t.Value.Abort ();
                }
            }
            this.isStartedStreaming = false;
            this.workerThreads.Clear ();
            */
        }

        /*
        /// <summary>
        /// 非同期で行われる処理の内容
        /// </summary>
        /// <param name="srv"></param>
        public void loadWorker ( object args )
        {
            object[] obj = (object[])args;
            TwitterInfo srv = (TwitterInfo)obj[0];
            List<OAuthBase.QueryParameter> param = (List<OAuthBase.QueryParameter>)obj[1];
            UserStreaming.TweetEventHandler completedHandler = (UserStreaming.TweetEventHandler)obj[2];
            UserStreaming.NotifyEventHandler notifyEventHandler = (UserStreaming.NotifyEventHandler)obj[3];
            UserStreaming.UserStreamingconnectStatusEventHandler disconnectHandler = (UserStreaming.UserStreamingconnectStatusEventHandler)obj[4];
            srv.stopStreamingFlag = false;
            srv.StartStreaming ( param, completedHandler, notifyEventHandler, disconnectHandler );
            //loadWorkerControl = new loadWorkerDelegate ( srv.StartStreaming );
            //loadWorkerResult = loadWorkerControl.BeginInvoke ( param, completedHandler, notifyEventHandler, disconnectHandler, null, null );
        }
        */

        /// <summary>
        /// HttpWebRequestのパラメータを設定します
        /// </summary>
        /// <param name="webreq"></param>
        private void SetWebReq(HttpWebRequest webreq)
        {
            webreq.Method = "GET";
            webreq.UserAgent = "Shrimp";
            webreq.ProtocolVersion = HttpVersion.Version11;
            webreq.AutomaticDecompression = DecompressionMethods.Deflate;
            webreq.ServicePoint.ConnectionLimit = 1000;
            webreq.Timeout = 30 * 1000;
            webreq.ContentType = "application/x-www-form-urlencoded";
        }


        /// <summary>
        /// ストリーミングを開始する
        /// 
        /// </summary>
        /// <param name="param"></param>
        public void StartStreaming(object args)
        {
            object[] obj = (object[])args;
            TwitterInfo srv = (TwitterInfo)obj[0];
            List<OAuthBase.QueryParameter> param = (List<OAuthBase.QueryParameter>)obj[1];
            UserStreaming.TweetEventDelegate completedHandler = (UserStreaming.TweetEventDelegate)obj[2];
            UserStreaming.NotifyEventDelegate notifyEventHandler = (UserStreaming.NotifyEventDelegate)obj[3];
            UserStreaming.UserStreamingconnectStatusEventDelegate disconnectHandler = (UserStreaming.UserStreamingconnectStatusEventDelegate)obj[4];
            UserStreamThreadData sender = (UserStreamThreadData)obj[5];
            List<decimal> friends = new List<decimal>();
            UserStreamQueue streamQueue = new UserStreamQueue();
            int ReconnectCount = 0;

            Uri uri;
            uri = new Uri(TwitterInfo.TwitterStreamingAPI);

            //  再接続処理も含めて。
            while (!sender.IsStopFlag)
            {
                OAuthBase oAuth = new OAuthBase();
                string nonce = oAuth.GenerateNonce();
                string timestamp = oAuth.GenerateTimeStamp();

                //OAuthBace.csを用いてsignature生成
                string normalizedUrl, normalizedRequestParameters;
                string sig = oAuth.GenerateSignature(uri, param, "oob", srv.ConsumerKey, srv.ConsumerSecret, srv.AccessTokenKey, srv.AccessTokenSecret,
                                                        "GET", timestamp, null, nonce, out normalizedUrl, out normalizedRequestParameters);
                sig = OAuthBase.UrlEncode(sig);

                HttpWebRequest webreq = (HttpWebRequest)WebRequest.Create(string.Format("{0}?{1}&oauth_signature={2}", normalizedUrl, normalizedRequestParameters, sig));
                this.SetWebReq(webreq);
                HttpWebResponse webres = null;
                Stream st = null;
                StreamReader sr = null;
                try
                {
                    webres = (HttpWebResponse)webreq.GetResponse();
                    st = webres.GetResponseStream();

                    if (webres != null && webres.ContentEncoding.ToLower() == "gzip")
                    {
                        //gzip。
                        GZipStream gzip = new GZipStream(st, CompressionMode.Decompress);
                        sr = new StreamReader(gzip, enc);
                    }
                    else
                    {
                        sr = new StreamReader(st, enc);
                    }
                    //  接続開始
                    streamQueue.Enqueue
                        (new UserStreamQueueData(this, new TwitterCompletedEventArgs(srv, HttpStatusCode.Unused, null, null), disconnectHandler));
                    bool isFirstTime = false;

                    while (!sender.IsStopFlag && !sr.EndOfStream)
                    {
                        string t = sr.ReadLine();
                        if (!String.IsNullOrEmpty(t) && !sender.IsStopFlag)
                        {
                            var data = DynamicJson.Parse(t);

                            if (!isFirstTime)
                            {
                                if (data.IsDefined("friends"))
                                {
                                    friends = ((List<decimal>)data.friends);
                                }
                                isFirstTime = true;
                                ReconnectCount = 0;
                                streamQueue.Enqueue
                                    (new UserStreamQueueData(this, new TwitterCompletedEventArgs(srv, HttpStatusCode.OK, null, null), disconnectHandler));
                                continue;
                            }

                            if (data.IsDefined("id"))
                            {
                                //  ツイート
                                streamQueue.Enqueue
                                    (new UserStreamQueueData(this, new TwitterCompletedEventArgs(srv, HttpStatusCode.OK, new TwitterStatus(data), null), completedHandler));
                            }
                            else if (data.IsDefined("event"))
                            {
                                //  イベント
                                if (data["event"] == "favorite" || data["event"] == "unfavorite" ||
                                    data["event"] == "follow" || data["event"] == "unfollow")
                                {
                                    streamQueue.Enqueue
                                        (new UserStreamQueueData(this, new TwitterCompletedEventArgs(srv, HttpStatusCode.OK, new TwitterNotifyStatus(data), null), notifyHandler));
                                }
                            }
                            else if (data.IsDefined("direct_message"))
                            {
                                //  ダイレクトメッセージ
                                var directMessage = new TwitterDirectMessageStatus(data.direct_message);
                                streamQueue.Enqueue
                                        (new UserStreamQueueData(this, new TwitterCompletedEventArgs(srv, HttpStatusCode.OK, directMessage, null), completedHandler));
                            }
                            //  深愛
                        }
                    }
                }
                catch (Exception e)
                {
                    LogControl.AddLogs("UserStreamが例外により切断されました: " + e.Message + "");
                }
                finally
                {
                    if (sender.IsStopFlag)
                        sender.IsFinishedThread = true;
                    if (ReconnectCount < 6)
                        ReconnectCount++;

                    streamQueue.Enqueue
                        (new UserStreamQueueData(this,
                            new TwitterCompletedEventArgs(srv, (sender.IsStopFlag ? HttpStatusCode.RequestTimeout : HttpStatusCode.Continue), null, null), disconnectHandler));

                    if (sr != null)
                        sr.Close();
                    if (st != null)
                        st.Close();

                    sr = null;

                    streamQueue.Wait();
                }
                if (sender.IsStopFlag)
                    break;
                Thread.Sleep(ReconnectCount * 10000);
            }

            return;
        }

        public bool isStartedStreaming
        {
            get { return this._isStartedStreaming; }
            set
            {
                this._isStartedStreaming = value;
            }
        }
    }
}
