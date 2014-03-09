using System.Drawing;
using Shrimp.Twitter.Entities;

namespace Shrimp.ControlParts.Timeline.Click
{
    /// <summary>
    /// セルの押したときの情報
    /// </summary>
    class ClickCellsData
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="detail"></param>
        /// <param name="type"></param>
        public ClickCellsData(Rectangle rect, TwitterEntitiesPosition entities_pos)
        {
            this.Rect = rect;
            this.entities_pos = entities_pos;
        }
        /// <summary>
        /// Rect
        /// </summary>
        public Rectangle Rect
        {
            get;
            set;
        }

        /// <summary>
        /// 詳細
        /// </summary>
        public TwitterEntitiesPosition entities_pos
        {
            get;
            set;
        }

        /// <summary>
        /// entities_posへ簡単にアクセスするためのもの
        /// </summary>
        public string type
        {
            get
            {
                if (this.entities_pos != null)
                    return entities_pos.type;
                return null;
            }
            set
            {
                if (this.entities_pos != null)
                    entities_pos.type = value;
            }
        }

        public object source
        {
            get
            {
                if (this.entities_pos != null)
                    return entities_pos.source;
                return null;
            }
            set
            {
                if (this.entities_pos != null)
                    entities_pos.source = (string)value;
            }
        }

    }
}
