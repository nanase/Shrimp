namespace Shrimp.ControlParts.Popup
{
    partial class SearchForm
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
            this.SearchCancelButton = new System.Windows.Forms.Button();
            this.SearchOKButton = new System.Windows.Forms.Button();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.ignoreRTBox = new System.Windows.Forms.CheckBox();
            this.onlyJapaneseBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // SearchCancelButton
            // 
            this.SearchCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.SearchCancelButton.Location = new System.Drawing.Point(237, 80);
            this.SearchCancelButton.Name = "SearchCancelButton";
            this.SearchCancelButton.Size = new System.Drawing.Size(75, 23);
            this.SearchCancelButton.TabIndex = 0;
            this.SearchCancelButton.Text = "キャンセル";
            this.SearchCancelButton.UseVisualStyleBackColor = true;
            this.SearchCancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // SearchOKButton
            // 
            this.SearchOKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.SearchOKButton.Location = new System.Drawing.Point(146, 80);
            this.SearchOKButton.Name = "SearchOKButton";
            this.SearchOKButton.Size = new System.Drawing.Size(75, 23);
            this.SearchOKButton.TabIndex = 1;
            this.SearchOKButton.Text = "検索";
            this.SearchOKButton.UseVisualStyleBackColor = true;
            this.SearchOKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // searchBox
            // 
            this.searchBox.Location = new System.Drawing.Point(12, 12);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(300, 19);
            this.searchBox.TabIndex = 2;
            this.searchBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchBox_KeyDown);
            // 
            // ignoreRTBox
            // 
            this.ignoreRTBox.AutoSize = true;
            this.ignoreRTBox.Location = new System.Drawing.Point(12, 37);
            this.ignoreRTBox.Name = "ignoreRTBox";
            this.ignoreRTBox.Size = new System.Drawing.Size(91, 16);
            this.ignoreRTBox.TabIndex = 3;
            this.ignoreRTBox.Text = "RTを除外する";
            this.ignoreRTBox.UseVisualStyleBackColor = true;
            this.ignoreRTBox.CheckedChanged += new System.EventHandler(this.onlyJapaneseBox_CheckedChanged);
            // 
            // onlyJapaneseBox
            // 
            this.onlyJapaneseBox.AutoSize = true;
            this.onlyJapaneseBox.Location = new System.Drawing.Point(12, 59);
            this.onlyJapaneseBox.Name = "onlyJapaneseBox";
            this.onlyJapaneseBox.Size = new System.Drawing.Size(133, 16);
            this.onlyJapaneseBox.TabIndex = 4;
            this.onlyJapaneseBox.Text = "日本語のみを検索する";
            this.onlyJapaneseBox.UseVisualStyleBackColor = true;
            this.onlyJapaneseBox.CheckedChanged += new System.EventHandler(this.onlyJapaneseBox_CheckedChanged);
            // 
            // SearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 115);
            this.Controls.Add(this.onlyJapaneseBox);
            this.Controls.Add(this.ignoreRTBox);
            this.Controls.Add(this.searchBox);
            this.Controls.Add(this.SearchOKButton);
            this.Controls.Add(this.SearchCancelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "SearchForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "検索";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchForm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SearchCancelButton;
        private System.Windows.Forms.Button SearchOKButton;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.CheckBox ignoreRTBox;
        private System.Windows.Forms.CheckBox onlyJapaneseBox;
    }
}