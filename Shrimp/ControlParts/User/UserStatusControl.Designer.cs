namespace Shrimp.ControlParts.User
{
    partial class UserStatusControl
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
            this.MainContainer = new System.Windows.Forms.SplitContainer();
            this.UserInfoControl = new System.Windows.Forms.TabControl();
            this.UserPage = new System.Windows.Forms.TabPage();
            this.UserTimelinePage = new System.Windows.Forms.TabPage();
            this.UserFavoritePage = new System.Windows.Forms.TabPage();
            this.ConversationPage = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.MainContainer)).BeginInit();
            this.MainContainer.Panel2.SuspendLayout();
            this.MainContainer.SuspendLayout();
            this.UserInfoControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainContainer
            // 
            this.MainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainContainer.Location = new System.Drawing.Point(0, 0);
            this.MainContainer.Name = "MainContainer";
            this.MainContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // MainContainer.Panel2
            // 
            this.MainContainer.Panel2.Controls.Add(this.UserInfoControl);
            this.MainContainer.Size = new System.Drawing.Size(285, 412);
            this.MainContainer.SplitterDistance = 152;
            this.MainContainer.TabIndex = 0;
            // 
            // UserInfoControl
            // 
            this.UserInfoControl.Controls.Add(this.UserPage);
            this.UserInfoControl.Controls.Add(this.UserTimelinePage);
            this.UserInfoControl.Controls.Add(this.UserFavoritePage);
            this.UserInfoControl.Controls.Add(this.ConversationPage);
            this.UserInfoControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UserInfoControl.Location = new System.Drawing.Point(0, 0);
            this.UserInfoControl.Name = "UserInfoControl";
            this.UserInfoControl.SelectedIndex = 0;
            this.UserInfoControl.Size = new System.Drawing.Size(285, 256);
            this.UserInfoControl.TabIndex = 0;
            this.UserInfoControl.SelectedIndexChanged += new System.EventHandler(this.UserInfoControl_SelectedIndexChanged);
            // 
            // UserPage
            // 
            this.UserPage.Location = new System.Drawing.Point(4, 22);
            this.UserPage.Name = "UserPage";
            this.UserPage.Size = new System.Drawing.Size(277, 230);
            this.UserPage.TabIndex = 0;
            this.UserPage.Text = "発言";
            this.UserPage.UseVisualStyleBackColor = true;
            // 
            // UserTimelinePage
            // 
            this.UserTimelinePage.Location = new System.Drawing.Point(4, 22);
            this.UserTimelinePage.Name = "UserTimelinePage";
            this.UserTimelinePage.Size = new System.Drawing.Size(277, 230);
            this.UserTimelinePage.TabIndex = 1;
            this.UserTimelinePage.Text = "タイムライン";
            this.UserTimelinePage.UseVisualStyleBackColor = true;
            // 
            // UserFavoritePage
            // 
            this.UserFavoritePage.Location = new System.Drawing.Point(4, 22);
            this.UserFavoritePage.Name = "UserFavoritePage";
            this.UserFavoritePage.Size = new System.Drawing.Size(277, 230);
            this.UserFavoritePage.TabIndex = 2;
            this.UserFavoritePage.Text = "お気に入り";
            this.UserFavoritePage.UseVisualStyleBackColor = true;
            // 
            // ConversationPage
            // 
            this.ConversationPage.Location = new System.Drawing.Point(4, 22);
            this.ConversationPage.Name = "ConversationPage";
            this.ConversationPage.Size = new System.Drawing.Size(277, 230);
            this.ConversationPage.TabIndex = 3;
            this.ConversationPage.Text = "会話";
            this.ConversationPage.UseVisualStyleBackColor = true;
            // 
            // UserStatusControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MainContainer);
            this.Name = "UserStatusControl";
            this.Size = new System.Drawing.Size(285, 412);
            this.MainContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainContainer)).EndInit();
            this.MainContainer.ResumeLayout(false);
            this.UserInfoControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer MainContainer;
        private System.Windows.Forms.TabControl UserInfoControl;
        private System.Windows.Forms.TabPage UserPage;
        private System.Windows.Forms.TabPage UserTimelinePage;
        private System.Windows.Forms.TabPage UserFavoritePage;
        private System.Windows.Forms.TabPage ConversationPage;
    }
}
