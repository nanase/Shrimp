namespace Shrimp.ControlParts.TweetBox
{
    partial class TweetBoxControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TweetBoxControl));
			this.TweetBoxSplit = new System.Windows.Forms.SplitContainer();
			this.TweetSplit = new System.Windows.Forms.SplitContainer();
			this.tweetDeleteBox = new System.Windows.Forms.Label();
			this.TweetBox = new ControlParts.TweetBox.TextBoxAC();
			this.AccountSelectedImage = new System.Windows.Forms.PictureBox();
			this.shrimpButton = new System.Windows.Forms.Button();
			this.tweetCountLabel = new System.Windows.Forms.Label();
			this.DraftButton = new System.Windows.Forms.Button();
			this.TweetSendButton = new System.Windows.Forms.Button();
			this.HashtagButton = new System.Windows.Forms.Button();
			this.PictureBox = new System.Windows.Forms.Button();
			this.replyButton = new System.Windows.Forms.Button();
			this.MainSplit = new System.Windows.Forms.SplitContainer();
			((System.ComponentModel.ISupportInitialize)(this.TweetBoxSplit)).BeginInit();
			this.TweetBoxSplit.Panel1.SuspendLayout();
			this.TweetBoxSplit.Panel2.SuspendLayout();
			this.TweetBoxSplit.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.TweetSplit)).BeginInit();
			this.TweetSplit.Panel1.SuspendLayout();
			this.TweetSplit.Panel2.SuspendLayout();
			this.TweetSplit.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.AccountSelectedImage)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.MainSplit)).BeginInit();
			this.MainSplit.Panel2.SuspendLayout();
			this.MainSplit.SuspendLayout();
			this.SuspendLayout();
			// 
			// TweetBoxSplit
			// 
			this.TweetBoxSplit.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TweetBoxSplit.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.TweetBoxSplit.IsSplitterFixed = true;
			this.TweetBoxSplit.Location = new System.Drawing.Point(0, 0);
			this.TweetBoxSplit.Name = "TweetBoxSplit";
			this.TweetBoxSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// TweetBoxSplit.Panel1
			// 
			this.TweetBoxSplit.Panel1.Controls.Add(this.TweetSplit);
			// 
			// TweetBoxSplit.Panel2
			// 
			this.TweetBoxSplit.Panel2.Controls.Add(this.shrimpButton);
			this.TweetBoxSplit.Panel2.Controls.Add(this.tweetCountLabel);
			this.TweetBoxSplit.Panel2.Controls.Add(this.DraftButton);
			this.TweetBoxSplit.Panel2.Controls.Add(this.TweetSendButton);
			this.TweetBoxSplit.Panel2.Controls.Add(this.HashtagButton);
			this.TweetBoxSplit.Panel2.Controls.Add(this.PictureBox);
			this.TweetBoxSplit.Panel2.Controls.Add(this.replyButton);
			this.TweetBoxSplit.Panel2MinSize = 32;
			this.TweetBoxSplit.Size = new System.Drawing.Size(422, 100);
			this.TweetBoxSplit.SplitterDistance = 64;
			this.TweetBoxSplit.TabIndex = 0;
			// 
			// TweetSplit
			// 
			this.TweetSplit.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TweetSplit.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.TweetSplit.IsSplitterFixed = true;
			this.TweetSplit.Location = new System.Drawing.Point(0, 0);
			this.TweetSplit.Name = "TweetSplit";
			// 
			// TweetSplit.Panel1
			// 
			this.TweetSplit.Panel1.Controls.Add(this.tweetDeleteBox);
			this.TweetSplit.Panel1.Controls.Add(this.TweetBox);
			// 
			// TweetSplit.Panel2
			// 
			this.TweetSplit.Panel2.Controls.Add(this.AccountSelectedImage);
			this.TweetSplit.Panel2MinSize = 48;
			this.TweetSplit.Size = new System.Drawing.Size(422, 64);
			this.TweetSplit.SplitterDistance = 354;
			this.TweetSplit.TabIndex = 0;
			// 
			// tweetDeleteBox
			// 
			this.tweetDeleteBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.tweetDeleteBox.AutoSize = true;
			this.tweetDeleteBox.BackColor = System.Drawing.Color.Transparent;
			this.tweetDeleteBox.Location = new System.Drawing.Point(339, 3);
			this.tweetDeleteBox.Name = "tweetDeleteBox";
			this.tweetDeleteBox.Size = new System.Drawing.Size(12, 12);
			this.tweetDeleteBox.TabIndex = 1;
			this.tweetDeleteBox.Text = "X";
			this.tweetDeleteBox.Click += new System.EventHandler(this.tweetDeleteBox_Click);
			// 
			// TweetBox
			// 
			this.TweetBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TweetBox.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.TweetBox.listShow = false;
			this.TweetBox.Location = new System.Drawing.Point(0, 0);
			this.TweetBox.Multiline = true;
			this.TweetBox.Name = "TweetBox";
			this.TweetBox.ShortcutsEnabled = false;
			this.TweetBox.Size = new System.Drawing.Size(354, 64);
			this.TweetBox.TabIndex = 2;
			this.TweetBox.TextChanged += new System.EventHandler(this.TweetBox_TextChanged);
			this.TweetBox.Enter += new System.EventHandler(this.TweetBox_Enter);
			this.TweetBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TweetBox_KeyDown);
			this.TweetBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TweetBox_KeyDown);
			this.TweetBox.Leave += new System.EventHandler(this.TweetBox_Leave);
			// 
			// AccountSelectedImage
			// 
			this.AccountSelectedImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.AccountSelectedImage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.AccountSelectedImage.Location = new System.Drawing.Point(0, 0);
			this.AccountSelectedImage.Name = "AccountSelectedImage";
			this.AccountSelectedImage.Size = new System.Drawing.Size(64, 64);
			this.AccountSelectedImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.AccountSelectedImage.TabIndex = 0;
			this.AccountSelectedImage.TabStop = false;
			this.AccountSelectedImage.DoubleClick += new System.EventHandler(this.AccountSelectedImage_DoubleClick);
			// 
			// shrimpButton
			// 
			this.shrimpButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("shrimpButton.BackgroundImage")));
			this.shrimpButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.shrimpButton.Dock = System.Windows.Forms.DockStyle.Left;
			this.shrimpButton.FlatAppearance.BorderSize = 0;
			this.shrimpButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.shrimpButton.Location = new System.Drawing.Point(128, 0);
			this.shrimpButton.Name = "shrimpButton";
			this.shrimpButton.Size = new System.Drawing.Size(32, 32);
			this.shrimpButton.TabIndex = 5;
			this.shrimpButton.UseVisualStyleBackColor = true;
			this.shrimpButton.Click += new System.EventHandler(this.shrimpButton_Click);
			// 
			// tweetCountLabel
			// 
			this.tweetCountLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.tweetCountLabel.AutoSize = true;
			this.tweetCountLabel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.tweetCountLabel.Location = new System.Drawing.Point(287, -1);
			this.tweetCountLabel.Name = "tweetCountLabel";
			this.tweetCountLabel.Size = new System.Drawing.Size(32, 16);
			this.tweetCountLabel.TabIndex = 4;
			this.tweetCountLabel.Text = "140";
			// 
			// DraftButton
			// 
			this.DraftButton.BackgroundImage = global::Shrimp.Properties.Resources.draft_16;
			this.DraftButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.DraftButton.Dock = System.Windows.Forms.DockStyle.Left;
			this.DraftButton.FlatAppearance.BorderSize = 0;
			this.DraftButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.DraftButton.Location = new System.Drawing.Point(96, 0);
			this.DraftButton.Name = "DraftButton";
			this.DraftButton.Size = new System.Drawing.Size(32, 32);
			this.DraftButton.TabIndex = 3;
			this.DraftButton.UseVisualStyleBackColor = true;
			this.DraftButton.Click += new System.EventHandler(this.NotImplementedButtonClicked);
			// 
			// TweetSendButton
			// 
			this.TweetSendButton.Dock = System.Windows.Forms.DockStyle.Right;
			this.TweetSendButton.Location = new System.Drawing.Point(341, 0);
			this.TweetSendButton.Name = "TweetSendButton";
			this.TweetSendButton.Size = new System.Drawing.Size(81, 32);
			this.TweetSendButton.TabIndex = 1;
			this.TweetSendButton.Text = "ツイート送信";
			this.TweetSendButton.UseVisualStyleBackColor = true;
			this.TweetSendButton.Click += new System.EventHandler(this.SendButton_Click);
			// 
			// HashtagButton
			// 
			this.HashtagButton.BackgroundImage = global::Shrimp.Properties.Resources.hash_16;
			this.HashtagButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.HashtagButton.Dock = System.Windows.Forms.DockStyle.Left;
			this.HashtagButton.FlatAppearance.BorderSize = 0;
			this.HashtagButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.HashtagButton.Location = new System.Drawing.Point(64, 0);
			this.HashtagButton.Name = "HashtagButton";
			this.HashtagButton.Size = new System.Drawing.Size(32, 32);
			this.HashtagButton.TabIndex = 2;
			this.HashtagButton.UseVisualStyleBackColor = true;
			this.HashtagButton.Click += new System.EventHandler(this.HashtagButton_Click);
			// 
			// PictureBox
			// 
			this.PictureBox.BackColor = System.Drawing.SystemColors.Control;
			this.PictureBox.BackgroundImage = global::Shrimp.Properties.Resources.image_16;
			this.PictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.PictureBox.Dock = System.Windows.Forms.DockStyle.Left;
			this.PictureBox.FlatAppearance.BorderSize = 0;
			this.PictureBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.PictureBox.ForeColor = System.Drawing.SystemColors.Control;
			this.PictureBox.Location = new System.Drawing.Point(32, 0);
			this.PictureBox.Name = "PictureBox";
			this.PictureBox.Size = new System.Drawing.Size(32, 32);
			this.PictureBox.TabIndex = 1;
			this.PictureBox.UseVisualStyleBackColor = false;
			this.PictureBox.Click += new System.EventHandler(this.PictureBox_Click);
			// 
			// replyButton
			// 
			this.replyButton.BackgroundImage = global::Shrimp.Properties.Resources.atmark_16;
			this.replyButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.replyButton.Dock = System.Windows.Forms.DockStyle.Left;
			this.replyButton.FlatAppearance.BorderSize = 0;
			this.replyButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.replyButton.Location = new System.Drawing.Point(0, 0);
			this.replyButton.Name = "replyButton";
			this.replyButton.Size = new System.Drawing.Size(32, 32);
			this.replyButton.TabIndex = 0;
			this.replyButton.UseVisualStyleBackColor = true;
			this.replyButton.Click += new System.EventHandler(this.replyButton_Click);
			// 
			// MainSplit
			// 
			this.MainSplit.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MainSplit.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.MainSplit.Location = new System.Drawing.Point(0, 0);
			this.MainSplit.Name = "MainSplit";
			this.MainSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
			this.MainSplit.Panel1Collapsed = true;
			// 
			// MainSplit.Panel2
			// 
			this.MainSplit.Panel2.Controls.Add(this.TweetBoxSplit);
			this.MainSplit.Size = new System.Drawing.Size(422, 100);
			this.MainSplit.SplitterDistance = 25;
			this.MainSplit.TabIndex = 1;
			// 
			// TweetBoxControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.MainSplit);
			this.Name = "TweetBoxControl";
			this.Size = new System.Drawing.Size(422, 100);
			this.TweetBoxSplit.Panel1.ResumeLayout(false);
			this.TweetBoxSplit.Panel2.ResumeLayout(false);
			this.TweetBoxSplit.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.TweetBoxSplit)).EndInit();
			this.TweetBoxSplit.ResumeLayout(false);
			this.TweetSplit.Panel1.ResumeLayout(false);
			this.TweetSplit.Panel1.PerformLayout();
			this.TweetSplit.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.TweetSplit)).EndInit();
			this.TweetSplit.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.AccountSelectedImage)).EndInit();
			this.MainSplit.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.MainSplit)).EndInit();
			this.MainSplit.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer TweetBoxSplit;
        private System.Windows.Forms.SplitContainer TweetSplit;
        private System.Windows.Forms.Button HashtagButton;
        private System.Windows.Forms.Button PictureBox;
        private System.Windows.Forms.Button replyButton;
        private System.Windows.Forms.Button TweetSendButton;
        private System.Windows.Forms.PictureBox AccountSelectedImage;
        private System.Windows.Forms.Button DraftButton;
        private System.Windows.Forms.SplitContainer MainSplit;
        private System.Windows.Forms.Label tweetCountLabel;
        private System.Windows.Forms.Label tweetDeleteBox;
        private System.Windows.Forms.Button shrimpButton;
        private TextBoxAC TweetBox;

    }
}
