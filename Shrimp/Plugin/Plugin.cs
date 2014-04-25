using System;
using System.IO;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using Shrimp.ControlParts.Timeline;
using Shrimp.Log;
using Shrimp.Plugin.Ref;
using System.Collections.Generic;

namespace Shrimp.Plugin
{
    /// <summary>
    /// Plugin Class
    /// プラグインを読み込むクラス
    /// </summary>
    public class Plugin : IPlugin, IDisposable
    {
        #region 定義
        private ScriptEngine engine;
        private ScriptScope scope;
        private ScriptSource source;
        private dynamic dynExecute;
		private CompiledCode cc;

        private string pluginName;
        private double pluginVersion;
        private string pluginPath;
        private string pluginDeveloper;
        private Object CallLock = new object();
        private TimelineControl.OnUseTwitterAPIDelegate onUseTwitterAPI;
        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Plugin()
        {
            this.engine = Python.CreateEngine ();
        }

        /// <summary>
        /// デストラクタ
        /// </summary>
        ~Plugin()
        {
            this.Dispose();
        }

        public void Dispose ()
        {
        }

        #endregion

        public string PluginName
        {
            get { return this.pluginName; }
        }

        public string PluginDeveloper
        {
            get { return this.pluginDeveloper; }
        }

        public double PluginVersion
        {
            get { return this.pluginVersion; }
        }

