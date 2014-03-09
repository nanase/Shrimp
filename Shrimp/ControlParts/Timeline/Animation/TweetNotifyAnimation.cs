using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Shrimp.Module.Parts;
using System.Drawing;
using Shrimp.Twitter.Entities;

namespace Shrimp.ControlParts.Timeline.Animation
{
    class TweetNotifyAnimation : IAnimation, IDisposable
    {
        #region 定義
        //private Timer timer;
        //  アニメーション
        //private ElapsedEventHandler handler;
        #endregion

        #region コンストラクタ
        public TweetNotifyAnimation ( )
        {
            this.ShowEnable = true;
            /*
            this.timer = new System.Timers.Timer ();
            this.timer.Interval = 16;
            this.handler = handler;
            this.timer.Elapsed += this.handler;
            */
        }

        ~TweetNotifyAnimation ()
        {
            /*
            this.timer.Stop ();
            this.timer.Elapsed -= this.handler;
            */
        }

        public void Dispose ()
        {
            GC.SuppressFinalize ( this );
        }
        #endregion


        /// <summary>
        /// アニメーション開始
        /// </summary>
        public void StartAnimation ( object[] obj = null )
        {
            
        }

         /// <summary>
        /// アニメーションストップ
        /// </summary>
        public void StopAnimation ()
        {
            //this.TimerEnable = false;
        }

        /// <summary>
        /// 隠す
        /// </summary>
        public void Hide ()
        {
            this.ShowEnable = false;
            this.Frame = 0;
            this.YOffset = 0;
        }

        /// <summary>
        /// 舞フレームごとに行うデリゲート処理
        /// </summary>
        /// <returns></returns>
        public bool FrameExecute ()
        {
            if ( this.notifyText != null )
            {
                if ( !this.Enable )
                {
                    if ( this.Frame > 0 )
                    {
                        this.Frame--;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if ( this.Frame < 16 )
                    {
                        this.Frame++;
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
            this.notifyText = null;

            return false;
        }

        /// <summary>
        /// フレーム数
        /// </summary>
        public int Frame
        {
            get; set;
        }

        /// <summary>
        /// trueのときはでてくる
        /// </summary>
        public bool Enable
        {
            get;
            set;
        }

        /// <summary>
        /// 表示自体が有効か無効か
        /// </summary>
        public bool ShowEnable
        {
            get;
            set;
        }

        /// <summary>
        /// Y位置のオフセット
        /// </summary>
        public int YOffset
        {
            get; set;
        }

        /// <summary>
        /// オフセット位置を計算する
        /// </summary>
        public int StartDrawOffset
        {
            get {
                return (int)( (double)-this.YOffset + Math.Sin ( Math.PI / 2 / 16 * ( this.Frame ) ) * this.YOffset );
            }
        }
        
        /// <summary>
        /// テキスト
        /// </summary>
        public string notifyText
        {
            get;
            set;
        }

        /// <summary>
        /// オフセットツイートID
        /// </summary>
        public decimal offsetTweetID
        {
            get;
            set;
        }

        public void Draw ( Graphics g, int maxWidth, ControlParts.Timeline.Draw.TweetDraw.SetClickLinkDelegate setClickLink )
        {
            if ( this.ShowEnable && this.notifyText != null )
            {
                //string notifyText = "新着ツイート\n" + ( this.newTweetNum ) + "件";
                if ( this.YOffset == 0 )
                    this.YOffset = ( DrawTextUtil.GetDrawTextSizeTrim ( "t", Setting.Fonts.NameFont, maxWidth ).Height * 2 ) + 2;
                int drawNotifyStartY = this.StartDrawOffset;
                var cRect = new Rectangle ( 0, drawNotifyStartY, maxWidth, this.YOffset );
                g.FillRectangle ( Setting.Colors.NotifyBackgroundColor, cRect );
                bool isFirst = false;
                foreach ( string text in notifyText.Split ( '\n' ) )
                {
                    Size s = DrawTextUtil.GetDrawTextSizeTrim ( text, Setting.Fonts.NameFont, maxWidth );
                    StringFormat sf = StringFormat.GenericDefault;
                    sf.Trimming = StringTrimming.EllipsisCharacter;
                    sf.FormatFlags = StringFormatFlags.NoWrap;
                    g.DrawString ( text, Setting.Fonts.NameFont, Setting.Colors.NotifyStringColor, new Rectangle ( new Point ( isFirst ? 5 : ( maxWidth - s.Width ) / 2, drawNotifyStartY ), new Size ( maxWidth, s.Height ) ), sf );
                    drawNotifyStartY += s.Height;
                    if ( isFirst )
                        break;
                    isFirst = true;
                }
                if ( setClickLink != null )
                    setClickLink ( cRect, new TwitterEntitiesPosition ( offsetTweetID.ToString (),  "notify" ) );
            }
        }
    }
}
