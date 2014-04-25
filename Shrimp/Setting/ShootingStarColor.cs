using System.Collections.Generic;
using System.Drawing;

namespace Shrimp.Setting
{
    class ShootingStarColor
    {
        #region 定義
        private static byte _alpha = 255;
        #endregion

        #region コンストラクタ

        static ShootingStarColor ()
        {
            //  初期設定
            BackgroundColor = new SolidBrush ( Color.FromArgb ( 255, 38, 38, 38 ) );
            ReplyBackgroundColor = new SolidBrush ( Color.FromArgb ( 255, 54, 22, 22 ) );
            RetweetBackgroundColor = new SolidBrush ( Color.FromArgb ( 255, 22, 22, 54 ) );
            OwnTweetBackgroundColor = new SolidBrush ( Color.FromArgb ( 255, 0, 217, 108 ) );
            NotifyTweetBackgroundColor = new SolidBrush ( Color.FromArgb ( 255, 54, 22, 22 ) );
            DirectMessageBackgroundColor = new SolidBrush ( Color.FromArgb ( 255, 54, 22, 22 ) );
            SelectBackgroundColor = new SolidBrush ( Color.FromArgb ( 255, 68, 68, 68 ) );
            NameColor = new SolidBrush ( Color.FromArgb ( 151, 202, 0 ) );
            TweetColor = new SolidBrush ( Color.FromArgb ( 221, 221, 221 ) );
            ViaColor = new SolidBrush ( Color.FromArgb ( 148, 148, 148 ) );
            LinkColor = new SolidBrush ( Color.FromArgb ( 255, 124, 0, 249 ) );

            RetweetNameColor = new SolidBrush ( Color.FromArgb ( 49, 175, 221 ) );
            RetweetTextColor = new SolidBrush ( Color.FromArgb ( 199, 199, 250 ) );


            ReplyNameColor = new SolidBrush ( Color.FromArgb ( 254, 67, 67 ) );
            ReplyTextColor = new SolidBrush ( Color.FromArgb ( 255, 219, 219 ) );
        }

        #endregion

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

        public static Brush RetweetNameColor
        {
            get;
            set;
        }

        public static Brush RetweetTextColor
        {
            get;
            set;
        }

        public static Brush ReplyNameColor
        {
            get;
            set;
        }

        public static Brush ReplyTextColor
        {
            get;
            set;
        }
    }
}
