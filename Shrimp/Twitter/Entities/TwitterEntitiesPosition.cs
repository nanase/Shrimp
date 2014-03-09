using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shrimp.Twitter.Entities
{
    /// <summary>
    /// タイムラインにて、Entitiesのポジションの管理をします
    /// </summary>
    public class TwitterEntitiesPosition : ICloneable
    {
        public string source;
        public string type;

        public TwitterEntitiesPosition ( string source, string type )
        {
            this.source = source;
            this.type = type;
        }

        protected TwitterEntitiesPosition ( TwitterEntitiesPosition that )
        {
            this.source = that.source;
            this.type = that.type;
        }

        public virtual object Clone ()
        {
            return new TwitterEntitiesPosition ( this ); // コピーコンストラクタを使ってコピーを作成
        }
    }
}
