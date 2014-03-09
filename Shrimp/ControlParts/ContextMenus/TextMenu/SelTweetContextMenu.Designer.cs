namespace Shrimp.ControlParts.ContextMenus.TextMenu
{
    partial class SelTweetContextMenu
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
            this.ReplyMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ReplyDMMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.FavMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.RetweetMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.RegistBookmarkMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteTweetMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.AboutThisUserMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.RetweetedByUserMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu.SuspendLayout();
            // 
            // Menu
            // 
            this.Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ReplyMenu,
            this.ReplyDMMenu,
            this.toolStripSeparator1,
            this.FavMenu,
            this.RetweetMenu,
            this.RegistBookmarkMenu,
            this.DeleteTweetMenu,
            this.toolStripSeparator2,
            this.AboutThisUserMenu,
            this.RetweetedByUserMenu});
            this.Menu.Name = "Menu";
            this.Menu.Size = new System.Drawing.Size(257, 192);
            this.Menu.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.Menu_Closed);
            this.Menu.Opening += new System.ComponentModel.CancelEventHandler(this.Menu_Opening);
            this.Menu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.Menu_ItemClicked);
            // 
            // ReplyMenu
            // 
            this.ReplyMenu.Name = "ReplyMenu";
            this.ReplyMenu.Size = new System.Drawing.Size(256, 22);
            this.ReplyMenu.Text = "リプライ(&R)";
            this.ReplyMenu.ToolTipText = "このツイートに対してリプライを返します";
            // 
            // ReplyDMMenu
            // 
            this.ReplyDMMenu.Name = "ReplyDMMenu";
            this.ReplyDMMenu.Size = new System.Drawing.Size(256, 22);
            this.ReplyDMMenu.Text = "DM返信(&A)";
            this.ReplyDMMenu.ToolTipText = "このユーザに対してDMを送信します";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(253, 6);
            // 
            // FavMenu
            // 
            this.FavMenu.Name = "FavMenu";
            this.FavMenu.Size = new System.Drawing.Size(256, 22);
            this.FavMenu.Text = "お気に入りに登録(&F)";
            this.FavMenu.ToolTipText = "このツイートをお気に入り登録します";
            // 
            // RetweetMenu
            // 
            this.RetweetMenu.Name = "RetweetMenu";
            this.RetweetMenu.Size = new System.Drawing.Size(256, 22);
            this.RetweetMenu.Text = "リツイート(&T)";
            this.RetweetMenu.ToolTipText = "このツイートをリツイートします";
            // 
            // RegistBookmarkMenu
            // 
            this.RegistBookmarkMenu.Name = "RegistBookmarkMenu";
            this.RegistBookmarkMenu.Size = new System.Drawing.Size(256, 22);
            this.RegistBookmarkMenu.Text = "ブックマークに追加(&B)";
            this.RegistBookmarkMenu.ToolTipText = "このツイートをブックマークに登録します";
            // 
            // DeleteTweetMenu
            // 
            this.DeleteTweetMenu.Name = "DeleteTweetMenu";
            this.DeleteTweetMenu.Size = new System.Drawing.Size(256, 22);
            this.DeleteTweetMenu.Text = "ツイートを削除する(&D)";
            this.DeleteTweetMenu.ToolTipText = "このツイートを削除します";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(253, 6);
            // 
            // AboutThisUserMenu
            // 
            this.AboutThisUserMenu.Name = "AboutThisUserMenu";
            this.AboutThisUserMenu.Size = new System.Drawing.Size(256, 22);
            this.AboutThisUserMenu.Text = "このユーザーについて";
            // 
            // RetweetedByUserMenu
            // 
            this.RetweetedByUserMenu.Name = "RetweetedByUserMenu";
            this.RetweetedByUserMenu.Size = new System.Drawing.Size(256, 22);
            this.RetweetedByUserMenu.Text = "リツイートしたユーザについて";
            this.Menu.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip Menu;
        private System.Windows.Forms.ToolStripMenuItem ReplyMenu;
        private System.Windows.Forms.ToolStripMenuItem FavMenu;
        private System.Windows.Forms.ToolStripMenuItem RetweetMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem DeleteTweetMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem AboutThisUserMenu;
        private System.Windows.Forms.ToolStripMenuItem RegistBookmarkMenu;
        private System.Windows.Forms.ToolStripMenuItem RetweetedByUserMenu;
        private System.Windows.Forms.ToolStripMenuItem ReplyDMMenu;
    }
}
