namespace Shrimp.Setting.Forms
{
    partial class NewColor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose ( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose ();
            }
            base.Dispose ( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent ()
        {
            this.colorName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.EOKButton = new System.Windows.Forms.Button();
            this.ECancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // colorName
            // 
            this.colorName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.colorName.Location = new System.Drawing.Point(12, 24);
            this.colorName.Name = "colorName";
            this.colorName.Size = new System.Drawing.Size(260, 19);
            this.colorName.TabIndex = 0;
            this.colorName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.colorName_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "色設定の名前を決めてください";
            // 
            // EOKButton
            // 
            this.EOKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.EOKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.EOKButton.Location = new System.Drawing.Point(96, 49);
            this.EOKButton.Name = "EOKButton";
            this.EOKButton.Size = new System.Drawing.Size(80, 23);
            this.EOKButton.TabIndex = 2;
            this.EOKButton.Text = "OK";
            this.EOKButton.UseVisualStyleBackColor = true;
            this.EOKButton.Click += new System.EventHandler(this.EOKButton_Click);
            // 
            // ECancelButton
            // 
            this.ECancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ECancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ECancelButton.Location = new System.Drawing.Point(197, 49);
            this.ECancelButton.Name = "ECancelButton";
            this.ECancelButton.Size = new System.Drawing.Size(75, 23);
            this.ECancelButton.TabIndex = 3;
            this.ECancelButton.Text = "キャンセル";
            this.ECancelButton.UseVisualStyleBackColor = true;
            // 
            // NewColor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 83);
            this.Controls.Add(this.ECancelButton);
            this.Controls.Add(this.EOKButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.colorName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "NewColor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "カラーの作成";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NewColor_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox colorName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button EOKButton;
        private System.Windows.Forms.Button ECancelButton;
    }
}