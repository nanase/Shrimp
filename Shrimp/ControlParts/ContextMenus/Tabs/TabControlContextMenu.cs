using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Shrimp.ControlParts.ContextMenus.Tabs
{
    public partial class TabControlContextMenu : Component
    {
        public CancelEventHandler Opening;
        private bool _isLock = false;
        private bool _isFlash = false;

        public TabControlContextMenu ()
        {
            InitializeComponent ();
            this.SearchMenu.Image = Setting.ResourceImages.SearchImage;
            this.Menu.Opening += new CancelEventHandler ( Menu_Opening );
        }

        ~TabControlContextMenu ()
        {
            this.Menu.Opening -= new CancelEventHandler ( Menu_Opening );
        }

        void Menu_Opening ( object sender, CancelEventArgs e )
        {
            this.LockTabMenu.Checked = this.isLock;
            if ( Opening != null )
                Opening.Invoke ( this, e );
        }

        /// <summary>
        /// ContextMenu
        /// </summary>
        public ContextMenuStrip ContextMenu
        {
            get { return this.Menu; }
        }

        public ContextMenuStrip CustomControlMenu
        {
            get
            {
                return this.CustomMenuStrip;
            }
        }

        /// <summary>
        /// コントロールを選択できるかどうかのフラグ
        /// </summary>
        public bool canSelect
        {
            get { return this.SelectedThisTabMenu.Enabled; }
            set
            {
                if ( this.Menu.InvokeRequired )
                {
                    this.Menu.Invoke ( (MethodInvoker)delegate ()
                    {
                        this.SelectedThisTabMenu.Enabled = value;
                    } );
                }
                else
                {
                    this.SelectedThisTabMenu.Enabled = value;
                }
            }
        }

        /// <summary>
        /// コントロールを編集できるかどうかのフラグ
        /// </summary>
        public bool canEdit
        {
            get { return this.TabSettingMenu.Enabled; }
            set
            {
                if ( this.Menu.InvokeRequired )
                {
                    this.Menu.Invoke ( (MethodInvoker)delegate ()
                    {
                        this.TabSettingMenu.Enabled = value;
                    } );
                }
                else
                {
                    this.TabSettingMenu.Enabled = value;
                }
            }
        }

        /// <summary>
        /// コントロールを破棄できるかどうかのフラグ
        /// </summary>
        public bool isLock
        {
            get { return this._isLock; }
            set
            {
                if ( this.Menu.InvokeRequired )
                {
                    this.Menu.Invoke ( (MethodInvoker)delegate ()
                    {
                        this.DestroyThisTabMenu.Enabled = !value;
                    } );
                }
                else
                {
                    this.DestroyThisTabMenu.Enabled = !value;
                }
                _isLock = value;
            }
        }

        /// <summary>
        /// ウィンドウをフラッシュさせるかどうか
        /// </summary>
        public bool isFlash
        {
            get { return this._isFlash; }
            set
            {
                if ( this.Menu.InvokeRequired )
                {
                    this.Menu.Invoke ( (MethodInvoker)delegate ()
                    {
                        this.FlashTabMenu.Checked = value;
                    } );
                }
                else
                {
                    this.FlashTabMenu.Checked = value;
                }
                _isFlash = value;
            }
        }
    }
}
