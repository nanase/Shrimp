using System;
using System.Windows.Forms;

namespace Shrimp.Log
{
    public partial class LogViewer : Form
    {
        #region -- Private Fields --
        private int offset = 0;
        #endregion

        #region -- Constructors --
        public LogViewer ()
        {
            InitializeComponent ();
        }
        #endregion

        #region -- Private Methods --
        private void logWatchTimer_Tick ( object sender, EventArgs e )
        {
            if ( LogControl.Count != offset )
            {
                this.textBox1.Text = LogControl.AllLogData;
                this.offset = LogControl.Count;
            }
        }
        #endregion
    }
}
