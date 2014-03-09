using System.Drawing;

namespace Shrimp.ControlParts
{
    interface IControl
    {
        Bitmap CaptureControl();
        void StartTabChangeAnimation(Bitmap BeforeControl, Bitmap AfterControl, bool tabLeftRight, bool tabVertical);
        void Resume();
        void Suspend();
    }
}
