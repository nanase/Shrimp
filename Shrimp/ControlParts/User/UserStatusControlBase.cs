using System.Drawing;
using System.Windows.Forms;
using Shrimp.ControlParts.ContextMenus.TextMenu;
using Shrimp.ControlParts.Timeline;
using Shrimp.ControlParts.Timeline.Animation;
using Shrimp.ControlParts.Timeline.Click;
using Shrimp.ControlParts.Timeline.Select;
using Shrimp.Module.ImageUtil;
using Shrimp.Module.Parts;
using Shrimp.Setting;
using Shrimp.Twitter.Entities;
using Shrimp.Twitter.Status;

namespace Shrimp.ControlParts.User
{
    public partial class UserStatusControlBase : UserControl, IControl
    {
        #region 定義
        private TwitterUserStatus user;
        private UserStatusCells cells = new UserStatusCells();
        private Bitmap icon;
        private bool ReadyIcon = false;
        private System.Timers.Timer imageChecker;
        private System.Windows.Forms.Timer loadingTimer;
        private SelectControl selectControl = new SelectControl();
        private ClickCells clickCells = new ClickCells();
        private PictureBox loadingBox = new PictureBox();
        private bool isSetBox = false;
        private bool isSuspended = false;
        private TabChangeAnimation tabchange_anime;
        private AnimationControl anicon;
        private SelUserContextMenu selUserContextMenu;
        private TimelineControl.TabControlOperationgDelegate TabControlOperatingHandler;
        private TimelineControl.OnUseTwitterAPIDelegate OnUseTwitterAPIHandler;
        private bool isDisposedShrimp = false;
        #endregion

        public UserStatusControlBase(TwitterUserStatus user, TimelineControl.TabControlOperationgDelegate TabControlOperatingHandler,
            TimelineControl.OnUseTwitterAPIDelegate OnUseTwitterAPIHandler)
        {
            InitializeComponent();
            this.user = user;
            //  アニメーション
            this.tabchange_anime = new TabChangeAnimation();
            this.anicon = new AnimationControl(this.RedrawControl, null, 0, null, 0, tabchange_anime.FrameExecute, 16);

            this.imageChecker = new System.Timers.Timer();
            this.imageChecker.Interval = 500;
            this.imageChecker.Elapsed += new System.Timers.ElapsedEventHandler(imageChecker_Elapsed);
            this.imageChecker.Start();
            this.loadingTimer = new Timer();
            this.loadingTimer.Tick += new System.EventHandler(loadingTimer_Tick);
            this.loadingTimer.Interval = 100;
            this.loadingTimer.Start();
            this.icon = (Bitmap)ResourceImages.LoadingImage.Clone();
            loadingBox.Image = (Image)Properties.Resources.loadingAnime.Clone();
            loadingBox.Dock = DockStyle.Fill;

            this.TabControlOperatingHandler = TabControlOperatingHandler;
            this.selUserContextMenu = new SelUserContextMenu();
            this.selUserContextMenu.MenuItemClicked += new ToolStripItemClickedEventHandler(selTweetContextMenu_MenuItemClicked);
            this.OnUseTwitterAPIHandler = OnUseTwitterAPIHandler;
        }

        ~UserStatusControlBase()
        {
            if (!isDisposedShrimp)
            {
                this.imageChecker.Stop();
                this.imageChecker.Elapsed -= new System.Timers.ElapsedEventHandler(imageChecker_Elapsed);
                this.selUserContextMenu.MenuItemClicked -= new ToolStripItemClickedEventHandler(selTweetContextMenu_MenuItemClicked);
                this.selUserContextMenu.Dispose();
                if (loadingTimer != null)
                {
                    this.loadingTimer.Stop();
                    this.loadingTimer.Tick -= new System.EventHandler(loadingTimer_Tick);
                }
                isDisposedShrimp = true;
            }
        }


        void selTweetContextMenu_MenuItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var scr = "";
            if (sender != null && sender is string)
                scr = sender as string;
            else
                return;

