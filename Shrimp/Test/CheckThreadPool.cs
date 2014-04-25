using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Shrimp.Test
{
    public partial class CheckThreadPool : Form
    {
        public CheckThreadPool ()
        {
            InitializeComponent ();
            this.timer1.Start ();
        }

        private void timer1_Tick ( object sender, EventArgs e )
        {
            var now = DateTime.Now;
            int availableWorkerThreads = 0;
            int availableCompletionPortThreads = 0;
            int maxWorkerThreads = 0;
            int maxCompletionPortThreads = 0;
            ThreadPool.GetAvailableThreads ( out availableWorkerThreads, out availableCompletionPortThreads );
            ThreadPool.GetMaxThreads ( out maxWorkerThreads, out maxCompletionPortThreads );
            label1.Text = "" + availableWorkerThreads +" / "+ availableCompletionPortThreads +"";
        }
    }
}
