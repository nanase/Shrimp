namespace Shrimp.Setting.Forms
{
    partial class TabSetting
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.TabSettingCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.TabAlignmentSelect = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TabAnimationSelect = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(435, 460);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "タブの設定";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.TabSettingCheckedListBox);
            this.groupBox4.Location = new System.Drawing.Point(6, 124);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(423, 54);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "タブの詳細設定";
            // 
            // TabSettingCheckedListBox
            // 
            this.TabSettingCheckedListBox.BackColor = System.Drawing.SystemColors.Control;
            this.TabSettingCheckedListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TabSettingCheckedListBox.CheckOnClick = true;
            this.TabSettingCheckedListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabSettingCheckedListBox.FormattingEnabled = true;
            this.TabSettingCheckedListBox.Items.AddRange(new object[] {
            "タブを作ったとき、そのタブにフォーカスを当てる",
            "タブを改行表示にする"});
            this.TabSettingCheckedListBox.Location = new System.Drawing.Point(3, 15);
            this.TabSettingCheckedListBox.Name = "TabSettingCheckedListBox";
            this.TabSettingCheckedListBox.Size = new System.Drawing.Size(417, 36);
            this.TabSettingCheckedListBox.TabIndex = 0;
            this.TabSettingCheckedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.TabSettingCheckedListBox_ItemCheck);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.TabAlignmentSelect);
            this.groupBox3.Location = new System.Drawing.Point(6, 70);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(423, 48);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "タブの向き";
            // 
            // TabAlignmentSelect
            // 
            this.TabAlignmentSelect.DisplayMember = "0";
            this.TabAlignmentSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabAlignmentSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TabAlignmentSelect.FormattingEnabled = true;
            this.TabAlignmentSelect.Items.AddRange(new object[] {
            "上",
            "下",
            "左",
            "右"});
            this.TabAlignmentSelect.Location = new System.Drawing.Point(3, 15);
            this.TabAlignmentSelect.Name = "TabAlignmentSelect";
            this.TabAlignmentSelect.Size = new System.Drawing.Size(417, 20);
            this.TabAlignmentSelect.TabIndex = 0;
            this.TabAlignmentSelect.SelectedIndexChanged += new System.EventHandler(this.TabAnimationSelect_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.TabAnimationSelect);
            this.groupBox2.Location = new System.Drawing.Point(6, 16);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(423, 48);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "タブの切り替えアニメーション設定(試験的実装)";
            // 
            // TabAnimationSelect
            // 
            this.TabAnimationSelect.DisplayMember = "0";
            this.TabAnimationSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabAnimationSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TabAnimationSelect.FormattingEnabled = true;
            this.TabAnimationSelect.Items.AddRange(new object[] {
            "なし",
            "スライド",
            "フェード"});
            this.TabAnimationSelect.Location = new System.Drawing.Point(3, 15);
            this.TabAnimationSelect.Name = "TabAnimationSelect";
            this.TabAnimationSelect.Size = new System.Drawing.Size(417, 20);
            this.TabAnimationSelect.TabIndex = 0;
            this.TabAnimationSelect.SelectedIndexChanged += new System.EventHandler(this.TabAnimationSelect_SelectedIndexChanged);
            // 
            // TabSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "TabSetting";
            this.Size = new System.Drawing.Size(435, 460);
            this.groupBox1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox TabAnimationSelect;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox TabAlignmentSelect;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.CheckedListBox TabSettingCheckedListBox;
    }
}
