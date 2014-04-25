using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Shrimp.ControlParts.Timeline.Draw;
using Shrimp.Module;
using Shrimp.Setting.ObjectXML;
using Shrimp.Twitter.Status;

namespace Shrimp.Setting.Forms
{
    public partial class TimelineColor : UserControl, ISettingForm, IDisposable
    {
        private Dictionary<string, BrushEX> tlcolors;
        private TweetDraw draw = new TweetDraw();
        private Brush background = Setting.Colors.BackgroundColor;
        private string[] colorPaths;
        public TimelineColor()
        {
            InitializeComponent();

            this.ColorComboBox.SelectedIndex = GetColors (); ;
            tlcolors = Setting.Colors.save();
            SettingReflection();
        }

        private int GetColors()
        {
            int res = -1;
            int i = 0;
            //  読み込み
            this.colorPaths = Directory.GetFiles(ShrimpSettings.ColorsDirectory, "*.shc");
            if (this.colorPaths.Length == 0)
            {
                Setting.Colors.initialize();
                SettingSerializer.SaveColor();
                this.colorPaths = Directory.GetFiles(ShrimpSettings.ColorsDirectory, "*.shc");
            }
            foreach (string colord in colorPaths)
            {
                this.ColorComboBox.Items.Add(Path.GetFileNameWithoutExtension(colord));
                if ( Path.GetFileNameWithoutExtension ( colord ) == Setting.Colors.ProfileName )
                    res = i;
                i++;
            }
            return res;
        }

        public void SettingReflection()
        {
            this.namePicture.Image = SettingUtils.CreateImageColor(tlcolors["NameColor"].Generate);
            this.textPicutre.Image = SettingUtils.CreateImageColor(tlcolors["TweetColor"].Generate);
            this.viaPicture.Image = SettingUtils.CreateImageColor(tlcolors["ViaColor"].Generate);
            this.linkPicture.Image = SettingUtils.CreateImageColor(tlcolors["LinkColor"].Generate);
            this.NormalBackgroundPicture.Image = SettingUtils.CreateImageColor(tlcolors["BackgroundColor"].Generate);
            this.SelectingPicture.Image = SettingUtils.CreateImageColor(tlcolors["SelectBackgroundColor"].Generate);
            this.ReplyPicture.Image = SettingUtils.CreateImageColor(tlcolors["ReplyBackgroundColor"].Generate);
            this.RetweetPicture.Image = SettingUtils.CreateImageColor(tlcolors["RetweetBackgroundColor"].Generate);
            this.OwnTweetPicture.Image = SettingUtils.CreateImageColor(tlcolors["OwnTweetBackgroundColor"].Generate);
            this.NotifyPicture.Image = SettingUtils.CreateImageColor(tlcolors["NotifyTweetBackgroundColor"].Generate);
            this.DirectMessagePicture.Image = SettingUtils.CreateImageColor(tlcolors["DirectMessageBackgroundColor"].Generate);
            DrawPreview();
        }

        public void SaveReflection()
        {
            Setting.Colors.load(tlcolors);
            SettingSerializer.SaveColor();
        }

        private void SelectColorButtonClicked(object sender, EventArgs e)
        {
            Button sender_button = sender as Button;
            var f = RefColor(sender_button.Name);
            this.SelectColorDialog.Color = f;
            if (this.SelectColorDialog.ShowDialog() == DialogResult.OK)
            {
                ColorReflection(sender_button.Name, new SolidBrush(this.SelectColorDialog.Color));
                SaveReflection();
                DrawPreview();
            }
        }

        private Color RefColor(string objName)
        {
            if (objName == "name")
            {
                return tlcolors["NameColor"].GenerateColor;
            }
            if (objName == "text")
            {
                return tlcolors["TweetColor"].GenerateColor;
            }
            if (objName == "via")
            {
                return tlcolors["ViaColor"].GenerateColor;
            }
            if (objName == "link")
            {
                return tlcolors["LinkColor"].GenerateColor;
            }
            if (objName == "Normal")
            {
                return tlcolors["BackgroundColor"].GenerateColor;
            }
            if (objName == "SelectingButton")
            {
                return tlcolors["SelectBackgroundColor"].GenerateColor;
            }
            if (objName == "Reply")
            {
                return tlcolors["ReplyBackgroundColor"].GenerateColor;
            }
            if (objName == "Retweet")
            {
                return tlcolors["RetweetBackgroundColor"].GenerateColor;
            }
            if (objName == "OwnTweet")
            {
                return tlcolors["OwnTweetBackgroundColor"].GenerateColor;
            }
            if (objName == "Notify")
            {
                return tlcolors["NotifyTweetBackgroundColor"].GenerateColor;
            }
            if (objName == "DirectMessage")
            {
                return tlcolors["DirectMessageBackgroundColor"].GenerateColor;
            }
            return Color.White;
        }


