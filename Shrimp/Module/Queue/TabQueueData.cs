using Shrimp.ControlParts.Tabs;

namespace Shrimp.Module.Queue
{
    class TabQueueData
    {
        public delegate void TabQueueActionDelegate ();

        private readonly TabQueueActionDelegate _tabQueueDelegate;
        private readonly ShrimpTabs _sender;
        private readonly object _raw_data;

        public TabQueueData ( ShrimpTabs sender, TabQueueActionDelegate _tabQueueDelegate )
        {
            //
            this._sender = sender;
            this._tabQueueDelegate = _tabQueueDelegate;
        }

        public TabQueueActionDelegate ActionData
        {
            get { return this._tabQueueDelegate; }
        }

        public ShrimpTabs sender
        {
            get { return this._sender; }
        }
    }
}
