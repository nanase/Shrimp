namespace Shrimp.Setting.Forms
{
    partial class PluginForm
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
            this.pluginList = new System.Windows.Forms.ListView();
            this.PluginNameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DevNameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.VersionHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pluginList);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(332, 225);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "登録されているプラグイン一覧";
            // 
            // pluginList
            // 
            this.pluginList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pluginList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.PluginNameHeader,
            this.DevNameHeader,
            this.VersionHeader});
            this.pluginList.FullRowSelect = true;
            this.pluginList.GridLines = true;
            this.pluginList.Location = new System.Drawing.Point(6, 18);
            this.pluginList.MultiSelect = false;
            this.pluginList.Name = "pluginList";
            this.pluginList.Size = new System.Drawing.Size(320, 148);
            this.pluginList.TabIndex = 0;
            this.pluginList.UseCompatibleStateImageBehavior = false;
            this.pluginList.View = System.Windows.Forms.View.Details;
            // 
            // PluginNameHeader
            // 
            this.PluginNameHeader.Text = "プラグイン名";
            this.PluginNameHeader.Width = 104;
            // 
            // DevNameHeader
            // 
            this.DevNameHeader.Text = "開発者名";
            this.DevNameHeader.Width = 113;
            // 
            // VersionHeader
            // 
            this.VersionHeader.Text = "バージョン";
            this.VersionHeader.Width = 108;
            // 
            // PluginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "PluginForm";
            this.Size = new System.Drawing.Size(332, 225);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView pluginList;
        private System.Windows.Forms.ColumnHeader PluginNameHeader;
        private System.Windows.Forms.ColumnHeader DevNameHeader;
        private System.Windows.Forms.ColumnHeader VersionHeader;
    }
}
