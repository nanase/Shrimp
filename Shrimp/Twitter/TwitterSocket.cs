using System;
using System.Net;

namespace Shrimp.Twitter
{
    public class TwitterSocket : ICloneable
    {
        public Uri Uri { get; private set; }

        /// <summary>
        /// 生データ
        /// </summary>
        public string RawData { get; private set; }

        /// <summary>
        /// ステータスコード
        /// </summary>
        public HttpStatusCode StatusCode { get; private set; }

        public TwitterSocket(TwitterSocket baseSocket, HttpStatusCode statusCode)
            : this(baseSocket.Uri, statusCode, baseSocket.RawData)
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="rawdata"></param>
        public TwitterSocket(Uri uri, HttpStatusCode statusCode, string rawdata)
        {
            if (uri == null)
                throw new ArgumentNullException("uri");

            this.Uri = uri;
            this.StatusCode = statusCode;
            if ( rawdata != null )
                this.RawData = (string)rawdata.Clone();
        }        

        public object Clone()
        {
            return new TwitterSocket(this.Uri, this.StatusCode, this.RawData);
        }
    }
}
