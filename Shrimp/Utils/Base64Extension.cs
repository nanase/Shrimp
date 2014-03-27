using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shrimp.Utils
{
    /// <summary>
    /// Base64のユーティリティーです
    /// </summary>
    static class Base64Util
    {
        /// <summary>
        /// バイナリをBase64へ変換します
        /// </summary>
        /// <param name="value">バイナリデータ</param>
        /// <returns>変換結果</returns>
        public static string ToBase64 ( this byte[] array )
        {
            if ( array == null )
                throw new ArgumentNullException ( "array" );

            return Convert.ToBase64String ( array );
        }
    }
}
