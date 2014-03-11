namespace Shrimp.Setting.Forms
{
    partial class SettingForms
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("アカウント管理");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("タブ色");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("タブの設定", new System.Windows.Forms.TreeNode[] {
            treeNode2});
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("ツイート色");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("タイムライン", new System.Windows.Forms.TreeNode[] {
            treeNode4});
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("ユーザーストリーム");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("一般設定", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode3,
            treeNode6,
            treeNode7});
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("グローバルミュート");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("ショートカットキーの設定");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("プラグイン");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Shrimpの情報");
            this.MainSplit = new System.Windows.Forms.SplitContainer();
            this.SettingListView = new System.Windows.Forms.TreeView();
            ((System.ComponentModel.ISupportInitialize)(this.MainSplit)).BeginInit();
            this.MainSplit.Panel1.SuspendLayout();
            this.MainSplit.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainSplit
            // 
            this.MainSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainSplit.Location = new System.Drawing.Point(0, 0);
            this.MainSplit.Name = "MainSplit";
            // 
            // MainSplit.Panel1
            // 
            this.MainSplit.Panel1.Controls.Add(this.SettingListView);
            this.MainSplit.Size = new System.Drawing.Size(624, 441);
            this.MainSplit.SplitterDistance = 145;
            this.MainSplit.TabIndex = 0;
            // 
            // SettingListView
            // 
            this.SettingListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SettingListView.Location = new System.Drawing.Point(0, 0);
            this.SettingListView.Name = "SettingListView";
            treeNode1.Name = "AccountSetting";
            treeNode1.Text = "アカウント管理";
            treeNode2.Name = "TabColorSetting";
            treeNode2.Text = "タブ色";
            treeNode3.Name = "TabMenuSetting";
            treeNode3.Text = "タブの設定";
            treeNode4.Name = "TimelineColorSetting";
            treeNode4.Text = "ツイート色";
            treeNode5.Name = "FontNode";
            treeNode5.Text = "フォント";
            treeNode6.Name = "TimelineSetting";
            treeNode6.Text = "タイムライン";
            treeNode7.Name = "UserStreamMenuSetting";
            treeNode7.Text = "ユーザーストリーム";
            treeNode8.Name = "ShrimpSetting";
            treeNode8.Text = "一般設定";
            treeNode9.Name = "GlobalMuteMenu";
            treeNode9.Text = "グローバルミュート";
            treeNode10.Name = "ShortcutKeySetting";
            treeNode10.Text = "ショートカットキーの設定";
            treeNode11.Name = "PluginNode";
            treeNode11.Text = "プラグイン";
            treeNode12.Name = "ShrimpInfo";
            treeNode12.Text = "Shrimpの情報";
            this.SettingListView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode7,
            treeNode8,
            treeNode9,
            treeNode10,
            treeNode11,
            treeNode12});
            this.SettingListView.Size = new System.Drawing.Size(145, 441);
            this.SettingListView.TabIndex = 0;
            this.SettingListView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.SettingListView_NodeMouseClick);
            // 
            // SettingForms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.MainSplit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "SettingForms";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "設定";
            this.MainSplit.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainSplit)).EndInit();
            this.MainSplit.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer MainSplit;
        private System.Windows.Forms.TreeView SettingListView;
    }
}

