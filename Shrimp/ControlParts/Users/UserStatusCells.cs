using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Shrimp.Twitter.Status;

namespace Shrimp.ControlParts.Users
{
	/// <summary>
	/// ユーザの一覧をすべて描画する
	/// </summary>
	class UserStatusCells : UserStatusCell
	{
		/// <summary>
		/// ユーザの表示開始位置
		/// </summary>
		private int showUserOffsetPosition = 0;
		private List<TwitterUserStatus> users;
		private List<UserStatusCellSize> userCellSizesTMP;

		public UserStatusCells(List<TwitterUserStatus> users)
		{
			this.users = users;
			this.showUserOffsetPosition = 0;
		}

		/// <summary>
		/// ユーザの一覧をすべて描画する
		/// </summary>
		/// <param name="g"></param>
		public void Draw(Graphics g, int maxWidth)
		{
			if (userCellSizesTMP == null)
				userCellSizesTMP = new List<UserStatusCellSize>();
			userCellSizesTMP.Clear();

			int offsetX = 0; int offsetY = 0;
			for (int i = showUserOffsetPosition; i < users.Count; i++)
			{
				var cell = this.DrawCell(g, users[i], maxWidth, offsetX, offsetY);
				offsetY += cell.CellRect.Bottom;
				userCellSizesTMP.Add (cell);
			}
		}
	}
}
