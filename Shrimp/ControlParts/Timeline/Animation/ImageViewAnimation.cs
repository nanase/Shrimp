using System;
using System.Drawing;

namespace Shrimp.ControlParts.Timeline.Animation
{
    class ImageViewAnimation : IAnimation, IDisposable
    {
        private bool _Enable;

        public void StartAnimation(object[] obj = null)
        {
            if (!Enabled)
            {
                //
            }
        }

        public void StopAnimation()
        {
            if (Enabled)
            {
                //
            }
        }

        public bool FrameExecute()
        {
            throw new NotImplementedException();
        }

        public void Draw ( System.Drawing.Graphics g, int maxWidth, Rectangle clipRectangle, Draw.TweetDraw.SetClickLinkDelegate setClickLink, object obj )
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool Enabled
        {
            get
            {
                return this._Enable;
            }
            set
            {
                this._Enable = value;
            }
        }
    }
}
