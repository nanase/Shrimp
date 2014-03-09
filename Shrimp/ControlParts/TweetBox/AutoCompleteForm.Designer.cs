namespace Shrimp.ControlParts.TweetBox
{
    partial class AutoCompleteForm
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
			this.acfBox = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// acfBox
			// 
			this.acfBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.acfBox.FormattingEnabled = true;
			this.acfBox.ItemHeight = 12;
			this.acfBox.Location = new System.Drawing.Point(0, 0);
			this.acfBox.Name = "acfBox";
			this.acfBox.Size = new System.Drawing.Size(231, 186);
			this.acfBox.Sorted = true;
			this.acfBox.TabIndex = 0;
			this.acfBox.DoubleClick += new System.EventHandler(this.acfBox_DoubleClick);
			// 
			// AutoCompleteForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(231, 186);
			this.Controls.Add(this.acfBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AutoCompleteForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "AutoCompleteForm";
			this.TopMost = true;
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox acfBox;
    }
}