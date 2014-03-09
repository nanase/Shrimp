using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Timers;
using Shrimp.Module.Parts;
using System.Net;
using System.IO;
using Shrimp.Module;
using Codeplex.Data;
using Shrimp.ControlParts.Timeline.Animation;
using Shrimp.Twitter;
using Shrimp.Twitter.Status;
using Shrimp.Twitter.Entities;
using Shrimp.Setting;
using Shrimp.Module.TimeUtil;
using Shrimp.ControlParts.ContextMenus.TextMenu;
using Shrimp.ControlParts.Timeline.Draw;
using Shrimp.ControlParts.Timeline.Click;
using Shrimp.ControlParts.Timeline.Select;
using System.Diagnostics;
using Shrimp.Account;
using Shrimp.ControlParts.Tabs;

namespace Shrimp.ControlParts.Timeline
{
    /// <summary>
    /// タイムラインを表示するコントロール
    /// </summary>
    public partial class TimelineControl : UserControl, IControl
    {
        #region 定義

        /// <summary>
        /// タイムラインのデータ管理
        /// </summary>
        private Timeline timeline;
        private SelectControl selectControl;
        private SelTextContextMenu selTextContextMenu;
        private SelTweetContextMenu selTweetContextMenu;
        private SelUserContextMenu selUserContextMenu;
        private TweetDraw tweetDraw;
        //  クリック位置
        private ClickCells clickCells = new ClickCells ();
        //  ツイート
        private List<TwitterStatus> tweets = new List<TwitterStatus> ();
        private System.Windows.Forms.Timer attachTweetTimer;
        //  イメージのチェックくらいなら、UIスレッドで回してもよさそう
        private System.Windows.Forms.Timer imageCheckTimer;
        private AnimationControl anicon;
        private TweetInsertAnimation insert_anime;
        private TweetNotifyAnimation notify_anime;
        private TabChangeAnimation tabchange_anime;

        private int drawNum = 0;
        private Brush originBackGroundColor = new SolidBrush ( SystemColors.Control );

        /// <summary>
        /// ツイートの描画位置
        /// </summary>
        private int _StartTweetShowPosition = 0;

        private bool ChangingControl = false;

        /// <summary>
        /// 選択中のツイート
        /// </summary>
        private decimal SelectTweetID = -1;
        /// <summary>
        /// 会話を開くかどうか(クリックで切り替えられる)
        /// </summary>
        private bool OpenConversation = false;
        /// <summary>
        /// マウスホバー中ツイートID
        /// </summary>
        private decimal HoverTweetID = -1;
        /// <summary>
        /// マウスホバー中の位置
        /// </summary>
        private Point HoverLocation;

        /// <summary>
        /// 取得漏れしたらしきツイート数
        /// </summary>
        private int DelayTweetNum = 0;
        private int DelayPercentage = 0;

        /// <summary>
        /// 凍結中かどうか
        /// </summary>
        private bool isSuspended = false;
        #endregion

        #region イベント
        public delegate void OnChangedTweetHandler ( TwitterStatus tweet );
        public event OnChangedTweetHandler OnChangeTweet;
        //  ツイートの取得漏れ率
        public delegate void OnChangedTweetDelayPercentageHandler ( int Percentage );
        public event OnChangedTweetDelayPercentageHandler OnChangedTweetDelayPercentage;
        /// <summary>
        /// TimelineControlからTwitterAPIを操作するときに使うデリゲート
        /// </summary>
        /// <param name="category"></param>
        public delegate void OnUseTwitterAPIDelegate ( object sender, object[] arg );
        public event OnUseTwitterAPIDelegate OnUseTwitterAPI;
        /// <summary>
        /// アカウント情報をアクセスする
        /// *必須
        /// </summary>
        public delegate AccountManager OnRequiredAccountInfoDeleagate ();
        public event OnRequiredAccountInfoDeleagate OnRequiredAccountInfo;
        /// <summary>
        /// リプライをUIに送信するフォーム
        /// </summary>
        /// <param name="selectedTweet"></param>
        public delegate void OnCreatedReplyDataDelegate ( TwitterStatus selectedTweet, bool isDirectMessage );
        public event OnCreatedReplyDataDelegate OnCreatedReplyData;

        /// <summary>
        /// タブ操作するときに使うデリゲートっぽい。
        /// </summary>
        /// <param name="type"></param>
        /// <param name="detail"></param>
        public delegate TabControls TabControlOperationgDelegate ( ActionType type, object detail, string tabID = "", bool isBootingup = false );
        public event TabControlOperationgDelegate TabControlOperatingHandler;
        /// <summary>
        /// Shrimpからデータを得るときに
        /// </summary>
        public delegate object OnRequiredShrimpData ( string type );
        /// <summary>
        /// Shrimpをリロードするときにつかおう
        /// </summary>
        public delegate void OnReloadShrimp ();
        public delegate void ReplyTweetDelegate ( decimal id, bool isDM );
        public delegate TwitterStatus SearchTweetDelegate ( decimal id );
        public delegate void OnNotifyClickedDelegate ( decimal id );
        private int ConversationNum = 0;
        private bool UseConversationBack = false;
        /// <summary>
        /// 通知領域がおされると、一回通知が引っ込む。一番上にいくまでは、通知領域は再度出さないために、このフラグがオンになる。
        /// </summary>
        private bool isClickedNotifyAnimation = false;
        /// <summary>
        /// downの段階で、最初に選択されたことを表示する変数
        /// </summary>
        private bool isFirstClickDown = false;
        /// <summary>
        /// 画面切り替えられて、最初にResumeされたらtrueになる
        /// </summary>
        private bool isFirstResume = false;
        /// <summary>
        /// ロード待ちの画像とか変数とか
        /// </summary>
        private System.Windows.Forms.Timer loadingTimer;
        private PictureBox loadingBox = new PictureBox ();
        private bool isSetBox = false;
        // SQLによる追加時、一番上の表示を示すフラグとしてtrueになる
        private bool isSetFirstViewBySQL = false;
        private bool isDisposedShrimp = false;
        #endregion

        #region コンストラクタ

