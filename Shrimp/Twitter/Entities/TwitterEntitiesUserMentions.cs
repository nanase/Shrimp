
namespace Shrimp.Twitter.Entities
{
    public class TwitterEntitiesUserMentions
    {
        #region 定義
        #endregion

        #region コンストラクタ
        public TwitterEntitiesUserMentions(dynamic raw_data)
        {
            this.id = (decimal)raw_data.id;
            this.screen_name = raw_data.screen_name;
            this.name = raw_data.name;
            this.indices = new int[] { (int)raw_data.indices[0], (int)raw_data.indices[1] - 1 };
        }
        public TwitterEntitiesUserMentions(string screen_name, int[] indices)
        {
            this.screen_name = screen_name;
            this.indices = indices;
        }

        #endregion

        /// <summary>
        /// ID
        /// </summary>
        public decimal id
        {
            get;
            set;
        }

        /// <summary>
        /// screen_name
        /// </summary>
        public string screen_name
        {
            get;
            set;
        }

        /// <summary>
        /// name
        /// </summary>
        public string name
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
