using System;
using System.Windows.Forms;

namespace Shrimp.Setting.Forms
{
    public partial class NewColor : Form
    {
        public NewColor()
        {
            InitializeComponent();
        }

        private void NewColor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        private void EOKButton_Click(object sender, EventArgs e)
        {
            var str = (string)this.colorName.Text.Clone();
            if (string.IsNullOrEmpty(str))
                str = "名無しのカラーさん";
            this.Tag = str;
        }
    }
}
