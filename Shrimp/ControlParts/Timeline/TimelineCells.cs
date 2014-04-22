using System;
using System.Collections.Generic;
using System.Drawing;
using Shrimp.Module;
using Shrimp.Module.Parts;
using Shrimp.Module.TimeUtil;
using Shrimp.Twitter.Entities;
using Shrimp.Twitter.Status;

namespace Shrimp.ControlParts.Timeline
{
    /// <summary>
    /// タイムラインセルの管理を行います
    /// </summary>
    public class TimelineCells : IDisposable
    {
        #region 定義
        private List<DrawCellSize> data;
        private DrawCellSize tmp;
        private List<TimelineCellsTweetID> tweets;
        private bool Disposed = false;
        #endregion

        #region コンストラクター
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TimelineCells()
        {
            data = new List<DrawCellSize>();
            tmp = new DrawCellSize();
            tweets = new List<TimelineCellsTweetID>();
        }

        ~TimelineCells()
        {
            Dispose(false);
        }
        #endregion

        /// <summary>
        /// 初期化
        /// </summary>
        protected virtual void initialize()
        {
            data.Clear();
            data.Capacity = 0;
            NextStartPositionOffsetY = 0;
            tweets.Clear();
            tweets.Capacity = 0;
        }

        public void Dispose()
        {
            Dispose(true);
            // Take yourself off the Finalization queue 
            // to prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);

        }

        protected virtual void Dispose(bool disposing)
        {
            if (!Disposed)
            {
                this.data.Clear();
                this.data = null;
                this.tmp = null;
                this.tweets.Clear();
                this.tweets = null;
            }

            Disposed = true;
        }

        /// <summary>
        /// 最大幅
        /// </summary>
        private int MaxWidth
        {
            get;
            set;
        }

        /// <summary>
        /// レイアウトの描画を開始するときに呼んでください
        /// </summary>
        protected virtual bool StartLayout(TwitterStatus tweet, bool isConversation, int offset_start_x, int image_size, int maxWidth)
        {
            if (tweet == null || tweet.user == null)
                return false;

            this.tweets.Add(new TimelineCellsTweetID(tweet.id, isConversation));
            var dynamic_t = tweet.DynamicTweet;

            tmp = new DrawCellSize();
            tmp.CellStartPosition = new Point(offset_start_x, this.NextStartPositionOffsetY);
            tmp.CellWidthSize = maxWidth;
            MaxWidth = maxWidth;

            //  リツイートの内容を書く
            string retweet_text = "";
            TimelineUtil.GenerateRetweetStatus(tweet, out retweet_text);
            //TimelineUtil.GenerateFavedStatus ( tweet, out retweet_text );

            //  生成
            LayoutPicture(offset_start_x, image_size);
            LayoutName(TimelineUtil.GenerateName(dynamic_t) + " ", Setting.Fonts.NameFont);
            var time = ( Setting.Timeline.isEnableAbsoluteTime ? TimeSpanUtil.AbsoluteTimeToString ( dynamic_t.created_at ) :
                TimeSpanUtil.AgoToString ( dynamic_t.created_at ) );
            LayoutTime(time + " ", Setting.Fonts.NameFont);
            LayoutText(dynamic_t.text, Setting.Fonts.TweetFont);
            if (dynamic_t.media_count != 0)
                LayoutImage(dynamic_t.media_count, dynamic_t.entities.media);
            LayoutVia("via " + dynamic_t.source, Setting.Fonts.ViaFont);
            LayoutRetweet(retweet_text, Setting.Fonts.RetweetNotify);
            LayoutStatuses(dynamic_t.favorite_count, dynamic_t.retweet_count, Setting.Fonts.RetweetNotify);
            LayoutButtons();
            return true;
        }

