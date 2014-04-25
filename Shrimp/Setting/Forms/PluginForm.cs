using System.Windows.Forms;
using Shrimp.Plugin;

namespace Shrimp.Setting.Forms
{
    public partial class PluginForm : UserControl
    {
        public PluginForm ( Plugins plugins )
        {
            InitializeComponent ();
            
            if ( plugins != null )
            {
                var lists = plugins.destPluginList ();
                foreach ( ListViewItem item in lists )
                {
                    this.pluginList.Items.Add ( item );
                }
            }
            this.pluginList.AutoResizeColumns ( ColumnHeaderAutoResizeStyle.HeaderSize );
        }
    }
}
