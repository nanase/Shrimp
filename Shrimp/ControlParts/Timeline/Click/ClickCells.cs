using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Shrimp.Twitter.Entities;

namespace Shrimp.ControlParts.Timeline.Click
{
    class ClickCells : IDisposable
    {
        #region 定義
        private List<ClickCellsData> clickData = new List<ClickCellsData> ();
        #endregion

        /// <summary>
        /// 初期化
        /// </summary>
        public void initialize ()
        {
            clickData.Clear ();
        }

        ~ClickCells ()
        {
        }

        public void Dispose ()
        {
            this.clickData.Clear ();
            this.clickData = null;
            GC.SuppressFinalize ( this );
        }

        /// <summary>
        /// クリックリンクを作成します
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="detail"></param>
        /// <param name="type"></param>
        public void SetClickLink ( Rectangle rect, TwitterEntitiesPosition entities_pos )
        {
            rect.Width++;
            rect.Height++;
            clickData.Add ( new ClickCellsData ( rect, entities_pos ) );
        }

        /// <summary>
        /// クリックリンクを取得します
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public ClickCellsData getClickLink ( Point location )
        {
            clickData.Reverse ();
            foreach ( ClickCellsData t in clickData )
            {
                if ( t.Rect.Contains ( location ) )
                    return t;
            }
            clickData.Reverse ();
            return null;
        }
    }
}
