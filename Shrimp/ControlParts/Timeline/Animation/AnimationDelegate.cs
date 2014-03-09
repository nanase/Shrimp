using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shrimp.ControlParts.Timeline.Animation
{
    /// <summary>
    /// アニメーションデリゲートを管理するクラス
    /// </summary>
    class AnimationDelegate
    {
        public ControlParts.Timeline.Animation.AnimationControl.FrameExecuteDelegate frame_deleage;
        public int Interval = 0;
    }
}
