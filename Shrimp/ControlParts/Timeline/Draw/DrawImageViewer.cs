using System;
using System.Drawing;
using System.Drawing.Imaging;
using Shrimp.ControlParts.Timeline.Animation;

namespace Shrimp.ControlParts.Timeline.Draw
{
	//	TimelineControl内のイメージビューアを描画するためのクラス
	class DrawImageViewer : IAnimation
	{
		private SolidBrush brush;
		public DrawImageViewer()
		{
			this.brush = new SolidBrush(Color.FromArgb(0, 0, 0, 0));

			//ImageAttributesを使用して画像を描画
			//g.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height),
			//	0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia);
			this.Frame = 0;
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

		public void ChangeTransparent(int num)
		{
			if (this.brush != null)
				this.brush.Dispose();
			this.brush = new SolidBrush(Color.FromArgb(num, 0, 0, 0));
		}

		public void StartAnimation(object[] obj = null)
		{
			this.Enable = true;
			this.Frame = 0;
		}

		public void StopAnimation()
		{
			this.Enable = false;
			this.Frame = 0;
		}

		public bool FrameExecute()
		{
			if (this.Enable && this.Frame < 16)
			{
				Frame++;
				var num = (int)(((double)Frame / 16.0) * 255);
				ChangeTransparent(num);
				return true;
			}
			else
			{
				if (!this.Enable && this.Frame > 0)
				{
					this.Frame--;
					var num = (int)(((double)Frame / 16.0) * 255);
					ChangeTransparent(num);
				}
				else
				{
					this.Enable = false;
					this.Frame = 0;
				}
			}
			return false;
		}

		public void Draw(Graphics g, int maxWidth, Rectangle clipRectangle, TweetDraw.SetClickLinkDelegate setClickLink, object obj)
		{
			g.FillRectangle(this.brush, clipRectangle );
			//	0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia);
		}
	}
}
