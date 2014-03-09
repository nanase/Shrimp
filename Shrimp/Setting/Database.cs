using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shrimp.Setting
{
    class Database
    {
        #region コンストラクタ

        internal static void initialize ()
        {
            //  初期設定
            SaveDatabaseNum = 200;
        }

        public static void load ( Dictionary<string, int> obj )
        {
            if ( obj == null )
                return;
            if ( obj.ContainsKey ( "SaveDatabaseNum" ) )
                SaveDatabaseNum = obj["SaveDatabaseNum"];
        }

        public static Dictionary<string, int> save ()
        {
            var dest = new Dictionary<string, int> ();
            dest["SaveDatabaseNum"] = 1000;
            return dest;
        }

        #endregion

        public static int SaveDatabaseNum
        {
            get;
            set;
        }
    }
}
