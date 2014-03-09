namespace Shrimp.ControlParts.Popup
{
    partial class TabNameChange
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
            this.NameBox = new System.Windows.Forms.TextBox();
            this.NameOKButton = new System.Windows.Forms.Button();
            this.NameCancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // NameBox
            // 
            this.NameBox.Location = new System.Drawing.Point(13, 13);
            this.NameBox.Name = "NameBox";
            this.NameBox.Size = new System.Drawing.Size(282, 19);
            this.NameBox.TabIndex = 0;
            this.NameBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NameBox_KeyDown);
            // 
            // NameOKButton
            // 
            this.NameOKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.NameOKButton.Location = new System.Drawing.Point(130, 45);
            this.NameOKButton.Name = "NameOKButton";
            this.NameOKButton.Size = new System.Drawing.Size(75, 23);
            this.NameOKButton.TabIndex = 1;
            this.NameOKButton.Text = "OK";
            this.NameOKButton.UseVisualStyleBackColor = true;
            this.NameOKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // NameCancelButton
            // 
            this.NameCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.NameCancelButton.Location = new System.Drawing.Point(220, 45);
            this.NameCancelButton.Name = "NameCancelButton";
            this.NameCancelButton.Size = new System.Drawing.Size(75, 23);
            this.NameCancelButton.TabIndex = 2;
            this.NameCancelButton.Text = "キャンセル";
            this.NameCancelButton.UseVisualStyleBackColor = true;
            this.NameCancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // TabNameChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(307, 75);
            this.Controls.Add(this.NameCancelButton);
            this.Controls.Add(this.NameOKButton);
            this.Controls.Add(this.NameBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "TabNameChange";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "タブ名変更";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox NameBox;
        private System.Windows.Forms.Button NameOKButton;
        private System.Windows.Forms.Button NameCancelButton;
    }
}