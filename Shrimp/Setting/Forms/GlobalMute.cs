using System;
using System.Windows.Forms;

namespace Shrimp.Setting.Forms
{
    public partial class GlobalMute : UserControl, ISettingForm, IDisposable
    {
        public GlobalMute()
        {
            InitializeComponent();
            SettingReflection();
        }

        public void SettingReflection()
        {
            this.queryBox.Text = (string)Setting.Timeline.GlobalMuteString.Clone();
        }

        public void SaveReflection()
        {
            Setting.Timeline.GlobalMuteString = (string)this.queryBox.Text.Clone();
        }

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            SaveReflection();
        }

        private void queryBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
                queryBox.SelectAll();
        }
    }
}
