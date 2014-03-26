using System;
using System.Diagnostics;
using System.Windows.Forms;
using Shrimp.ControlParts.ContextMenus.TextMenu;
using Shrimp.ControlParts.Timeline;
using Shrimp.ControlParts.Timeline.Click;
using Shrimp.Twitter;
using Shrimp.Twitter.Status;

namespace Shrimp.ControlParts
{
    /// <summary>
    /// ActionControlで使うenum
    /// </summary>
    public enum ActionType
    {
        None,
        URL,
        Media,
        Mention,
        UserDBTimeline,
        UserFavoriteTimeline,
        UserTimeline,
        UserConversation,
        Notify,
        Search,
        Reply,
        Favorite,
        Retweet,
        Follow,
        Block,
        Spam,
        Focus,
        RegistBookMark
    };

    class ActionControl
    {
        private static TimelineControl.TabControlOperationgDelegate TabControlOperatingHandler;
        private static TimelineControl.OnUseTwitterAPIDelegate OnUseTwitterAPI;
        private static TimelineControl.OnRequiredAccountInfoDeleagate OnRequiredAccountInfo;

        public static void initialize(TimelineControl.TabControlOperationgDelegate _TabControlOperatingHandler,
            TimelineControl.OnUseTwitterAPIDelegate _OnUseTwitterAPI, TimelineControl.OnRequiredAccountInfoDeleagate _OnRequiredAccountInfo)
        {
            TabControlOperatingHandler = _TabControlOperatingHandler;
            OnUseTwitterAPI = _OnUseTwitterAPI;
            OnRequiredAccountInfo = _OnRequiredAccountInfo;
        }

        public static void OnShortcutAction(Actions action, decimal id, string screen_name, SelUserContextMenu selUserContextMenu = null,
           TimelineControl.ReplyTweetDelegate ReplyTweet = null, TimelineControl.SearchTweetDelegate tweetSearchAction = null, TwitterInfo ControlAccountID = null,
            TimelineControl.OnNotifyClickedDelegate OnNotifyClicked = null)
        {
            if (action == Actions.Fav)
                DoAction(ActionType.Favorite, id, selUserContextMenu, ReplyTweet, tweetSearchAction, ControlAccountID, OnNotifyClicked);
            if (action == Actions.Reply)
                DoAction(ActionType.Reply, id, selUserContextMenu, ReplyTweet, tweetSearchAction, ControlAccountID, OnNotifyClicked);
            if (action == Actions.Retweet)
                DoAction(ActionType.Retweet, id, selUserContextMenu, ReplyTweet, tweetSearchAction, ControlAccountID, OnNotifyClicked);
            if (action == Actions.ShowUserFavoriteTimeline)
                DoAction(ActionType.UserFavoriteTimeline, screen_name, selUserContextMenu, ReplyTweet, tweetSearchAction, ControlAccountID, OnNotifyClicked);
            if (action == Actions.ShowUserTimeline)
                DoAction(ActionType.UserTimeline, screen_name, selUserContextMenu, ReplyTweet, tweetSearchAction, ControlAccountID, OnNotifyClicked);
            if (action == Actions.ShowUserInformation)
                DoAction(ActionType.Mention, screen_name, selUserContextMenu, ReplyTweet, tweetSearchAction, ControlAccountID, OnNotifyClicked);
            if (action == Actions.SetFocusInput)
                DoAction(ActionType.Focus, null, selUserContextMenu, ReplyTweet, tweetSearchAction, ControlAccountID, OnNotifyClicked);
        }

