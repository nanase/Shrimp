using Shrimp.Twitter.Status;
using System.Drawing;
using Shrimp.Module.ImageUtil;
using Shrimp.Setting;

namespace Shrimp.ControlParts.Users
{
	/// <summary>
	/// ユーザを描画するセル
	/// </summary>
	class UserStatusCell
	{
		/// <summary>
		/// ユーザのセルを描画します
		/// </summary>
		/// <param name="g">描画先グラフィックスオブジェクト</param>
		/// <param name="user">ユーザの情報源</param>
		/// <param name="offsetX">描画開始位置X</param>
		/// <param name="offsetY">描画開始位置Y</param>
		public UserStatusCellSize DrawCell(Graphics g, TwitterUserStatus user, int maxWidth, int offsetX, int offsetY)
		{
			UserStatusCellSize cellSize = new UserStatusCellSize(user, maxWidth, offsetX, offsetY);

			var tmpIcon = (Bitmap)ResourceImages.LoadingImage.Clone();
			g.DrawImage(tmpIcon, cellSize.icon.Rect);
			g.DrawString(cellSize.name.Detail, Setting.Fonts.NameFont, Brushes.Black, cellSize.name.Rect);
			g.DrawString(cellSize.url.Detail, Setting.Fonts.ViaFont, Brushes.Black, cellSize.url.Rect);
			g.DrawString(cellSize.follow.Detail, Setting.Fonts.ViaFont, Brushes.Black, cellSize.follow.Rect);
			g.DrawString(cellSize.follower.Detail, Setting.Fonts.ViaFont, Brushes.Black, cellSize.follower.Rect);
			g.DrawString(cellSize.favorites.Detail, Setting.Fonts.ViaFont, Brushes.Black, cellSize.favorites.Rect);
			g.DrawString(cellSize.description.Detail, Setting.Fonts.TweetFont, Brushes.Black, cellSize.description.Rect);

			return cellSize;
		}
	}
}
