using System.Drawing;

namespace Shrimp.ControlParts.Timeline.Animation
{
    interface IAnimation
    {
        void StartAnimation(object[] obj = null);
        void StopAnimation();
        bool FrameExecute();
        void Draw(Graphics g, int maxWidth, ControlParts.Timeline.Draw.TweetDraw.SetClickLinkDelegate setClickLink);
    }
}
