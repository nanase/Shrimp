namespace Shrimp.ControlParts.TabSetting
{
    partial class TabCategory
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
            this.TabCategoryBox = new System.Windows.Forms.ComboBox();
            this.otherBox = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.TabCategoryBox);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(362, 46);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "タブの種類";
            // 
            // TabCategoryBox
            // 
            this.TabCategoryBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabCategoryBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TabCategoryBox.FormattingEnabled = true;
            this.TabCategoryBox.Items.AddRange(new object[] {
            "なし",
            "ホームタイムライン",
            "返信",
            "ダイレクトメッセージ",
            "リスト",
            "通知"});
            this.TabCategoryBox.Location = new System.Drawing.Point(3, 15);
            this.TabCategoryBox.Name = "TabCategoryBox";
            this.TabCategoryBox.Size = new System.Drawing.Size(356, 20);
            this.TabCategoryBox.TabIndex = 0;
            this.TabCategoryBox.SelectedIndexChanged += new System.EventHandler(this.TabCategoryBox_SelectedIndexChanged);
            // 
            // otherBox
            // 
            this.otherBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.otherBox.Location = new System.Drawing.Point(0, 53);
            this.otherBox.Name = "otherBox";
            this.otherBox.Size = new System.Drawing.Size(362, 232);
            this.otherBox.TabIndex = 1;
            this.otherBox.TabStop = false;
            this.otherBox.Text = "設定";
            // 
            // TabCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.otherBox);
            this.Controls.Add(this.groupBox1);
            this.Name = "TabCategory";
            this.Size = new System.Drawing.Size(362, 288);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox TabCategoryBox;
        private System.Windows.Forms.GroupBox otherBox;
    }
}
