using System;
using System.Drawing;
using System.Windows.Forms;
using Shrimp.ControlParts.Timeline.Draw;
using Shrimp.Twitter.Status;
using System.Collections.Generic;

namespace Shrimp.Setting.Forms
{
    public partial class FontForm : UserControl, ISettingForm, IDisposable
    {
        private TweetDraw draw = new TweetDraw ();
        private Dictionary<string, string> Settings;

        public FontForm ()
        {
            InitializeComponent ();
            Settings = Setting.Fonts.save ();
            SettingReflection ();
            DrawPreview ();
        }

        /// <summary>
        /// リプライソース描画
        /// </summary>
        /// <param name="tweet"></param>
        private void DrawPreview ()
        {
            TwitterStatus status = new TwitterStatus ();
            status.user = new TwitterUserStatus ();
            status.user.screen_name = "Shrimp";
            status.user.name = "小エビ";
            status.created_at = DateTime.Now;
            status.text = "このテキストはプレビューです。http://hogehoge.com";
            status.entities = new Twitter.Entities.TwitterEntities ( status.text );
            status.source = "Shrimp";

            TwitterStatus op = new TwitterStatus ();
            op.retweeted_status = status;
            op.created_at = DateTime.Now;
            op.user = new TwitterUserStatus ();
            op.user.screen_name = "Shrimp";
            op.user.name = "小エビマン";

            draw.initialize ();
            draw.StartLayout ( op, false, 0, this.Width );
            draw.EndLayout ();
            var height = draw.get ( 0 ).CellSizeWithoutPadding;
            if ( this.PreviewBox.Image == null )
            {
                var bmp = new Bitmap ( this.Width, height );
                this.PreviewBox.Image = bmp;
            }
            else
            {
                this.PreviewBox.Image.Dispose ();
                var bmp = new Bitmap ( this.Width, height );
                this.PreviewBox.Image = bmp;
            }
            using ( Graphics g = Graphics.FromImage ( this.PreviewBox.Image ) )
            {
                draw.initialize ();
                draw.DrawTweet ( g, op, false, 0, this.PreviewBox.Width, null, null, new Point (), Setting.Colors.BackgroundColor );
            }
        }

        public void SettingReflection ()
        {
            this.NameFontButton.Tag = Setting.Fonts.FontConverter ( Settings["NameFont"] );
            this.TweetFontButton.Tag = Setting.Fonts.FontConverter ( Settings["TweetFont"] );
            this.ViaFontButton.Tag = Setting.Fonts.FontConverter ( Settings["ViaFont"] );
            this.RetweetNotifyButton.Tag = Setting.Fonts.FontConverter ( Settings["RetweetNotify"] );
        }

        public void SaveReflection ()
        {
            Setting.Fonts.load ( this.Settings );
            DrawPreview ();
        }

        private void FontSelect ( object sender, EventArgs e )
        {
            Button obj = sender as Button;
            fontSelectDialog.Font = (Font)obj.Tag;
            if ( fontSelectDialog.ShowDialog () == DialogResult.OK )
            {
                //  ok
                this.ChangeFont ( obj.Name, fontSelectDialog.Font );
                obj.Tag = fontSelectDialog.Font;
                SettingReflection ();
                SaveReflection ();
            }
        }

        private void ChangeFont ( string name, Font font )
        {
            if ( name == "NameFontButton" )
            {
                this.Settings["NameFont"] = Setting.Fonts.FontConverter ( font );
            }
            if ( name == "TweetFontButton" )
            {
                this.Settings["TweetFont"] = Setting.Fonts.FontConverter ( font );
            }
            if ( name == "ViaFontButton" )
            {
                this.Settings["ViaFont"] = Setting.Fonts.FontConverter ( font );
            }
            if ( name == "RetweetNotifyButton" )
            {
                this.Settings["RetweetNotify"] = Setting.Fonts.FontConverter ( font );
            }
        }
    }
}
