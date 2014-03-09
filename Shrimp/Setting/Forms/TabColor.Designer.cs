namespace Shrimp.Setting.Forms
{
    partial class TabColor
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
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.SelectCheckBox = new System.Windows.Forms.CheckedListBox();
            this.SelectTextPicture = new System.Windows.Forms.PictureBox();
            this.SelectTextButton = new System.Windows.Forms.Button();
            this.SelectBackgroundPicture = new System.Windows.Forms.PictureBox();
            this.SelectBackgrounButton = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.NormalCheckBox = new System.Windows.Forms.CheckedListBox();
            this.NormalTextPicture = new System.Windows.Forms.PictureBox();
            this.NormalTextButton = new System.Windows.Forms.Button();
            this.NormalBackgroundPicture = new System.Windows.Forms.PictureBox();
            this.NormalBackgroundButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.UnOpenedCheckBox = new System.Windows.Forms.CheckedListBox();
            this.UnOpenedTextPicture = new System.Windows.Forms.PictureBox();
            this.UnOpenedTextButton = new System.Windows.Forms.Button();
            this.UnOpenedBackgroundPicture = new System.Windows.Forms.PictureBox();
            this.UnOpenedBackgroundButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.UnReadCheckBox = new System.Windows.Forms.CheckedListBox();
            this.UnReadTextPicture = new System.Windows.Forms.PictureBox();
            this.UnReadTextButton = new System.Windows.Forms.Button();
            this.UnReadBackgroundPicture = new System.Windows.Forms.PictureBox();
            this.UnReadBackgroundButton = new System.Windows.Forms.Button();
            this.SelectColorDialog = new System.Windows.Forms.ColorDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SelectTextPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SelectBackgroundPicture)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NormalTextPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NormalBackgroundPicture)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UnOpenedTextPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UnOpenedBackgroundPicture)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UnReadTextPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UnReadBackgroundPicture)).BeginInit();
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
            this.groupBox1.Size = new System.Drawing.Size(435, 460);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "タブの配色";
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.SelectCheckBox);
            this.groupBox5.Controls.Add(this.SelectTextPicture);
            this.groupBox5.Controls.Add(this.SelectTextButton);
            this.groupBox5.Controls.Add(this.SelectBackgroundPicture);
            this.groupBox5.Controls.Add(this.SelectBackgrounButton);
            this.groupBox5.Location = new System.Drawing.Point(6, 252);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(417, 73);
            this.groupBox5.TabIndex = 10;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "選択タブ";
            // 
            // SelectCheckBox
            // 
            this.SelectCheckBox.BackColor = System.Drawing.SystemColors.Control;
            this.SelectCheckBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SelectCheckBox.CheckOnClick = true;
            this.SelectCheckBox.FormattingEnabled = true;
            this.SelectCheckBox.Items.AddRange(new object[] {
            "文字を太くする"});
            this.SelectCheckBox.Location = new System.Drawing.Point(19, 49);
            this.SelectCheckBox.Name = "SelectCheckBox";
            this.SelectCheckBox.Size = new System.Drawing.Size(392, 14);
            this.SelectCheckBox.TabIndex = 9;
            this.SelectCheckBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.SelectCheckBox_ItemCheck);
            // 
            // SelectTextPicture
            // 
            this.SelectTextPicture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SelectTextPicture.Location = new System.Drawing.Point(152, 20);
            this.SelectTextPicture.Name = "SelectTextPicture";
            this.SelectTextPicture.Size = new System.Drawing.Size(24, 24);
            this.SelectTextPicture.TabIndex = 8;
            this.SelectTextPicture.TabStop = false;
            // 
            // SelectTextButton
            // 
            this.SelectTextButton.Location = new System.Drawing.Point(182, 20);
            this.SelectTextButton.Name = "SelectTextButton";
            this.SelectTextButton.Size = new System.Drawing.Size(88, 23);
            this.SelectTextButton.TabIndex = 7;
            this.SelectTextButton.Text = "テキスト";
            this.SelectTextButton.UseVisualStyleBackColor = true;
            this.SelectTextButton.Click += new System.EventHandler(this.SelectColorButtonClicked);
            // 
            // SelectBackgroundPicture
            // 
            this.SelectBackgroundPicture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SelectBackgroundPicture.Location = new System.Drawing.Point(19, 18);
            this.SelectBackgroundPicture.Name = "SelectBackgroundPicture";
            this.SelectBackgroundPicture.Size = new System.Drawing.Size(24, 24);
            this.SelectBackgroundPicture.TabIndex = 6;
            this.SelectBackgroundPicture.TabStop = false;
            // 
            // SelectBackgrounButton
            // 
            this.SelectBackgrounButton.Location = new System.Drawing.Point(49, 19);
            this.SelectBackgrounButton.Name = "SelectBackgrounButton";
            this.SelectBackgrounButton.Size = new System.Drawing.Size(88, 23);
            this.SelectBackgrounButton.TabIndex = 5;
            this.SelectBackgrounButton.Text = "背景";
            this.SelectBackgrounButton.UseVisualStyleBackColor = true;
            this.SelectBackgrounButton.Click += new System.EventHandler(this.SelectColorButtonClicked);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.NormalCheckBox);
            this.groupBox4.Controls.Add(this.NormalTextPicture);
            this.groupBox4.Controls.Add(this.NormalTextButton);
            this.groupBox4.Controls.Add(this.NormalBackgroundPicture);
            this.groupBox4.Controls.Add(this.NormalBackgroundButton);
            this.groupBox4.Location = new System.Drawing.Point(6, 173);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(417, 73);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "通常タブ";
            // 
            // NormalCheckBox
            // 
            this.NormalCheckBox.BackColor = System.Drawing.SystemColors.Control;
            this.NormalCheckBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.NormalCheckBox.CheckOnClick = true;
            this.NormalCheckBox.FormattingEnabled = true;
            this.NormalCheckBox.Items.AddRange(new object[] {
            "文字を太くする"});
            this.NormalCheckBox.Location = new System.Drawing.Point(19, 49);
            this.NormalCheckBox.Name = "NormalCheckBox";
            this.NormalCheckBox.Size = new System.Drawing.Size(392, 14);
            this.NormalCheckBox.TabIndex = 9;
            this.NormalCheckBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.SelectCheckBox_ItemCheck);
            // 
            // NormalTextPicture
            // 
            this.NormalTextPicture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NormalTextPicture.Location = new System.Drawing.Point(152, 20);
            this.NormalTextPicture.Name = "NormalTextPicture";
            this.NormalTextPicture.Size = new System.Drawing.Size(24, 24);
            this.NormalTextPicture.TabIndex = 8;
            this.NormalTextPicture.TabStop = false;
            // 
            // NormalTextButton
            // 
            this.NormalTextButton.Location = new System.Drawing.Point(182, 20);
            this.NormalTextButton.Name = "NormalTextButton";
            this.NormalTextButton.Size = new System.Drawing.Size(88, 23);
            this.NormalTextButton.TabIndex = 7;
            this.NormalTextButton.Text = "テキスト";
            this.NormalTextButton.UseVisualStyleBackColor = true;
            this.NormalTextButton.Click += new System.EventHandler(this.SelectColorButtonClicked);
            // 
            // NormalBackgroundPicture
            // 
            this.NormalBackgroundPicture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NormalBackgroundPicture.Location = new System.Drawing.Point(19, 18);
            this.NormalBackgroundPicture.Name = "NormalBackgroundPicture";
            this.NormalBackgroundPicture.Size = new System.Drawing.Size(24, 24);
            this.NormalBackgroundPicture.TabIndex = 6;
            this.NormalBackgroundPicture.TabStop = false;
            // 
            // NormalBackgroundButton
            // 
            this.NormalBackgroundButton.Location = new System.Drawing.Point(49, 19);
            this.NormalBackgroundButton.Name = "NormalBackgroundButton";
            this.NormalBackgroundButton.Size = new System.Drawing.Size(88, 23);
            this.NormalBackgroundButton.TabIndex = 5;
            this.NormalBackgroundButton.Text = "背景";
            this.NormalBackgroundButton.UseVisualStyleBackColor = true;
            this.NormalBackgroundButton.Click += new System.EventHandler(this.SelectColorButtonClicked);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.UnOpenedCheckBox);
            this.groupBox3.Controls.Add(this.UnOpenedTextPicture);
            this.groupBox3.Controls.Add(this.UnOpenedTextButton);
            this.groupBox3.Controls.Add(this.UnOpenedBackgroundPicture);
            this.groupBox3.Controls.Add(this.UnOpenedBackgroundButton);
            this.groupBox3.Location = new System.Drawing.Point(6, 94);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(417, 73);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "未開封タブ(作成直後のタブ)";
            // 
            // UnOpenedCheckBox
            // 
            this.UnOpenedCheckBox.BackColor = System.Drawing.SystemColors.Control;
            this.UnOpenedCheckBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.UnOpenedCheckBox.CheckOnClick = true;
            this.UnOpenedCheckBox.FormattingEnabled = true;
            this.UnOpenedCheckBox.Items.AddRange(new object[] {
            "文字を太くする"});
            this.UnOpenedCheckBox.Location = new System.Drawing.Point(19, 49);
            this.UnOpenedCheckBox.Name = "UnOpenedCheckBox";
            this.UnOpenedCheckBox.Size = new System.Drawing.Size(392, 14);
            this.UnOpenedCheckBox.TabIndex = 9;
            this.UnOpenedCheckBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.SelectCheckBox_ItemCheck);
            // 
            // UnOpenedTextPicture
            // 
            this.UnOpenedTextPicture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UnOpenedTextPicture.Location = new System.Drawing.Point(152, 20);
            this.UnOpenedTextPicture.Name = "UnOpenedTextPicture";
            this.UnOpenedTextPicture.Size = new System.Drawing.Size(24, 24);
            this.UnOpenedTextPicture.TabIndex = 8;
            this.UnOpenedTextPicture.TabStop = false;
            // 
            // UnOpenedTextButton
            // 
            this.UnOpenedTextButton.Location = new System.Drawing.Point(182, 20);
            this.UnOpenedTextButton.Name = "UnOpenedTextButton";
            this.UnOpenedTextButton.Size = new System.Drawing.Size(88, 23);
            this.UnOpenedTextButton.TabIndex = 7;
            this.UnOpenedTextButton.Text = "テキスト";
            this.UnOpenedTextButton.UseVisualStyleBackColor = true;
            this.UnOpenedTextButton.Click += new System.EventHandler(this.SelectColorButtonClicked);
            // 
            // UnOpenedBackgroundPicture
            // 
            this.UnOpenedBackgroundPicture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UnOpenedBackgroundPicture.Location = new System.Drawing.Point(19, 18);
            this.UnOpenedBackgroundPicture.Name = "UnOpenedBackgroundPicture";
            this.UnOpenedBackgroundPicture.Size = new System.Drawing.Size(24, 24);
            this.UnOpenedBackgroundPicture.TabIndex = 6;
            this.UnOpenedBackgroundPicture.TabStop = false;
            // 
            // UnOpenedBackgroundButton
            // 
            this.UnOpenedBackgroundButton.Location = new System.Drawing.Point(49, 19);
            this.UnOpenedBackgroundButton.Name = "UnOpenedBackgroundButton";
            this.UnOpenedBackgroundButton.Size = new System.Drawing.Size(88, 23);
            this.UnOpenedBackgroundButton.TabIndex = 5;
            this.UnOpenedBackgroundButton.Text = "背景";
            this.UnOpenedBackgroundButton.UseVisualStyleBackColor = true;
            this.UnOpenedBackgroundButton.Click += new System.EventHandler(this.SelectColorButtonClicked);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.UnReadCheckBox);
            this.groupBox2.Controls.Add(this.UnReadTextPicture);
            this.groupBox2.Controls.Add(this.UnReadTextButton);
            this.groupBox2.Controls.Add(this.UnReadBackgroundPicture);
            this.groupBox2.Controls.Add(this.UnReadBackgroundButton);
            this.groupBox2.Location = new System.Drawing.Point(6, 18);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(417, 71);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "未読タブ";
            // 
            // UnReadCheckBox
            // 
            this.UnReadCheckBox.BackColor = System.Drawing.SystemColors.Control;
            this.UnReadCheckBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.UnReadCheckBox.CheckOnClick = true;
            this.UnReadCheckBox.FormattingEnabled = true;
            this.UnReadCheckBox.Items.AddRange(new object[] {
            "文字を太くする"});
            this.UnReadCheckBox.Location = new System.Drawing.Point(19, 49);
            this.UnReadCheckBox.Name = "UnReadCheckBox";
            this.UnReadCheckBox.Size = new System.Drawing.Size(392, 14);
            this.UnReadCheckBox.TabIndex = 4;
            this.UnReadCheckBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.SelectCheckBox_ItemCheck);
            // 
            // UnReadTextPicture
            // 
            this.UnReadTextPicture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UnReadTextPicture.Location = new System.Drawing.Point(152, 20);
            this.UnReadTextPicture.Name = "UnReadTextPicture";
            this.UnReadTextPicture.Size = new System.Drawing.Size(24, 24);
            this.UnReadTextPicture.TabIndex = 3;
            this.UnReadTextPicture.TabStop = false;
            // 
            // UnReadTextButton
            // 
            this.UnReadTextButton.Location = new System.Drawing.Point(182, 20);
            this.UnReadTextButton.Name = "UnReadTextButton";
            this.UnReadTextButton.Size = new System.Drawing.Size(88, 23);
            this.UnReadTextButton.TabIndex = 2;
            this.UnReadTextButton.Text = "テキスト";
            this.UnReadTextButton.UseVisualStyleBackColor = true;
            this.UnReadTextButton.Click += new System.EventHandler(this.SelectColorButtonClicked);
            // 
            // UnReadBackgroundPicture
            // 
            this.UnReadBackgroundPicture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UnReadBackgroundPicture.Location = new System.Drawing.Point(19, 18);
            this.UnReadBackgroundPicture.Name = "UnReadBackgroundPicture";
            this.UnReadBackgroundPicture.Size = new System.Drawing.Size(24, 24);
            this.UnReadBackgroundPicture.TabIndex = 1;
            this.UnReadBackgroundPicture.TabStop = false;
            // 
            // UnReadBackgroundButton
            // 
            this.UnReadBackgroundButton.Location = new System.Drawing.Point(49, 19);
            this.UnReadBackgroundButton.Name = "UnReadBackgroundButton";
            this.UnReadBackgroundButton.Size = new System.Drawing.Size(88, 23);
            this.UnReadBackgroundButton.TabIndex = 0;
            this.UnReadBackgroundButton.Text = "背景";
            this.UnReadBackgroundButton.UseVisualStyleBackColor = true;
            this.UnReadBackgroundButton.Click += new System.EventHandler(this.SelectColorButtonClicked);
            // 
            // TabColor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "TabColor";
            this.Size = new System.Drawing.Size(435, 460);
            this.groupBox1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SelectTextPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SelectBackgroundPicture)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.NormalTextPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NormalBackgroundPicture)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.UnOpenedTextPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UnOpenedBackgroundPicture)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.UnReadTextPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UnReadBackgroundPicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox UnReadTextPicture;
        private System.Windows.Forms.Button UnReadTextButton;
        private System.Windows.Forms.PictureBox UnReadBackgroundPicture;
        private System.Windows.Forms.Button UnReadBackgroundButton;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckedListBox UnOpenedCheckBox;
        private System.Windows.Forms.PictureBox UnOpenedTextPicture;
        private System.Windows.Forms.Button UnOpenedTextButton;
        private System.Windows.Forms.PictureBox UnOpenedBackgroundPicture;
        private System.Windows.Forms.Button UnOpenedBackgroundButton;
        private System.Windows.Forms.CheckedListBox UnReadCheckBox;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckedListBox SelectCheckBox;
        private System.Windows.Forms.PictureBox SelectTextPicture;
        private System.Windows.Forms.Button SelectTextButton;
        private System.Windows.Forms.PictureBox SelectBackgroundPicture;
        private System.Windows.Forms.Button SelectBackgrounButton;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckedListBox NormalCheckBox;
        private System.Windows.Forms.PictureBox NormalTextPicture;
        private System.Windows.Forms.Button NormalTextButton;
        private System.Windows.Forms.PictureBox NormalBackgroundPicture;
        private System.Windows.Forms.Button NormalBackgroundButton;
		private System.Windows.Forms.ColorDialog SelectColorDialog;
    }
}
