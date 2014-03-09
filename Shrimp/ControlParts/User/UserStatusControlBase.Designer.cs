namespace Shrimp.ControlParts.User
{
    partial class UserStatusControlBase
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
            this.SuspendLayout();
            // 
            // UserStatusControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.DoubleBuffered = true;
            this.Name = "UserStatusControl";
            this.Size = new System.Drawing.Size(10, 10);
            this.Paint += new System.Windows.Forms.PaintEventHandler ( this.UserStatusContol_Paint );
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UserStatusControl_KeyDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.UserStatusContol_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.UserStatusContol_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion


    }
}
