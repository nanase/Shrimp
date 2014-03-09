using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
using System.Timers;
using Shrimp.Log;

namespace Shrimp.Module.ImageUtil
{
    /// <summary>
    /// ImageCache Class
    /// 画像などのキャッシュを管理します
    /// </summary>
    class ImageCache : IDisposable
    {
        #region 定義

        private delegate void loadWorkerDelegate(string url, bool isIcon);

        private static volatile Dictionary<string, ImageCacheData> cacheData;
        private static volatile Queue<ImageCacheData> loadingCacheData;
        private static System.Timers.Timer checkTimer;
        private static Thread thread;
        private static volatile object lockWorker = new object();
        private static volatile bool isStopCrawling = false;
        private static volatile List<ImageCacheData> tmpData = new List<ImageCacheData>();
        private static volatile bool isCrawling = false;
        private static decimal syncNum = 0;
        #endregion

        #region コンストラクタ

        static ImageCache()
        {
            cacheData = new Dictionary<string, ImageCacheData>();
            loadingCacheData = new Queue<ImageCacheData>();
            thread = new Thread(new ThreadStart(loadAsync));
            thread.Start();
            checkTimer = new System.Timers.Timer();
            checkTimer.Interval = 500;
            checkTimer.Elapsed += new ElapsedEventHandler(checkTimer_Elapsed);
            checkTimer.Start();
        }

        ~ImageCache()
        {
            StopCrawling();
            this.Dispose();
        }

        public void Dispose()
        {
            foreach (ImageCacheData data in cacheData.Values)
            {
                data.bitmap.Dispose();
            }

            cacheData = null;

            checkTimer.Stop();
            checkTimer.Elapsed -= new ElapsedEventHandler(checkTimer_Elapsed);
            checkTimer = null;
            loadingCacheData = null;
        }

        #endregion


        /// <summary>
        /// タイマーを回して、一定時間ごとにキャッシュを拾う
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void checkTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (loadingCacheData.Count != 0)
            {
                System.Timers.Timer t = sender as System.Timers.Timer;
                t.Stop();
                loadAsync();
                t.Start();
            }
        }

        public static void StopCrawling()
        {
            if (thread != null)
            {
                isStopCrawling = true;
                thread.Abort();
                thread = null;
            }
        }

        /// <summary>
        /// ロードシンク
        /// </summary>
        private static void loadAsync()
        {
            for (; ; )
            {
                if (isCrawling)
                    return;

                isCrawling = true;
                lock (lockWorker)
                {
                    tmpData.Clear();

                    if (loadingCacheData.Count != 0)
                    {
                        for (int i = 0; i < loadingCacheData.Count && i < 5; i++)
                        {
                            ImageCacheData tmp = loadingCacheData.Dequeue();
                            if (tmp == null)
                                break;
                            tmpData.Add(tmp);
                        }
                    }
                }

                foreach (ImageCacheData t in tmpData)
                {
                    if (isStopCrawling)
                        break;
                    loadWorker(t.url, t.isIcon);
                }

                syncNum++;

                if (syncNum % 60000 == 0)
                {
                    List<string> keys = new List<string>();
                    lock (lockWorker)
                    {
                        foreach (KeyValuePair<string, ImageCacheData> value in cacheData)
                        {
                            //  
                            if (value.Value.UseCount < -5000 && value.Value.bitmap != null)
                            {
                                value.Value.bitmap.Dispose();
                                value.Value.bitmap = null;
                                keys.Add(value.Key);
                            }
                        }
                        foreach (string key in keys)
                        {
                            cacheData.Remove(key);
                        }
                    }
                }
                Thread.Sleep(1);
                if (isStopCrawling)
                    break;
                isCrawling = false;
            }
            //loadWorkerDelegate l = new loadWorkerDelegate ( loadWorker );
            //l.BeginInvoke ( url, isIcon, null, null );
        }

