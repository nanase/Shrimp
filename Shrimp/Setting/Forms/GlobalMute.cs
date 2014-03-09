using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Shrimp.Setting.ObjectXML;

namespace Shrimp.Setting.Forms
{
    public partial class GlobalMute : UserControl, ISettingForm, IDisposable
    {
        public GlobalMute ()
        {
            InitializeComponent ();
            SettingReflection ();
        }

		public void SettingReflection()
		{
			this.queryBox.Text = (string)Setting.Timeline.GlobalMuteString.Clone ();
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
