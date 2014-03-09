﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Shrimp.ControlParts
{
    interface IControl
    {
        Bitmap CaptureControl ();
        void StartTabChangeAnimation ( Bitmap BeforeControl, Bitmap AfterControl, bool tabLeftRight, bool tabVertical );
        void Resume ();
        void Suspend ();
    }
}
