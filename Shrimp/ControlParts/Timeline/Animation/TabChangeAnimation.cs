using System;
using System.Drawing;
using Shrimp.Setting;

namespace Shrimp.ControlParts.Timeline.Animation
{
    class TabChangeAnimation : IAnimation, IDisposable
    {
        private bool _Enable = false;
        /// <summary>
        /// アニメーションのフレーム
        /// </summary>
        private int FrameCount = 0;
        /// <summary>
        /// 移動する量
        /// </summary>
        private int MoveWidth = 0;
        /// <summary>
        /// タブがどっちへ進むか
        /// </summary>
        private bool tabLeftRight = false;
        /// <summary>
        /// タブが横にあれば、横に移動するし、左右に配置されているなら、上下に移動するようにする
        /// </summary>
        private bool tabVertical = false;
        private Bitmap BeforeControl, AfterControl;

        /// <summary>
        /// アニメーションを開始します
        /// </summary>
        /// <param name="obj"></param>
        public void StartAnimation(object[] obj = null)
        {
            if (!this.Enable)
            {
                this.BeforeControl = (Bitmap)((Bitmap)obj[0]).Clone();
                this.AfterControl = (Bitmap)((Bitmap)obj[1]).Clone();
                this.tabLeftRight = (bool)obj[2];
                this.tabVertical = (bool)obj[3];
                this.MoveWidth = this.BeforeControl.Width;
                this._Enable = true;
            }
        }

        /// <summary>
        /// アニメーションを停止します
        /// </summary>
        public void StopAnimation()
        {
            this._Enable = false;
            this.FrameCount = 0;
            if (this.BeforeControl != null)
                this.BeforeControl.Dispose();
            if (this.AfterControl != null)
                this.AfterControl.Dispose();
            this.BeforeControl = null;
            this.AfterControl = null;
        }

        /// <summary>
        /// アニメーションを描画します
        /// </summary>
        /// <param name="g"></param>
        /// <param name="maxWidth"></param>
        /// <param name="setClickLink"></param>
        public void Draw ( Graphics g, int maxWidth, Rectangle clipRectangle, ControlParts.Timeline.Draw.TweetDraw.SetClickLinkDelegate setClickLink, object obj )
        {
            if (Setting.Timeline.TabChangeAnimation == TabAnimation.Move)
            {
                int offset = StartDrawOffset;
                if (this.tabLeftRight)
                {
                    if (this.tabVertical)
                    {
                        //  上下
                        g.DrawImage(AfterControl, new Point(0, offset - AfterControl.Width));
                        g.DrawImage(BeforeControl, new Point(0, offset));
                    }
                    else
                    {
                        g.DrawImage(AfterControl, new Point(offset - AfterControl.Width, 0));
                        g.DrawImage(BeforeControl, new Point(offset, 0));
                    }
                }
                else
                {
                    if (this.tabVertical)
                    {
                        //  上下
                        g.DrawImage(BeforeControl, new Point(0, -offset));
                        g.DrawImage(AfterControl, new Point(0, -offset + BeforeControl.Width));
                    }
                    else
                    {
                        g.DrawImage(BeforeControl, new Point(-offset, 0));
                        g.DrawImage(AfterControl, new Point(-offset + BeforeControl.Width, 0));
                    }
                }
            }
            else if (Setting.Timeline.TabChangeAnimation == TabAnimation.Fade)
            {
                float offset = StartDrawFadeout;
                //imgを半透明にしてtransImgに描画
                System.Drawing.Imaging.ColorMatrix cm =
                    new System.Drawing.Imaging.ColorMatrix();
                cm.Matrix00 = 1;
                cm.Matrix11 = 1;
                cm.Matrix22 = 1;
                cm.Matrix33 = 1 - offset;
                cm.Matrix44 = 1;
                System.Drawing.Imaging.ImageAttributes ia =
                    new System.Drawing.Imaging.ImageAttributes();
                ia.SetColorMatrix(cm);
                g.DrawImage(BeforeControl, new Rectangle(0, 0, BeforeControl.Width, BeforeControl.Height), 0f, 0f, (float)BeforeControl.Width, (float)BeforeControl.Height, GraphicsUnit.Pixel, ia);
                System.Drawing.Imaging.ColorMatrix cm2 =
        new System.Drawing.Imaging.ColorMatrix();
                cm2.Matrix00 = 1;
                cm2.Matrix11 = 1;
                cm2.Matrix22 = 1;
                cm2.Matrix33 = offset;
                cm2.Matrix44 = 1;
                System.Drawing.Imaging.ImageAttributes ia2 =
                    new System.Drawing.Imaging.ImageAttributes();
                ia2.SetColorMatrix(cm2);
                g.DrawImage(AfterControl, new Rectangle(0, 0, AfterControl.Width, AfterControl.Height), 0f, 0f, (float)AfterControl.Width, (float)AfterControl.Height, GraphicsUnit.Pixel, ia2);
            }

        }

        /// <summary>
        /// 有効かどうか
        /// </summary>
        public bool Enable
        {
            get { return this._Enable; }
        }

        /// <summary>
        /// オブジェクトを破棄するときに呼び出されます
        /// </summary>
        public void Dispose()
        {
            this.StopAnimation();
        }

        /// <summary>
        /// オフセット位置を計算します
        /// </summary>
        public int StartDrawOffset
        {
            get
            {
                return (int)(Math.Sin(Math.PI / 2 / 16 * (this.FrameCount)) * this.MoveWidth);
            }
        }

        /// <summary>
        /// オフセット位置を計算します
        /// </summary>
        public float StartDrawFadeout
        {
            get
            {
                return ((float)Math.Sin(Math.PI / 2 / 16 * (this.FrameCount)) * 1f);
            }
        }

        /// <summary>
        /// 毎フレーム毎に行われる計算をいたします
        /// </summary>
        /// <returns></returns>
        public bool FrameExecute()
        {
            if (this.Enable && this.FrameCount < 16)
            {
                this.FrameCount++;
                return true;
            }
            else
            {
                if (this.Enable)
                {
                    this._Enable = false;
                    this.FrameCount = 0;
                    return true;
                }
            }
            return false;
        }
    }
}
