using System;
using System.Windows.Forms;
using Shrimp.ControlParts.Tabs;
using Shrimp.Twitter.REST.List;
using Shrimp.Twitter.Status;

namespace Shrimp.ControlParts.TabSetting
{
    public partial class TabCategory : UserControl
    {
        private readonly TimelineCategory _category;
        private readonly listDataCollection _listDatas;
        private ListSelectView listControl;
        private NotifySelectView notifyControl;

        public TabCategory(TabDelivery delivery, listDataCollection listDatas)
        {
            InitializeComponent();
            this._category = delivery.Category;
            this._listDatas = listDatas;

            this.listControl = new ListSelectView(this._listDatas);
            this.listControl.OnChangedDetail += new EventHandler(listOnChangedDetail);
            if (this._category.notifyFilter == null)
                this._category.notifyFilter = new Twitter.Status.NotifyFilter();
            this.notifyControl = new NotifySelectView(this._category.notifyFilter);
            this.notifyControl.OnChangedDetail += new EventHandler(notifyOnChangedDetail);

            this.TabCategoryBox.SelectedIndex = TimelineCategoriesConverter(_category.category);
            if (this._category.category == TimelineCategories.ListTimeline)
                AddControl(listControl);
            if (this._category.category == TimelineCategories.NotifyTimeline)
                AddControl(notifyControl);
        }

        ~TabCategory()
        {
            this.listControl.OnChangedDetail -= new EventHandler(listOnChangedDetail);
            this.notifyControl.OnChangedDetail -= new EventHandler(notifyOnChangedDetail);
        }

        private void listOnChangedDetail(object sender, EventArgs e)
        {
            this._category.ListData = this.listControl.getNowSelectedList();
            this.Tag = true;
        }


        private void notifyOnChangedDetail(object sender, EventArgs e)
        {
            if ( this.notifyControl.getNotifyFilter != null )
                this._category.notifyFilter = (NotifyFilter)this.notifyControl.getNotifyFilter.Clone ();
            this.Tag = true;
        }

        private int TimelineCategoriesConverter(TimelineCategories category)
        {
            if (category == TimelineCategories.None)
                return 0;
            if (category == TimelineCategories.HomeTimeline)
                return 1;
            if (category == TimelineCategories.MentionTimeline)
                return 2;
            if (category == TimelineCategories.DirectMessageTimeline)
                return 3;
            if (category == TimelineCategories.ListTimeline)
                return 4;
            if (category == TimelineCategories.NotifyTimeline)
                return 5;
            return 0;
        }

        private void AddControl(Control c)
        {
            this.otherBox.Controls.Clear();
            c.Dock = DockStyle.Fill;
            this.otherBox.Controls.Add(c);
        }

        private TimelineCategories TabCategoryConverter(int category)
        {
            if (category == 0)
                return TimelineCategories.None;
            if (category == 1)
                return TimelineCategories.HomeTimeline;
            if (category == 2)
                return TimelineCategories.MentionTimeline;
            if (category == 3)
                return TimelineCategories.DirectMessageTimeline;
            if (category == 4)
                return TimelineCategories.ListTimeline;
            if (category == 5)
                return TimelineCategories.NotifyTimeline;
            return TimelineCategories.None;
        }

        private void TabCategoryBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._category.category = TabCategoryConverter(this.TabCategoryBox.SelectedIndex);
            if (this._category.category == TimelineCategories.ListTimeline)
            {
                AddControl(listControl);
                return;
            }
            if (this._category.category == TimelineCategories.NotifyTimeline)
            {
                if (this._category.notifyFilter == null)
                    this._category.notifyFilter = new NotifyFilter();
                AddControl(notifyControl);
                return;
            }
            this.Tag = true;
            this.otherBox.Controls.Clear();
        }
    }
}
