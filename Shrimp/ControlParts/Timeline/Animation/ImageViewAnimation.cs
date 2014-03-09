using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shrimp.ControlParts.Timeline.Animation
{
    class ImageViewAnimation : IAnimation, IDisposable
    {
        private bool _Enable;

        public void StartAnimation ( object[] obj = null )
        {
            if ( !Enabled )
            {
                //
            }
        }

        public void StopAnimation ()
        {
            if ( Enabled )
            {
                //
            }
        }

        public bool FrameExecute ()
        {
            throw new NotImplementedException ();
        }

        public void Draw ( System.Drawing.Graphics g, int maxWidth, Draw.TweetDraw.SetClickLinkDelegate setClickLink )
        {
            throw new NotImplementedException ();
        }

        public void Dispose ()
        {
            throw new NotImplementedException ();
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
