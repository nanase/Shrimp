using System;
using System.Collections.Generic;
using System.IO;
using Shrimp.Log;
using Shrimp.Plugin.Ref;
using Shrimp.Setting;
using System.Windows.Forms;
using Shrimp.ControlParts.Timeline;

namespace Shrimp.Plugin
{
    /// <summary>
    /// Plugins Class
    /// プラグインを実行するクラス
    /// </summary>
    public class Plugins : IDisposable
    {
        #region 定義

        private List<Plugin> plugins;

        #endregion


        #region 静的メソッドの最適化リスト
        private Dictionary<Plugin, dynamic> OnTweetSendingList,
            OnRegistTweetboxMenuList, OnCreatedTweetList, OnStreamTweetList;
        #endregion

        #region コンストラクタ

        public Plugins()
        {
            this.OnTweetSendingList = new Dictionary<Plugin, dynamic> ();
            this.OnRegistTweetboxMenuList = new Dictionary<Plugin, dynamic> ();
            this.OnCreatedTweetList = new Dictionary<Plugin, dynamic> ();
            this.OnStreamTweetList = new Dictionary<Plugin, dynamic> ();
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            foreach (Plugin p in plugins)
            {
                p.unloadPlugin();
                p.Dispose();
            }
        }

        #endregion

		public void LoadPlugins(TimelineControl.OnUseTwitterAPIDelegate onuseTwitterAPI, RegistFunc.ShrimpHandler shrimpHandler)
        {
            plugins = new List<Plugin>();
            LogControl.AddLogs("プラグインの読み込みが開始されました\nDir: " + ShrimpSettings.PluginDirectory + "");

            if (!Directory.Exists(ShrimpSettings.PluginDirectory))
            {
                //  ディレクトリないんですが・・・
                Directory.CreateDirectory(ShrimpSettings.PluginDirectory);
            }
            //  読み込み
            string[] pluginPath = Directory.GetFiles(ShrimpSettings.PluginDirectory, "*.py");
            foreach (string plugin in pluginPath)
            {
                string fileName = Path.GetFileName(plugin);
                LogControl.AddLogs("" + plugin + "を読み込みます。");
                var newPlugin = new Plugin();
                string err = null;
                if ((err = newPlugin.loadPlugin(plugin, onuseTwitterAPI, shrimpHandler)) != null)
                {
                    LogControl.AddLogs("プラグインを読み込み中に、エラーが発生しました\n" + err + "");
                    continue;
                }
                if (!newPlugin.initializePlugin())
                {
                    LogControl.AddLogs("プラグインを初期化中に、エラーが発生しました\n" + fileName + "");
                    continue;
                }
                plugins.Add(newPlugin);
            }
            LogControl.AddLogs("プラグインの関数をチェック中です・・・");
            CheckPlugin();
            LogControl.AddLogs("プラグインの関数をチェックしました。");
            LogControl.AddLogs("プラグインの読み込み完了");
        }

        /// <summary>
        /// プラグインの登録されている関数をチェックする
        /// </summary>
        private void CheckPlugin()
        {
            try
            {
                foreach (Plugin p in plugins)
                {
                    dynamic l = p.OnTweetSendingReady ();
                    if (l != null)
                        this.OnTweetSendingList.Add(p, l);
                    l = p.OnRegistTweetBoxMenuReady();
                    if (l != null)
                        this.OnRegistTweetboxMenuList.Add(p, l);
                    l = p.OnCreatedTweetReady();
                    if (l != null)
                        this.OnCreatedTweetList.Add(p, l);
                    l = p.OnStreamTweetReady ();
                    if ( l != null )
                        this.OnStreamTweetList.Add ( p, l );
                }
            }
            catch (Exception e)
            {
                LogControl.AddLogs("プラグインの関数をチェック中に、エラーが発生しました\n" + e.Message + "");
                return;
            }
        }

        public List<ListViewItem> destPluginList ()
        {
            List<ListViewItem> itemCollect = new List<ListViewItem> ();
            foreach ( Plugin p in plugins )
            {
                string[] item = { p.PluginName, p.PluginDeveloper, p.PluginVersion.ToString ()};
                itemCollect.Add ( new ListViewItem ( item ) );
            }
            return itemCollect;
        }

        /// <summary>
        /// ツイートが送信される直前に呼び出して
        /// </summary>
        public void OnTweetSendingHook(OnTweetSendingHook hook)
        {
            if (this.OnTweetSendingList.Count == 0)
                return;
            try
            {
                foreach ( KeyValuePair<Plugin, dynamic> p in this.OnTweetSendingList )
                {
                    p.Key.OnTweetSending(p.Value, hook);
                }
            }
            catch (Exception e)
            {
                LogControl.AddLogs("プラグインを実行中に、エラーが発生しました\n" + e.Message + "");
                return;
            }
        }

        /// <summary>
        /// ツイートが送信される直前に呼び出して
        /// </summary>
        public List<OnRegistTweetBoxMenuHook> OnRegistTweetBoxMenuHook()
        {
            List<OnRegistTweetBoxMenuHook> result = new List<OnRegistTweetBoxMenuHook>();
            if (this.OnRegistTweetboxMenuList.Count == 0)
                return result;
            try
            {
                foreach ( KeyValuePair<Plugin, dynamic> p in this.OnRegistTweetboxMenuList )
                {
                    OnRegistTweetBoxMenuHook hook = new OnRegistTweetBoxMenuHook(p.Key);
                    p.Key.OnRegistTweetBoxMenu(p.Value, hook);
                    result.Add(hook);
                }
            }
            catch (Exception e)
            {
                LogControl.AddLogs("プラグインを実行中に、エラーが発生しました\n" + e.Message + "");
                return null;
            }
            return result;
        }

        /// <summary>
        /// ツイートが生成された直前に呼び出します
        /// </summary>
        public void OnCreatedTweet(OnCreatedTweetHook hook)
        {
            if ( this.OnCreatedTweetList.Count == 0 )
                return;
            try
            {
                foreach ( KeyValuePair<Plugin, dynamic> p in this.OnCreatedTweetList )
                {
                    p.Key.OnCreatedTweet(p.Value, hook);
                }
            }
            catch (Exception e)
            {
                LogControl.AddLogs("プラグインを実行中に、エラーが発生しました\n" + e.Message + "");
            }
        }

        /// <summary>
        /// ストリームでツイートが流れてきたときに呼び出します
        /// </summary>
        public void OnStreamTweet ( OnCreatedTweetHook hook )
        {
            if ( this.OnStreamTweetList.Count == 0 )
                return;
            try
            {
                foreach ( KeyValuePair<Plugin, dynamic> p in this.OnStreamTweetList )
                {
                    p.Key.OnStreamTweet ( p.Value, hook );
                }
            }
            catch ( Exception e )
            {
                LogControl.AddLogs ( "プラグインを実行中に、エラーが発生しました\n" + e.Message + "" );
            }
        }

        /// <summary>
        /// プラグインが初期化されるときに呼び出して
        /// </summary>
        public void InitializePlugin()
        {
            foreach (Plugin p in plugins)
            {
                p.initializePlugin();
            }
        }
    }
}
