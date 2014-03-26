using System;
using System.Net;
using System.Collections.Generic;

namespace Shrimp.Twitter.REST
{
    /// <summary>
    /// TwitterAPI利用時のイベント引数です
    /// </summary>
    public class TwitterCompletedEventArgs : EventArgs, ICloneable
    {
        private readonly TwitterInfo _account_source;
        private readonly HttpStatusCode _error_code = 0;
        private readonly object ret;
        private readonly TwitterSocket _raw_data;
        private readonly List<decimal> _friends;

        public TwitterCompletedEventArgs(TwitterInfo account_source, HttpStatusCode error_code, List<decimal> friends, object ret, TwitterSocket raw_data)
        {
            this._error_code = error_code;
            this._account_source = account_source;
            this.ret = ret;
            this._raw_data = raw_data;
            this._friends = friends;
        }

        /// <summary>
        /// 取得したデータが格納されています
        /// </summary>
        public object data
        {
            get { return ret; }
        }

        /// <summary>
        /// エラーコード
        /// </summary>
        public HttpStatusCode error_code
        {
            get { return this._error_code; }
        }

        /// <summary>
        /// TwitterSocket
        /// </summary>
        public TwitterSocket raw_data
        {
            get { return this._raw_data; }
        }

        /// <summary>
        /// 使われたアカウント
        /// </summary>
        public TwitterInfo account_source
        {
            get { return this._account_source; }
        }

        /// <summary>
        /// フォローしているユーザのID
        /// </summary>
        public List<decimal> friends
        {
            get
            {
                return this._friends;
            }
        }

        public object Clone()
        {
            var dest = new TwitterCompletedEventArgs(this.account_source, this.error_code, this.friends, this.ret, (this.raw_data != null ? (TwitterSocket)this.raw_data.Clone() : null));
            return dest;
        }
    }
}
