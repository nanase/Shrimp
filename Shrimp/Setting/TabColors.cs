using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Shrimp.Setting.ObjectXML;

namespace Shrimp.Setting
{
    /// <summary>
    /// Color Class
    /// タブの色設定クラス
    /// </summary>
    class TabColors
    {
        #region 定義

        #endregion

        #region コンストラクタ

        internal static void initialize ()
        {
            //  初期設定
            ExistUnReadBackgroundColor = new SolidBrush ( Color.FromArgb ( 255, 255, 96, 96 ) );
            ExistUnReadStringColor = Brushes.Black;
            isBoldUnRead = true;
            UnOpenedBackgroundColor = Brushes.LightYellow;
            UnOpenedStringColor = Brushes.Black;
            isBoldUnOpened = false;
            NormalTabBackground = Brushes.LightGray;
            NormalTabString = Brushes.Black;
            isBoldNormalTab = false;
            SelectedTabBackground = Brushes.Beige;
            SelectedTabString = Brushes.Black;
            isBoldSelectedTab = true;
        }

        public static void load ( Dictionary<string, TabColorManager> obj )
        {
            if (obj == null)
                return;
            TabColorManager tab = null;
            if ( obj.ContainsKey ( "ExistUnRead" ) )
            {
                tab = obj["ExistUnRead"] as TabColorManager;
                ExistUnReadBackgroundColor = tab.BackgroundColor.Generate;
                ExistUnReadStringColor = tab.StringColor.Generate;
                isBoldUnRead = tab.isBold;
            }

            if ( obj.ContainsKey ( "UnOpened" ) )
            {
                tab = obj["UnOpened"] as TabColorManager;
                UnOpenedBackgroundColor = tab.BackgroundColor.Generate;
                UnOpenedStringColor = tab.StringColor.Generate;
                isBoldUnOpened = tab.isBold;
            }

            if ( obj.ContainsKey ( "Normal" ) )
            {
                tab = obj["Normal"] as TabColorManager;
                NormalTabBackground = tab.BackgroundColor.Generate;
                NormalTabString = tab.StringColor.Generate;
                isBoldNormalTab = tab.isBold;
            }

            if ( obj.ContainsKey ( "Selected" ) )
            {
                tab = obj["Selected"] as TabColorManager;
                SelectedTabBackground = tab.BackgroundColor.Generate;
                SelectedTabString = tab.StringColor.Generate;
                isBoldSelectedTab = tab.isBold;
            }
        }

        public static Dictionary<string, TabColorManager> save ()
        {
            var dest = new Dictionary<string, TabColorManager> ();
            dest["ExistUnRead"] = new TabColorManager ( new BrushEX ( ExistUnReadBackgroundColor ), new BrushEX ( ExistUnReadStringColor ), isBoldUnRead );
            dest["UnOpened"] = new TabColorManager ( new BrushEX ( UnOpenedBackgroundColor ), new BrushEX ( UnOpenedStringColor ), isBoldUnOpened );
            dest["Normal"] = new TabColorManager ( new BrushEX ( NormalTabBackground ), new BrushEX ( NormalTabString ), isBoldNormalTab );
            dest["Selected"] = new TabColorManager ( new BrushEX ( SelectedTabBackground ), new BrushEX ( SelectedTabString ), isBoldSelectedTab );
            return dest;
        }
        #endregion


        /// <summary>
        /// 未読の背景色
        /// </summary>
        public static Brush ExistUnReadBackgroundColor
        {
            get;
            set;
        }

        /// <summary>
        /// 未読の文字色
        /// </summary>
        public static Brush ExistUnReadStringColor
        {
            get;
            set;
        }

        /// <summary>
        /// 未読を太文字にするかどうか
        /// </summary>
        public static bool isBoldUnRead
        {
            get;
            set;
        }

        /// <summary>
        /// 通知時、フラッシュするかどうか
        /// </summary>
        public static bool isFlashUnRead
        {
            get;
            set;
        }

        /// <summary>
        /// 作成したがまだ開いていないタブの背景色
        /// </summary>
        public static Brush UnOpenedBackgroundColor
        {
            get;
            set;
        }

        /// <summary>
        /// 作成したがまだ開いていないタブの文字色
        /// </summary>
        public static Brush UnOpenedStringColor
        {
            get;
            set;
        }

        /// <summary>
        /// 作成したがまだ開いていないタブの太文字
        /// </summary>
        public static bool isBoldUnOpened
        {
            get;
            set;
        }

        /// <summary>
        /// 選択もされていない通常食
        /// </summary>
        public static Brush NormalTabBackground
        {
            get;
            set;
        }

        /// <summary>
        /// 選択もされていない通常タブの文字色
        /// </summary>
        public static Brush NormalTabString
        {
            get;
            set;
        }

        /// <summary>
        /// 選択もされていない通常タブの太文字かどうか
        /// </summary>
        public static bool isBoldNormalTab
        {
            get;
            set;
        }

        /// <summary>
        /// 選択されている通常食
        /// </summary>
        public static Brush SelectedTabBackground
        {
            get;
            set;
        }

        /// <summary>
        /// 選択されている通常タブの文字色
        /// </summary>
        public static Brush SelectedTabString
        {
            get;
            set;
        }

        /// <summary>
        /// 選択されている通常タブの太文字
        /// </summary>
        public static bool isBoldSelectedTab
        {
            get;
            set;
        }
    }
}