        /// <summary>
        /// アクションを行う
        /// </summary>
        /// <param name="type">アクションの種類</param>
        /// <param name="source">ソース</param>
        /// <param name="ControlAccountID">実行もとアカウント（nullの場合、現在選択されているアカウントが使われます)</param>
        public static void DoAction(ActionType type, object source, SelUserContextMenu selUserContextMenu = null,
           TimelineControl.ReplyTweetDelegate ReplyTweet = null, TimelineControl.SearchTweetDelegate tweetSearchAction = null, TwitterInfo ControlAccountID = null,
            TimelineControl.OnNotifyClickedDelegate OnNotifyClicked = null)
        {
            if (type == ActionType.URL || type == ActionType.Media)
            {
                Process.Start((string)source);
            }

            if (type == ActionType.Mention)
            {
                if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
                {
                    //  Shift押されている。
                    selUserContextMenu.ShowMenu(System.Windows.Forms.Cursor.Position, ((string)source).TrimStart('@'));
                }
                else
                {
                    if (TabControlOperatingHandler != null)
                        TabControlOperatingHandler.Invoke(type, ((string)source).TrimStart('@'));
                }
            }

            if (type == ActionType.UserFavoriteTimeline || type == ActionType.UserTimeline)
            {
                if (TabControlOperatingHandler != null)
                    TabControlOperatingHandler.Invoke(type, ((string)source).TrimStart('@'));
            }

            if (type == ActionType.Notify)
            {
                decimal repID = Decimal.Parse((string)source);
                if (OnNotifyClicked != null)
                    OnNotifyClicked.Invoke(repID);
            }

            if (type == ActionType.Search)
            {
                if (TabControlOperatingHandler != null)
                    TabControlOperatingHandler.Invoke(type, source + " -rt");
            }

            if (type == ActionType.Reply)
            {
                decimal id = 0;
                if (source is decimal)
                    id = (decimal)source;
                else
                    id = Decimal.Parse((string)source);
                ReplyTweet.Invoke(id, false);
            }

            if (type == ActionType.Favorite || type == ActionType.Retweet)
            {
                if (OnUseTwitterAPI == null || OnRequiredAccountInfo == null)
                    return;
                decimal id = 0;
                if (source is decimal)
                    id = (decimal)source;
                else
                    id = Decimal.Parse((string)source);

                TwitterStatus tweet = null;
                if (tweetSearchAction != null)
                {
                    tweet = tweetSearchAction.Invoke(id);
                }
                if (tweet == null)
                    return;

                TwitterInfo selectedInfo = null;
                if (ControlAccountID == null)
                    selectedInfo = OnRequiredAccountInfo.Invoke().SelectedAccount;
                else
                    selectedInfo = ControlAccountID;

                if (type == ActionType.Favorite)
                {
                    if (Setting.Timeline.isConfirmFav)
                    {
                        if (MessageBox.Show("このツイートをお気に入りに追加します。よろしいですか？\n\n"
                            + "@" + tweet.DynamicTweet.user.screen_name + " / " + tweet.DynamicTweet.user.name + "\n"
                            + "==============================\n " + tweet.DynamicTweet.text + "", "確認", MessageBoxButtons.YesNo) == DialogResult.No)
                            return;
                    }
                    OnUseTwitterAPI.Invoke(null, new object[] { (tweet.favorited ? "unfav" : "fav"), id, selectedInfo });
                }

                if (type == ActionType.Retweet)
                {
                    if (Setting.Timeline.isConfirmRT)
                    {
                        if (MessageBox.Show("このツイートをリツイートします。よろしいですか？\n\n"
                            + "@" + tweet.DynamicTweet.user.screen_name + " / " + tweet.DynamicTweet.user.name + "\n"
                            + "==============================\n " + tweet.DynamicTweet.text + "", "確認", MessageBoxButtons.YesNo) == DialogResult.No)
                            return;
                    }
                    OnUseTwitterAPI.Invoke(null, new object[] { "retweet", id, selectedInfo });
                }
            }

            if (type == ActionType.Follow)
            {
                TwitterInfo selectedInfo = null;
                if (ControlAccountID == null)
                    selectedInfo = OnRequiredAccountInfo.Invoke().SelectedAccount;
                else
                    selectedInfo = ControlAccountID;

                OnUseTwitterAPI.Invoke(null, new object[] { "follow", source, selectedInfo });
            }

            if (type == ActionType.Block)
            {
                TwitterInfo selectedInfo = null;
                if (ControlAccountID == null)
                    selectedInfo = OnRequiredAccountInfo.Invoke().SelectedAccount;
                else
                    selectedInfo = ControlAccountID;
                OnUseTwitterAPI.Invoke(null, new object[] { "block", source, selectedInfo });
            }

            if (type == ActionType.Spam)
            {
                TwitterInfo selectedInfo = null;
                if (ControlAccountID == null)
                    selectedInfo = OnRequiredAccountInfo.Invoke().SelectedAccount;
                else
                    selectedInfo = ControlAccountID;
                OnUseTwitterAPI.Invoke(null, new object[] { "spam", source, selectedInfo });
            }

            if (type == ActionType.Focus)
            {
                if (TabControlOperatingHandler != null)
                    TabControlOperatingHandler.Invoke(type, "");
            }
        }

        public static ActionType ConvertType(string type)
        {
            if (type == "url")
                return ActionType.URL;
            if (type == "usertimeline")
                return ActionType.UserTimeline;
            if (type == "userfavoritetimeline")
                return ActionType.UserFavoriteTimeline;
            if (type == "mention")
                return ActionType.Mention;
            if (type == "hashtags")
                return ActionType.Search;
            if (type == "media")
                return ActionType.Media;
            if (type == "favButton")
                return ActionType.Favorite;
            if (type == "retweetButton")
                return ActionType.Retweet;
            if (type == "replyButton")
                return ActionType.Reply;
            if (type == "notify")
                return ActionType.Notify;
            return ActionType.None;
        }
    }
}
