using System.Drawing;

namespace Shrimp.Module.ImageUtil
{
    /// <summary>
    /// イメージキャッシュに使われるクラス
    /// </summary>
    class ImageCacheData
    {

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="_isIcon"></param>
        /// <param name="bitmap"></param>
        public ImageCacheData(string url, bool _isIcon, Bitmap bitmap)
        {
            this.url = url;
            this.isIcon = _isIcon;
            this.bitmap = bitmap;
            this.UseCount = 0;
        }

        /// <summary>
        /// URL
        /// </summary>
        public string url
        {
            get;
            set;
        }

        /// <summary>
        /// アイコンで使われるかどうか
        /// </summary>
        public bool isIcon
        {
            get;
            set;
        }

        /// <summary>
        /// ビットマップデータ
        /// </summary>
        public Bitmap bitmap
        {
            get;
            set;
        }

        /// <summary>
        /// ビットマップの使用回数。オートクリーニングに利用される
        /// </summary>
        public int UseCount
        {
            get;
            set;
        }
    }
}
