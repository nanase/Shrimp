using System;
using System.Drawing;
using System.Windows.Forms;
using Shrimp.ControlParts.Tabs;
using Shrimp.ControlParts.Timeline;
using Shrimp.Twitter.Status;

namespace Shrimp.ControlParts.User
{
    public partial class UserStatusControl : UserControl, IControl
    {
        private UserStatusControlBase userBase;
        private TimelineControl userdbtimeline, usertimeline, userfavoritetimeline, userconversation;

        private TimelineControl.OnRequiredAccountInfoDeleagate OnRequiredAccountInfo;
        private TimelineControl.OnRequiredShrimpData OnRequiredShrimpData;
        private TimelineControl.OnUseTwitterAPIDelegate OnUserTwitterAPI;
        private TimelineControl.OnCreatedReplyDataDelegate OnCreatedReplyData;
        private TimelineControl.TabControlOperationgDelegate TabControlOperationHandler;
        private TimelineControl.OnReloadShrimp OnReloadShrimp;
        private Shrimp.OnCreatedTweetDelegate OnCreatedTweet;
        private TabControls.FlashWindowDelegate OnFlashWindowDelegate;
        private Shrimp.OnUserStatusControlAPIDelegate OnUserStatusControlAPI;
        private bool isUser, isTimeline, isFavoriteTimeline, isConversationTimeline;

        public UserStatusControl(TwitterUserStatus user, TimelineControl.TabControlOperationgDelegate TabControlOperatingHandler,
            TimelineControl.OnUseTwitterAPIDelegate OnUseTwitterAPIHandler, Shrimp.OnUserStatusControlAPIDelegate OnUserStatusControlAPI)
        {
            InitializeComponent();
            this.userBase = new UserStatusControlBase(user, TabControlOperatingHandler, OnUseTwitterAPIHandler);
            this.userBase.Dock = DockStyle.Fill;
            this.OnUserStatusControlAPI = OnUserStatusControlAPI;


            this.userdbtimeline = new TimelineControl ();
            this.userdbtimeline.Dock = DockStyle.Fill;

            this.UserPage.Controls.Add ( this.userdbtimeline );
            this.UserPage.Tag = this.userdbtimeline;


            this.usertimeline = new TimelineControl();
            this.usertimeline.Dock = DockStyle.Fill;

            this.UserTimelinePage.Controls.Add(this.usertimeline);
            this.UserTimelinePage.Tag = this.usertimeline;

            this.userfavoritetimeline = new TimelineControl();
            this.userfavoritetimeline.Dock = DockStyle.Fill;

            this.UserFavoritePage.Controls.Add(this.userfavoritetimeline);
            this.UserFavoritePage.Tag = this.userfavoritetimeline;

            this.userconversation = new TimelineControl();
            this.userconversation.Dock = DockStyle.Fill;

            this.ConversationPage.Controls.Add(this.userconversation);
            this.ConversationPage.Tag = this.userconversation;

            this.MainContainer.Panel1.Controls.Add(this.userBase);
        }

        public void SetTimelineControlHander(TimelineControl obj)
        {
            if (OnUserTwitterAPI != null)
                obj.OnUseTwitterAPI += OnUserTwitterAPI;
            if (OnRequiredAccountInfo != null)
                obj.OnRequiredAccountInfo += OnRequiredAccountInfo;
            if (OnCreatedReplyData != null)
                obj.OnCreatedReplyData += OnCreatedReplyData;
            if (TabControlOperationHandler != null)
                obj.TabControlOperatingHandler += TabControlOperationHandler;
        }

        public void SetHandlers(TimelineControl.OnChangedTweetHandler OnChangedTweetHandler, TimelineControl.OnUseTwitterAPIDelegate OnUserTwitterAPI,
                     TimelineControl.OnChangedTweetDelayPercentageHandler OnChangedTweetDelay, TimelineControl.OnRequiredAccountInfoDeleagate OnRequiredAccountInfo,
                     TimelineControl.OnCreatedReplyDataDelegate OnCreatedReplyData, TimelineControl.TabControlOperationgDelegate TabControlOperationHandler,
    TimelineControl.OnRequiredShrimpData OnRequiredShrimpData, TimelineControl.OnReloadShrimp OnReloadShrimp, Shrimp.OnCreatedTweetDelegate OnCreatedTweet,
    TabControls.FlashWindowDelegate OnFlashWindowDelegate, Shrimp.OnUserStatusControlAPIDelegate OnUserStatusControlAPI)
        {
            //this.OnChangedTweetDelay += OnChangedTweetDelay;
            //this.OnChangedTweetHandler += OnChangedTweetHandler;
            this.OnUserTwitterAPI += OnUserTwitterAPI;
            this.OnRequiredAccountInfo += OnRequiredAccountInfo;
            this.OnCreatedReplyData += OnCreatedReplyData;
            this.TabControlOperationHandler += TabControlOperationHandler;
            this.OnRequiredShrimpData += OnRequiredShrimpData;
            this.OnReloadShrimp += OnReloadShrimp;
            this.OnCreatedTweet += OnCreatedTweet;
            this.OnFlashWindowDelegate += OnFlashWindowDelegate;
            this.OnUserStatusControlAPI += OnUserStatusControlAPI;

            this.SetTimelineControlHander ( this.userdbtimeline );
            this.SetTimelineControlHander(this.usertimeline);
            this.SetTimelineControlHander(this.userfavoritetimeline);
            this.SetTimelineControlHander(this.userconversation);
        }


