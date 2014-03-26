using System.Threading;

namespace Shrimp.Twitter.Streaming
{
    class UserStreamThreadData
    {

        public Thread Thread { get; private set; }

        /// <summary>
        /// スレッドの終了フラグがたっているかどうか
        /// </summary>
        public bool IsStopFlag { get; set; }

        public bool IsFinishedThread { get; set; }

        /// <summary>
        /// 終了時、プログラムの終了とかだともう通知を出す必要がないので、通知を抑制するかどうか
        /// </summary>
        public bool isDestroy { get; set; }

        public UserStreamThreadData()
        {
        }

        public UserStreamThreadData(Thread thread, bool isFinished)
        {
            this.Thread = thread;
            this.isFinished = isFinished;
        }
    }
}
