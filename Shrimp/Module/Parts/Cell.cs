using System;
using System.Drawing;

namespace Shrimp.Module.Parts
{
    /// <summary>
    /// 個々のセルが持つ位置情報
    /// </summary>
    public class Cell
    {
        public Cell()
        {
        }

        /// <summary>
        /// セルを初期化します
        /// </summary>
        public void initialize()
        {
            this.Position = Point.Empty;
            this.Size = Size.Empty;
            this.Detail = String.Empty;
        }

        public Cell(Point point, Size size, string detail)
        {
            this.Position = point;
            this.Size = size;
            this.Detail = detail;
        }

        /// <summary>
        /// 位置
        /// </summary>
        public Point Position
        {
            get;
            set;
        }

        /// <summary>
        /// 大きさ
        /// </summary>
        public Size Size
        {
            get;
            set;
        }

        /// <summary>
        /// Rectangle
        /// </summary>
        public Rectangle Rect
        {
            get
            {
                return new Rectangle(Position, Size);
            }
        }

        /// <summary>
        /// セルの文字とか、そういうの。
        /// </summary>
        public string Detail
        {
            get;
            set;
        }
    }
}
