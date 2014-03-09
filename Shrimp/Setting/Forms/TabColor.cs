using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Shrimp.Setting.ObjectXML;

namespace Shrimp.Setting.Forms
{
    public partial class TabColor : UserControl, ISettingForm, IDisposable
    {
        private Dictionary<string, TabColorManager> tabcolors;
        public TabColor ()
        {
            InitializeComponent ();
            tabcolors = Setting.TabColors.save ();
            SettingReflection ();
        }

        public void SettingReflection ()
        {
            var p = tabcolors["ExistUnRead"];
			this.UnReadBackgroundPicture.Image = SettingUtils.CreateImageColor(p.BackgroundColor.Generate);
			this.UnReadTextPicture.Image = SettingUtils.CreateImageColor(p.StringColor.Generate);
			this.UnReadCheckBox.SetItemChecked(0, p.isBold);

            p = tabcolors["UnOpened"];
			this.UnOpenedBackgroundPicture.Image = SettingUtils.CreateImageColor(p.BackgroundColor.Generate);
			this.UnOpenedTextPicture.Image = SettingUtils.CreateImageColor(p.StringColor.Generate);
			this.UnOpenedCheckBox.SetItemChecked(0, p.isBold);

            p = tabcolors["Normal"];
			this.NormalBackgroundPicture.Image = SettingUtils.CreateImageColor(p.BackgroundColor.Generate);
			this.NormalTextPicture.Image = SettingUtils.CreateImageColor(p.StringColor.Generate);
			this.NormalCheckBox.SetItemChecked(0, p.isBold);

            p = tabcolors["Selected"];
			this.SelectBackgroundPicture.Image = SettingUtils.CreateImageColor(p.BackgroundColor.Generate);
			this.SelectTextPicture.Image = SettingUtils.CreateImageColor(p.StringColor.Generate);
			this.SelectCheckBox.SetItemChecked(0, p.isBold);
        }

        public void SaveReflection ()
        {
            Setting.TabColors.load ( tabcolors );
        }

		private void SelectColorButtonClicked(object sender, EventArgs e)
		{
			Button sender_button = sender as Button;
			if (this.SelectColorDialog.ShowDialog() == DialogResult.OK)
			{
				ColorReflection(sender_button.Name, new SolidBrush(this.SelectColorDialog.Color));
				SaveReflection();
			}
		}

		private void ColorReflection(string objName, Brush cl)
		{
			if (objName == "UnReadBackgroundButton")
			{
				if (this.UnReadBackgroundPicture.Image != null)
					this.UnReadBackgroundPicture.Image.Dispose();
				this.UnReadBackgroundPicture.Image = SettingUtils.CreateImageColor(cl);
				tabcolors["ExistUnRead"].BackgroundColor = new BrushEX(cl);
			}
			if (objName == "UnReadTextButton")
			{
				if (this.UnReadTextPicture.Image != null)
					this.UnReadTextPicture.Image.Dispose();
				this.UnReadTextPicture.Image = SettingUtils.CreateImageColor(cl);
				tabcolors["ExistUnRead"].StringColor = new BrushEX(cl);
			}
			if (objName == "UnOpenedBackgroundButton")
			{
				if (this.UnOpenedBackgroundPicture.Image != null)
					this.UnOpenedBackgroundPicture.Image.Dispose();
				this.UnOpenedBackgroundPicture.Image = SettingUtils.CreateImageColor(cl);
				tabcolors["UnOpened"].BackgroundColor = new BrushEX(cl);
			}
			if (objName == "UnOpenedTextButton")
			{
				if (this.UnOpenedTextPicture.Image != null)
					this.UnOpenedTextPicture.Image.Dispose();
				this.UnOpenedTextPicture.Image = SettingUtils.CreateImageColor(cl);
				tabcolors["UnOpened"].StringColor = new BrushEX(cl);
			}
			if (objName == "NormalBackgroundButton")
			{
				if (this.NormalBackgroundPicture.Image != null)
					this.NormalBackgroundPicture.Image.Dispose();
				this.NormalBackgroundPicture.Image = SettingUtils.CreateImageColor(cl);
				tabcolors["Normal"].BackgroundColor = new BrushEX(cl);
			}
			if (objName == "NormalTextButton")
			{
				if (this.NormalTextPicture.Image != null)
					this.NormalTextPicture.Image.Dispose();
				this.NormalTextPicture.Image = SettingUtils.CreateImageColor(cl);
				tabcolors["Normal"].StringColor = new BrushEX(cl);
			}
			if (objName == "SelectBackgrounButton")
			{
				if (this.SelectBackgroundPicture.Image != null)
					this.SelectBackgroundPicture.Image.Dispose();
				this.SelectBackgroundPicture.Image = SettingUtils.CreateImageColor(cl);
				tabcolors["Selected"].BackgroundColor = new BrushEX(cl);
			}
			if (objName == "SelectTextButton")
			{
				if (this.SelectTextPicture.Image != null)
                    this.SelectTextPicture.Image.Dispose ();
                this.SelectTextPicture.Image = SettingUtils.CreateImageColor ( cl );
				tabcolors["Selected"].StringColor = new BrushEX(cl);
			}
		}

		private void SelectCheckBox_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			CheckedListBox box = sender as CheckedListBox;
			if (box.Name == "UnReadCheckBox")
			{
				if (e.Index == 0)
					tabcolors["ExistUnRead"].isBold = (e.NewValue == CheckState.Checked ? true : false);
			}
			if (box.Name == "UnOpenedCheckBox")
			{
				if (e.Index == 0)
					tabcolors["UnOpened"].isBold = (e.NewValue == CheckState.Checked ? true : false);
			}
			if (box.Name == "NormalCheckBox")
			{
				if (e.Index == 0)
					tabcolors["Normal"].isBold = (e.NewValue == CheckState.Checked ? true : false);
			}
			if (box.Name == "SelectCheckBox")
			{
				if (e.Index == 0)
					tabcolors["Selected"].isBold = (e.NewValue == CheckState.Checked ? true : false);
			}
			SaveReflection();
		}
	}
}
