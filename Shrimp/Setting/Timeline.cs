using System.Collections.Generic;
using System.Windows.Forms;

namespace Shrimp.Setting
{
    /// <summary>
    /// タブの切り替えアニメーション
    /// </summary>
    public enum TabAnimation
    {
        None = 0,
        Move,
        Fade
    };
    /// <summary>
    /// Timeline Setting
    /// タイムラインの設定
    /// </summary>
    class Timeline
    {
        #region 定義

        private static int _refTimeline = 500;

        #endregion

        #region コンストラクタ

        /// <summary>
        /// 静的コンストラクタ
        /// </summary>
        public static void initialize()
        {
            isEnableSourceLink = false;
            isEnableRetweetLink = true;
            isEnableTimeLink = false;
            SelectTabWhenCreatedTab = true;
            isHoverSelectMode = false;
            SavedTimelineTweetNum = 3000;
            TabChangeAnimation = TabAnimation.None;
            isEnableInsertAnimation = true;
            isEnableNotifyAnimation = true;
            GlobalMuteString = "";
            ShrimpTabAlignment = TabAlignment.Top;
            isNotifyBold = true;
            isRetweetBold = false;
            isReplyBold = false;
            isConfirmFav = true;
            isConfirmRT = true;
        }

        public static void load(Dictionary<string, object> obj)
        {
            if (obj == null)
                return;
            if (obj.ContainsKey("refTimeline"))
                refTimeline = (int)obj["refTimeline"];
            if (obj.ContainsKey("isEnableSourceLink"))
                isEnableSourceLink = (bool)obj["isEnableSourceLink"];
            if (obj.ContainsKey("isEnableTimeLink"))
                isEnableTimeLink = (bool)obj["isEnableTimeLink"];
            if (obj.ContainsKey("isEnableRetweetLink"))
                isEnableRetweetLink = (bool)obj["isEnableRetweetLink"];
            if (obj.ContainsKey("isLineMode"))
                isLineMode = (bool)obj["isLineMode"];
            if (obj.ContainsKey("isShowUserInformation"))
                isShowUserInformation = (bool)obj["isShowUserInformation"];
            if (obj.ContainsKey("isConfirmFav"))
                isConfirmFav = (bool)obj["isConfirmFav"];
            if (obj.ContainsKey("isConfirmRT"))
                isConfirmRT = (bool)obj["isConfirmRT"];
            if (obj.ContainsKey("SelectTabWhenCreatedTab"))
                SelectTabWhenCreatedTab = (bool)obj["SelectTabWhenCreatedTab"];
            if (obj.ContainsKey("isHoverSelectMode"))
                isHoverSelectMode = (bool)obj["isHoverSelectMode"];
            if (obj.ContainsKey("SavedTimelineTweetNum"))
                SavedTimelineTweetNum = (int)obj["SavedTimelineTweetNum"];
            if (obj.ContainsKey("TabChangeAnimation"))
                TabChangeAnimation = (TabAnimation)((int)obj["TabChangeAnimation"]);
            if (obj.ContainsKey("isEnableInsertAnimation"))
                isEnableInsertAnimation = (bool)obj["isEnableInsertAnimation"];
            if (obj.ContainsKey("isEnableNotifyAnimation"))
                isEnableNotifyAnimation = (bool)obj["isEnableNotifyAnimation"];
            if (obj.ContainsKey("GlobalMuteString"))
                GlobalMuteString = (string)((string)obj["GlobalMuteString"]).Clone();
            if (obj.ContainsKey("ShrimpTabAlignment"))
                ShrimpTabAlignment = (TabAlignment)obj["ShrimpTabAlignment"];
            if (obj.ContainsKey("isNotifyBold"))
                isNotifyBold = (bool)obj["isNotifyBold"];
            if (obj.ContainsKey("isRetweetBold"))
                isRetweetBold = (bool)obj["isRetweetBold"];
            if (obj.ContainsKey("isReplyBold"))
                isReplyBold = (bool)obj["isReplyBold"];
        }

