using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Shrimp.ControlParts.User
{
    public partial class UserInformation : UserControl
    {
        public UserInformation()
        {
            InitializeComponent();
        }

        public Control Panel1
        {
            get { return this.UserInformationSplit.Panel1; }
            set
            {
                value.Dock = DockStyle.Fill;
                this.UserInformationSplit.Panel1.Controls.Add ( value );
            }
        }
    }
}
