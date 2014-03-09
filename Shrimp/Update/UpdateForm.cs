using System;
using System.Windows.Forms;

namespace Shrimp.Update
{
    public partial class UpdateForm : Form
    {
        public UpdateForm()
        {
            InitializeComponent();
        }

        public void SetLog(string log)
        {
            this.logBox.Text = (string)log.Clone();
            this.logBox.SelectionStart = this.logBox.TextLength;
        }

        private void IgnoreNotifyButton_Click(object sender, EventArgs e)
        {
            this.Tag = "Ignore";
        }
    }
}
