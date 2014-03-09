using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shrimp.Twitter.Status;

namespace Shrimp.Plugin.Ref
{
    public class OnCreatedTweetHook
    {
        /// <summary>
        /// ツイート内容
        /// </summary>
        public TwitterStatus status;
        /// <summary>
        /// trueにすると、タイムラインに流す処理を取りやめます。
        /// </summary>
        public bool isCancel;
        /// <summary>
        /// ツイートを変更した場合、ここをtrueにしてください
        /// </summary>
        public bool isModified;

        public OnCreatedTweetHook ( TwitterStatus status )
        {
            this.status = status;
        }

        /*
        /// <summary>
        /// 呼び出しもとプラグイン
        /// </summary>
        public object[] CallBackPlugin ( object[] args )
        {
            if ( String.IsNullOrEmpty ( this.callbackFunction ) )
                return null;
            return this._callPlugin.FunctionCall ( this.callbackFunction, args );
        }
        */
    }
}
