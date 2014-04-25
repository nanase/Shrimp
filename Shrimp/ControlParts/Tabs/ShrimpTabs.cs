using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Shrimp.ControlParts.ContextMenus.Tabs;
using Shrimp.ControlParts.Popup;
using Shrimp.ControlParts.TabSetting;
using Shrimp.ControlParts.Timeline;
using Shrimp.ControlParts.User;
using Shrimp.Module;
using Shrimp.Module.Queue;
using Shrimp.Plugin.Ref;
using Shrimp.Query;
using Shrimp.Setting;
using Shrimp.SQL;
using Shrimp.Twitter;
using Shrimp.Twitter.REST.DirectMessage;
using Shrimp.Twitter.REST.List;
using Shrimp.Twitter.REST.Timelines;
using Shrimp.Twitter.Status;

namespace Shrimp.ControlParts.Tabs
{
    /// <summary>
    /// タブコントロールのShrimp版
    /// </summary>
    class ShrimpTabs : ShrimpTabControl, IDisposable
    {
        /// <summary>
        /// タブを保持する変数
        /// </summary>
        private TabControlsCollection tabs = new TabControlsCollection();
        private TabControlContextMenu TabControlContextMenu;
        private bool isDisposeds = false;

        private TabQueue tabQueue = new TabQueue ();
        /// <summary>
        /// アカウント情報を取得するのに必要
        /// </summary>
        private TimelineControl.OnRequiredAccountInfoDeleagate OnRequiredAccountInfo;
        private TimelineControl.OnRequiredShrimpData OnRequiredShrimpData;
        private TimelineControl.OnChangedTweetHandler OnChangedTweetHandler;
        private TimelineControl.OnUseTwitterAPIDelegate OnUserTwitterAPI;
        private TimelineControl.OnChangedTweetDelayPercentageHandler OnChangedTweetDelay;
        private TimelineControl.OnCreatedReplyDataDelegate OnCreatedReplyData;
        private TimelineControl.TabControlOperationgDelegate TabControlOperationHandler;
        private TimelineControl.OnReloadShrimp OnReloadShrimp;
        private Shrimp.OnCreatedTweetDelegate OnCreatedTweet;
        private TabControls.FlashWindowDelegate OnFlashWindowDelegate;
        private Shrimp.OnUserStatusControlAPIDelegate OnUserStatusControlAPI;

        private DBControl db;
        private object lockTabs = new object();
        private string beforeSearchDetail = "";
        private int BeforeSelectedTabIndex = -1;
        private TabControls BeforeSelectedTab = null;
        private Bitmap BeforeControlBitmap = null;

        private Stack<int> tabChangedStack;
        /// <summary>
        /// グローバルミュート
        /// </summary>
        private QueryParser shrimpQueryParser = new QueryParser();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ShrimpTabs()
        {
            this.Multiline = Setting.Timeline.isTabMultiline;
            this.tabChangedStack = new Stack<int>();
            this.TabControlContextMenu = new TabControlContextMenu();
            this.ContextMenuStrip = this.TabControlContextMenu.ContextMenu;
            this.TabCloseButtonClick += new EventHandler(ShrimpTabs_TabCloseButtonClick);
            this.TabControlContextMenu.Opening += new System.ComponentModel.CancelEventHandler(ContextMenuStrip_Opening);
            this.ContextMenuStrip.ItemClicked += new ToolStripItemClickedEventHandler(ContextMenuStrip_ItemClicked);
            this.TabControlContextMenu.CustomControlMenu.ItemClicked += new ToolStripItemClickedEventHandler(ContextMenuStrip_ItemClicked);
            this.SelectedIndexChanged += new EventHandler(ShrimpTabs_SelectedIndexChanged);
        }

        /// <summary>
        /// DBをセットする
        /// </summary>
        public void SetDB()
        {
            if (OnRequiredShrimpData != null)
                db = (DBControl)OnRequiredShrimpData.Invoke("sql");
        }

        /// <summary>
        /// タブを保存する
        /// </summary>
        public void SaveTabs()
        {
            try
            {
                EncryptSerializer.Encrypt(ShrimpSettings.TabSettingPath, typeof(List<TabManager>), tabs.Save(db));
            }
            catch (Exception e)
            {
                MessageBox.Show("エラーが発生しました。タブの保存に失敗しました\n" + e.Message + "");
            }
        }

        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            int sel = this.MouseCursorPointToItem(this.PointToClient(new Point(drgevent.X, drgevent.Y)));
            if (sel < 0)
            {
                base.OnDragDrop(drgevent);
                return;
            }
            var destTabControls = this.tabs[sel];
            TabControls tabPage = (TabControls)(drgevent.Data.GetData(typeof(TabControls)));
            if (destTabControls.TabID == tabPage.TabID)
            {
                base.OnDragDrop(drgevent);
                return;
            }
            lock ( ( (ICollection)this.TabPages ).SyncRoot )
            {
                this.TabPages.Remove ( tabPage );
            }
            lock ( ( (ICollection)this.tabs ).SyncRoot )
            {
                this.tabs.Remove ( tabPage );
            }
            lock ( ( (ICollection)this.TabPages ).SyncRoot )
            {
                this.TabPages.Insert ( sel, tabPage );
            }
            lock ( ( (ICollection)this.tabs ).SyncRoot )
            {
                this.tabs.Insert ( sel, tabPage );
            }
            
            

