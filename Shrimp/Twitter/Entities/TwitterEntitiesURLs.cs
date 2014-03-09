
namespace Shrimp.Twitter.Entities
{
    /// <summary>
    /// URL エンティティ
    /// </summary>
    public class TwitterEntitiesURLs
    {
        #region 定義
        #endregion

        #region コンストラクタ
        public TwitterEntitiesURLs(dynamic raw_data)
        {
            this.url = raw_data.url;
            this.expanded_url = raw_data.expanded_url;
            this.display_url = raw_data.display_url;
            this.indices = new int[] { (int)raw_data.indices[0], (int)raw_data.indices[1] };
        }
        public TwitterEntitiesURLs(string url, int[] indices)
        {
            this.url = url;
            this.indices = indices;
        }

        #endregion

        /// <summary>
        /// URL
        /// </summary>
        public string url
        {
            get;
            set;
        }

        /// <summary>
        /// 拡張したURL
        /// </summary>
        public string expanded_url
        {
            get;
            set;
        }

        /// <summary>
        /// 表示されるURL
        /// </summary>
        public string display_url
        {
            get;
            set;
        }

        /// <summary>
        /// ツイートのなかで表示されている位置
        /// </summary>
        public int[] indices
        {
            get;
            set;
        }
    }
}
