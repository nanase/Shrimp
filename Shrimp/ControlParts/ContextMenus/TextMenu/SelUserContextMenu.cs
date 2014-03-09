using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Shrimp.ControlParts.ContextMenus.TextMenu
{
    public partial class SelUserContextMenu : Component
    {
        #region 定義
        /// <summary>
        /// メニューが閉じたときのイベントハンドラ
        /// </summary>
        public event EventHandler MenuClosed;
        public event EventHandler MenuOpening;
        public event ToolStripItemClickedEventHandler MenuItemClicked;
        public bool isClosed = false;
        public string screen_name = "";

        #endregion

        public SelUserContextMenu ()
        {
            InitializeComponent ();
            this.OpenUserTimelineTabMenu.Image = Setting.ResourceImages.TextImage;
            this.OpenReplyToUserTabMenu.Image = Setting.ResourceImages.RepliesImage;
            this.OpenUserFavTimelineTabMenu.Image = Setting.ResourceImages.FavsImage;
            this.BlockMenu.Image = Setting.ResourceImages.BlockImage;
            this.OpenConversationTabMenu.Image = Setting.ResourceImages.ConversationImage;
        }

        void Menu_Opening ( object sender, CancelEventArgs e )
        {
            this.isClosed = false;
            if ( this.MenuOpening != null )
                this.MenuOpening ( sender, e );
        }

        /// <summary>
        /// ユーザーメニュー表示
        /// </summary>
        /// <param name="p"></param>
        public void ShowMenu ( Point screen_location, string screen_name )
        {
            this.screen_name = screen_name;
            this.UserMenu.Show ( screen_location );
        }

        /// <summary>
        /// ユーザーメニュー表示
        /// </summary>
        /// <param name="p"></param>
        public void MenuClose ()
        {
            this.UserMenu.Close ();
        }

        /// <summary>
        /// 表示中？
        /// </summary>
        public bool Visible
        {
            get { return this.UserMenu.Visible; }
        }

        /// <summary>
        /// メニューが閉じたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_Closed ( object sender, ToolStripDropDownClosedEventArgs e )
        {
            this.isClosed = true;
            if ( MenuClosed != null )
            {
                MenuClosed ( sender, e );
            }
        }

        public ContextMenuStrip ContextMenu
        {
            get { return this.UserMenu; }
        }

        private void UserMenu_ItemClicked ( object sender, ToolStripItemClickedEventArgs e )
        {
            if ( MenuItemClicked != null )
            {
                MenuItemClicked.Invoke ( screen_name, e );
            }
        }
    }
}
