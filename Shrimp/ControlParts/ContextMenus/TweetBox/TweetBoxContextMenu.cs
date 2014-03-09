using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Shrimp.Plugin.Ref;

namespace Shrimp.ControlParts.ContextMenus.TweetBox
{
    public partial class TweetBoxContextMenu : Component
    {
        private List<OnRegistTweetBoxMenuHook> OnRegistTweetBoxMenuHookList;
        public TweetBoxContextMenu()
        {
            InitializeComponent();
            this.OnRegistTweetBoxMenuHookList = new List<OnRegistTweetBoxMenuHook>();
        }

        public void AddRangeRegistTweetBoxMenuHook(List<OnRegistTweetBoxMenuHook> hooks)
        {
            foreach (OnRegistTweetBoxMenuHook hook in hooks)
            {
                this.OnRegistTweetBoxMenuHookList.Add(hook);
                if (this.OnRegistTweetBoxMenuHookList.Count == 1)
                    this.Menu.Items.Add(new ToolStripSeparator());
                this.Menu.Items.Add(new ToolStripMenuItem(hook.text) { ToolTipText = hook.tooltipText, Tag = hook });
            }
        }

        public ContextMenuStrip contextMenu
        {
            get { return this.Menu; }
        }

        /// <summary>
        /// プラグインへコールバックする際に使われる関数です
        /// </summary>
        /// <param name="e"></param>
        /// <param name="textBoxValue"></param>
        /// <returns>成功すると、trueが帰ります</returns>
        public bool DoRegistTweetBoxMenuHook(ToolStripItemClickedEventArgs e, TweetBoxValue outputTextBoxValue)
        {
            var hook = OnRegistTweetBoxMenuHookList.Find((f) => f.text == e.ClickedItem.Text);
            if (hook != null)
            {
                hook.CallBackPlugin(new object[] { outputTextBoxValue });
                return true;
            }
            return false;
        }

        public bool isEnabledClipboardImage
        {
            get { return this.PasteImageMenu.Enabled; }
            set
            {
                this.PasteImageMenu.Enabled = value;
            }
        }
    }
}
