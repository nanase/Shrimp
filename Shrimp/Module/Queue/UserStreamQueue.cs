using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shrimp.Twitter.Streaming;
using System.Timers;
using System.Threading;
using System.Collections;

namespace Shrimp.Module.Queue
{
    /// <summary>
    /// UserStreamで発生したイベントをQueueに回す
    /// </summary>
    class UserStreamQueue : Queue<UserStreamQueueData>, IQueue
    {
        private System.Timers.Timer queueCheckTimer;

        public UserStreamQueue ()
        {
            this.queueCheckTimer = new System.Timers.Timer ();
            this.queueCheckTimer.Interval = 100;
            this.queueCheckTimer.Elapsed += new ElapsedEventHandler ( queueCheckTimer_Elapsed );
            this.queueCheckTimer.Start ();
        }

        ~UserStreamQueue ()
        {
            Wait ();
            this.StopQueue ();
            this.queueCheckTimer.Stop ();
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
        public new void Enqueue ( UserStreamQueueData data )
        {
            lock ( ( (ICollection)this).SyncRoot )
            {
                base.Enqueue ( data );
            }
        }

        /// <summary>
        /// デキュー
        /// </summary>
        public new UserStreamQueueData Dequeue ()
        {
            UserStreamQueueData data = null;
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
                    if ( tmp.EventHandler is UserStreaming.TweetEventDelegate )
                    {
                        UserStreaming.TweetEventDelegate obj = tmp.EventHandler as UserStreaming.TweetEventDelegate;
                        obj.BeginInvoke ( tmp.sender, tmp.args, null, null );
                    }
                    if ( tmp.EventHandler is UserStreaming.NotifyEventDelegate )
                    {
                        UserStreaming.NotifyEventDelegate obj = tmp.EventHandler as UserStreaming.NotifyEventDelegate;
                        obj.BeginInvoke ( tmp.sender, tmp.args, null, null );
                    }
                    if ( tmp.EventHandler is UserStreaming.UserStreamingconnectStatusEventDelegate )
                    {
                        UserStreaming.UserStreamingconnectStatusEventDelegate obj = tmp.EventHandler as UserStreaming.UserStreamingconnectStatusEventDelegate;
                        obj.BeginInvoke ( tmp.sender, tmp.args, null, null );
                    }
                }
            }
        }
    }
}
