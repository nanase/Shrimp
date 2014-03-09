using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Shrimp.Module.ImageUtil
{
    class ImageGenerateUtil
    {
        /// <summary>
        /// アイコンサイズにリサイズします
        /// </summary>
        /// <param name="bmp">ビットマップデータ</param>
        /// <returns>リサイズした画像</returns>
        public static Bitmap ResizeIcon(Bitmap bmp, bool isIconSize)
        {
            if (bmp == null)
                return null;

            var iconSize = (isIconSize ? new Size(Setting.Timeline.IconSize, Setting.Timeline.IconSize) : new Size(Setting.Timeline.ImageWidth, Setting.Timeline.ImageHeight));

            Bitmap ret = new Bitmap(iconSize.Width, iconSize.Height);
            Graphics g = Graphics.FromImage(ret);

            //補間方法として最近傍補間を指定する
            g.InterpolationMode =
                System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            //画像を縮小して描画する
            g.DrawImage(bmp, 0, 0, iconSize.Width, iconSize.Height);
            g.Dispose();

            return ret;
        }

        /// <summary>
        /// アス比を固定してリサイズします
        /// </summary>
        /// <param name="image"></param>
        /// <param name="dw"></param>
        /// <param name="dh"></param>
        /// <returns></returns>
        public static Bitmap ResizeImage(Bitmap image, double dw, double dh)
        {
            double hi;
            double imagew = image.Width;
            double imageh = image.Height;

            if ((dh / dw) <= (imageh / imagew))
            {
                hi = dh / imageh;
            }
            else
            {
                hi = dw / imagew;
            }
            int w = (int)(imagew * hi);
            int h = (int)(imageh * hi);

            Bitmap result = new Bitmap(w, h);
            Graphics g = Graphics.FromImage(result);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.DrawImage(image, 0, 0, result.Width, result.Height);
            g.Dispose();

            return result;
        }

        /// <summary>
        /// ドロップシャドウを加えた新しいビットマップを作成します。
        /// </summary>
        /// <param name="srcBmp">対象のビットマップ</param>
        /// <param name="blur">ぼかし</param>
        /// <param name="distance">影の距離</param>
        /// <param name="shadowColor">影の色</param>
        /// <param name="baseOffset">描画先オフセット値の格納先</param>
        /// <returns>ドロップシャドウが加えられた新しいビットマップ</returns>
        public static Bitmap AppendDropShadow(Bitmap srcBmp, int blur, Point distance, Color shadowColor, out Point baseOffset)
        {
            baseOffset = new Point(0, 0);
            if (srcBmp == null || blur < 0)
            {
                return null;
            }

            // ドロップシャドウを含めた新しいサイズを算出
            Rectangle srcRect = new Rectangle(0, 0, srcBmp.Width, srcBmp.Height);
            Rectangle shadowRect = srcRect;
            shadowRect.Offset(distance.X, distance.Y);
            Rectangle shadowBlurRect = shadowRect;
            shadowBlurRect.Inflate(blur, blur);
            Rectangle destRect = Rectangle.Union(srcRect, shadowBlurRect);
            baseOffset.X = destRect.X - srcRect.X;
            baseOffset.Y = destRect.Y - srcRect.Y;

            Bitmap destBmp = new Bitmap(destRect.Width, destRect.Height, PixelFormat.Format32bppArgb);

            // 影部分をレンダリングする
            BitmapData destBmpData = destBmp.LockBits(new Rectangle(0, 0, destRect.Width, destRect.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            BitmapData srcBmpData = srcBmp.LockBits(srcRect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

            unsafe
            {
                byte* destLine = (byte*)destBmpData.Scan0 + (shadowBlurRect.Y - destRect.Y) * destBmpData.Stride + (shadowBlurRect.X - destRect.X) * 4;
                byte* srcBeginLine = (byte*)srcBmpData.Scan0 + (-blur - blur) * srcBmpData.Stride + (-blur - blur) * 4;

                int destWidth = shadowBlurRect.Width;
                int destHeight = shadowBlurRect.Height;

                int srcWidth = srcBmp.Width;
                int srcHeight = srcBmp.Height;

                int div = (1 + blur + blur) * (1 + blur + blur);

                byte r = shadowColor.R;
                byte g = shadowColor.G;
                byte b = shadowColor.B;
                float alpha = shadowColor.A / 255.0f;

                int destStride = destBmpData.Stride;
                int srcStride = srcBmpData.Stride;

                for (int destY = 0; destY < destHeight; destY++, destLine += destStride, srcBeginLine += srcStride)
                {
                    byte* dest = destLine;
                    byte* srcBegin = srcBeginLine;
                    for (int destX = 0; destX < destWidth; destX++, dest += 4, srcBegin += 4)
                    {
                        // α値をぼかす
                        int total = 0;
                        byte* srcLine = srcBegin;
                        for (int srcY = destY - blur - blur; srcY <= destY; srcY++, srcLine += srcStride)
                        {
                            if (srcY >= 0 && srcY < srcHeight)
                            {
                                byte* src = srcLine;
                                for (int srcX = destX - blur - blur; srcX <= destX; srcX++, src += 4)
                                {
                                    if (srcX >= 0 && srcX < srcWidth)
                                    {
                                        total += src[3];
                                    }
                                }
                            }
                        }

                        dest[0] = b;
                        dest[1] = g;
                        dest[2] = r;
                        dest[3] = (byte)((total / div) * alpha);
                    }
                }
            }

            srcBmp.UnlockBits(srcBmpData);
            destBmp.UnlockBits(destBmpData);

            // 元の画像を重ねる
            using (Graphics g = Graphics.FromImage(destBmp))
            {
                g.DrawImage(srcBmp, srcRect.X - destRect.X, srcRect.Y - destRect.Y, srcBmp.Width, srcBmp.Height);
            }
            return destBmp;
        }

        public static Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
    }
}
