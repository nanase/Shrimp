using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading;

namespace Shrimp.ControlParts.Timeline.Animation
{
    /// <summary>
    /// アニメーションを管理します
    /// </summary>
    class AnimationControl : IDisposable
    {
        #region 定義
        /// <summary>
        /// 毎フレームに実行されるデリゲートです
        /// trueが返されると、再描画を行います
        /// </summary>
        public delegate bool FrameExecuteDelegate ();
        public delegate bool RedrawControlDelegate ();
        /// <summary>
        /// 再描画のスタックをまとめます
        /// </summary>
        private bool RedrawFlg = false;
        private System.Timers.Timer timer = new System.Timers.Timer ();
        private List<AnimationDelegate> delegateControl = new List<AnimationDelegate> ();
        private decimal counter = 0;
        private RedrawControlDelegate redrawControl;
        private object lockObj = new object ();
        #endregion

        #region コンストラクタ
        public AnimationControl ( RedrawControlDelegate redraw, FrameExecuteDelegate notify, int interval, FrameExecuteDelegate insert, int interval2,
                                    FrameExecuteDelegate tab, int interval3 )
        {
            timer.Interval = 16;
            timer.Elapsed += new ElapsedEventHandler ( timer_Elapsed );
            this.Start ();
            this.redrawControl = redraw;
            if ( notify != null )
                delegateControl.Add ( new AnimationDelegate () { frame_deleage = notify, Interval = interval } );
            if ( insert != null )
                delegateControl.Add ( new AnimationDelegate () { frame_deleage = insert, Interval = interval2 } );
            if ( tab != null )
                delegateControl.Add ( new AnimationDelegate () { frame_deleage = tab, Interval = interval3 } );
        }
        #endregion

        ~AnimationControl ()
        {
        }

        public void Dispose ()
        {
            this.timer.Stop ();
            this.timer.Close ();
            this.timer.Dispose ();
            delegateControl.Clear ();
            delegateControl = null;
            this.redrawControl = null;
            timer.Elapsed -= new ElapsedEventHandler ( timer_Elapsed );
            timer = null;
            /*
            for ( ; ; )
            {
                if ( !isDrawing )
                    break;
                //Thread.Sleep ( 0 );
            }
            */
            GC.SuppressFinalize ( this );
        }

        /// <summary>
        /// スタート
        /// </summary>
        public void Start ()
        {
            this.timer.Start ();
        }

        /// <summary>
        /// ストップ
        /// </summary>
        public void Stop ()
        {
            this.timer.Stop ();
        }

        /// <summary>
        /// 再描画のキューを追加します
        /// </summary>
        public void SetRedrawQueue ()
        {
            this.RedrawFlg = true;
        }

        /// <summary>
        /// 再描画のキューを追加します
        /// </summary>
        public void DestroyRedrawQueue ()
        {
            this.RedrawFlg = false;
        }
        /// <summary>
        /// タイマー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timer_Elapsed ( object sender, ElapsedEventArgs e )
        {
            lock ( lockObj )
            {
                foreach ( AnimationDelegate d in delegateControl )
                {
                    if ( counter % ( d.Interval - 15 ) == 0 )
                    {
                        FrameExecuteDelegate f = new FrameExecuteDelegate ( d.frame_deleage );
                        if ( f.Invoke () )
                            SetRedrawQueue ();
                    }
                }

                if ( this.RedrawFlg )
                {
                    if ( redrawControl != null )
                    {
                        redrawControl.BeginInvoke (null,null);
                        this.RedrawFlg = false;
                    }
                }
                counter++;
            }
        }

    }
}
