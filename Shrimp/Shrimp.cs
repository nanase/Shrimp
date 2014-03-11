using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using OAuth;
using Shrimp.Account;
using Shrimp.ControlParts;
using Shrimp.ControlParts.AuthPanel;
using Shrimp.ControlParts.Popup;
using Shrimp.ControlParts.Tabs;
using Shrimp.ControlParts.Timeline;
using Shrimp.ControlParts.TweetBox;
using Shrimp.ControlParts.User;
using Shrimp.Log;
using Shrimp.Module;
using Shrimp.Module.Forms;
using Shrimp.Module.FormUtil;
using Shrimp.Module.ImageUtil;
using Shrimp.Module.Parts.ShrimpStatusBar;
using Shrimp.Plugin;
using Shrimp.Plugin.Ref;
using Shrimp.Setting;
using Shrimp.Setting.Forms;
using Shrimp.SQL;
using Shrimp.Twitter;
using Shrimp.Twitter.REST;
using Shrimp.Twitter.REST.DirectMessage;
using Shrimp.Twitter.REST.Help;
using Shrimp.Twitter.REST.List;
using Shrimp.Twitter.REST.Search;
using Shrimp.Twitter.REST.Timelines;
using Shrimp.Twitter.REST.Tweets;
using Shrimp.Twitter.REST.Users;
using Shrimp.Twitter.Status;
using Shrimp.Twitter.Status.StatusChecker;
using Shrimp.Twitter.Streaming;
using Shrimp.Update;
using Shrimp.Win32API;

namespace Shrimp
{
    public partial class Shrimp : Form
    {
        ShrimpTabs TimelineTabControl = new ShrimpTabs();
        public AccountManager accountManager = new AccountManager();
        TweetBoxControl tweetBox = new TweetBoxControl();
        UserInformation userInfo = new UserInformation();
        UserStatusControl userControl;
        UserStreaming us;
        Plugins plugins;
        AboutUsers userAPI = new AboutUsers();
        help HelpAPI = new help();
        DirectMessages directMessage = new DirectMessages();
        Timelines timelines = new Timelines();
        Statuses statuses = new Statuses();
        lists listAPI = new lists();
        search searchAPI = new search();
        ShrimpSpeed shrimpSpeed = null;
        DBControl db;
        private List<TwitterStatus> queueStatuses = new List<TwitterStatus>();
        private int queueStatusesCount = 0;
        private decimal ReceivedStatuses = 0;
        private object queueLockObject = new object();
        private object queueLockObject2 = new object();
        /// <summary>
        /// ローカルタイマー
        /// </summary>
        private System.Windows.Forms.Timer iconDownloadTimer = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer crollingTweetTimer = new System.Windows.Forms.Timer();

        private decimal crollingCounter = 0;
        private int boxHeight = 0;
        private listDataCollection tmplistDatas = new listDataCollection();
        private SendingTweet prevTweet = new SendingTweet();
        private bool isNowFlashing = false;
        private bool isDisposedShrimp = false;
        private bool BootingUpdater = false;
        private int tweetBoxHeight = 0;

        #region デリゲート
        public delegate void CallPlugin(string function, object[] args);
        public delegate void OnCreatedTweetDelegate(OnCreatedTweetHook hook);
        public delegate void OnChangedTabControlAlignment(TabAlignment align);
        public delegate void OnDeletingUserInformationDelegate(int selNum);
        public delegate bool OnAddingUserInformationDelegate(TwitterInfo t);
        public delegate void OnCreatingUpdateFormDelegate(string log);
        public delegate void OnUserStatusControlAPIDelegate(TimelineControl sender, ActionType type, string screen_name);
        #endregion

        public Shrimp()
        {
            InitializeComponent();

            ServicePointManager.DefaultConnectionLimit = 100;
            //  デザイナーがエラーおこしやがるからこっちで書くわ
            //t = new TwitterInfo("AZZEt8SJg2CY60h3iB4kaw", "JXqD9GxFrJx6QBz0BV8jkdWfoAGH184Jj1iFJOKRWBU", "107318695-P3aZHOhV1zMmbGFOExs33ffCtP7MHHRfyZWQKNwG", "4i3j8DBLdgWIKMYBV6lFilbk6S59folMnPB9L8NkKnsSb");
            ActionControl.initialize(this.userControl_TabControlUserInformationHandler, this.TimelineObject_OnUseTwitterAPI,
                this.timeline_OnRequiredAccountInfo);
            this.userControl = new UserStatusControl(null, userControl_TabControlUserInformationHandler, TimelineObject_OnUseTwitterAPI, OnUserStatusControlAPI);
            this.userControl.Dock = DockStyle.Fill;
            this.userControl.SetHandlers(new TimelineControl.OnChangedTweetHandler(timeline_OnChangeTweet),
                new TimelineControl.OnUseTwitterAPIDelegate(TimelineObject_OnUseTwitterAPI),
                new TimelineControl.OnChangedTweetDelayPercentageHandler(timeline_OnChangedTweetDelayPercentage),
                new TimelineControl.OnRequiredAccountInfoDeleagate(timeline_OnRequiredAccountInfo),
                new TimelineControl.OnCreatedReplyDataDelegate(timeline_OnCreatedReplyData),
                new TimelineControl.TabControlOperationgDelegate(userControl_TabControlUserInformationHandler),
                new TimelineControl.OnRequiredShrimpData(timeline_OnRequiredShrimpData),
                new TimelineControl.OnReloadShrimp(timeline_OnReloadShrimp),
                new OnCreatedTweetDelegate(onCreatedTweet),
                new TabControls.FlashWindowDelegate(FlashWindow),
                new OnUserStatusControlAPIDelegate(OnUserStatusControlAPI));
            this.TimelineSplit.Panel2.Controls.Add(userControl);
            this.TimelineTabControl.Dock = DockStyle.Fill;
            this.TimelineTabControl.SetHandlers(new TimelineControl.OnChangedTweetHandler(timeline_OnChangeTweet),
                new TimelineControl.OnUseTwitterAPIDelegate(TimelineObject_OnUseTwitterAPI),
                new TimelineControl.OnChangedTweetDelayPercentageHandler(timeline_OnChangedTweetDelayPercentage),
                new TimelineControl.OnRequiredAccountInfoDeleagate(timeline_OnRequiredAccountInfo),
                new TimelineControl.OnCreatedReplyDataDelegate(timeline_OnCreatedReplyData),
                new TimelineControl.TabControlOperationgDelegate(userControl_TabControlUserInformationHandler),
                new TimelineControl.OnRequiredShrimpData(timeline_OnRequiredShrimpData),
                new TimelineControl.OnReloadShrimp(timeline_OnReloadShrimp),
                new OnCreatedTweetDelegate(onCreatedTweet),
                new TabControls.FlashWindowDelegate(FlashWindow),
                new OnUserStatusControlAPIDelegate(OnUserStatusControlAPI));

            this.TimelineSplit.Panel1.Controls.Add(this.TimelineTabControl);
            this.settingButton.ItemClicked += new StatusPopup.ItemClickedDelegate(settingButton_ItemClicked);
            tweetBox.Dock = DockStyle.Fill;
            tweetBox.SendButtonClicked += new EventHandler(SendButtonClicked);
            tweetBox.DeleteTweetClicked += new EventHandler(tweetBox_DeleteTweetClicked);
            tweetBox.ShrimpBeamClicked += new EventHandler(tweetBox_ShrimpBeamClicked);
            tweetBox.AccountImageDoubleClicked += new EventHandler(tweetBox_AccountImageDoubleClicked);

            //this.MainSplit.Panel1.Controls.Clear ();
            //this.MainSplit.Panel2.Controls.Add ( this.TimelineSplit );
            this.MainSplit.Panel2.Controls.Add(tweetBox);
            this.MainSplit.Panel2MinSize = tweetBox.Height;
            this.boxHeight = tweetBox.Height;
            this.tweetBox.EnableControls = false;
            this.userControl.isLoadingFinished = true;
        }

