using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Timers;
using Shrimp.Module;
using Shrimp.Twitter.Status;

namespace Shrimp.ControlParts.Timeline
{
    /// <summary>
    /// Timeline Class
    /// タイムラインの制御
    /// </summary>
    class Timeline : ITimeline, IDisposable
    {
        #region 定義

        private Stack<TwitterStatus> tweetStack;
        private Stack<TwitterStatus> tweetStackRange;
        /// <summary>
        /// いままでのツイートID
        /// </summary>
        private Stack<decimal> tweetStacks;
        private Stack<TwitterNotifyStatus> notifyStacks;
        private Timer bombDetect;
        /// <summary>
        /// タイマーが動くまでに拾ったツイート数
        /// </summary>
        private int bombDetectTweetNum;
        /// <summary>
        /// 設定の、タイマーが動くまでに拾ったツイート数を記録してある変数
        /// </summary>
        private int setting_bombDetectTweetNum;
        /// <summary>
        /// 爆撃検知タイマーが動いたか？
        /// </summary>
        private bool isBombDetectTimer;
        private object lockObj = new object ();
        private object lockRangeObj = new object ();
        private object lockStacks = new object ();
        #endregion

        #region コンストラクタ

        public Timeline ()
        {
            this.tweetStack = new Stack<TwitterStatus> ();
            this.tweetStackRange = new Stack<TwitterStatus> ();
            this.tweetStacks = new Stack<decimal> ();
            this.notifyStacks = new Stack<TwitterNotifyStatus> ();
            this.bombDetect = new Timer();
            this.bombDetectTweetNum = 0;
            this.setting_bombDetectTweetNum = Setting.BombDetect.bombDetectTweetNum;
            this.isBombDetectTimer = false;

            //  タイマーの設定を行う
            this.bombDetect.Elapsed += new ElapsedEventHandler(bombDetect_Elapsed);
            this.bombDetect.Interval = Setting.BombDetect.bombDetectSec;
        }

        public void Dispose()
        {
            this.tweetStack.Clear ();
            this.tweetStack = null;
            this.tweetStacks.Clear ();
            this.tweetStacks = null;
            this.bombDetect.Stop();
            this.bombDetect.Elapsed -= new ElapsedEventHandler(bombDetect_Elapsed);
            this.bombDetect = null;
        }

        #endregion

        #region メソッド

		/// <summary>
		/// タイムラインツイートが保存(キャッシュ)されているかどうか
		/// </summary>
		public bool isSavingTimeline
		{
			get { return this.tweetStack.Count != 0; }
		}

        /// <summary>
        /// 一度に大量のツイートを受信すると、ここがtrueになる(Rangeを利用した場合のみ)
        /// </summary>
        public bool isBigTweetReceived
        {
            get { return this.tweetStackRange.Count != 0; }
        }

        /// <summary>
        /// タイムラインツイートの数
        /// </summary>
        public int Count
        {
            get { return this.tweetStack.Count; }
        }

        /// <summary>
        /// タイムラインにツイートを一時的に追加
        /// プッシュされたツイートは、タイムラインへのイベント通知とともに反映される
        /// </summary>
        public void PushTweet ( TwitterStatus tweet )
        {
            lock ( lockObj )
            {
                lock ( lockStacks )
                {
                    if ( this.tweetStacks.Any ( ( twit ) => twit == tweet.id ) )
                        return;
                    if ( tweet.isNotify )
                    {
                        if ( this.notifyStacks.Any ( ( notify ) =>
                            ( notify.notify_event == tweet.NotifyStatus.notify_event ) &&
                            ( notify.source.id == tweet.NotifyStatus.source.id ) &&
                            ( notify.target.id == tweet.NotifyStatus.target.id ) &&
                            ( notify.isFollow ? true : ((TwitterStatus)notify.target_object).id == ((TwitterStatus)tweet.NotifyStatus.target_object).id ) ) )
                            return;
                    }
                }
                if ( !this.isBombDetectTimer && this.bombDetectTweetNum++ > setting_bombDetectTweetNum )
                {
                    //  爆撃っぽい

                    //Console.WriteLine ( "!!" );
                }
                else
                {
                    //this.tweetStack.Push(tweet);
                }
                this.tweetStack.Push ( tweet );
                lock ( lockStacks )
                {
                    this.tweetStacks.Push ( tweet.id );
                    if ( tweet.isNotify )
                        this.notifyStacks.Push ( tweet.NotifyStatus );
                }

                //  検知用タイマーのフラグは折る
                this.isBombDetectTimer = false;
            }
        }

