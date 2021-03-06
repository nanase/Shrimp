﻿
namespace Shrimp.Plugin
{
    /// <summary>
    /// IPlugin
    /// プラグインインターフェース
    /// </summary>
    interface IPlugin
    {
        //  プラグインが読み込まれる
        string loadPlugin(string pluginPath);
        //  プラグインが初期化されたとき
        bool initializePlugin();
        //  プラグインがアンロードされたとき
        void unloadPlugin();
    }
}
