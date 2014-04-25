using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.Drawing;

namespace Shrimp.ControlParts.ToolTips
{
    /// <summary>
    /// タイムラインで表示するツールチップです。
    /// </summary>
    class TimelineTooltips : ToolTip
    {
        public TimelineTooltips()
            : base()
        {
            this.OwnerDraw = true;
            this.Draw += new DrawToolTipEventHandler(TimelineTooltips_Draw);
            this.Popup += new PopupEventHandler(TimelineTooltips_Popup);
        }

        ~TimelineTooltips()
        {
            this.Draw -= new DrawToolTipEventHandler(TimelineTooltips_Draw);
            this.Popup -= new PopupEventHandler(TimelineTooltips_Popup);
        }

        /// <summary>
        /// 内部で表示するときに使うイメージデータです
        /// </summary>
        private Bitmap DrawImageData
        {
            get;
            set;
        }

        /// <summary>
        /// イメージをセットします
        /// </summary>
        /// <param name="bmp"></param>
        public void SetImage(Bitmap bmp)
        {
            this.DrawImageData = bmp;
        }


        void TimelineTooltips_Popup(object sender, PopupEventArgs e)
        {
            if (DrawImageData != null)
                e.ToolTipSize = DrawImageData.Size; 
        }

        void TimelineTooltips_Draw(object sender, DrawToolTipEventArgs e)
        {
            if (DrawImageData != null)
            {
                e.DrawBackground();
                e.Graphics.DrawImage(this.DrawImageData, new Rectangle ( Point.Empty, this.DrawImageData.Size ) );
            }
        } 
    }
}
