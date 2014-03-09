namespace Shrimp.ControlParts.ContextMenus.Tabs
{
    partial class TabControlContextMenu
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
            this.AddNewTabMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.AddCustomTabMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.CustomMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.SelectedThisTabMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ChangeTabNameMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.DestroyThisTabMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.DestroyAllTabWithoutThisTabMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.FlashTabMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.TabSettingMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.SearchMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.LockTabMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu.SuspendLayout();
            this.CustomMenuStrip.SuspendLayout();
            // 
            // Menu
            // 
            this.Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddNewTabMenu,
            this.AddCustomTabMenu,
            this.toolStripSeparator1,
            this.SelectedThisTabMenu,
            this.ChangeTabNameMenu,
            this.LockTabMenu,
            this.DestroyThisTabMenu,
            this.DestroyAllTabWithoutThisTabMenu,
            this.toolStripSeparator3,
            this.FlashTabMenu,
            this.TabSettingMenu,
            this.toolStripSeparator2,
            this.SearchMenu});
            this.Menu.Name = "Menu";
            this.Menu.Size = new System.Drawing.Size(265, 242);
            // 
            // AddNewTabMenu
            // 
            this.AddNewTabMenu.Name = "AddNewTabMenu";
            this.AddNewTabMenu.ShortcutKeyDisplayString = "";
            this.AddNewTabMenu.Size = new System.Drawing.Size(264, 22);
            this.AddNewTabMenu.Text = "タブを新しく追加する(&A)";
            // 
            // AddCustomTabMenu
            // 
            this.AddCustomTabMenu.DropDown = this.CustomMenuStrip;
            this.AddCustomTabMenu.Name = "AddCustomTabMenu";
            this.AddCustomTabMenu.Size = new System.Drawing.Size(264, 22);
            this.AddCustomTabMenu.Text = "カスタムタブの作成(&C)";
            // 
            // CustomMenuStrip
            // 
            this.CustomMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.CustomMenuStrip.Name = "CustomMenuStrip";
            this.CustomMenuStrip.OwnerItem = this.AddCustomTabMenu;
            this.CustomMenuStrip.Size = new System.Drawing.Size(201, 26);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(200, 22);
            this.toolStripMenuItem1.Text = "toolStripMenuItem1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(261, 6);
            // 
            // SelectedThisTabMenu
            // 
            this.SelectedThisTabMenu.Name = "SelectedThisTabMenu";
            this.SelectedThisTabMenu.ShortcutKeyDisplayString = "";
            this.SelectedThisTabMenu.Size = new System.Drawing.Size(264, 22);
            this.SelectedThisTabMenu.Text = "このタブを選択する(&S)";
            // 
            // ChangeTabNameMenu
            // 
            this.ChangeTabNameMenu.Name = "ChangeTabNameMenu";
            this.ChangeTabNameMenu.Size = new System.Drawing.Size(264, 22);
            this.ChangeTabNameMenu.Text = "タブ名を変更する(&N)";
            // 
            // DestroyThisTabMenu
            // 
            this.DestroyThisTabMenu.Name = "DestroyThisTabMenu";
            this.DestroyThisTabMenu.Size = new System.Drawing.Size(264, 22);
            this.DestroyThisTabMenu.Text = "このタブを閉じる(&D)";
            // 
            // DestroyAllTabWithoutThisTabMenu
            // 
            this.DestroyAllTabWithoutThisTabMenu.Name = "DestroyAllTabWithoutThisTabMenu";
            this.DestroyAllTabWithoutThisTabMenu.Size = new System.Drawing.Size(264, 22);
            this.DestroyAllTabWithoutThisTabMenu.Text = "他のタブをすべて閉じる(&H)";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(261, 6);
            // 
            // FlashTabMenu
            // 
            this.FlashTabMenu.Name = "FlashTabMenu";
            this.FlashTabMenu.Size = new System.Drawing.Size(264, 22);
            this.FlashTabMenu.Text = "新着ツイート時、点滅させる(&F)";
            // 
            // TabSettingMenu
            // 
            this.TabSettingMenu.Name = "TabSettingMenu";
            this.TabSettingMenu.Size = new System.Drawing.Size(264, 22);
            this.TabSettingMenu.Text = "タブ設定を開く(&O)";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(261, 6);
            // 
            // SearchMenu
            // 
            this.SearchMenu.Name = "SearchMenu";
            this.SearchMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.SearchMenu.Size = new System.Drawing.Size(264, 22);
            this.SearchMenu.Text = "検索ボックスを開く(&T)";
            // 
            // LockTabMenu
            // 
            this.LockTabMenu.Name = "LockTabMenu";
            this.LockTabMenu.Size = new System.Drawing.Size(264, 22);
            this.LockTabMenu.Text = "タブロックする(&L)";
            this.Menu.ResumeLayout(false);
            this.CustomMenuStrip.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip Menu;
        private System.Windows.Forms.ToolStripMenuItem SelectedThisTabMenu;
        private System.Windows.Forms.ToolStripMenuItem DestroyThisTabMenu;
        private System.Windows.Forms.ToolStripMenuItem TabSettingMenu;
        private System.Windows.Forms.ToolStripMenuItem AddNewTabMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem AddCustomTabMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem SearchMenu;
        private System.Windows.Forms.ContextMenuStrip CustomMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ChangeTabNameMenu;
        private System.Windows.Forms.ToolStripMenuItem DestroyAllTabWithoutThisTabMenu;
        private System.Windows.Forms.ToolStripMenuItem FlashTabMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem LockTabMenu;
    }
}
