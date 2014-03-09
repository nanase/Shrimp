using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shrimp.Plugin.Ref
{
    /// <summary>
    /// ツイートが送信される直前に送られてきます
    /// </summary>
    public class OnTweetSendingHook
    {
        public string text;
        public byte[] textBytes;
        public decimal in_reply_to_status_id;
        /// <summary>
        /// ここをtrueにすると、ツイートをキャンセルします
        /// </summary>
        public bool isCancel;

        public OnTweetSendingHook ( string text, decimal in_reply_to_status_id )
        {
            this.text = (string)text.Clone ();
            this.in_reply_to_status_id = in_reply_to_status_id;
            this.textBytes = Encoding.Unicode.GetBytes ( this.text );
        }
    }
}
