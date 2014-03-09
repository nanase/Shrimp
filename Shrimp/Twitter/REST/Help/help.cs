using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OAuth;

namespace Shrimp.Twitter.REST.Help
{
    /// <summary>
    /// helpクラス
    /// </summary>
    class help : TwitterWorker, IDisposable
    {
        #region 定義
        private Thread configurationResult;
        private bool isDisposed;
        #endregion

        #region コンストラクタ
        ~help ()
        {
            this.Dispose ();
        }

        /// <summary>
        /// オブジェクト解放
        /// </summary>
        public void Dispose()
        {
            if (!isDisposed)
            {
                WaitResult ( configurationResult );
                isDisposed = true;
            }
        }
        #endregion


        /// <summary>
        ///  Twitterの設定を取得
        /// </summary>
        public void configuration ( TwitterInfo srv, TwitterCompletedProcessDelegate completedDelegate, TwitterErrorProcessDelegate errorProcess )
        {
            List<OAuthBase.QueryParameter> q = new List<OAuthBase.QueryParameter> ();
            this.configurationResult = base.loadAsync ( srv, "GET", workerResult, completedDelegate, errorProcess, "help/configuration.json", q );
        }


        /// <summary>
        /// 受信したデータを処理する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private object workerResult ( dynamic data )
        {
            if ( data == null )
                return null;
            return new ConfigStatus (data);
        }

    }
}
