using System.Text;

namespace Shrimp.Plugin.Ref
{
    /// <summary>
    /// ツイート入力欄のテキストが格納されている
    /// </summary>
    public class TweetBoxValue
    {
        /// <summary>
        /// テキストボックスの内容
        /// </summary>
        public string text;
        public readonly byte[] textUniBytes;
        /// <summary>
        /// 変更したあと、ツイートするかどうか
        /// </summary>
        public bool sendTweet;

        public TweetBoxValue(string text)
        {
            if (text != null && text.Length == 0)
                this.text = "";
            else
                this.text = (string)text.Clone();
            this.textUniBytes = Encoding.Unicode.GetBytes(this.text);
        }

        /// <summary>
        /// Unicodeバイト列を指定されると、こちらを優先的に扱います
        /// </summary>
        public void SetUniTextBytes(byte[] value)
        {
            this.text = Encoding.Unicode.GetString(value);
        }

    }
}