        public TimelineControl()
        {
            InitializeComponent();

            
            SetStyle (
                        ControlStyles.OptimizedDoubleBuffer |
                        ControlStyles.AllPaintingInWmPaint,
                        true );
            SetStyle ( ControlStyles.ResizeRedraw, true );

            this.MouseWheel += new MouseEventHandler ( TimelineControl_MouseWheel );

            this.timeline = new Timeline();
            this.attachTweetTimer = new System.Windows.Forms.Timer ();

            this.selTextContextMenu = new SelTextContextMenu ();
            this.selTextContextMenu.ItemClicked += new ToolStripItemClickedEventHandler ( selTextContextMenu_ItemClicked );
            this.selTweetContextMenu = new SelTweetContextMenu ();
            this.selTweetContextMenu.MenuOpening += new EventHandler ( selTweetContextMenu_MenuOpening );
            this.selTweetContextMenu.MenuItemClicked += new ToolStripItemClickedEventHandler ( selTweetContextMenu_MenuClicked );
            this.selUserContextMenu = new SelUserContextMenu ();
            this.selUserContextMenu.MenuItemClicked += new ToolStripItemClickedEventHandler ( selTweetContextMenu_MenuClicked );
            //this.selTextContextMenu.MenuClosed += new EventHandler ( selContextMenu_MenuClosed );
            //this.selTweetContextMenu.MenuClosed += new EventHandler ( selContextMenu_MenuClosed );

            tweetDraw = new TweetDraw ();
            this.selectControl = new SelectControl ();
            this.vsc.Maximum = 0;

			this.attachTweetTimer.Interval = Setting.Timeline.refTimeline;
            this.attachTweetTimer.Tick += new EventHandler ( attachTweetTimer_Tick );
			this.attachTweetTimer.Start ();

            this.imageCheckTimer = new System.Windows.Forms.Timer ();
            this.imageCheckTimer.Interval = 1000;
            this.imageCheckTimer.Tick += new EventHandler ( imageCheckTimer_Tick );
            this.imageCheckTimer.Start ();

            this.insert_anime = new TweetInsertAnimation ();
            this.notify_anime = new TweetNotifyAnimation ();
            this.tabchange_anime = new TabChangeAnimation ();
            this.anicon = new AnimationControl ( this.RedrawControl, this.notify_anime.FrameExecute, 16,
                                            this.insert_anime.FrameExecute, 16, this.tabchange_anime.FrameExecute, 16 );

            this.loadingTimer = new System.Windows.Forms.Timer ();
            this.loadingTimer.Tick += new EventHandler ( loadingTimer_Tick );
            this.loadingTimer.Interval = 100;
            this.loadingTimer.Start ();
            loadingBox.Image = (Image)Properties.Resources.loadingAnime.Clone ();
            loadingBox.Dock = DockStyle.Fill;
        }

        void loadingTimer_Tick ( object sender, EventArgs e )
        {
            if ( tweets.Count == 0 && !isLoadingFinished )
            {
                if ( !this.isSetBox )
                {
                    this.Controls.Add ( loadingBox );
                    this.isSetBox = true;
                }
            }
            else
            {
                this.Controls.Remove ( loadingBox );
                //this.loadingBox.Dispose ();
                //this.loadingBox = null;
                this.loadingTimer.Stop ();
                //this.loadingTimer.Tick -= new EventHandler ( loadingTimer_Tick );
                //this.loadingTimer.Dispose ();
                //this.loadingTimer = null;
            }
        }

        /// <summary>
        /// デストラクタ
        /// </summary>
        ~TimelineControl ()
        {
            if ( !IsDisposed )
                base.Dispose ( false );
        }

        public void ShrimpDispose ( bool isDisposed)
        {
            if ( !isDisposedShrimp )
            {
                this.anicon.Dispose (); this.anicon = null;
                this.attachTweetTimer.Stop ();
                this.attachTweetTimer.Tick -= new EventHandler ( attachTweetTimer_Tick );
                this.attachTweetTimer.Dispose ();
                this.attachTweetTimer = null;

                this.imageCheckTimer.Stop ();
                this.imageCheckTimer.Tick -= new EventHandler ( imageCheckTimer_Tick );
                this.imageCheckTimer.Dispose ();
                this.imageCheckTimer = null;

                if ( this.loadingTimer != null )
                {
                    this.loadingTimer.Stop ();
                    this.loadingTimer.Tick -= new EventHandler ( loadingTimer_Tick );
                    this.loadingTimer.Dispose ();
                    this.loadingTimer = null;
                }

                this.selTweetContextMenu.MenuItemClicked -= new ToolStripItemClickedEventHandler ( selTweetContextMenu_MenuClicked );
                this.selTextContextMenu.ItemClicked -= new ToolStripItemClickedEventHandler ( selTextContextMenu_ItemClicked );
                this.selTweetContextMenu.MenuOpening -= new EventHandler ( selTweetContextMenu_MenuOpening );
                this.selUserContextMenu.MenuItemClicked -= new ToolStripItemClickedEventHandler ( selTweetContextMenu_MenuClicked );
                this.selTweetContextMenu.Dispose (); this.selTweetContextMenu = null;
                this.selTextContextMenu.Dispose (); this.selTextContextMenu = null;
                this.selUserContextMenu.Dispose (); this.selUserContextMenu = null;
                this.MouseWheel -= new MouseEventHandler ( TimelineControl_MouseWheel );
                
                this.selectControl.Dispose (); this.selectControl = null;
                this.tweetDraw.Dispose (); this.tweetDraw = null;
                this.clickCells.Dispose (); this.clickCells = null;
                this.tweets.Clear (); this.tweets = null;
                this.insert_anime.Dispose (); this.insert_anime = null;
                this.notify_anime.Dispose (); this.notify_anime = null;
                this.originBackGroundColor.Dispose (); this.originBackGroundColor = null;
                this.timeline.Dispose (); this.timeline = null;
                isDisposedShrimp = true;
            }
        }

        #endregion

        /// <summary>
        /// ツイート数
        /// </summary>
        public int TweetCount
        {
            get
            {
                return this.tweets.Count;
            }
        }

        public void initialize ()
        {
            this.timeline.PopAllTweet ();
            this.tweets.Clear ();
            this.isLoadingFinished = false;
            SetFirstView ();
            this.loadingTimer.Start ();
            this.RedrawControl ();
        }


        /// <summary>
        /// タイムラインに挿入する
        /// </summary>
        /// <param name="t"></param>
        public void InsertTimeline ( TwitterStatus t )
        {
            if ( timeline == null || t == null )
                return;
            this.timeline.PushTweet ( t );
        }

