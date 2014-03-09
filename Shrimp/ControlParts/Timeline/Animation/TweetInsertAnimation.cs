using System;
using System.Drawing;

namespace Shrimp.ControlParts.Timeline.Animation
{
    class TweetInsertAnimation : IAnimation, IDisposable
    {
        #region 定義
        //private Timer timer;
        #endregion

        #region コンストラクタ
        public TweetInsertAnimation()
        {
        }

        ~TweetInsertAnimation()
        {
            this.Dispose();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion


        /// <summary>
        /// アニメーション開始
        /// </summary>
        public void StartAnimation(object[] obj = null)
        {
            this.Enable = true;
            this.Frame = 0;
            this.YOffset = 0;
            //this.timer.Start ();
        }

        /// <summary>
        /// アニメーションストップ
        /// </summary>
        public void StopAnimation()
        {
            this.Enable = false;
            this.Frame = 0;
            this.YOffset = 0;
            //this.timer.Stop ();
        }

        /// <summary>
        /// 舞フレームごとに行うデリゲート処理
        /// </summary>
        /// <returns></returns>
        public bool FrameExecute()
        {
            if (this.Enable && this.Frame < 16)
            {
                Frame++;
                return true;
            }
            else
            {
                this.Enable = false;
                this.Frame = 0;
                this.YOffset = 0;
            }
            return false;
        }

        /// <summary>
        /// フレーム数
        /// </summary>
        public int Frame
        {
            get;
            set;
        }

        /// <summary>
        /// 有効か無効か
        /// </summary>
        public bool Enable
        {
            get;
            set;
        }

        /// <summary>
        /// Y位置のオフセット
        /// </summary>
        public int YOffset
        {
            get;
            set;
        }

        /// <summary>
        /// タイムラインでの描画用（計算された物)
        /// </summary>
        public int StartPositionOffset
        {
            get
            {
                return (int)((double)-this.YOffset + Math.Sin(Math.PI / 2 / 16 * (this.Frame)) * this.YOffset);
            }
        }

        public void Draw(Graphics g, int maxWidth, ControlParts.Timeline.Draw.TweetDraw.SetClickLinkDelegate del)
        {
        }

    }
}
