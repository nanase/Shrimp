using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Shrimp.Account;
using Shrimp.ControlParts.Tabs;
using Shrimp.Twitter;

namespace Shrimp.ControlParts.TabSetting
{
    public partial class DeliverFromUser : UserControl
    {
        private readonly AccountManager _manager;
        private TabDelivery _users;

        public DeliverFromUser(AccountManager manager, TabDelivery users)
        {
            InitializeComponent();
            this._manager = manager;
            this._users = users;

            this.deliveryUsers.Items.Clear();
            this.registUsers.Items.Clear();
            List<string> ii = new List<string>();
            foreach (decimal num in this._users.DeliveryFromUsers)
            {
                TwitterInfo sn = this._manager.accounts.Find((t) => t.UserId == num);
                if (sn != null)
                {
                    this.deliveryUsers.Items.Add(sn.ScreenName);
                    ii.Add(sn.ScreenName);
                }
            }
            foreach (TwitterInfo num in this._manager.accounts)
            {
                if (ii.FindIndex((s) => s == num.ScreenName) >= 0)
                    continue;
                this.registUsers.Items.Add(num.ScreenName);
            }

            setAllCheckBox();
        }

        private void setAllCheckBox()
        {
            this.allUserCheckBox.Checked = this._users.Category.isAllUserAccept;
            if (this.allUserCheckBox.Checked)
            {
                this.deliveryUsers.Enabled = false;
                this.registUsers.Enabled = false;
                this.upbutton.Enabled = false;
                this.downbutton.Enabled = false;
            }
            else
            {
                this.deliveryUsers.Enabled = true;
                this.registUsers.Enabled = true;
                this.upbutton.Enabled = true;
                this.downbutton.Enabled = true;
            }
        }

        private void upbutton_Click(object sender, EventArgs e)
        {
            var t = this.registUsers.SelectedItem;
            if (t != null)
            {
                this.deliveryUsers.Items.Add(t);
                this.registUsers.Items.Remove(t);
                AddUser(this._users.DeliveryFromUsers, (string)t);
                this.Tag = true;
            }
        }

        private void downbutton_Click(object sender, EventArgs e)
        {
            var t = this.deliveryUsers.SelectedItem;

            if (t != null)
            {
                this.registUsers.Items.Add(t);
                this.deliveryUsers.Items.Remove(t);
                TrimUser(this._users.DeliveryFromUsers, (string)t);
                this.Tag = true;
            }
        }

        private void AddUser(List<decimal> list, string screen_name)
        {
            var t = this._manager.accounts.Find((s) => s.ScreenName == screen_name);
            list.Add(t.UserId);
        }

        private void TrimUser(List<decimal> list, string screen_name)
        {
            var t = this._manager.accounts.Find((s) => s.ScreenName == screen_name);
            list.Remove(t.UserId);
        }

        private void allUserCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this._users.Category.isAllUserAccept = this.allUserCheckBox.Checked;
            setAllCheckBox();
            this.Tag = true;
        }

    }
}
