using System.Collections.Generic;
using System.Drawing;
using Shrimp.Module.ImageUtil;
using Shrimp.Module.Parts;
using Shrimp.Setting;
using Shrimp.Twitter.Entities;
using Shrimp.Twitter.Status;

namespace Shrimp.ControlParts.Timeline.Draw.Cells
{
    class DrawTweetCell : IDrawCell
    {
        Graphics g;
        DrawCellSize drawCellSize;
        Rectangle cellRect;
        Point MouseHoverLocation;
        int maxWidth;
        ControlParts.Timeline.Draw.TweetDraw.SetClickLinkDelegate SetClickLink;
        /// <summary>
        /// 書き込み字に
        /// </summary>
        /// <param name="g"></param>
        /// <param name="offset"></param>
        /// <param name="drawCellSize"></param>
        /// <param name="maxWidth"></param>
        public void DrawInitialize(Graphics g, Point offset, DrawCellSize drawCellSize, ControlParts.Timeline.Draw.TweetDraw.SetClickLinkDelegate SetClickLink, int maxWidth, Point MouseHover)
        {
            this.g = g;
            this.drawCellSize = drawCellSize;
            this.maxWidth = maxWidth;
            this.SetClickLink = SetClickLink;
            this.cellRect = new Rectangle(offset, new Size(maxWidth, drawCellSize.CellSize - 1));
            this.MouseHoverLocation = MouseHover;
        }
        /// <summary>
        /// 背景描画
        /// </summary>
        public void DrawBackground(Brush brush)
        {
            g.FillRectangle(brush, this.cellRect);
        }

        /// <summary>
        /// アイコン描画
        /// </summary>
        public void DrawIcon(Bitmap image, string screen_name)
        {
			if (image != null)
			{
				g.DrawImage(image, this.drawCellSize.Icon.Rect);
				image.Dispose();
				if (SetClickLink != null)
					SetClickLink.Invoke(this.drawCellSize.Icon.Rect, new TwitterEntitiesPosition(screen_name, "mention"));
			}
        }

        /// <summary>
        /// 名前描画
        /// </summary>
        public void DrawName(string screen_name)
        {
            g.DrawString(this.drawCellSize.Name.Detail, this.drawCellSize.Name.TextFont, this.drawCellSize.Name.TextBrush, this.drawCellSize.Name.Rect);
            if (SetClickLink != null)
                SetClickLink.Invoke(this.drawCellSize.Name.Rect, new TwitterEntitiesPosition(screen_name, "mention"));
        }

        /// <summary>
        /// 鍵アカウントの名前描画
        /// </summary>
        /// <param name="g"></param>
        /// <param name="cellRect"></param>
        public void DrawProtectedIcon()
        {
            g.DrawImage(Setting.ResourceImages.Protected, new Rectangle(this.drawCellSize.Name.Rect.Right, this.drawCellSize.Name.Rect.Top, 16, 16));
        }

        /// <summary>
        /// 時間を描画
        /// </summary>
        /// <param name="useCellInfo">セル変数の情報を優先して使用するかどうか</param>
        public void DrawTime(bool useCellInfo, decimal tweet_id)
        {
            //  new Point ( maxWidth - p.Time.Size.Width, p.Time.Position.Y )
            if (!useCellInfo)
                this.drawCellSize.Time.Position = new Point(this.maxWidth - this.drawCellSize.Time.Size.Width, this.drawCellSize.Time.Position.Y);
            g.DrawString ( this.drawCellSize.Time.Detail, this.drawCellSize.Time.TextFont, this.drawCellSize.Time.TextBrush, this.drawCellSize.Time.Rect );
            if (SetClickLink != null && Setting.Timeline.isEnableTimeLink)
                SetClickLink.Invoke(this.drawCellSize.Time.Rect, new TwitterEntitiesPosition("https://twitter.com/shrimp/status/" + tweet_id + "", "url"));

        }

