using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using Shrimp.ControlParts.ContextMenus.TweetBox;
using Shrimp.ControlParts.Timeline.Draw;
using Shrimp.Module.FormUtil;
using Shrimp.Module.ImageUtil;
using Shrimp.Plugin.Ref;
using Shrimp.Twitter.REST.Help;
using Shrimp.Twitter.Status;

namespace Shrimp.ControlParts.TweetBox
{
    public partial class TweetBoxControl : UserControl
    {
        #region 定義
        public event EventHandler SendButtonClicked;
        public event EventHandler DeleteTweetClicked;
        public event EventHandler ShrimpBeamClicked;
        public event EventHandler AccountImageDoubleClicked;
        private bool _enabled;
        private TwitterStatus replyStatus;
        private PictureBox replyImage = new PictureBox();
        private Graphics g;
        private TweetDraw draw = new TweetDraw();
        private TweetBoxContextMenu contextMenu;
        //  入力待ちの時（What are you doing?)
        private bool isWaitingBox = false;
        private int DefaultSplitSize;
        private int DefaultPanel2Size;
        private int ImageURLIncrease = 0;
        private bool isImageEnable = false;
        private string AttachImagePath = "";
        private ConfigStatus tmpConfigStatus = null;
        private int _BoxHeight = 0;
        //  AutoCompleteBox
        //private AutoCompleteWindow completeWindow = new AutoCompleteWindow ();
        public delegate object RetStatus();
        private byte[] imageBytes = null;
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TweetBoxControl()
        {
            InitializeComponent();
            replyImage = new PictureBox();
            //replyImage.Dock = DockStyle.Fill;
            this.MainSplit.Panel1.AutoScroll = true;
            this.replyImage.Width = this.Width;
            this.replyImage.SizeMode = PictureBoxSizeMode.AutoSize;
            this.replyImage.Location = new Point();
            this.tweetDeleteBox.BackColor = Color.FromArgb(0, 255, 0, 255);
            this.MainSplit.Panel1.Controls.Add(replyImage);
            this.DefaultSplitSize = this.MainSplit.SplitterDistance;
            this.DefaultPanel2Size = this.MainSplit.Panel2.Height;
            this.contextMenu = new TweetBoxContextMenu();
            //this.TweetBox.ShortcutsEnabled = false;
            this.TweetBox.ContextMenuStrip = this.contextMenu.contextMenu;
            this.TweetBox.ContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(ContextMenuStrip_Opening);
            this.TweetBox.ContextMenuStrip.ItemClicked += new ToolStripItemClickedEventHandler(ContextMenuStrip_ItemClicked);
            this._BoxHeight = this.MainSplit.SplitterDistance;
        }

        void ContextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            IDataObject data = Clipboard.GetDataObject();
            this.contextMenu.isEnabledClipboardImage = false;
            if (data != null)
            {
                if (data.GetDataPresent(typeof(Bitmap)))
                {
                    this.contextMenu.isEnabledClipboardImage = true;
                }
            }
        }

