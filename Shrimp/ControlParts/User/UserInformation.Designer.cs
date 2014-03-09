namespace Shrimp.ControlParts.User
{
    partial class UserInformation
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
            this.UserInformationSplit = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.UserInformationSplit)).BeginInit();
            this.UserInformationSplit.SuspendLayout();
            this.SuspendLayout();
            // 
            // UserInformationSplit
            // 
            this.UserInformationSplit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UserInformationSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UserInformationSplit.Location = new System.Drawing.Point(0, 0);
            this.UserInformationSplit.Name = "UserInformationSplit";
            this.UserInformationSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.UserInformationSplit.Size = new System.Drawing.Size(255, 420);
            this.UserInformationSplit.SplitterDistance = 135;
            this.UserInformationSplit.TabIndex = 0;
            // 
            // UserInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.UserInformationSplit);
            this.Name = "UserInformation";
            this.Size = new System.Drawing.Size(255, 420);
            ((System.ComponentModel.ISupportInitialize)(this.UserInformationSplit)).EndInit();
            this.UserInformationSplit.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer UserInformationSplit;
    }
}