        /// <summary>
        /// タイムラインを一度に大量に入れる
        /// </summary>
        /// <param name="t"></param>
        public int InsertTimelineRange ( List<TwitterStatus> t )
        {
            if ( timeline == null || t == null )
                return 0;
            return this.timeline.PushTweetRange ( t );
        }

        /// <summary>
        /// 一番上にビューをもっていきます
        /// </summary>
        public void SetFirstView ()
        {
            this.Invoke ( (MethodInvoker)delegate ()
            {
                this.vsc.Value = 0;
                this.StartTweetShowPosition = 0;
                if ( this.tweets.Count != 0 )
                    this.SelectTweetID = this.tweets[0].id;
                else
                    this.SelectTweetID = -1;
                this.RedrawControl ();
            } );
        }

        /// <summary>
        /// SQLで挿入したら、使ってください
        /// </summary>
        public void SetSQL ()
        {
            this.Invoke ( (MethodInvoker)delegate ()
            {
                isSetFirstViewBySQL = true;
            } );
        }

        /// <summary>
        /// 最後を表示しているかどうか
        /// </summary>
        public bool isLastView
        {
            get {
                return (( StartTweetShowPosition + drawNum ) >= tweets.Count - 1);
            }
        }

        /// <summary>
        /// StatusIDだけ羅列されたListを返します
        /// </summary>
        public List<decimal> GenerateStatusIDs
        {
            get
            {
                if ( this.tweets != null )
                {
                    var newTweet = new List<TwitterStatus> ();
                    if ( this.timeline != null && ( this.timeline.isBigTweetReceived || this.timeline.isSavingTimeline ) )
                    {
                        newTweet.AddRange ( this.timeline.PopAllTweet () );
                    }
                    newTweet.AddRange ( this.tweets );
                    var lis = newTweet.FindAll ( ( t ) => !t.isNotify );
                    return lis.ConvertAll ( x => x.id );
                }
                return null;
            }
        }

        /// <summary>
        /// タブのアニメーションを開始させる
        /// </summary>
        /// <param name="BeforeControl">前表示してたコントロール</param>
        /// <param name="AfterControl">いま表示してるコントロール</param>
        /// <param name="tabLeftRight">左にタブが移動するか、右にいくか</param>
        public void StartTabChangeAnimation ( Bitmap BeforeControl, Bitmap AfterControl, bool tabLeftRight, bool tabVertical )
        {
            if ( BeforeControl != null && AfterControl != null )
                this.tabchange_anime.StartAnimation ( new object[] { BeforeControl, AfterControl, tabLeftRight, tabVertical } );
        }

        /// <summary>
        /// 画像のロードが終わったかチェック（タイマーで回すのが効率良いかな？)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void imageCheckTimer_Tick ( object sender, EventArgs e )
        {
            if ( this.tweetDraw.cacheWaitingURLs.Count != 0 )
            {
                anicon.SetRedrawQueue ();
            }
        }

        public void ReplySelectedTweet ( decimal tweetID, bool isDirectMessage )
        {
            if ( tweetID >= 0 )
            {
                var tweet = tweets.Find ( ( t ) => t.DynamicTweet.id == tweetID );
                if ( tweet != null && OnCreatedReplyData != null )
                {
                    OnCreatedReplyData.Invoke ( tweet, tweet.isDirectMessage | isDirectMessage );
                }
            }
        }
        /// <summary>
        /// メニューが押されたときに実行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void selTweetContextMenu_MenuClicked ( object sender, ToolStripItemClickedEventArgs e )
        {
            if ( OnUseTwitterAPI == null )
                return;

            if ( sender != null && sender is string )
            {
                //  ユーザー情報がおされたっぽい
                var scr = sender as string;
                if ( e.ClickedItem.Name == "OpenUserInformationTabMenu" )
                {
                    ActionControl.DoAction ( ActionType.Mention, scr );
                }
                else if ( e.ClickedItem.Name == "OpenUserTimelineTabMenu" )
                {
                    ActionControl.DoAction ( ActionType.UserTimeline, scr );
                }
                else if ( e.ClickedItem.Name == "OpenUserFavTimelineTabMenu" )
                {
                    ActionControl.DoAction ( ActionType.UserFavoriteTimeline, scr );
                }
                else if ( e.ClickedItem.Name == "OpenReplyToUserTabMenu" )
                {
                    ActionControl.DoAction ( ActionType.Search, "@" + scr + "" );
                }
                else if ( e.ClickedItem.Name == "OpenConversationTabMenu" )
                {
                    ActionControl.DoAction ( ActionType.Search, "from:" + scr + " OR @" + scr + "" );
                }
                else if ( e.ClickedItem.Name == "FollowMenu" )
                {
                    selTweetContextMenu.MenuClose ();
                    selUserContextMenu.MenuClose ();
                    ActionControl.DoAction ( ActionType.Follow, scr );
                }
                else if ( e.ClickedItem.Name == "BlockMenu" )
                {
                    selTweetContextMenu.MenuClose ();
                    selUserContextMenu.MenuClose ();
                    ActionControl.DoAction ( ActionType.Block, scr );
                }
                 else if ( e.ClickedItem.Name == "ReportSpamMenu" )
                 {
                     selTweetContextMenu.MenuClose ();
                     selUserContextMenu.MenuClose ();
                     ActionControl.DoAction ( ActionType.Spam, scr );
                 }
            } else {
                if ( SelectTweetID < 0 )
                    return;
                //selTweetContextMenu.MenuClose ();
                var tweet = tweets.Find ( ( s ) => s.id == SelectTweetID );
                if ( tweet == null )
                    return;

                //  アカウント選択時に、ユーザーIDを引っ張ってくる。
                TwitterInfo accountSelectedID = null;
                if ( e.ClickedItem.Name == "AccountSelected" )
                {
                    accountSelectedID = (TwitterInfo)e.ClickedItem.Tag;
                }

                if ( e.ClickedItem.Name == "RetweetMenu" || ( e.ClickedItem.OwnerItem != null && e.ClickedItem.OwnerItem.Name == "RetweetMenu" && e.ClickedItem.Name == "AccountSelected" ) )
                {
                    selTweetContextMenu.MenuClose ();
                    ActionControl.DoAction ( ActionType.Retweet, tweet.DynamicTweet.id, selUserContextMenu, ReplySelectedTweet, 
                    (SearchTweetDelegate)delegate (decimal id) {
                        return tweets.Find ( (t) => t.DynamicNotifyTweet.id == id );
                    }, accountSelectedID );
                }
                else if ( e.ClickedItem.Name == "FavMenu" || ( e.ClickedItem.OwnerItem != null && e.ClickedItem.OwnerItem.Name == "FavMenu" && e.ClickedItem.Name == "AccountSelected" ) )
                {
                    selTweetContextMenu.MenuClose ();
                    ActionControl.DoAction ( ActionType.Favorite, tweet.DynamicTweet.id, selUserContextMenu, ReplySelectedTweet, 
                    (SearchTweetDelegate)delegate (decimal id) {
                        return tweets.Find ( (t) => t.DynamicNotifyTweet.id == id );
                    }, accountSelectedID );
                }
                else if ( e.ClickedItem.Name == "UnFavMenu" || ( e.ClickedItem.OwnerItem != null && e.ClickedItem.OwnerItem.Name == "UnFavMenu" && e.ClickedItem.Name == "AccountSelected" ) )
                {
                    OnUseTwitterAPI.Invoke ( null, new object[] { "unfav", tweet.id, accountSelectedID } );
                }
                else if ( e.ClickedItem.Name == "ReplyMenu" || e.ClickedItem.Name == "ReplyDMMenu" )
                {
                    ReplySelectedTweet ( tweet.DynamicTweet.id, ( e.ClickedItem.Name == "ReplyDMMenu" ) );
                }
                else if ( e.ClickedItem.Name == "RegistBookmarkMenu" )
                {
                    if ( TabControlOperatingHandler != null )
                        TabControlOperatingHandler.Invoke ( ActionType.RegistBookMark, tweet );
                }
                else if ( e.ClickedItem.Name == "DeleteTweetMenu" )
                {
                    OnUseTwitterAPI.Invoke ( null, new object[] { "delete", tweet, null } );
                }
            }
        }

