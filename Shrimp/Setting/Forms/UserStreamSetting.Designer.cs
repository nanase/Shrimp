namespace Shrimp.Setting.Forms
{
    partial class UserStreamSetting
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
            this.UserStreamCheckedBox = new System.Windows.Forms.CheckedListBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.UserStreamCheckedBox);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(435, 460);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "UserStreamの詳細設定";
            // 
            // UserStreamCheckedBox
            // 
            this.UserStreamCheckedBox.BackColor = System.Drawing.SystemColors.Control;
            this.UserStreamCheckedBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.UserStreamCheckedBox.CheckOnClick = true;
            this.UserStreamCheckedBox.FormattingEnabled = true;
            this.UserStreamCheckedBox.Items.AddRange(new object[] {
            "フォローユーザーのお気に入りやフォロー状況を取得する",
            "すべてのリプライを取得する",
            "フォロー外からのリプライ・通知を受信しない"});
            this.UserStreamCheckedBox.Location = new System.Drawing.Point(6, 18);
            this.UserStreamCheckedBox.Name = "UserStreamCheckedBox";
            this.UserStreamCheckedBox.Size = new System.Drawing.Size(423, 56);
            this.UserStreamCheckedBox.TabIndex = 0;
            this.UserStreamCheckedBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.UserStreamCheckedBox_ItemCheck);
            // 
            // UserStreamSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "UserStreamSetting";
            this.Size = new System.Drawing.Size(435, 460);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox UserStreamCheckedBox;
    }
}
