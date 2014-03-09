using System;
using System.Windows.Forms;
using Shrimp.Twitter.REST.List;

namespace Shrimp.ControlParts.TabSetting
{
    public partial class ListSelectView : UserControl
    {
        private readonly listDataCollection list;
        public EventHandler OnChangedDetail;

        public ListSelectView(listDataCollection list)
        {
            InitializeComponent();
            this.list = list;

            foreach (listData l in list.lists)
            {
                this.listSelectCombobox.Items.Add(l.name);
            }
        }

        /// <summary>
        /// 選択中のリストを選択する
        /// </summary>
        /// <returns></returns>
        public listData getNowSelectedList()
        {
            if (this.listSelectCombobox.SelectedIndex < 0)
                return null;
            return this.list.get(this.listSelectCombobox.SelectedIndex);
        }

        private void listSelectCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OnChangedDetail != null)
                OnChangedDetail.Invoke(sender, e);
        }
    }
}
