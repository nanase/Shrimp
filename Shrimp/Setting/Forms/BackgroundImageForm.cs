using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Shrimp.Setting.Forms
{
    public partial class BackgroundImageForm : UserControl, ISettingForm, IDisposable
    {
        private Dictionary<string, object> setting;

        public BackgroundImageForm()
        {
            InitializeComponent();
            setting = Setting.BackgroundImage.save ();
            SettingReflection();
        }

        public void SettingReflection()
        {
            ImagePositionBox.SelectedIndex = (int)setting["ImagePos"];
            BackgroundImagePathBox.Text = (string)setting["BackgroundImagePath"];
            BackgroundImageTransparentBar.Value = (int)setting["BackgroundTransparent"];
        }

        public void SaveReflection()
        {
            Setting.BackgroundImage.load(setting);
        }

        private void ImagePositionBox_SelectedIndexChanged ( object sender, EventArgs e )
        {
            ComboBox box = sender as ComboBox;
            setting["ImagePos"] = box.SelectedIndex;
            SaveReflection ();
        }

        private void FileDialogButton_Click ( object sender, EventArgs e )
        {
            if ( this.ImageFileDialog.ShowDialog () == DialogResult.OK )
            {
                setting["BackgroundImagePath"] = this.ImageFileDialog.FileName;
                BackgroundImagePathBox.Text = this.ImageFileDialog.FileName;
                SaveReflection ();
            }
        }

        private void DeleteBackgroundImageButton_Click ( object sender, EventArgs e )
        {
            setting["BackgroundImagePath"] = "";
            BackgroundImagePathBox.Text = "";
            SaveReflection ();
        }

        private void BackgroundImageTransparentBar_Scroll ( object sender, EventArgs e )
        {
            TrackBar bar = sender as TrackBar;
            setting["BackgroundTransparent"] = bar.Value;
            SaveReflection ();
        }
    }
}