        /// <summary>
        /// テキストを描画します
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="selTextPosition"></param>
        /// <param name="isBoldText"></param>
        /// <param name="isTrimIndent"></param>
        public void DrawText(TwitterEntities entities, int[] selTextPosition, bool isBoldText, bool isTrimIndent, bool isDrawShootingStar)
        {
            //  ツイート描画
            var cellOffset = this.drawCellSize.Tweet.Position;
            int strX = cellOffset.X, strY = cellOffset.Y;
            int num = 0;
            Brush cl;
            foreach (char t in this.drawCellSize.Tweet.Detail)
            {
                var one_size = DrawTextUtil.GetDrawTextSize(t.ToString(), this.drawCellSize.Tweet.TextFont, maxWidth, true);

                cl = this.drawCellSize.Tweet.TextBrush;
                bool isLink = false;
                if (entities != null)
                {
                    TwitterEntitiesPosition entities_pos = TwitterEntitiesUtil.getEntitiesPosition(entities, num);
                    if (entities_pos != null)
                    {
                        isLink = true;
                        if ( !isDrawShootingStar && !Setting.Colors.IsShootingStar)
                            cl = Setting.Colors.LinkColor;
                        if (SetClickLink != null)
                            SetClickLink.Invoke(new Rectangle(new Point(strX, strY), one_size), entities_pos);
                    }
                }

                if (selTextPosition != null)
                {
                    if (selTextPosition[0] <= num && selTextPosition[1] >= num)
                    {
                        g.FillRectangle(Brushes.BlueViolet, new Rectangle(strX + 1, strY + 1, one_size.Width + 1, one_size.Height));
                        cl = Brushes.White;
                    }
                }

                var font = (isBoldText ? (isLink ? Setting.Fonts.TweetUnderLineBoldFont : Setting.Fonts.TweetFontBold) : (isLink ? Setting.Fonts.TweetUnderLineFont : Setting.Fonts.TweetFont));
                g.DrawString(t.ToString(), font, cl, new PointF(strX, (float)strY));
                //  位置
                strX += one_size.Width + Setting.Timeline.TweetPadding;
                if (strX + one_size.Width >= maxWidth || t == '\n')
                {
                    //
                    strX = cellOffset.X;
                    strY += one_size.Height;
                }

                num++;
            }
        }

        /// <summary>
        /// 通常描画
        /// </summary>
        public void DrawNormalText( bool isTrimIndent )
        {
            var opt = new StringFormat() { Trimming = StringTrimming.EllipsisCharacter };
            string text = ( this.drawCellSize.Tweet.Detail == null ? "" : this.drawCellSize.Tweet.Detail );
            if ( isTrimIndent )
                text = text.Replace ( "\r", " " ).Replace ( "\n", " " );
            g.DrawString ( text, this.drawCellSize.Tweet.TextFont, this.drawCellSize.Tweet.TextBrush, this.drawCellSize.Tweet.Rect, opt );
        }