        public static Dictionary<string, object> save()
        {
            var dest = new Dictionary<string, object>();
            dest["refTimeline"] = refTimeline;
            dest["isEnableSourceLink"] = isEnableSourceLink;
            dest["isEnableTimeLink"] = isEnableTimeLink;
            dest["isEnableRetweetLink"] = isEnableRetweetLink;
            dest["isLineMode"] = isLineMode;
            dest["isShowUserInformation"] = isShowUserInformation;
            dest["isConfirmFav"] = isConfirmFav;
            dest["isConfirmRT"] = isConfirmRT;
            dest["SelectTabWhenCreatedTab"] = SelectTabWhenCreatedTab;
            dest["isHoverSelectMode"] = isHoverSelectMode;
            dest["SavedTimelineTweetNum"] = SavedTimelineTweetNum;
            dest["TabChangeAnimation"] = (int)TabChangeAnimation;
            dest["isEnableInsertAnimation"] = isEnableInsertAnimation;
            dest["isEnableNotifyAnimation"] = isEnableNotifyAnimation;
            dest["GlobalMuteString"] = GlobalMuteString;
            dest["ShrimpTabAlignment"] = (int)ShrimpTabAlignment;
            dest["isNotifyBold"] = isNotifyBold;
            dest["isRetweetBold"] = isRetweetBold;
            dest["isReplyBold"] = isReplyBold;
            return dest;
        }

        #endregion

        /// <summary>
        /// タイムラインへの反映までの時間
        /// </summary>
        public static int refTimeline
        {
            get { return (_refTimeline <= 0 ? 1 : _refTimeline); }
            set { _refTimeline = value; }
        }

        /// <summary>
        /// 挿入アニメーションを有効化するかどうか
        /// </summary>
        public static bool isEnableInsertAnimation
        {
            get;
            set;
        }


        /// <summary>
        /// 通知アニメーションを有効化するかどうか
        /// </summary>
        public static bool isEnableNotifyAnimation
        {
            get;
            set;
        }

        /// <summary>
        /// 通知を太文字にする
        /// </summary>
        public static bool isNotifyBold
        {
            get;
            set;
        }

        /// <summary>
        /// リツイートのとき太文字にする
        /// </summary>
        public static bool isRetweetBold
        {
            get;
            set;
        }

        /// <summary>
        /// リプライのとき太文字にする
        /// </summary>
        public static bool isReplyBold
        {
            get;
            set;
        }

        /// <summary>
        /// タイムラインに保持するツイート数
        /// </summary>
        public static int SavedTimelineTweetNum
        {
            get;
            set;
        }

        /// <summary>
        /// グローバルミュート文字列
        /// </summary>
        public static string GlobalMuteString
        {
            get;
            set;
        }

        /// <summary>
        /// viaをクリックしたとき、反応するかどうか
        /// </summary>
        public static bool isEnableSourceLink
        {
            get;
            set;
        }

        /// <summary>
        /// timeをクリックしたとき、反応するかどうか
        /// </summary>
        public static bool isEnableTimeLink
        {
            get;
            set;
        }

        /// <summary>
        /// RTをクリックしたとき、反応するかどうか
        /// </summary>
        public static bool isEnableRetweetLink
        {
            get;
            set;
        }

        /// <summary>
        /// タブの切り替えアニメーション（試験実装）
        /// </summary>
        public static TabAnimation TabChangeAnimation
        {
            get;
            set;
        }

        /// <summary>
        /// タブの向き
        /// </summary>
        public static TabAlignment ShrimpTabAlignment
        {
            get;
            set;
        }

        /// <summary>
        /// 単行モードかどうか
        /// </summary>
        public static bool isLineMode
        {
            get;
            set;
        }

        /// <summary>
        /// 単行モード時のみ有効。ホバーセレクトが有効かどうか
        /// </summary>
        public static bool isHoverSelectMode
        {
            get;
            set;
        }

        /// <summary>
        /// ユーザーインフォを表示するかどうか
        /// </summary>
        public static bool isShowUserInformation
        {
            get;
            set;
        }

        /// <summary>
        /// インライン画像の横幅
        /// </summary>
        public static int ImageWidth
        {
            get { return 320; }
        }

        /// <summary>
        /// インライン画像の縦幅
        /// </summary>
        public static int ImageHeight
        {
            get { return 240; }
        }

        /// <summary>
        /// ボタンのパディング
        /// </summary>
        public static int ButtonPadding
        {
            get { return 25; }
        }

        /// <summary>
        /// アイコンサイズ
        /// </summary>
        public static int IconSize
        {
            get { return 48; }
        }

        /// <summary>
        /// 文字の行間
        /// </summary>
        public static int TweetPadding
        {
            get { return 1; }
        }

        /// <summary>
        /// リツイートしましたのアイコンのサイズ
        /// </summary>
        public static int RetweetMarkSize
        {
            get { return 16; }
        }

        /// <summary>
        /// ふぁぼの確認画面
        /// </summary>
        public static bool isConfirmFav
        {
            get;
            set;
        }

        /// <summary>
        /// RTの確認画面
        /// </summary>
        public static bool isConfirmRT
        {
            get;
            set;
        }

        /// <summary>
        /// タブ作成時、そっちに選択するかどうか
        /// </summary>
        public static bool SelectTabWhenCreatedTab
        {
            get;
            set;
        }
    }
}
