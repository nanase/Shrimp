using System;
using System.Windows.Forms;
using Shrimp.Account;
using Shrimp.ControlParts.AuthPanel;
using Shrimp.Twitter;

namespace Shrimp.Setting.Forms
{
    /// <summary>
    /// アカウント管理のコントロールパネル
    /// </summary>
    public partial class AccountManagement : UserControl
    {
        private AccountManager accountManager;
        private AccountRegister accountRegister;
        private Shrimp.OnDeletingUserInformationDelegate OnDeletingUserInformation;
        private Shrimp.OnAddingUserInformationDelegate OnAddingUserInformation;

        public AccountManagement(AccountManager accountManager, Shrimp.OnDeletingUserInformationDelegate OnDeletingUserInformation,
            Shrimp.OnAddingUserInformationDelegate OnAddingUserInformation)
        {
            InitializeComponent();
            this.OnDeletingUserInformation = OnDeletingUserInformation;
            this.OnAddingUserInformation = OnAddingUserInformation;
            this.accountManager = accountManager;
            this.consumerKeySelected.SelectedIndex = 0;
        }

        private void AccountManagement_Load(object sender, EventArgs e)
        {
            this.RedrawAccounts();
        }

        private void AccountAuthorizedButton_Click(object sender, EventArgs e)
        {
            if (this.consumerKeySelected.SelectedIndex <= 0)
            {
                //  ck, csオリジナル
                this.accountRegister = new AccountRegister("AZZEt8SJg2CY60h3iB4kaw", "JXqD9GxFrJx6QBz0BV8jkdWfoAGH184Jj1iFJOKRWBU");
                //this.accountRegister = new AccountRegister ( "hoFsyRdSsx6dmo2A8op1w", "LzCxIXP6twzvZNWpQLyAhn2HUnRQD3Oh7v6IOkmo" );
            }
            else
            {
                if (String.IsNullOrEmpty(this.consumer_key_box.Text) || String.IsNullOrEmpty(this.consumer_secret_key_box.Text))
                    return;
                this.accountRegister = new AccountRegister(this.consumer_key_box.Text,
                                                             this.consumer_secret_key_box.Text);
            }
            this.accountRegister.CompletedAuthorizeTwitter += new AccountRegister.CompletedAuthorizeTwitterDelegate(accountRegister_CompletedAuthorizeTwitter);
            this.accountRegister.ShowDialog();
        }

        public void RedrawAccounts()
        {
            if (this.accountView.InvokeRequired)
            {
                this.accountView.Invoke((MethodInvoker)delegate()
                {

                    this.accountView.Items.Clear();
                    foreach (TwitterInfo t in this.accountManager.accounts)
                    {
                        this.accountView.Items.Add(new ListViewItem(new string[] { t.ScreenName, "" + t.UserId + "" }));
                    }
                });
            }
            else
            {
                this.accountView.Items.Clear();
                foreach (TwitterInfo t in this.accountManager.accounts)
                {
                    this.accountView.Items.Add(new ListViewItem(new string[] { t.ScreenName, "" + t.UserId + "" }));
                }
            }
        }

        void accountRegister_CompletedAuthorizeTwitter(object sender, TwitterInfo data)
        {
            AccountRegister obj = sender as AccountRegister;
            obj.CompletedAuthorizeTwitter -= new AccountRegister.CompletedAuthorizeTwitterDelegate(accountRegister_CompletedAuthorizeTwitter);
            if (OnAddingUserInformation.Invoke(data))
                this.RedrawAccounts();
        }

        private void consumerKeySelected_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            ComboBox c = sender as ComboBox;
            bool isEnabled = c.SelectedIndex > 0;
            this.consumer_key_box.Enabled = isEnabled;
            this.consumer_secret_key_box.Enabled = isEnabled;
        }

        private void accountDeleteButton_Click(object sender, EventArgs e)
        {
            if (this.accountView.SelectedIndices.Count == 0 || this.accountView.SelectedIndices[0] < 0)
                return;
            var selNum = this.accountView.SelectedIndices[0];

            var s = this.accountView.SelectedItems[0];
            if (MessageBox.Show("@" + s.Text + "を削除します。よろしいですか", "注意", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            this.accountView.Enabled = false;
            this.OnDeletingUserInformation.Invoke(selNum);
            this.accountView.Enabled = true;
            this.RedrawAccounts();
        }
    }
}