        /// <summary>
        /// タイムラインが復帰されるときに使われます
        /// </summary>
        public void Resume()
        {
            this.userBase.Resume();
        }

        /// <summary>
        /// タイムラインがサスペンドモードに切り替わるときに使われます
        /// </summary>
        public void Suspend()
        {
            this.userBase.Suspend();
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

        public bool isLoadingFinished
        {
            get { return this.userBase.isLoadingFinished; }
            set { this.userBase.isLoadingFinished = value; }
        }

        public void ChangeUserStatus(TwitterUserStatus user)
        {
            this.userBase.ChangeUserStatus(user);
            this.userdbtimeline.initialize ();
            this.usertimeline.initialize();

            this.userfavoritetimeline.initialize();

            this.userconversation.initialize();
            this.BeginInvoke((MethodInvoker)delegate()
            {
                this.UserInfoControl.SelectedIndex = 0;
                this.UserInfoControl_SelectedIndexChanged ( this.UserInfoControl, null );
            });
        }

        //
        public TwitterUserStatus UserStatus
        {
            get
            {
                return this.userBase.UserStatus;
            }
        }

        public void StartTabChangeAnimation(Bitmap BeforeControl, Bitmap AfterControl, bool tabLeftRight, bool tabVertical)
        {
            this.userBase.StartTabChangeAnimation(BeforeControl, AfterControl, tabLeftRight, tabVertical);
        }

        /// <summary>
        /// タブの切り替え
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserInfoControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl obj = sender as TabControl;
            TabPage tab = obj.SelectedTab;

            if ( tab.Name == "UserPage" )
            {
                if ( OnUserStatusControlAPI != null && this.UserStatus != null && !isUser )
                    OnUserStatusControlAPI ( (TimelineControl)tab.Tag, ActionType.UserDBTimeline, this.UserStatus.screen_name, this.UserStatus.id );

                this.isUser = true;
                this.userdbtimeline.Resume ();
                this.usertimeline.Suspend ();
                this.userfavoritetimeline.Suspend ();
                this.userconversation.Suspend ();
                ( (TimelineControl)tab.Tag ).Focus ();
            }

            if (tab.Name == "UserTimelinePage")
            {
                if (OnUserStatusControlAPI != null && this.UserStatus != null && !isTimeline)
                    OnUserStatusControlAPI((TimelineControl)tab.Tag, ActionType.UserTimeline, this.UserStatus.screen_name, this.UserStatus.id);
                this.isTimeline = true;
                this.userdbtimeline.Suspend ();
                this.usertimeline.Resume();
                this.userfavoritetimeline.Suspend();
                this.userconversation.Suspend();
                ((TimelineControl)tab.Tag).Focus();
            }
            if (tab.Name == "UserFavoritePage")
            {
                if (OnUserStatusControlAPI != null && this.UserStatus != null && !isFavoriteTimeline)
                    OnUserStatusControlAPI ( (TimelineControl)tab.Tag, ActionType.UserFavoriteTimeline, this.UserStatus.screen_name, this.UserStatus.id );
                this.isFavoriteTimeline = true;
                this.userdbtimeline.Suspend ();
                this.usertimeline.Suspend();
                this.userfavoritetimeline.Resume();
                this.userconversation.Suspend();
                ((TimelineControl)tab.Tag).Focus();
            }
            if (tab.Name == "ConversationPage")
            {
                if (OnUserStatusControlAPI != null && this.UserStatus != null && !isConversationTimeline)
                    OnUserStatusControlAPI ( (TimelineControl)tab.Tag, ActionType.UserConversation, this.UserStatus.screen_name, this.UserStatus.id );

                this.isConversationTimeline = true;
                this.userdbtimeline.Suspend ();
                this.usertimeline.Suspend();
                this.userfavoritetimeline.Suspend();
                this.userconversation.Resume();
                ((TimelineControl)tab.Tag).Focus();
            }
        }

        private void UserInfoControl_KeyDown ( object sender, KeyEventArgs e )
        {
            TabControl obj = sender as TabControl;
            TabPage tab = obj.SelectedTab;
            if ( tab.Tag != null )
            {
                ( (TimelineControl)tab.Tag ).PreviewOnKeyDown ( e );
            }
        }
    }
}
