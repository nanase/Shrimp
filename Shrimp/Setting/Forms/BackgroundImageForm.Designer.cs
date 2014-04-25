namespace Shrimp.Setting.Forms
{
    partial class BackgroundImageForm
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
            this.ImagePositionBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.FileDialogButton = new System.Windows.Forms.Button();
            this.BackgroundImagePathBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ImageFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.DeleteBackgroundImageButton = new System.Windows.Forms.Button();
            this.BackgroundImageTransparentBar = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BackgroundImageTransparentBar)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.BackgroundImageTransparentBar);
            this.groupBox1.Controls.Add(this.DeleteBackgroundImageButton);
            this.groupBox1.Controls.Add(this.ImagePositionBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.FileDialogButton);
            this.groupBox1.Controls.Add(this.BackgroundImagePathBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(435, 460);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "タイムラインの背景色";
            // 
            // ImagePositionBox
            // 
            this.ImagePositionBox.DisplayMember = "0";
            this.ImagePositionBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ImagePositionBox.FormattingEnabled = true;
            this.ImagePositionBox.Items.AddRange(new object[] {
            "左上",
            "中央",
            "右下"});
            this.ImagePositionBox.Location = new System.Drawing.Point(8, 111);
            this.ImagePositionBox.Name = "ImagePositionBox";
            this.ImagePositionBox.Size = new System.Drawing.Size(131, 20);
            this.ImagePositionBox.TabIndex = 4;
            this.ImagePositionBox.ValueMember = "0";
            this.ImagePositionBox.SelectedIndexChanged += new System.EventHandler(this.ImagePositionBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "画像の配置方法";
            // 
            // FileDialogButton
            // 
            this.FileDialogButton.Location = new System.Drawing.Point(358, 28);
            this.FileDialogButton.Name = "FileDialogButton";
            this.FileDialogButton.Size = new System.Drawing.Size(34, 23);
            this.FileDialogButton.TabIndex = 2;
            this.FileDialogButton.Text = "...";
            this.FileDialogButton.UseVisualStyleBackColor = true;
            this.FileDialogButton.Click += new System.EventHandler(this.FileDialogButton_Click);
            // 
            // BackgroundImagePathBox
            // 
            this.BackgroundImagePathBox.Location = new System.Drawing.Point(8, 30);
            this.BackgroundImagePathBox.Name = "BackgroundImagePathBox";
            this.BackgroundImagePathBox.ReadOnly = true;
            this.BackgroundImagePathBox.Size = new System.Drawing.Size(344, 19);
            this.BackgroundImagePathBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "背景画像のパス";
            // 
            // ImageFileDialog
            // 
            this.ImageFileDialog.Filter = "背景画像(*.jpeg;*.jpg;*.png;*.gif;*.bmp)|*.jpeg;*.jpg;*.png;*.gif;*.bmp|すべてのファイル(*.*)" +
    "|*.*";
            // 
            // DeleteBackgroundImageButton
            // 
            this.DeleteBackgroundImageButton.Location = new System.Drawing.Point(8, 55);
            this.DeleteBackgroundImageButton.Name = "DeleteBackgroundImageButton";
            this.DeleteBackgroundImageButton.Size = new System.Drawing.Size(110, 23);
            this.DeleteBackgroundImageButton.TabIndex = 5;
            this.DeleteBackgroundImageButton.Text = "背景画像を削除";
            this.DeleteBackgroundImageButton.UseVisualStyleBackColor = true;
            this.DeleteBackgroundImageButton.Click += new System.EventHandler(this.DeleteBackgroundImageButton_Click);
            // 
            // BackgroundImageTransparentBar
            // 
            this.BackgroundImageTransparentBar.Location = new System.Drawing.Point(8, 160);
            this.BackgroundImageTransparentBar.Maximum = 255;
            this.BackgroundImageTransparentBar.Name = "BackgroundImageTransparentBar";
            this.BackgroundImageTransparentBar.Size = new System.Drawing.Size(387, 45);
            this.BackgroundImageTransparentBar.TabIndex = 6;
            this.BackgroundImageTransparentBar.TickFrequency = 10;
            this.BackgroundImageTransparentBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.BackgroundImageTransparentBar.Scroll += new System.EventHandler(this.BackgroundImageTransparentBar_Scroll);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "背景画像の透過率";
            // 
            // BackgroundImageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "BackgroundImageForm";
            this.Size = new System.Drawing.Size(435, 460);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BackgroundImageTransparentBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox ImagePositionBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button FileDialogButton;
        private System.Windows.Forms.TextBox BackgroundImagePathBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog ImageFileDialog;
        private System.Windows.Forms.Button DeleteBackgroundImageButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar BackgroundImageTransparentBar;
    }
}
