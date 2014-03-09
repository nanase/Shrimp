namespace Shrimp.ControlParts.Timeline
{
    partial class TimelineControl
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
            ShrimpDispose ( true );
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
            this.vsc = new System.Windows.Forms.VScrollBar();
            this.SuspendLayout();
            // 
            // vsc
            // 
            this.vsc.Dock = System.Windows.Forms.DockStyle.Right;
            this.vsc.Location = new System.Drawing.Point(-7, 0);
            this.vsc.Name = "vsc";
            this.vsc.Size = new System.Drawing.Size(17, 10);
            this.vsc.TabIndex = 0;
            this.vsc.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vsc_Scroll);
            // 
            // TimelineControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.vsc);
            this.DoubleBuffered = true;
            this.Name = "TimelineControl";
            this.Size = new System.Drawing.Size(10, 10);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.TimelineControl_Paint);
            this.Enter += new System.EventHandler(this.TimelineControl_Enter);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TimelineControl_MouseDoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TimelineControl_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TimelineControl_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TimelineControl_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.VScrollBar vsc;

    }
}
