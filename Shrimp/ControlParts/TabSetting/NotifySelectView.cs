using System;
using System.Windows.Forms;
using Shrimp.Twitter.Status;

namespace Shrimp.ControlParts.TabSetting
{
    public partial class NotifySelectView : UserControl
    {
        public EventHandler OnChangedDetail;
        private NotifyFilter tmpFilter = null;
        private bool isInitialized = false;
        public NotifySelectView(NotifyFilter notifyFilter)
        {
            InitializeComponent();
            notifyBox.SetItemChecked(0, notifyFilter.Favorited);
            notifyBox.SetItemChecked(1, notifyFilter.UnFavorited);
            notifyBox.SetItemChecked(2, notifyFilter.Followed);
            notifyBox.SetItemChecked(3, notifyFilter.Unfollowed);
            tmpFilter = notifyFilter;
            this.isInitialized = true;
        }

        public void setNotifyFilter()
        {
            //NotifyFilter notify = new NotifyFilter ();
            foreach (string item in this.notifyBox.SelectedItems)
            {
                if (item == "お気に入りに追加されたとき")
                    tmpFilter.Favorited = !tmpFilter.Favorited;
                if (item == "お気に入りから削除されたとき")
                    tmpFilter.UnFavorited = !tmpFilter.UnFavorited;
                if (item == "フォローされたとき")
                    tmpFilter.Followed = !tmpFilter.Followed;
                if (item == "アンフォローしたとき")
                    tmpFilter.Unfollowed = !tmpFilter.Unfollowed;
            }
            //return notify;
        }

        private void notifyBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!this.isInitialized)
                return;
            this.setNotifyFilter();
            if (OnChangedDetail != null)
                OnChangedDetail.Invoke(sender, e);
        }
    }
}