        /// <summary>
        /// テキストについてメニューがクリックされた飛んできます
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void selTextContextMenu_ItemClicked ( object sender, ToolStripItemClickedEventArgs e )
        {
            if ( e.ClickedItem.Name == "CopyMenu" )
            {
                if ( selectControl.selText != null )
                    Clipboard.SetDataObject ( selectControl.selText, true );
            }
            if ( e.ClickedItem.Name == "SelectAllMenu" )
            {
                if ( SelectTweetID >= 0 )
                {
                    var t = ( tweets.Find ( ( twit ) => twit.id == SelectTweetID ) );
                    if ( t != null )
                    {
                        selectControl.SelectAll ( t.DynamicTweet.text, SelectTweetID );
                        this.anicon.SetRedrawQueue ();
                    }
                }
            }
            if ( e.ClickedItem.Name == "GoogleSearchMenu" )
            {
                if ( selectControl.selText != null )
                    Process.Start ( "http://www.google.co.jp/search?q="+ WebUtility.HtmlEncode ( selectControl.selText ) +"" );
            }

            if ( e.ClickedItem.Name == "TwitterSearchMenu" )
            {
                if ( selectControl.selText != null && TabControlOperatingHandler != null )
                {
                    TabControlOperatingHandler.Invoke ( ActionType.Search, selectControl.selText );
                }
            }
        }

		/// <summary>
		/// ツイートが貯まってるかを定期チェック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        void attachTweetTimer_Tick ( object sender, EventArgs e )
		{
            //  ツイートリミット
            bool isModified = false;
            bool isBig = false;
            var countNum = tweets.Count;
            if ( countNum >= Setting.Timeline.SavedTimelineTweetNum && !isLastView )
            {
                tweets.RemoveAt ( tweets.Count - 1 );
            }

            if ( ( this.timeline.isSavingTimeline || this.timeline.isBigTweetReceived )
                    && !isShowingMenu  && !this.ChangingControl && !this.insert_anime.Enable && !this.selectControl.isSelecting)
            {
                //	あった
                this.attachTweetTimer.Interval = ( this.timeline.Count != 1 ? 16 : 500 );
                if ( this.timeline.isBigTweetReceived )
                {
                    //  現在位置を取得する
                    decimal bakStatus = -1;
                    if ( this.tweets.Count != 0 && this.tweets.Count >= StartTweetShowPosition - 1 && !isSetFirstViewBySQL )
                        bakStatus = this.tweets[( StartTweetShowPosition - 1 < 0 ? 0 : StartTweetShowPosition - 1 )].id;
                    this.tweets.AddRange ( this.timeline.PopAllTweet () );
                    isModified = true;
                    isBig = true;
                    this.tweets.Sort ( delegate ( TwitterStatus x, TwitterStatus y )
                    {
                        return y.id.CompareTo ( x.id );
                    } );
                    if ( bakStatus >= 0 )
                    {
                        int index = this.tweets.FindIndex ( ( t ) => t.id == bakStatus );
                        if ( index >= 0 )
                            StartTweetShowPosition = index;
                    }
                }
                else
                {
                    //this.attachTweetTimer.Interval = ( this.timeline.Count != 1 ? 16 : 500 );
                    this.isSetFirstViewBySQL = false;
                    if ( this.isFirstResume )
                        this.timeline.PopTweetSort ();
                    var tweet = this.timeline.PopTweet ();

                    TwitterStatus now_tweet = null;
                    if ( tweet.NotifyStatus != null )
                    {
                        if ( this.tweets.Count > 1 )
                        {
                            tweet.id = this.tweets[0].id + 1;
                        }

                        if ( !tweet.NotifyStatus.isRetweeted && ( tweet.NotifyStatus.isOwnFav || tweet.NotifyStatus.isOwnUnFav ) )
                        {
                            var notify_tweet = tweet.NotifyStatus.target_object as TwitterStatus;
                            now_tweet = this.tweets.Find ( ( t ) => t.id == notify_tweet.id );
                            if ( now_tweet != null )
                            {
                                now_tweet.favorite_count += ( tweet.NotifyStatus.isOwnFav ? 1 : -1 );
                                now_tweet.favorited = ( tweet.NotifyStatus.isOwnFav ? true : false );
                            }
                            else
                            {
                                return;
                            }
                        }
                    }

                    if ( now_tweet == null )
                    {
                        if ( StartTweetShowPosition == 0 && !this.isFirstResume )
                        {
                            if ( Setting.Timeline.isEnableInsertAnimation )
                                this.insert_anime.StartAnimation ();
                        }
                        else
                        {
                            if ( Setting.Timeline.isEnableNotifyAnimation && !this.notify_anime.Enable && !this.isClickedNotifyAnimation )
                            {
                                this.notify_anime.Enable = true;
                                this.notify_anime.offsetTweetID = ( this.tweets.Count == 0 ? 0 : this.tweets[0].id );
                            }
                            StartTweetShowPosition++;
                        }
                        this.isFirstResume = false;

                        //StartTweetShowPosition++
                        this.tweets.Insert ( 0, tweet );
                        isModified = true;
                    }
                }

                //  スクロールバーの最大値を増加
                if ( isModified )
                {
                    if ( vsc.InvokeRequired )
                    {
                        this.Invoke ( (MethodInvoker)delegate ()
                        {
                            vsc.Maximum = this.tweets.Count;
                        } );
                    }
                    else
                    {
                        vsc.Maximum = this.tweets.Count;
                    }
                }

                //  プロフィール画像先読み
                //if (!ImageCache.isCached(this.tweets[0].user.profile_image_url))
                //{
                    //   ImageCache.SetQueueImage(this.tweets[0].user.profile_image_url, true);
                //}
                if ( isModified && !isBig && ( Setting.Timeline.isEnableInsertAnimation && !this.insert_anime.Enable ) && ( Setting.Timeline.isEnableNotifyAnimation && !this.notify_anime.Enable ) )
                    return;
                //  リドロー
                this.anicon.SetRedrawQueue();
            }
		}

