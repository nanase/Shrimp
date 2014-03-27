using System;
using System.Drawing;
using Shrimp.ControlParts.Toolstrip;
using System.Drawing.Text;

namespace Shrimp.ControlParts.Timeline.Animation
{
    class ToolStripAnimation : IDisposable, IAnimation
    {
        private bool _Enable = false;
        /// <summary>
        /// アニメーションのフレーム
        /// </summary>
        private int FrameCount = 0;
        /// <summary>
        /// 移動する量
        /// </summary>
        private int MoveHeight = 0;
        //
        private ToolStripStatusLabelText beforeText, Text;

        /// <summary>
        /// アニメーションを開始します
        /// </summary>
        /// <param name="obj"></param>
        public void StartAnimation ( object[] obj = null )
        {
            //  obj[0] = 移動量（フォントのYサイズでいいや)
            if ( !this.Enable )
            {
                this.MoveHeight = (int)obj[0];
                if ( this.Text != null )
                    this.beforeText = this.Text;
                this.Text = (ToolStripStatusLabelText)obj[1];
                this._Enable = true;
            }
        }

        /// <summary>
        /// アニメーションを停止します
        /// </summary>
        public void StopAnimation ()
        {
            this._Enable = false;
            this.FrameCount = 0;
        }

        /// <summary>
        /// アニメーションを描画します
        /// </summary>
        /// <param name="g"></param>
        /// <param name="maxWidth"></param>
        /// <param name="setClickLink"></param>
        public void Draw ( Graphics g, int maxWidth, Rectangle clipRectangle, ControlParts.Timeline.Draw.TweetDraw.SetClickLinkDelegate setClickLink, object obj )
        {
            g.Clear ( SystemColors.Control );
            //StringFormatを作成
            StringFormat sf = new StringFormat ();
            //文字を真ん中に表示
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            sf.FormatFlags = StringFormatFlags.NoWrap;
            g.TextRenderingHint = TextRenderingHint.SystemDefault;
            Font fnt = obj as Font;
            if ( this.Text == null )
            {
                g.DrawString ( "Welcome to Shrimp!!", fnt, Brushes.Black, clipRectangle, sf );
            }
            else
            {
                SizeF size = g.MeasureString ( this.Text.text, SystemFonts.DefaultFont, clipRectangle.Width );
                g.DrawString ( this.Text.text, fnt, Brushes.Black, new Rectangle ( clipRectangle.X, ( clipRectangle.Y + StartDrawOffset ) - (int)size.Height, clipRectangle.Width, clipRectangle.Height ), sf );
                if ( this.beforeText != null )
                    g.DrawString ( this.beforeText.text, fnt, Brushes.Black, new Rectangle ( clipRectangle.X, clipRectangle.Y + StartDrawOffset, clipRectangle.Width, clipRectangle.Height ), sf );
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
        public void Dispose ()
        {
            this.StopAnimation ();
        }

        /// <summary>
        /// オフセット位置を計算します
        /// </summary>
        public int StartDrawOffset
        {
            get
            {
                return (int)( Math.Sin ( Math.PI / 2 / 16 * ( this.FrameCount ) ) * this.MoveHeight );
            }
        }

        /// <summary>
        /// 毎フレーム毎に行われる計算をいたします
        /// </summary>
        /// <returns></returns>
        public bool FrameExecute ()
        {
            if ( this.Enable && this.FrameCount < 16 )
            {
                this.FrameCount++;
                return true;
            }
            else
            {
                if ( this.Enable )
                {
                    this._Enable = false;
                    this.FrameCount = 0;
                    this.beforeText = this.Text;
                    return true;
                }
            }
            return false;
        }
    }
}
