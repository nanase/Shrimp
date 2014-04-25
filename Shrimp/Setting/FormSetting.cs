using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Shrimp.Setting
{
    /// <summary>
    /// フォームの設定を管理します
    /// </summary>
    class FormSetting
    {
        #region コンストラクタ

        internal static void initialize()
        {
            //  初期設定
            TimelineSplitterDistance = 452;
            Bounds = new Rectangle();
            WindowState = FormWindowState.Normal;
        }

        #endregion

        public static void load(Dictionary<string, object> obj)
        {
            if (obj == null)
                return;
            if (obj.ContainsKey("TimelineSplitterDistance"))
                TimelineSplitterDistance = (int)obj["TimelineSplitterDistance"];
            if (obj.ContainsKey("Bounds"))
                BoundsConverted = (string)obj["Bounds"];
            if (obj.ContainsKey("WindowState"))
                WindowStateConverted = (string)obj["WindowState"];
			if (obj.ContainsKey("isInsertTaskTrayWhenClosing"))
				isInsertTaskTrayWhenClosing = (bool)obj["isInsertTaskTrayWhenClosing"];
        }

        public static Dictionary<string, object> save()
        {
            var dest = new Dictionary<string, object>();
            dest["TimelineSplitterDistance"] = (int)TimelineSplitterDistance;
            dest["Bounds"] = (string)BoundsConverted;
            dest["WindowState"] = (string)WindowStateConverted;
			dest["isInsertTaskTrayWhenClosing"] = isInsertTaskTrayWhenClosing;
            return dest;
        }

        public static string BoundsConverted
        {
            get
            {
                return TypeDescriptor.GetConverter(typeof(Rectangle)).ConvertToString(Bounds);
            }
            set
            {
                Bounds = (Rectangle)TypeDescriptor.GetConverter(typeof(Rectangle)).ConvertFromString(value);
            }
        }

        public static string WindowStateConverted
        {
            get
            {
                return TypeDescriptor.GetConverter(typeof(FormWindowState)).ConvertToString(WindowState);
            }
            set
            {
                WindowState = (FormWindowState)TypeDescriptor.GetConverter(typeof(FormWindowState)).ConvertFromString(value);
            }
        }

        /// <summary>
        /// ウィンドウの大きさ
        /// </summary>
        public static Rectangle Bounds
        {
            get;
            set;
        }

        /// <summary>
        /// 状態
        /// </summary>
        public static FormWindowState WindowState
        {
            get;
            set;
        }

        /// <summary>
        /// スプリッターの幅
        /// </summary>
        public static int TimelineSplitterDistance
        {
            get;
            set;
        }

		/// <summary>
		/// 閉じたときに終了せずタスクトレイにいれる
		/// </summary>
		public static bool isInsertTaskTrayWhenClosing
		{
			get;
			set;
		}
    }
}
