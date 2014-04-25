namespace Shrimp.Setting.Forms
{
	partial class ShortcutKeyEdit
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.label1 = new System.Windows.Forms.Label();
            this.sourceBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.actionKeyBox = new System.Windows.Forms.TextBox();
            this.actionBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.EOKButton = new System.Windows.Forms.Button();
            this.ECancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "1. 入力元を選択してください";
            // 
            // sourceBox
            // 
            this.sourceBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sourceBox.FormattingEnabled = true;
            this.sourceBox.Items.AddRange(new object[] {
            "ダブルクリック",
            "キーボード"});
            this.sourceBox.Location = new System.Drawing.Point(12, 24);
            this.sourceBox.Name = "sourceBox";
            this.sourceBox.Size = new System.Drawing.Size(306, 20);
            this.sourceBox.TabIndex = 1;
            this.sourceBox.SelectedIndexChanged += new System.EventHandler(this.sourceBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(190, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "2. アクションを起こすキーを押してください";
            // 
            // actionKeyBox
            // 
            this.actionKeyBox.Location = new System.Drawing.Point(12, 75);
            this.actionKeyBox.Name = "actionKeyBox";
            this.actionKeyBox.ShortcutsEnabled = false;
            this.actionKeyBox.Size = new System.Drawing.Size(306, 19);
            this.actionKeyBox.TabIndex = 3;
            this.actionKeyBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.actionKeyBox_KeyDown);
            // 
            // actionBox
            // 
            this.actionBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.actionBox.FormattingEnabled = true;
            this.actionBox.Items.AddRange(new object[] {
            "なし",
            "リプライ",
            "リツイート",
            "お気に入りに追加",
            "ユーザのタイムラインを開く",
            "ユーザ情報を開く",
            "ユーザのお気に入りタイムラインを開く",
            "ユーザの会話タイムラインを開く",
            "ツイート入力欄にフォーカスを移す"});
            this.actionBox.Location = new System.Drawing.Point(12, 123);
            this.actionBox.Name = "actionBox";
            this.actionBox.Size = new System.Drawing.Size(306, 20);
            this.actionBox.TabIndex = 5;
            this.actionBox.SelectedIndexChanged += new System.EventHandler(this.actionBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(143, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "3. アクションを選択してください";
            // 
            // EOKButton
            // 
            this.EOKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.EOKButton.Location = new System.Drawing.Point(152, 164);
            this.EOKButton.Name = "EOKButton";
            this.EOKButton.Size = new System.Drawing.Size(75, 23);
            this.EOKButton.TabIndex = 6;
            this.EOKButton.Text = "OK";
            this.EOKButton.UseVisualStyleBackColor = true;
            this.EOKButton.Click += new System.EventHandler(this.EOKButton_Click);
            // 
            // ECancelButton
            // 
            this.ECancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ECancelButton.Location = new System.Drawing.Point(243, 164);
            this.ECancelButton.Name = "ECancelButton";
            this.ECancelButton.Size = new System.Drawing.Size(75, 23);
            this.ECancelButton.TabIndex = 7;
            this.ECancelButton.Text = "キャンセル";
            this.ECancelButton.UseVisualStyleBackColor = true;
            this.ECancelButton.Click += new System.EventHandler(this.ECancelButton_Click);
            // 
            // ShortcutKeyEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 199);
            this.Controls.Add(this.ECancelButton);
            this.Controls.Add(this.EOKButton);
            this.Controls.Add(this.actionBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.actionKeyBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.sourceBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ShortcutKeyEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ショートカットキーの設定";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ShortcutKeyEdit_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox sourceBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox actionKeyBox;
		private System.Windows.Forms.ComboBox actionBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button EOKButton;
		private System.Windows.Forms.Button ECancelButton;
	}
}