        /// <summary>
        /// タイムラインを凍結させます
        /// </summary>
        public void Suspend ()
        {
            if ( !this.isSuspended )
            {
                this.isSuspended = true;
                this.anicon.Stop ();
                this.attachTweetTimer.Stop ();
                this.imageCheckTimer.Stop ();
                if ( this.loadingTimer != null )
                    this.loadingTimer.Stop ();
                this.isFirstResume = false;
            }
        }

        /// <summary>
        /// 凍結したタイムラインを復活させます
        /// </summary>
        public void Resume ()
        {
            if ( this.isSuspended )
            {
                this.isFirstResume = true;
                this.anicon.Start ();
                this.attachTweetTimer.Start ();
                this.imageCheckTimer.Start ();
                if ( this.loadingTimer != null )
                    this.loadingTimer.Start ();
                this.Focus ();
                this.isSuspended = false;
            }
        }

        /// <summary>
        /// メニューを表示中？
        /// </summary>
        public bool isShowingMenu
        {
            get { return this.selTweetContextMenu.Visible || this.selTextContextMenu.Visible; }
        }

        /// <summary>
        /// ツイートメニューを開いている
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void selTweetContextMenu_MenuOpening ( object sender, EventArgs e )
        {
            if ( OnRequiredAccountInfo != null )
            {
                var selTweet = ( this.tweets.Find ( ( t ) => t.id == SelectTweetID ) );
                if ( selTweet != null )
                {
                    AccountManager account = OnRequiredAccountInfo.Invoke ();
                    selTweetContextMenu.isEnableDelTweet = account.accounts.Any ( (t)=>t.user_id == selTweet.user.id );
                }
            }
        }

        public void OnNotifyClicked ( decimal id )
        {
            int pos = this.tweets.FindIndex ( ( t ) => t.id == id );
            if ( pos >= 0 )
            {
                StartTweetShowPosition = ( pos - 1 < 0 ? 0 : pos - 1 );
                this.isClickedNotifyAnimation = true;
                this.notify_anime.Enable = false;
            }
            this.anicon.SetRedrawQueue ();
        }

        /// <summary>
        /// マウスホイールの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TimelineControl_MouseWheel ( object sender, MouseEventArgs e )
        {
            int num = 0;
            if ( e.Delta < 0 )
            {
                //  上に
                int tmp = this.vsc.Value + Math.Abs ( e.Delta / 120 );
                num = Math.Min ( tmp, this.vsc.Maximum );
            }
            else
            {
                // 下方向へ
                int tmp = this.vsc.Value - Math.Abs ( e.Delta / 120 );
                num = Math.Max ( tmp, this.vsc.Minimum );
            }
            this.StartTweetShowPosition = num;
            if ( StartTweetShowPosition == 0 )
                this.notify_anime.Enable = false;
            this.anicon.SetRedrawQueue ();
        }

        /// <summary>
        /// スクロール
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void vsc_Scroll ( object sender, ScrollEventArgs e )
        {
            StartTweetShowPosition = e.NewValue;
            if ( StartTweetShowPosition == 0 )
                this.notify_anime.Enable = false;
            this.anicon.SetRedrawQueue ();
            //vsc.Maximum = tweets.Count + drawNum -1;
        }

        /// <summary>
        /// スクロールバーの横幅を抜いた大きさ
        /// </summary>
        public int WidthWithoutScrollBar
        {
            get
            {
                return ( this.vsc != null && this.vsc.Visible ? this.Width - this.vsc.Width : this.Width );
            }
        }

        /// <summary>
        /// ツイート表示位置
        /// </summary>
        public int StartTweetShowPosition
        {
            get { return this._StartTweetShowPosition; }
            set
            {
                if ( value == 0 )
                    this.isClickedNotifyAnimation = false;
                if ( this.vsc.InvokeRequired )
                {
                    this.vsc.Invoke ( (MethodInvoker)delegate ()
                    {
                        this.vsc.Value = MathUtil.limit ( value, this.vsc.Minimum, this.vsc.Maximum );
                    } );
                }
                else
                {
                    this.vsc.Value = MathUtil.limit ( value, this.vsc.Minimum, this.vsc.Maximum );
                }
                _StartTweetShowPosition = value;
            }
        }

        public void ScrollUp ()
        {
            this.Invoke ( (MethodInvoker)delegate ()
            {
                var t = ( tweets.FindIndex ( ( twit ) => twit.id == SelectTweetID ) );
                if ( t >= 0 )
                {
                    var tmp = t - 1;
                    if ( tmp < 0 )
                        tmp = 0;
                    this.SelectTweetID = tweets[tmp].id;
                    if ( StartTweetShowPosition >= 0 && tmp < StartTweetShowPosition )
                    {
                        var tmp2 = StartTweetShowPosition - 1;
                        if ( tmp2 < 0 )
                            tmp2 = 0;
                        StartTweetShowPosition = tmp2;
                    }
                    this.Focus ();
                    this.anicon.SetRedrawQueue ();
                }
            } );

        }

