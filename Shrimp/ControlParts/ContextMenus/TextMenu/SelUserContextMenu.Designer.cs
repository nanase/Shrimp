namespace Shrimp.ControlParts.ContextMenus.TextMenu
{
    partial class SelUserContextMenu
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
            this.UserMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.OpenUserInformationTabMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenUserTimelineTabMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenUserFavTimelineTabMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenReplyToUserTabMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenConversationTabMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.FollowMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.BlockMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ReportSpamMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.UserMenu.SuspendLayout();
            // 
            // UserMenu
            // 
            this.UserMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenUserInformationTabMenu,
            this.OpenUserTimelineTabMenu,
            this.OpenUserFavTimelineTabMenu,
            this.OpenReplyToUserTabMenu,
            this.OpenConversationTabMenu,
            this.toolStripSeparator4,
            this.FollowMenu,
            this.BlockMenu,
            this.ReportSpamMenu});
            this.UserMenu.Name = "Menu";
            this.UserMenu.Size = new System.Drawing.Size(330, 186);
            this.UserMenu.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.Menu_Closed);
            this.UserMenu.Opening += new System.ComponentModel.CancelEventHandler(this.Menu_Opening);
            this.UserMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.UserMenu_ItemClicked);
            // 
            // OpenUserInformationTabMenu
            // 
            this.OpenUserInformationTabMenu.Name = "OpenUserInformationTabMenu";
            this.OpenUserInformationTabMenu.Size = new System.Drawing.Size(329, 22);
            this.OpenUserInformationTabMenu.Text = "ユーザ情報を開く(&U)";
            this.OpenUserInformationTabMenu.ToolTipText = "ユーザ情報を、新しくタブを開いて表示します";
            // 
            // OpenUserTimelineTabMenu
            // 
            this.OpenUserTimelineTabMenu.Name = "OpenUserTimelineTabMenu";
            this.OpenUserTimelineTabMenu.Size = new System.Drawing.Size(329, 22);
            this.OpenUserTimelineTabMenu.Text = "ユーザのタイムラインを開く(&T)";
            this.OpenUserTimelineTabMenu.ToolTipText = "ユーザのタイムラインを、新しくタブを開いて表示します";
            // 
            // OpenUserFavTimelineTabMenu
            // 
            this.OpenUserFavTimelineTabMenu.Name = "OpenUserFavTimelineTabMenu";
            this.OpenUserFavTimelineTabMenu.Size = new System.Drawing.Size(329, 22);
            this.OpenUserFavTimelineTabMenu.Text = "ユーザのお気に入りタイムラインを開く(&E)";
            this.OpenUserFavTimelineTabMenu.ToolTipText = "ユーザのお気に入りタイムラインを、新しくタブを開いて表示します";
            // 
            // OpenReplyToUserTabMenu
            // 
            this.OpenReplyToUserTabMenu.Name = "OpenReplyToUserTabMenu";
            this.OpenReplyToUserTabMenu.Size = new System.Drawing.Size(329, 22);
            this.OpenReplyToUserTabMenu.Text = "このユーザへのリプライを開く(&R)";
            this.OpenReplyToUserTabMenu.ToolTipText = "ユーザへのリプライを新しくタブを開いて表示します";
            // 
            // OpenConversationTabMenu
            // 
            this.OpenConversationTabMenu.Name = "OpenConversationTabMenu";
            this.OpenConversationTabMenu.Size = new System.Drawing.Size(329, 22);
            this.OpenConversationTabMenu.Text = "このユーザの会話を開く(&C)";
            this.OpenConversationTabMenu.ToolTipText = "ユーザのツイートとそれへのリプライを、新しくタブを開いて表示します";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(326, 6);
            // 
            // FollowMenu
            // 
            this.FollowMenu.Name = "FollowMenu";
            this.FollowMenu.Size = new System.Drawing.Size(329, 22);
            this.FollowMenu.Text = "フォローする(&F)";
            this.FollowMenu.ToolTipText = "ユーザをフォローします";
            // 
            // BlockMenu
            // 
            this.BlockMenu.Name = "BlockMenu";
            this.BlockMenu.Size = new System.Drawing.Size(329, 22);
            this.BlockMenu.Text = "ブロックする(&B)";
            this.BlockMenu.ToolTipText = "ユーザをブロックします";
            // 
            // ReportSpamMenu
            // 
            this.ReportSpamMenu.Name = "ReportSpamMenu";
            this.ReportSpamMenu.Size = new System.Drawing.Size(329, 22);
            this.ReportSpamMenu.Text = "スパム報告する(&S)";
            this.ReportSpamMenu.ToolTipText = "ユーザをスパム報告します";
            this.UserMenu.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip UserMenu;
        private System.Windows.Forms.ToolStripMenuItem OpenUserInformationTabMenu;
        private System.Windows.Forms.ToolStripMenuItem OpenUserTimelineTabMenu;
        private System.Windows.Forms.ToolStripMenuItem OpenUserFavTimelineTabMenu;
        private System.Windows.Forms.ToolStripMenuItem OpenReplyToUserTabMenu;
        private System.Windows.Forms.ToolStripMenuItem OpenConversationTabMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem FollowMenu;
        private System.Windows.Forms.ToolStripMenuItem BlockMenu;
        private System.Windows.Forms.ToolStripMenuItem ReportSpamMenu;
    }
}
