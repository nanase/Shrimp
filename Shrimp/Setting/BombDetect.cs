
namespace Shrimp.Setting
{
    /// <summary>
    /// BombDetect Class
    /// 爆撃検知設定
    /// </summary>
    class BombDetect
    {
        #region 定義

        private static bool _isEnableBombDetect = false;
        private static int _bombDetectSec = 5;
        private static int _bombDetectTweetNum = 1000;

        #endregion

        #region コンストラクタ

        /// <summary>
        /// 静的コンストラクタ
        /// </summary>
        public static void initialize()
        {
        }

        #endregion
        /// <summary>
        /// 爆撃検知を行うかどうか
        /// </summary>
        public static bool isEnableBombDetect
        {
            get { return _isEnableBombDetect; }
            set { _isEnableBombDetect = value; }
        }

        /// <summary>
        /// 爆撃検知に要する時間
        /// </summary>
        public static int bombDetectSec
        {
            get { return _bombDetectSec; }
            set { _bombDetectSec = value; }
        }

        /// <summary>
        /// 爆撃検知時、流れてきたツイート数を定める
        /// </summary>
        public static int bombDetectTweetNum
        {
            get { return _bombDetectTweetNum; }
            set { _bombDetectTweetNum = value; }
        }
    }
}
