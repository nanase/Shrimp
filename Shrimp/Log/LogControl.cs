using System;
using System.Collections.Generic;

namespace Shrimp.Log
{
    class LogControl
    {
        #region -- Private Static Fields --
        static List<string> logs = new List<string> ();
        static object addObject = new object ();
        #endregion

        #region -- Public Static Properties --
        /// <summary>
        /// ログ数
        /// </summary>
        public static int Count { get { return logs.Count; } }

        public static IEnumerable<string> LogData { get { return logs; } }

        public static string AllLogData { get { return string.Join ( "\r\n", logs ); } }
        #endregion

        #region -- Public Static Methods --
        public static void AddLogs ( string text )
        {
            lock ( addObject )
            {
                logs.Add ( text );
            }
        }
        #endregion
    }
}