            if (e.ClickedItem.Name == "OpenUserInformationTabMenu")
            {
                ActionControl.DoAction(ActionType.Mention, scr);
            }
            else if (e.ClickedItem.Name == "OpenUserTimelineTabMenu")
            {
                ActionControl.DoAction(ActionType.UserTimeline, scr);
            }
            else if (e.ClickedItem.Name == "OpenUserFavTimelineTabMenu")
            {
                ActionControl.DoAction(ActionType.UserFavoriteTimeline, scr);
            }
            else if (e.ClickedItem.Name == "OpenReplyToUserTabMenu")
            {
                ActionControl.DoAction(ActionType.Search, "@" + scr + "");
            }
            else if (e.ClickedItem.Name == "OpenConversationTabMenu")
            {
                ActionControl.DoAction(ActionType.Search, "from:" + scr + " OR @" + scr + "");
            }
            else if (e.ClickedItem.Name == "FollowMenu")
            {
                selUserContextMenu.MenuClose();
                ActionControl.DoAction(ActionType.Follow, scr);
            }
            else if (e.ClickedItem.Name == "BlockMenu")
            {
                selUserContextMenu.MenuClose();
                ActionControl.DoAction(ActionType.Block, scr);
            }
            else if (e.ClickedItem.Name == "ReportSpamMenu")
            {
                selUserContextMenu.MenuClose();
                ActionControl.DoAction(ActionType.Spam, scr);
            }
            /*
            if ( e.ClickedItem.Name == "OpenUserInformationTabMenu" )
            {
                if ( TabControlOperatingHandler != null )
                    TabControlOperatingHandler.Invoke ( "mention", scr );
            }
            else if ( e.ClickedItem.Name == "OpenUserTimelineTabMenu" )
            {
                if ( TabControlOperatingHandler != null )
                    TabControlOperatingHandler.Invoke ( "usertimeline", scr );
            }
            else if ( e.ClickedItem.Name == "OpenUserFavTimelineTabMenu" )
            {
                if ( TabControlOperatingHandler != null )
                    TabControlOperatingHandler.Invoke ( "userfavoritetimeline", scr );
            }
            else if ( e.ClickedItem.Name == "OpenReplyToUserTabMenu" )
            {
                if ( TabControlOperatingHandler != null )
                    TabControlOperatingHandler.Invoke ( "hashtags", "to:" + scr + "" );
            }
            else if ( e.ClickedItem.Name == "OpenConversationTabMenu" )
            {
                if ( TabControlOperatingHandler != null )
                    TabControlOperatingHandler.Invoke ( "hashtags", "from:" + scr + " OR to:" + scr + "" );
            }
            else if ( e.ClickedItem.Name == "FollowMenu" )
            {
                selUserContextMenu.MenuClose ();
                OnUseTwitterAPIHandler.Invoke ( null, new object[] { "follow", scr, 0 } );
                
            }
            else if ( e.ClickedItem.Name == "BlockMenu" )
            {
                selUserContextMenu.MenuClose ();
                OnUseTwitterAPIHandler.Invoke ( null, new object[] { "block", scr, 0 } );
            }
            else if ( e.ClickedItem.Name == "ReportSpamMenu" )
            {
                selUserContextMenu.MenuClose ();
                OnUseTwitterAPIHandler.Invoke ( null, new object[] { "spam", scr, 0 } );
            }
            */
        }

        void loadingTimer_Tick(object sender, System.EventArgs e)
        {
            if (this.user != null || isLoadingFinished)
            {
                this.Controls.Remove(this.loadingBox);
                this.loadingBox.Dispose();
                this.loadingBox = null;
                this.loadingTimer.Stop();
                this.loadingTimer.Tick -= new System.EventHandler(loadingTimer_Tick);
                this.loadingTimer = null;

            }
            else
            {
                if (!isSetBox)
                {
                    this.Controls.Add(this.loadingBox);
                    isSetBox = true;
                }
            }
        }

        void imageChecker_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

            if (!this.ReadyIcon && this.user != null && this.user.profile_image_url != null)
            {
                var tmp = ImageCache.getCache(this.user.profile_image_url);
                if (tmp != null)
                {
                    this.icon = tmp;
                    ReadyIcon = true;
                }
            }
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
        /// 設定されているユーザ
        /// </summary>
        public TwitterUserStatus UserStatus
        {
            get { return this.user; }
        }


