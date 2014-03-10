using System;
using System.IO;
using NLua;
using Shrimp.Log;
using Shrimp.Plugin.Ref;

namespace Shrimp.Plugin
{
    /// <summary>
    /// Plugin Class
    /// プラグインを読み込むクラス
    /// </summary>
    public class Plugin : Lua, IPlugin, IDisposable
    {
        #region 定義

        private string pluginName;
        private double pluginVersion;
        private string pluginPath;
        private string pluginDeveloper;
        private Object CallLock = new object();
        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Plugin()
        {
        }

        /// <summary>
        /// デストラクタ
        /// </summary>
        ~Plugin()
        {
            this.Dispose();
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public override void Dispose()
        {
            //  Disposeの内容を記述
            base.Dispose();
        }

        #endregion

        /// <summary>
        /// プラグインを読み込む
        /// </summary>
        /// <param name="filePath">ファイルパス</param>
        public string loadPlugin(string filePath)
        {
            //  プラグイン読み込む
            string err = null;
            this.pluginPath = (string)filePath.Clone();
            this.LoadCLRPackage();
            this.RegisterFunction("print", this.GetType().GetMethod("lua_print"));
            this.RegisterFunction ( "tweet", this.GetType ().GetMethod ( "lua_tweet" ) );
            this.RegisterFunction ( "update_profile", this.GetType ().GetMethod ( "lua_update_profile" ) );

            try
            {
                this.DoFile(filePath);
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
            var func = this.GetFunction("initialize");
            if (func == null)
                return true;
            object[] ret = null;
            try
            {
                ret = func.Call(new ShrimpVersion());
            }
            catch (Exception e)
            {
                LogControl.AddLogs("プラグイン初期化中の際にエラーが発生しました。:" + e.StackTrace + "");
                return false;
            }
            //  デフォルトプラグイン名
            this.pluginName = Path.GetFileName(this.pluginPath);
            this.pluginVersion = 100;
            if (ret.Length == 0)
                return true;
            if (ret.Length == 1)
                return (bool)ret[0];
            if (ret.Length >= 2)
                this.pluginName = (string)((string)ret[1]).Clone();
            if (ret.Length >= 3)
                this.pluginVersion = (double)ret[2];
            if (ret.Length >= 4)
                this.pluginDeveloper = (string)((string)ret[3]).Clone();

            return (bool)ret[0];
        }

        /// <summary>
        /// デバッグ用。Luaでprintを呼び出すと呼ばれます。
        /// </summary>
        /// <param name="str"></param>
        public void lua_tweet(string str)
        {
            LogControl.AddLogs(str);
        }

        /// <summary>
        /// Luaでtweetを呼び出すと呼ばれます。
        /// </summary>
        /// <param name="str"></param>
        public void lua_tweet ( string str, decimal in_reply_to_status_id )
        {
            //  ツイート処理
        }


        /// <summary>
        /// Luaでupdate_profileを呼び出すと呼ばれます
        /// </summary>
        /// <param name="str"></param>
        public void lua_update_profile ( string name, string url, string location, string description )
        {
            //  プロフィール変更
        }

        /// <summary>
        /// 関数を呼び出す
        /// </summary>
        /// <param name="func"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public object[] FunctionCall(string func, object[] arg = null)
        {
            lock (this.CallLock)
            {
                var f = this.GetFunction(func);
                if (f != null)
                {
                    if (arg == null)
                        return f.Call();
                    else
                        return f.Call(arg);
                }
            }
            return null;
        }

        /// <summary>
        /// それぞれのメソッドがあるか調べる
        /// </summary>
        /// <returns></returns>
        public LuaFunction OnTweetSendingReady()
        {
            return this.GetFunction("OnTweetSending");
        }

        /// <summary>
        /// ツイートが送信される直前に呼び出されます
        /// </summary>
        public void OnTweetSending(LuaFunction func, OnTweetSendingHook hook)
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
        public LuaFunction OnRegistTweetBoxMenuReady()
        {
            return this.GetFunction("OnRegistTweetBoxMenu");
        }

        /// <summary>
        /// テキストボックスの、右クリックメニューの追加をするタイミングになったら、とんできます
        /// </summary>
        /// <param name="hook"></param>
        public void OnRegistTweetBoxMenu(LuaFunction func, OnRegistTweetBoxMenuHook hook)
        {
            lock (this.CallLock)
            {
                var ret = func.Call(new object[] { hook });
            }
        }

        /// <summary>
        /// ツイートがShrimp内部で処理されたときに呼び出される
        /// </summary>
        /// <returns></returns>
        public LuaFunction OnCreatedTweetReady()
        {
            return this.GetFunction("OnCreatedTweet");
        }

        /// <summary>
        /// ツイートがShrimp内部で処理されたときに呼び出される
        /// </summary>
        /// <returns></returns>
        public void OnCreatedTweet(LuaFunction func, OnCreatedTweetHook hook)
        {
            lock (this.CallLock)
            {
                var ret = func.Call(new object[] { hook });
            }
        }

        /// <summary>
        /// プラグインが有効になったら実行される
        /// </summary>
        /// <returns></returns>
        public LuaFunction OnEnabledPlugin ()
        {
            return this.GetFunction ( "OnEnabledPlugin" );
        }

        /// <summary>
        /// プラグインが無効になったら実行される
        /// </summary>
        /// <returns></returns>
        public LuaFunction OnDisabledPlugin ()
        {
            return this.GetFunction ( "OnDisabledPlugin" );
        }


        /// <summary>
        /// プラグインをアンロードする
        /// </summary>
        public void unloadPlugin()
        {
            //  プラグインアンロード
            this.Close();
        }
    }
}
