namespace Shrimp.Setting.Forms
{
    partial class TimelineManagement
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SavingTweetNumNumeric = new System.Windows.Forms.NumericUpDown();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.fTimelineSettingCheckBox = new System.Windows.Forms.CheckedListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.NotifyBackgroundAlphaNumeric = new System.Windows.Forms.NumericUpDown();
            this.NotifyStringColorButton = new System.Windows.Forms.Button();
            this.NotifyStringColorPictureBox = new System.Windows.Forms.PictureBox();
            this.NotifyBackgroundColorButton = new System.Windows.Forms.Button();
            this.NotifyBackgroundColorPictureBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.AnimationCheckBox = new System.Windows.Forms.CheckedListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.timelineSettingCheckBox = new System.Windows.Forms.CheckedListBox();
            this.selectColor = new System.Windows.Forms.ColorDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SavingTweetNumNumeric)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NotifyBackgroundAlphaNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NotifyStringColorPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NotifyBackgroundColorPictureBox)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(410, 413);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "タイムラインの設定";
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.SavingTweetNumNumeric);
            this.groupBox5.Location = new System.Drawing.Point(6, 209);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(398, 51);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "キャッシュ保持設定";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(170, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "タイムライン毎に保持するツイート数";
            // 
            // SavingTweetNumNumeric
            // 
            this.SavingTweetNumNumeric.Location = new System.Drawing.Point(6, 29);
            this.SavingTweetNumNumeric.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.SavingTweetNumNumeric.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.SavingTweetNumNumeric.Name = "SavingTweetNumNumeric";
            this.SavingTweetNumNumeric.Size = new System.Drawing.Size(63, 19);
            this.SavingTweetNumNumeric.TabIndex = 9;
            this.SavingTweetNumNumeric.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.SavingTweetNumNumeric.ValueChanged += new System.EventHandler(this.SavingTweetNumNumeric_ValueChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.fTimelineSettingCheckBox);
            this.groupBox4.Location = new System.Drawing.Point(3, 17);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(398, 62);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "全体設定";
            // 
            // fTimelineSettingCheckBox
            // 
            this.fTimelineSettingCheckBox.BackColor = System.Drawing.SystemColors.Control;
            this.fTimelineSettingCheckBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.fTimelineSettingCheckBox.CheckOnClick = true;
            this.fTimelineSettingCheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fTimelineSettingCheckBox.FormattingEnabled = true;
            this.fTimelineSettingCheckBox.Items.AddRange(new object[] {
            "リツイートする際、確認を行う",
            "お気に入りに登録する際、確認を行う",
            "ホバーセレクトモードを有効にする(単行モード時のみ有効)"});
            this.fTimelineSettingCheckBox.Location = new System.Drawing.Point(3, 15);
            this.fTimelineSettingCheckBox.Name = "fTimelineSettingCheckBox";
            this.fTimelineSettingCheckBox.Size = new System.Drawing.Size(392, 44);
            this.fTimelineSettingCheckBox.TabIndex = 1;
            this.fTimelineSettingCheckBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.fTimelineSettingCheckBox_ItemCheck);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.NotifyBackgroundAlphaNumeric);
            this.groupBox3.Controls.Add(this.NotifyStringColorButton);
            this.groupBox3.Controls.Add(this.NotifyStringColorPictureBox);
            this.groupBox3.Controls.Add(this.NotifyBackgroundColorButton);
            this.groupBox3.Controls.Add(this.NotifyBackgroundColorPictureBox);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.AnimationCheckBox);
            this.groupBox3.Location = new System.Drawing.Point(3, 266);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(398, 141);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "アニメーション";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "背景透過率";
            // 
            // NotifyBackgroundAlphaNumeric
            // 
            this.NotifyBackgroundAlphaNumeric.Location = new System.Drawing.Point(9, 113);
            this.NotifyBackgroundAlphaNumeric.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.NotifyBackgroundAlphaNumeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NotifyBackgroundAlphaNumeric.Name = "NotifyBackgroundAlphaNumeric";
            this.NotifyBackgroundAlphaNumeric.Size = new System.Drawing.Size(63, 19);
            this.NotifyBackgroundAlphaNumeric.TabIndex = 7;
            this.NotifyBackgroundAlphaNumeric.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NotifyBackgroundAlphaNumeric.ValueChanged += new System.EventHandler(this.NotifyBackgroundAlphaNumeric_ValueChanged);
            // 
            // NotifyStringColorButton
            // 
            this.NotifyStringColorButton.Location = new System.Drawing.Point(216, 66);
            this.NotifyStringColorButton.Name = "NotifyStringColorButton";
            this.NotifyStringColorButton.Size = new System.Drawing.Size(75, 23);
            this.NotifyStringColorButton.TabIndex = 6;
            this.NotifyStringColorButton.Text = "文字色";
            this.NotifyStringColorButton.UseVisualStyleBackColor = true;
            this.NotifyStringColorButton.Click += new System.EventHandler(this.NotifyColorButton_Click);
            // 
            // NotifyStringColorPictureBox
            // 
            this.NotifyStringColorPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NotifyStringColorPictureBox.Location = new System.Drawing.Point(185, 65);
            this.NotifyStringColorPictureBox.Name = "NotifyStringColorPictureBox";
            this.NotifyStringColorPictureBox.Size = new System.Drawing.Size(24, 24);
            this.NotifyStringColorPictureBox.TabIndex = 5;
            this.NotifyStringColorPictureBox.TabStop = false;
            // 
            // NotifyBackgroundColorButton
            // 
            this.NotifyBackgroundColorButton.Location = new System.Drawing.Point(40, 66);
            this.NotifyBackgroundColorButton.Name = "NotifyBackgroundColorButton";
            this.NotifyBackgroundColorButton.Size = new System.Drawing.Size(75, 23);
            this.NotifyBackgroundColorButton.TabIndex = 4;
            this.NotifyBackgroundColorButton.Text = "背景色";
            this.NotifyBackgroundColorButton.UseVisualStyleBackColor = true;
            this.NotifyBackgroundColorButton.Click += new System.EventHandler(this.NotifyColorButton_Click);
            // 
            // NotifyBackgroundColorPictureBox
            // 
            this.NotifyBackgroundColorPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NotifyBackgroundColorPictureBox.Location = new System.Drawing.Point(9, 65);
            this.NotifyBackgroundColorPictureBox.Name = "NotifyBackgroundColorPictureBox";
            this.NotifyBackgroundColorPictureBox.Size = new System.Drawing.Size(24, 24);
            this.NotifyBackgroundColorPictureBox.TabIndex = 3;
            this.NotifyBackgroundColorPictureBox.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "新着ツイートアニメーションの色設定";
            // 
            // AnimationCheckBox
            // 
            this.AnimationCheckBox.BackColor = System.Drawing.SystemColors.Control;
            this.AnimationCheckBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.AnimationCheckBox.CheckOnClick = true;
            this.AnimationCheckBox.FormattingEnabled = true;
            this.AnimationCheckBox.Items.AddRange(new object[] {
            "挿入アニメーションを有効化する",
            "新着ツイートアニメーションを有効化する"});
            this.AnimationCheckBox.Location = new System.Drawing.Point(3, 15);
            this.AnimationCheckBox.Name = "AnimationCheckBox";
            this.AnimationCheckBox.Size = new System.Drawing.Size(293, 28);
            this.AnimationCheckBox.TabIndex = 1;
            this.AnimationCheckBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.AnimationCheckBox_ItemCheck);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.timelineSettingCheckBox);
            this.groupBox2.Location = new System.Drawing.Point(3, 85);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(398, 118);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "セル設定";
            // 
            // timelineSettingCheckBox
            // 
            this.timelineSettingCheckBox.BackColor = System.Drawing.SystemColors.Control;
            this.timelineSettingCheckBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.timelineSettingCheckBox.CheckOnClick = true;
            this.timelineSettingCheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timelineSettingCheckBox.FormattingEnabled = true;
            this.timelineSettingCheckBox.Items.AddRange(new object[] {
            "時刻をクリックしたとき、ブラウザでツイートURLを開く",
            "\"via～\"をクリックしたとき、クライアントのURLを開く",
            "\"～がリツイートしました\"をクリックしたとき、リツイートしたユーザの情報を開く",
            "リツイートのテキストを太文字にする",
            "通知ツイートのテキストを太文字にする",
            "返信ツイートのテキストを太文字にする",
            "時刻表示を絶対表記にする(例: 200x年y月z日 a時b分c秒)"});
            this.timelineSettingCheckBox.Location = new System.Drawing.Point(3, 15);
            this.timelineSettingCheckBox.Name = "timelineSettingCheckBox";
            this.timelineSettingCheckBox.Size = new System.Drawing.Size(392, 100);
            this.timelineSettingCheckBox.TabIndex = 1;
            this.timelineSettingCheckBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.timelineSettingCheckBox_ItemCheck);
            // 
            // selectColor
            // 
            this.selectColor.FullOpen = true;
            // 
            // TimelineManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "TimelineManagement";
            this.Size = new System.Drawing.Size(410, 413);
            this.groupBox1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SavingTweetNumNumeric)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NotifyBackgroundAlphaNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NotifyStringColorPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NotifyBackgroundColorPictureBox)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox timelineSettingCheckBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckedListBox AnimationCheckBox;
        private System.Windows.Forms.Button NotifyStringColorButton;
        private System.Windows.Forms.PictureBox NotifyStringColorPictureBox;
        private System.Windows.Forms.Button NotifyBackgroundColorButton;
        private System.Windows.Forms.PictureBox NotifyBackgroundColorPictureBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown NotifyBackgroundAlphaNumeric;
        private System.Windows.Forms.ColorDialog selectColor;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckedListBox fTimelineSettingCheckBox;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown SavingTweetNumNumeric;
    }
}
