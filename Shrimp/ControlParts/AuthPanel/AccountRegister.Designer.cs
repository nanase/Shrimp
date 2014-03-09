namespace Shrimp.ControlParts.AuthPanel
{
    partial class AccountRegister
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
			this.accountURLLabel = new System.Windows.Forms.LinkLabel();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.Pinbox = new System.Windows.Forms.TextBox();
			this.AuthorizeButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// accountURLLabel
			// 
			this.accountURLLabel.Enabled = false;
			this.accountURLLabel.Location = new System.Drawing.Point(12, 49);
			this.accountURLLabel.Name = "accountURLLabel";
			this.accountURLLabel.Size = new System.Drawing.Size(380, 34);
			this.accountURLLabel.TabIndex = 0;
			this.accountURLLabel.TabStop = true;
			this.accountURLLabel.Text = "Now Loading...";
			this.accountURLLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.accountURLLabel_LinkClicked);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 27);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(248, 12);
			this.label1.TabIndex = 1;
			this.label1.Text = "1. 次のURLを開いて、アカウント認証を行ってください";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 95);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(339, 12);
			this.label2.TabIndex = 2;
			this.label2.Text = "2. 認証しましたら、表示されたPINコードを下のボックスに入力してください";
			// 
			// Pinbox
			// 
			this.Pinbox.Enabled = false;
			this.Pinbox.Location = new System.Drawing.Point(14, 121);
			this.Pinbox.Name = "Pinbox";
			this.Pinbox.Size = new System.Drawing.Size(326, 19);
			this.Pinbox.TabIndex = 3;
			this.Pinbox.TextChanged += new System.EventHandler(this.Pinbox_TextChanged);
			this.Pinbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Pinbox_KeyDown);
			// 
			// AuthorizeButton
			// 
			this.AuthorizeButton.Enabled = false;
			this.AuthorizeButton.Location = new System.Drawing.Point(117, 164);
			this.AuthorizeButton.Name = "AuthorizeButton";
			this.AuthorizeButton.Size = new System.Drawing.Size(143, 51);
			this.AuthorizeButton.TabIndex = 4;
			this.AuthorizeButton.Text = "認証する";
			this.AuthorizeButton.UseVisualStyleBackColor = true;
			this.AuthorizeButton.Click += new System.EventHandler(this.AuthorizeButton_Click);
			// 
			// AccountRegister
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(365, 227);
			this.Controls.Add(this.AuthorizeButton);
			this.Controls.Add(this.Pinbox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.accountURLLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "AccountRegister";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "アカウント追加";
			this.Load += new System.EventHandler(this.AccountRegister_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel accountURLLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Pinbox;
        private System.Windows.Forms.Button AuthorizeButton;
    }
}