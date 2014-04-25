using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Shrimp.Twitter;

namespace Shrimp.ControlParts.Popup
{
    public partial class StatusPopup : Component
    {
        public event ItemClickedDelegate ItemClicked;
        public delegate void ItemClickedDelegate(object sender, ToolStripItemClickedEventArgs e);
		private Dictionary<decimal, ToolStripMenuItem> accountNames = new Dictionary<decimal, ToolStripMenuItem>();

        public StatusPopup()
        {
            InitializeComponent();
        }

        /// <summary>
        /// アカウント選択一覧につっこむ
        /// </summary>
        /// <param name="account"></param>
        public void InsertAccountName(TwitterInfo t, bool isSelected = false)
        {
			var item = new ToolStripMenuItem();
			item.Image = t.IconData;
			string us = "";
			if (t.isStreamingEnable)
				us = "○ UserStreamに接続しています。";
			else
				us = "× UserStreamに接続していません。";
			item.Text = "@" + t.ScreenName + "\n"+ us +"";
			item.Name = t.UserId.ToString();
            this.Menu.Items.Insert(0, item);
            accountNames[t.UserId] = item;

            accountNames[t.UserId].Checked = isSelected;
        }

        public void Show(Point p)
        {
            this.LineTweetModeMenu.Checked = Setting.Timeline.isLineMode;
            this.UserInformationMenu.Checked = Setting.Timeline.isShowUserInformation;
            this.Menu.Show(p);
        }

        /// <summary>
        /// ユーザーストリームの接続表記を変更する
        /// </summary>
        /// <param name="value"></param>
        public void ChangeUserStreamMenu(bool value)
        {
            if (!value)
            {
                ConnectUserStreamNenu.Text = "UserStreamへ接続する";
            }
            else
            {
                ConnectUserStreamNenu.Text = "UserStreamを切断する";
            }
        }

        /// <summary>
        /// メニューのアイテムが洗濯されました
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (ItemClicked != null)
                ItemClicked.Invoke(sender, e);
        }

        private void Menu_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            foreach (var key in this.accountNames.Keys.ToList())
            {
                if (this.accountNames.ContainsKey(key) && this.accountNames[key] != null)
                {
                    this.accountNames[key].Dispose();
                    this.accountNames[key] = null;
                }
            }
        }
    }
}
