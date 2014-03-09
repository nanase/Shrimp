namespace Shrimp.Setting.Forms
{
    partial class AccountManagement
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

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.AccountAuthorizedButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.accountDeleteButton = new System.Windows.Forms.Button();
            this.accountView = new System.Windows.Forms.ListView();
            this.screen_name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.user_id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AccountListMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.アカウントを削除DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingSplit = new System.Windows.Forms.SplitContainer();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.consumerKeySelected = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.consumer_secret_key_box = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.consumer_key_box = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.AccountListMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SettingSplit)).BeginInit();
            this.SettingSplit.Panel1.SuspendLayout();
            this.SettingSplit.Panel2.SuspendLayout();
            this.SettingSplit.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.AccountAuthorizedButton);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(320, 78);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "アカウント追加";
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(3, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(314, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "アカウント認証を行います";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AccountAuthorizedButton
            // 
            this.AccountAuthorizedButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.AccountAuthorizedButton.Location = new System.Drawing.Point(3, 37);
            this.AccountAuthorizedButton.Name = "AccountAuthorizedButton";
            this.AccountAuthorizedButton.Size = new System.Drawing.Size(314, 38);
            this.AccountAuthorizedButton.TabIndex = 0;
            this.AccountAuthorizedButton.Text = "認証する";
            this.AccountAuthorizedButton.UseVisualStyleBackColor = true;
            this.AccountAuthorizedButton.Click += new System.EventHandler(this.AccountAuthorizedButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.accountDeleteButton);
            this.groupBox2.Controls.Add(this.accountView);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 127);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(320, 194);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "アカウント登録一覧";
            // 
            // accountDeleteButton
            // 
            this.accountDeleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.accountDeleteButton.Location = new System.Drawing.Point(250, 159);
            this.accountDeleteButton.Name = "accountDeleteButton";
            this.accountDeleteButton.Size = new System.Drawing.Size(64, 29);
            this.accountDeleteButton.TabIndex = 1;
            this.accountDeleteButton.Text = "削除";
            this.accountDeleteButton.UseVisualStyleBackColor = true;
            this.accountDeleteButton.Click += new System.EventHandler(this.accountDeleteButton_Click);
            // 
            // accountView
            // 
            this.accountView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.screen_name,
            this.user_id});
            this.accountView.ContextMenuStrip = this.AccountListMenu;
            this.accountView.Dock = System.Windows.Forms.DockStyle.Top;
            this.accountView.FullRowSelect = true;
            this.accountView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.accountView.Location = new System.Drawing.Point(3, 15);
            this.accountView.MultiSelect = false;
            this.accountView.Name = "accountView";
            this.accountView.ShowGroups = false;
            this.accountView.Size = new System.Drawing.Size(314, 138);
            this.accountView.TabIndex = 0;
            this.accountView.UseCompatibleStateImageBehavior = false;
            this.accountView.View = System.Windows.Forms.View.Details;
            // 
            // screen_name
            // 
            this.screen_name.Text = "アカウント名";
            this.screen_name.Width = 250;
            // 
            // user_id
            // 
            this.user_id.Text = "ユーザーID";
            this.user_id.Width = 250;
            // 
            // AccountListMenu
            // 
            this.AccountListMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.アカウントを削除DToolStripMenuItem});
            this.AccountListMenu.Name = "AccountListMenu";
            this.AccountListMenu.Size = new System.Drawing.Size(200, 26);
            // 
            // アカウントを削除DToolStripMenuItem
            // 
            this.アカウントを削除DToolStripMenuItem.Name = "アカウントを削除DToolStripMenuItem";
            this.アカウントを削除DToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.アカウントを削除DToolStripMenuItem.Text = "アカウントを削除(&D)";
            // 
            // SettingSplit
            // 
            this.SettingSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SettingSplit.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.SettingSplit.IsSplitterFixed = true;
            this.SettingSplit.Location = new System.Drawing.Point(0, 0);
            this.SettingSplit.Name = "SettingSplit";
            this.SettingSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // SettingSplit.Panel1
            // 
            this.SettingSplit.Panel1.Controls.Add(this.groupBox1);
            // 
            // SettingSplit.Panel2
            // 
            this.SettingSplit.Panel2.Controls.Add(this.groupBox3);
            this.SettingSplit.Panel2.Controls.Add(this.groupBox2);
            this.SettingSplit.Size = new System.Drawing.Size(320, 400);
            this.SettingSplit.SplitterDistance = 75;
            this.SettingSplit.TabIndex = 2;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.consumerKeySelected);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.consumer_secret_key_box);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.consumer_key_box);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(320, 127);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "コンシューマーキー設定";
            // 
            // consumerKeySelected
            // 
            this.consumerKeySelected.Dock = System.Windows.Forms.DockStyle.Top;
            this.consumerKeySelected.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.consumerKeySelected.FormattingEnabled = true;
            this.consumerKeySelected.Items.AddRange(new object[] {
            "Shrimpキーを使う(通常はこちらを選択してください)",
            "独自キーを使う"});
            this.consumerKeySelected.Location = new System.Drawing.Point(3, 15);
            this.consumerKeySelected.Name = "consumerKeySelected";
            this.consumerKeySelected.Size = new System.Drawing.Size(314, 20);
            this.consumerKeySelected.TabIndex = 6;
            this.consumerKeySelected.SelectedIndexChanged += new System.EventHandler(this.consumerKeySelected_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "コンシューマーシークレット";
            // 
            // consumer_secret_key_box
            // 
            this.consumer_secret_key_box.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.consumer_secret_key_box.Enabled = false;
            this.consumer_secret_key_box.Location = new System.Drawing.Point(133, 71);
            this.consumer_secret_key_box.Name = "consumer_secret_key_box";
            this.consumer_secret_key_box.Size = new System.Drawing.Size(181, 19);
            this.consumer_secret_key_box.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "コンシューマーキー";
            // 
            // consumer_key_box
            // 
            this.consumer_key_box.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.consumer_key_box.Enabled = false;
            this.consumer_key_box.Location = new System.Drawing.Point(133, 45);
            this.consumer_key_box.Name = "consumer_key_box";
            this.consumer_key_box.Size = new System.Drawing.Size(181, 19);
            this.consumer_key_box.TabIndex = 2;
            // 
            // AccountManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SettingSplit);
            this.Name = "AccountManagement";
            this.Size = new System.Drawing.Size(320, 400);
            this.Load += new System.EventHandler(this.AccountManagement_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.AccountListMenu.ResumeLayout(false);
            this.SettingSplit.Panel1.ResumeLayout(false);
            this.SettingSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SettingSplit)).EndInit();
            this.SettingSplit.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button AccountAuthorizedButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView accountView;
        private System.Windows.Forms.ColumnHeader screen_name;
        private System.Windows.Forms.ColumnHeader user_id;
        private System.Windows.Forms.SplitContainer SettingSplit;
        private System.Windows.Forms.Button accountDeleteButton;
        private System.Windows.Forms.ContextMenuStrip AccountListMenu;
        private System.Windows.Forms.ToolStripMenuItem アカウントを削除DToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox consumerKeySelected;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox consumer_secret_key_box;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox consumer_key_box;
    }
}
