using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shrimp.Setting
{
    /// <summary>
    /// ISetting
    /// 設定のインターフェース
    /// </summary>
    interface ISetting
    {
        /// <summary>
        /// ロードを行う
        /// </summary>
        void loadQuery();
        /// <summary>
        /// セーブを行う
        /// </summary>
        void saveQuery();
    }
}
