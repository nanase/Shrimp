using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using OAuth;

namespace Shrimp.Twitter
{
    class TwitterConnect
    {
        /// <summary>
        /// TwitterAPIのURLの根幹を取得
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public static string twitterAPIBase
        {
            get { return "https://api.twitter.com/1.1/"; }
        }

        /// <summary>
        /// Streaming API URLを取得
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public static string twitterStreamingAPI
        {
            get { return "https://userstream.twitter.com/1.1/user.json"; }
        }


        /// <summary>
        /// WebSocket
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="method">GET or POST</param>
        /// <param name="param">パラメータ</param>
        /// <returns></returns>
        private TwitterSocket socket(TwitterInfo srv, string url, string method, List<OAuthBase.QueryParameter> param, TwitterUpdateImage image = null)
        {
            OAuthBase oAuth = new OAuthBase();
            string nonce = oAuth.GenerateNonce();
            string timestamp = oAuth.GenerateTimeStamp();

            Uri uri;
            //OAuthBace.csを用いてsignature生成
            string normalizedUrl, normalizedRequestParameters, sig = "";

            if (url == "oauth/request_token" || url == "oauth/access_token")
            {
                uri = new Uri("https://api.twitter.com/" + url + "");
                if (param != null && param[0].Name == "oauth_verifier")
                {
                    sig = oAuth.GenerateSignature(uri, null, "oob", srv.ConsumerKey, srv.ConsumerSecret, param[1].Value, param[2].Value,
                        method, timestamp, param[0].Value, nonce, out normalizedUrl, out normalizedRequestParameters);
                }
                else
                {
                    sig = oAuth.GenerateSignature(uri, param, "oob", srv.ConsumerKey, srv.ConsumerSecret, null, null,
                        method, timestamp, null, nonce, out normalizedUrl, out normalizedRequestParameters);
                }
            }
            else
            {
                uri = new Uri(twitterAPIBase + url);
                sig = oAuth.GenerateSignature(uri, param, "oob", srv.ConsumerKey, srv.ConsumerSecret, srv.AccessTokenKey, srv.AccessTokenSecret,
                                                        method, timestamp, null, nonce, out normalizedUrl, out normalizedRequestParameters);
            }
            sig = OAuthBase.UrlEncode(sig);
            string raw_data = null;
            Stream st = null;
            StreamReader sr = null;
            HttpStatusCode code = HttpStatusCode.RequestTimeout;
            HttpWebRequest webreq = null;
            try
            {
                webreq = (HttpWebRequest)WebRequest.Create(string.Format("{0}?{1}&oauth_signature={2}", normalizedUrl, normalizedRequestParameters, sig));
                webreq.Method = method;
                webreq.Timeout = 60 * 1000;
                webreq.UserAgent = "Shrimp";
                webreq.ProtocolVersion = HttpVersion.Version11;
                webreq.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                webreq.ServicePoint.ConnectionLimit = 1000;
                webreq.ContentType = "application/x-www-form-urlencoded";

                if (url.IndexOf("statuses/update_with_media") >= 0)
                {
                    //  メディアアップロードらしい。
                    //区切り文字列
                    if (image == null)
                        throw new WebException("imageがありません");
                    var media = image.Data;
                    var filename = image.FileName;
                    var status = image.Status;
                    Encoding enc = Encoding.GetEncoding("utf-8");
                    string boundary = Environment.TickCount.ToString();
                    webreq.ContentType = "multipart/form-data; boundary=" + boundary;

                    //POST送信するデータを作成
                    string postData = "";
                    postData = "--" + boundary + "\r\n" +
                        "Content-Disposition: form-data; name=\"status\"\r\n\r\n" +
                        "" + status + "\r\n" +
                        "--" + boundary + "\r\n" +
                        "Content-Disposition: form-data; name=\"media[]\"; filename=\"" +
                            filename + "\"\r\n" +
                        "Content-Type: application/octet-stream\r\n" +
                        "Content-Transfer-Encoding: binary\r\n\r\n";
                    //バイト型配列に変換
                    byte[] startData = enc.GetBytes(postData);
                    postData = "\r\n--" + boundary + "--\r\n";
                    byte[] endData = enc.GetBytes(postData);

                    //POST送信するデータの長さを指定
                    webreq.ContentLength = startData.Length + endData.Length + media.Length;

                    //データをPOST送信するためのStreamを取得
                    Stream reqStream = webreq.GetRequestStream();

                    //送信するデータを書き込む
                    reqStream.Write(startData, 0, startData.Length);
                    //ファイルの内容を送信
                    reqStream.Write(media, 0, media.Length);

                    reqStream.Write(endData, 0, endData.Length);
                    reqStream.Close();
                }
                HttpWebResponse webres = (HttpWebResponse)webreq.GetResponse();

                //  コードチェック
                if ((code = webres.StatusCode) == HttpStatusCode.OK)
                {
                    st = webres.GetResponseStream();
                    if (webres != null && webres.ContentEncoding.ToLower() == "gzip")
                    {
                        //gzip。
                        GZipStream gzip = new GZipStream(st, CompressionMode.Decompress);
                        sr = new StreamReader(gzip, Encoding.GetEncoding(932));
                        gzip.Close();
                    }
                    else
                    {
                        sr = new StreamReader(st, Encoding.GetEncoding(932));
                    }
                    raw_data = sr.ReadToEnd();
                    if (raw_data == null)
                        raw_data = "";
                }
            }
            catch (Exception e)
            {
                if (e is WebException)
                {
                    var exp = e as WebException;
                    if (exp.Status == WebExceptionStatus.ProtocolError)
                    {
                        HttpWebResponse err = (HttpWebResponse)exp.Response;
                        code = err.StatusCode;
                    }
                }
            }
            finally
            {
                if (sr != null)
                    sr.Close();
                if (st != null)
                    st.Close();
            }
            return new TwitterSocket((webreq != null ? webreq.RequestUri : null), code, raw_data);
        }

        /// <summary>
        /// 同期的に行うので、どっかでやって
        /// </summary>
        /// <param name="uri"></param>
        public TwitterSocket get(TwitterInfo srv, string url, List<OAuthBase.QueryParameter> param)
        {
            return this.socket(srv, url, "GET", param);
        }

        /// <summary>
        /// 同期的に行うので、どっかでやって
        /// </summary>
        /// <param name="uri"></param>
        public TwitterSocket post(TwitterInfo srv, string url, List<OAuthBase.QueryParameter> param, TwitterUpdateImage image)
        {
            return this.socket(srv, url, "POST", param, image);
        }

    }
}
