namespace Shrimp.ControlParts.Popup
{
    partial class TabSound
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
            this.SoundBox = new System.Windows.Forms.TextBox();
            this.SoundOKButton = new System.Windows.Forms.Button();
            this.SoundCancelButton = new System.Windows.Forms.Button();
            this.OpenSoundButton = new System.Windows.Forms.Button();
            this.soundFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.PlaySoundButton = new System.Windows.Forms.Button();
            this.delPathButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SoundBox
            // 
            this.SoundBox.Location = new System.Drawing.Point(13, 13);
            this.SoundBox.Name = "SoundBox";
            this.SoundBox.ReadOnly = true;
            this.SoundBox.Size = new System.Drawing.Size(192, 19);
            this.SoundBox.TabIndex = 0;
            this.SoundBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NameBox_KeyDown);
            // 
            // SoundOKButton
            // 
            this.SoundOKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.SoundOKButton.Location = new System.Drawing.Point(151, 45);
            this.SoundOKButton.Name = "SoundOKButton";
            this.SoundOKButton.Size = new System.Drawing.Size(75, 23);
            this.SoundOKButton.TabIndex = 1;
            this.SoundOKButton.Text = "OK";
            this.SoundOKButton.UseVisualStyleBackColor = true;
            this.SoundOKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // SoundCancelButton
            // 
            this.SoundCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.SoundCancelButton.Location = new System.Drawing.Point(241, 45);
            this.SoundCancelButton.Name = "SoundCancelButton";
            this.SoundCancelButton.Size = new System.Drawing.Size(75, 23);
            this.SoundCancelButton.TabIndex = 2;
            this.SoundCancelButton.Text = "キャンセル";
            this.SoundCancelButton.UseVisualStyleBackColor = true;
            this.SoundCancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // OpenSoundButton
            // 
            this.OpenSoundButton.Location = new System.Drawing.Point(220, 11);
            this.OpenSoundButton.Name = "OpenSoundButton";
            this.OpenSoundButton.Size = new System.Drawing.Size(43, 23);
            this.OpenSoundButton.TabIndex = 3;
            this.OpenSoundButton.Text = "...";
            this.OpenSoundButton.UseVisualStyleBackColor = true;
            this.OpenSoundButton.Click += new System.EventHandler(this.OpenSoundButton_Click);
            // 
            // soundFileDialog
            // 
            this.soundFileDialog.FileName = "WAVEファイル(*.wav)|*.wav|すべてのファイル(*.*)|*.*";
            // 
            // PlaySoundButton
            // 
            this.PlaySoundButton.Location = new System.Drawing.Point(13, 45);
            this.PlaySoundButton.Name = "PlaySoundButton";
            this.PlaySoundButton.Size = new System.Drawing.Size(56, 23);
            this.PlaySoundButton.TabIndex = 4;
            this.PlaySoundButton.Text = "再生";
            this.PlaySoundButton.UseVisualStyleBackColor = true;
            this.PlaySoundButton.Click += new System.EventHandler(this.PlaySoundButton_Click);
            // 
            // delPathButton
            // 
            this.delPathButton.Location = new System.Drawing.Point(273, 11);
            this.delPathButton.Name = "delPathButton";
            this.delPathButton.Size = new System.Drawing.Size(43, 23);
            this.delPathButton.TabIndex = 5;
            this.delPathButton.Text = "削除";
            this.delPathButton.UseVisualStyleBackColor = true;
            this.delPathButton.Click += new System.EventHandler(this.delPathButton_Click);
            // 
            // TabSound
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 75);
            this.Controls.Add(this.delPathButton);
            this.Controls.Add(this.PlaySoundButton);
            this.Controls.Add(this.OpenSoundButton);
            this.Controls.Add(this.SoundCancelButton);
            this.Controls.Add(this.SoundOKButton);
            this.Controls.Add(this.SoundBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "TabSound";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "通知音の設定";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox SoundBox;
        private System.Windows.Forms.Button SoundOKButton;
        private System.Windows.Forms.Button SoundCancelButton;
        private System.Windows.Forms.Button OpenSoundButton;
        private System.Windows.Forms.OpenFileDialog soundFileDialog;
        private System.Windows.Forms.Button PlaySoundButton;
        private System.Windows.Forms.Button delPathButton;
    }
}