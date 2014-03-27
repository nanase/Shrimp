using System.IO.Compression;
using System.Text;

namespace System.IO
{
    /// <summary>
    /// Stream に対する拡張メソッドを提供します。
    /// </summary>
    static class StreamExtension
    {
        /// <summary>
        /// ストリームに対する OpenStreamReader を生成します。
        /// </summary>
        /// <param name="baseStream">基となるストリーム。</param>
        /// <param name="decompress">GZip による伸長の可否を表す真偽値。</param>
        /// <param name="encode">StreamReader で用いるエンコード。</param>
        /// <returns>生成された StreamReader オブジェクト。</returns>
        public static StreamReader OpenStreamReader ( this Stream baseStream, bool decompress, Encoding encode = null )
        {
            if ( baseStream == null )
                throw new ArgumentNullException ( "baseStream" );

            if ( encode == null )
                encode = Encoding.UTF8;

            if ( decompress )
            {
                GZipStream gzip = new GZipStream ( baseStream, CompressionMode.Decompress );
                return new StreamReader ( gzip, encode );
            }
            else
                return new StreamReader ( baseStream, encode );
        }

        /// <summary>
        /// 複数の byte型配列をストリームにすべて書き込みます。
        /// </summary>
        /// <param name="baseStream">書き込み先のストリーム。</param>
        /// <param name="arrays">書き込まれる配列群。</param>
        public static void WriteArrays ( this Stream baseStream, params byte[][] arrays )
        {
            if ( baseStream == null )
                throw new ArgumentNullException ( "baseStream" );

            if ( !baseStream.CanWrite )
                throw new InvalidOperationException();

            foreach ( var array in arrays )
                baseStream.Write ( array, 0, array.Length );
        }
    }
}
