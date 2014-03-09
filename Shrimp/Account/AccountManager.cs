using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shrimp.Twitter;
using System.Xml.Serialization;
using System.IO;
using System.Collections;

namespace Shrimp.Account
{
    /// <summary>
    /// アカウントを統括的に管理するクラスです
    /// </summary>
    public class AccountManager
    {
        /// <summary>
        /// アカウント集
        /// </summary>
        public List<TwitterInfo> accounts = new List<TwitterInfo> ();

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
            if ( selNum < 0 )
                selNum = 0;
            if ( selNum >= this.accounts.Count - 1 )
                selNum = this.accounts.Count - 1;
        }

        /// <summary>
        /// 次のアカウントへ勧める
        /// </summary>
        public void NextAccount ()
        {
            if ( this.accounts.Count == 0 )
                return;
            selNum++;
            if ( selNum >= this.accounts.Count )
                selNum = 0;
        }

        /// <summary>
        /// 選択中のアカウント
        /// </summary>
        public TwitterInfo SelectedAccount
        {
            get
            {
                if ( this.accounts.Count == 0 )
                    return null;
                if ( selNum < 0 )
                    selNum = 0;
                if ( selNum >= this.accounts.Count - 1 )
                    selNum = this.accounts.Count - 1;
                return this.accounts[selNum];
            }
        }

        /// <summary>
        /// 選択中の位置
        /// </summary>
        public int selNum
        {
            get;
            set;
        }
    }
}
