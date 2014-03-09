using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace Shrimp.Setting.Forms
{
	public partial class ShrimpInfo : UserControl
	{
        int num = 0;
        bool flg = false;
        private Dictionary<string, bool> updateSetting;
		public ShrimpInfo()
		{
			InitializeComponent();

            string appCopyright = "-";
            Assembly mainAssembly = Assembly.GetEntryAssembly ();
            object[] CopyrightArray =
              mainAssembly.GetCustomAttributes (
                typeof ( AssemblyCopyrightAttribute ), false );
            if ( ( CopyrightArray != null ) && ( CopyrightArray.Length > 0 ) )
            {
                appCopyright =
                  ( (AssemblyCopyrightAttribute)CopyrightArray[0] ).Copyright;
            }
            this.richTextBox1.Text += "\r\n\r\n" + appCopyright;
            this.updateSetting = Setting.Update.save ();
            this.UpdateCheckBox.Checked = this.updateSetting["isUpdateEnable"];
		}

        private void pictureBox1_Click ( object sender, EventArgs e )
        {
            num++;
            if ( num % 5 == 0 )
            {
                if ( num == 15 )
                {
                    MessageBox.Show ( "もう怒った！エビビーム！！" );
                    System.Environment.Exit ( 0 );
                }
                else if ( num == 10 )
                {
                    MessageBox.Show ( "いたいってば！" );
                }
                else if ( num == 5 )
                {
                    MessageBox.Show ( "いたい！" );
                }
            }
        }

        private void UpdateCheckBox_CheckedChanged ( object sender, EventArgs e )
        {
            var obj = sender as CheckBox;
            this.updateSetting["isUpdateEnable"] = obj.Checked;
            Setting.Update.load ( this.updateSetting );
        }
	}
}