        public void ScrollDown ()
        {
            this.Invoke ( (MethodInvoker)delegate ()
            {
                var t = ( tweets.FindIndex ( ( twit ) => twit.id == SelectTweetID ) );
                if ( t >= 0 )
                {
                    var tmp = t + 1;
                    if ( tmp >= this.tweets.Count - 1 )
                        tmp = this.tweets.Count - 1;
                    this.SelectTweetID = tweets[tmp].id;
                    if ( StartTweetShowPosition >= 0 && tmp >= StartTweetShowPosition + drawNum )
                    {
                        var tmp2 = StartTweetShowPosition + 1;
                        if ( tmp2 >= this.tweets.Count - 1 )
                            tmp2 = this.tweets.Count - 1;
                        StartTweetShowPosition = tmp2;
                    }
                    this.Focus ();
                    this.anicon.SetRedrawQueue ();
                }
            } );
        }


        /// <summary>
        /// もともと、何もデータを入れる必要がないか、エラーでロードが終わった場合はここをtrueにする
        /// </summary>
        public bool isLoadingFinished
        {
            get;
            set;
        }


        /// <summary>
        /// 描画時飛んでくる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimelineControl_Paint(object sender, PaintEventArgs e)
        {
            this.ChangingControl = true;
            //  切り替えアニメ
            if ( this.tabchange_anime.Enable )
            {
                this.tabchange_anime.Draw ( e.Graphics, this.Width, null );
                this.ChangingControl = false;
                return;
            }
            //  セル描画しなおしだから、初期化
            tweetDraw.initialize ();
            //  クリック位置初期化
            clickCells.initialize ();
            //  背景
            e.Graphics.FillRectangle ( originBackGroundColor, e.ClipRectangle );
            drawNum = 0;
            decimal next_reply_num = -1;
            int next_reply_tweet_num = -1;

            //  ツイート描画開始
            if ( StartTweetShowPosition >= tweets.Count - 1 )
                StartTweetShowPosition = tweets.Count - 1;
            if ( StartTweetShowPosition < 0 )
                StartTweetShowPosition = 0;


            //  ツイート会話のオフセットを調べる
            //  たとえば、ツイートの表示位置の上に選択されていて尚且つツイートがあったばあい、そこをオフセットにして表示する
            bool isBakoffTweetSelected = false;
            int setOffsetStartPosition = 0;
            ConversationNum = 0;
            UseConversationBack = false;
            if ( this.OpenConversation && SelectTweetID >= 0 )
            {
                //  選択されたツイートの位置を取得
                int firstViewPosition = tweets.FindIndex ( ( t ) => t.id == SelectTweetID );
                //  おるか？ｗ
                if ( firstViewPosition >= 0 )
                {
                    //  選択されたツイートを取得
                    TwitterStatus firstView = tweets[firstViewPosition];
                    //  in_reply_to_status_idはあるか？ｗ
                    if ( firstView.DynamicTweet.in_reply_to_status_id > 0 )
                    {
                        //  選択されたツイートが、表示位置より上にあった場合。
                        if ( firstViewPosition < StartTweetShowPosition )
                        {
                            decimal nextID = firstView.DynamicTweet.in_reply_to_status_id;
                            for ( ; ; )
                            {
                                ConversationNum++;
                                
                                var next = tweets.Find ( ( status ) => status.DynamicTweet.id == nextID );
                                if ( next == null )
                                    break;
                                nextID = next.DynamicTweet.in_reply_to_status_id;
                                if ( firstViewPosition + ConversationNum == StartTweetShowPosition )
                                {
                                    next_reply_num = next.DynamicTweet.id;
                                    isBakoffTweetSelected = true;
                                    UseConversationBack = true;
                                    break;
                                }
                                if ( nextID <= 0 || tweets.Find ( (status)=> status.DynamicTweet.id == nextID ) == null )
                                {
                                    isBakoffTweetSelected = true;
                                    UseConversationBack = true;
                                    setOffsetStartPosition -= ConversationNum;
                                    break;
                                }
                                
                                
                            }
                        }

                    }
                }
            }

			//	ツイート描画
            bool isFirstGet = false;
            for ( int i = StartTweetShowPosition + setOffsetStartPosition; i < tweets.Count; drawNum++ )
            {
				//	描画量が、ウィンドウをはみ出たら、終わる
                if ( tweetDraw.NextStartPositionOffsetY >= this.Height )
                    break;

                int offset_start_x = 0;
                if ( i < 0 )
                    i = 0;
                TwitterStatus tweet = tweets[i];
                int maxWidth_new = this.WidthWithoutScrollBar;
                var isConversation = false;
                if ( next_reply_num > 0 )
                {
                    isConversation = true;
                    next_reply_tweet_num = tweets.FindIndex ( ( status ) => status.DynamicTweet.id == next_reply_num );
                    if ( next_reply_tweet_num >= 0 && this.OpenConversation )
                    {
                        offset_start_x = 30;
                        tweet = tweets[next_reply_tweet_num];
                        e.Graphics.DrawImage ( Setting.ResourceImages.In_Reply_To_Status_ID_Arrow, new Rectangle ( 0, tweetDraw.NextStartPositionOffsetY,
                            30, 30 ) );
                        next_reply_num = ( tweet.DynamicTweet.in_reply_to_status_id > 0 ? tweet.DynamicTweet.in_reply_to_status_id : -1 );
                        if ( next_reply_num < 0 && isBakoffTweetSelected )
                        {
                            i -= ConversationNum;
                        }
                    }
                    else
                    {
                        //if ( tweets.Find ( ( t ) => t.DynamicTweet.id == next_reply_num ) == null )
                        //{
                        //    //  ツイートないから、さがしてくるか
                        //    OnUseTwitterAPI.Invoke ( this, new object[] { "loadNewTweet", next_reply_num, next_reply_num } );
                        //}
                        next_reply_num = -1;
                        i++;
                        if ( i >= tweets.Count - 1 )
                            i = tweets.Count - 1;
                        tweet = tweets[i];
                        isConversation = false;
                    }
                    
                }

                tweetDraw.isSelected = ( SelectTweetID == tweet.id );
                if ( tweetDraw.isSelected && this.OpenConversation && !isFirstGet )
                {
                    next_reply_num = ( tweet.DynamicTweet.in_reply_to_status_id != 0 ? tweet.DynamicTweet.in_reply_to_status_id : -1 );
                    isFirstGet = true;
                }
                tweetDraw.isHover = ( HoverTweetID == tweet.id );
                tweetDraw.isLine = ( Setting.Timeline.isLineMode && !tweetDraw.isSelected && !isConversation ? true : false );
                //  ツイート描画アニメーション
                if ( drawNum == 0 && StartTweetShowPosition == 0 && this.insert_anime.Enable )
                {
                    //  ツイート描画アニメーション
                    if ( this.insert_anime.YOffset == 0 )
                    {
                        if ( tweetDraw.isLine )
                        {
                            tweetDraw.StartLayoutLine ( tweet, isConversation, offset_start_x, this.WidthWithoutScrollBar );
                        }
                        else
                        {
                            tweetDraw.StartLayout ( tweet, isConversation, offset_start_x, this.WidthWithoutScrollBar );
                        }
                        var lay = tweetDraw.getLayout ();
                        this.insert_anime.YOffset = lay.CellSize;
                    }
                    tweetDraw.NextStartPositionOffsetY = this.insert_anime.StartPositionOffset;
                }

                tweetDraw.DrawTweet ( e.Graphics, tweet, isConversation, offset_start_x, this.WidthWithoutScrollBar,
                                                clickCells.SetClickLink, selectControl, this.PointToClient ( MousePosition ) );
                if ( next_reply_num < 0)
                  i++;
            }

            if ( this.notify_anime.Enable )
            {
                int num = 0;
                if ( StartTweetShowPosition >= 0 && tweets.Count - 1 >= StartTweetShowPosition )
                {
                    if ( this.notify_anime.offsetTweetID <= tweets[StartTweetShowPosition].id )
                        this.notify_anime.offsetTweetID = tweets[StartTweetShowPosition].id;
                }
                foreach ( TwitterStatus t in tweets )
                {
                    if ( t.id == this.notify_anime.offsetTweetID )
                        break;
                    num ++;
                }
                notify_anime.notifyText = "新着ツイート: " + num + "件\n@"+ tweets[0].user.screen_name +":"+ tweets[0].text.Replace ( "\r", "" ).Replace ("\n", "" ) +"";
			}
            notify_anime.Draw ( e.Graphics, this.WidthWithoutScrollBar, clickCells.SetClickLink );
            this.ChangingControl = false;
        }