            base.OnDragDrop(drgevent);
            this.SelectTab(sel);
            //this.TabPages.Insert ( index, tabPage );

        }

        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);
            if (this.SelectedTab != null)
            {
                var tab = this.SelectedTab as TabControls;
                tab.SetFirstView(false);
            }
        }

        /// <summary>
        /// タブを読み込む
        /// </summary>
        /// <param name="tabControlOperation"></param>
        /// <returns></returns>
        public bool LoadTabs(TimelineControl.TabControlOperationgDelegate tabControlOperation)
        {
            try
            {
                if (File.Exists(ShrimpSettings.TabSettingPath))
                {
                    //シリアル化し、XMLファイルに保存する
                    var sourceTabs = (List<TabManager>)EncryptSerializer.Decrypt(ShrimpSettings.TabSettingPath, typeof(List<TabManager>));
                    foreach (TabManager t in sourceTabs)
                    {
                        TabControls newTab = null;
                        if (t.TabDelivery.TopCategory == TimelineCategories.UserInformation)
                        {
                            newTab = tabControlOperation.Invoke(ActionType.Mention, t.TabDelivery.deliveries[0].Category.categoryDetail, t.tabID, true);
                            newTab.isFlash = t.isFlash;
                            newTab.NewTweetSoundPath = (string)t.NewTweetSounds.Clone ();
                        }
                        else if (t.TabDelivery.TopCategory == TimelineCategories.UserTimeline)
                        {
                            //  こういうときは、べつなので生成した方がいいのでは。
                            newTab = tabControlOperation.Invoke(ActionType.UserTimeline, t.TabDelivery.deliveries[0].Category.categoryDetail, t.tabID, true);
                            newTab.isFlash = t.isFlash;
                            newTab.NewTweetSoundPath = (string)t.NewTweetSounds.Clone ();
                        }
                        else if (t.TabDelivery.TopCategory == TimelineCategories.UserFavoriteTimeline)
                        {
                            //  こういうときは、べつなので生成した方がいいのでは。
                            newTab = tabControlOperation.Invoke(ActionType.UserTimeline, t.TabDelivery.deliveries[0].Category.categoryDetail, t.tabID, true);
                            newTab.isFlash = t.isFlash;
                            newTab.NewTweetSoundPath = (string)t.NewTweetSounds.Clone ();
                        }
                        else if (t.TabDelivery.TopCategory == TimelineCategories.SearchTimeline)
                        {
                            newTab = tabControlOperation.Invoke(ActionType.Search, t.TabDelivery.deliveries[0].Category.categoryDetail, t.tabID, true);
                            newTab.isFlash = t.isFlash;
                            newTab.NewTweetSoundPath = (string)t.NewTweetSounds.Clone ();
                        }
                        else if (t.TabDelivery.TopCategory == TimelineCategories.BookmarkTimeline)
                        {
                            newTab = NewTab(t.isDefaultTab, t.isLock, t.sourceTabName, new TimelineCategory(TimelineCategories.BookmarkTimeline, null, true), null, t.tabID);
                            newTab.tabDelivery = (TabDeliveryCollection)t.TabDelivery.Clone();
                            newTab.isFlash = t.isFlash;
                            newTab.NewTweetSoundPath = (string)t.NewTweetSounds.Clone ();
                            newTab.SetFinishedLoading();
                        }
                        else
                        {
                            newTab = NewTab(t.isDefaultTab, t.isLock, t.sourceTabName, new TimelineCategory(TimelineCategories.None, null, false), null, t.tabID);
                            newTab.tabDelivery = (TabDeliveryCollection)t.TabDelivery.Clone();
                            newTab.isFlash = t.isFlash;
                            newTab.NewTweetSoundPath = (string)t.NewTweetSounds.Clone ();
                            newTab.SetFinishedLoading();
                        }
                        if (newTab != null)
                            LoadBySQL(newTab);
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("エラーが発生しました。タブの読み込みに失敗しました\n" + e.Message + "");
                return false;
            }
            return true;
        }

        public void SetHandlers(TimelineControl.OnChangedTweetHandler OnChangedTweetHandler, TimelineControl.OnUseTwitterAPIDelegate OnUserTwitterAPI,
                             TimelineControl.OnChangedTweetDelayPercentageHandler OnChangedTweetDelay, TimelineControl.OnRequiredAccountInfoDeleagate OnRequiredAccountInfo,
                             TimelineControl.OnCreatedReplyDataDelegate OnCreatedReplyData, TimelineControl.TabControlOperationgDelegate TabControlOperationHandler,
            TimelineControl.OnRequiredShrimpData OnRequiredShrimpData, TimelineControl.OnReloadShrimp OnReloadShrimp, Shrimp.OnCreatedTweetDelegate OnCreatedTweet,
            TabControls.FlashWindowDelegate OnFlashWindowDelegate, Shrimp.OnUserStatusControlAPIDelegate OnUserStatusControlAPI)
        {
            this.OnChangedTweetDelay += OnChangedTweetDelay;
            this.OnChangedTweetHandler += OnChangedTweetHandler;
            this.OnUserTwitterAPI += OnUserTwitterAPI;
            this.OnRequiredAccountInfo += OnRequiredAccountInfo;
            this.OnCreatedReplyData += OnCreatedReplyData;
            this.TabControlOperationHandler += TabControlOperationHandler;
            this.OnRequiredShrimpData += OnRequiredShrimpData;
            this.OnReloadShrimp += OnReloadShrimp;
            this.OnCreatedTweet += OnCreatedTweet;
            this.OnFlashWindowDelegate += OnFlashWindowDelegate;
            this.OnUserStatusControlAPI += OnUserStatusControlAPI;
        }

        /// <summary>
        /// タブを閉じるボタンが押された
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ShrimpTabs_TabCloseButtonClick(object sender, EventArgs e)
        {
            if (HoverControlNum >= 0)
            {
                if (DeleteTab(HoverControlNum))
                {
                    this.HoverControl.Dispose();
                    this.HoverControl = null;
                    this.HoverControlNum = -1;
                }
            }
        }

        void ContextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (sender != null)
            {
                ContextMenuStrip c = sender as ContextMenuStrip;
                c.Hide();
            }

            if (e.ClickedItem.Name == "SelectedThisTabMenu")
            {
                if (this.HoverControl != null)
                    this.SelectedTab = this.HoverControl;
            }

            if (e.ClickedItem.Name == "DestroyThisTabMenu")
            {
                if (this.HoverControl != null && this.HoverControlNum >= 0)
                {
                    if (this.DeleteTab(this.HoverControlNum))
                    {
                        this.HoverControl.Dispose();
                        this.HoverControl = null;
                        this.HoverControlNum = -1;
                    }
                }
            }

            if (e.ClickedItem.Name == "LockTabMenu")
            {
                if (this.HoverControl != null && this.HoverControlNum >= 0)
                {
                    if (!this.HoverControl.isDefaultTab)
                        this.HoverControl.isLock = !this.HoverControl.isLock;
                }
            }

            if (e.ClickedItem.Name == "FlashTabMenu")
            {
                if (this.HoverControl != null && this.HoverControlNum >= 0)
                {
                    this.HoverControl.isFlash = !this.HoverControl.isFlash;
                }
            }

            if ( e.ClickedItem.Name == "NewTweetSoundSetting" )
            {
                if ( this.HoverControl != null && this.HoverControlNum >= 0 )
                {
                    TabSound tabSound = new TabSound ( this.HoverControl.NewTweetSoundPath );
                    tabSound.ShowDialog ();
                    if ( tabSound.Tag != null )
                        this.HoverControl.NewTweetSoundPath = (string)tabSound.Tag;
                    tabSound.Dispose ();
                }
            }

            if (e.ClickedItem.Name == "DestroyAllTabWithoutThisTabMenu")
            {
                if (this.HoverControl != null && this.HoverControlNum >= 0)
                {
                    if (MessageBox.Show("他のタブをすべて閉じます。よろしいですか？", "注意", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return;
                    int bak = HoverControlNum;
                    for (int i = 0; i < this.TabCount; i++)
                    {
                        if (bak == i)
                            continue;
                        this.DeleteTab(i);
                    }
                }
            }

            if (e.ClickedItem.Name == "TabSettingMenu")
            {
                if (this.HoverControl != null && this.HoverControlNum >= 0 && OnRequiredAccountInfo != null)
                {
                    TabSettings tabSetting = new TabSettings(OnRequiredAccountInfo.Invoke(),
                        this.HoverControl.tabDelivery, this.HoverControl.ignoreTweet, (listDataCollection)OnRequiredShrimpData.Invoke("listDatas"));
                    tabSetting.ShowDialog();
                    if (tabSetting.Tag != null)
                    {
                        object[] obj = tabSetting.Tag as object[];
                        this.HoverControl.tabDelivery = (TabDeliveryCollection)obj[0];
                        this.HoverControl.ignoreTweet = (string)obj[1];
                        if (OnReloadShrimp != null)
                            OnReloadShrimp.Invoke();
                    }
                }
            }

            if (e.ClickedItem.Name == "ChangeTabNameMenu")
            {
                if (this.HoverControl != null && this.HoverControlNum >= 0)
                {
                    TabNameChange tabName = new TabNameChange(this.HoverControl.tabText);
                    tabName.ShowDialog();
                    if (tabName.Tag != null)
                        this.HoverControl.tabText = (string)tabName.Tag;
                    tabName.Dispose();
                }
            }

            if (e.ClickedItem.Name == "SearchMenu")
            {
                this.OpeningSearchBox();
            }

            if (e.ClickedItem.Name == "AddNewTabMenu")
            {
                var new_tab = this.NewTab(false, false, "新しいタブ", new TimelineCategory(TimelineCategories.None, null, false));
                TabSettings tabSetting = new TabSettings(OnRequiredAccountInfo.Invoke(),
                        new_tab.tabDelivery, new_tab.ignoreTweet, (listDataCollection)OnRequiredShrimpData.Invoke("listDatas"));
                tabSetting.ShowDialog();
                if (tabSetting.Tag != null)
                {
                    object[] obj = tabSetting.Tag as object[];
                    new_tab.tabDelivery = (TabDeliveryCollection)obj[0];
                    new_tab.ignoreTweet = (string)obj[1];
                }
            }

            if (e.ClickedItem.Name == "AddHomeTimelineWithNotify" || e.ClickedItem.Name == "AddHomeTimelineWithoutNotify")
            {
                var user_id = (decimal)e.ClickedItem.Tag;
                var new_tab = this.NewTab(false, false, e.ClickedItem.Text, new TimelineCategory(TimelineCategories.HomeTimeline, null, false));
                new_tab.tabDelivery.deliveries[0].DeliveryFromUsers.Add(user_id);
                if (e.ClickedItem.Name == "AddHomeTimelineWithNotify")
                {
                    var user = new List<decimal>();
                    user.Add(user_id);
                    new_tab.tabDelivery.AddDelivery(new TabDelivery(new TimelineCategory(TimelineCategories.NotifyTimeline, null, false),
                        user));
                }
            }

            if (e.ClickedItem.Name == "AddReplyWithNotify" || e.ClickedItem.Name == "AddReplyWithoutNotify")
            {
                var user_id = (decimal)e.ClickedItem.Tag;
                var new_tab = this.NewTab(false, false, e.ClickedItem.Text, new TimelineCategory(TimelineCategories.MentionTimeline, null, false));
                new_tab.tabDelivery.deliveries[0].DeliveryFromUsers.Add(user_id);
                if (e.ClickedItem.Name == "AddReplyWithNotify")
                {
                    var user = new List<decimal>();
                    user.Add(user_id);
                    new_tab.tabDelivery.AddDelivery(new TabDelivery(new TimelineCategory(TimelineCategories.NotifyTimeline, null, false),
                        user));
                }
            }

            if (e.ClickedItem.Name == "AddNotify")
            {
                var user_id = (decimal)e.ClickedItem.Tag;
                var new_tab = this.NewTab(false, false, e.ClickedItem.Text, new TimelineCategory(TimelineCategories.NotifyTimeline, null, false));
                new_tab.tabDelivery.deliveries[0].DeliveryFromUsers.Add(user_id);
            }

            if (e.ClickedItem.Name == "AddDirectMessage")
            {
                var user_id = (decimal)e.ClickedItem.Tag;
                var new_tab = this.NewTab(false, false, e.ClickedItem.Text, new TimelineCategory(TimelineCategories.DirectMessageTimeline, null, false));
                new_tab.tabDelivery.deliveries[0].DeliveryFromUsers.Add(user_id);
            }
        }

        private void OpeningSearchBox()
        {
            SearchForm sr = new SearchForm(this.beforeSearchDetail);
            DialogResult res = sr.ShowDialog();
            if (res == DialogResult.OK && sr.Tag != null && OnUserTwitterAPI != null)
            {
                string pr = sr.Tag as string;
                this.beforeSearchDetail = pr;
                var tab = this.NewTab(true, false, "\"" + pr + "\"の検索結果",
                     new TimelineCategory(TimelineCategories.SearchTimeline, pr, false));
                if (Setting.Search.isIgnoreRT)
                    pr += " -rt";
                if (Setting.Search.isOnlyJapanese)
                    pr += " lang:ja";
                OnUserTwitterAPI.Invoke(tab, new object[] { "search", pr, null });
                this.SelectedTab = tab;
            }
            sr.Dispose();
        }

        void ContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            TabControlContextMenu ctl = sender as TabControlContextMenu;
            ctl.canSelect = (this.HoverControl != null);
            if (this.HoverControl != null)
            {
                ctl.isFlash = this.HoverControl.isFlash;
                ctl.isLock = this.HoverControl.isLock;
                ctl.canEdit = !(this.HoverControl.tabDelivery.TopCategory == TimelineCategories.SearchTimeline ||
                                this.HoverControl.tabDelivery.TopCategory == TimelineCategories.UserFavoriteTimeline ||
                                this.HoverControl.tabDelivery.TopCategory == TimelineCategories.BookmarkTimeline ||
                                this.HoverControl.tabDelivery.TopCategory == TimelineCategories.UserInformation ||
                                this.HoverControl.tabDelivery.TopCategory == TimelineCategories.UserTimeline);
            }

            ctl.CustomControlMenu.Items.Clear();
            var account = (OnRequiredAccountInfo != null ? OnRequiredAccountInfo.Invoke() : null);
            if (account == null)
                return;
            foreach (TwitterInfo t in account.accounts)
            {
                ToolStripMenuItem item = new ToolStripMenuItem("@" + t.ScreenName + "のホームタイムライン(通知あり)", t.IconData);
                item.Name = "AddHomeTimelineWithNotify";
                item.Tag = t.UserId;
                ctl.CustomControlMenu.Items.Add(item);

                item = new ToolStripMenuItem("@" + t.ScreenName + "のホームタイムライン(通知なし)");
                item.Name = "AddHomeTimelineWithoutNotify";
                item.Tag = t.UserId;
                ctl.CustomControlMenu.Items.Add(item);

                item = new ToolStripMenuItem("@" + t.ScreenName + "の返信(通知あり)");
                item.Name = "AddReplyWithNotify";
                item.Tag = t.UserId;
                ctl.CustomControlMenu.Items.Add(item);

                item = new ToolStripMenuItem("@" + t.ScreenName + "の返信(通知なし)");
                item.Name = "AddReplyWithoutNotify";
                item.Tag = t.UserId;
                ctl.CustomControlMenu.Items.Add(item);

                item = new ToolStripMenuItem("@" + t.ScreenName + "の通知");
                item.Name = "AddNotify";
                item.Tag = t.UserId;
                ctl.CustomControlMenu.Items.Add(item);

                item = new ToolStripMenuItem("@" + t.ScreenName + "のダイレクトメッセージ");
                item.Name = "AddDirectMessage";
                item.Tag = t.UserId;
                ctl.CustomControlMenu.Items.Add(item);

                ctl.CustomControlMenu.Items.Add(new ToolStripSeparator());
            }
        }

        ~ShrimpTabs()
        {
        }

        protected override void Dispose(bool isDisposing)
        {
            if (!this.isDisposeds)
            {
                this.TabCloseButtonClick -= new EventHandler(ShrimpTabs_TabCloseButtonClick);
                if (this.ContextMenuStrip != null)
                {
                    this.ContextMenuStrip.ItemClicked -= new ToolStripItemClickedEventHandler(ContextMenuStrip_ItemClicked);
                    this.ContextMenuStrip.Opening -= new System.ComponentModel.CancelEventHandler(ContextMenuStrip_Opening);
                    this.ContextMenuStrip = null;
                }
                this.SelectedIndexChanged -= new EventHandler(ShrimpTabs_SelectedIndexChanged);
                if (this.tabs != null)
                {
                    foreach (TabControls tab in this.tabs)
                    {
                        tab.Dispose();
                    }
                    this.tabs.Clear();
                    this.tabs = null;
                }

                base.Dispose(isDisposing);
                this.isDisposeds = true;
            }
        }

        public void StopTabQueue ()
        {
            this.tabQueue.Wait ();
        }

        /// <summary>
        /// タブを作成する
        /// </summary>
        /// <param name="isDefaultTab"></param>
        /// <param name="isLock"></param>
        /// <param name="tabText"></param>
        /// <param name="timelineCategory"></param>
        public TabControls NewTab(bool isDefaultTab, bool isLock, string tabText, TimelineCategory timelineCategory, ControlParts.Tabs.TabControls.OnReloadDelegate OnReload = null, string tabID = "")
        {
            var t = new TabControls(db, isDefaultTab, isLock, timelineCategory, OnReload, TabControlOperationHandler,
                OnUserTwitterAPI, OnFlashWindowDelegate, OnUserStatusControlAPI);
            t.tabText = tabText;
            t.isVisible = (this.tabs.Count == 0);
            if (timelineCategory.category == TimelineCategories.UserInformation)
            {
                UserStatusControl obj = t.TimelineObject as UserStatusControl;
                obj.SetHandlers(OnChangedTweetHandler, OnUserTwitterAPI, OnChangedTweetDelay, OnRequiredAccountInfo, OnCreatedReplyData,
                    TabControlOperationHandler, OnRequiredShrimpData, OnReloadShrimp, OnCreatedTweet, OnFlashWindowDelegate, OnUserStatusControlAPI);
                //if ( TabControlOperationHandler != null )
                //    obj.TabControlUserInformationHandler += TabControlOperationHandler;
            }
            else
            {
                TimelineControl obj = t.TimelineObject as TimelineControl;
                if (OnChangedTweetHandler != null)
                    obj.OnChangeTweet += OnChangedTweetHandler;
                if (OnUserTwitterAPI != null)
                    obj.OnUseTwitterAPI += OnUserTwitterAPI;
                if (OnRequiredAccountInfo != null)
                    obj.OnRequiredAccountInfo += OnRequiredAccountInfo;
                if (OnCreatedReplyData != null)
                    obj.OnCreatedReplyData += OnCreatedReplyData;
                if (TabControlOperationHandler != null)
                    obj.TabControlOperatingHandler += TabControlOperationHandler;
            }
            if (tabID == "")
            {
                Random rnd = new Random();
                for (; ; )
                {
                    tabID = "t_" + rnd.Next() + "";
                    if (this.tabs.Any((tb) => tb.TabID == tabID))
                        continue;
                    break;
                }

            }
            t.TabID = tabID;

            if (db != null)
                db.CreateTable(t.tableCreateSQL);
            this.tabs.Add(t);
            this.TabPages.Add ( t );
            return t;
        }

        /// <summary>
        /// タブを削除する
        /// </summary>
        /// <param name="num"></param>
        public bool DeleteTab(int num)
        {
            if (this.tabs[num].isLock)
                return false;
            if (!this.tabs[num].tabDelivery.isTimeline)
            {
                UserStatusControl us = this.tabs[num].TimelineObject as UserStatusControl;
                if (us != null)
                {
                    us.Dispose(); us = null;
                }
            }
            else
            {
                TimelineControl us = this.tabs[num].TimelineObject as TimelineControl;
                if (us != null)
                {
                    us.Dispose(); us = null;
                }
            }

            if (db != null)
                db.CreateTable(tabs[num].destroyTableSQL);
            this.tabs.RemoveAt(num);
            this.TabPages.RemoveAt(num);

            if (this.tabChangedStack.Count >= 3)
            {
                this.tabChangedStack.Pop();
                this.tabChangedStack.Pop();
                var beforeNum = this.tabChangedStack.Pop();
                if (this.TabPages.Count != 0)
                {
                    var cl = this.TabPages.Count;
                    if (beforeNum < 0)
                        beforeNum = 0;
                    if (beforeNum >= cl - 1)
                        beforeNum = cl - 1;

                    this.SelectTab(beforeNum);
                }
            }
            return true;
        }


        /// <summary>
        /// タブを削除する
        /// </summary>
        /// <param name="num"></param>
        public bool DeleteTab(TabControls tab)
        {
            if (tab.isLock)
                return false;
            if (!tab.tabDelivery.isTimeline)
            {
                UserStatusControl us = tab.TimelineObject as UserStatusControl;
                if (us != null)
                {
                    us.Dispose(); us = null;
                }
            }
            else
            {
                TimelineControl us = tab.TimelineObject as TimelineControl;
                if (us != null)
                {
                    us.Dispose(); us = null;
                }
            }

            if (db != null)
                db.CreateTable(tab.destroyTableSQL);

            this.tabs.Remove(tab);
            this.TabPages.Remove(tab);

            if (this.tabChangedStack.Count >= 3)
            {
                this.tabChangedStack.Pop();
                this.tabChangedStack.Pop();
                var beforeNum = this.tabChangedStack.Pop();
                if (this.TabPages.Count != 0)
                {
                    var cl = this.TabPages.Count;
                    if (beforeNum < 0)
                        beforeNum = 0;
                    if (beforeNum >= cl - 1)
                        beforeNum = cl - 1;

                    this.SelectTab(beforeNum);
                }
            }
            return true;
        }

        /// <summary>
        /// SQLから読み込む
        /// </summary>
        /// <param name="tab"></param>
        public void LoadBySQL(TabControls tab)
        {
            if (db != null)
            {
                if (tab.tabDelivery.isTimeline)
                {
					Task.Factory.StartNew(() =>
					{
						List<TwitterStatus> statuses = null;
						if (tab.tabDelivery.TopCategory == TimelineCategories.DirectMessageTimeline)
						{
							//  DM
							var res = db.GetlistDirectMessages(tab.getDMTimelineSQL, 0, 0);
							if ( res != null )
								statuses = res.ConvertAll((t) => (TwitterStatus)t);
						}
						else
						{
							statuses = db.GetlistTweets(tab.getTimelineSQL, 0, 0);
						}
						if (statuses != null)
						{
							if (OnCreatedTweet != null)
							{
								foreach (TwitterStatus tweet in statuses)
								{
									OnCreatedTweet.BeginInvoke(new OnCreatedTweetHook(tweet), null, null);
								}
							}
							tab.BeginInvoke((MethodInvoker)delegate()
							{
								tab.InsertTimelineRange(statuses, true);
								tab.SetFirstView(true);
							});
						}
					});
                }
            }
        }

        /// <summary>
        /// ツイートを振り分けて送信する
        /// </summary>
        /// <param name="tweet"></param>
        /// <param name="destCategories"></param>
        public void InsertTweet(TwitterInfo sourceUser, TwitterStatus tweet, TimelineCategories destCategories)
        {
            tabQueue.Enqueue ( new TabQueueData ( this, (Module.Queue.TabQueueData.TabQueueActionDelegate)delegate ()
            {
                object obj = tweet;
                //  プラグイン
                if ( OnCreatedTweet != null )
                {
                    var hook = new OnCreatedTweetHook ( tweet );
                    OnCreatedTweet.BeginInvoke ( hook, null, null );
                    tweet = hook.status;
                }

                if ( destCategories == TimelineCategories.NotifyTimeline )
                    obj = tweet.NotifyStatus;
                decimal id = ( sourceUser != null ? sourceUser.UserId : 0 );
                this.tabs.InsertTweet ( this.shrimpQueryParser, tweet, id, destCategories, obj );
            } ) );
        }

        /// <summary>
        /// ツイートをいっきにいれる
        /// </summary>
        /// <param name="sourceUser"></param>
        /// <param name="tweets"></param>
        /// <param name="destCategories"></param>
        /// <param name="obj"></param>
        public void InsertTweetRange(decimal user_id, List<TwitterStatus> tweets, TimelineCategories destCategories, object obj)
        {
            if (tweets == null || tweets.Count == 0)
                return;

            tabQueue.Enqueue ( new TabQueueData ( this, (Module.Queue.TabQueueData.TabQueueActionDelegate)delegate()
            {
                if (OnCreatedTweet != null)
                {
                    foreach (TwitterStatus tweet in tweets)
                    {
                        OnCreatedTweet.BeginInvoke(new OnCreatedTweetHook(tweet), null, null);
                    }
                }
                this.tabs.InsertTweetRange(this.shrimpQueryParser, tweets, user_id, destCategories, obj);
            } ) );

            return;
        }

        public void InsertTweetRange ( TabControls tab, List<TwitterStatus> tweets )
        {
            if ( tweets == null || tweets.Count == 0 )
                return;

            tabQueue.Enqueue ( new TabQueueData ( this, (Module.Queue.TabQueueData.TabQueueActionDelegate)delegate ()
            {
                if ( OnCreatedTweet != null )
                {
                    foreach ( TwitterStatus tweet in tweets )
                    {
                        OnCreatedTweet.BeginInvoke ( new OnCreatedTweetHook ( tweet ), null, null );
                    }
                }
                tab.InsertTimelineRange ( tweets );
            } ) );
            return;
        }

        /// <summary>
        /// 選択中のタブを拾う
        /// </summary>
        public TabControls SelectedTabControls
        {
            get
            {
                return this.tabs[this.SelectedIndex];
            }
        }

        public void Crolling(TwitterInfo t, Timelines timelines, DirectMessages directMessage, lists lists, decimal counter)
        {
            if (timelines == null || directMessage == null || lists == null)
                return;

            if (counter % Setting.CrollingTimeline.MentionTimeline == 0)
            {
                timelines.MentionTimeline(t,
                 (Twitter.REST.TwitterWorker.TwitterCompletedProcessDelegate)delegate(object data)
                 {
                     List<TwitterStatus> tmp = (List<TwitterStatus>)data;
                     if (tmp.Count != 0)
                     {
                         t.MentionTimelineSinceID = tmp[0].id;
                         this.BeginInvoke ( (MethodInvoker)delegate ()
                         {
                             this.InsertTweetRange ( t.UserId, tmp, TimelineCategories.MentionTimeline, null );
                         } );
                     }
                 }, null, t.MentionTimelineSinceID);
            }


            if (counter % Setting.CrollingTimeline.DirectMessage == 0)
            {
                directMessage.GetDirectMessage(t,
                    (Twitter.REST.TwitterWorker.TwitterCompletedProcessDelegate)delegate(object data)
                    {
                        List<TwitterDirectMessageStatus> tmp = (List<TwitterDirectMessageStatus>)data;
                        if (tmp.Count != 0)
                        {
                            db.InsertDMRange(tmp);
                            db.InsertUserRange(tmp.ConvertAll((dm) => dm.user));
                            t.DirectMessageReceivedSinceID = tmp[0].id;
                            var lis = tmp.ConvertAll((tweet) => (TwitterStatus)tweet);
                            this.BeginInvoke ( (MethodInvoker)delegate ()
                            {
                                this.InsertTweetRange ( t.UserId, lis, TimelineCategories.DirectMessageTimeline, null );
                            } );
                        }
                    }, null, t.DirectMessageReceivedSinceID, 0);
                directMessage.GetSentDirectMessage(t,
                    (Twitter.REST.TwitterWorker.TwitterCompletedProcessDelegate)delegate(object data)
                    {
                        List<TwitterDirectMessageStatus> tmp = (List<TwitterDirectMessageStatus>)data;
                        if (tmp.Count != 0)
                        {
                            db.InsertDMRange(tmp);
                            db.InsertUserRange(tmp.ConvertAll((dm) => dm.user));
                            t.DirectMessageReceivedSinceID = tmp[0].id;
                            var lis = tmp.ConvertAll((tweet) => (TwitterStatus)tweet);
                            this.BeginInvoke ( (MethodInvoker)delegate ()
                            {
                                this.InsertTweetRange ( t.UserId, lis, TimelineCategories.DirectMessageTimeline, null );
                            } );
                        }
                    }, null, t.DirectMessageSendSinceID, 0);
            }


            if (counter % Setting.CrollingTimeline.HomeTimeline == 0)
            {
                timelines.HomeTimeline(t,
                 (Twitter.REST.TwitterWorker.TwitterCompletedProcessDelegate)delegate(object data)
                 {
                     List<TwitterStatus> tmp = (List<TwitterStatus>)data;
                     if (tmp.Count != 0)
                     {
                         t.HomeTimelineSinceID = tmp[0].id;
                         if ( this.IsHandleCreated )
                         {
                             this.BeginInvoke ( (MethodInvoker)delegate ()
                            {
                                this.InsertTweetRange ( t.UserId, tmp, TimelineCategories.HomeTimeline, null );
                            } );
                         }
                         //delay.Invoke ( per );
                     }
                 }, null, t.HomeTimelineSinceID);

                listDataCollection destC = new listDataCollection();
                foreach (TabControls tabctl in this.tabs)
                {
                    var dest1 = tabctl.tabDelivery.FindListData(t.UserId);
                    if (dest1 != null)
                        destC.AddlistRange(dest1);
                }

                foreach (listData list in destC.lists)
                {
                    /*
                    if ( list.list_users == null || list.list_users_cursor != 0 )
                    {
                        //  リストのメンバーを取得
                        Task.Factory.StartNew ( () =>
                        {
                            for ( ; ; )
                            {
                                var isLimited = false;
                                var thread = lists.listMembers ( t,
                                (Twitter.REST.TwitterWorker.TwitterCompletedProcessDelegate)delegate ( object data )
                                {
                                    TwitterFriendshipResult tmp = (TwitterFriendshipResult)data;
                                    list.list_users_cursor = tmp.next_cursor;
                                    if ( tmp.Count != 0 )
                                    {
                                        if ( list.list_users == null )
                                            list.list_users = new List<decimal> ();
                                        list.list_users.AddRange ( tmp.ConvertAll ( ( user ) => user.id ) );
                                    }
                                }, (Twitter.REST.TwitterWorker.TwitterErrorProcessDelegate)delegate ( TwitterCompletedEventArgs data )
                                {
                                    isLimited = true;
                                }, list.list_id, list.slug, list.list_users_cursor );
                                thread.Join ();

                                if ( list.list_users_cursor <= 0 || isLimited )
                                    break;

                            }
                        } );
                    }
                    */
                    lists.listStatuses(t,
                    (Twitter.REST.TwitterWorker.TwitterCompletedProcessDelegate)delegate(object data)
                    {
                        List<TwitterStatus> tmp = (List<TwitterStatus>)data;
                        if (tmp.Count != 0)
                        {
                            if ( this.IsHandleCreated )
                            {
                                this.BeginInvoke ( (MethodInvoker)delegate ()
                                {
                                    this.InsertTweetRange ( list.create_user_id, tmp, TimelineCategories.ListTimeline, list.list_id );
                                } );
                            }
                        }
                    }, null, list.list_id, list.slug);
                }
            }
        }

        /// <summary>
        /// タブが変更された
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ShrimpTabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tab = sender as TabControl;
            int num = tab.SelectedIndex;

            var res = this.tabs.SelectedChange(num);
            if (res != null && Setting.Timeline.TabChangeAnimation != TabAnimation.None)
            {
                if (res is TimelineControl)
                {
                    var res2 = res as TimelineControl;
                    res2.StartTabChangeAnimation(this.BeforeControlBitmap, res2.CaptureControl(), num < this.BeforeSelectedTabIndex, (this.Alignment == TabAlignment.Left || this.Alignment == TabAlignment.Right));
                }
                else
                {
                    var res2 = res as UserStatusControl;
                    res2.StartTabChangeAnimation(this.BeforeControlBitmap, res2.CaptureControl(), num < this.BeforeSelectedTabIndex, (this.Alignment == TabAlignment.Left || this.Alignment == TabAlignment.Right));
                }
            }

            if (tab.SelectedTab != null && tab.SelectedTab.Controls != null)
                tab.SelectedTab.Controls[0].Focus();

            //  前押されたタブの管理を行う
            this.BeforeSelectedTab = (TabControls)this.SelectedTab;
            this.BeforeSelectedTabIndex = this.SelectedIndex;
            if (this.BeforeControlBitmap != null)
                this.BeforeControlBitmap.Dispose();
            this.BeforeControlBitmap = this.BeforeSelectedTab.CaptureControl();
            this.tabChangedStack.Push(num);
            //tab.SelectedTab.Focus ();
        }
    }
}
