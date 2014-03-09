namespace Shrimp.ControlParts.TabSetting
{
    partial class NotifySelectView
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
            this.notifyBox = new System.Windows.Forms.CheckedListBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.notifyBox);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(354, 212);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "通知を表示する種類を選択してください";
            // 
            // notifyBox
            // 
            this.notifyBox.BackColor = System.Drawing.SystemColors.Control;
            this.notifyBox.CheckOnClick = true;
            this.notifyBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.notifyBox.FormattingEnabled = true;
            this.notifyBox.Items.AddRange(new object[] {
            "お気に入りに追加されたとき",
            "お気に入りから削除されたとき",
            "フォローされたとき",
            "アンフォローしたとき"});
            this.notifyBox.Location = new System.Drawing.Point(3, 15);
            this.notifyBox.Name = "notifyBox";
            this.notifyBox.Size = new System.Drawing.Size(348, 194);
            this.notifyBox.TabIndex = 0;
            this.notifyBox.SelectedValueChanged += new System.EventHandler(this.notifyBox_SelectedValueChanged);
            // 
            // NotifySelectView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "NotifySelectView";
            this.Size = new System.Drawing.Size(354, 212);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox notifyBox;
    }
}
