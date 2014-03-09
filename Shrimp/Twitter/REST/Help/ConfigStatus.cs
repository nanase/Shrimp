using System;

namespace Shrimp.Twitter.REST.Help
{
    /// <summary>
    /// Twitterの設定を管理するクラス
    /// </summary>
    public class ConfigStatus : ICloneable
    {
        private readonly int _characters_reserved_per_media;
        private readonly int _photo_size_limit;

        public ConfigStatus(int _characters_reserved_per_media, int _photo_size_limit)
        {
            this._characters_reserved_per_media = _characters_reserved_per_media;
            this._photo_size_limit = _photo_size_limit;
        }

        public ConfigStatus(dynamic data)
        {
            this._characters_reserved_per_media = (int)data.characters_reserved_per_media;
            this._photo_size_limit = (int)data.photo_size_limit;
        }

        /// <summary>
        /// メディアをあげたとき、減る最大ツイート文字数
        /// </summary>
        public int characters_reserved_per_media
        {
            get { return this._characters_reserved_per_media; }
        }

        /// <summary>
        /// 画像の容量サイズ制限
        /// </summary>
        public int photo_size_limit
        {
            get { return this._photo_size_limit; }
        }

        public object Clone()
        {
            return new ConfigStatus(characters_reserved_per_media, photo_size_limit);
        }
    }
}
