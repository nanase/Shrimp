using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Shrimp.ControlParts.TweetBox
{
    public partial class AutoCompleteForm : Form
    {
        private List<string> screen_name;
        private List<string> hashtags;

        public EventHandler OnDoubleClickedItem;

        public AutoCompleteForm()
        {
            InitializeComponent();
            this.screen_name = new List<string>();
            this.hashtags = new List<string>();
        }

        public void AddWord(string word, bool isScreenName)
        {
            if (isScreenName)
                this.screen_name.Add(word);
            else
                this.hashtags.Add(word);
        }

        public int ItemTotalHeight
        {
            get
            {
                return this.acfBox.ItemHeight;
            }
        }

        public int SelectedIndex
        {
            get
            {
                return this.acfBox.SelectedIndex;
            }
            set
            {
                this.Invoke((MethodInvoker)delegate()
                {
                    var count = this.acfBox.Items.Count;
                    var tmp = value;
                    if (tmp < 0)
                        tmp = 0;
                    if (tmp >= count - 1)
                        tmp = count - 1;
                    this.acfBox.SelectedIndex = tmp;
                });
            }
        }

        public string SelectedItem
        {
            get
            {
                if (this.acfBox.SelectedItem != null)
                    return this.acfBox.SelectedItem.ToString();
                return null;
            }
        }

        public int FindString(string str)
        {
            return this.acfBox.FindString(str);
        }


        // ShowWithoutActivationプロパティのオーバーライド
        protected override bool ShowWithoutActivation
        {
            get
            {
                return true;
            }
        }

        public int SetItems(string text, bool isScreenName)
        {
            var selNum = this.acfBox.SelectedIndex;
            if (isScreenName)
            {
                var item = this.screen_name.FindAll((word) => word.IndexOf(text) >= 0);
                if (item != null)
                {
                    this.acfBox.Items.Clear();
                    this.acfBox.Items.AddRange(item.ToArray());
                    if (selNum < 0)
                        selNum = 0;
                    if (selNum >= this.acfBox.Items.Count - 1)
                        selNum = this.acfBox.Items.Count - 1;
                    this.acfBox.SelectedIndex = selNum;
                }
            }
            else
            {
                var item = this.hashtags.FindAll((word) => word.IndexOf(text) >= 0);
                if (item != null)
                {
                    this.acfBox.Items.Clear();
                    this.acfBox.Items.AddRange(item.ToArray());
                    if (selNum < 0)
                        selNum = 0;
                    if (selNum >= this.acfBox.Items.Count - 1)
                        selNum = this.acfBox.Items.Count - 1;
                    this.acfBox.SelectedIndex = selNum;
                }
            }
            return this.acfBox.Items.Count;
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern UInt32 SetWindowPos(IntPtr hWnd,
                int hWndInsertAfter,
                int x, int y, int width, int height, int flags);
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        //このメソッドでフォームを表示します
        public void ShowWithoutActive()
        {
            const int SWP_NOSIZE = 0x0001;
            const int SWP_NOMOVE = 0x0002;
            const int SWP_NOACTIVATE = 0x0010;
            const int SWP_SHOWWINDOW = 0x0040;

            ShowWindow(this.Handle, 4);
            SetWindowPos(this.Handle, -1, 0, 0, 0, 0,
                     SWP_NOMOVE | SWP_NOACTIVATE |
                     SWP_NOSIZE | SWP_SHOWWINDOW);
            //this.Refresh ();
            var i = this.ItemTotalHeight * (this.acfBox.Items.Count + 1);
            if (i > 320)
                i = 320;
            this.Height = i;
        }

        private void acfBox_DoubleClick(object sender, EventArgs e)
        {
            if (OnDoubleClickedItem != null)
                OnDoubleClickedItem.Invoke(sender, e);
        }

    }
}
