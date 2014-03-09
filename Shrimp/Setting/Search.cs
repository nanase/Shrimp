using System.Collections.Generic;

namespace Shrimp.Setting
{
    /// <summary>
    /// 検索の設定
    /// </summary>
    class Search
    {
        #region 定義

        #endregion

        #region コンストラクタ

        /// <summary>
        /// 静的コンストラクタ
        /// </summary>
        public static void initialize()
        {
            isIgnoreRT = false;
            isOnlyJapanese = false;
        }

        public static void load(Dictionary<string, bool> obj)
        {
            if (obj == null)
                return;
            if (obj.ContainsKey("isIgnoreRT"))
                isIgnoreRT = (bool)obj["isIgnoreRT"];
            if (obj.ContainsKey("isOnlyJapanese"))
                isOnlyJapanese = (bool)obj["isOnlyJapanese"];
        }

        public static Dictionary<string, bool> save()
        {
            var dest = new Dictionary<string, bool>();
            dest["isIgnoreRT"] = isIgnoreRT;
            dest["isOnlyJapanese"] = isOnlyJapanese;
            return dest;
        }

        #endregion

        /// <summary>
        /// RTを除外するかどうか
        /// </summary>
        public static bool isIgnoreRT
        {
            get;
            set;
        }

        /// <summary>
        /// 日本語のみにするかどうか
        /// </summary>
        public static bool isOnlyJapanese
        {
            get;
            set;
        }
    }
}