        void ContextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "RestoreMenu")
            {
                if (this.TweetBox.CanUndo)
                    this.TweetBox.Undo();
            }
            else if (e.ClickedItem.Name == "CutMenu")
            {
                this.TweetBox.Cut();
            }
            else if (e.ClickedItem.Name == "CopyMenu")
            {
                this.TweetBox.Copy();
            }
            else if (e.ClickedItem.Name == "PasteMenu")
            {
                this.TweetBox.Paste();
            }
            else if (e.ClickedItem.Name == "PasteImageMenu")
            {
                this.loadImage("", true);
                this.SelectImage(true, tmpConfigStatus);
                this.SelectTweetBox();
            }
            else if (e.ClickedItem.Name == "SelectAllMenu")
            {
                this.TweetBox.SelectAll();
            }
            else if (e.ClickedItem.Name == "DeleteMenu")
            {
                // 文字列を選択している場合は選択部分を削除
                if (this.TweetBox.SelectionLength > 0)
                {
                    this.TweetBox.Text = this.TweetBox.Text.Remove(
                    this.TweetBox.SelectionStart, this.TweetBox.SelectionLength);
                }
                // キャレットが先頭でなければ1文字削除
                if (this.TweetBox.SelectionStart > 0)
                {
                    // 1文字先頭の方へずらして、キャレット位置の1文字を削除。
                    // 削除後はキャレット位置がおかしくなるので再設定する。
                    this.TweetBox.SelectionStart--;
                    int index = this.TweetBox.SelectionStart;
                    this.TweetBox.Text = this.TweetBox.Text.Remove(index, 1);
                    this.TweetBox.SelectionStart = index;
                }
            }
            else
            {
                TweetBoxValue retValue = new TweetBoxValue((string)this.Tweet.Clone());
                contextMenu.DoRegistTweetBoxMenuHook(e, retValue);
                if (retValue != null)
                {
                    if (retValue.text != null)
                        this.Tweet = (string)retValue.text.Clone();
                    if (retValue.sendTweet)
                        SendButton_Click(null, null);
                }
            }
        }

        /// <summary>
        /// メニューのクリックフックに追加
        /// </summary>
        /// <param name="hook"></param>
        public void AddRangeRegistTweetBoxMenuHook(List<OnRegistTweetBoxMenuHook> hook)
        {
            if (hook == null || hook.Count == 0)
                return;
            this.contextMenu.AddRangeRegistTweetBoxMenuHook(hook);
        }

        void SendButton_Click(object sender, EventArgs e)
        {
            if (SendButtonClicked != null)
                SendButtonClicked.Invoke(sender, e);
        }

        public int BoxHeight
        {
            get
            {
                return this._BoxHeight;
            }
            set
            {
                this._BoxHeight = value;
            }
        }

        /// <summary>
        /// 選択されたアイコン
        /// </summary>
        public Bitmap SelectedIcon
        {
            get { return new Bitmap(this.AccountSelectedImage.Image); }
            set
            {
                if (this.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate()
                    {
                        this.AccountSelectedImage.Image = value;
                    });
                }
                else
                {
                    this.AccountSelectedImage.Image = value;
                }
            }
        }

        /// <summary>
        /// ツイートの取得・設定
        /// </summary>
        public string Tweet
        {
            get { return this.TweetBox.Text; }
            set
            {
                if (this.TweetBox.InvokeRequired)
                {
                    this.TweetBox.Invoke((MethodInvoker)delegate()
                    {
                        this.TweetBox.Text = value;
                    });
                }
                else
                {
                    this.TweetBox.Text = value;
                }
            }
        }

        /// <summary>
        /// コントロールの有効・無効化
        /// </summary>
        public bool EnableControls
        {
            get { return _enabled; }
            set
            {
                if (this.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate()
                    {
                        this.TweetBox.Enabled = value;
                        this.TweetSendButton.Enabled = value;
                        this.replyButton.Enabled = value;
                        this.HashtagButton.Enabled = value;
                        this.PictureBox.Enabled = value;
                        this.DraftButton.Enabled = value;
                    });
                }
                else
                {
                    this.TweetBox.Enabled = value;
                    this.TweetSendButton.Enabled = value;
                    this.replyButton.Enabled = value;
                    this.HashtagButton.Enabled = value;
                    this.PictureBox.Enabled = value;
                    this.DraftButton.Enabled = value;
                }
                _enabled = value;
            }
        }

        /// <summary>
        /// リプライ元がみられるパネルをせっちするかどうか
        /// </summary>
        public bool ReplySourcePanel
        {
            get { return !this.MainSplit.Panel1Collapsed; }
            set
            {
                if (this.MainSplit.InvokeRequired)
                {
                    this.MainSplit.Invoke((MethodInvoker)delegate()
                    {
                        this.MainSplit.FixedPanel = FixedPanel.Panel2;
                        //this.MainSplit.IsSplitterFixed = true;
                        this.MainSplit.Panel1Collapsed = !value;
                    });
                }
                else
                {
                    this.MainSplit.FixedPanel = FixedPanel.Panel2;
                    //this.MainSplit.IsSplitterFixed = true;
                    this.MainSplit.Panel1Collapsed = !value;
                }
            }
        }

        /// <summary>
        /// リプライ元がみられるパネルの高さ
        /// </summary>
        public int ReplySourcePanelHeight
        {
            get { return this.MainSplit.Panel1.Height; }
        }

        /// <summary>
        /// リプライ元がみられるパネルの高さ
        /// </summary>
        public int ReplySourcePanel2Height
        {
            get { return this.MainSplit.Panel2.Height; }
        }

        /// <summary>
        /// リプライソース描画
        /// </summary>
        /// <param name="tweet"></param>
        public void DrawReplySourcePanel(TwitterStatus tweet)
        {
            draw.initialize();
            draw.StartLayout(tweet, false, 0, this.replyImage.Width);
            draw.EndLayout();
            var height = draw.get(0).CellSizeWithoutPadding;
            if (replyImage.Image == null)
            {
                var bmp = new Bitmap(this.replyImage.Width, height);
                replyImage.Image = bmp;
            }
            else
            {
                replyImage.Image.Dispose();
                var bmp = new Bitmap(this.replyImage.Width, height);
                replyImage.Image = bmp;
            }

            this.replyStatus = tweet;
            g = Graphics.FromImage(replyImage.Image);
            draw.initialize();
            draw.DrawTweet(g, this.replyStatus, false, 0, this.replyImage.Width, null, null, new Point());
            g.Dispose();
        }

        public void DisableSourcePanel()
        {
            this.replyStatus = null;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.replyImage.Width = this.Width - 20;
            if (this.replyStatus != null)
            {
                DrawReplySourcePanel(this.replyStatus);
            }
        }

        /// <summary>
        /// 入力ボックスをSelectする
        /// </summary>
        public void SelectTweetBox()
        {
            if (this.TweetBox.InvokeRequired)
            {
                this.TweetBox.Invoke((MethodInvoker)delegate()
                {
                    this.TweetBox.Select();
                });
            }
            else
            {
                this.TweetBox.Select();
            }
        }

        /// <summary>
        /// 入力ボックスのセレクションをはじめる
        /// </summary>
        public void SelectTweetBoxStart()
        {
            if (this.TweetBox.InvokeRequired)
            {
                this.TweetBox.Invoke((MethodInvoker)delegate()
                {
                    this.TweetBox.SelectionStart = this.TweetBox.TextLength;
                });
            }
            else
            {
                this.TweetBox.SelectionStart = this.TweetBox.TextLength;
            }

        }

        public bool loadImage(string filePath, bool clipboard)
        {
            if (!String.IsNullOrEmpty(this.GetAttachImagePath))
            {
                if (MessageBox.Show("既に添付画像が設定されています。\n上書きしてもよろしいですか？", "注意", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return false;
            }

            if (clipboard)
            {
                Image img = Clipboard.GetImage();
                if (img != null)
                {
                    this.AttachImagePath = "clipboard";
                    using (MemoryStream ms = new MemoryStream())
                    {
                        img.Save(ms, ImageFormat.Jpeg);
                        this.AttachImagePath += ".jpg";
                        this.imageBytes = ms.ToArray();
                    }
                }
                else
                {
                    MessageBoxEX.ShowErrorMessageBox("クリップボードの画像読み込みに失敗しました。");
                    return false;
                }
            }
            else
            {
                if (File.Exists(filePath))
                {
                    this.AttachImagePath = (string)filePath.Clone();
                    using (FileStream fileStream = new FileStream(this.GetAttachImagePath, FileMode.Open, FileAccess.Read))
                    {
                        this.imageBytes = new byte[fileStream.Length];
                        fileStream.Read(this.imageBytes, 0, this.imageBytes.Length);
                    }
                }
                else
                {
                    MessageBoxEX.ShowErrorMessageBox("画像ファイルの読み込みに失敗しました。ファイルがみつかりませんでした。\nFilePath:" + filePath + "");
                    return false;
                }
            }

            ResizeImage();
            return true;
        }

        /// <summary>
        /// イメージがあるかどうか
        /// </summary>
        public byte[] GetImageArrayByte
        {
            get
            {
                return this.imageBytes;
            }
        }

        /// <summary>
        /// イメージをセットする
        /// </summary>
        public bool ResizeImage()
        {
            try
            {
                using (Image img = ImageGenerateUtil.byteArrayToImage(this.GetImageArrayByte))
                {
                    if (img.Width > 3000 || img.Height > 3000 ||
                        this.tConfigStatus != null && this.GetImageArrayByte.Length > this.tConfigStatus.photo_size_limit)
                    {
                        //  半分くらいにリサイズでいいんじゃね
                        var destImage = (Image)ImageGenerateUtil.ResizeImage(new Bitmap(img), (double)(img.Width / 2), (double)(img.Height / 2));
                        using (MemoryStream ms = new MemoryStream())
                        {
                            destImage.Save(ms, ImageFormat.Jpeg);
                            this.imageBytes = ms.ToArray();
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("画像ファイルの読み込みに失敗しました。ファイルが壊れている可能性があります\nFilePath:" + this.GetAttachImagePath + "", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 画像を選択したとき、枠を表示するかどうか
        /// </summary>
        /// <param name="value"></param>
        public void SelectImage(bool value, ConfigStatus config)
        {
            if (config != null)
            {
                if (value)
                    this.ImageURLIncrease = config.characters_reserved_per_media;
            }
            if (!value)
                this.ImageURLIncrease = 0;

            this.isImageEnable = value;
            if (this.TweetBox.InvokeRequired)
            {
                this.TweetBox.Invoke((MethodInvoker)delegate()
                {
                    this.PictureBox.BackgroundImage = (value ? Properties.Resources.image_16_c : Properties.Resources.image_16);
                    this.TweetBox_TextChanged(null, null);
                    //this.PictureBox = ( value ? BorderStyle.Fixed3D : BorderStyle.None );
                });
            }
            else
            {
                this.PictureBox.BackgroundImage = (value ? Properties.Resources.image_16_c : Properties.Resources.image_16);
                this.TweetBox_TextChanged(null, null);
                //this.TweetBox.BorderStyle = ( value ? BorderStyle.Fixed3D : BorderStyle.None );
            }

        }

        private void TweetBox_TextChanged(object sender, EventArgs e)
        {
            var num = (this.isWaitingBox ? 140 - ImageURLIncrease : (140 - this.Tweet.Length - ImageURLIncrease));
            this.TweetSendButton.Enabled = !(Tweet.Length == 0 || num < 0 || this.isWaitingBox);
            if (this.isImageEnable)
                this.TweetSendButton.Enabled = true;
            this.tweetDeleteBox.Visible = (this.TweetSendButton.Enabled || this.replyStatus != null);
            this.tweetCountLabel.Text = "" + num;
            this.tweetCountLabel.ForeColor = (num < 0 ? Color.Red : Color.Black);

        }

        private void TweetBox_KeyDown(object sender, KeyEventArgs e)
        {
            var textbox = sender as TextBox;
            if ((e.Control || e.Shift) && e.KeyValue == 13)
                this.TweetSendButton.PerformClick();
        }

        private void TweetBox_Leave(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (this.replyStatus == null && box.Text == "" && !this.isImageEnable)
            {
                this.isWaitingBox = true;
                box.Text = "What are you doing?";
                box.ForeColor = Color.Gray;
            }
        }

        private void TweetBox_Enter(object sender, EventArgs e)
        {
            if (this.isWaitingBox)
            {
                TextBox box = sender as TextBox;
                this.isWaitingBox = false;
                box.Text = "";
                box.ForeColor = Color.Black;
            }
            this.TweetBox.Focus();
        }

        private void TweetBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //completeWindow.ShowWithoutActive ();
            /*
            if ( completeWindow.autoComplete ( e.KeyChar.ToString() ) )
            {
                var nPoint = new Size ( 0, TweetBox.Height );
                completeWindow.Location = this.TweetBox.PointToScreen ( TweetBox.Location + nPoint );
                completeWindow.ShowWithoutActive();
                completeWindow.Show ();
            } else {
                completeWindow.Hide ();
            }
            */
            //this.SelectTweetBox ();
        }

        public string GetAttachImagePath
        {
            get
            {
                return this.AttachImagePath;
            }
        }

        public ConfigStatus tConfigStatus
        {
            get
            {
                return this.tmpConfigStatus;
            }
            set
            {
                this.Invoke((MethodInvoker)delegate()
                {
                    this.tmpConfigStatus = value;
                    this.SelectImage(!String.IsNullOrEmpty(this.AttachImagePath), this.tmpConfigStatus);
                });
            }
        }

        public void ResetImagePath()
        {
            this.Invoke((MethodInvoker)delegate()
            {
                this.AttachImagePath = "";
                this.SelectImage(false, this.tmpConfigStatus);
            });
        }


        private void tweetDeleteBox_Click(object sender, EventArgs e)
        {
            if (DeleteTweetClicked != null)
                DeleteTweetClicked.Invoke(sender, e);
        }

        private void shrimpButton_Click(object sender, EventArgs e)
        {
            if (ShrimpBeamClicked != null)
                ShrimpBeamClicked.Invoke(sender, e);
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter =
                "画像ファイル(*.png;*.jpg;*.gif)|*.png;*.jpg;*.gif|すべてのファイル(*.*)|*.*";
            ofd.FilterIndex = 1;
            //タイトルを設定する
            ofd.Title = "添付する画像ファイルを選択してください";
            ofd.RestoreDirectory = true;

            //ダイアログを表示する
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var path = (string)ofd.FileName.Clone();
                if (this.loadImage(path, false))
                    this.AttachImagePath = path;
            }

            this.SelectImage(!String.IsNullOrEmpty(this.AttachImagePath), this.tmpConfigStatus);
        }

        private void NotImplementedButtonClicked(object sender, EventArgs e)
        {
            MessageBox.Show("申し訳ございません、このボタンは工事中です。今後のバージョンアップにご期待ください", "エビ", MessageBoxButtons.OK);
        }

        public void ChangeButton(bool isDirectMessage)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate()
                {
                    this.TweetSendButton.Text = (isDirectMessage ? "DM送信" : "ツイート送信");
                });
            }
            else
            {
                this.TweetSendButton.Text = (isDirectMessage ? "DM送信" : "ツイート送信");
            }
        }

        private void AccountSelectedImage_DoubleClick(object sender, EventArgs e)
        {
            if (AccountImageDoubleClicked != null)
                AccountImageDoubleClicked.Invoke(sender, e);
        }

        private void TweetBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        private void TweetBox_DragDrop(object sender, DragEventArgs e)
        {
            string[] fileName = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (File.Exists(fileName[0]))
            {
                var ext = Path.GetExtension(fileName[0]);
                if (ext == ".png" || ext == ".jpg" || ext == ".gif")
                {
                    //  
                    this.AttachImagePath = fileName[0];
                    this.SelectImage(true, tmpConfigStatus);
                    this.SelectTweetBox();
                }
            }
        }

        private void replyButton_Click(object sender, EventArgs e)
        {
            this.TweetBox.ShowAutoComplete(true);
        }

        private void HashtagButton_Click(object sender, EventArgs e)
        {
            this.TweetBox.ShowAutoComplete(false);
        }
    }
}
