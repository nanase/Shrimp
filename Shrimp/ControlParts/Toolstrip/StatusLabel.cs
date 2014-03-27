using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using Shrimp.ControlParts.Timeline.Animation;

namespace Shrimp.ControlParts.Toolstrip
{
    /// <summary>
    /// アニメーション機能がついたToolStripStatusLabel
    /// </summary>
    public class StatusLabel : ToolStripStatusLabel
    {
        private Queue<ToolStripStatusLabelText> queue;
        private System.Timers.Timer queueTimer;
        private System.Windows.Forms.Timer animeTimer;
        private ToolStripAnimation animation;

        public StatusLabel ()
        {
            this.queue = new Queue<ToolStripStatusLabelText> ();
            this.animation = new ToolStripAnimation ();

            this.animeTimer = new System.Windows.Forms.Timer ();
            this.animeTimer.Interval = 16;
            this.animeTimer.Tick += new EventHandler ( animeTimer_Tick );
            this.animeTimer.Start ();

            this.queueTimer = new System.Timers.Timer ();
            this.queueTimer.Interval = 16;
            this.queueTimer.Elapsed += new System.Timers.ElapsedEventHandler ( queueTimer_Elapsed );
            this.queueTimer.Start ();
        }

        void animeTimer_Tick ( object sender, EventArgs e )
        {
            if ( animation.FrameExecute () )
            {
                this.Invalidate ();
            }
        }

        ~StatusLabel ()
        {
            this.queueTimer.Stop ();
            this.animeTimer.Stop ();
        }

        void queueTimer_Elapsed ( object sender, System.Timers.ElapsedEventArgs e )
        {
            lock ( ( (ICollection)this.queue ).SyncRoot )
            {
                if ( this.queue.Count != 0 )
                {
                    if ( !this.animation.Enable )
                    {
                        //  
                        this.animation.StartAnimation ( new object[] { 
                            this.Font.Height, this.queue.Dequeue () } );
                    }
                }
            }
        }

        /// <summary>
        /// テキストを追加します
        /// </summary>
        /// <param name="text"></param>
        /// <param name="image"></param>
        public void AddText ( string text, Bitmap image )
        {
            lock ( ( (ICollection)this.queue ).SyncRoot )
            {
                this.queue.Enqueue ( new ToolStripStatusLabelText ( text, image ) );
            }
        }

        /// <summary>
        /// オーナードロー
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint ( PaintEventArgs e )
        {
            base.OnPaint ( e );
            this.animation.Draw ( e.Graphics, this.Width, e.ClipRectangle, null, this.Font );
        }
    }
}
