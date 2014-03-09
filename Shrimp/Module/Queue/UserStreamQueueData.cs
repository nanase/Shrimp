using Shrimp.Twitter.REST;
using Shrimp.Twitter.Streaming;

namespace Shrimp.Module.Queue
{
    class UserStreamQueueData
    {
        private readonly TwitterCompletedEventArgs _args;
        private readonly UserStreaming _sender;
        private readonly object _EventHandler;

        public UserStreamQueueData(UserStreaming sender, TwitterCompletedEventArgs args, object EventHandler)
        {
            //
            this._args = (TwitterCompletedEventArgs)args.Clone();
            this._sender = sender;
            this._EventHandler = EventHandler;
        }

        public TwitterCompletedEventArgs args
        {
            get { return this._args; }
        }

        public UserStreaming sender
        {
            get { return this._sender; }
        }

        public object EventHandler
        {
            get { return this._EventHandler; }
        }
    }
}
