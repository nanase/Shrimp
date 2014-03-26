using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Shrimp.ControlParts.TweetBox
{
    class TextBoxAC : TextBox
    {
        private AutoCompleteForm acf;
        public List<string> list = new List<string>();
        bool _listShow = false;
        bool isScreenName = false;
        int offset = 0;
        bool EnableAutoComplete = true;

        public TextBoxAC()
        {
            this.acf = new AutoCompleteForm();
            this.acf.Visible = false;
            this.acf.OnDoubleClickedItem += new EventHandler(itemDoubleClicked);
            //this.Controls.Add ( this.acf );
            /*
            this.acf.AddWord("@jaga_nyan", true);
            this.acf.AddWord("@ulicknormanowen", true);
            this.acf.AddWord("@jaga_nyan", true);
            this.acf.AddWord("@ulicknormanowen", true);
            this.acf.AddWord("#hage", false);
            this.acf.AddWord("#hoge", false);
            this.acf.AddWord("#hage", false);
            this.acf.AddWord("#hoge", false);
            */
        }

        public void AddWord ( string text, bool isScreenName )
        {
            this.acf.AddWord ( text, isScreenName );
        }

        public void AddWordRange ( List<string> text, bool isScreenName )
        {
            this.acf.AddWordRange ( text, isScreenName );
        }

        public bool listShow
        {
            get { return this._listShow; }
            set
            {
                if (!value)
                {
                    this.offset = 0;
                }
                this._listShow = value;
            }
        }

        [DllImport("user32.dll")]
        static extern bool GetCaretPos(out Point point);

        public Point GetCaretPosition()
        {
            Point p;
            GetCaretPos(out p);
            return p;
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            if ( !EnableAutoComplete )
                return;

            if (this.listShow)
            {
                if ( this.offset >= this.Text.Length )
                {
                    return;
                }
                string repSourceText = this.Text.Substring(this.offset);
                var tmpStr = this.searchStr(repSourceText + e.KeyChar.ToString());
                if (this.acf.SetItems(tmpStr, isScreenName) != 0)
                {
                    this.acf.ShowWithoutActive();
                }
                else
                {
                    this.acf.Hide();
                    this.listShow = false;
                }
                this.Focus();
            }

            //  @から候補が始まる
            if (e.KeyChar == '@' || e.KeyChar == '#')
            {
                this.listShow = true;
                this.isScreenName = (e.KeyChar == '@' ? true : false);
                this.offset = this.SelectionStart;
                Point point = this.GetCaretPosition();
                point.Y += (int)Math.Ceiling(this.Font.GetHeight()); //13 is the .y postion of the richtectbox
                this.acf.Location = this.PointToScreen(point);
                this.acf.SetItems(e.KeyChar.ToString(), this.isScreenName);
                this.acf.ShowWithoutActive();
                this.acf.SelectedIndex = 0;
                this.Focus();
            }
        }

        /// <summary>
        /// リスト位置をリセット
        /// </summary>
        public void ResetListPosition ()
        {
            if ( this.listShow )
            {
                this.offset = this.SelectionStart;
                Point point = this.GetCaretPosition ();
                point.Y += (int)Math.Ceiling ( this.Font.GetHeight () ); //13 is the .y postion of the richtectbox
                this.acf.Location = this.PointToScreen ( point );
            }
        }

        public void ShowAutoComplete(bool isScreenName)
        {
            if ( !EnableAutoComplete )
                return;

            if (this.listShow)
                return;

            this.listShow = true;
            this.isScreenName = isScreenName;
            this.SelectionLength = 0;
            this.offset = this.SelectionStart;
            Point point = this.GetCaretPosition();
            point.Y += (int)Math.Ceiling(this.Font.GetHeight()); //13 is the .y postion of the richtectbox
            this.acf.Location = this.PointToScreen(point);
            this.acf.SetItems((isScreenName ? "@" : "#"), this.isScreenName);
            this.acf.ShowWithoutActive();
            this.acf.SelectedIndex = 0;
            this.Focus();

        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if ( !EnableAutoComplete )
                return;

            if (e.Control || e.Shift || e.Alt)
                return;

            if (e.KeyCode == Keys.Space)
            {
                this.listShow = false;
                this.acf.Hide();
            }

            if (listShow)
            {
                if (e.KeyCode == Keys.Back)
                {
                    if (offset >= this.TextLength - 1)
                    {
                        listShow = false;
                        this.acf.Hide();
                    }
                }

                if (e.KeyCode == Keys.Up)
                {
                    this.acf.ShowWithoutActive();
                    this.acf.SelectedIndex--;
                    this.Focus();

                }
                else if (e.KeyCode == Keys.Down)
                {
                    this.acf.ShowWithoutActive();
                    this.acf.SelectedIndex++;
                    this.Focus();
                }

                if (e.KeyCode == Keys.Enter)
                {
                    if (this.acf.SelectedItem == null)
                        return;
                    string autoText = (string)this.acf.SelectedItem.Clone();
                    autoText += " ";

                    if ( this.Text.Length <= this.offset )
                        return;
                    string repSourceText = this.Text.Substring(this.offset);

                    StringBuilder sb = new System.Text.StringBuilder(this.Text);
                    var tmpStr = this.searchStr(repSourceText);
                    if (tmpStr != "")
                    {
                        sb.Replace(tmpStr, autoText, this.offset, tmpStr.Length);

                        this.Text = sb.ToString();
                        this.Select(this.offset + autoText.Length, 0);
                    }
                    this.listShow = false;
                    this.acf.Hide();
                    e.SuppressKeyPress = true;
                }
            }
        }

        private string searchStr(string text)
        {
            string tmpStr = "";
            foreach (char tmp in text)
            {
                if (char.IsWhiteSpace(tmp))
                    break;
                tmpStr += tmp.ToString();
            }
            return tmpStr;
        }

        private void itemDoubleClicked(object sender, EventArgs e)
        {
            if (this.acf.SelectedItem == null)
                return;
            string autoText = (string)this.acf.SelectedItem.Clone();
            autoText += " ";

            string repSourceText = this.Text.Substring(this.offset);

            StringBuilder sb = new System.Text.StringBuilder(this.Text);
            var tmpStr = this.searchStr(repSourceText);
            if (tmpStr != "")
            {
                sb.Replace(tmpStr, autoText, this.offset, tmpStr.Length);

                this.Text = sb.ToString();
                this.Select(this.offset + autoText.Length, 0);
            }
            this.listShow = false;
            this.acf.Hide();
        }
    }
}
