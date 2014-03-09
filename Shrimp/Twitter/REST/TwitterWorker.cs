using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shrimp.ControlParts.Tabs;
using OAuth;
using Codeplex.Data;
using Shrimp.Twitter.Status;
using System.Net;
using System.Threading;
using Shrimp.Log;

namespace Shrimp.Twitter.REST
{
    public class TwitterWorker
    {
        #region 定義
        // 　ロード処理の跡の個別処理をこのデリゲートで管理します
        public delegate void TwitterCompletedProcessDelegate ( object data );
        /// <summary>
        /// エラーのとき、渡されるデリゲートです
        /// </summary>
        /// <param name="e"></param>
        public delegate void TwitterErrorProcessDelegate ( TwitterCompletedEventArgs e );
        //  ロード完了時のイベントハンドラ
        public delegate void loadCompletedEventHandler ( object sender, TwitterCompletedEventArgs e );
        public event loadCompletedEventHandler loadCompletedEvent;

        private delegate void loadWorkerDelegate ( TwitterInfo srv, string Method, workerResultDelegate workerResult,
            TwitterCompletedProcessDelegate completedProcess, string url, List<OAuthBase.QueryParameter> q );
        public delegate object workerResultDelegate ( dynamic obj );
        //private Thread thread;
        private List<object[]> objs = new List<object[]> ();
        private object lockObjects = new object ();
        #endregion
        /*
        public TwitterWorker ()
        {
            this.thread = new Thread ( new ParameterizedThreadStart ( pollingAsync ) );
            this.thread.Start ( new object[] { this.objs, lockObjects } );
        }

        private void pollingAsync ( object objects )
        {
            object[] obj = objects as object[];
            List<object[]> objs = obj[0] as List<object[]>;
            object lockObjects = obj[1] as object;

            for ( ; ; )
            {
                lock ( lockObjects )
                {
                    foreach ( object[] o in objs )
                    {
                        loadWorkerDelegate l = new loadWorkerDelegate ( loadWorker );
                        IAsyncResult res = l.BeginInvoke ( (TwitterInfo)o[0], (string)o[1], (workerResultDelegate)o[2],
                            (TwitterCompletedProcessDelegate)o[3], (string)o[4], (List<OAuthBase.QueryParameter>)o[5], null, null );
                    }
                    objs.Clear ();
                }
                Thread.Sleep ( 1 );
            }
        }
        */
        /// <summary>
        /// ロードシンク
        /// </summary>
        protected Thread loadAsync ( TwitterInfo srv, string Method, workerResultDelegate workerResult,
            TwitterCompletedProcessDelegate completedProcess, TwitterErrorProcessDelegate errorProcess, 
            string url, List<OAuthBase.QueryParameter> q, TwitterUpdateImage image = null )
        {
            if ( srv == null || Method == null || workerResult == null )
                return null;
            var thread = new Thread ( new ParameterizedThreadStart ( loadWorker ) );
            //loadWorkerDelegate l = new loadWorkerDelegate (loadWorker);
            //IAsyncResult res = l.BeginInvoke ( srv, Method, workerResult, completedProcess, url, q, null, null );
            /*
            lock ( lockObjects )
            {
                this.objs.Add  ( new object[] { srv, Method, workerResult, completedProcess, url, q } );
            }
             */
            //return res;
            thread.Start ( new object[] { srv, Method, workerResult, completedProcess, errorProcess, url, q, image } );
            return thread;
        }

        /// <summary>
        /// 非同期で行われる処理の内容
        /// </summary>
        /// <param name="srv"></param>
        private void loadWorker ( object obj )
        {
            object[] obs = obj as object[];
            TwitterInfo srv = obs[0] as TwitterInfo;
            string Method = obs[1] as string;
            workerResultDelegate workerResult = obs[2] as workerResultDelegate;
            TwitterCompletedProcessDelegate completedProcess = obs[3] as TwitterCompletedProcessDelegate;
            TwitterErrorProcessDelegate errorProcess = obs[4] as TwitterErrorProcessDelegate;
            string url = obs[5] as string;
            List<OAuthBase.QueryParameter> q = obs[6] as List<OAuthBase.QueryParameter>;
            TwitterUpdateImage objects = obs[7] as TwitterUpdateImage;
        /*
        private void loadWorker ( TwitterInfo srv, string Method, workerResultDelegate workerResult,
            TwitterCompletedProcessDelegate completedProcess, string url, List<OAuthBase.QueryParameter> q )
        {
        */
            TwitterSocket res;
            if ( Method == "GET" )
                res = srv.get ( url, q );
            else
                res = srv.post ( url, q, objects );

            dynamic data = null;
            if ( res.rawdata != null && url != "oauth/request_token" && url != "oauth/access_token" )
            {
                try
                {
                    data = DynamicJson.Parse ( res.rawdata );
                }
                catch ( Exception e )
                {
                    LogControl.AddLogs ( "DynamicJSONをパース中にエラーが発生しました\n" + e.StackTrace );
                    res.status_code = HttpStatusCode.BadGateway;
                }
            }
            else if ( url == "oauth/request_token" || url == "oauth/access_token" )
            {
                //  ああ・・・
                data = res;
            }
            OnLoadCompleted ( completedProcess, errorProcess, new TwitterCompletedEventArgs ( srv, res.status_code, workerResult.Invoke ( data ), res ) );
        }


        protected virtual void OnLoadCompleted ( TwitterCompletedProcessDelegate completedProcess,
               TwitterErrorProcessDelegate errorProcess, TwitterCompletedEventArgs e )
        {
            if ( loadCompletedEvent != null )
                loadCompletedEvent.BeginInvoke ( completedProcess, e, null, null );
            if ( e.error_code == HttpStatusCode.OK && e.data != null && completedProcess != null )
                completedProcess.BeginInvoke ( e.data, null, null );
            if ( e.error_code != HttpStatusCode.OK && errorProcess != null )
                errorProcess.BeginInvoke ( e, null, null );
        }

        /// <summary>
        /// ハンドルを待機する
        /// </summary>
        /// <param name="res"></param>
        protected void WaitResult ( Thread res )
        {
            if ( res != null && res.IsAlive )
            {
                res.Abort ();
            }
        }
    }
}
