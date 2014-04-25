
using Shrimp.ControlParts.Timeline;
namespace Shrimp.Plugin
{
    /// <summary>
    /// IPlugin
    /// プラグインインターフェース
    /// </summary>
    interface IPlugin
    {
        //  プラグインが読み込まれる
        string loadPlugin ( string pluginPath, TimelineControl.OnUseTwitterAPIDelegate onuseTwitterAPI,
			RegistFunc.ShrimpHandler handler );
        //  プラグインが初期化されたとき
        bool initializePlugin();
        //  プラグインがアンロードされたとき
        void unloadPlugin();
    }
}
