using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Shrimp.ControlParts.Popup
{
    public partial class SearchForm : Form
    {
        public SearchForm ( string detail )
        {
            InitializeComponent ();
            this.ignoreRTBox.Checked = Setting.Search.isIgnoreRT;
            this.onlyJapaneseBox.Checked = Setting.Search.isOnlyJapanese;
            this.searchBox.Text = (string)detail.Clone ();
            this.ActiveControl = this.searchBox;
        }

        private void searchBox_KeyDown ( object sender, KeyEventArgs e )
        {
            if ( e.KeyCode == Keys.Enter )
                OKButton_Click ( sender, e );
            if ( e.KeyCode == Keys.Escape )
                this.Close ();
        }

        private void OKButton_Click ( object sender, EventArgs e )
        {
            this.Tag = this.searchBox.Text.Clone ();
            this.DialogResult = DialogResult.OK;
            this.Close ();
        }

        private void CancelButton_Click ( object sender, EventArgs e )
        {
            this.Tag = this.searchBox.Text.Clone ();
            this.DialogResult = DialogResult.Cancel;
            this.Close ();
        }

        private void onlyJapaneseBox_CheckedChanged ( object sender, EventArgs e )
        {
            CheckBox chk = sender as CheckBox;
            if ( chk.Name == "ignoreRTBox" )
                Setting.Search.isIgnoreRT = chk.Checked;
            if ( chk.Name == "onlyJapaneseBox" )
                Setting.Search.isOnlyJapanese = chk.Checked;
        }

        private void SearchForm_KeyDown ( object sender, KeyEventArgs e )
        {
            if ( e.KeyCode == Keys.Escape )
                this.Close ();
        }
    }
}
