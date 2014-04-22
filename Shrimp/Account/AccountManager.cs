using System.Collections.Generic;
using Shrimp.Twitter;
using System.Xml.Serialization;

namespace Shrimp.Account
{
    /// <summary>
    /// アカウントを統括的に管理するクラスです
    /// </summary>
    public class AccountManager
    {
        #region -- Public Fields --
        // TODO: accounts はプロパティにして、IE<TwitterInfo>型にしてもいいかも。

        /// <summary>
        /// アカウント集
        /// </summary>
        [XmlElement ( "accounts" )]
        public List<TwitterInfo> accounts = new List<TwitterInfo> ();
        #endregion

        #region -- Public Properties --
        /// <summary>
        /// 選択中の位置
        /// </summary>
        [XmlElement ( "accountSelectedNumber" )]
        public int selNum { get; set; }
        
        /// <summary>
        /// 選択中のアカウント
        /// </summary>
        public TwitterInfo SelectedAccount
        {
            get
            {
                if ( this.accounts.Count == 0 )
                    return null;

                if ( this.selNum < 0 )
                    this.selNum = 0;

                if ( this.selNum >= this.accounts.Count - 1 )
                    this.selNum = this.accounts.Count - 1;

                return this.accounts[selNum];
            }
        }
        #endregion

        #region -- Public Methods --
        /// <summary>
        /// アカウントの追加
        /// </summary>
        /// <param name="twitterAccount">アカウントデータ</param>
        public bool AddNewAccount ( TwitterInfo twitterAccount )
        {
            foreach ( TwitterInfo tmp in this.accounts )
            {
                if ( tmp.Equals ( twitterAccount ) )
                {
                    //  おなじやん！！
                    return false;
                }
            }

            this.accounts.Add ( twitterAccount );

            return true;
        }

        /// <summary>
        /// アカウントを削除する
        /// </summary>
        /// <param name="num"></param>
        public void RemoveAccount ( int num )
        {
            this.accounts.RemoveAt ( num );

            if ( this.selNum < 0 )
                this.selNum = 0;

            if ( this.selNum >= this.accounts.Count - 1 )
                this.selNum = this.accounts.Count - 1;
        }

        /// <summary>
        /// 次のアカウントへ進める
        /// </summary>
        public void NextAccount ()
        {
            if ( this.accounts.Count == 0 )
                return;

            this.selNum++;

            if ( this.selNum >= this.accounts.Count )
                this.selNum = 0;
        }
        #endregion
    }
}
