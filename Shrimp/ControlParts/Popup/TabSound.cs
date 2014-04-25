using System;
using System.Windows.Forms;
using System.Media;
using Shrimp.Module.FormUtil;

namespace Shrimp.ControlParts.Popup
{
    public partial class TabSound : Form
    {
        private string _path = "";
        private SoundPlayer sound;

        public TabSound ( string path )
        {
            InitializeComponent();
            _path = path;
            this.SoundBox.Text = _path;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            this.Tag = (string)this.SoundBox.Text.Clone();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void NameBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        private void OpenSoundButton_Click ( object sender, EventArgs e )
        {
            this.soundFileDialog.FileName = _path;
            if ( this.soundFileDialog.ShowDialog () == DialogResult.OK )
            {
                _path = this.soundFileDialog.FileName;
                this.SoundBox.Text = _path;
            }
        }

        private void PlaySoundButton_Click ( object sender, EventArgs e )
        {
            //if ( this.sound != null )
            //    this.sound.Dispose ();
            using ( this.sound = new SoundPlayer ( this._path ) )
            {
                try
                {
                    this.sound = new SoundPlayer ( this._path );
                    this.sound.Play ();
                }
                catch ( Exception err )
                {
                    MessageBoxEX.ShowErrorMessageBox ( "音声を再生できませんでした。\nPath:" + this._path + "\n" + err.StackTrace + "" );
                }
            }
        }

        private void delPathButton_Click ( object sender, EventArgs e )
        {
            _path = "";
            this.SoundBox.Text = "";
        }

    }
}
