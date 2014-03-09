namespace Shrimp.ControlParts.ContextMenus.TextMenu
{
    partial class SelTextContextMenu
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose ( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose ();
            }
            base.Dispose ( disposing );
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent ()
        {
            this.components = new System.ComponentModel.Container();
            this.Menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CopyMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.SelectAllMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.GoogleSearchMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.TwitterSearchMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu.SuspendLayout();
            // 
            // Menu
            // 
            this.Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CopyMenu,
            this.toolStripSeparator1,
            this.SelectAllMenu,
            this.toolStripSeparator2,
            this.GoogleSearchMenu,
            this.TwitterSearchMenu});
            this.Menu.Name = "contextMenuStrip1";
            this.Menu.Size = new System.Drawing.Size(192, 98);
            this.Menu.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.Menu_Closed);
            this.Menu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.Menu_ItemClicked);
            // 
            // CopyMenu
            // 
            this.CopyMenu.Name = "CopyMenu";
            this.CopyMenu.Size = new System.Drawing.Size(191, 22);
            this.CopyMenu.Text = "コピー(&C)";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(188, 6);
            // 
            // SelectAllMenu
            // 
            this.SelectAllMenu.Name = "SelectAllMenu";
            this.SelectAllMenu.Size = new System.Drawing.Size(191, 22);
            this.SelectAllMenu.Text = "全選択(&A)";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 6);
            // 
            // GoogleSearchMenu
            // 
            this.GoogleSearchMenu.Name = "GoogleSearchMenu";
            this.GoogleSearchMenu.Size = new System.Drawing.Size(191, 22);
            this.GoogleSearchMenu.Text = "グーグルで検索する";
            // 
            // TwitterSearchMenu
            // 
            this.TwitterSearchMenu.Name = "TwitterSearchMenu";
            this.TwitterSearchMenu.Size = new System.Drawing.Size(191, 22);
            this.TwitterSearchMenu.Text = "Twitterで検索する";
            this.Menu.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip Menu;
        private System.Windows.Forms.ToolStripMenuItem CopyMenu;
        private System.Windows.Forms.ToolStripMenuItem SelectAllMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem GoogleSearchMenu;
        private System.Windows.Forms.ToolStripMenuItem TwitterSearchMenu;
    }
}
