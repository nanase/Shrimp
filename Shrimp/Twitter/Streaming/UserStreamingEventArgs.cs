using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shrimp.Module;
using Shrimp.Twitter.Status;

namespace Shrimp.Twitter.Streaming
{
    public class UserStreamingEventArgs
    {
        private readonly int _error_code = 0;
        private readonly TwitterStatus ret;

        public UserStreamingEventArgs ( int error_code, TwitterStatus ret )
        {
            this._error_code = error_code;
            this.ret = ret;
        }

        /// <summary>
        /// 取得したタイムラインが格納されています
        /// </summary>
        public TwitterStatus tweet
        {
            get { return this.ret; }
        }

        /// <summary>
        /// エラーコード
        /// </summary>
        public int error_code
        {
            get { return this._error_code; }
        }
    }
}
