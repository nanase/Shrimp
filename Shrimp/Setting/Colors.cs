using System.Collections.Generic;
using System.Drawing;
using Shrimp.Setting.ObjectXML;

namespace Shrimp.Setting
{
    /// <summary>
    /// Color Class
    /// 色設定クラス
    /// </summary>
    class Colors
    {
        #region 定義
        private static byte _alpha = 255;
        #endregion

        #region コンストラクタ

        internal static void initialize()
        {
            //  初期設定
            BackgroundColor = new SolidBrush(Color.FromArgb(255, 202, 202, 202));
            ReplyBackgroundColor = new SolidBrush(Color.FromArgb(255, 255, 176, 176));
            RetweetBackgroundColor = new SolidBrush(Color.FromArgb(255, 183, 226, 253));
            OwnTweetBackgroundColor = new SolidBrush(Color.FromArgb(255, 0, 217, 108));
            NotifyTweetBackgroundColor = new SolidBrush(Color.FromArgb(255, 255, 157, 172));
            NotifyBackgroundColor = new SolidBrush(Color.FromArgb(200, 96, 96, 96));
            NotifyStringColor = Brushes.White;
            DirectMessageBackgroundColor = new SolidBrush(Color.FromArgb(255, 171, 168, 198));
            SelectBackgroundColor = new SolidBrush(Color.FromArgb(255, 232, 232, 232));
            NameColor = new SolidBrush(Color.FromArgb(255, 47, 79, 79));
            TweetColor = new SolidBrush(Color.FromArgb(255, 64, 0, 64));
            ViaColor = new SolidBrush(Color.FromArgb(255, 108, 108, 108));
            LinkColor = new SolidBrush(Color.FromArgb(255, 124, 0, 249));
            ProfileName = "Default";
        }

        public static void load(Dictionary<string, BrushEX> obj)
        {
            if (obj == null)
                return;
            if (obj.ContainsKey("ProfileName"))
                ProfileName = (string)obj["ProfileName"].profileName.Clone();
            if (obj.ContainsKey("BackgroundColor"))
                BackgroundColor = obj["BackgroundColor"].Generate;
            if (obj.ContainsKey("ReplyBackgroundColor"))
                ReplyBackgroundColor = obj["ReplyBackgroundColor"].Generate;
            if (obj.ContainsKey("RetweetBackgroundColor"))
                RetweetBackgroundColor = obj["RetweetBackgroundColor"].Generate;
            if (obj.ContainsKey("OwnTweetBackgroundColor"))
                OwnTweetBackgroundColor = obj["OwnTweetBackgroundColor"].Generate;
            if (obj.ContainsKey("NotifyTweetBackgroundColor"))
                NotifyTweetBackgroundColor = obj["NotifyTweetBackgroundColor"].Generate;
            if (obj.ContainsKey("SelectBackgroundColor"))
                SelectBackgroundColor = obj["SelectBackgroundColor"].Generate;
            if (obj.ContainsKey("NotifyBackgroundColor"))
                NotifyBackgroundColor = obj["NotifyBackgroundColor"].Generate;
            if (obj.ContainsKey("NotifyStringColor"))
                NotifyStringColor = obj["NotifyStringColor"].Generate;
            if (obj.ContainsKey("NameColor"))
                NameColor = obj["NameColor"].Generate;
            if (obj.ContainsKey("TweetColor"))
                TweetColor = obj["TweetColor"].Generate;
            if (obj.ContainsKey("ViaColor"))
                ViaColor = obj["ViaColor"].Generate;
            if (obj.ContainsKey("LinkColor"))
                LinkColor = obj["LinkColor"].Generate;
            if (obj.ContainsKey("DirectMessageBackgroundColor"))
                DirectMessageBackgroundColor = obj["DirectMessageBackgroundColor"].Generate;
            if ( obj.ContainsKey ( "ProfileName" ) )
                Alpha = obj["ProfileName"].Alpha;
            if ( !string.IsNullOrEmpty ( Setting.BackgroundImage.BackgroundImagePath ) )
                Alpha = (byte)Setting.BackgroundImage.BackgroundTransparent;
        }

