using System;
using System.Collections.Generic;
using System.Drawing;
using Shrimp.ControlParts.Timeline.Draw.Cells;
using Shrimp.ControlParts.Timeline.Select;
using Shrimp.Module.ImageUtil;
using Shrimp.Module.Parts;
using Shrimp.Setting;
using Shrimp.Twitter.Entities;
using Shrimp.Twitter.Status;

namespace Shrimp.ControlParts.Timeline.Draw
{
    /// <summary>
    /// ツイートを描画するクラスです
    /// </summary>
    public class TweetDraw : TimelineCells, IDisposable
    {
        public delegate void SetClickLinkDelegate(Rectangle rect, TwitterEntitiesPosition pos);

        //  キャッシュ待ち画像
        public List<string> cacheWaitingURLs = new List<string>();
        /// <summary>
        /// 選択済み？
        /// </summary>
        public bool isSelected = false;
        public bool isLine = false;
        public bool isHover = false;
        private DrawTweetCell draw = new DrawTweetCell();
        private bool Disposed = false;

        public static Bitmap drawOnlyTweet(TwitterStatus status, int width)
        {
            var draw = new TweetDraw();
            draw.initialize();
            draw.StartLayout(status, false, 0, width);
            draw.EndLayout();
            var height = draw.get(0).CellSizeWithoutPadding;
            var bmp = new Bitmap(width, height);
            var g = Graphics.FromImage(bmp);
            draw.initialize();
            draw.DrawTweet(g, status, false, 0, width, null, null, new Point());
            g.Dispose();
            return bmp;
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public new void initialize()
        {
            base.initialize();
            this.cacheWaitingURLs.Clear();
            isSelected = false;
        }

        ~TweetDraw()
        {
        }

        protected override void Dispose(bool disposing)
        {
            //base.Dispose ();
            if (!Disposed)
            {
                this.cacheWaitingURLs.Clear();
                this.cacheWaitingURLs = null;
                this.draw = null;
                base.Dispose(disposing);
            }
            Disposed = true;
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// オーバーライドし、更に基底クラスのinitializeを隠蔽
        /// </summary>
        /// <param name="tweet"></param>
        /// <param name="offset_start_x"></param>
        /// <param name="maxWidth"></param>
        public Bitmap StartLayout(TwitterStatus tweet, bool isConversation, int offset_start_x, int maxWidth)
        {
            //  アイコン
            Bitmap tmpIcon = null;
			if (Setting.Timeline.isEnableIcon)
			{
				if (tweet != null && tweet.DynamicTweet.user != null)
				{
					tmpIcon = ImageCache.AutoCache(tweet.DynamicTweet.user.profile_image_url, true);
					if (tmpIcon == null)
					{
						//  アイコンがキャッシュ待ちです
						cacheWaitingURLs.Add(tweet.user.profile_image_url);
						tmpIcon = (Bitmap)ResourceImages.LoadingImage.Clone();
						//tmpIcon = new Bitmap ( Setting.Timeline.IconSize, Setting.Timeline.IconSize );
					}
				}
				else
				{
					tmpIcon = (Bitmap)ResourceImages.LoadingImage.Clone();
					//tmpIcon = new Bitmap ( Setting.Timeline.IconSize, Setting.Timeline.IconSize );
				}
			}
            if (!base.StartLayout(tweet, isConversation, offset_start_x, ( tmpIcon != null ? tmpIcon.Width : 0 ), maxWidth))
                return null;
            return tmpIcon;
        }

        /// <summary>
        /// オーバーライドし、更に基底クラスのinitializeを隠蔽
        /// </summary>
        /// <param name="tweet"></param>
        /// <param name="offset_start_x"></param>
        /// <param name="maxWidth"></param>
        public Bitmap StartLayoutLine(TwitterStatus tweet, bool isConversation, int offset_start_x, int maxWidth)
        {
            //  アイコン
            Bitmap tmpIcon = null;
			if (Setting.Timeline.isEnableIcon)
			{
				if (tweet.DynamicTweet.user != null)
					tmpIcon = ImageCache.AutoCache(tweet.user.profile_image_url, true);
				if (tmpIcon == null)
				{
					//  アイコンがキャッシュ待ちです
					cacheWaitingURLs.Add(tweet.user.profile_image_url);
					tmpIcon = (Bitmap)ResourceImages.LoadingImage.Clone();
					//tmpIcon = new Bitmap ( Setting.Timeline.IconSize, Setting.Timeline.IconSize );
				}
			}

            base.StartLayoutLine(tweet, isConversation, offset_start_x, (tmpIcon != null ? tmpIcon.Width : 0 ), maxWidth);
            return tmpIcon;
        }

        public bool DrawBackgroundOnly ( Graphics g, TwitterStatus tweet, bool isConversation, int offset_start_x, int maxWidth, Brush DefaultBackBrush = null )
        {
            if ( this.isLine )
            {
                //  単行表示なら
                return DrawLineTweet ( g, tweet, isConversation, offset_start_x, maxWidth );
            }

            if ( tweet == null || g == null )
                return false;

            //  Start
            var tmpIcon = this.StartLayout ( tweet, isConversation, offset_start_x, maxWidth );
            if ( tmpIcon == null )
                return false;
            var dyn_tweet = tweet.DynamicTweet;

            //  画像はまだ含まれていないが、生成されたセルサイズを拾う
            var p = this.getLayout ();
            p.Icon.initialize ();
            p.ImageTotal.initialize ();
            p.Name.initialize ();
            p.RetweetNotify.initialize ();
            p.StatusesMuch.initialize ();
            p.Time.initialize ();
            p.Via.initialize ();
            draw.DrawInitialize ( g, new Point ( offset_start_x, this.NextStartPositionOffsetY ),
                                                        p, null, maxWidth, Point.Empty );

            Brush BackColor = null;
            if ( DefaultBackBrush != null )
            {
                BackColor = DefaultBackBrush;
            }
            else
            {
                BackColor = GetColor ( tweet );
            }

            draw.DrawBackground ( BackColor );

            if ( Setting.Colors.IsShootingStar )
            {
                g.DrawLine ( new Pen ( Color.FromArgb ( 57, 57, 57 ) ), new Point ( offset_start_x, p.CellRect.Top ), new Point ( maxWidth, p.CellRect.Top ) );
                g.DrawLine ( new Pen ( Color.FromArgb ( 67, 67, 69 ) ), new Point ( offset_start_x, p.CellRect.Top + 1 ), new Point ( maxWidth, p.CellRect.Top + 1 ) );
            }

            var str = new StringFormat ();
            str.Alignment = StringAlignment.Center;
            str.LineAlignment = StringAlignment.Center;
            g.DrawString ( "読み込み中です。。。", Setting.Fonts.TweetFont, (Setting.Colors.IsShootingStar ? Setting.ShootingStarColor.TweetColor : Setting.Colors.TweetColor ), p.CellRect, str );
            if ( Setting.Colors.IsShootingStar )
            {
                g.DrawLine ( new Pen ( Color.FromArgb ( 25, 25, 25 ) ), new Point ( offset_start_x, p.CellRect.Bottom - 1 ), new Point ( maxWidth, p.CellRect.Bottom - 1 ) );
                g.DrawLine ( new Pen ( Color.FromArgb ( 12, 12, 12 ) ), new Point ( offset_start_x, p.CellRect.Bottom ), new Point ( maxWidth, p.CellRect.Bottom ) );
            }
            this.EndLayout ();
            return true;
        }
        /// <summary>
        /// ツイートを描画します。
        /// </summary>
        /// <param name="g"></param>
        /// <param name="tweet"></param>
        /// <param name="icon"></param>
        /// <param name="SetClickLink"></param>
        /// <returns>失敗したらfalse</returns>
        public bool DrawTweet(Graphics g, TwitterStatus tweet, bool isConversation, int offset_start_x, int maxWidth,
                                    SetClickLinkDelegate SetClickLink, SelectControl selectControl, Point MouseHoverLocation,
                                    Brush DefaultBackBrush = null)
        {
            if (this.isLine)
            {
                //  単行表示なら
                return DrawLineTweet(g, tweet, isConversation, offset_start_x, maxWidth);
            }

            if (tweet == null || g == null)
                return false;

            //  Start
            var tmpIcon = this.StartLayout(tweet, isConversation, offset_start_x, maxWidth);
            //if (tmpIcon == null)
            //    return false;
            var dyn_tweet = tweet.DynamicTweet;

            //  画像はまだ含まれていないが、生成されたセルサイズを拾う
            var p = this.getLayout();
            draw.DrawInitialize(g, new Point(offset_start_x, this.NextStartPositionOffsetY),
                                                        p, SetClickLink, maxWidth, MouseHoverLocation);

            Brush BackColor = null;
            if (DefaultBackBrush != null)
            {
                BackColor = DefaultBackBrush;
            }
            else
            {
                BackColor = GetColor ( tweet );
            }

            draw.DrawBackground(BackColor);

            if ( Setting.Colors.IsShootingStar )
            {
                g.DrawLine ( new Pen ( Color.FromArgb ( 57, 57, 57 ) ), new Point ( offset_start_x, p.CellRect.Top ), new Point ( maxWidth, p.CellRect.Top ) );
                g.DrawLine ( new Pen ( Color.FromArgb ( 67, 67, 69 ) ), new Point ( offset_start_x, p.CellRect.Top + 1 ), new Point ( maxWidth, p.CellRect.Top + 1 ) );
            }
            draw.DrawIcon(tmpIcon, tweet.DynamicTweet.user.screen_name);
            draw.DrawName(tweet.DynamicTweet.user.screen_name);
            if (dyn_tweet.user.protected_account)
                draw.DrawProtectedIcon();

            draw.DrawTime(false, tweet.id);

            if (selectControl != null)
            {
                if (selectControl.selCellPosition == tweet.id && selectControl.isMouseDown && !selectControl.selectAll)
                {
                    string tmp_text = tweet.DynamicTweet.text;
                    //  ツイート選択範囲を取得
                    int[] tmp_Pos = DrawTextUtil.GetSelectTextlen(tmp_text, Setting.Fonts.TweetFont, p.Tweet.Position.X,
                                                                                        p.Tweet.Position.Y, maxWidth, selectControl.startDown, selectControl.endDown);
                    if ((tmp_Pos[0] >= 0 && tmp_Pos[0] <= tmp_text.Length - 1) && (tmp_Pos[1] >= 0 && tmp_Pos[1] <= tmp_text.Length - 1))
                    {
                        if (tmp_Pos[0] != tmp_Pos[1])
                        {
                            selectControl.selText = tmp_text.Substring(tmp_Pos[0], tmp_Pos[1] - tmp_Pos[0] + 1);
                            selectControl.selTextPosition = tmp_Pos;
                        }
                    }
                }
            }

            var isTextBool = ((Setting.Timeline.isReplyBold && tweet.DynamicTweet.isReply) || (Setting.Timeline.isNotifyBold && tweet.DynamicTweet.isNotify) ||
                 (Setting.Timeline.isRetweetBold && tweet.DynamicTweet.isRetweet));
            draw.DrawText(dyn_tweet.entities, (selectControl != null && selectControl.selCellPosition == tweet.id ? selectControl.selTextPosition : null),
                            isTextBool, false, true);

            //  画像のインライン表示
            if (dyn_tweet.entities != null)
                draw.DrawImage(dyn_tweet.entities.media);

            //  via表示
            draw.DrawSource(dyn_tweet.source_url);

            //  リツイートされたことを記述
            draw.DrawRetweetNotify(tweet.user.screen_name);

            //	ふぁぼRT数描画
            if (dyn_tweet.retweet_count != 0 || dyn_tweet.favorite_count != 0)
                draw.DrawStatuses(dyn_tweet.favorite_count, dyn_tweet.retweet_count, tweet.retweeted_status != null);

            //  ボタンアイコン
            if (this.isHover)
                draw.DrawButtons(dyn_tweet, (dyn_tweet.isFollowing ? false : true),
                    (dyn_tweet.isFollowing || dyn_tweet.isDirectMessage ? false : true),
                    (dyn_tweet.isFollowing || dyn_tweet.isDirectMessage ? false : true), dyn_tweet.retweeted, dyn_tweet.favorited);

            if ( Setting.Colors.IsShootingStar )
            {
                g.DrawLine ( new Pen ( Color.FromArgb ( 25, 25, 25 ) ), new Point ( offset_start_x, p.CellRect.Bottom - 1 ), new Point ( maxWidth, p.CellRect.Bottom - 1 ) );
                g.DrawLine ( new Pen ( Color.FromArgb ( 12, 12, 12 ) ), new Point ( offset_start_x, p.CellRect.Bottom), new Point ( maxWidth, p.CellRect.Bottom ) );
            }
            this.EndLayout();
            return true;
        }

        /// <summary>
        /// 単行表示
        /// </summary>
        /// <param name="g"></param>
        /// <param name="tweet"></param>
        /// <param name="offset_start_x"></param>
        /// <param name="maxWidth"></param>
        /// <returns></returns>
        public bool DrawLineTweet(Graphics g, TwitterStatus tweet, bool isConversation, int offset_start_x, int maxWidth)
        {
            if (tweet == null || g == null)
                return false;

            //  Start
            var tmpIcon = this.StartLayoutLine(tweet, isConversation, offset_start_x, maxWidth);
            var dyn_tweet = tweet.DynamicTweet;


            //  画像はまだ含まれていないが、生成されたセルサイズを拾う
            var p = this.getLayout();

            draw.DrawInitialize(g, new Point(offset_start_x, this.NextStartPositionOffsetY), p, null, maxWidth, new Point());

            var BackColor = GetColor ( tweet );

            draw.DrawBackground ( BackColor );
            draw.DrawIcon(tmpIcon, tweet.DynamicTweet.user.screen_name);
            draw.DrawName(tweet.DynamicTweet.user.screen_name);
            //if ( dyn_tweet.user.protected_account )
            //    draw.DrawProtectedIcon ();

            draw.DrawTime(true, tweet.id);

            draw.DrawNormalText(true);
            //  via表示
            //draw.DrawSource(dyn_tweet.source);


			if (Setting.Colors.IsShootingStar)
			{
				g.DrawLine(new Pen(Color.FromArgb(25, 25, 25)), new Point(offset_start_x, p.CellRect.Bottom - 1), new Point(maxWidth, p.CellRect.Bottom - 1));
				g.DrawLine(new Pen(Color.FromArgb(12, 12, 12)), new Point(offset_start_x, p.CellRect.Bottom), new Point(maxWidth, p.CellRect.Bottom));
			}
            this.EndLayoutLine();
            return true;
        }

        /// <summary>
        /// 色を取得
        /// </summary>
        /// <param name="tweet"></param>
        /// <returns></returns>
        public Brush GetColor ( TwitterStatus tweet )
        {
            Brush BackColor = null;
            if ( !Setting.Colors.IsShootingStar )
            {
                BackColor = ( this.isSelected ? Setting.Colors.SelectBackgroundColor :
                    tweet.NotifyStatus != null ? Setting.Colors.NotifyTweetBackgroundColor :
                    tweet.isReply ? Setting.Colors.ReplyBackgroundColor :
                    tweet.isDirectMessage ? Setting.Colors.DirectMessageBackgroundColor :
                    tweet.retweeted_status != null ? Setting.Colors.RetweetBackgroundColor :
                    tweet.DynamicTweet.isReply ? Setting.Colors.ReplyBackgroundColor : Setting.Colors.BackgroundColor );
            } else {
                var alpha = Setting.Colors.Alpha;
                BackColor = ( this.isSelected ? new SolidBrush ( Color.FromArgb ( alpha, 68, 68, 68 ) ) :
                        tweet.NotifyStatus != null ? new SolidBrush ( Color.FromArgb ( alpha, 54, 22, 22 ) ) :
                        tweet.isReply ? new SolidBrush ( Color.FromArgb ( alpha, 54, 22, 22 ) ) :
                        tweet.isDirectMessage ? new SolidBrush ( Color.FromArgb ( alpha, 54, 22, 22 ) ) :
                        tweet.retweeted_status != null ?  new SolidBrush ( Color.FromArgb ( alpha, 22, 22, 54 ) ):
                        tweet.DynamicTweet.isReply ?  new SolidBrush ( Color.FromArgb ( alpha, 54, 22, 22 ) ) : new SolidBrush ( Color.FromArgb ( alpha, 38, 38, 38 ) ) );
            }
            return BackColor;
        }
    }
}