        /// <summary>
        /// マウスが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimelineControl_MouseDown ( object sender, MouseEventArgs e )
        {
            if ( tweets.Count == 0 )
                return;
            int sel = 0;
            TimelineCellsTweetID tweet_id;
            var c = clickCells.getClickLink ( e.Location );
            if ( e.Button == MouseButtons.Left || e.Button == MouseButtons.Right )
            {
                if ( ( tweet_id = tweetDraw.IsCursorInCellTweetID ( e.Location ) ) != null && c == null )
                {
                    //  実際のツイートと比較
                    var selt = tweets.Find ( p => p.id == tweet_id.id );
                    if ( selt != null )
                    {
                        isFirstClickDown = false;
                        if ( SelectTweetID != selt.id )
                        {
                            this.OpenConversation = true;
                            selectControl.SelectEnd ( e.Location );
                            selectControl.SelectInitialize ();
                            SelectTweetID = selt.id;
                            isFirstClickDown = true;
                            if ( this.UseConversationBack )
                            {
                                StartTweetShowPosition -= ConversationNum;
                                if ( StartTweetShowPosition < 0 )
                                    StartTweetShowPosition = 0;
                            }

                            if ( OnChangeTweet != null )
                                OnChangeTweet ( selt );
                        }
                    }
                }

                if ( e.Button == MouseButtons.Left )
                {
                    selectControl.SelectInitialize ();
                    if ( ( sel = tweetDraw.IsCursorInTweet ( e.Location ) ) >= 0 )
                    {
                        //  選択時に文字の上にあったら、選択を開始する
                        if ( tweets.Count - sel <= this.StartTweetShowPosition - 1 )
                        {
                            this.StartTweetShowPosition = tweets.Count - sel - 1;
                        }
                        var tmpSel = this.StartTweetShowPosition + sel;
                        if ( tweets.Count <= tmpSel )
                            tmpSel = tweets.Count - 1;
                        selectControl.SelectStart ( e.Location, tweets[tmpSel].id );
                    }

                    if ( tweet_id != null )
                        this.anicon.SetRedrawQueue ();
                    return;

                }
                this.anicon.SetRedrawQueue ();
            }
        }

        /// <summary>
        /// マウスが放されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimelineControl_MouseUp ( object sender, MouseEventArgs e )
        {
            if ( tweets.Count == 0 )
                return;

            //  左クリック
            if ( e.Button == MouseButtons.Left )
            {
                if ( selectControl.isMouseDown )
                {
                    selectControl.SelectEnd ( e.Location );
                }

                if ( !selectControl.isSelecting )
                {
                    if ( !isFirstClickDown && !selTweetContextMenu.isClosed && !selTextContextMenu.isClosed )
                    {
                        this.OpenConversation = !this.OpenConversation;
                        if ( Setting.Timeline.isLineMode && ( !this.OpenConversation || !this.UseConversationBack ) )
                        {
                            this.SelectTweetID = -1;
                        }
                        this.anicon.SetRedrawQueue ();
                    }

                    //  リンクとか、そういうクリック判定
                    ClickCellsData clickData = clickCells.getClickLink ( e.Location );
                    if ( clickData != null && !isFirstClickDown && !selTweetContextMenu.isClosed && !selTextContextMenu.isClosed )
                    {
                        ActionControl.DoAction ( ActionControl.ConvertType ( clickData.type ), clickData.source, 
                            selUserContextMenu, ReplySelectedTweet, 
                                (SearchTweetDelegate)delegate (decimal id) {
                                    return tweets.Find ( (t) => t.DynamicNotifyTweet.id == id );
                                }, null, this.OnNotifyClicked );
                    }
                    selTweetContextMenu.isClosed = false;
                    selTextContextMenu.isClosed = false;

                }
            }

            //  右クリック
            if ( e.Button == MouseButtons.Right )
            {
                if ( selectControl.isSelecting )
                {
                    selTextContextMenu.ShowMenu ( this.PointToScreen ( e.Location ) );
                }
                else
                {
                    if ( OnRequiredAccountInfo == null )
                        throw new Exception ( "OnRequiredAccountInfoが定義されていません" );
                    var acinfo = OnRequiredAccountInfo.Invoke ();
                    var tweet = this.tweets.Find ( ( t ) => t.id == SelectTweetID );
                    if ( tweet != null )
                        selTweetContextMenu.ShowMenu ( this.PointToScreen ( e.Location ), acinfo, tweet );
                }
            }

            this.isFirstClickDown = false;
        }

