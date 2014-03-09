using System;
using System.Windows.Forms;

namespace Shrimp.ControlParts.TabSetting
{
    public partial class MuteTweet : UserControl
    {
        public MuteTweet(string text)
        {
            InitializeComponent();
            this.queryBox.Text = text;
            this.ApplyButton.Enabled = false;
        }

        private void queryBox_TextChanged(object sender, EventArgs e)
        {
            this.ApplyButton.Enabled = true;
        }

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            this.ApplyButton.Enabled = false;
            this.Tag = (string)this.queryBox.Text.Clone();
        }

        private void queryBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
                queryBox.SelectAll();
        }
    }
}
