using System;
using System.Windows.Forms;
using Shrimp.ControlParts.Timeline.Click;

namespace Shrimp.Setting.Forms
{
    public partial class ShortcutKeyEdit : Form
    {
        ControlParts.Timeline.Click.ShortcutKey key;
        Actions actions;
        UserActions userActions;

        public ShortcutKeyEdit(ShortcutAction action)
        {
            InitializeComponent();
            this.key = new ControlParts.Timeline.Click.ShortcutKey(Keys.None, false, false);
            this.actions = Actions.None;
            this.userActions = UserActions.KeyboardShortcut;
            if (action != null)
            {
                this.key = action.shortcut_key;
                this.actions = action.action;
                this.userActions = action.user_action;
            }
            this.sourceBox.SelectedIndex = (int)this.userActions;
            this.actionBox.SelectedIndex = (int)this.actions;
            this.actionKeyBox.Text = key.KeyToString();
        }

        private void actionKeyBox_KeyDown(object sender, KeyEventArgs e)
        {
            key = new ControlParts.Timeline.Click.ShortcutKey(e.KeyCode, e.Control, e.Shift);
            this.actionKeyBox.Text = key.KeyToString();
            e.SuppressKeyPress = true;
        }

        private void sourceBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.userActions = (UserActions)this.sourceBox.SelectedIndex;
            this.actionKeyBox.Enabled = (this.userActions == UserActions.KeyboardShortcut);
        }

        private void actionBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.actions = (Actions)this.actionBox.SelectedIndex;

        }

        private void EOKButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            if (this.userActions == UserActions.MouseDoubleClick)
                this.key = new ControlParts.Timeline.Click.ShortcutKey(Keys.None, false, false);
            this.Tag = new ShortcutAction(this.actions, this.userActions, this.key);
        }

        private void ECancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void ShortcutKeyEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Escape)
                this.Close();
        }
    }
}