        /// <summary>
        /// コントロール上でマウスが動いたらくる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimelineControl_MouseMove ( object sender, MouseEventArgs e )
        {
            if ( selectControl.isMouseDown )
            {
                selectControl.SelectNow ( e.Location );
                this.anicon.SetRedrawQueue ();
            } else {
                TimelineCellsTweetID tweet_id;
                this.HoverLocation = e.Location;
                if ( ( tweet_id = tweetDraw.IsCursorInCellTweetID ( e.Location ) ) != null )
                {
                    //  実際のツイートと比較
                    var selt = tweets.Find ( p => p.id == tweet_id.id );
                    if ( selt != null && HoverTweetID != selt.id )
                    {
                        HoverTweetID = selt.id;
						if (Setting.Timeline.isHoverSelectMode && Setting.Timeline.isLineMode && this.OpenConversation)
						{
							SelectTweetID = HoverTweetID;
						}

						this.anicon.SetRedrawQueue();
                    }
                }
                else
                {
                    //  ボタン消去
                    if ( HoverTweetID != -1 )
                    {
                        HoverTweetID = -1;
                        this.anicon.SetRedrawQueue ();
                    }
                }
                //  カーソル移動中、リンクがあったらカーソルを変更する
                if ( ( clickCells.getClickLink ( e.Location ) != null ) )
                {
					if (this.Cursor != Cursors.Hand)
						this.anicon.SetRedrawQueue();
                    this.Cursor = Cursors.Hand;
                }
                else
                {
                    if ( this.Cursor != Cursors.Default )
                    {
                        this.Cursor = Cursors.Default;
						this.anicon.SetRedrawQueue();
                    }
                }
            }
        }

        /// <summary>
        /// ダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimelineControl_MouseDoubleClick ( object sender, MouseEventArgs e )
        {
            int sel = 0;

            if ( Setting.ShortcutKeys.Shortcuts != null )
            {
                Actions action = Setting.ShortcutKeys.Shortcuts.DoubleClicked ();
                if ( action != Actions.None )
                {
                    var tweet = tweets.Find ( ( t ) => t.DynamicTweet.id == SelectTweetID );
                    if ( tweet != null )
                    {
                        ActionControl.OnShortcutAction ( action, SelectTweetID, tweet.DynamicTweet.user.screen_name,
                            selUserContextMenu, ReplySelectedTweet,
                            (SearchTweetDelegate)delegate ( decimal id )
                        {
                            return tweets.Find ( ( t ) => t.DynamicNotifyTweet.id == id );
                        } );
                    }
                }
            }

            if ( e.Button == MouseButtons.Left && ( sel = tweetDraw.IsCursorInTweet ( e.Location ) ) >= 0 )
            {
                selectControl.SelectAll ( tweets[sel].DynamicTweet.text, tweets[sel].DynamicTweet.id );
                this.anicon.SetRedrawQueue ();
                return;
                //this.RedrawControl ();
            }

        }

        /// <summary>
        /// ウィンドウを再描画します
        /// falseが返されたときは、現在描画ができない状態なので、再度試してください
        /// </summary>
        private bool RedrawControl ()
        {
            this.ChangingControl = true;
            if ( this.InvokeRequired )
            {
                if ( !this.IsDisposed )
                {
                    this.Invoke ( (MethodInvoker)delegate ()
                    {
                        if ( !this.IsDisposed )
                        {
                            this.Invalidate ();
                            this.Update ();
                        }

                    } );
                }
            }
            else
            {
                if ( !this.IsDisposed )
                {
                    this.Invalidate ();
                    this.Update ();
                }
            }
            this.ChangingControl = false;
            return true;
        }

        public void TimelineControl_KeyDown ( object sender, KeyEventArgs e )
        {
            if ( e.Control && e.KeyCode == Keys.C )
            {
                //
                if ( selectControl.selText != null )
                    Clipboard.SetDataObject ( selectControl.selText, true );
            }

            if ( e.Control && e.KeyCode == Keys.A )
            {
                if ( SelectTweetID >= 0 )
                {
                    var t = ( tweets.Find ( ( twit ) => twit.id == SelectTweetID ) );
                    if ( t != null )
                    {
                        selectControl.SelectAll ( t.DynamicTweet.text, SelectTweetID );
                        this.anicon.SetRedrawQueue ();
                    }
                }
            }

            if ( Setting.ShortcutKeys.Shortcuts != null )
            {
                Actions action = Setting.ShortcutKeys.Shortcuts.KeyDown ( e.Control, e.Shift, e.KeyCode );
                if ( action != Actions.None )
                {
                    var tweet = tweets.Find ( ( t ) => t.id == SelectTweetID );
                    if ( tweet != null )
                    {
                        ActionControl.OnShortcutAction ( action, tweet.DynamicTweet.id, tweet.DynamicTweet.user.screen_name,
                                                            selUserContextMenu, ReplySelectedTweet,
                                (SearchTweetDelegate)delegate ( decimal id )
                        {
                            return tweets.Find ( ( t ) => t.DynamicNotifyTweet.id == id );
                        } );
                    }

                }
            }
        }

        private void TimelineControl_Enter ( object sender, EventArgs e )
        {
            this.ActiveControl = this.Controls[0];
            this.Focus ();
        }

        /// <summary>
        /// コントロールを撮影
        /// </summary>
        /// <returns></returns>
        public Bitmap CaptureControl ()
        {
            Bitmap bmp = new Bitmap ( this.Width, this.Height );
            this.DrawToBitmap ( bmp, this.ClientRectangle );
            return bmp;
        }
    }
}