        /// <summary>
        /// 画像
        /// </summary>
        /// <param name="media"></param>
        public void DrawImage(List<TwitterEntitiesMedia> media)
        {
			if (media != null && Setting.Timeline.isEnableInlineView)
            {
                foreach (TwitterEntitiesMedia data in media)
                {
                    Bitmap inline_pic = ImageCache.AutoCache(data.media_url, false);
                    if (inline_pic != null)
                    {
						var rect = new Rectangle(this.drawCellSize.ImageTotal.Position, inline_pic.Size);
                        g.DrawImage(inline_pic, rect);
						if (SetClickLink != null)
							SetClickLink.Invoke(rect, new TwitterEntitiesPosition ( data.media_url, "inlineImage" ) );
                        inline_pic.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// リツイート
        /// </summary>
        public void DrawRetweetNotify(string screen_name)
        {
            if (this.drawCellSize.RetweetNotify.Detail != null)
            {
                g.DrawImage(ResourceImages.Retweet.normal, new Rectangle(new Point(this.drawCellSize.RetweetNotify.Position.X - Setting.Timeline.RetweetMarkSize, this.drawCellSize.RetweetNotify.Position.Y), new Size(Setting.Timeline.RetweetMarkSize, Setting.Timeline.RetweetMarkSize)));
                g.DrawString ( this.drawCellSize.RetweetNotify.Detail, this.drawCellSize.RetweetNotify.TextFont, this.drawCellSize.RetweetNotify.TextBrush, this.drawCellSize.RetweetNotify.Position );
                if (SetClickLink != null && Setting.Timeline.isEnableRetweetLink)
                    SetClickLink.Invoke(this.drawCellSize.RetweetNotify.Rect, new TwitterEntitiesPosition(screen_name, "mention"));

            }
        }

        /// <summary>
        /// via
        /// </summary>
        /// <param name="source"></param>
        /// <param name="SetClickLink"></param>
        public void DrawSource(string source)
        {
            g.DrawString ( this.drawCellSize.Via.Detail, this.drawCellSize.Via.TextFont, this.drawCellSize.Via.TextBrush, this.drawCellSize.Via.Position );
            if (SetClickLink != null && Setting.Timeline.isEnableSourceLink)
                SetClickLink.Invoke(this.drawCellSize.Via.Rect, new TwitterEntitiesPosition(source, "url"));
        }

        public void DrawStatuses(decimal fav, decimal rt, bool isRetweeted)
        {
            Point tmp = this.drawCellSize.StatusesMuch.Position;
            if ((isRetweeted && rt != 1) || (!isRetweeted && rt != 0))
            {
                g.DrawImage(ResourceImages.Retweet.normal, new Rectangle(this.drawCellSize.StatusesMuch.Position, new Size(Setting.Timeline.RetweetMarkSize, Setting.Timeline.RetweetMarkSize)));
                tmp.X += Setting.Timeline.RetweetMarkSize;

                g.DrawString ( "" + rt + "", this.drawCellSize.StatusesMuch.TextFont, this.drawCellSize.StatusesMuch.TextBrush, tmp );
                tmp.X += (int)g.MeasureString("" + rt + "", Setting.Fonts.RetweetNotify).Width + 5;
            }
            if (fav != 0)
            {
                g.DrawImage(ResourceImages.Fav.normal, new Rectangle(tmp, new Size(Setting.Timeline.RetweetMarkSize, Setting.Timeline.RetweetMarkSize)));
                tmp.X += Setting.Timeline.RetweetMarkSize;
                g.DrawString ( "" + fav + "", this.drawCellSize.StatusesMuch.TextFont, this.drawCellSize.StatusesMuch.TextBrush, tmp );
            }
        }


        /// <summary>
        /// ボタン
        /// </summary>
        /// <param name="SetClickLink"></param>
        public void DrawButtons(TwitterStatus tweet, bool CanReply, bool CanFavorite, bool CanRetweet, bool isAlreadyRetweeted, bool isAlreadyFaved)
        {
            Image image = (this.drawCellSize.Buttons.ReplyIconRect.Contains(MouseHoverLocation) ? ResourceImages.Reply.hover : ResourceImages.Reply.normal);
            g.DrawImage(image, this.drawCellSize.Buttons.ReplyIconRect);
            if (SetClickLink != null && CanReply)
                SetClickLink.Invoke ( this.drawCellSize.Buttons.ReplyIconRect, new TwitterEntitiesPosition ( "" + tweet.id + "", "replyButton" ) );

            image = (this.drawCellSize.Buttons.RetweetIconRect.Contains(MouseHoverLocation) || isAlreadyRetweeted ? ResourceImages.Retweet.hover : ResourceImages.Retweet.normal);
            g.DrawImage(image, this.drawCellSize.Buttons.RetweetIconRect);
            if (SetClickLink != null && CanRetweet)
                SetClickLink.Invoke(this.drawCellSize.Buttons.RetweetIconRect, new TwitterEntitiesPosition("" + tweet.id + "", "retweetButton"));

            image = (this.drawCellSize.Buttons.FavIconRect.Contains(MouseHoverLocation) || isAlreadyFaved ? ResourceImages.Fav.hover : ResourceImages.Fav.normal);
            g.DrawImage(image, this.drawCellSize.Buttons.FavIconRect);
            if (SetClickLink != null && CanFavorite)
                SetClickLink.Invoke ( this.drawCellSize.Buttons.FavIconRect, new TwitterEntitiesPosition ( "" + tweet.id + "", "favButton" ) );
            //SetClickLink.Invoke ( this.drawCellSize.Via.Rect, new TwitterEntitiesPosition ( sou, "url" ) );
        }
    }
}