        public static Dictionary<string, BrushEX> save()
        {
            var dest = new Dictionary<string, BrushEX>();
            dest["ProfileName"] = new BrushEX(BackgroundColor) { profileName = (string)ProfileName.Clone(), a = Alpha };
            dest["BackgroundColor"] = new BrushEX(BackgroundColor);
            dest["ReplyBackgroundColor"] = new BrushEX(ReplyBackgroundColor);
            dest["RetweetBackgroundColor"] = new BrushEX(RetweetBackgroundColor);
            dest["OwnTweetBackgroundColor"] = new BrushEX(OwnTweetBackgroundColor);
            dest["NotifyTweetBackgroundColor"] = new BrushEX(NotifyTweetBackgroundColor);
            dest["SelectBackgroundColor"] = new BrushEX(SelectBackgroundColor);
            dest["NotifyBackgroundColor"] = new BrushEX(NotifyBackgroundColor);
            dest["NotifyStringColor"] = new BrushEX(NotifyStringColor);
            dest["NameColor"] = new BrushEX(NameColor);
            dest["TweetColor"] = new BrushEX(TweetColor);
            dest["ViaColor"] = new BrushEX(ViaColor);
            dest["LinkColor"] = new BrushEX(LinkColor);
            dest["DirectMessageBackgroundColor"] = new BrushEX(DirectMessageBackgroundColor);
            return dest;
        }

        #endregion

        public static bool IsShootingStar
        {
            get
            {
                return ProfileName == "ShootingStar";
            }
        }

        public static string ProfileName
        {
            get;
            set;
        }


        public static byte Alpha
        {
            get { return _alpha; }
            set
            {
                _alpha = value;
                DirectMessageBackgroundColor = ChangeAlpha ( DirectMessageBackgroundColor );
                BackgroundColor = ChangeAlpha ( BackgroundColor );
                ReplyBackgroundColor = ChangeAlpha ( ReplyBackgroundColor );
                RetweetBackgroundColor = ChangeAlpha ( RetweetBackgroundColor );
                OwnTweetBackgroundColor = ChangeAlpha ( OwnTweetBackgroundColor );
                NotifyTweetBackgroundColor = ChangeAlpha ( NotifyTweetBackgroundColor );
                SelectBackgroundColor = ChangeAlpha ( SelectBackgroundColor );
            }
        }

        public static Brush ChangeAlpha ( Brush b )
        {
            if ( b != null )
            {
                using ( var pen = new Pen ( b ) )
                {
                    var col = pen.Color;
                    return new SolidBrush ( Color.FromArgb ( Alpha, col ) );
                }
            }
            return null;
        }

        public static Brush DirectMessageBackgroundColor
        {
            get;
            set;
        }

        public static Brush NotifyBackgroundColor
        {
            get;
            set;
        }

        public static Brush NotifyStringColor
        {
            get;
            set;
        }

        /// <summary>
        /// 背景色
        /// </summary>
        public static Brush BackgroundColor
        {
            get;
            set;
        }

        /// <summary>
        /// リプライ背景色
        /// </summary>
        public static Brush ReplyBackgroundColor
        {
            get;
            set;
        }

        /// <summary>
        /// 選択中の背景色
        /// </summary>
        public static Brush SelectBackgroundColor
        {
            get;
            set;
        }

        /// <summary>
        /// 通知の背景色
        /// </summary>
        public static Brush NotifyTweetBackgroundColor
        {
            get;
            set;
        }

        /// <summary>
        /// 自分のツイートの背景色
        /// </summary>
        public static Brush OwnTweetBackgroundColor
        {
            get;
            set;
        }

        /// <summary>
        /// リツイート背景色
        /// </summary>
        public static Brush RetweetBackgroundColor
        {
            get;
            set;
        }

        /// <summary>
        /// 名前
        /// </summary>
        public static Brush NameColor
        {
            get;
            set;
        }

        /// <summary>
        /// ツイート
        /// </summary>
        public static Brush TweetColor
        {
            get;
            set;
        }

        /// <summary>
        /// via
        /// </summary>
        public static Brush ViaColor
        {
            get;
            set;
        }

        /// <summary>
        /// LinkColoro
        /// </summary>
        public static Brush LinkColor
        {
            get;
            set;
        }
    }
}
