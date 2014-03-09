using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
using System.Collections;

namespace Shrimp.Module.Queue
{
    /// <summary>
    /// タブキュー
    /// </summary>
    class TabQueue : Queue<TabQueueData>, IQueue
    {
        private System.Timers.Timer queueCheckTimer;

        public TabQueue ()
        {
            this.queueCheckTimer = new System.Timers.Timer ();
            this.queueCheckTimer.Interval = 100;
            this.queueCheckTimer.Elapsed += new ElapsedEventHandler ( queueCheckTimer_Elapsed );
            this.queueCheckTimer.Start ();
        }

        ~TabQueue ()
        {
            Wait ();
            this.StopQueue ();
            this.queueCheckTimer.Elapsed -= new ElapsedEventHandler ( queueCheckTimer_Elapsed );
        }

        private void StopQueue ()
        {
            if ( this.Enabled )
                return;
            this.queueCheckTimer.Stop ();
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
        public new void Enqueue ( TabQueueData data )
        {
            lock ( ( (ICollection)this ).SyncRoot )
            {
                base.Enqueue ( data );
            }
        }

        /// <summary>
        /// デキュー
        /// </summary>
        public new TabQueueData Dequeue ()
        {
            TabQueueData data = null;
            lock ( ( (ICollection)this ).SyncRoot )
            {
                data = base.Dequeue ();
            }
            return data;
        }

        public void Wait ()
        {
            while ( true )
            {
                lock ( ( (ICollection)this ).SyncRoot )
                {
                    if ( this.Count == 0 )
                    {
                        // 
                        this.StopQueue ();
                        return;
                    }
                }
                Thread.Sleep ( 1 );
            }
        }

        void queueCheckTimer_Elapsed ( object sender, ElapsedEventArgs e )
        {
            lock ( ( (ICollection)this ).SyncRoot )
            {
                if ( this.Count != 0 )
                {
                    var tmp = this.Dequeue ();
                }
            }
        }
    }
}
