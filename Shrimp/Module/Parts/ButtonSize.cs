using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Shrimp.Module.Parts
{
    public class ButtonSize
    {
        /// <summary>
        /// リプライ
        /// </summary>
        public Point ReplyIconPosition
        {
            get;
            set;
        }

        /// <summary>
        /// リプライのサイズ
        /// </summary>
        public Size ReplyIconSize
        {
            get;
            set;
        }

        /// <summary>
        /// リプライのサイズの大きさ
        /// </summary>
        public Rectangle ReplyIconRect
        {
            get { return new Rectangle ( ReplyIconPosition, ReplyIconSize ); }
        }

        /// <summary>
        /// リツイート
        /// </summary>
        public Point RetweetIconPosition
        {
            get;
            set;
        }

        /// <summary>
        /// リツイートのサイズ
        /// </summary>
        public Size RetweetIconSize
        {
            get;
            set;
        }

        /// <summary>
        /// リツイートのサイズの大きさ
        /// </summary>
        public Rectangle RetweetIconRect
        {
            get { return new Rectangle ( RetweetIconPosition, RetweetIconSize ); }
        }

        /// <summary>
        /// ふぁぼ
        /// </summary>
        public Point FavIconPosition
        {
            get;
            set;
        }

        /// <summary>
        /// ふぁぼのサイズ
        /// </summary>
        public Size FavIconSize
        {
            get;
            set;
        }

        /// <summary>
        /// ふぁう゛ぉサイズの大きさ
        /// </summary>
        public Rectangle FavIconRect
        {
            get { return new Rectangle ( FavIconPosition, FavIconSize ); }
        }

        /// <summary>
        /// 全部の大きさ
        /// </summary>
        public Rectangle CellRect
        {
            get
            {
                return new Rectangle ( ReplyIconPosition, new Size ( ReplyIconSize.Width + RetweetIconSize.Width + FavIconSize.Width + ( Setting.Timeline.ButtonPadding * 3 ), RetweetIconSize.Height ) );
            }
        }
    }
}
