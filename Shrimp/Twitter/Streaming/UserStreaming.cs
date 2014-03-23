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
using System.Net.Sockets;

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
			thread.Name = "UserStream:" + srv.ScreenName + "";
            thread.Start(new object[6] { srv, param, completedHandler, notifyHandler, disconnectHandler, this.workerThreads[srv.UserId] });

            this.isStartedStreaming = true;
        }

        /// <summary>
        /// ストリーミングをストップさせる
        /// </summary>
        /// <param name="srv"></param>
        /// <param name="isJoin"></param>
        public void stopStreaming(TwitterInfo srv, bool isDestroy = false)
        {
            if (!this.workerThreads.ContainsKey(srv.UserId))
                return;


            if (this.workerThreads[srv.UserId] != null && this.workerThreads[srv.UserId].Thread.ThreadState == ThreadState.Running)
            {
                //
                //Thread.Sleep ( 1 );
                this.workerThreads[srv.UserId].isStopFlag = true;
                this.workerThreads[srv.UserId].isDestroy = isDestroy;
                Thread.Sleep(0);
                if ( isDestroy )
                this.workerThreads[srv.UserId].Thread.Abort();
                else
                    this.workerThreads[srv.UserId].Thread.Abort ();
                // this.workerThreads[srv.user_id] = null;
                // this.workerThreads.Remove ( srv.user_id );
            }
        }

        public void CheckStopped()
        {
            if (this.workerThreads.All((d) => d.Value.isFinishedThread == true))
                this.isStartedStreaming = false;
        }

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
			HttpWebResponse webres = null;
			Stream st = null;
			StreamReader sr = null;
			bool isAbortException = false;

            Uri uri;
            uri = new Uri(TwitterInfo.TwitterStreamingAPI);
            HttpWebRequest webreq = null;

			while (true)
			{
				try
				{
					Thread.Sleep(ReconnectCount * 10000);

					//  再接続処理も含めて。
					while (!sender.isStopFlag)
					{
						OAuthBase oAuth = new OAuthBase();
						string nonce = oAuth.GenerateNonce();
						string timestamp = oAuth.GenerateTimeStamp();

						//OAuthBace.csを用いてsignature生成
						string normalizedUrl, normalizedRequestParameters;
						string sig = oAuth.GenerateSignature(uri, param, "oob", srv.ConsumerKey, srv.ConsumerSecret, srv.AccessTokenKey, srv.AccessTokenSecret,
																"GET", timestamp, null, nonce, out normalizedUrl, out normalizedRequestParameters);
						sig = OAuthBase.UrlEncode(sig);

						webreq = (HttpWebRequest)WebRequest.Create(string.Format("{0}?{1}&oauth_signature={2}", normalizedUrl, normalizedRequestParameters, sig));
						this.SetWebReq(webreq);

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
							(new UserStreamQueueData(this, new TwitterCompletedEventArgs(srv, HttpStatusCode.Unused, null, null, null), disconnectHandler));

						//	データ取得
						while (!sender.isStopFlag && !sr.EndOfStream)
						{
							string t = sr.ReadLine();

							//	空白でないのなら、データを振り分ける
							if (!String.IsNullOrEmpty(t) && !sender.isStopFlag)
							{
								this.RaiseEvents(srv, t, ref friends, streamQueue);
								ReconnectCount = 0;
								//  深愛
							}
						}
						
					}
				}
				catch (Exception e)
				{
					if (e is ThreadAbortException)
					{
						LogControl.AddLogs("UserStreamスレッドがAbortされます");
						isAbortException = true;
					}
					else
					{
						LogControl.AddLogs("UserStreamが例外により切断されました: " + e.Message + "");
					}
				}
				finally
				{
					if (sender.isStopFlag)
						sender.isFinishedThread = true;
					if (ReconnectCount < 6)
						ReconnectCount++;

                    streamQueue.Clear ();
					if (!sender.isDestroy)
					{
						streamQueue.Enqueue
							(new UserStreamQueueData(this,
								new TwitterCompletedEventArgs(srv, (sender.isStopFlag ? HttpStatusCode.RequestTimeout : HttpStatusCode.Continue),
									friends, null, null), disconnectHandler));
					}

                    if ( webreq != null )
                        webreq.Abort ();

					if (sr != null)
						sr.Close();
					if (st != null)
						st.Close();

					sr = null;

					streamQueue.Wait();
					Console.WriteLine("終了");
				}
				if (isAbortException)
					break;
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

		/// <summary>
		/// UserStreamスレッドが終了したときに呼び出される
		/// </summary>
		private void StoppedUserStreaming()
		{
		}

        /// <summary>
        /// イベントが発生した際に実行される
        /// </summary>
        /// <param name="line"></param>
        private void RaiseEvents ( TwitterInfo srv, string line, ref List<decimal> friends, UserStreamQueue streamQueue )
        {
            var data = DynamicJson.Parse ( line );

            if ( data.IsDefined ( "friends" ) )
            {
                friends = ( (List<decimal>)data.friends );
                streamQueue.Enqueue
                    ( new UserStreamQueueData ( this, new TwitterCompletedEventArgs ( srv, HttpStatusCode.OK, friends, null, null ), disconnectHandler ) );
            } else if ( data.IsDefined ( "id" ) )
            {
                //  ツイート
                streamQueue.Enqueue
                    ( new UserStreamQueueData ( this, new TwitterCompletedEventArgs ( srv, HttpStatusCode.OK, friends, new TwitterStatus ( data ), null ), completedHandler ) );
            } else if ( data.IsDefined ( "event" ) )
            {
                //  イベント
                if ( data["event"] == "favorite" || data["event"] == "unfavorite" ||
                    data["event"] == "follow" || data["event"] == "unfollow" || data["event"] == "user_update" )
                {
                    var notify = new TwitterNotifyStatus ( srv, data );
                    friendsControl ( friends, notify );
                    streamQueue.Enqueue
                        ( new UserStreamQueueData ( this, new TwitterCompletedEventArgs ( srv, HttpStatusCode.OK, friends, notify, null ), notifyHandler ) );
                }
            } else if ( data.IsDefined ( "direct_message" ) )
            {
                //  ダイレクトメッセージ
                var directMessage = new TwitterDirectMessageStatus ( data.direct_message );
                streamQueue.Enqueue
                        ( new UserStreamQueueData ( this, new TwitterCompletedEventArgs ( srv, HttpStatusCode.OK, friends, directMessage, null ), completedHandler ) );
            }
        }

        private void friendsControl ( List<decimal> friends, TwitterNotifyStatus notify )
        {
            if ( notify.isOwnFollow )
            {
                friends.Add ( notify.target.id );
            }
            if ( notify.isOwnUnFollow )
            {
                friends.Remove ( notify.target.id );
            }
        }
    }
}
