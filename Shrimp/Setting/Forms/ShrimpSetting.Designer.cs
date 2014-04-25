namespace Shrimp.Setting.Forms
{
    partial class ShrimpSetting
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
            this.ShrimpCheckedBox = new System.Windows.Forms.CheckedListBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ShrimpCheckedBox);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(435, 460);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Shrimp全般の設定";
            // 
            // ShrimpCheckedBox
            // 
            this.ShrimpCheckedBox.BackColor = System.Drawing.SystemColors.Control;
            this.ShrimpCheckedBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ShrimpCheckedBox.CheckOnClick = true;
            this.ShrimpCheckedBox.FormattingEnabled = true;
            this.ShrimpCheckedBox.Items.AddRange(new object[] {
            "Shrimpを閉じたとき終了せずタスクトレイにいれる"});
            this.ShrimpCheckedBox.Location = new System.Drawing.Point(6, 18);
            this.ShrimpCheckedBox.Name = "ShrimpCheckedBox";
            this.ShrimpCheckedBox.Size = new System.Drawing.Size(423, 14);
            this.ShrimpCheckedBox.TabIndex = 0;
            this.ShrimpCheckedBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ShrimpCheckedBox_ItemCheck);
            // 
            // ShrimpSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "ShrimpSetting";
            this.Size = new System.Drawing.Size(435, 460);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox ShrimpCheckedBox;
    }
}
