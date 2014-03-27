using System;
using System.Collections.Generic;

namespace Shrimp.Log
{
    class LogControl
    {
        #region 定義
        static List<string> logs = new List<string> ();
        static object addObject = new object ();
        #endregion

        public static void AddLogs ( string text )
        {
            lock ( addObject )
            {
                logs.Add ( text );
            }
        }

        /// <summary>
        /// ログ数
        /// </summary>
        public static int Count { get { return logs.Count; } }

        public static List<string> LogData { get { return logs; } }

        public static string AllLogData { get { return string.Join ( "\r\n", logs ); } }
    }
}
