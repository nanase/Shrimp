using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shrimp.Twitter.Status;

namespace Shrimp.Module
{
    /// <summary>
    /// 送るツイートの情報
    /// </summary>
    public class SendingTweet
    {
		public TwitterStatus sourceStatus;
        public bool isDirectMessage = false;
        public string status = "";
        public decimal in_reply_to_status_id;
    }
}
