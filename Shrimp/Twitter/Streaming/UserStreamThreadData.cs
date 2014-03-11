using System.Threading;

namespace Shrimp.Twitter.Streaming
{
    class UserStreamThreadData
    {
        private bool isFinished = false;

        public Thread Thread { get; private set; }

        // FIXME: このプロパティ名を IsFinished に統一したほうがいいかと
        public bool IsStopFlag
        {
            get { return this.isFinished; }
            set { this.isFinished = value; }
        }

        public bool IsFinishedThread
        {
            get;
            set;
        }

        public UserStreamThreadData ()
        {

        }

        public UserStreamThreadData ( Thread thread, bool isFinished )
        {
            this.Thread = thread;
            this.isFinished = isFinished;
        }
    }
}
