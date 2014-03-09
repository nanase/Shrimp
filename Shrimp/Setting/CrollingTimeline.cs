using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shrimp.Setting
{
    /// <summary>
    /// それぞれのタブの巡回
    /// </summary>
    class CrollingTimeline
    {
        #region コンストラクタ

        /// <summary>
        /// 静的コンストラクタ
        /// </summary>
        public static void initialize ()
        {
            HomeTimeline = ( 60 );
            MentionTimeline = ( 60 );
            DirectMessage = ( 60 * 30 );
            Search = ( 60 );
            UserTimeline = ( 60 * 2 );
        }

        #endregion

        public static int HomeTimeline
        {
            get;
            set;
        }

        public static int MentionTimeline
        {
            get;
            set;
        }

        public static int DirectMessage
        {
            get;
            set;
        }

        public static int Search
        {
            get;
            set;
        }

        public static int UserTimeline
        {
            get;
            set;
        }
    }
}