        /// <summary>
        /// タイムラインなんかを取得したときに、いっきにツイートをいれる
        /// </summary>
        /// <param name="tweets"></param>
        public int PushTweetRange ( List<TwitterStatus> tweets )
        {
            int num = 0;
            lock ( lockRangeObj )
            {
                foreach ( TwitterStatus t in tweets )
                {
                    lock ( lockStacks )
                    {
                        if ( this.tweetStacks.Any ( ( twit ) => twit == t.id ) )
                            continue;
                        this.tweetStacks.Push ( t.id );
                    }
                    num++;
                    this.tweetStackRange.Push ( t );
                }
            }
            return num;
        }

        /// <summary>
        /// プッシュされたツイートをPOPする
        /// </summary>
        /// <returns>Popされたツイート</returns>
        public TwitterStatus PopTweet ()
        {
            lock ( lockObj )
            {
                if ( this.tweetStack.Count != 0 )
                    return this.tweetStack.Pop ();
            }
            return null;
        }


        /// <summary>
        /// プッシュされたツイートをすべてPOPする
        /// </summary>
        /// <returns>Popされたツイート</returns>
        public List<TwitterStatus> PopAllTweet ()
        {
            List<TwitterStatus> statuses = null;
            lock ( lockRangeObj )
            {
                statuses = new List<TwitterStatus> ();
                if ( this.tweetStackRange.Count != 0 )
                {
                    statuses.AddRange ( this.tweetStackRange.ToList () );
                    this.tweetStackRange.Clear ();
                }
            }
            lock ( lockObj )
            {
                if ( this.tweetStacks.Count != 0 )
                {
                    var lis = this.tweetStack.ToList ();
                    lis.Sort ( delegate ( TwitterStatus x, TwitterStatus y )
                    {
                        return y.id.CompareTo ( x.id );
                    } );
                    statuses.AddRange ( lis );

                    this.tweetStack.Clear ();
                }
            }
            return statuses;
        }

        /// <summary>
        /// ツイートをソートしていれる
        /// </summary>
        public void PopTweetSort ()
        {
            lock ( lockObj )
            {
                var lis = this.tweetStack.ToList ();
                lis.Sort ( delegate ( TwitterStatus x, TwitterStatus y )
                {
                    return y.id.CompareTo ( x.id );
                } );
                this.tweetStack.Clear ();
                foreach ( var tmp in lis )
                {
                    this.tweetStack.Push ( tmp );
                }
            }
        }

        /// <summary>
        /// タイムラインからツイートを削除
        /// </summary>
        /// <param name="id">tweetのid</param>
        public void DeleteTimeline(decimal id)
        {
        }

        /// <summary>
        /// 爆撃検知用タイマー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void bombDetect_Elapsed(object sender, ElapsedEventArgs e)
        {
            //  秒数が経過すると飛んでくるので
            this.isBombDetectTimer = true;
            this.bombDetectTweetNum = 0;
        }

        /// <summary>
        /// 設定が変更されたときに飛んできます
        /// </summary>
        public void OnChangeSetting()
        {
            this.bombDetect.Interval = Setting.BombDetect.bombDetectSec;
            this.setting_bombDetectTweetNum = Setting.BombDetect.bombDetectTweetNum;
        }

        #endregion
    }
}
