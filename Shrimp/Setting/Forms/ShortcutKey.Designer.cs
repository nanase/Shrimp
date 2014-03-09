namespace Shrimp.Setting.Forms
{
	partial class ShortcutKey
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.EditShortcutKeyButton = new System.Windows.Forms.Button();
			this.DelShortcutKeyButton = new System.Windows.Forms.Button();
			this.AddShortcutKeyButton = new System.Windows.Forms.Button();
			this.shortcutKeysList = new System.Windows.Forms.ListView();
			this.SourceColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.KeyColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.ValuneColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.EditShortcutKeyButton);
			this.groupBox1.Controls.Add(this.DelShortcutKeyButton);
			this.groupBox1.Controls.Add(this.AddShortcutKeyButton);
			this.groupBox1.Controls.Add(this.shortcutKeysList);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(350, 417);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "ショートカットキーの設定";
			// 
			// EditShortcutKeyButton
			// 
			this.EditShortcutKeyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.EditShortcutKeyButton.Location = new System.Drawing.Point(177, 237);
			this.EditShortcutKeyButton.Name = "EditShortcutKeyButton";
			this.EditShortcutKeyButton.Size = new System.Drawing.Size(75, 23);
			this.EditShortcutKeyButton.TabIndex = 3;
			this.EditShortcutKeyButton.Text = "編集";
			this.EditShortcutKeyButton.UseVisualStyleBackColor = true;
			this.EditShortcutKeyButton.Click += new System.EventHandler(this.EditShortcutKeyButton_Click);
			// 
			// DelShortcutKeyButton
			// 
			this.DelShortcutKeyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.DelShortcutKeyButton.Location = new System.Drawing.Point(269, 237);
			this.DelShortcutKeyButton.Name = "DelShortcutKeyButton";
			this.DelShortcutKeyButton.Size = new System.Drawing.Size(75, 23);
			this.DelShortcutKeyButton.TabIndex = 2;
			this.DelShortcutKeyButton.Text = "削除";
			this.DelShortcutKeyButton.UseVisualStyleBackColor = true;
			this.DelShortcutKeyButton.Click += new System.EventHandler(this.DelShortcutKeyButton_Click);
			// 
			// AddShortcutKeyButton
			// 
			this.AddShortcutKeyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.AddShortcutKeyButton.Location = new System.Drawing.Point(80, 237);
			this.AddShortcutKeyButton.Name = "AddShortcutKeyButton";
			this.AddShortcutKeyButton.Size = new System.Drawing.Size(75, 23);
			this.AddShortcutKeyButton.TabIndex = 1;
			this.AddShortcutKeyButton.Text = "追加";
			this.AddShortcutKeyButton.UseVisualStyleBackColor = true;
			this.AddShortcutKeyButton.Click += new System.EventHandler(this.AddShortcutKeyButton_Click);
			// 
			// shortcutKeysList
			// 
			this.shortcutKeysList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.shortcutKeysList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.SourceColumn,
            this.KeyColumn,
            this.ValuneColumn});
			this.shortcutKeysList.FullRowSelect = true;
			this.shortcutKeysList.GridLines = true;
			this.shortcutKeysList.Location = new System.Drawing.Point(7, 19);
			this.shortcutKeysList.MultiSelect = false;
			this.shortcutKeysList.Name = "shortcutKeysList";
			this.shortcutKeysList.Size = new System.Drawing.Size(337, 212);
			this.shortcutKeysList.TabIndex = 0;
			this.shortcutKeysList.UseCompatibleStateImageBehavior = false;
			this.shortcutKeysList.View = System.Windows.Forms.View.Details;
			this.shortcutKeysList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.shortcutKeysList_MouseDoubleClick);
			// 
			// SourceColumn
			// 
			this.SourceColumn.Text = "入力元";
			this.SourceColumn.Width = 89;
			// 
			// KeyColumn
			// 
			this.KeyColumn.Text = "ショートカットキーの組み合わせ";
			this.KeyColumn.Width = 206;
			// 
			// ValuneColumn
			// 
			this.ValuneColumn.Text = "アクション内容";
			this.ValuneColumn.Width = 161;
			// 
			// ShortcutKey
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox1);
			this.Name = "ShortcutKey";
			this.Size = new System.Drawing.Size(350, 417);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ListView shortcutKeysList;
		private System.Windows.Forms.ColumnHeader KeyColumn;
		private System.Windows.Forms.ColumnHeader ValuneColumn;
		private System.Windows.Forms.Button EditShortcutKeyButton;
		private System.Windows.Forms.Button DelShortcutKeyButton;
		private System.Windows.Forms.Button AddShortcutKeyButton;
		private System.Windows.Forms.ColumnHeader SourceColumn;
	}
}
