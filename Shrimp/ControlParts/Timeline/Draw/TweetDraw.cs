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
            if (!base.StartLayout(tweet, isConversation, offset_start_x, tmpIcon.Width, maxWidth))
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
            if (tweet.DynamicTweet.user != null)
                tmpIcon = ImageCache.AutoCache(tweet.user.profile_image_url, true);
            if (tmpIcon == null)
            {
                //  アイコンがキャッシュ待ちです
                cacheWaitingURLs.Add(tweet.user.profile_image_url);
                tmpIcon = (Bitmap)ResourceImages.LoadingImage.Clone();
                //tmpIcon = new Bitmap ( Setting.Timeline.IconSize, Setting.Timeline.IconSize );
            }

            base.StartLayoutLine(tweet, isConversation, offset_start_x, tmpIcon.Width, maxWidth);
            return tmpIcon;
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
            if (tmpIcon == null)
                return false;
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
                BackColor = (this.isSelected ? Setting.Colors.SelectBackgroundColor :
                    tweet.NotifyStatus != null ? Setting.Colors.NotifyTweetBackgroundColor :
                    tweet.isReply ? Setting.Colors.ReplyBackgroundColor :
                    tweet.isDirectMessage ? Setting.Colors.DirectMessageBackgroundColor :
                    tweet.retweeted_status != null ? Setting.Colors.RetweetBackgroundColor :
                    tweet.DynamicTweet.isReply ? Setting.Colors.ReplyBackgroundColor : Setting.Colors.BackgroundColor);
            }

            draw.DrawBackground(BackColor);
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
                            isTextBool);

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

            Brush Backcolor = (this.isSelected ? Setting.Colors.SelectBackgroundColor :
                tweet.NotifyStatus != null ? Setting.Colors.NotifyTweetBackgroundColor :
                tweet.isReply ? Setting.Colors.ReplyBackgroundColor :
                tweet.isDirectMessage ? Setting.Colors.DirectMessageBackgroundColor :
                tweet.retweeted_status != null ? Setting.Colors.RetweetBackgroundColor :
                tweet.DynamicTweet.isReply ? Setting.Colors.ReplyBackgroundColor : Setting.Colors.BackgroundColor);
            draw.DrawBackground(Backcolor);
            draw.DrawIcon(tmpIcon, tweet.DynamicTweet.user.screen_name);
            draw.DrawName(tweet.DynamicTweet.user.screen_name);
            //if ( dyn_tweet.user.protected_account )
            //    draw.DrawProtectedIcon ();

            draw.DrawTime(true, tweet.id);

            draw.DrawNormalText();
            //  via表示
            draw.DrawSource(dyn_tweet.source);

            this.EndLayoutLine();
            return true;
        }
    }
}
