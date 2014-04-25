namespace Shrimp.ControlParts.Popup
{
    partial class StatusPopup
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
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.ConnectUserStreamNenu = new System.Windows.Forms.ToolStripMenuItem();
			this.LineTweetModeMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.UserInformationMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.OpenSettingMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.Menu.SuspendLayout();
			// 
			// Menu
			// 
			this.Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.ConnectUserStreamNenu,
            this.LineTweetModeMenu,
            this.UserInformationMenu,
            this.OpenSettingMenu});
			this.Menu.Name = "Menu";
			this.Menu.Size = new System.Drawing.Size(218, 98);
			this.Menu.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.Menu_Closed);
			this.Menu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.Menu_ItemClicked);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(248, 6);
			// 
			// ConnectUserStreamNenu
			// 
			this.ConnectUserStreamNenu.AutoToolTip = true;
			this.ConnectUserStreamNenu.Name = "ConnectUserStreamNenu";
			this.ConnectUserStreamNenu.Size = new System.Drawing.Size(251, 22);
			this.ConnectUserStreamNenu.Text = "UserStreamへ接続する(&U)";
			this.ConnectUserStreamNenu.ToolTipText = "UserStreamへ接続します";
			// 
			// LineTweetModeMenu
			// 
			this.LineTweetModeMenu.AutoToolTip = true;
			this.LineTweetModeMenu.CheckOnClick = true;
			this.LineTweetModeMenu.Name = "LineTweetModeMenu";
			this.LineTweetModeMenu.Size = new System.Drawing.Size(251, 22);
			this.LineTweetModeMenu.Text = "単行表示モード(&L)";
			this.LineTweetModeMenu.ToolTipText = "タイムラインのツイートを単行表示にします。";
			// 
			// UserInformationMenu
			// 
			this.UserInformationMenu.Name = "UserInformationMenu";
			this.UserInformationMenu.Size = new System.Drawing.Size(251, 22);
			this.UserInformationMenu.Text = "ユーザー情報パネルを開く(&P)";
			// 
			// OpenSettingMenu
			// 
			this.OpenSettingMenu.Name = "OpenSettingMenu";
			this.OpenSettingMenu.ShowShortcutKeys = false;
			this.OpenSettingMenu.Size = new System.Drawing.Size(251, 22);
			this.OpenSettingMenu.Text = "設定を開く(&S)";
			this.Menu.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip Menu;
        private System.Windows.Forms.ToolStripMenuItem ConnectUserStreamNenu;
        private System.Windows.Forms.ToolStripMenuItem OpenSettingMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem LineTweetModeMenu;
        private System.Windows.Forms.ToolStripMenuItem UserInformationMenu;
    }
}
