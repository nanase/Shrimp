using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Shrimp.Twitter.Status;

namespace Shrimp.ControlParts.Users
{
	public partial class Form1 : Form
	{
		private UserStatusCells cells;
		public Form1()
		{
			InitializeComponent();

			var tmp = new List<TwitterUserStatus>();
			tmp.Add(new TwitterUserStatus()
			{
				name = "hoge",
				screen_name = "hoge",
				url = "http://hoge",
				followers_count = 100,
				friends_count = 100,
				favourites_count = 100,
				description = "hoge",
				profile_image_url = "http://google.com"
			});
			tmp.Add(new TwitterUserStatus()
			{
				name = "hoge2",
				screen_name = "hoge2",
				url = "http://hoge",
				followers_count = 10000,
				friends_count = 10000,
				favourites_count = 100000000000,
				description = "ほげっほげっほげっほげっほげっほげっ",
				profile_image_url = "http://google.com"
			});
			cells = new UserStatusCells(tmp);
		}

		private void Form1_Paint(object sender, PaintEventArgs e)
		{
			cells.Draw(e.Graphics, this.Width);
		}
	}
}
