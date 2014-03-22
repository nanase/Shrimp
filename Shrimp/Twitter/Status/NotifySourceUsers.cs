using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shrimp.Twitter.Status
{
	/// <summary>
	/// お気に入りに登録したユーザや、リツイートしたユーザの情報をまとめるクラス
	/// </summary>
	public class NotifySourceUsers : ICloneable
	{
		private List<TwitterUserStatus> users;

		public NotifySourceUsers()
		{
			this.users = new List<TwitterUserStatus> ();
		}

		public NotifySourceUsers(List<TwitterUserStatus> users)
		{
			this.users = new List<TwitterUserStatus> ();
			if (users != null)
			{
				this.users.AddRange(users);
			}
		}

		public void AddNotifySourceUser(TwitterUserStatus user)
		{
			this.users.Add(user);
		}

		public void RemoveNotifySourceUser(TwitterUserStatus user)
		{
			this.users.Remove(user);
		}

		public object Clone()
		{
			var dest = new NotifySourceUsers(this.users);
			return dest;
		}
	}
}
