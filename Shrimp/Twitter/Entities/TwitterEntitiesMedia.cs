using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Shrimp.Twitter.Entities
{
    public class TwitterEntitiesMedia
    {
        #region 定義
        #endregion

        #region コンストラクタ
        public TwitterEntitiesMedia ( dynamic raw_data )
        {
            this.url = raw_data.url;
            this.expanded_url = raw_data.expanded_url;
            this.display_url = raw_data.display_url;
            this.indices = new int[] { (int)raw_data.indices[0], (int)raw_data.indices[1] };

            this.id = (decimal)raw_data.id;
            this.media_url = raw_data.media_url;
            this.type = raw_data.type;
            this.thumb_size = new Size ( (int)raw_data.sizes.thumb.w, (int)raw_data.sizes.thumb.h );
        }

        public TwitterEntitiesMedia ( string url, string mediaURL, int []indices )
        {
            this.url = url;
            this.media_url = mediaURL;
            this.indices = indices;
            this.type = "photo";
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
        /// media_url
        /// </summary>
        public string media_url
        {
            get;
            set;
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

        /// <summary>
        /// type
        /// </summary>
        public string type
        {
            get;
            set;
        }

        /// <summary>
        /// てきとう・・・
        /// </summary>
        public Size thumb_size
        {
            get;
            set;
        }
    }
}
