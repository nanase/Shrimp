using System.Collections.Generic;
using System.Drawing;
using System;

namespace Shrimp.Setting
{
    enum ImagePosition
    {
        LEFTTOP,
        CENTER,
        RIGHTBOTTOM
    };
    class BackgroundImage
    {
        #region 定義

        #endregion

        #region コンストラクタ

        /// <summary>
        /// 静的コンストラクタ
        /// </summary>
        public static void initialize()
        {
            BackgroundImagePath = "";
            BackgroundTransparent = 255;
            ImagePos = ImagePosition.CENTER;
        }

        public static void load(Dictionary<string, object> obj)
        {
            if (obj == null)
                return;
            if (obj.ContainsKey("BackgroundImagePath"))
                BackgroundImagePath = (string)((string)obj["BackgroundImagePath"]).Clone();
            if ( obj.ContainsKey ( "ImagePos" ) )
                ImagePos = (ImagePosition)obj["ImagePos"];
            if ( obj.ContainsKey ( "BackgroundTransparent" ) )
                BackgroundTransparent = (int)obj["BackgroundTransparent"];
        }

        public static Dictionary<string, object> save()
        {
            var dest = new Dictionary<string, object>();
            dest["BackgroundImagePath"] = BackgroundImagePath;
            dest["ImagePos"] = (int)ImagePos;
            dest["BackgroundTransparent"] = BackgroundTransparent;
            return dest;
        }

        #endregion

        /// <summary>
        /// 背景画像を設定されているかどうか
        /// </summary>
        private static string _BackgroundImagePath;
        public static string BackgroundImagePath
        {
            get
            {
                return _BackgroundImagePath;
            }
            set {
                _BackgroundImagePath = value;
                Image dest = null;
                try
                {
                    dest = Image.FromFile ( value );
                    BackgroundImageData = dest;
                } catch ( Exception )
                {
                    BackgroundImageData = new Bitmap ( 1, 1 );
                    Log.LogControl.AddLogs ( "背景画像の設定に失敗しました。\n" + value + "" );
                }
            }
        }

        public static Image BackgroundImageData
        {
            get;
            set;
        }

        public static ImagePosition ImagePos
        {
            get;
            set;
        }

        private static int _BackgroundTransparent;
        public static int BackgroundTransparent
        {
            get { return _BackgroundTransparent; }
            set
            {
                if ( !string.IsNullOrEmpty(BackgroundImagePath) )
                    Setting.Colors.Alpha = (byte)value;
                _BackgroundTransparent = value;
            }
        }
    }
}
