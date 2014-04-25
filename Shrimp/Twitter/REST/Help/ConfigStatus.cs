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
        private readonly int _short_url_length_https;
        private readonly int _short_url_length;

        public ConfigStatus(int _characters_reserved_per_media, int _photo_size_limit,
            int _short_url_length_https, int _short_url_length)
        {
            this._characters_reserved_per_media = _characters_reserved_per_media;
            this._photo_size_limit = _photo_size_limit;
            this._short_url_length_https = _short_url_length_https;
            this._short_url_length = _short_url_length;
        }

        public ConfigStatus(dynamic data)
        {
            this._characters_reserved_per_media = (int)data.characters_reserved_per_media;
            this._photo_size_limit = (int)data.photo_size_limit;
            this._short_url_length_https = (int)data.short_url_length_https;
            this._short_url_length = (int)data.short_url_length;
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

        /// <summary>
        /// HTTPSの短縮URL
        /// </summary>
        public int short_url_length_https
        {
            get { return this._short_url_length_https; }
        }

        /// <summary>
        /// 短縮URLの文字数
        /// </summary>
        public int short_url_length
        {
            get { return this._short_url_length; }
        }

        public object Clone()
        {
            return new ConfigStatus(characters_reserved_per_media, photo_size_limit, short_url_length_https,
                short_url_length);
        }
    }
}
