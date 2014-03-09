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
    public partial class TabNameChange : Form
    {
        private string _name = "";
        public TabNameChange ( string name )
        {
            InitializeComponent ();
            _name = name;
            this.NameBox.Text = _name;
        }

        private void OKButton_Click ( object sender, EventArgs e )
        {
            this.Tag = (string)this.NameBox.Text.Clone ();
            this.DialogResult = DialogResult.OK;
            this.Close ();
        }

        private void CancelButton_Click ( object sender, EventArgs e )
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close ();
        }

        private void NameBox_KeyDown ( object sender, KeyEventArgs e )
        {
            if ( e.KeyCode == Keys.Enter )
            {
                OKButton_Click ( null, null );
            }
            if ( e.KeyCode == Keys.Escape )
                this.Close ();
        }

    }
}
