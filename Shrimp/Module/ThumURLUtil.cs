
namespace Shrimp.Module
{
    class ThumURLUtil
    {
        /// <summary>
        /// サムネイルへのURLを判断して返します
        /// </summary>
        /// <param name="path">URL</param>
        /// <param name="isThumb">サムネイルかどうかをかえします</param>
        /// <returns>サムネイルへのパス</returns>
        public static string GetThumbURL ( string path, out bool isThumb )
        {
            //
            isThumb = false;
            if ( path == null )
                return null;

            string thumb_result = path;
            if ( path.IndexOf ( @"pbs.twimg.com" ) != -1 )
            {
                //  Twitterのイメージでは？
                thumb_result = path;
                isThumb = true;
            }
            else
            {
                thumb_result = RegexUtil.ReplaceThumbURL ( path, out isThumb );
            }
            return thumb_result;
        }
    }
}