        /// <summary>
        /// 単行表示（テスト
        /// </summary>
        /// <param name="tweet"></param>
        /// <param name="offset_start_x"></param>
        /// <param name="image_size"></param>
        /// <param name="maxWidth"></param>
        protected virtual void StartLayoutLine(TwitterStatus tweet, bool isConversation, int offset_start_x, int image_size, int maxWidth)
        {
            if (tweet == null)
                return;

            this.tweets.Add(new TimelineCellsTweetID(tweet.id, isConversation));
            var dynamic_t = tweet.DynamicTweet;

            tmp = new DrawCellSize();
            tmp.isLine = true;
            tmp.CellStartPosition = new Point(offset_start_x, this.NextStartPositionOffsetY);
            tmp.CellWidthSize = maxWidth;
            MaxWidth = maxWidth;

            var time = ( Setting.Timeline.isEnableAbsoluteTime ? TimeSpanUtil.AbsoluteTimeToString ( dynamic_t.created_at ) :
    TimeSpanUtil.AgoToString ( dynamic_t.created_at ) );
            LayoutTime(time, Setting.Fonts.NameFont);
            double line_x = MaxWidth - (16.0 + tmp.Time.Size.Width + 10);
            //  生成
            LayoutPicture(offset_start_x, image_size);
            tmp.Icon.Position = new Point(1, tmp.Icon.Position.Y - 4);
            tmp.Icon.Size = new Size(16, 16);
            LayoutName("@" + tweet.user.screen_name, Setting.Fonts.NameFont);

            tmp.Name.Size = new Size((int)(0.15 * line_x), tmp.Name.Size.Height);
            tmp.Name.Position = new Point(tmp.Icon.Rect.Right + 5, tmp.Icon.Rect.Top);
            var ret = (tweet.retweeted_status != null ? "【@" + tweet.retweeted_status.user.screen_name + "をリツイートしました】 " + tweet.retweeted_status.text + "" : tweet.text);
            LayoutText(ret, Setting.Fonts.TweetFont);
            tmp.Tweet.Size = new Size((int)(0.85 * line_x), tmp.Name.Size.Height);
            tmp.Tweet.Position = new Point(tmp.Name.Rect.Right + 5, tmp.Icon.Rect.Top);
            tmp.Time.Position = new Point(tmp.Tweet.Rect.Right, tmp.Icon.Rect.Top);
            tmp.Time.Size = new Size(tmp.Time.Size.Width + 10, tmp.Time.Size.Height);
            //tmp.Time.Position = new Point ( tmp.Tweet.Rect.Right + 5, tmp.Icon.Rect.Top );
        }

        /// <summary>
        /// 描画途中のレイアウトセルサイズを取得します
        /// </summary>
        /// <returns></returns>
        public DrawCellSize getLayout()
        {
            return tmp;
        }

        private Point LayoutPicture(int offset_start_x, int image_size)
        {
            if (tmp != null)
            {
                //  Padding考慮
                Point p = new Point(offset_start_x + 5, 5 + NextStartPositionOffsetY);
                tmp.Icon.Position = p;
                tmp.Icon.Size = new Size(image_size, image_size);
                return p;
            }
            return Point.Empty;
        }

        private Point LayoutName(string name, Font f)
        {
            if (tmp != null)
            {
                //  Padding考慮
                Point p = new Point(tmp.Icon.Rect.Right + 5, tmp.Icon.Rect.Top);
                tmp.Name.Position = p;
                tmp.Name.Size = DrawTextUtil.GetDrawTextSize(name, f, MaxWidth, false, true);
                tmp.Name.Detail = name;
                return p;
            }
            return Point.Empty;
        }

        private Point LayoutTime(string time, Font f)
        {
            if (tmp != null)
            {
                tmp.Time.Size = DrawTextUtil.GetDrawTextSize(time, f, MaxWidth, false, true);
                //  Padding考慮
                Point p = new Point(this.MaxWidth - tmp.Name.Size.Width, tmp.Icon.Rect.Top);
                tmp.Time.Position = p;
                tmp.Time.Detail = time;
                return p;
            }
            return Point.Empty;
        }

