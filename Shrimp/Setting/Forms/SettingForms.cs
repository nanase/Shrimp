using System.Windows.Forms;
using Shrimp.Account;

namespace Shrimp.Setting.Forms
{
    //	設定画面
    public partial class SettingForms : Form
    {
        private AccountManager accountManager;
        private Shrimp shrimp;
        private Control BeforeSelectControl = null;
        private string BeforeSelectControlNodeName = null;
        private Shrimp.OnChangedTabControlAlignment OnTabAlignChanged;
        private Shrimp.OnDeletingUserInformationDelegate OnDeletingUserInformation;
        private Shrimp.OnAddingUserInformationDelegate OnAddingUserInformation;

        public SettingForms(AccountManager accountManager, bool isSelectedAccountRegist, Shrimp shrimp, Shrimp.OnChangedTabControlAlignment OnTabAlignChanged,
                                Shrimp.OnDeletingUserInformationDelegate OnDeletingUserInformation, Shrimp.OnAddingUserInformationDelegate OnAddingUserInformation)
        {
            InitializeComponent();
            this.accountManager = accountManager;
            this.shrimp = shrimp;
            this.OnTabAlignChanged = OnTabAlignChanged;
            this.OnDeletingUserInformation = OnDeletingUserInformation;
            this.OnAddingUserInformation = OnAddingUserInformation;
            this.SettingListView.ExpandAll();
            if (isSelectedAccountRegist)
            {
                this.SettingListView.SelectedNode = this.SettingListView.Nodes[0].Nodes[0];
            }
        }

        //	各ノードがクリックされたとき
        private void SettingListView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (BeforeSelectControlNodeName != null && BeforeSelectControlNodeName == e.Node.Name)
                return;
            if (BeforeSelectControl != null)
                BeforeSelectControl.Dispose();

            TreeNode node = e.Node;
            //  バージョン情報
            if (node.Name == "ShrimpInfo")
            {
                BeforeSelectControl = new ShrimpInfo();
            }

            //  アカウント設定
            if (node.Name == "AccountSetting")
            {
                BeforeSelectControl = new AccountManagement(accountManager, OnDeletingUserInformation, OnAddingUserInformation);
            }

            //	タイムラインの設定
            if (node.Name == "TimelineSetting")
            {
                //
                BeforeSelectControl = new TimelineManagement();
            }

            //	タイムライン色
            if (node.Name == "TimelineColorSetting")
            {
                //
                BeforeSelectControl = new TimelineColor();
            }

            //	タブ設定
            if (node.Name == "TabMenuSetting")
            {
                BeforeSelectControl = new TabSetting(OnTabAlignChanged);
            }

            //	タブ食
            if (node.Name == "TabColorSetting")
            {
                BeforeSelectControl = new TabColor();
            }

            if (node.Name == "UserStreamMenuSetting")
            {
                BeforeSelectControl = new UserStreamSetting();
            }

            if (node.Name == "ShortcutKeySetting")
            {
                BeforeSelectControl = new ShortcutKey();
            }

            if (node.Name == "GlobalMuteMenu")
            {
                BeforeSelectControl = new GlobalMute();
            }

            if ( node.Name == "PluginNode" )
            {
                BeforeSelectControl = new PluginForm ();
            }

            if ( node.Name == "FontNode" )
            {
                BeforeSelectControl = new FontForm ();
            }

            if ( node.Name == "PrivateFunctionMenu" )
            {
                BeforeSelectControl = new PrivateFunction ();
            }

            if (BeforeSelectControl != null)
            {
                BeforeSelectControl.Dock = DockStyle.Fill;
                this.MainSplit.Panel2.Controls.Clear();
                this.MainSplit.Panel2.Controls.Add(BeforeSelectControl);
                BeforeSelectControlNodeName = e.Node.Name;
            }
        }
    }
}
