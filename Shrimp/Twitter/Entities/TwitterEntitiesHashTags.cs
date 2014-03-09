
namespace Shrimp.Twitter.Entities
{
    public class TwitterEntitiesHashTags
    {
        #region 定義
        #endregion

        #region コンストラクタ
        public TwitterEntitiesHashTags(dynamic raw_data)
        {
            this.text = raw_data.text;
            this.indices = new int[] { (int)raw_data.indices[0], (int)raw_data.indices[1] };
        }
        public TwitterEntitiesHashTags(string text, int[] indices)
        {
            this.text = text;
            this.indices = indices;
        }
        #endregion

        /// <summary>
        /// text
        /// </summary>
        public string text
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
