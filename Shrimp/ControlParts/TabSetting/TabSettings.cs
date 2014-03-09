using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Shrimp.Account;
using Shrimp.ControlParts.Tabs;
using Shrimp.Twitter.REST.List;

namespace Shrimp.ControlParts.TabSetting
{
    public partial class TabSettings : Form
    {
        private readonly AccountManager _manager;
        private TabDeliveryCollection _tabData, _tabDataClone;
        private listDataCollection _listDatas;
        private Control control;
        private string beforeSelected = "";
        private string _ignoreTweetClone = "";
        private string _ignoreTweet = "";
        private bool isChanged = false;

        public TabSettings(AccountManager manager, TabDeliveryCollection tabData, string ignoreTweet, listDataCollection listDatas)
        {
            InitializeComponent();

            this._manager = manager;
            this._tabDataClone = (TabDeliveryCollection)tabData.Clone();
            this._tabData = tabData;
            this._listDatas = listDatas;
            this._ignoreTweetClone = (ignoreTweet == null ? "" : (string)ignoreTweet.Clone());
            this._ignoreTweet = ignoreTweet;

            foreach (TabDelivery tab in this._tabDataClone.deliveries)
            {
                this.tabTreeView.Nodes[0].Nodes.Add(CreateTreeNode(tab));
            }
            this.tabTreeView.ExpandAll();
        }

        /// <summary>
        /// ツリーノードを作成する
        /// </summary>
        /// <param name="tab"></param>
        /// <returns></returns>
        private TreeNode CreateTreeNode(TabDelivery tab)
        {
            TreeNode[] nodes = new TreeNode[1];
            nodes[0] = new TreeNode("振り分け元ユーザーの選択");
            nodes[0].Name = "DeliveryFromUserMenu";
            nodes[0].Tag = tab;
            TreeNode topNode = new TreeNode("" + tab.Category.CategoryName + "の設定", nodes);
            topNode.Name = "CategoryMenu";
            topNode.Tag = tab;
            return topNode;
        }

        private void RedrawName()
        {
            foreach (TreeNode t in this.tabTreeView.Nodes[0].Nodes)
            {
                TimelineCategory c = ((TabDelivery)t.Tag).Category;
                if (t.Text != "" + c.CategoryName + "の設定")
                {
                    t.Text = "" + c.CategoryName + "の設定";
                }
            }
        }

        /// <summary>
        /// タブを新しく追加
        /// </summary>
        private void AddNewTabs()
        {
            TabDelivery d = new TabDelivery(new TimelineCategory(TimelineCategories.None, null, false), null);
            this._tabDataClone.deliveries.Add(d);
            this.tabTreeView.Nodes[0].Nodes.Add(CreateTreeNode(d));
        }


        /// <summary>
        /// タブを削除
        /// </summary>
        private void RemoveTabs(TabDelivery tab)
        {
            if (this._tabDataClone.deliveries.Count == 1)
                return;
            int i = this._tabDataClone.deliveries.FindIndex((t) => t == tab);
            this._tabDataClone.deliveries.Remove(tab);
            this.tabTreeView.Nodes[0].Nodes.RemoveAt(i);
        }

        private void tabTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Name == "DeliveryFromUserMenu")
            {
                InitializeForm(this.beforeSelected);
                DeliverFromUser user = new DeliverFromUser(_manager, (TabDelivery)e.Node.Tag) { Dock = DockStyle.Fill };
                this.control = user;
                this.MainSplit.Panel2.Controls.Clear();
                this.MainSplit.Panel2.Controls.Add(control);
                RedrawName();
                this.beforeSelected = e.Node.Name;
            }

            if (e.Node.Name == "CategoryMenu")
            {
                InitializeForm(this.beforeSelected);
                this.control = new TabCategory((TabDelivery)e.Node.Tag, _listDatas) { Dock = DockStyle.Fill };
                this.MainSplit.Panel2.Controls.Clear();
                this.MainSplit.Panel2.Controls.Add(this.control);
                RedrawName();
                this.beforeSelected = e.Node.Name;
            }

            if (e.Node.Name == "TweetIgnoreMenu")
            {
                InitializeForm(this.beforeSelected);
                this.control = new MuteTweet(this._ignoreTweet) { Dock = DockStyle.Fill };
                this.MainSplit.Panel2.Controls.Clear();
                this.MainSplit.Panel2.Controls.Add(this.control);
                RedrawName();
                this.beforeSelected = e.Node.Name;
            }
        }

        private void InitializeForm(string name, bool isDispose = true)
        {
            if (name == "DeliveryFromUserMenu")
            {
                DeliverFromUser d = this.control as DeliverFromUser;
                if (d.Tag != null && (bool)d.Tag == true)
                    this.isChanged = true;
                if (isDispose)
                    d.Dispose();
            }
            if (name == "CategoryMenu")
            {
                TabCategory d = this.control as TabCategory;
                if (d.Tag != null && (bool)d.Tag == true)
                    this.isChanged = true;
                if (isDispose)
                    d.Dispose();
            }
            if (name == "TweetIgnoreMenu")
            {
                MuteTweet d = this.control as MuteTweet;
                if (d.Tag != null)
                {
                    this._ignoreTweetClone = d.Tag as string;
                    this.isChanged = true;
                }
                if (isDispose)
                    d.Dispose();
            }
        }

        private void OnChangedDeliveryUsersDelegate(List<decimal> data)
        {
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button bt = sender as Button;
            TreeNode node = this.tabTreeView.SelectedNode;
            if (bt.Name == "addTabsButton" && node != null)
            {
                //  追加
                if (node.Name == "CategoryMenu" || node.Name == "TopNode")
                {
                    AddNewTabs();
                    this.isChanged = true;
                }
            }

            if (bt.Name == "delTabsButton")
            {
                //  削除
                if (node.Name == "CategoryMenu" || node.Name == "DeliveryFromUserMenu")
                {
                    //
                    RemoveTabs((TabDelivery)node.Tag);
                    this.isChanged = true;
                }
            }
        }

        private void TabSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            InitializeForm(this.beforeSelected, false);
            if (this.isChanged)
            {
                DialogResult d = MessageBox.Show("変更された設定を適用しますか？", "注意", MessageBoxButtons.YesNoCancel);
                if (d == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
                if (d == DialogResult.No)
                    return;
                this.Tag = new object[] { this._tabDataClone.Clone(), this._ignoreTweetClone.Clone() };
            }
        }

    }
}
