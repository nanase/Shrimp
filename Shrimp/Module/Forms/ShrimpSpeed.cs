using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Shrimp.Module.Forms
{
    /// <summary>
    /// ツイートの流速を測るクラス
    /// </summary>
    class ShrimpSpeed : Timer
    {
        /// <summary>
        /// Shrimpのスピードを測るための変数
        /// </summary>
        private int shrimpSpeedCount = 0;
        private OnChnageSpeedDelegate OnChangeSpeed = null;
        private object lockSpeedCount = null;

        #region デリゲート
        public delegate void OnChnageSpeedDelegate ( int percentage );
        #endregion

        public ShrimpSpeed ( OnChnageSpeedDelegate OnChangeSpeed )
        {
            this.OnChangeSpeed = OnChangeSpeed;
            this.lockSpeedCount = new object ();
            this.Interval = 10000;
            this.Tick += new EventHandler ( ShrimpSpeed_Tick );
            this.Start ();
        }

        void ShrimpSpeed_Tick ( object sender, EventArgs e )
        {
            var speed = 0;
            lock ( this.lockSpeedCount )
            {
                speed = (int)( (double)this.shrimpSpeedCount / 100.0 ) * 100;
                this.shrimpSpeedCount = 0;
            }
            OnChangeSpeed.Invoke ( speed );
        }

        /// <summary>
        /// スピードを加速させる
        /// </summary>
        public void IncreaseSpeedCount ()
        {
            lock ( this.lockSpeedCount )
            {
                this.shrimpSpeedCount++;
            }
        }
    }
}
