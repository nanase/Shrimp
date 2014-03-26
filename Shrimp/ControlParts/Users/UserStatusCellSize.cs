using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shrimp.Module.Parts;
using Shrimp.Twitter.Status;
using System.Drawing;

namespace Shrimp.ControlParts.Users
{
	/// <summary>
	/// ユーザ情報を表示するセルの位置を取得する
	/// </summary>
	class UserStatusCellSize
	{
		public Cell icon
		{
			get;
			set;
		}

		public Cell name
		{
			get;
			set;
		}

		public Cell url
		{
			get;
			set;
		}

		public Cell follow
		{
			get;
			set;
		}

		public Cell follower
		{
			get;
			set;
		}

		public Cell favorites
		{
			get;
			set;
		}

		public Cell description
		{
			get;
			set;
		}

		public int offsetX
		{
			get;
			set;
		}

		public int offsetY
		{
			get;
			set;
		}

		public int maxWidth
		{
			get;
			set;
		}

		/// <summary>
		/// セルの大きさを取得
		/// </summary>
		/// <param name="offsetX">表示開始X</param>
		/// <param name="offsetY">表示開始Y</param>
		/// <param name="maxWidth">最大横幅</param>
		/// <returns></returns>
		public Rectangle CellRect
		{
			get
			{
				return new Rectangle(offsetX, offsetY, maxWidth, description.Rect.Bottom + offsetY);
			}
		}

		public UserStatusCellSize(TwitterUserStatus user, int maxWidth, int offsetX, int offsetY)
		{
			this.offsetX = offsetX;
			this.offsetY = offsetY;
			this.maxWidth = maxWidth;

			//	アイコンの位置
			this.icon = new Cell(new Point(5 + offsetX, 5 + offsetY), new Size(48, 48), null);

			//	名前の位置
			var tmp = "@"+ user.screen_name +" / "+ user.name +"";
			this.name = new Cell(new Point(this.icon.Rect.Right + 5, this.icon.Position.Y),
				DrawTextUtil.GetDrawTextSize(tmp, Setting.Fonts.NameFont, maxWidth), tmp);

			//	URLの位置
			tmp = "URL: "+ user.url;
			this.url = new Cell(new Point(this.icon.Rect.Right + 5, this.name.Rect.Bottom + 5),
				DrawTextUtil.GetDrawTextSize(tmp, Setting.Fonts.ViaFont, maxWidth), tmp);

			//	フォローの位置
			tmp = "フォロー数: "+ user.friends_count;
			this.follow = new Cell(new Point(this.icon.Rect.Right + 5, this.url.Rect.Bottom + 5),
				DrawTextUtil.GetDrawTextSize(tmp, Setting.Fonts.ViaFont, maxWidth), tmp);

			//	フォロワーの位置
			tmp = "フォロワー数: "+ user.followers_count;
			this.follower = new Cell(new Point(this.follow.Rect.Right + 5, this.follow.Rect.Y),
				DrawTextUtil.GetDrawTextSize(tmp, Setting.Fonts.ViaFont, maxWidth), tmp);

			//	ふぁぼりての位置
			tmp = "お気に入り数: "+ user.favourites_count;
			this.favorites = new Cell(new Point(this.follower.Rect.Right + 5, this.follow.Rect.Y),
				DrawTextUtil.GetDrawTextSize(tmp, Setting.Fonts.ViaFont, maxWidth), tmp);

			//	詳細の位置
			tmp = user.description;
			this.description = new Cell(new Point(this.icon.Rect.Right + 5, this.follow.Rect.Bottom + 5),
				DrawTextUtil.GetOwnerDrawTextSize(tmp, Setting.Fonts.TweetFont, 0, maxWidth), tmp);

		}
	}
}