        /// <summary>
        /// プラグインを読み込む
        /// </summary>
        /// <param name="filePath">ファイルパス</param>
        public string loadPlugin(string filePath, TimelineControl.OnUseTwitterAPIDelegate onuseTwitterAPI,
			RegistFunc.ShrimpHandler handler)
        {
            //  プラグイン読み込む
            string err = null;
            this.onUseTwitterAPI = onuseTwitterAPI;
            this.pluginPath = (string)filePath.Clone();

			var test = new RegistFunc.ShrimpHandler();
            this.scope = this.engine.CreateScope ();
            this.scope.SetVariable ( "Twitter", new RegistFunc.Twitter ( onuseTwitterAPI ) );
			this.scope.SetVariable("Shrimp", handler);
            /*
            this.RegisterFunction("print", this,  this.GetType().GetMethod("lua_print"));
            this.RegisterFunction ( "tweet", this, this.GetType ().GetMethod ( "lua_tweet" ) );
            this.RegisterFunction ( "favorite", this, this.GetType ().GetMethod ( "lua_favorite" ) );
            this.RegisterFunction ( "unfavorite", this, this.GetType ().GetMethod ( "lua_unfavorite" ) );
            this.RegisterFunction ( "retweet", this, this.GetType ().GetMethod ( "lua_retweet" ) );
            this.RegisterFunction ( "update_profile", this, this.GetType ().GetMethod ( "lua_update_profile" ) );
            */

            try
            {
                this.source = this.engine.CreateScriptSourceFromFile (filePath);
				//this.cc = this.source.Compile();
                this.dynExecute = this.source.Execute ( this.scope );
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return err;
        }

        /// <summary>
        /// プラグインを初期化する
        /// 返値がfalseなら、そのプラグインはアンロードする
        /// </summary>
        public bool initializePlugin()
        {
            //  プラグイン初期化
            Dictionary<string, object> ret = null;
            Func<ShrimpVersion, Dictionary<string, object>> initialize = this.scope.GetVariable<Func<ShrimpVersion, Dictionary<string, object>>> ( "initialize" );
            try
            {
                ret = initialize.Invoke ( new ShrimpVersion () );
            }
            catch (Exception e)
            {
                LogControl.AddLogs("プラグイン初期化中の際にエラーが発生しました。:" + e.StackTrace + "");
                return false;
            }
            //  デフォルトプラグイン名
            this.pluginName = Path.GetFileName(this.pluginPath);
            this.pluginVersion = 100;

            if (ret == null || ret.Count == 0)
                return true;

            if ( ret.ContainsKey ( "PluginName" ) )
                this.pluginName = (string)( (string)ret["PluginName"] ).Clone ();

            //if ( ret.ContainsKey ( "PluginName" ) )
            //    return (bool)ret[0];
            if ( ret.ContainsKey ( "PluginVersion" ) )
                this.pluginVersion = (int)ret["PluginVersion"];

            if ( ret.ContainsKey ( "PluginDeveloper" ) )
                this.pluginDeveloper = (string)((string)ret["PluginDeveloper"]).Clone();

            if ( ret.ContainsKey ( "isLoadPlugin" ) )
                return (bool)ret["isLoadPlugin"];

            return true;
        }

        /// <summary>
        /// デバッグ用。Luaでprintを呼び出すと呼ばれます。
        /// </summary>
        /// <param name="str"></param>
        public void lua_print(string str)
        {
            LogControl.AddLogs(str);
        }

        /// <summary>
        /// それぞれのメソッドがあるか調べる
        /// </summary>
        /// <returns></returns>
        public dynamic OnTweetSendingReady()
        {
            return this.dynExecute.OnTweetSending;
        }

        /// <summary>
        /// ツイートが送信される直前に呼び出されます
        /// </summary>
        public void OnTweetSending(dynamic func, OnTweetSendingHook hook)
        {
            lock (this.CallLock)
            {
                var ret = func.Call(new object[] { hook });
            }
        }

        /// <summary>
        /// ツイートボックス右クリックメニューを追加する宣言をチェックするメソッド
        /// </summary>
        /// <returns></returns>
        public dynamic OnRegistTweetBoxMenuReady ()
        {
            return this.dynExecute.OnRegistTweetBoxMenu;
        }

        /// <summary>
        /// テキストボックスの、右クリックメニューの追加をするタイミングになったら、とんできます
        /// </summary>
        /// <param name="hook"></param>
        public void OnRegistTweetBoxMenu ( dynamic func, OnRegistTweetBoxMenuHook hook )
        {
            lock (this.CallLock)
            {
                var ret = func(new object[] { hook });
            }
        }

        /// <summary>
        /// ツイートがShrimp内部で処理されたときに呼び出される
        /// </summary>
        /// <returns></returns>
        public dynamic OnCreatedTweetReady ()
        {
            return this.dynExecute.OnCreatedTweet;
        }

        /// <summary>
        /// ツイートがShrimp内部で処理されたときに呼び出される
        /// </summary>
        /// <returns></returns>
        public void OnCreatedTweet(dynamic func, OnCreatedTweetHook hook)
        {
            lock (this.CallLock)
            {
                var ret = func(new object[] { hook });
            }
        }

        /// <summary>
        /// ツイートがUserStreamで流れてきたら取得する
        /// </summary>
        /// <returns></returns>
        public dynamic OnStreamTweetReady ()
        {
            return this.dynExecute.OnStreamTweet;
        }

        /// <summary>
        ///ツイートがUserStreamで流れてきたら取得する
        /// </summary>
        /// <returns></returns>
        public void OnStreamTweet ( dynamic func, OnCreatedTweetHook hook )
        {
            lock ( this.CallLock )
            {
                var ret = func( new object[] { hook } );
            }
        }

        /// <summary>
        /// プラグインが有効になったら実行される
        /// </summary>
        /// <returns></returns>
        public dynamic OnEnabledPlugin ()
        {
            return this.dynExecute.OnEnabledPlugin;
        }

        /// <summary>
        /// プラグインが無効になったら実行される
        /// </summary>
        /// <returns></returns>
        public dynamic OnDisabledPlugin ()
        {
            return this.dynExecute.OnDisabledPlugin;
        }


        /// <summary>
        /// プラグインをアンロードする
        /// </summary>
        public void unloadPlugin()
        {
            //  プラグインアンロード
            //this.Close();
        }
    }
}
