namespace Shrimp.ControlParts.TabSetting
{
    partial class TabSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose ( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose ();
            }
            base.Dispose ( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent ()
        {
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("タブの設定");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("ツイートの除外条件式");
            this.MainSplit = new System.Windows.Forms.SplitContainer();
            this.delTabsButton = new System.Windows.Forms.Button();
            this.addTabsButton = new System.Windows.Forms.Button();
            this.tabTreeView = new System.Windows.Forms.TreeView();
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
            this.MainSplit.Panel1.Controls.Add(this.delTabsButton);
            this.MainSplit.Panel1.Controls.Add(this.addTabsButton);
            this.MainSplit.Panel1.Controls.Add(this.tabTreeView);
            this.MainSplit.Size = new System.Drawing.Size(624, 442);
            this.MainSplit.SplitterDistance = 208;
            this.MainSplit.TabIndex = 0;
            // 
            // delTabsButton
            // 
            this.delTabsButton.Location = new System.Drawing.Point(182, 418);
            this.delTabsButton.Name = "delTabsButton";
            this.delTabsButton.Size = new System.Drawing.Size(23, 24);
            this.delTabsButton.TabIndex = 1;
            this.delTabsButton.Text = "-";
            this.delTabsButton.UseVisualStyleBackColor = true;
            this.delTabsButton.Click += new System.EventHandler(this.Button_Click);
            // 
            // addTabsButton
            // 
            this.addTabsButton.Location = new System.Drawing.Point(144, 418);
            this.addTabsButton.Name = "addTabsButton";
            this.addTabsButton.Size = new System.Drawing.Size(23, 24);
            this.addTabsButton.TabIndex = 0;
            this.addTabsButton.Text = "+";
            this.addTabsButton.UseVisualStyleBackColor = true;
            this.addTabsButton.Click += new System.EventHandler(this.Button_Click);
            // 
            // tabTreeView
            // 
            this.tabTreeView.Location = new System.Drawing.Point(0, 0);
            this.tabTreeView.Name = "tabTreeView";
            treeNode3.Name = "TopNode";
            treeNode3.Text = "タブの設定";
            treeNode4.Name = "TweetIgnoreMenu";
            treeNode4.Text = "ツイートの除外条件式";
            this.tabTreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode4});
            this.tabTreeView.Size = new System.Drawing.Size(208, 412);
            this.tabTreeView.TabIndex = 0;
            this.tabTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tabTreeView_NodeMouseClick);
            // 
            // TabSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 442);
            this.Controls.Add(this.MainSplit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "TabSettings";
            this.Text = "タブの設定";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TabSettings_FormClosing);
            this.MainSplit.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainSplit)).EndInit();
            this.MainSplit.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer MainSplit;
        private System.Windows.Forms.TreeView tabTreeView;
        private System.Windows.Forms.Button delTabsButton;
        private System.Windows.Forms.Button addTabsButton;
    }
}