using System;

namespace Shrimp.Module
{
    /// <summary>
    /// Base64のユーティリティーです
    /// </summary>
    class Base64Util
    {
        /// <summary>
        /// バイナリをBase64へ変換します
        /// </summary>
        /// <param name="value">バイナリデータ</param>
        /// <returns>変換結果</returns>
        public static string ToBase64(byte[] value)
        {
            if (value == null)
                return null;
            return Convert.ToBase64String(value);
        }
    }
}