        /// <summary>
        /// タブのアニメーションを開始させる
        /// </summary>
        /// <param name="BeforeControl">前表示してたコントロール</param>
        /// <param name="AfterControl">いま表示してるコントロール</param>
        /// <param name="tabLeftRight">左にタブが移動するか、右にいくか</param>
        public void StartTabChangeAnimation(Bitmap BeforeControl, Bitmap AfterControl, bool tabLeftRight, bool tabVertical)
        {
            if (BeforeControl != null && AfterControl != null)
                this.tabchange_anime.StartAnimation(new object[] { BeforeControl, AfterControl, tabLeftRight, tabVertical });
        }

        /// <summary>
        /// ユーザー情報を入れ替える
        /// </summary>
        /// <param name="user"></param>
        public void ChangeUserStatus(TwitterUserStatus user)
        {
            if (user == null)
                return;
            this.user = user;
            this.ReadyIcon = false;
            var tmp = ImageCache.AutoCache(this.user.profile_image_url, true);
            if (tmp != null)
            {
                this.icon = (Bitmap)tmp.Clone();
                ReadyIcon = true;
            }

            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate()
                {
                    this.Invalidate();
                    this.Update();
                });
            }
            else
            {
                this.Invalidate();
                this.Update();
            }
        }

        private void UserStatusContol_Paint(object sender, PaintEventArgs e)
        {
            //  切り替えアニメ
            if (this.tabchange_anime.Enable)
            {
                this.tabchange_anime.Draw(e.Graphics, this.Width, e.ClipRectangle, null, null);
                return;
            }
            e.Graphics.FillRectangle(Brushes.LightGray, e.ClipRectangle);
            if (this.user != null)
            {
                if (user.description == null || user.name == null || user.screen_name == null)
                    return;

                clickCells.initialize();
                cells.getLayout(this.Width - 10, this.icon, user);

                //  描画開始
                e.Graphics.DrawImage(this.icon, cells.icon.Rect);
                e.Graphics.DrawString(user.name, Setting.Fonts.NameFont, Brushes.Black, cells.name.Position);
                e.Graphics.DrawString("@" + user.screen_name, Setting.Fonts.TweetFont, Brushes.Black, cells.screen_name.Position);
                e.Graphics.DrawLine(Pens.Gray, new Point(0, cells.screen_name.Rect.Bottom + 5), new Point(this.Width, cells.screen_name.Rect.Bottom + 5));
                e.Graphics.DrawString("ツイート数:  " + user.statuses_count + "", Setting.Fonts.TweetUnderLineFont, Setting.Colors.LinkColor, cells.tweet_count.Position);
                clickCells.SetClickLink(cells.tweet_count.Rect, new TwitterEntitiesPosition(user.screen_name, "usertimeline"));
                e.Graphics.DrawString("フォロー数:  " + user.friends_count, Setting.Fonts.TweetUnderLineFont, Setting.Colors.LinkColor, cells.following_count.Position);
                clickCells.SetClickLink(cells.following_count.Rect, new TwitterEntitiesPosition("https://twitter.com/" + user.screen_name + "/following", "url"));
                e.Graphics.DrawString("フォロワー数:" + user.followers_count, Setting.Fonts.TweetUnderLineFont, Setting.Colors.LinkColor, cells.follower_count.Position);
                clickCells.SetClickLink(cells.follower_count.Rect, new TwitterEntitiesPosition("https://twitter.com/" + user.screen_name + "/followers", "url"));
                e.Graphics.DrawString("ふぁぼ数:    " + user.favourites_count, Setting.Fonts.TweetUnderLineFont, Setting.Colors.LinkColor, cells.favorites_count.Position);
                clickCells.SetClickLink(cells.favorites_count.Rect, new TwitterEntitiesPosition(user.screen_name, "userfavoritetimeline"));
                e.Graphics.DrawString("リスト数:    " + user.listed_count, Setting.Fonts.TweetFont, Brushes.Black, cells.listed_count.Position);

                e.Graphics.DrawString("このユーザについて", Setting.Fonts.TweetUnderLineFont, Setting.Colors.LinkColor, cells.AboutUser.Position);
                clickCells.SetClickLink(cells.AboutUser.Rect, new TwitterEntitiesPosition(user.screen_name, "aboutUser"));

                int strX = cells.bio.Position.X, strY = cells.bio.Position.Y;
                int num = 0;
                Brush cl;
                foreach (char t in user.description)
                {
                    var one_size = DrawTextUtil.GetDrawTextSize(t.ToString(), Setting.Fonts.TweetFont, this.Width, true);

                    cl = Setting.Colors.TweetColor;
                    bool isLink = false;
                    TwitterEntitiesPosition entities_pos = TwitterEntitiesUtil.getEntitiesPosition(user.entities, num);
                    if (entities_pos != null)
                    {
                        isLink = true;
                        cl = Setting.Colors.LinkColor;
                        clickCells.SetClickLink(new Rectangle(new Point(strX, strY), one_size), entities_pos);
                    }

                    if (selectControl.isSelecting)
                    {
                        if (selectControl.selTextPosition[0] <= num && selectControl.selTextPosition[1] >= num)
                        {
                            e.Graphics.FillRectangle(Brushes.BlueViolet, new Rectangle(strX + 1, strY + 1, one_size.Width + 1, one_size.Height));
                            cl = Brushes.White;
                        }
                    }

                    e.Graphics.DrawString(t.ToString(), (isLink ? Setting.Fonts.TweetUnderLineFont : Setting.Fonts.TweetFont), cl, new PointF(strX, (float)strY));
                    strX += one_size.Width + Setting.Timeline.TweetPadding;
                    if (strX + one_size.Width >= this.Width || t == '\n')
                    {
                        //
                        strX = cells.bio.Position.X;
                        strY += one_size.Height;
                    }

                    num++;
                }

                e.Graphics.DrawString("Twitter開始時期:" + user.created_at.ToLongDateString(), Setting.Fonts.TweetFont, Brushes.Black, new Point(cells.created_at.Position.X, this.Height - cells.created_at.Size.Height));
            }
        }

        private void UserStatusContol_MouseMove(object sender, MouseEventArgs e)
        {
            if (clickCells.getClickLink(e.Location) != null)
            {
                this.Cursor = Cursors.Hand;
            }
            else
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void UserStatusContol_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ClickCellsData data;
                if ((data = clickCells.getClickLink(e.Location)) != null)
                {
                    if (data.type == "aboutUser")
                    {
                        this.selUserContextMenu.ShowMenu(System.Windows.Forms.Cursor.Position, (string)data.source);
                    }
                    else
                    {
                        if (TabControlOperatingHandler != null)
                            TabControlOperatingHandler.Invoke(ActionControl.ConvertType(data.type), data.source);
                    }
                }
            }
        }

        private void UserStatusControl_KeyDown(object sender, KeyEventArgs e)
        {

        }

        /// <summary>
        /// タイムラインが復帰されるときに使われます
        /// </summary>
        public void Resume()
        {
            this.imageChecker.Start();
            if (this.loadingTimer != null)
                this.loadingTimer.Start();
            this.anicon.Start();
            this.Focus();
            this.isSuspended = false;
        }

        /// <summary>
        /// タイムラインがサスペンドモードに切り替わるときに使われます
        /// </summary>
        public void Suspend()
        {
            if (!this.isSuspended)
            {
                this.imageChecker.Stop();
                if (this.loadingTimer != null)
                    this.loadingTimer.Stop();
                this.anicon.Stop();
                this.isSuspended = true;
            }
        }

        /// <summary>
        /// コントロールを撮影
        /// </summary>
        /// <returns></returns>
        public Bitmap CaptureControl()
        {
            Bitmap bmp = new Bitmap(this.Width, this.Height);
            this.DrawToBitmap(bmp, this.ClientRectangle);
            return bmp;
        }

        /// <summary>
        /// ウィンドウを再描画します
        /// falseが返されたときは、現在描画ができない状態なので、再度試してください
        /// </summary>
        private bool RedrawControl()
        {
            if (this.InvokeRequired)
            {
                if (!this.IsDisposed)
                {
                    this.Invoke((MethodInvoker)delegate()
                    {
                        if (!this.IsDisposed)
                        {
                            this.Invalidate();
                            this.Update();
                        }

                    });
                }
            }
            else
            {
                if (!this.IsDisposed)
                {
                    this.Invalidate();
                    this.Update();
                }
            }
            return true;
        }
    }
}
