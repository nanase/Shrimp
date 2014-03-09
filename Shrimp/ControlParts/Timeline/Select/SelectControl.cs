using System;
using System.Drawing;

namespace Shrimp.ControlParts.Timeline.Select
{
    /// <summary>
    /// 選択をオーナードローします
    /// </summary>
    public class SelectControl : IDisposable
    {

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SelectControl()
        {
            BeforeRevPosition = startDown = endDown = new Point();
            selTextPosition = new int[2];
            selCellPosition = -1;
        }

        ~SelectControl()
        {
        }

        public void Dispose()
        {
            this.selTextPosition = null;
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// マウスが押されている場合はtrue
        /// </summary>
        public bool isMouseDown
        {
            get;
            set;
        }

        /// <summary>
        /// 全選択されている場合はtrue
        /// </summary>
        public bool selectAll
        {
            get;
            set;
        }

        /// <summary>
        /// 選択範囲が逆になった場合
        /// </summary>
        public bool selectReverse
        {
            get;
            set;
        }

        /// <summary>
        /// 選択開始位置
        /// </summary>
        public Point startDown
        {
            get;
            set;
        }

        /// <summary>
        /// 選択終了位置
        /// </summary>
        public Point endDown
        {
            get;
            set;
        }

        /// <summary>
        /// 選択範囲が入れ替わった際に保存された場所
        /// </summary>
        public Point BeforeRevPosition
        {
            get;
            set;
        }

        /// <summary>
        /// 選択の範囲
        /// </summary>
        public int[] selTextPosition
        {
            get;
            set;
        }

        /// <summary>
        /// 選択範囲のテキスト
        /// </summary>
        public string selText
        {
            get;
            set;
        }

        /// <summary>
        /// 選択されているセルの位置(ツイートID)
        /// </summary>
        public decimal selCellPosition
        {
            get;
            set;
        }

        public double TwoPoint
        {
            get
            {
                if (endDown != null && startDown != null)
                {
                    return Math.Sqrt(Math.Pow(endDown.X - startDown.X, 2)
                        + Math.Pow(endDown.Y - startDown.Y, 2));
                }
                else
                {
                    return 0.0;
                }
            }
        }

        /// <summary>
        /// 全選択を行う
        /// </summary>
        /// <param name="textlen"></param>
        public void SelectAll(string text, decimal tweetID)
        {
            this.isMouseDown = true;
            this.selectAll = true;
            this.selectReverse = false;
            this.selText = (string)text.Clone();
            this.selCellPosition = tweetID;
            this.selTextPosition[0] = 0;
            this.selTextPosition[1] = this.selText.Length;
        }

        /// <summary>
        /// 選択時に初期化を行う
        /// </summary>
        public void SelectInitialize()
        {
            this.isMouseDown = false;
            this.selectAll = false;
            this.selText = null;
            this.selectReverse = false;
            this.selCellPosition = -1;
            this.selTextPosition[0] = this.selTextPosition[1] = -1;
        }

        /// <summary>
        /// 選択開始
        /// </summary>
        /// <param name="MouseLocation"></param>
        /// <param name="selCellPosition"></param>
        public void SelectStart(Point MouseLocation, decimal selCellPosition)
        {
            this.isMouseDown = true;
            this.startDown = this.endDown = MouseLocation;
            this.selCellPosition = selCellPosition;
        }

        /// <summary>
        /// マウスが移動した際にカーソルを移動させる
        /// </summary>
        /// <param name="MouseLocation"></param>
        public void SelectNow(Point MouseLocation)
        {
            if (this.selectReverse)
            {
                //  逆側を走っているらしい・・・
                this.startDown = MouseLocation;
                if (MouseLocation.X > this.BeforeRevPosition.X && MouseLocation.Y > this.BeforeRevPosition.Y)
                {
                    this.selectReverse = false;
                    this.startDown = this.endDown = MouseLocation;
                }

            }
            else
            {
                this.endDown = MouseLocation;
                if (this.startDown.X > MouseLocation.X && this.startDown.Y > MouseLocation.Y)
                {
                    //
                    this.selectReverse = true;
                    this.BeforeRevPosition = MouseLocation;
                    this.endDown = this.startDown;
                    this.startDown = MouseLocation;
                }
            }
        }
        /// <summary>
        /// 選択終了
        /// </summary>
        /// <param name="MouseLocation"></param>
        public void SelectEnd(Point MouseLocation)
        {
            this.isMouseDown = false;
            this.endDown = MouseLocation;
        }

        /// <summary>
        /// 選択中？
        /// </summary>
        public bool isSelecting
        {
            get { return this.selCellPosition >= 0 && (this.selTextPosition[0] != this.selTextPosition[1]); }
        }
    }
}