        private void ColorReflection(string objName, Brush cl)
        {
            if (objName == "name")
            {
                if (this.namePicture.Image != null)
                    this.namePicture.Image.Dispose();
                this.namePicture.Image = SettingUtils.CreateImageColor(cl);
                tlcolors["NameColor"] = new BrushEX(cl);
            }
            if (objName == "text")
            {
                if (this.textPicutre.Image != null)
                    this.textPicutre.Image.Dispose();
                this.textPicutre.Image = SettingUtils.CreateImageColor(cl);
                tlcolors["TweetColor"] = new BrushEX(cl);
            }
            if (objName == "via")
            {
                if (this.viaPicture.Image != null)
                    this.viaPicture.Image.Dispose();
                this.viaPicture.Image = SettingUtils.CreateImageColor(cl);
                tlcolors["ViaColor"] = new BrushEX(cl);
            }
            if (objName == "link")
            {
                if (this.linkPicture.Image != null)
                    this.linkPicture.Image.Dispose();
                this.linkPicture.Image = SettingUtils.CreateImageColor(cl);
                tlcolors["LinkColor"] = new BrushEX(cl);
            }
            if (objName == "Normal")
            {
                if (this.NormalBackgroundPicture.Image != null)
                    this.NormalBackgroundPicture.Image.Dispose();
                this.NormalBackgroundPicture.Image = SettingUtils.CreateImageColor(cl);
                tlcolors["BackgroundColor"] = new BrushEX(cl);
            }
            if (objName == "SelectingButton")
            {
                if (this.SelectingPicture.Image != null)
                    this.SelectingPicture.Image.Dispose();
                this.SelectingPicture.Image = SettingUtils.CreateImageColor(cl);
                tlcolors["SelectBackgroundColor"] = new BrushEX(cl);
            }
            if (objName == "Reply")
            {
                if (this.ReplyPicture.Image != null)
                    this.ReplyPicture.Image.Dispose();
                this.ReplyPicture.Image = SettingUtils.CreateImageColor(cl);
                tlcolors["ReplyBackgroundColor"] = new BrushEX(cl);
            }
            if (objName == "Retweet")
            {
                if (this.RetweetPicture.Image != null)
                    this.RetweetPicture.Image.Dispose();
                this.RetweetPicture.Image = SettingUtils.CreateImageColor(cl);
                tlcolors["RetweetBackgroundColor"] = new BrushEX(cl);
            }
            if (objName == "OwnTweet")
            {
                if (this.OwnTweetPicture.Image != null)
                    this.OwnTweetPicture.Image.Dispose();
                this.OwnTweetPicture.Image = SettingUtils.CreateImageColor(cl);
                tlcolors["OwnTweetBackgroundColor"] = new BrushEX(cl);
            }
            if (objName == "Notify")
            {
                if (this.NotifyPicture.Image != null)
                    this.NotifyPicture.Image.Dispose();
                this.NotifyPicture.Image = SettingUtils.CreateImageColor(cl);
                tlcolors["NotifyTweetBackgroundColor"] = new BrushEX(cl);
            }
            if (objName == "DirectMessage")
            {
                if (this.DirectMessagePicture.Image != null)
                    this.DirectMessagePicture.Image.Dispose();
                this.DirectMessagePicture.Image = SettingUtils.CreateImageColor(cl);
                tlcolors["DirectMessageBackgroundColor"] = new BrushEX(cl);
            }
        }


        /// <summary>
        /// リプライソース描画
        /// </summary>
        /// <param name="tweet"></param>
        private void DrawPreview()
        {
            TwitterStatus status = new TwitterStatus();
            status.user = new TwitterUserStatus();
            status.user.screen_name = "Shrimp";
            status.user.name = "小エビ";
            status.created_at = DateTime.Now;
            status.text = "このテキストはプレビューです。http://hogehoge.com";
            status.entities = new Twitter.Entities.TwitterEntities(status.text);
            status.source = "Shrimp";

            draw.initialize();
            draw.StartLayout(status, false, 0, this.Width);
            draw.EndLayout();
            var height = draw.get(0).CellSizeWithoutPadding;
            if (this.PreviewBox.Image == null)
            {
                var bmp = new Bitmap(this.Width, height);
                this.PreviewBox.Image = bmp;
            }
            else
            {
                this.PreviewBox.Image.Dispose();
                var bmp = new Bitmap(this.Width, height);
                this.PreviewBox.Image = bmp;
            }
            using (Graphics g = Graphics.FromImage(this.PreviewBox.Image))
            {
                draw.initialize();
                draw.DrawTweet(g, status, false, 0, this.PreviewBox.Width, null, null, new Point(), background);
            }
        }

        private void NormalBackgroundPicture_Click(object sender, EventArgs e)
        {
            PictureBox pic = sender as PictureBox;
            background = new SolidBrush((((Bitmap)(pic.Image)).GetPixel(1, 1)));
            this.DrawPreview();
        }

        private void ColorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var box = sender as ComboBox;
            var selectedIndex = box.SelectedIndex;
            if (selectedIndex < 0)
                return;

            SettingSerializer.LoadColor(colorPaths[selectedIndex]);
            tlcolors = Setting.Colors.save();
            SettingReflection();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            NewColor c = new NewColor();
            if (c.ShowDialog() == DialogResult.OK)
            {
                Setting.Colors.initialize();
                Setting.Colors.ProfileName = ((string)c.Tag == null ? "Default" : (string)c.Tag);
                SettingSerializer.SaveColor();
                this.ColorComboBox.Items.Clear();
                GetColors();
                this.ColorComboBox.SelectedIndex = this.ColorComboBox.Items.Count - 1;
            }
        }
    }
}
