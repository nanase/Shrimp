namespace Shrimp.Setting.Forms
{
    partial class FontForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.NameFontButton = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.TweetFontButton = new System.Windows.Forms.Button();
            this.ViaFontButton = new System.Windows.Forms.Button();
            this.RetweetNotifyButton = new System.Windows.Forms.Button();
            this.PreviewBox = new System.Windows.Forms.PictureBox();
            this.fontSelectDialog = new System.Windows.Forms.FontDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PreviewBox)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RetweetNotifyButton);
            this.groupBox1.Controls.Add(this.ViaFontButton);
            this.groupBox1.Controls.Add(this.TweetFontButton);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.NameFontButton);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(465, 452);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "フォントの選択";
            // 
            // NameFontButton
            // 
            this.NameFontButton.Location = new System.Drawing.Point(11, 27);
            this.NameFontButton.Name = "NameFontButton";
            this.NameFontButton.Size = new System.Drawing.Size(75, 23);
            this.NameFontButton.TabIndex = 1;
            this.NameFontButton.Text = "名前";
            this.NameFontButton.UseVisualStyleBackColor = true;
            this.NameFontButton.Click += new System.EventHandler(this.FontSelect);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.PreviewBox);
            this.groupBox4.Location = new System.Drawing.Point(8, 71);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(451, 122);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "プレビュー";
            // 
            // TweetFontButton
            // 
            this.TweetFontButton.Location = new System.Drawing.Point(108, 27);
            this.TweetFontButton.Name = "TweetFontButton";
            this.TweetFontButton.Size = new System.Drawing.Size(75, 23);
            this.TweetFontButton.TabIndex = 11;
            this.TweetFontButton.Text = "ツイート";
            this.TweetFontButton.UseVisualStyleBackColor = true;
            this.TweetFontButton.Click += new System.EventHandler(this.FontSelect);
            // 
            // ViaFontButton
            // 
            this.ViaFontButton.Location = new System.Drawing.Point(205, 27);
            this.ViaFontButton.Name = "ViaFontButton";
            this.ViaFontButton.Size = new System.Drawing.Size(75, 23);
            this.ViaFontButton.TabIndex = 12;
            this.ViaFontButton.Text = "Via";
            this.ViaFontButton.UseVisualStyleBackColor = true;
            this.ViaFontButton.Click += new System.EventHandler(this.FontSelect);
            // 
            // RetweetNotifyButton
            // 
            this.RetweetNotifyButton.Location = new System.Drawing.Point(305, 27);
            this.RetweetNotifyButton.Name = "RetweetNotifyButton";
            this.RetweetNotifyButton.Size = new System.Drawing.Size(75, 23);
            this.RetweetNotifyButton.TabIndex = 13;
            this.RetweetNotifyButton.Text = "リツイート";
            this.RetweetNotifyButton.UseVisualStyleBackColor = true;
            this.RetweetNotifyButton.Click += new System.EventHandler(this.FontSelect);
            // 
            // PreviewBox
            // 
            this.PreviewBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PreviewBox.Location = new System.Drawing.Point(3, 15);
            this.PreviewBox.Name = "PreviewBox";
            this.PreviewBox.Size = new System.Drawing.Size(445, 104);
            this.PreviewBox.TabIndex = 0;
            this.PreviewBox.TabStop = false;
            // 
            // FontForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "FontForm";
            this.Size = new System.Drawing.Size(465, 452);
            this.groupBox1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PreviewBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button NameFontButton;
        private System.Windows.Forms.Button RetweetNotifyButton;
        private System.Windows.Forms.Button ViaFontButton;
        private System.Windows.Forms.Button TweetFontButton;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.PictureBox PreviewBox;
        private System.Windows.Forms.FontDialog fontSelectDialog;
    }
}
