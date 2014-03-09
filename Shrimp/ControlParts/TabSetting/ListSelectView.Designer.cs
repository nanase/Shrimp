namespace Shrimp.ControlParts.TabSetting
{
    partial class ListSelectView
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
            this.listSelectCombobox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listSelectCombobox
            // 
            this.listSelectCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.listSelectCombobox.FormattingEnabled = true;
            this.listSelectCombobox.Location = new System.Drawing.Point(6, 15);
            this.listSelectCombobox.Name = "listSelectCombobox";
            this.listSelectCombobox.Size = new System.Drawing.Size(436, 20);
            this.listSelectCombobox.TabIndex = 0;
            this.listSelectCombobox.SelectedIndexChanged += new System.EventHandler(this.listSelectCombobox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "表示するリストを選択してください";
            // 
            // ListSelectView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listSelectCombobox);
            this.Name = "ListSelectView";
            this.Size = new System.Drawing.Size(445, 51);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox listSelectCombobox;
        private System.Windows.Forms.Label label1;
    }
}
