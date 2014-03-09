using System.Collections.Generic;

namespace Shrimp.Twitter.REST.List
{
    /// <summary>
    /// リストのコレクション
    /// </summary>
    public class listDataCollection
    {
        private List<listData> _lists;

        public listDataCollection()
        {
            this._lists = new List<listData>();
        }

        ~listDataCollection()
        {
            /*
            if ( this._lists != null )
            {
                this._lists.Clear ();
                this._lists = null;
            }
            */
        }

        /// <summary>
        /// リストデータ集
        /// </summary>
        public List<listData> lists
        {
            get { return this._lists; }
        }

        /// <summary>
        /// リスト数
        /// </summary>
        public int Count
        {
            get { return this._lists.Count; }
        }

        /// <summary>
        /// 取得する
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public listData get(int num)
        {
            return this._lists[num];
        }

        /// <summary>
        /// リストを追加する
        /// </summary>
        /// <param name="list"></param>
        public void Addlist(listData list)
        {
            this._lists.Add(list);
        }

        /// <summary>
        /// いっきに追加する
        /// </summary>
        /// <param name="lists"></param>
        public void AddlistRange(listDataCollection lists)
        {
            this._lists.AddRange(lists.lists);
        }
    }
}
