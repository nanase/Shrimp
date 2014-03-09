namespace Shrimp.ControlParts.TabSetting
{
    partial class DeliverFromUser
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
            this.upbutton = new System.Windows.Forms.Button();
            this.downbutton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.registUsers = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.allUserCheckBox = new System.Windows.Forms.CheckBox();
            this.deliveryUsers = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.upbutton);
            this.groupBox1.Controls.Add(this.downbutton);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(640, 480);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "振り分け元ユーザーの指定";
            // 
            // upbutton
            // 
            this.upbutton.Location = new System.Drawing.Point(145, 215);
            this.upbutton.Name = "upbutton";
            this.upbutton.Size = new System.Drawing.Size(38, 34);
            this.upbutton.TabIndex = 3;
            this.upbutton.Text = "↑";
            this.upbutton.UseVisualStyleBackColor = true;
            this.upbutton.Click += new System.EventHandler(this.upbutton_Click);
            // 
            // downbutton
            // 
            this.downbutton.Location = new System.Drawing.Point(226, 215);
            this.downbutton.Name = "downbutton";
            this.downbutton.Size = new System.Drawing.Size(38, 34);
            this.downbutton.TabIndex = 2;
            this.downbutton.Text = "↓";
            this.downbutton.UseVisualStyleBackColor = true;
            this.downbutton.Click += new System.EventHandler(this.downbutton_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.registUsers);
            this.groupBox3.Location = new System.Drawing.Point(7, 271);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(627, 194);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "除外する登録ユーザ一覧";
            // 
            // registUsers
            // 
            this.registUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.registUsers.FormattingEnabled = true;
            this.registUsers.ItemHeight = 12;
            this.registUsers.Location = new System.Drawing.Point(3, 15);
            this.registUsers.Name = "registUsers";
            this.registUsers.Size = new System.Drawing.Size(621, 176);
            this.registUsers.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.splitContainer1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(3, 15);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(634, 194);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "このタブに振り分けられるユーザ一覧";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(3, 15);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.allUserCheckBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.deliveryUsers);
            this.splitContainer1.Size = new System.Drawing.Size(628, 176);
            this.splitContainer1.SplitterDistance = 25;
            this.splitContainer1.TabIndex = 2;
            // 
            // allUserCheckBox
            // 
            this.allUserCheckBox.AutoSize = true;
            this.allUserCheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.allUserCheckBox.Location = new System.Drawing.Point(0, 0);
            this.allUserCheckBox.Name = "allUserCheckBox";
            this.allUserCheckBox.Size = new System.Drawing.Size(628, 25);
            this.allUserCheckBox.TabIndex = 1;
            this.allUserCheckBox.Text = "登録してあるアカウントすべてを適用する";
            this.allUserCheckBox.UseVisualStyleBackColor = true;
            this.allUserCheckBox.CheckedChanged += new System.EventHandler(this.allUserCheckBox_CheckedChanged);
            // 
            // deliveryUsers
            // 
            this.deliveryUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deliveryUsers.FormattingEnabled = true;
            this.deliveryUsers.ItemHeight = 12;
            this.deliveryUsers.Location = new System.Drawing.Point(0, 0);
            this.deliveryUsers.Name = "deliveryUsers";
            this.deliveryUsers.Size = new System.Drawing.Size(628, 147);
            this.deliveryUsers.TabIndex = 0;
            // 
            // DeliverFromUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "DeliverFromUser";
            this.Size = new System.Drawing.Size(640, 480);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button upbutton;
        private System.Windows.Forms.Button downbutton;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox registUsers;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox deliveryUsers;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.CheckBox allUserCheckBox;
    }
}
