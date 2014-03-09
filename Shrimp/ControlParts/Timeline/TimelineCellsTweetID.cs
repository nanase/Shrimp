using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shrimp.ControlParts.Timeline
{
    /// <summary>
    /// TimelineCellsで使われる、ツイート保存管理
    /// </summary>
    public class TimelineCellsTweetID
    {
        public readonly decimal id;
        public readonly bool isConversation = false;

        public TimelineCellsTweetID ( decimal id, bool isConv )
        {
            this.id = id;
            this.isConversation = isConv;
        }
    }
}