        /// <summary>
        /// 非同期で行われる処理の内容
        /// </summary>
        /// <param name="srv"></param>
        private static void loadWorker(string url, bool isIcon)
        {
            Bitmap res = loadImageFromURL(url);
            if (res != null)
            {
                Point p = new Point();
                if (isIcon)
                {
                    Bitmap new_res = ImageGenerateUtil.AppendDropShadow(res, 1, new Point(2, 2), Color.Gray, out p);
                    res.Dispose(); res = new_res;
                }
                if (isIcon)
                {
                    if (res.Width != Setting.Timeline.IconSize || res.Height != Setting.Timeline.IconSize)
                        res = ImageGenerateUtil.ResizeIcon(res, true);
                }
                else
                {
                    res = ImageGenerateUtil.ResizeImage(res, Setting.Timeline.ImageWidth, Setting.Timeline.ImageHeight);
                }
                setCache(url, isIcon, res);
            }
            res = null;
        }

        /// <summary>
        /// 画像を読み込む
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns>イメージデータ</returns>
        private static Bitmap loadImageFromURL(string url)
        {
            int buffSize = 65536; // 一度に読み込むサイズ
            MemoryStream imgStream = new MemoryStream();
            Bitmap bmp = null;
            if (url == null || url.Trim().Length <= 0)
            {
                return null;
            }

            try
            {
                //  あんまつかいたくないけど、例外処理
                WebRequest req = WebRequest.Create(url);
                BinaryReader reader = new BinaryReader(req.GetResponse().GetResponseStream());

                while (true)
                {
                    byte[] buff = new byte[buffSize];

                    // 応答データの取得
                    int readBytes = reader.Read(buff, 0, buffSize);
                    if (readBytes <= 0)
                    {
                        // 最後まで取得した->ループを抜ける
                        break;
                    }

                    // バッファに追加
                    imgStream.Write(buff, 0, readBytes);
                }

                bmp = new Bitmap(imgStream);
            }
            catch (Exception e)
            {
                LogControl.AddLogs("画像読み込みの際にエラーが発生しました。:" + e.StackTrace + "");
                return null;
            }

            return bmp;
        }

        /// <summary>
        /// キャッシュを挿入する
        /// </summary>
        /// <param name="key">URL</param>
        /// <param name="value">ビットマップデータ</param>
        private static void setCache(string key, bool isIcon, Bitmap value)
        {
            lock (lockWorker)
            {
                Bitmap b = (value != null ? (Bitmap)value.Clone() : null);
                cacheData[key] = new ImageCacheData(key, isIcon, b);
            }
        }

        /// <summary>
        /// キャッシュがあるかどうか
        /// キャッシュがあるかどうかに限らず、URLがセットされていればtrueが変えるので要注意
        /// </summary>
        /// <param name="key">URL</param>
        /// <returns>キャッシュがあればtrue、なければfalse</returns>
        public static bool isCached(string key)
        {
            return cacheData.ContainsKey(key);
        }

        /// <summary>
        /// キャッシュを取得する
        /// キャッシュがnullの場合は取得中を差しますので、またのちほどアクセスしてください
        /// </summary>
        /// <param name="key">URL</param>
        /// <returns>キャッシュのBitmapを返します。cloneをさしあげるので、各自廃棄してください</returns>
        public static Bitmap getCache(string key)
        {
            if (!cacheData.ContainsKey(key))
                return null;
            lock (lockWorker)
            {
                Bitmap b = cacheData[key].bitmap;
                if (b == null)
                    return null;
                foreach (ImageCacheData value in cacheData.Values)
                {
                    //  
                    value.UseCount--;
                }
                cacheData[key].UseCount = 100;
                return (Bitmap)b.Clone();
            }
        }

        /// <summary>
        /// 画像をキューにいれる
        /// </summary>
        /// <param name="url">URL</param>
        public static void SetQueueImage(string url, bool isIcon)
        {
            setCache(url, isIcon, null);
            loadingCacheData.Enqueue(new ImageCacheData(url, isIcon, null));
        }

        /// <summary>
        /// キャッシュがあればそれを取り出し、なければ勝手にキューにいれます
        /// </summary>
        /// <param name="url"></param>
        /// <param name="isIcon"></param>
        /// <returns></returns>
        public static Bitmap AutoCache(string url, bool isIcon)
        {
            Bitmap tmp = null;
            if (url == null)
                return null;
            if (!isCached(url))
            {
                ImageCache.SetQueueImage(url, isIcon);
            }
            else
            {
                tmp = ImageCache.getCache(url);
            }
            return tmp;
        }
    }
}
