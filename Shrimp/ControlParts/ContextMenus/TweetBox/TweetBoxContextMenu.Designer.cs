namespace Shrimp.ControlParts.ContextMenus.TweetBox
{
    partial class TweetBoxContextMenu
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
            this.RestoreMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.CutMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.PasteMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.SelectAllMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.PasteImageMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu.SuspendLayout();
            // 
            // Menu
            // 
            this.Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RestoreMenu,
            this.toolStripSeparator1,
            this.CutMenu,
            this.CopyMenu,
            this.PasteMenu,
            this.PasteImageMenu,
            this.DeleteMenu,
            this.toolStripSeparator2,
            this.SelectAllMenu});
            this.Menu.Name = "Menu";
            this.Menu.Size = new System.Drawing.Size(297, 170);
            // 
            // RestoreMenu
            // 
            this.RestoreMenu.Name = "RestoreMenu";
            this.RestoreMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.RestoreMenu.ShowShortcutKeys = false;
            this.RestoreMenu.Size = new System.Drawing.Size(296, 22);
            this.RestoreMenu.Text = "元に戻す(&U)";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(293, 6);
            // 
            // CutMenu
            // 
            this.CutMenu.Name = "CutMenu";
            this.CutMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.CutMenu.ShowShortcutKeys = false;
            this.CutMenu.Size = new System.Drawing.Size(296, 22);
            this.CutMenu.Text = "切り取り(&T)";
            // 
            // CopyMenu
            // 
            this.CopyMenu.Name = "CopyMenu";
            this.CopyMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.CopyMenu.ShowShortcutKeys = false;
            this.CopyMenu.Size = new System.Drawing.Size(296, 22);
            this.CopyMenu.Text = "コピー(&C)";
            // 
            // PasteMenu
            // 
            this.PasteMenu.Name = "PasteMenu";
            this.PasteMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.PasteMenu.ShowShortcutKeys = false;
            this.PasteMenu.Size = new System.Drawing.Size(296, 22);
            this.PasteMenu.Text = "貼り付け(&P)";
            // 
            // DeleteMenu
            // 
            this.DeleteMenu.Name = "DeleteMenu";
            this.DeleteMenu.ShowShortcutKeys = false;
            this.DeleteMenu.Size = new System.Drawing.Size(296, 22);
            this.DeleteMenu.Text = "削除(&U)";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(293, 6);
            // 
            // SelectAllMenu
            // 
            this.SelectAllMenu.Name = "SelectAllMenu";
            this.SelectAllMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.SelectAllMenu.ShowShortcutKeys = false;
            this.SelectAllMenu.Size = new System.Drawing.Size(296, 22);
            this.SelectAllMenu.Text = "すべて選択(&A)";
            // 
            // PasteImageMenu
            // 
            this.PasteImageMenu.Name = "PasteImageMenu";
            this.PasteImageMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.PasteImageMenu.ShowShortcutKeys = false;
            this.PasteImageMenu.Size = new System.Drawing.Size(296, 22);
            this.PasteImageMenu.Text = "クリップボードの画像を貼り付ける(&B)";
            this.Menu.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip Menu;
        private System.Windows.Forms.ToolStripMenuItem RestoreMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem CutMenu;
        private System.Windows.Forms.ToolStripMenuItem CopyMenu;
        private System.Windows.Forms.ToolStripMenuItem PasteMenu;
        private System.Windows.Forms.ToolStripMenuItem SelectAllMenu;
        private System.Windows.Forms.ToolStripMenuItem DeleteMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem PasteImageMenu;
    }
}
