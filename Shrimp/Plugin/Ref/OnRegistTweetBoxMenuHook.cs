using System;

namespace Shrimp.Plugin.Ref
{
    /// <summary>
    /// 右クリックメニューでの動作処理
    /// </summary>
    public class OnRegistTweetBoxMenuHook
    {
        private readonly Plugin _callPlugin;

        public string callbackFunction;
        public string text;
        public string tooltipText;

        public OnRegistTweetBoxMenuHook(Plugin callPlugin)
        {
            this._callPlugin = callPlugin;
        }

        /// <summary>
        /// 呼び出しもとプラグイン
        /// </summary>
        public object[] CallBackPlugin(object[] args)
        {
            if (String.IsNullOrEmpty(this.callbackFunction))
                return null;
            return this._callPlugin.FunctionCall(this.callbackFunction, args);
        }
    }
}
