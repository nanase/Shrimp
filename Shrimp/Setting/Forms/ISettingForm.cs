using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shrimp.Setting.Forms
{
    interface ISettingForm
    {
        /// <summary>
        /// 設定をフォームに反映する
        /// </summary>
        void SettingReflection ();
        /// <summary>
        /// 変更を変数に反映する
        /// </summary>
        void SaveReflection ();
    }
}
