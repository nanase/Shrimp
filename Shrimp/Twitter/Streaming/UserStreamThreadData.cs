using System.Threading;

namespace Shrimp.Twitter.Streaming
{
    class UserStreamThreadData
    {
        private Thread thread;
        private bool isFinished = false;

        public UserStreamThreadData()
        {

        }

        public UserStreamThreadData(Thread thread, bool isFinished)
        {
            this.thread = thread;
            this.isFinished = isFinished;
        }

        public Thread Thread
        {
            get { return this.thread; }
            set { this.thread = value; }
        }

        public bool isStopFlag
        {
            get { return this.isFinished; }
            set { this.isFinished = value; }
        }

        public bool isFinishedThread
        {
            get;
            set;
        }
    }
}
