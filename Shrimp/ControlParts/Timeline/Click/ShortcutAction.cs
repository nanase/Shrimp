using System;
using System.Runtime.Serialization;

namespace Shrimp.ControlParts.Timeline.Click
{
    [DataContract]
    public enum Actions
    {
        None,
        Reply,
        Retweet,
        Fav,
        ShowUserTimeline,
        ShowUserInformation,
        ShowUserFavoriteTimeline,
        ShowUserConversation,
        SetFocusInput
    }

    [DataContract]
    public enum UserActions
    {
        MouseDoubleClick,
        KeyboardShortcut
    }

    [DataContract]
    public class ShortcutAction : ICloneable
    {
        [DataMember]
        private int ActionsInt;
        public Actions action;
        [DataMember]
        private int UserActionsInt;
        public UserActions user_action;
        [DataMember]
        public ShortcutKey shortcut_key;

        public ShortcutAction(Actions action, UserActions user_action, ShortcutKey shortcut_key)
        {
            this.action = action;
            this.user_action = user_action;
            this.shortcut_key = (ShortcutKey)shortcut_key.Clone();
        }

        public void save()
        {
            //	保存する際、Enumをintに変換する
            this.ActionsInt = (int)action;
            this.UserActionsInt = (int)user_action;
            if (this.shortcut_key != null)
                this.shortcut_key.save();
        }

        public void load()
        {
            //	読み込む際、Enumへ変換する
            this.action = (Actions)this.ActionsInt;
            this.user_action = (UserActions)this.UserActionsInt;
            if (this.shortcut_key != null)
                this.shortcut_key.load();
        }
        public string ActionsToString()
        {
            if (this.action == Actions.Fav)
                return "お気に入りに登録";
            if (this.action == Actions.None)
                return "なし";
            if (this.action == Actions.Reply)
                return "リプライ";
            if (this.action == Actions.Retweet)
                return "リツイート";
            if (this.action == Actions.ShowUserFavoriteTimeline)
                return "ユーザのお気に入りタイムラインを開く";
            if (this.action == Actions.ShowUserInformation)
                return "ユーザの情報を開く";
            if (this.action == Actions.ShowUserTimeline)
                return "ユーザのタイムラインを開く";
            if (this.action == Actions.SetFocusInput)
                return "入力欄にフォーカスを移動する";
            return "Unknown";
        }

        public string UserActionToString()
        {
            if (this.user_action == UserActions.KeyboardShortcut)
                return "キーボード";
            if (this.user_action == UserActions.MouseDoubleClick)
                return "ダブルクリック";
            return "Unknown";
        }

        public object Clone()
        {
            var dest = new ShortcutAction(this.action, this.user_action, (ShortcutKey)this.shortcut_key.Clone());
            return dest;
        }
    }
}
