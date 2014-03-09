using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;

namespace Shrimp.Setting
{
    /// <summary>
    /// 主にツイートのフォント
    /// </summary>
    class Fonts
    {
        #region 定義

        #endregion

        #region コンストラクタ

        internal static void initialize ()
        {
            //  初期設定
            NameFont = new Font ( "Meiryo", 9 );
            TweetFont = new Font ( "Meiryo", 10 );
            ViaFont = new Font ( "Meiryo", 9 );
            RetweetNotify = new Font ( "Meiryo", 9 );
        }

        public static void load ( Dictionary<string, string> obj )
        {
            if (obj == null)
                return;
            if ( obj.ContainsKey ( "NameFont" ) )
                NameFontConverted = (string)obj["NameFont"];
            if (obj.ContainsKey("TweetFont"))
                TweetFontCoverted = (string)obj["TweetFont"];
            if (obj.ContainsKey("ViaFont"))
                ViaFontConverted = (string)obj["ViaFont"];
            if (obj.ContainsKey("RetweetNotify"))
                RetweetNotifyFontConverted = (string)obj["RetweetNotify"];
        }

        public static Dictionary<string, string> save ()
        {
            var dest = new Dictionary<string, string>();
            dest["NameFont"] = (string)NameFontConverted;
            dest["TweetFont"] = (string)TweetFontCoverted;
            dest["ViaFont"] = (string)ViaFontConverted;
            dest["RetweetNotify"] = (string)RetweetNotifyFontConverted;
            return dest;
        }

        public static string NameFontConverted
        {
            get
            {
                return TypeDescriptor.GetConverter ( typeof ( Font ) ).ConvertToString ( NameFont );
            }
            set
            {
                NameFont = (Font)TypeDescriptor.GetConverter ( typeof ( Font ) ).ConvertFromString ( value );
            }
        }

        public static string TweetFontCoverted
        {
            get
            {
                return TypeDescriptor.GetConverter ( typeof ( Font ) ).ConvertToString ( TweetFont );
            }
            set
            {
                TweetFont = (Font)TypeDescriptor.GetConverter ( typeof ( Font ) ).ConvertFromString ( value );
            }
        }

        public static string ViaFontConverted
        {
            get
            {
                return TypeDescriptor.GetConverter ( typeof ( Font ) ).ConvertToString ( ViaFont );
            }
            set
            {
                ViaFont = (Font)TypeDescriptor.GetConverter ( typeof ( Font ) ).ConvertFromString ( value );
            }
        }

        public static string RetweetNotifyFontConverted
        {
            get
            {
                return TypeDescriptor.GetConverter ( typeof ( Font ) ).ConvertToString ( RetweetNotify );
            }
            set
            {
                RetweetNotify = (Font)TypeDescriptor.GetConverter ( typeof ( Font ) ).ConvertFromString ( value );
            }
        }

        #endregion

        public static Font NameFont
        {
            get;
            set;
        }

        private static Font _TweetFont;
        public static Font TweetFont
        {
            get
            {
                return _TweetFont;
            }
            set
            {
                TweetUnderLineFont = new Font ( value, FontStyle.Underline );
                TweetUnderLineBoldFont = new Font ( value, FontStyle.Underline | FontStyle.Bold );
                TweetFontBold = new Font ( value, FontStyle.Bold );
                _TweetFont = value;
            }
        }

        public static Font TweetUnderLineFont
        {
            get;
            set;
        }

        public static Font TweetUnderLineBoldFont
        {
            get;
            set;
        }

        public static Font TweetFontBold
        {
            get;
            set;
        }

        public static Font ViaFont
        {
            get;
            set;
        }

        public static Font RetweetNotify
        {
            get;
            set;
        }
    }
}