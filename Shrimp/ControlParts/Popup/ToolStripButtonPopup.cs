using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Shrimp.Twitter;

namespace Shrimp.ControlParts.Popup
{
    class ToolStripButtonPopup : ToolStripButton
    {
        public event ControlParts.Popup.StatusPopup.ItemClickedDelegate ItemClicked;
        private StatusPopup popup = new StatusPopup ();

        public ToolStripButtonPopup ()
        {
            this.popup.ItemClicked += new StatusPopup.ItemClickedDelegate ( popup_ItemClicked );
            //this.Click += new EventHandler ( ToolStripButtonPopup_Click );
        }

        ~ToolStripButtonPopup ()
        {
            this.popup.ItemClicked -= new StatusPopup.ItemClickedDelegate ( popup_ItemClicked );
            //this.Click -= new EventHandler ( ToolStripButtonPopup_Click );
        }

        /// <summary>
        /// アカウント選択一覧につっこむ
        /// </summary>
        /// <param name="account"></param>
        public void InsertAccountName ( TwitterInfo t, bool isSelected = false )
        {
            this.popup.InsertAccountName ( t, isSelected );
        }

        /// <summary>
        /// ユーザーストリームの接続表記を変更する
        /// </summary>
        /// <param name="status"></param>
        public void ChangeUserStreamMenu ( bool status )
        {
            this.popup.ChangeUserStreamMenu ( status );
        }

        void popup_ItemClicked ( object sender, ToolStripItemClickedEventArgs e )
        {
            if ( ItemClicked != null )
                ItemClicked.Invoke ( sender, e );
        }

        public void Show ( Point l )
        {
            this.popup.Show ( l );
        }
    }
}
