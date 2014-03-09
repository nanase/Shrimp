using Shrimp.ControlParts.Tabs;

namespace Shrimp.Module.Queue
{
    class TabQueueData
    {
        private readonly TabControls _sender;
        private readonly object _raw_data;

        public TabQueueData(TabControls sender, object raw_data)
        {
            //
            this._sender = sender;
            this._raw_data = raw_data;
        }

        public object raw_data
        {
            get { return this._raw_data; }
        }

        public TabControls sender
        {
            get { return this._sender; }
        }
    }
}
