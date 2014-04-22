﻿using System;

namespace Shrimp.Module.TimeUtil
{
    class TimeSpanUtil
    {
        /// <summary>
        /// 20xx/x/x x時x分x秒の表記を表示します。
        /// </summary>
        /// <param name="tweetSource">時間計る元のツイート時間</param>
        /// <returns>表示する時刻</returns>
        public static string AbsoluteTimeToString ( DateTime tweetSource )
        {
            //isEnableAbsoluteTime
            return tweetSource.ToString ( (DateTime.Now.Year == tweetSource.Year) ?
                "yyyy年MM月dd日(ddd) HH時mm分ss秒" :
                "MM月dd日(ddd) HH時mm分ss秒" );
        }

        /// <summary>
        /// ～分前の表記を表示します。
        /// </summary>
        /// <param name="tweetSource">時間計る元のツイート時間</param>
        /// <returns>表示</returns>
        public static string agoToString(DateTime tweetSource)
        {
            string result = "";
            TimeSpan t = DateTime.Now - tweetSource;
            if (t.Days != 0)
            {
                result = "" + tweetSource.ToShortDateString() + "";
            }
            else
            {
                if (t.Hours != 0)
                {
                    result = "" + t.Hours + "時間前";
                }
                else
                {
                    if (t.Minutes != 0)
                    {
                        result = "" + t.Minutes + "分前";
                    }
                    else
                    {
                        result = "" + (t.Seconds < 0 ? 0 : t.Seconds) + "秒前";
                    }
                }
            }
            return result;
        }
    }
}