        /// <summary>
        /// ユーザー情報画面の表示
        /// </summary>
        /// <param name="value"></param>
        public void ShowUserInformation(bool value)
        {
            this.TimelineSplit.Panel2Collapsed = !value;
        }

        /// <summary>
        /// ユーザー切り替え
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tweetBox_AccountImageDoubleClicked(object sender, EventArgs e)
        {
            accountManager.NextAccount();
        }

        private void onCreatedTweet(OnCreatedTweetHook hook)
        {
            //  プラグイン呼び出し
            if (this.plugins == null)
                return;
            this.plugins.OnCreatedTweet(hook);
            if (hook.isModified)
                hook.status.DynamicTweet.entities = new Twitter.Entities.TwitterEntities(hook.status.DynamicTweet.text);
        }

        private void OnTabAligneChanged(TabAlignment alignment)
        {
            this.TimelineTabControl.Alignment = alignment;
        }

        /// <summary>
        /// タブを開くハンドラ(ユーザー情報から飛んできた場合)
        /// </summary>
        /// <param name="type"></param>
        /// <param name="detail"></param>
        TabControls userControl_TabControlUserInformationHandler(ActionType type, object detail, string tabID = "", bool isBootingUp = false)
        {
            if (accountManager.SelectedAccount == null)
                return null;

            if (type == ActionType.UserTimeline)
            {
                this.ChangeAnyDatasStart();
                detail = ((string)detail).TrimStart('@');
                var tab = TimelineTabControl.NewTab(true, false, "@" + detail + "のタイムライン",
                    new TimelineCategory(TimelineCategories.UserTimeline, detail, false),
                    (TabControls.OnReloadDelegate)delegate(TabControls tabData)
                {
                    timelines.UserTimeline(accountManager.SelectedAccount,
                        (Twitter.REST.TwitterWorker.TwitterCompletedProcessDelegate)delegate(object data)
                        {
                            List<TwitterStatus> tmp = (List<TwitterStatus>)data;
                            TimelineTabControl.InsertTweetRange(tabData, tmp);

                            if (db != null)
                            {
                                db.InsertTweetRange(tmp);
                                db.InsertUserRange(tmp.ConvertAll((t) => t.user));
                            }
                            tabData.SetFinishedLoading();
                            tabData.SetFirstView(true);
                        }, (Twitter.REST.TwitterWorker.TwitterErrorProcessDelegate)delegate(TwitterCompletedEventArgs data)
                        {
                            tabData.SetFinishedLoading();
                            MessageBox.Show("@" + detail + "のタイムラインを取得することができませんでした", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            tabData.Invoke((MethodInvoker)delegate()
                            {
                                TimelineTabControl.DeleteTab(tabData);
                            });
                        }, (string)detail, 0);
                }, tabID);
                this.ChangeAnyDatasEnd();
                if (Setting.Timeline.SelectTabWhenCreatedTab && !isBootingUp)
                    TimelineTabControl.SelectedTab = tab;
                return tab;
            }
            if (type == ActionType.Search)
            {
                this.ChangeAnyDatasStart();
                var tab = TimelineTabControl.NewTab(true, false, "\"" + detail + "\"の検索結果",
                    new TimelineCategory(TimelineCategories.SearchTimeline, detail, false),
                    (TabControls.OnReloadDelegate)delegate(TabControls tabData)
                {
                    searchAPI.searchTweet(accountManager.SelectedAccount,
                    (Twitter.REST.TwitterWorker.TwitterCompletedProcessDelegate)delegate(object data)
                    {
                        List<TwitterStatus> tmp = (List<TwitterStatus>)data;
                        TimelineTabControl.InsertTweetRange(tabData, tmp);

                        if (db != null)
                        {
                            db.InsertTweetRange(tmp);
                            db.InsertUserRange(tmp.ConvertAll((t) => t.user));
                        }
                        tabData.SetFinishedLoading();
                        tabData.SetFirstView(true);
                    }, (Twitter.REST.TwitterWorker.TwitterErrorProcessDelegate)delegate(TwitterCompletedEventArgs data)
                    {
                        tabData.SetFinishedLoading();
                    }, (string)detail);
                }, tabID);

                this.ChangeAnyDatasEnd();
                if (Setting.Timeline.SelectTabWhenCreatedTab && !isBootingUp)
                    TimelineTabControl.SelectedTab = tab;
                return tab;
            }
            if (type == ActionType.UserFavoriteTimeline)
            {
                this.ChangeAnyDatasStart();
                detail = ((string)detail).TrimStart('@');
                var tab = TimelineTabControl.NewTab(true, false, "@" + detail + "のお気に入り",
                    new TimelineCategory(TimelineCategories.UserFavoriteTimeline, detail, false),
                    (TabControls.OnReloadDelegate)delegate(TabControls tabData)
                    {
                        timelines.FavoriteTimeline(accountManager.SelectedAccount,
                         (Twitter.REST.TwitterWorker.TwitterCompletedProcessDelegate)delegate(object data)
                         {
                             List<TwitterStatus> tmp = (List<TwitterStatus>)data;
                             TimelineTabControl.InsertTweetRange(tabData, tmp);

                             if (db != null)
                             {
                                 db.InsertTweetRange(tmp);
                                 db.InsertUserRange(tmp.ConvertAll((t) => t.user));
                             }
                             tabData.SetFinishedLoading();
                             tabData.SetFirstView(true);
                         }, (Twitter.REST.TwitterWorker.TwitterErrorProcessDelegate)delegate(TwitterCompletedEventArgs data)
                         {
                             tabData.SetFinishedLoading();
                             MessageBox.Show("@" + detail + "のお気に入りを取得することができませんでした", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                             tabData.Invoke((MethodInvoker)delegate()
                             {
                                 TimelineTabControl.DeleteTab(tabData);
                             });
                         }, (string)detail, 0);
                    }, tabID);

                this.ChangeAnyDatasEnd();
                if (Setting.Timeline.SelectTabWhenCreatedTab && !isBootingUp)
                    TimelineTabControl.SelectedTab = tab;
                return tab;
            }
            if (type == ActionType.Mention)
            {
                this.ChangeAnyDatasStart();
                detail = ((string)detail).TrimStart('@');
                var tab = TimelineTabControl.NewTab(true, false, "@" + detail + "の情報",
                    new TimelineCategory(TimelineCategories.UserInformation, detail, false),
                    (TabControls.OnReloadDelegate)delegate(TabControls tabData)
                    {
                        userAPI.UserShow(accountManager.SelectedAccount,
                        (Twitter.REST.TwitterWorker.TwitterCompletedProcessDelegate)delegate(object data)
                        {
                            var user = data as TwitterUserStatus;
                            tabData.ChangeUserStatus(user);
                        }, (Twitter.REST.TwitterWorker.TwitterErrorProcessDelegate)delegate(TwitterCompletedEventArgs data)
                        {
                            tabData.SetFinishedLoading();
                            MessageBox.Show("@" + detail + "は削除されているか、凍結されています", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            tabData.Invoke((MethodInvoker)delegate()
                            {
                                TimelineTabControl.DeleteTab(tabData);
                            });
                        }, (string)detail, 0);
                    }, tabID);
                this.ChangeAnyDatasEnd();
                if (Setting.Timeline.SelectTabWhenCreatedTab && !isBootingUp)
                    TimelineTabControl.SelectedTab = tab;
                return tab;
                //tab.Controls.Add ( userc );
            }
            if (type == ActionType.RegistBookMark)
            {
                TimelineTabControl.InsertTweet(null, (TwitterStatus)detail, TimelineCategories.BookmarkTimeline);
            }
            if (type == ActionType.URL || type == ActionType.Media)
            {
                Process.Start((string)detail);
            }
            if (type == ActionType.Focus)
            {
                this.tweetBox.SelectTweetBox();
            }
            return null;
        }

        /// <summary>
        /// コントロールから、フォームへのデータアクセスに用いる
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private object timeline_OnRequiredShrimpData(string type)
        {
            if (type == "listDatas")
                return this.tmplistDatas;
            if (type == "sql")
                return this.db;
            return null;
        }

        private void timeline_OnReloadShrimp()
        {
            this.crollingCounter = 0;
            this.crollingTweetTimer.Stop();
            this.crollingTweetTimer.Start();
        }

        public new void Dispose()
        {
            //base.Dispose ();
            if (db != null)
                db.Dispose(); db = null;
            if (this.plugins != null)
                this.plugins.Dispose();
            userAPI = null;
            timelines = null;
            TimelineTabControl.Dispose();
            TimelineTabControl = null;
            base.Dispose();
        }

        private void Shrimp_Load(object sender, EventArgs e)
        {
            Setting.Timeline.initialize();
            Setting.BombDetect.initialize();
            Setting.Colors.initialize();
            Setting.Fonts.initialize();
            Setting.CrollingTimeline.initialize();
            Setting.TabColors.initialize();
            Setting.FormSetting.initialize();
            Setting.ShortcutKeys.initialize();
            Setting.UserStream.initialize();
            Setting.Update.initialize();
            Setting.BackgroundImage.initialize();

            //	設定読み込み処理
            SettingSerializer.Load();
            if (Setting.FormSetting.WindowState == FormWindowState.Minimized)
            {
                this.Width = 640;
                this.Height = 480;
            }
            if (Setting.FormSetting.Bounds.Width == 0 || Setting.FormSetting.Bounds.Height == 0)
            {
                this.Width = 640;
                this.Height = 480;
                this.SetBounds((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2,
                (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2, this.Width,
                this.Height);
            }
            else
            {
                this.Bounds = Setting.FormSetting.Bounds;
            }
            //this.WindowState = Setting.FormSetting.WindowState;
            this.TimelineSplit.SplitterDistance = Setting.FormSetting.TimelineSplitterDistance;
            OnTabAligneChanged(Setting.Timeline.ShrimpTabAlignment);

            //デスクトップのワークエリアとウィンドウの矩形が重なっていなかったら位置を戻す

            if (System.Windows.Forms.Screen.GetWorkingArea(this).IntersectsWith(this.Bounds) == false)
            {
                this.SetBounds((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2,
                                (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2, this.Width,
                                this.Height);
            }

            //  プラグイン読み込み
            try
            {
                this.plugins = new Plugins();
            }
            catch (Exception)
            {
                //  DLLがないかもな
                MessageBox.Show("お使いのコンピュータには、Visual Studio 2012の再頒布可能パッケージがインストールされていない可能性があります。プログラムをインストールしてから、もう一度お試しください。");
                Process.Start("http://www.microsoft.com/ja-jp/download/details.aspx?id=30679");
                System.Environment.Exit(0);
            }
            this.plugins.LoadPlugins();
            this.tweetBox.AddRangeRegistTweetBoxMenuHook(this.plugins.OnRegistTweetBoxMenuHook());


            //  データベースのオープン
            db = new DBControl(ShrimpSettings.DatabasePath);
            db.CreateTable(TwitterStatus.sqlCreate);
            db.CreateTable(TwitterUserStatus.sqlCreate);
            db.CreateTable(TwitterDirectMessageStatus.DBsqlCreate);

            TimelineTabControl.SetDB();
            //  設定読み込み
            if (!this.LoadAccount())
            {
                var accountRegist = new AccountRegister("AZZEt8SJg2CY60h3iB4kaw", "JXqD9GxFrJx6QBz0BV8jkdWfoAGH184Jj1iFJOKRWBU");
                accountRegist.StartPosition = FormStartPosition.CenterScreen;
                accountRegist.ShowDialog();
                if (accountRegist.Tag != null)
                {
                    this.accountManager.AddNewAccount((TwitterInfo)accountRegist.Tag);
                    this.SaveAccount();
                }
                else
                {
                    if (this.accountManager.accounts.Count == 0)
                        System.Environment.Exit(0);
                }
            }

            if (!this.TimelineTabControl.LoadTabs(userControl_TabControlUserInformationHandler))
            {
                //  デフォルトタブを作成する
                TabControls new_tab = TimelineTabControl.NewTab(true, true, "ホームタイムライン",
                    new TimelineCategory(TimelineCategories.HomeTimeline, null, true));
                new_tab.tabDelivery.AddDelivery(new TabDelivery(new TimelineCategory(TimelineCategories.NotifyTimeline, null, true), null));
                new_tab.SetFinishedLoading();

                TimelineTabControl.NewTab(true, true, "返信",
                    new TimelineCategory(TimelineCategories.MentionTimeline, null, true)).SetFinishedLoading();
                TimelineTabControl.NewTab(true, true, "ダイレクトメッセージ",
                    new TimelineCategory(TimelineCategories.DirectMessageTimeline, null, true)).SetFinishedLoading();
                TimelineTabControl.NewTab(true, true, "通知",
                    new TimelineCategory(TimelineCategories.NotifyTimeline, null, true)).SetFinishedLoading();
                TimelineTabControl.NewTab(true, true, "ブックマーク",
                    new TimelineCategory(TimelineCategories.BookmarkTimeline, null, true)).SetFinishedLoading();
            }
            if (this.TimelineTabControl.TabCount != 0)
                TimelineTabControl.SelectTab(0);

            LogViewer v = new LogViewer();
            //v.Show ();

            //  イベントハンドラーの設定
            timelines.loadCompletedEvent += new TwitterWorker.loadCompletedEventHandler(timelines_loadCompletedEvent);
            statuses.loadCompletedEvent += new TwitterWorker.loadCompletedEventHandler(statuses_loadCompletedEvent);
            userAPI.loadCompletedEvent += new TwitterWorker.loadCompletedEventHandler(userAPI_loadCompletedEvent);

            //  ユーザーストリーム
            us = new UserStreaming();
            us.completedHandler += new UserStreaming.TweetEventDelegate(us_newTweetEvent);
            us.disconnectHandler += new UserStreaming.UserStreamingconnectStatusEventDelegate(us_userstream_status_event);
            us.notifyHandler += new UserStreaming.NotifyEventDelegate(us_notifyHandler);

            this.iconDownloadTimer.Interval = 500;
            this.iconDownloadTimer.Tick += new EventHandler(iconDownloadTimer_Tick);
            this.iconDownloadTimer.Start();

            this.crollingTweetTimer.Interval = 1000;
            this.crollingTweetTimer.Tick += new EventHandler(crollingTweetTimer_Tick);
            this.crollingTweetTimer.Start();

            this.shrimpSpeed = new ShrimpSpeed(this.timeline_OnChangedTweetDelayPercentage);

            //this.accountManager.AddNewAccount ( new TwitterInfo ( "hoFsyRdSsx6dmo2A8op1w", "LzCxIXP6twzvZNWpQLyAhn2HUnRQD3Oh7v6IOkmo", "2306345184-6qk3bN29tP4Pxujlm8iaY5Asdvmew2uYrIpKvD8", "vAD3IWZbkQNKDaAVt2zcMjy9uPgXEtT44qyFev0idx2ws" ) );

            this.ShowUserInformation(Setting.Timeline.isShowUserInformation);
            this.tweetBox.EnableControls = true;

            var task = Task.Factory.StartNew(() =>
            {
                HelpAPI.configuration(accountManager.SelectedAccount,
                    (Twitter.REST.TwitterWorker.TwitterCompletedProcessDelegate)delegate(object data)
                    {
                        ConfigStatus status = data as ConfigStatus;
                        this.tweetBox.tConfigStatus = (ConfigStatus)status.Clone();
                    }, null
                );
                { }
            });
            LoadUserInformation();
            if (Setting.UserStream.isEnableUserstream)
                this.ConnectUserStream();
            if (Setting.Update.isUpdateEnable && !Setting.Update.isIgnoreUpdate)
                CheckUpdate.CheckUpdateSync(OnCreatingUpdateForm);
        }

        private void OnCreatingUpdateForm(string log)
        {
            UpdateForm upd = new UpdateForm();
            upd.SetLog(log);
            if (upd.ShowDialog() == DialogResult.OK)
            {
                this.BootingUpdater = true;
                this.Close();
            }
            else if (upd.Tag != null && (string)upd.Tag == "Ignore")
            {
                //  今後のバージョンは一切無視
                Setting.Update.isIgnoreUpdate = true;
            }

        }

        /// <summary>
        /// タイムライン取得のタイマー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void crollingTweetTimer_Tick(object sender, EventArgs e)
        {
            foreach (TwitterInfo t in accountManager.accounts)
            {
                var twIn = t;
                TimelineTabControl.Crolling(twIn, timelines, directMessage, listAPI, crollingCounter);
                /*
                if ( crollingCounter % 60 == 0 )
                {
                    Task.Factory.StartNew ( () =>
                    {
                        timelines.UserTimeline ( t,
                         (Twitter.REST.TwitterWorker.TwitterCompletedProcessDelegate)delegate ( object data )
                         {
                             List<TwitterStatus> tmp = (List<TwitterStatus>)data;
                             t.SetUserTimeline ( tmp );
                         }, null, t.screen_name, 0 );
                    } );
                }
                */
            }
            crollingCounter++;
        }

        /// <summary>
        /// アイコンダウンロードする巡回用タイマー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void iconDownloadTimer_Tick(object sender, EventArgs e)
        {
            foreach (TwitterInfo tmp in this.accountManager.accounts)
            {
                if (tmp != null)
                {
                    if (tmp.IconUrl != null)
                    {
                        tmp.IconData = ImageCache.AutoCache(tmp.IconUrl, true);
                        if (tmp.IconData != null && accountManager.SelectedAccount == tmp)
                        {
                            this.tweetBox.SelectedIcon = tmp.IconData;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// データを変更するとき、衝突しかねない関数をストップさせます
        /// </summary>
        public void ChangeAnyDatasStart()
        {
            if (this.crollingTweetTimer.Enabled)
                this.crollingTweetTimer.Enabled = false;
            if (this.iconDownloadTimer.Enabled)
                this.iconDownloadTimer.Enabled = false;
        }

        /// <summary>
        ///	ChangeAnyDatasStart後、データを変更したらこれを必ず実行してください
        /// </summary>
        public void ChangeAnyDatasEnd()
        {
            if (!this.crollingTweetTimer.Enabled)
                this.crollingTweetTimer.Enabled = true;
            if (!this.iconDownloadTimer.Enabled)
                this.iconDownloadTimer.Enabled = true;
        }

        /// <summary>
        /// ユーザーが消去されるとき、呼び出してください
        /// </summary>
        /// <param name="user_id"></param>
        public void OnDeletingUserInformation(int selNum)
        {
            //ChangeAnyDatasStart();
            if (us != null)
            {
                TwitterInfo t = this.accountManager.accounts[selNum];
                us.stopStreaming(t, true);
            }
            this.accountManager.RemoveAccount(selNum);
            SaveAccount();
            //ChangeAnyDatasEnd();
        }

        /// <summary>
        /// ユーザーが作成されるとき、呼び出してください
        /// </summary>
        /// <param name="user_id"></param>
        public bool OnAddingUserInformation(TwitterInfo t)
        {
            bool isOK = false;
            //ChangeAnyDatasStart ();
            if (this.accountManager.AddNewAccount(t))
            {
                //ChangeAnyDatasEnd ();
                if (us != null && Setting.UserStream.isEnableUserstream)
                {
                    List<OAuthBase.QueryParameter> q = new List<OAuthBase.QueryParameter>();
                    if (Setting.UserStream.isRepliesAll)
                        q.Add(new OAuthBase.QueryParameter("replies", "all"));
                    if (Setting.UserStream.isIncludeFollowingsActivity)
                        q.Add(new OAuthBase.QueryParameter("include_followings_activity", "true"));
                    us.loadAsync(t, q);
                }
                LoadUserInformation();
                SaveAccount();
                isOK = true;
            }

            //ChangeAnyDatasEnd ();
            return isOK;
        }

        /// <summary>
        /// エビビームｗｗｗｗビビビビビ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tweetBox_ShrimpBeamClicked(object sender, EventArgs e)
        {
            var selected = accountManager.SelectedAccount;
            var time = DateTime.Now - selected.ShrimpBeamLatestDate;
            if ( time.TotalHours < 1.0 )
            {
                MessageBoxEX.ShowErrorMessageBox ( "大変申し訳ありませんが、エビビームは清楚故1時間に1回のみのご利用となります。ご了承ください。" );
                return;
            }
            else
            {
                selected.ShrimpBeam ++;
                statuses.Update ( selected, null, null, "エビビーム！ﾋﾞﾋﾞﾋﾞﾋﾞﾋﾞﾋﾞﾋﾞｗｗｗｗｗ ("+ selected.ShrimpBeam +"回目)", 0 );
            }
            selected.ShrimpBeamLatestDate = DateTime.Now;
        }

        /// <summary>
        /// ユーザー情報を取得する
        /// </summary>
        public void LoadUserInformation()
        {
            var task = Task.Factory.StartNew(() =>
            {
                foreach (TwitterInfo t in accountManager.accounts)
                {
                    var twIn = t;
                    userAPI.UserShow(twIn, (Twitter.REST.TwitterWorker.TwitterCompletedProcessDelegate)delegate(object data)
                    {
                        TwitterUserStatus user = (TwitterUserStatus)data;
                        ImageCache.AutoCache(user.profile_image_url, true);
                        twIn.IconUrl = user.profile_image_url;
                    }, null, null, t.UserId);
                    //  リスト取得
                    listAPI.list(twIn, (Twitter.REST.TwitterWorker.TwitterCompletedProcessDelegate)delegate(object data)
                    {
                        listDataCollection res_data = (listDataCollection)data;
                        if (res_data.Count == 0)
                            return;
                        this.tmplistDatas.AddlistRange(res_data);
                    }, null, null, t.UserId);
                }
            });

        }


        public bool isReloadUserInformation
        {
            get;
            set;
        }

        /// <summary>
        /// アカウント保存
        /// </summary>
        public void SaveAccount()
        {
            try
            {
                EncryptSerializer.Encrypt(ShrimpSettings.AccountPath, typeof(AccountManager), accountManager);
            }
            catch (Exception e)
            {
                MessageBoxEX.ShowErrorMessageBox("エラーが発生しました。アカウントの保存に失敗しました\n" + e.Message + "");
            }
        }

        /// <summary>
        /// ロードアカウント
        /// </summary>
        public bool LoadAccount()
        {
            try
            {
                if (File.Exists(ShrimpSettings.AccountPath))
                {
                    //シリアル化し、XMLファイルに保存する
                    this.accountManager = (AccountManager)EncryptSerializer.Decrypt(ShrimpSettings.AccountPath, typeof(AccountManager));
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                MessageBoxEX.ShowErrorMessageBox("エラーが発生しました。アカウントの読み込みに失敗しました\n" + e.Message + "");
                return false;
            }
            return true;
        }

        /// <summary>
        /// リプライもとの表示を行うかどうか
        /// 
        /// </summary>
        public bool ReplySourcePanel
        {
            get
            {
                return this.tweetBox.ReplySourcePanel;
            }
            set
            {
                if (this.tweetBox.ReplySourcePanel != value)
                {

                    if (!value)
                        tweetBox.ReplySourcePanel = value;

                    if (value)
                        this.tweetBoxHeight = tweetBox.BoxHeight;

                    if (this.MainSplit.InvokeRequired)
                    {
                        this.MainSplit.Invoke((MethodInvoker)delegate()
                        {
                            this.MainSplit.FixedPanel = FixedPanel.Panel2;
                            this.MainSplit.IsSplitterFixed = false;
                            if (value)
                            {
                                this.MainSplit.SplitterDistance = this.Height - 220;
                            }
                            else
                            {
                                this.MainSplit.SplitterDistance = this.Height - 120;
                            }

                        });
                    }
                    else
                    {
                        if (value)
                        {
                            this.MainSplit.SplitterDistance = this.Height - (220);
                        }
                        else
                        {
                            this.MainSplit.SplitterDistance = this.Height - (120);
                        }
                    }

                    //  閉じたり開けたり
                    if (value)
                        tweetBox.ReplySourcePanel = value;

                }
            }
        }

        /// <summary>
        /// ShrimpSpringLabelのテキスト
        /// </summary>
        public string ShrimpSpringLabelText
        {
            get { return this.shrimpSpringLabel.Text; }
            set
            {
                if (this.IsDisposed)
                    return;
                if (this.InvokeRequired && this.IsHandleCreated)
                {
                    this.Invoke((MethodInvoker)delegate()
                    {
                        if (this.IsDisposed)
                            return;
                        this.shrimpSpringLabel.Text = value;
                    });
                }
                else
                {
                    this.shrimpSpringLabel.Text = value;
                }
            }
        }

        /// <summary>
        /// Labelのテキスト
        /// </summary>
        public string StatusLabelText
        {
            get { return this.APIStatusLabel.Text; }
            set
            {
                if (this.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate()
                    {
                        this.APIStatusLabel.Text = value;
                    });
                }
                else
                {
                    this.APIStatusLabel.Text = value;
                }
            }
        }

        /// <summary>
        /// UserStreamの接続
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void settingButton_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "ConnectUserStreamNenu")
            {
                this.ConnectUserStream();
            }
            if (e.ClickedItem.Name == "OpenSettingMenu")
            {
                var SettingForm = new SettingForms(this.accountManager, false, this, OnTabAligneChanged, OnDeletingUserInformation, OnAddingUserInformation);
                SettingForm.ShowDialog();
            }
            if (e.ClickedItem.Name == "LineTweetModeMenu")
            {
                Setting.Timeline.isLineMode = !Setting.Timeline.isLineMode;
                this.TimelineTabControl.Refresh();
            }
            if (e.ClickedItem.Name == "UserInformationMenu")
            {
                Setting.Timeline.isShowUserInformation = !Setting.Timeline.isShowUserInformation;
                ShowUserInformation(Setting.Timeline.isShowUserInformation);
            }

            int i = 0;
            foreach (TwitterInfo t in this.accountManager.accounts)
            {
                if (e.ClickedItem.Name == "" + t.UserId + "")
                {
                    this.accountManager.selNum = i;
                }
                i++;
            }

        }

        /// <summary>
        /// UserStreamへ接続する
        /// </summary>
        private void ConnectUserStream()
        {
            if (this.us.isStartedStreaming)
            {
                Setting.UserStream.isEnableUserstream = false;
                this.ShrimpSpringLabelText = "UserStreamの接続を停止しています...";
                foreach (TwitterInfo t in this.accountManager.accounts)
                {
                    us.stopStreaming(t);
                }
            }
            else
            {
                Setting.UserStream.isEnableUserstream = true;
                this.shrimpSpringLabel.Text = "UserStreamの接続を開始しています...";
                List<OAuthBase.QueryParameter> q = new List<OAuthBase.QueryParameter>();
                if (Setting.UserStream.isRepliesAll)
                    q.Add(new OAuthBase.QueryParameter("replies", "all"));
                if (Setting.UserStream.isIncludeFollowingsActivity)
                    q.Add(new OAuthBase.QueryParameter("include_followings_activity", "true"));
                //q.Add(new OAuthBase.QueryParameter("track", "@"));
                foreach (TwitterInfo t in accountManager.accounts)
                {
                    us.loadAsync(t, q);
                }
            }
        }

        /// <summary>
        /// タイムライン系の取得が完了したら、振り分ける
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timelines_loadCompletedEvent(object sender, TwitterCompletedEventArgs e)
        {
            var apiI = APIIntroduction.retTwitterAPIIntro(e.raw_data.Uri.AbsoluteUri);
            if (e.error_code == HttpStatusCode.OK)
            {
                List<TwitterStatus> data = e.data as List<TwitterStatus>;
                List<TwitterUserStatus> users = new List<TwitterUserStatus>(data.Count);
                foreach (TwitterStatus tmpTweet in data)
                {
                    TwitterStatusChecker.SetIsReply(this.accountManager.accounts, tmpTweet);
                    users.Add(tmpTweet.user);
                }

                db.InsertTweetRange(data);
                db.InsertUserRange(users);
                this.ShrimpSpringLabelText = "@" + e.account_source.ScreenName + "の" + apiI + "しました(" + data.Count + "件)";
            }
            else
            {
                this.ShrimpSpringLabelText = "@" + e.account_source.ScreenName + "の" + apiI + "に失敗しました";
            }
        }

        /// <summary>
        /// 消去ボタンが押された
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tweetBox_DeleteTweetClicked(object sender, EventArgs e)
        {
            this.tweetBox.Tweet = "";
            this.tweetBox.DisableSourcePanel();
            this.ReplySourcePanel = false;
            this.prevTweet.isDirectMessage = false;
            this.prevTweet.in_reply_to_status_id = -1;
            tweetBox.ChangeButton(false);
            tweetBox.ResetImagePath();
        }


        /// <summary>
        /// Streamによる、ツイート受信がされたら
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void us_newTweetEvent(object sender, TwitterCompletedEventArgs e)
        {
            if (e != null && e.data != null)
            {
                var tmpTweet = (TwitterStatus)e.data;
                TwitterStatusChecker.SetIsReply(this.accountManager.accounts, tmpTweet);
                TwitterStatusChecker.SetIsRetweeted(this.accountManager.accounts, tmpTweet);

                //db.InsertTweet ( tmpTweet );
                ReceivedStatuses++;
                this.shrimpSpeed.IncreaseSpeedCount();

                db.InsertUser(tmpTweet.user);
                if (tmpTweet.NotifyStatus != null)
                {
                    this.TimelineTabControl.InsertTweet(e.account_source, tmpTweet, TimelineCategories.NotifyTimeline);
                }
                else
                {
                    this.TimelineTabControl.InsertTweet(e.account_source, tmpTweet, (tmpTweet.isDirectMessage ? TimelineCategories.DirectMessageTimeline : TimelineCategories.HomeTimeline));
                }
                if (tmpTweet.DynamicTweet.isReply)
                    this.TimelineTabControl.InsertTweet(e.account_source, tmpTweet, TimelineCategories.MentionTimeline);
                //  一括でいれる
                lock (this.queueLockObject)
                {
                    queueStatuses.Add(tmpTweet);
                    queueStatusesCount++;
                    if (queueStatusesCount >= 500)
                    {
                        db.InsertTweetRange(queueStatuses);
                        queueStatuses.Clear();
                        queueStatusesCount = 0;
                    }
                }
            }
        }

        /// <summary>
        /// Streamによるツイート通知
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void us_notifyHandler(object sender, TwitterCompletedEventArgs e)
        {
            if (e != null && e.data != null)
            {
                TwitterStatusChecker.SetNotify(this.accountManager.accounts, (TwitterNotifyStatus)e.data);
                this.TimelineTabControl.InsertTweet(e.account_source, new TwitterStatus((TwitterNotifyStatus)e.data), TimelineCategories.NotifyTimeline);
            }
        }

        /// <summary>
        /// ストリームの接続、切断の通知
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void us_userstream_status_event(object sender, TwitterCompletedEventArgs e)
        {
            if (e != null)
            {
                if (e.error_code == HttpStatusCode.OK)
                {
                    //  接続成功
                    StreamState.setUserStream = true;
                    this.ShrimpSpringLabelText = "UserStreamへ接続しました";
                }
                else if (e.error_code == HttpStatusCode.RequestTimeout)
                {
                    //  切断かな？
                    StreamState.setUserStream = false;
                    this.us.CheckStopped();
                    if (this.us.isStartedStreaming)
                    {
                        this.ShrimpSpringLabelText = "@" + e.account_source.ScreenName + "のUserStreamが停止されました";
                    }
                    else
                    {
                        this.ShrimpSpringLabelText = "すべてのアカウントでUserStreamが停止されました";
                    }
                }
                else if (e.error_code == HttpStatusCode.Continue)
                {
                    //  切断かな？
                    StreamState.setUserStream = false;
                    this.ShrimpSpringLabelText = "UserStreamが切断されました。再接続処理待ちです。";
                }
                else if (e.error_code == HttpStatusCode.Unused)
                {
                    //  接続初期化待ちだな？
                    StreamState.setUserStream = true;
                    this.ShrimpSpringLabelText = "UserStreamの初期化待ちです...";
                }
                this.StatusLabelText = StreamState.getStatusString();
            }
        }

        /// <summary>
        /// アカウント情報だけを返す
        /// </summary>
        /// <returns></returns>
        AccountManager timeline_OnRequiredAccountInfo()
        {
            return this.accountManager;
        }

        /// <summary>
        /// リプライとして渡されたツイート
        /// </summary>
        /// <param name="tweet"></param>
        void timeline_OnCreatedReplyData(TwitterStatus tweet, bool isDirectMessage)
        {
            if (this.prevTweet.in_reply_to_status_id < 0 || isDirectMessage != this.prevTweet.isDirectMessage)
            {
                this.ReplySourcePanel = true;
                this.prevTweet.isDirectMessage = isDirectMessage;
                tweetBox.ChangeButton(isDirectMessage);
                this.tweetBox.DrawReplySourcePanel(tweet.DynamicTweet);
                this.prevTweet.in_reply_to_status_id = (this.prevTweet.isDirectMessage ? tweet.user.id : tweet.DynamicTweet.id);
                this.tweetBox.SelectTweetBox();
                this.tweetBox.Tweet = "";
                if (!this.prevTweet.isDirectMessage)
                    this.tweetBox.Tweet = "@" + tweet.DynamicTweet.user.screen_name + " ";
                this.tweetBox.SelectTweetBoxStart();
            }
            else
            {
                this.tweetBox.SelectTweetBox();
                this.tweetBox.Tweet = "@" + tweet.DynamicTweet.user.screen_name + " " + this.tweetBox.Tweet + "";
                this.tweetBox.SelectTweetBoxStart();
            }
        }

        /// <summary>
        /// ツイートの遅延率の変更
        /// </summary>
        /// <param name="Percentage"></param>
        void timeline_OnChangedTweetDelayPercentage(int Percentage)
        {
            StreamState.setPercentage = Percentage;
            this.StatusLabelText = StreamState.getStatusString();
        }

        /// <summary>
        /// ツイートボタン送信
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SendButtonClicked(object sender, EventArgs e)
        {
            this.tweetBox.EnableControls = false;

            if (this.prevTweet.isDirectMessage)
            {
                this.prevTweet.status = (string)tweetBox.Tweet.Clone();
                directMessage.SendDirectMessage(accountManager.SelectedAccount,
                    (TwitterWorker.TwitterCompletedProcessDelegate)delegate(object data)
                {
                    this.tweetBox.EnableControls = true;
                    tweetBox_DeleteTweetClicked(null, null);
                }, (TwitterWorker.TwitterErrorProcessDelegate)delegate(TwitterCompletedEventArgs err)
                {
                    this.tweetBox.EnableControls = true;
                }, null, this.prevTweet.in_reply_to_status_id, tweetBox.Tweet);
            }
            else
            {
                this.prevTweet.status = (string)tweetBox.Tweet.Clone();
                if (String.IsNullOrEmpty(this.tweetBox.GetAttachImagePath))
                {
                    //  プラグイン処理
                    //  OnTweetSendingHook
                    var hook = new OnTweetSendingHook((string)tweetBox.Tweet.Clone(), this.prevTweet.in_reply_to_status_id);
                    this.plugins.OnTweetSendingHook(hook);
                    if (hook.isCancel)
                    {
                        this.tweetBox.EnableControls = true;
                        return;
                    }

                    statuses.Update(accountManager.SelectedAccount, (TwitterWorker.TwitterCompletedProcessDelegate)delegate(object data)
                    {
                        sendTweet();
                    }, null, hook.text, hook.in_reply_to_status_id);
                }
                else
                {
                    if (this.tweetBox.GetImageArrayByte != null)
                    {
                        //  プラグイン処理
                        //  OnTweetSendingHook
                        var hook = new OnTweetSendingHook((string)tweetBox.Tweet.Clone(), this.prevTweet.in_reply_to_status_id);
                        this.plugins.OnTweetSendingHook(hook);
                        if (hook.isCancel)
                        {
                            this.tweetBox.EnableControls = true;
                            return;
                        }
                        statuses.UpdateMedia(accountManager.SelectedAccount, (TwitterWorker.TwitterCompletedProcessDelegate)delegate(object data)
                        {
                            sendTweet();
                        }, null, this.tweetBox.GetImageArrayByte, Path.GetFileName(this.tweetBox.GetAttachImagePath), hook.text, hook.in_reply_to_status_id);
                    }
                    else
                    {
                        MessageBox.Show("添付された画像が存在しませんです・・・\nFilePath:" + this.tweetBox.GetAttachImagePath + "", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.tweetBox.EnableControls = true;
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// ツイートを送信したら、こっちを実行
        /// </summary>
        private void sendTweet()
        {
            this.tweetBox.EnableControls = true;
            tweetBox_DeleteTweetClicked(null, null);
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate()
                {
                    this.Focus();
                });
            }
            else
            {
                this.Focus();
            }
            this.tweetBox.SelectTweetBox();
        }

        /// <summary>
        /// ツイートが送信された
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void statuses_loadCompletedEvent(object sender, TwitterCompletedEventArgs e)
        {
            if (e.error_code == HttpStatusCode.OK)
            {
                if (e.raw_data.Uri.AbsoluteUri.IndexOf("statuses/update.json") != -1 ||
                    e.raw_data.Uri.AbsoluteUri.IndexOf("statuses/update_with_media.json") != -1)
                {
                    this.ShrimpSpringLabelText = "ツイートを送信しました";
                }
                else
                {
                    var apiI = APIIntroduction.retTwitterAPIIntro(e.raw_data.Uri.AbsoluteUri);
                    this.ShrimpSpringLabelText = "@" + e.account_source.ScreenName + "で" + apiI + "しました";
                }
            }
            else
            {
                if (e.raw_data.Uri.AbsoluteUri.IndexOf("statuses/update.json") != -1 || e.raw_data.Uri.AbsoluteUri.IndexOf("statuses/update_with_media.json") != -1)
                {
                    this.ShrimpSpringLabelText = "ツイートの送信に失敗しました";
                    this.tweetBox.EnableControls = true;
                }
                else
                {
                    var apiI = APIIntroduction.retTwitterAPIIntro(e.raw_data.Uri.AbsoluteUri);
                    this.ShrimpSpringLabelText = "@" + e.account_source.ScreenName + "で" + apiI + "に失敗しました";
                }
            }
        }

        /// <summary>
        /// ユーザ情報を取得したら飛んでくる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void userAPI_loadCompletedEvent(object sender, TwitterCompletedEventArgs e)
        {
            if (e.error_code == HttpStatusCode.OK)
            {
                var apiI = APIIntroduction.retTwitterAPIIntro(e.raw_data.Uri.AbsoluteUri);
                this.ShrimpSpringLabelText = "@" + e.account_source.ScreenName + "で" + apiI + "しました";
            }
            else
            {
                var apiI = APIIntroduction.retTwitterAPIIntro(e.raw_data.Uri.AbsoluteUri);
                this.ShrimpSpringLabelText = "@" + e.account_source.ScreenName + "で" + apiI + "に失敗しました";
            }
        }


        /// <summary>
        /// チェンジ
        /// </summary>
        /// <param name="tweet"></param>
        void timeline_OnChangeTweet(TwitterStatus tweet)
        {
            if (this.userInfo.InvokeRequired)
            {
                this.userInfo.Invoke((MethodInvoker)delegate()
                {
                    this.userControl.ChangeUserStatus(tweet.DynamicTweet.user);
                });
            }
            else
            {
                this.userControl.ChangeUserStatus(tweet.DynamicTweet.user);
            }
        }

        /// <summary>
        /// UserStatusControlのユーザータブにて使う
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="type"></param>
        /// <param name="screen_name"></param>
        void OnUserStatusControlAPI(TimelineControl sender, ActionType type, string screen_name)
        {
            if (type == ActionType.UserFavoriteTimeline)
            {
                timelines.FavoriteTimeline(this.accountManager.SelectedAccount,
                (Twitter.REST.TwitterWorker.TwitterCompletedProcessDelegate)delegate(object data)
                {
                    List<TwitterStatus> tmp = (List<TwitterStatus>)data;
                    sender.Invoke((MethodInvoker)delegate()
                    {
                        //sender.initialize ();
                        sender.InsertTimelineRange(tmp);
                    });
                }, null, screen_name, 0);
            }

            if (type == ActionType.UserTimeline)
            {
                timelines.UserTimeline(this.accountManager.SelectedAccount,
                (Twitter.REST.TwitterWorker.TwitterCompletedProcessDelegate)delegate(object data)
                {
                    List<TwitterStatus> tmp = (List<TwitterStatus>)data;
                    sender.Invoke((MethodInvoker)delegate()
                    {
                        //sender.initialize ();
                        sender.InsertTimelineRange(tmp);
                    });
                }, null, screen_name, 0);
            }

            if (type == ActionType.UserConversation)
            {
                searchAPI.searchTweet(this.accountManager.SelectedAccount,
                (Twitter.REST.TwitterWorker.TwitterCompletedProcessDelegate)delegate(object data)
                {
                    List<TwitterStatus> tmp = (List<TwitterStatus>)data;
                    sender.Invoke((MethodInvoker)delegate()
                    {
                        //sender.initialize ();
                        sender.InsertTimelineRange(tmp);
                    });
                }, null, "from:" + screen_name + " OR @" + screen_name + "");
            }
        }


        /// <summary>
        /// APIつかうっぽい(RTとかそういうの)
        /// </summary>
        /// <param name="arg"></param>
        void TimelineObject_OnUseTwitterAPI(object sender, object[] arg)
        {
            string category = (string)arg[0];
            TwitterInfo user = (arg[2] == null ? null : arg[2] as TwitterInfo);
            var tabdata = this.TimelineTabControl.SelectedTabControls;
            TwitterInfo useAccount = accountManager.SelectedAccount;
            //  アカウント選択で行う
            if (user != null)
            {
                useAccount = user;
            }
            if (category == "retweet")
            {
                statuses.Retweet(useAccount, null, null, (decimal)arg[1]);
            }
            else if (category == "fav")
            {
                statuses.Favorite(useAccount, null, null, (decimal)arg[1]);
            }
            else if (category == "unfav")
            {
                statuses.UnFavorite(useAccount, null, null, (decimal)arg[1]);
            }
            else if (category == "loadNewTweet")
            {
                var sender_1 = sender as TimelineControl;
                timelines.TweetShow(useAccount, (Twitter.REST.TwitterWorker.TwitterCompletedProcessDelegate)delegate(object data)
                {
                    TwitterStatus state = (TwitterStatus)data;
                    sender_1.InsertTimeline(state);
                    //tabdata.InsertTweet ( user );
                }, null, (decimal)arg[1]);
            }
            else if (category == "delete")
            {
                TwitterStatus t = arg[1] as TwitterStatus;
                if (t.user != null)
                {
                    var info = accountManager.accounts.Find((u) => u.UserId == t.user.id);
                    if (t.isDirectMessage)
                        directMessage.DestroyDirectMessage(info, null, null, t.id);
                    else
                        statuses.Delete(info, null, null, t.id);
                }
            }
            else if (category == "search")
            {
                var sender_1 = sender as TabControls;
                searchAPI.searchTweet(useAccount,
                (Twitter.REST.TwitterWorker.TwitterCompletedProcessDelegate)delegate(object data)
                {
                    List<TwitterStatus> tmp = (List<TwitterStatus>)data;
                    TimelineTabControl.InsertTweetRange(sender_1, tmp);
                }, null, (string)arg[1]);
            }
            else if (category == "follow")
            {
                if (MessageBox.Show("@" + arg[1] + "をフォローします。よろしいですか？", "確認", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;
                var sender_1 = sender as TabControls;
                userAPI.FollowUser(useAccount, null, null, (string)arg[1], 0);
            }
            else if (category == "block")
            {
                if (MessageBox.Show("@" + arg[1] + "をブロックします。よろしいですか？", "確認", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;
                var sender_1 = sender as TabControls;
                userAPI.BlockUser(useAccount, null, null, (string)arg[1], 0);
            }
            else if (category == "spam")
            {
                var sender_1 = sender as TabControls;
                if (MessageBox.Show("@" + arg[1] + "をスパム報告します。よろしいですか？", "確認", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;
                userAPI.ReportSpam(useAccount, null, null, (string)arg[1], 0);
            }
        }

        private void Shrimp_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.isDisposedShrimp)
            {
                this.Hide();
                if (this.us != null)
                {
                    foreach (TwitterInfo t in this.accountManager.accounts)
                    {
                        us.stopStreaming(t, true);
                    }
                }
                this.timelines.Dispose();
                this.timelines = null;
                ImageCache.StopCrawling();
                this.crollingTweetTimer.Stop();
                this.crollingTweetTimer = null;
                this.iconDownloadTimer.Stop();
                this.iconDownloadTimer = null;


                this.TimelineTabControl.SaveTabs();

                this.db.Close();
                this.SaveAccount();
                Setting.FormSetting.Bounds = this.Bounds;
                Setting.FormSetting.WindowState = this.WindowState;
                Setting.FormSetting.TimelineSplitterDistance = this.TimelineSplit.SplitterDistance;
                SettingSerializer.Save();
                this.isDisposedShrimp = true;
                if (this.BootingUpdater)
                {
                    if (File.Exists("ShrimpAutoUpdater.exe"))
                    {
                        Process.Start("ShrimpAutoUpdater.exe");
                    }
                }
                System.Environment.Exit ( 0 );
            }
        }

        private void settingButton_Click(object sender, EventArgs e)
        {
            ToolStripButtonPopup p = sender as ToolStripButtonPopup;
            int i = 0;
            p.ChangeUserStreamMenu(this.us.isStartedStreaming);

            foreach (TwitterInfo t in this.accountManager.accounts)
            {
                p.InsertAccountName(t, this.accountManager.selNum == i);
                i++;
            }
            p.Show(System.Windows.Forms.Cursor.Position);
        }

        /// <summary>
        /// メインでFlashWindow
        /// </summary>
        public void FlashWindow()
        {
            this.Invoke((MethodInvoker)delegate()
            {
                if (Form.ActiveForm != this && this.WindowState == FormWindowState.Minimized)
                {
                    User32.FlashWindow(this);
                    this.isNowFlashing = true;
                }
            });
        }

        private void Shrimp_Activated(object sender, EventArgs e)
        {
            if (isNowFlashing)
            {
                User32.FlashWindow(this, true);
                isNowFlashing = false;
            }
        }

        private void MainSplit_SplitterMoving(object sender, SplitterCancelEventArgs e)
        {
            SplitContainer obj = sender as SplitContainer;
            this.tweetBox.BoxHeight = (this.Height - obj.SplitterDistance) / 2;
        }
    }
}
