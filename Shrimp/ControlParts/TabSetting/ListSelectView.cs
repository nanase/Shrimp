using System;
using System.Windows.Forms;
using Shrimp.Twitter.REST.List;

namespace Shrimp.ControlParts.TabSetting
{
    public partial class ListSelectView : UserControl
    {
        private readonly listDataCollection list;
        public EventHandler OnChangedDetail;

        public ListSelectView(listDataCollection list, listData nowlist)
        {
            InitializeComponent();
            this.list = list;

            int i = 0;
            foreach (listData l in list.lists)
            {
                this.listSelectCombobox.Items.Add(l.name);
                if ( nowlist != null && l.name == nowlist.name )
                    this.listSelectCombobox.SelectedIndex = i;
                i++;
            }

            if ( nowlist == null )
                this.listSelectCombobox.SelectedIndex = 0;

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
