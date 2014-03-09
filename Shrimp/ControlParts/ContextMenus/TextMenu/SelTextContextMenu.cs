using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Shrimp.ControlParts.ContextMenus.TextMenu
{
    public partial class SelTextContextMenu : Component
    {
        #region 定義
        /// <summary>
        /// メニューが閉じたときのイベントハンドラ
        /// </summary>
        public event EventHandler MenuClosed;
        /// <summary>
        /// アイテムがクリックされたときのイベントハンドラ
        /// </summary>
        public event ToolStripItemClickedEventHandler ItemClicked;
        public bool isClosed = false;
        #endregion

        public SelTextContextMenu()
        {
            InitializeComponent();
            this.Menu.Opening += new CancelEventHandler(Menu_Opening);
            this.Menu.Closed += new ToolStripDropDownClosedEventHandler(Menu_Closed);
        }

        ~SelTextContextMenu()
        {
            this.Menu.Opening -= new CancelEventHandler(Menu_Opening);
            this.Menu.Closed -= new ToolStripDropDownClosedEventHandler(Menu_Closed);
        }

        void Menu_Opening(object sender, CancelEventArgs e)
        {
            this.isClosed = false;
        }

        public SelTextContextMenu(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        /// <summary>
        /// メニューを表示する
        /// </summary>
        /// <param name="p"></param>
        public void ShowMenu(Point p)
        {
            this.Menu.Show(p);
        }

        /// <summary>
        /// 表示中？
        /// </summary>
        public bool Visible
        {
            get { return this.Menu.Visible; }
        }

        /// <summary>
        /// メニューが閉じたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            this.isClosed = true;
            if (MenuClosed != null)
            {
                MenuClosed(sender, e);
            }
        }

        /// <summary>
        /// アイテムがクリックされたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (ItemClicked != null)
                ItemClicked(sender, e);
        }
    }
}