        private Point LayoutText(string text, Font f)
        {
            if (tmp != null)
            {
                //  Padding考慮
                Point p = new Point(tmp.Icon.Rect.Right + 5, tmp.Name.Rect.Bottom);
                tmp.Tweet.Position = p;
                tmp.Tweet.Size = DrawTextUtil.GetOwnerDrawTextSize(text, f, p.X, MaxWidth);
                tmp.Tweet.Detail = text;
                return p;
            }
            return Point.Empty;
        }

        private Point LayoutImage(int num, List<TwitterEntitiesMedia> medias)
        {
            if (tmp != null && num > 0)
            {
                //  Padding考慮
                Point p = new Point(tmp.Icon.Rect.Right + 5, tmp.Tweet.Rect.Bottom);
                tmp.ImageTotal.Position = p;
                int limit_width = MaxWidth / Setting.Timeline.ImageWidth;
                if (limit_width <= 0)
                    limit_width = 1;
                tmp.ImageTotal.Size = new Size((num > limit_width ? MaxWidth : Setting.Timeline.ImageWidth * num), (num > limit_width ? (num / limit_width) * Setting.Timeline.ImageHeight : Setting.Timeline.ImageHeight));
                return p;
            }
            return Point.Empty;
        }

        private Point LayoutVia(string via, Font f)
        {
            if (tmp != null)
            {
                //  Padding考慮
                Point p = new Point(tmp.Icon.Rect.Right + 5, (tmp.ImageTotal.Size.Height != 0 ? tmp.ImageTotal.Rect.Bottom : tmp.Tweet.Rect.Bottom));
                tmp.Via.Position = p;
                tmp.Via.Size = DrawTextUtil.GetDrawTextSize(via, f, MaxWidth, false, true);
                tmp.Via.Detail = via;
                return p;
            }
            return Point.Empty;
        }


        private Point LayoutRetweet(string retweet_notify_text, Font f)
        {
            if (tmp != null && retweet_notify_text != null)
            {
                //  Padding考慮
                Point p = new Point(tmp.Icon.Rect.Right + 5 + Setting.Timeline.RetweetMarkSize, tmp.Via.Rect.Bottom);
                tmp.RetweetNotify.Position = p;
                tmp.RetweetNotify.Size = DrawTextUtil.GetDrawTextSize(retweet_notify_text, f, MaxWidth, false, true);
                tmp.RetweetNotify.Detail = retweet_notify_text;
                return p;
            }
            return Point.Empty;
        }

        private Point LayoutStatuses(decimal fav, decimal rt, Font f)
        {
            if (tmp != null)
            {
                //  Padding考慮
                Point p = new Point(tmp.Icon.Rect.Right + 5, (tmp.RetweetNotify.Position.Y == 0 ? tmp.Via.Rect.Bottom : tmp.RetweetNotify.Rect.Bottom));
                tmp.StatusesMuch.Position = p;
                tmp.StatusesMuch.Size = DrawTextUtil.GetDrawTextSize("" + fav + " " + rt + "", f, MaxWidth, false, true);
                tmp.StatusesMuch.Detail = "";
                return p;
            }
            return Point.Empty;
        }

