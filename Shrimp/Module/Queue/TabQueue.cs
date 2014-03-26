using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Timers;

namespace Shrimp.Module.Queue
{
    /// <summary>
    /// タブキュー
    /// </summary>
    class TabQueue : Queue<TabQueueData>, IQueue
    {
        private System.Timers.Timer queueCheckTimer;
        private bool isStopRequest = false;

        public TabQueue()
        {
            this.queueCheckTimer = new System.Timers.Timer();
            this.queueCheckTimer.Interval = 50;
            this.queueCheckTimer.Elapsed += new ElapsedEventHandler(queueCheckTimer_Elapsed);
            this.queueCheckTimer.Start();
        }

        ~TabQueue()
        {
            Wait();
            this.StopQueue();
            this.queueCheckTimer.Elapsed -= new ElapsedEventHandler(queueCheckTimer_Elapsed);
        }

        private void StopQueue()
        {
            if (this.Enabled)
                return;
            this.queueCheckTimer.Stop();
        }

        public bool Enabled
        {
            get
            {
                return this.queueCheckTimer.Enabled;
            }
        }

        /// <summary>
        /// エンキュー
        /// </summary>
        /// <param name="data"></param>
        public new void Enqueue(TabQueueData data)
        {
            if ( isStopRequest )
                return;

            lock (((ICollection)this).SyncRoot)
            {
                base.Enqueue(data);
            }
        }

        /// <summary>
        /// デキュー
        /// </summary>
        public new TabQueueData Dequeue()
        {
            TabQueueData data = null;
            lock (((ICollection)this).SyncRoot)
            {
                data = base.Dequeue();
            }
            return data;
        }

        public void Wait()
        {
            this.isStopRequest = true;
            while (true)
            {
                lock (((ICollection)this).SyncRoot)
                {
                    if (this.Count == 0)
                    {
                        // 
                        this.StopQueue();
                        return;
                    }
                }
                Thread.Sleep(1);
            }
        }

        void queueCheckTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            lock (((ICollection)this).SyncRoot)
            {
                if (this.Count != 0)
                {
                    var tmp = base.Dequeue();
                    tmp.ActionData.BeginInvoke ( null, null );
                }
            }
        }
    }
}
