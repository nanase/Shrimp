
namespace System.Text
{
    /// <summary>
    /// 追加のエンコードを提供します。
    /// </summary>
    class AdditionalEncoding
    {
        private static readonly Encoding shiftJIS = Encoding.GetEncoding ( 932 );

        /// <summary>
        /// Shift-JIS 形式のエンコーディングを取得します。
        /// </summary>
        public static Encoding ShiftJIS { get { return shiftJIS; } }
    }
}
