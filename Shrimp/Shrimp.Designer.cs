using Shrimp.ControlParts.Tabs;
using Shrimp.ControlParts.Toolstrip;
namespace Shrimp
{
    partial class Shrimp
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose ( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose ();
            }
            base.Dispose ( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent ()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Shrimp));
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.notifyLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.shrimpSpringLabel = new StatusLabel();
            this.APIStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.settingButton = new ControlParts.Popup.ToolStripButtonPopup();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.connectUserstreamMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.MainSplit = new System.Windows.Forms.SplitContainer();
            this.TimelineSplit = new System.Windows.Forms.SplitContainer();
            this.MenuBar = new System.Windows.Forms.MenuStrip();
            this.タイムラインToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.テストToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.タイムラインTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ホームタイムラインHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.返信RToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ダイレクトメッセージDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.検索RToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.通知ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.単行モードToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userStreamへ接続するToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShrimpNotify = new System.Windows.Forms.NotifyIcon(this.components);
            this.ShrimpNotifyContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ViewShrimpWindowMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitShrimpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainSplit)).BeginInit();
            this.MainSplit.Panel1.SuspendLayout();
            this.MainSplit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TimelineSplit)).BeginInit();
            this.TimelineSplit.SuspendLayout();
            this.MenuBar.SuspendLayout();
            this.ShrimpNotifyContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.notifyLabel,
            this.shrimpSpringLabel,
            this.APIStatusLabel,
            this.settingButton});
            this.statusBar.Location = new System.Drawing.Point(0, 420);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(624, 22);
            this.statusBar.TabIndex = 0;
            this.statusBar.Text = "statusStrip1";
            // 
            // notifyLabel
            // 
            this.notifyLabel.Name = "notifyLabel";
            this.notifyLabel.Size = new System.Drawing.Size(49, 17);
            this.notifyLabel.Text = "Shrimp";
            // 
            // shrimpSpringLabel
            // 
            this.shrimpSpringLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Adjust;
            this.shrimpSpringLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.shrimpSpringLabel.Name = "shrimpSpringLabel";
            this.shrimpSpringLabel.Size = new System.Drawing.Size(506, 17);
            this.shrimpSpringLabel.Spring = true;
            this.shrimpSpringLabel.Text = "Welcome to Shrimp";
            // 
            // APIStatusLabel
            // 
            this.APIStatusLabel.Name = "APIStatusLabel";
            this.APIStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // settingButton
            // 
            this.settingButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.settingButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.settingButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.settingButton.Image = ((System.Drawing.Image)(resources.GetObject("settingButton.Image")));
            this.settingButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.settingButton.Name = "settingButton";
            this.settingButton.Size = new System.Drawing.Size(23, 20);
            this.settingButton.Text = "設定";
            this.settingButton.ToolTipText = "設定バーです";
            this.settingButton.Click += new System.EventHandler(this.settingButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(206, 6);
            // 
            // connectUserstreamMenu
            // 
            this.connectUserstreamMenu.Name = "connectUserstreamMenu";
            this.connectUserstreamMenu.Size = new System.Drawing.Size(32, 19);
            // 
            // MainSplit
            // 
            this.MainSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainSplit.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.MainSplit.Location = new System.Drawing.Point(0, 0);
            this.MainSplit.Name = "MainSplit";
            this.MainSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // MainSplit.Panel1
            // 
            this.MainSplit.Panel1.Controls.Add(this.TimelineSplit);
            this.MainSplit.Size = new System.Drawing.Size(624, 420);
            this.MainSplit.SplitterDistance = 339;
            this.MainSplit.TabIndex = 1;
            this.MainSplit.SplitterMoving += new System.Windows.Forms.SplitterCancelEventHandler(this.MainSplit_SplitterMoving);
            // 
            // TimelineSplit
            // 
            this.TimelineSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TimelineSplit.Location = new System.Drawing.Point(0, 0);
            this.TimelineSplit.Name = "TimelineSplit";
            this.TimelineSplit.Size = new System.Drawing.Size(624, 339);
            this.TimelineSplit.SplitterDistance = 452;
            this.TimelineSplit.TabIndex = 0;
            // 
            // MenuBar
            // 
            this.MenuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.タイムラインToolStripMenuItem,
            this.タイムラインTToolStripMenuItem});
            this.MenuBar.Location = new System.Drawing.Point(0, 0);
            this.MenuBar.Name = "MenuBar";
            this.MenuBar.Size = new System.Drawing.Size(624, 24);
            this.MenuBar.TabIndex = 2;
            this.MenuBar.Text = "menuStrip1";
            this.MenuBar.Visible = false;
            // 
            // タイムラインToolStripMenuItem
            // 
            this.タイムラインToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.テストToolStripMenuItem});
            this.タイムラインToolStripMenuItem.Name = "タイムラインToolStripMenuItem";
            this.タイムラインToolStripMenuItem.Size = new System.Drawing.Size(105, 20);
            this.タイムラインToolStripMenuItem.Text = "アカウント(&A)";
            // 
            // テストToolStripMenuItem
            // 
            this.テストToolStripMenuItem.Name = "テストToolStripMenuItem";
            this.テストToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.テストToolStripMenuItem.Text = "テスト";
            // 
            // タイムラインTToolStripMenuItem
            // 
            this.タイムラインTToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ホームタイムラインHToolStripMenuItem,
            this.返信RToolStripMenuItem,
            this.ダイレクトメッセージDToolStripMenuItem,
            this.検索RToolStripMenuItem,
            this.通知ToolStripMenuItem,
            this.toolStripSeparator1,
            this.単行モードToolStripMenuItem});
            this.タイムラインTToolStripMenuItem.Name = "タイムラインTToolStripMenuItem";
            this.タイムラインTToolStripMenuItem.Size = new System.Drawing.Size(118, 20);
            this.タイムラインTToolStripMenuItem.Text = "タイムライン(&T)";
            // 
            // ホームタイムラインHToolStripMenuItem
            // 
            this.ホームタイムラインHToolStripMenuItem.Name = "ホームタイムラインHToolStripMenuItem";
            this.ホームタイムラインHToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.ホームタイムラインHToolStripMenuItem.Text = "ホームタイムライン(&H)";
            // 
            // 返信RToolStripMenuItem
            // 
            this.返信RToolStripMenuItem.Name = "返信RToolStripMenuItem";
            this.返信RToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.返信RToolStripMenuItem.Text = "返信(&R)";
            // 
            // ダイレクトメッセージDToolStripMenuItem
            // 
            this.ダイレクトメッセージDToolStripMenuItem.Name = "ダイレクトメッセージDToolStripMenuItem";
            this.ダイレクトメッセージDToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.ダイレクトメッセージDToolStripMenuItem.Text = "ダイレクトメッセージ(&D)";
            // 
            // 検索RToolStripMenuItem
            // 
            this.検索RToolStripMenuItem.Name = "検索RToolStripMenuItem";
            this.検索RToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.検索RToolStripMenuItem.Text = "検索(&S)";
            // 
            // 通知ToolStripMenuItem
            // 
            this.通知ToolStripMenuItem.Name = "通知ToolStripMenuItem";
            this.通知ToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.通知ToolStripMenuItem.Text = "通知(&N)";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(222, 6);
            // 
            // 単行モードToolStripMenuItem
            // 
            this.単行モードToolStripMenuItem.Name = "単行モードToolStripMenuItem";
            this.単行モードToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.単行モードToolStripMenuItem.Text = "単行モード(&L)";
            // 
            // userStreamへ接続するToolStripMenuItem
            // 
            this.userStreamへ接続するToolStripMenuItem.Name = "userStreamへ接続するToolStripMenuItem";
            this.userStreamへ接続するToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.userStreamへ接続するToolStripMenuItem.Text = "UserStreamへ接続する";
            // 
            // ShrimpNotify
            // 
            this.ShrimpNotify.ContextMenuStrip = this.ShrimpNotifyContextMenuStrip;
            this.ShrimpNotify.Icon = ((System.Drawing.Icon)(resources.GetObject("ShrimpNotify.Icon")));
            this.ShrimpNotify.Text = "Shrimp";
            this.ShrimpNotify.Visible = true;
            this.ShrimpNotify.DoubleClick += new System.EventHandler(this.ShrimpNotify_DoubleClick);
            // 
            // ShrimpNotifyContextMenuStrip
            // 
            this.ShrimpNotifyContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewShrimpWindowMenu,
            this.ExitShrimpMenu});
            this.ShrimpNotifyContextMenuStrip.Name = "ShrimpNotifyContextMenuStrip";
            this.ShrimpNotifyContextMenuStrip.Size = new System.Drawing.Size(101, 48);
            // 
            // ViewShrimpWindowMenu
            // 
            this.ViewShrimpWindowMenu.Name = "ViewShrimpWindowMenu";
            this.ViewShrimpWindowMenu.Size = new System.Drawing.Size(100, 22);
            this.ViewShrimpWindowMenu.Text = "表示";
            this.ViewShrimpWindowMenu.Click += new System.EventHandler(this.ViewShrimpWindowMenu_Click);
            // 
            // ExitShrimpMenu
            // 
            this.ExitShrimpMenu.Name = "ExitShrimpMenu";
            this.ExitShrimpMenu.Size = new System.Drawing.Size(100, 22);
            this.ExitShrimpMenu.Text = "終了";
            this.ExitShrimpMenu.Click += new System.EventHandler(this.ExitShrimpMenu_Click);
            // 
            // Shrimp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 442);
            this.Controls.Add(this.MainSplit);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.MenuBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Shrimp";
            this.Text = "Shrimp";
            this.Activated += new System.EventHandler(this.Shrimp_Activated);
            this.Move += new System.EventHandler(this.Shrimp_Move);
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.MainSplit.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainSplit)).EndInit();
            this.MainSplit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TimelineSplit)).EndInit();
            this.TimelineSplit.ResumeLayout(false);
            this.MenuBar.ResumeLayout(false);
            this.MenuBar.PerformLayout();
            this.ShrimpNotifyContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.SplitContainer MainSplit;
        private System.Windows.Forms.SplitContainer TimelineSplit;
        private StatusLabel shrimpSpringLabel;
        private System.Windows.Forms.ToolStripStatusLabel notifyLabel;
        private System.Windows.Forms.MenuStrip MenuBar;
        private System.Windows.Forms.ToolStripMenuItem タイムラインToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem テストToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem タイムラインTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ホームタイムラインHToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 返信RToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ダイレクトメッセージDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 検索RToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 単行モードToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 通知ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userStreamへ接続するToolStripMenuItem;
        private ControlParts.Popup.ToolStripButtonPopup settingButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem connectUserstreamMenu;
        private System.Windows.Forms.ToolStripStatusLabel APIStatusLabel;
        private System.Windows.Forms.NotifyIcon ShrimpNotify;
        private System.Windows.Forms.ContextMenuStrip ShrimpNotifyContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem ViewShrimpWindowMenu;
        private System.Windows.Forms.ToolStripMenuItem ExitShrimpMenu;
    }
}