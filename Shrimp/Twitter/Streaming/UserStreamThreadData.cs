using System.Threading;

namespace Shrimp.Twitter.Streaming
{
    class UserStreamThreadData
    {
        private Thread thread;

        public UserStreamThreadData()
        {

        }

        public UserStreamThreadData ( Thread thread, bool isStopFlag )
        {
            this.thread = thread;
            this.isStopFlag = isStopFlag;
        }

        public Thread Thread
        {
            get { return this.thread; }
            set { this.thread = value; }
        }

        /// <summary>
        /// スレッドの終了フラグがたっているかどうか
        /// </summary>
        public bool isStopFlag
        {
            get;
            set;
        }

        /// <summary>
        /// 終了時、プログラムの終了とかだともう通知を出す必要がないので、通知を抑制するかどうか
        /// </summary>
        public bool isDestroy
        {
            get;
            set;
        }

        /// <summary>
        /// スレッドが終了したかどうか
        /// </summary>
        public bool isFinishedThread
        {
            get;
            set;
        }
    }
}
