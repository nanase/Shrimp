
namespace Shrimp.Module.Parts.ShrimpStatusBar
{
    /// <summary>
    /// UserStreamの接続状況を管理します
    /// </summary>
    class StreamState
    {
        private static bool isUserstream = false;
        private static int delayPer = 0;

        /// <summary>
        /// UserStreamが接続されているかどうか
        /// </summary>
        public static bool setUserStream
        {
            set { isUserstream = value; }
        }

        /// <summary>
        /// ツイート取得漏れ率
        /// </summary>
        public static int setPercentage
        {
            set { delayPer = value; }
        }

        /// <summary>
        /// ステータス情報を取得します
        /// </summary>
        /// <returns></returns>
        public static string getStatusString()
        {
            string result = "";
            result += "US:" + (isUserstream ? "○" : "×");
            result += " / エビの早さ:" + (delayPer < 10 ? "おっそい" : delayPer < 20 ? "ふつう" : delayPer < 40 ? "はやい" : delayPer < 50 ? "ちょーはやい" : "もうだめ");
            //result += " / エビの状態:" + ( delayPer < 10 ? "割といい" : delayPer < 20 ? "ちょっと良くない" : delayPer < 40 ? "割とよくない" : delayPer < 50 ? "すごく良くない" : "激ぉこ" );
            //result += "(遅延率:" + delayPer + "%)";
            return result;
        }

        /// <summary>
        /// ツールチップに表示する情報を取得します
        /// </summary>
        /// <returns></returns>
        public static string getStatusTooltipString()
        {
            string result = "";
            result += "";
            return result;
        }
    }
}
