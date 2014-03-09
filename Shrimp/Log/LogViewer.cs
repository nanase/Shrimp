using System;
using System.Windows.Forms;

namespace Shrimp.Log
{
    public partial class LogViewer : Form
    {
        private int offset = 0;
        public LogViewer()
        {
            InitializeComponent();
        }

        private void logWatchTimer_Tick(object sender, EventArgs e)
        {
            if (LogControl.Count != offset)
            {
                this.textBox1.Text = LogControl.allLogData;
                this.offset = LogControl.Count;
            }
        }
    }
}
