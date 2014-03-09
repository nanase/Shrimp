using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Shrimp.Module.Parts
{
    /// <summary>
    /// セルを描画する際に個々のパーツの大きさが記録される
    /// </summary>
    public class DrawCellSize : IDisposable
    {
        /// <summary>
        /// 単行表示かどうか
        /// </summary>
        public bool isLine = false;
		private bool isDisposed = false;
        #region コンストラクター

        public DrawCellSize ()
        {
            this.Name = new Cell ();
            this.Time = new Cell ();
            this.Icon = new Cell ();
            this.ImageTotal = new Cell ();
            this.StatusesMuch = new Cell ();
            this.RetweetNotify = new Cell ();
            this.Tweet = new Cell ();
            this.Via = new Cell ();
        }

		public void initialize()
		{
			this.Name.initialize();
			this.Time.initialize();
			this.Icon.initialize();
			this.ImageTotal.initialize();
			this.StatusesMuch.initialize();
			this.RetweetNotify.initialize();
			this.Tweet.initialize();
			this.Via.initialize();
		}

		public void Dispose()
		{
			if (!isDisposed)
			{
				this.Name = null;
				this.Time = null;
				this.Icon = null;
				this.ImageTotal = null;
				this.StatusesMuch = null;
				this.RetweetNotify = null;
				this.Tweet = null;
				this.Via = null;
			}
			this.isDisposed = true;
		}
        /*
        public DrawCellSize ( Size _nameSize, Size _tweetSize, Size _retweet_Notify_Size, Size _ViaSize )
        {
            NameSize = _nameSize;
            TweetSize = _tweetSize;
            ViaSize = _ViaSize;
            RetweetNotifySize = _retweet_Notify_Size;
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_nameSize"></param>
        /// <param name="_tweet"></param>
        /// <param name="_tweetSize"></param>
        /// <param name="_Via"></param>
        /// <param name="_ViaSize"></param>
        public DrawCellSize ( Point _name, Size _nameSize, Point _tweet, Size _tweetSize, Point _retweet_notify, Size _retweetSize, Point _Via, Size _ViaSize )
        {
            NamePosition = _name;
            NameSize = _nameSize;
            TweetPosition = _tweet;
            TweetSize = _tweetSize;
            RetweetNotifyPosition = _retweet_notify;
            RetweetNotifySize = _retweetSize;
            ViaPosition = _Via;
            ViaSize = _ViaSize;
        }
        */
        #endregion

        /// <summary>
        /// アイコン
        /// </summary>
        public Cell Icon
        {
            get;
            set;
        }

        /// <summary>
        /// 名前
        /// </summary>
        public Cell Name
        {
            get;
            set;
        }

        /// <summary>
        /// 時間
        /// </summary>
        public Cell Time
        {
            get;
            set;
        }

        /// <summary>
        /// ツイート
        /// </summary>
        public Cell Tweet
        {
            get;
            set;
        }

        /// <summary>
        /// 「～がリツイートしました」
        /// </summary>
        public Cell RetweetNotify
        {
            get;
            set;
        }

        /// <summary>
        /// リツイート・ふぁぼ数を表示
        /// </summary>
        public Cell StatusesMuch
        {
            get;
            set;
        }

        /// <summary>
        /// Via
        /// </summary>
        public Cell Via
        {
            get;
            set;
        }

        /// <summary>
        /// ボタンのいろいろ
        /// </summary>
        public ButtonSize Buttons
        {
            get;
            set;
        }

        /// <summary>
        /// 画像の大きさ
        /// </summary>
        public Cell ImageTotal
        {
            get;
            set;
        }

        /// <summary>
        /// パディングを除いたセルの大きさ
        /// </summary>
        public int CellSizeWithoutPadding
        {
            get {
                if ( this.isLine )
                    return this.CellSizeLine;

                return ( Name != null ? Name.Size.Height : 0 )
                    + ( Tweet != null ? Tweet.Size.Height : 0 )
                    + ( RetweetNotify != null ? RetweetNotify.Size.Height : 0 )
                    + ( Via != null ? Via.Size.Height : 0 )
                    + ( ImageTotal != null ? ImageTotal.Rect.Height : 0 )
                    + ( Buttons != null ? Buttons.FavIconSize.Height : 0 ); }
        }

        /// <summary>
        /// 単行表示の時のセルの大きさ
        /// </summary>
        public int CellSizeLine
        {
            get
            {
                return Name.Size.Height;
            }
        }

        /// <summary>
        /// テキストを描画するときのパディング
        /// </summary>
        public int DrawTextPadding
        {
            get { return 5; }
        }

        public Point CellStartPosition
        {
            get;
            set;
        }

        public Rectangle CellRect
        {
            get
            {
                return new Rectangle ( this.CellStartPosition, new Size ( CellWidthSize, CellSize ) );
            }
        }

        /// <summary>
        /// セルの大きさ
        /// </summary>
        public int CellSize
        {
            get {
                if ( this.isLine )
                    return this.CellSizeLine;
                return CellSizeWithoutPadding + ( DrawTextPadding * 3 ) + ( ImageTotal.Rect.Height <= 0 ? 0 : DrawTextPadding ) + ( RetweetNotify.Rect.Height <= 0 ? 0 : DrawTextPadding );
            }
        }

        public int CellWidthSize
        {
            get; set;
        }
	}
}
