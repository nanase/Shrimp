using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Shrimp.Account;
using Shrimp.Twitter;
using Shrimp.Twitter.Status;

namespace Shrimp.ControlParts.ContextMenus.TextMenu
{
    public partial class SelTweetContextMenu : Component
    {
        #region 定義
        /// <summary>
        /// メニューが閉じたときのイベントハンドラ
        /// </summary>
        public event EventHandler MenuClosed;
        public event EventHandler MenuOpening;
        public event ToolStripItemClickedEventHandler MenuItemClicked;
        List<ToolStripMenuItem> accountItems = new List<ToolStripMenuItem>();
        public bool isClosed = false;
        private SelUserContextMenu UserMenu = new SelUserContextMenu();
        private SelUserContextMenu RetweetedUserMenu = new SelUserContextMenu();
        #endregion

        public SelTweetContextMenu()
        {
            InitializeComponent();
            this.FavMenu.DropDownItemClicked += new ToolStripItemClickedEventHandler(Menu_ItemClicked);
            this.RetweetMenu.DropDownItemClicked += new ToolStripItemClickedEventHandler(Menu_ItemClicked);
            this.ReplyMenu.Image = Setting.ResourceImages.Reply.hover;
            this.FavMenu.Image = Setting.ResourceImages.Fav.hover;
            this.RetweetMenu.Image = Setting.ResourceImages.Retweet.hover;
            this.AboutThisUserMenu.Image = Setting.ResourceImages.UserImage;
            this.RegistBookmarkMenu.Image = Setting.ResourceImages.BookmarkImage;
            this.DeleteTweetMenu.Image = Setting.ResourceImages.RemoveImage;

            this.AboutThisUserMenu.DropDown = this.UserMenu.ContextMenu;
            this.RetweetedByUserMenu.DropDown = this.RetweetedUserMenu.ContextMenu;
            this.UserMenu.MenuItemClicked += new ToolStripItemClickedEventHandler(Menu_ItemClicked);
            this.RetweetedUserMenu.MenuItemClicked += new ToolStripItemClickedEventHandler(Menu_ItemClicked);
        }

        ~SelTweetContextMenu()
        {
            this.FavMenu.DropDownItemClicked -= new ToolStripItemClickedEventHandler(Menu_ItemClicked);
            this.RetweetMenu.DropDownItemClicked -= new ToolStripItemClickedEventHandler(Menu_ItemClicked);
            this.Menu.Opening -= new CancelEventHandler(Menu_Opening);
            this.Menu.Closed -= new ToolStripDropDownClosedEventHandler(Menu_Closed);
            this.UserMenu.MenuItemClicked -= new ToolStripItemClickedEventHandler(Menu_ItemClicked);
            this.RetweetedUserMenu.MenuItemClicked -= new ToolStripItemClickedEventHandler(Menu_ItemClicked);
        }

        void Menu_Opening(object sender, CancelEventArgs e)
        {
            this.isClosed = false;
            if (this.MenuOpening != null)
                this.MenuOpening(sender, e);
        }

        /// <summary>
        /// メニューを表示
        /// </summary>
        /// <param name="p"></param>
        public void ShowMenu(Point p, AccountManager accounts, TwitterStatus status)
        {
            if (!status.isDirectMessage && !status.isNotify)
            {
                this.DeleteTweetMenu.Text = "ツイートを削除する(&D)";
                this.FavMenu.Enabled = true;
                this.RetweetMenu.Enabled = true;
                this.RegistBookmarkMenu.Enabled = true;

                var selAccount = accounts.SelectedAccount;
                if (selAccount != null)
                {
                    this.FavMenu.DropDownItems.Add(getSelectingAccount(selAccount));
                    this.RetweetMenu.DropDownItems.Add(getSelectingAccount(selAccount));
                }
                var t = getAccountList(accounts.accounts);
                foreach (ToolStripItem item in t)
                {
                    this.FavMenu.DropDownItems.Add(item);
                }
                t = getAccountList(accounts.accounts);
                foreach (ToolStripItem item in t)
                {
                    this.RetweetMenu.DropDownItems.Add(item);
                }
                t.Clear(); t = null;
            }
            else
            {
                if (status.isDirectMessage)
                    this.DeleteTweetMenu.Text = "DMを削除する(&D)";
                this.FavMenu.Enabled = false;
                this.RetweetMenu.Enabled = false;
                this.RegistBookmarkMenu.Enabled = false;
            }

            this.AboutThisUserMenu.Text = "このユーザー(@" + status.DynamicTweet.user.screen_name + ")について(&U)";
            this.UserMenu.screen_name = status.DynamicTweet.user.screen_name;
            if (status.retweeted_status != null)
            {
                this.RetweetedByUserMenu.Text = "リツイートしたユーザ(@" + status.user.screen_name + ")について(&B)";
                this.RetweetedUserMenu.screen_name = status.user.screen_name;
            }
            this.RetweetedByUserMenu.Enabled = (status.retweeted_status != null);
            this.Menu.Show(p);
        }

        /// <summary>
        /// 選択中のアカウント、を表示するToolStripMenuItemを表示
        /// </summary>
        /// <param name="SelectAccount"></param>
        /// <returns></returns>
        private ToolStripMenuItem getSelectingAccount(TwitterInfo SelectAccount)
        {
            var iconImageSel = SelectAccount.IconData;
            var dropItemFirst = new ToolStripMenuItem("選択中のアカウント(@" + SelectAccount.ScreenName + ")", iconImageSel)
            {
                Name = "AccountSelected",
                Tag = SelectAccount
            };
            accountItems.Add(dropItemFirst);
            return dropItemFirst;
        }

        /// <summary>
        /// アカウントリストを表示するList<ToolStripItem>を表示</ToolStripItem>
        /// </summary>
        /// <param name="accounts"></param>
        /// <returns></returns>
        private List<ToolStripItem> getAccountList(List<TwitterInfo> accounts)
        {
            List<ToolStripItem> items = new List<ToolStripItem>();
            foreach (TwitterInfo t in accounts)
            {
                var iconImage = t.IconData;
                var dropItem = new ToolStripMenuItem("@" + t.ScreenName + "", iconImage) { Name = "AccountSelected", Tag = t };
                accountItems.Add(dropItem);
                items.Add(dropItem);
            }
            return items;
        }

        /// <summary>
        /// ツイート削除可能かどうかを調べる
        /// </summary>
        public bool isEnableDelTweet
        {
            get { return this.DeleteTweetMenu.Enabled; }
            set
            {
                if (isEnableDelTweet != value)
                {
                    if (this.Menu.InvokeRequired)
                    {
                        this.Menu.Invoke((MethodInvoker)delegate()
                        {
                            this.DeleteTweetMenu.Enabled = value;
                        });
                    }
                    else
                    {
                        this.DeleteTweetMenu.Enabled = value;
                    }
                }
            }
        }

        /// <summary>
        /// 表示中？
        /// </summary>
        public bool Visible
        {
            get { return this.Menu.Visible; }
        }

        /// <summary>
        /// メニューが閉じたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            this.isClosed = true;
            foreach (ToolStripMenuItem items in this.accountItems)
            {
                items.Dispose();
            }
            accountItems.Clear();

            if (MenuClosed != null)
            {
                MenuClosed(sender, e);
            }
        }

        /// <summary>
        /// メニューがクリックされたとき。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "FavMenu" || e.ClickedItem.Name == "RetweetMenu")
                return;
            if (MenuItemClicked != null)
            {
                MenuItemClicked.Invoke(sender, e);
            }
        }

        public void MenuClose()
        {
            this.Menu.Hide();
        }
    }
}
