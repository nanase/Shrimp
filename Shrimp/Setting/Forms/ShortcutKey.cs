using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Shrimp.ControlParts.Timeline.Click;

namespace Shrimp.Setting.Forms
{
	/// <summary>
	/// ショートカットキーのフォーム
	/// </summary>
	public partial class ShortcutKey : UserControl, ISettingForm, IDisposable
	{
		private Dictionary<string, ShortcutActionCollection> settings;
		private ShortcutActionCollection actions;
		public ShortcutKey()
		{
			InitializeComponent();
			settings = Setting.ShortcutKeys.save();
			SettingReflection();
		}

		public void SettingReflection()
		{
			actions = settings["ShortCutKeys"];
			foreach (ShortcutAction action in actions.Keys)
			{
				var item = new ListViewItem (new string[] {
					action.UserActionToString(),
					action.shortcut_key.KeyToString(),
					action.ActionsToString() } );
				this.shortcutKeysList.Items.Add(item);
			}
		}

		public void SaveReflection()
		{
			Setting.ShortcutKeys.load(this.settings);
		}

		private void AddShortcutKeyButton_Click(object sender, EventArgs e)
		{
			ShortcutKeyEdit edit = new ShortcutKeyEdit(null);
			if (edit.ShowDialog() == DialogResult.OK)
			{
				this.actions.Keys.Add((ShortcutAction)edit.Tag);
				var action = edit.Tag as ShortcutAction;
				var item = new ListViewItem (new string[] {
					action.UserActionToString(),
					action.shortcut_key.KeyToString(),
					action.ActionsToString() } );
				action.save();
				
				this.shortcutKeysList.Items.Add(item);
				this.SaveReflection();
			}
		}

		private void EditShortcutKeyButton_Click(object sender, EventArgs e)
		{
			if (this.shortcutKeysList.SelectedItems.Count == 0)
				return;
			var selectedIndex = this.shortcutKeysList.SelectedItems[0].Index;
			ShortcutKeyEdit edit = new ShortcutKeyEdit(this.actions.Keys[selectedIndex]);
			if (edit.ShowDialog() == DialogResult.OK)
			{
				this.actions.Keys[selectedIndex] = (ShortcutAction)edit.Tag ;
				var action = edit.Tag as ShortcutAction;
				var item = new ListViewItem(new string[] {
					action.UserActionToString(),
					action.shortcut_key.KeyToString(),
					action.ActionsToString() });
				action.save();

				this.shortcutKeysList.Items[selectedIndex] = item;
				this.SaveReflection();
			}
		}

		private void DelShortcutKeyButton_Click(object sender, EventArgs e)
		{
			if (this.shortcutKeysList.SelectedItems.Count == 0)
				return;
			var selectedIndex = this.shortcutKeysList.SelectedItems[0].Index;
			this.actions.RemoveAt(selectedIndex);
			this.shortcutKeysList.Items.RemoveAt(selectedIndex);
			this.SaveReflection();
		}

		private void shortcutKeysList_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (this.shortcutKeysList.SelectedItems.Count == 0)
				return;
			EditShortcutKeyButton_Click(null, null);
		}
	}
}
