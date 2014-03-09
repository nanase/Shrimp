using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Shrimp.Module
{
    /// <summary>
    /// ボタンイメージクラス
    /// </summary>
    class ButtonImage
    {
        #region コンストラクタ
        public ButtonImage ( Image normal, Image hover )
        {
            this.normal = normal;
            this.hover = hover;
        }
        #endregion

        /// <summary>
        /// 通常時の画像
        /// </summary>
        public Image normal
        {
            get;
            set;
        }

        /// <summary>
        /// ホバー中の画像
        /// </summary>
        public Image hover
        {
            get;
            set;
        }

    }
}