        private void LayoutButtons()
        {
            if (tmp != null)
            {
                //  Padding考慮
                //Point p = new Point ( tmp.IconRect.Right + 5, ( tmp.RetweetNotifyPosition.Y == 0 ? tmp.ViaRect.Bottom : tmp.RetweetNotifyRect.Bottom ) + 3 );
                Point p = new Point(this.MaxWidth - ((20 + 26 + 20) + (Setting.Timeline.ButtonPadding * 4)), (tmp.RetweetNotify.Position.Y == 0 ? tmp.Via.Rect.Bottom : tmp.RetweetNotify.Rect.Bottom) + 5);
                tmp.Buttons = new ButtonSize();
                tmp.Buttons.ReplyIconPosition = p;
                tmp.Buttons.ReplyIconSize = new Size(20, 20);
                tmp.Buttons.RetweetIconPosition = new Point(tmp.Buttons.ReplyIconRect.Right + Setting.Timeline.ButtonPadding, tmp.Buttons.ReplyIconRect.Top - 2);
                tmp.Buttons.RetweetIconSize = new Size(26, 24);
                tmp.Buttons.FavIconPosition = new Point(tmp.Buttons.RetweetIconRect.Right + Setting.Timeline.ButtonPadding, tmp.Buttons.RetweetIconRect.Top);
                tmp.Buttons.FavIconSize = new Size(20, 20);
            }
        }

        public void EndLayoutLine()
        {
            NextStartPositionOffsetY += tmp.CellSizeLine;
            data.Add(tmp);
            tmp = null;
        }

        public void EndLayout()
        {
            NextStartPositionOffsetY += tmp.CellSize;
            data.Add(tmp);
            tmp = null;
        }

        /// <summary>
        /// 取得
        /// </summary>
        /// <param name="index">位置</param>
        /// <returns>取得したデータ</returns>
        public DrawCellSize get(int index)
        {
            return data[index];
        }

        /// <summary>
        /// 次の表示セル位置
        /// </summary>
        public int NextStartPositionOffsetY
        {
            get;
            set;
        }

        /// <summary>
        /// カーソルがツイート(テキスト)の中にあるかどうかを調べます
        /// </summary>
        /// <param name="cursor">位置</param>
        /// <returns>ある場合は、ツイート位置が返されます。なければ-1が返されます</returns>
        public int IsCursorInTweetText(Point cursor)
        {
            int num = 0;
            foreach (DrawCellSize t in data)
            {
                if (t.Tweet.Rect.Contains(cursor))
                {
                    //  含まれていた
                    return num;
                }

                num++;
            }
            return -1;
        }


        /// <summary>
        /// カーソルがツイートの中にあるかどうかを調べます
        /// </summary>
        /// <param name="cursor">位置</param>
        /// <returns>ある場合は、ツイート位置が返されます。なければ-1が返されます</returns>
        public int IsCursorInTweet(Point cursor)
        {
            int num = 0;
            foreach (DrawCellSize t in data)
            {
                if (t.CellRect.Contains(cursor))
                {
                    //  含まれていた
                    return num;
                }

                num++;
            }
            return -1;
        }

        /// <summary>
        /// カーソルがツイートの中にあるかどうかを調べます
        /// </summary>
        /// <param name="cursor">位置</param>
        /// <returns>ある場合は、ツイート位置が返されます。なければ-1が返されます</returns>
        public int IsCursorInCell(Point cursor)
        {
            int num = 0;
            foreach (DrawCellSize t in data)
            {
                Rectangle rect = new Rectangle(t.Icon.Position, new Size(this.MaxWidth, t.CellSize));
                if (rect.Contains(cursor))
                {
                    //  含まれていた
                    return num;
                }

                num++;
            }
            return -1;
        }


        /// <summary>
        /// カーソルがツイートの中(テキスト)にあるかどうかを調べます
        /// 返されるデータは、「セルに割り振られたID」です。TweetIDではありません
        /// </summary>
        /// <param name="cursor">位置</param>
        /// <returns>ある場合は、ツイートIDが返されます。なければnullが返されます</returns>
        public TimelineCellsTweetID IsCursorInCellTweetID(Point cursor)
        {
            int num = 0;
            foreach (DrawCellSize t in data)
            {
                Rectangle rect = new Rectangle(t.Icon.Position, new Size(this.MaxWidth, t.CellSize));
                if (rect.Contains(cursor))
                {
                    //  含まれていた
                    return tweets[num];
                }

                num++;
            }
            return null;
        }
    }

}
