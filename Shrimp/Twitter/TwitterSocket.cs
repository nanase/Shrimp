using System;
using System.Net;

namespace Shrimp.Twitter
{
    public class TwitterSocket : ICloneable
    {
        public Uri uri;
        /// <summary>
        /// 生データ
        /// </summary>
        public string rawdata;
        /// <summary>
        /// ステータスコード
        /// </summary>
        public HttpStatusCode status_code;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="status_code"></param>
        /// <param name="rawdata"></param>
        public TwitterSocket(Uri uri, HttpStatusCode status_code, string rawdata)
        {
            this.uri = uri;
            this.status_code = status_code;
            this.rawdata = (string)rawdata.Clone();
        }

        public object Clone()
        {
            var dest = new TwitterSocket(this.uri, this.status_code, this.rawdata);
            return dest;
        }
    }
}
