using System.Drawing;
using Shrimp.Module.Parts;
using Shrimp.Twitter.Status;

namespace Shrimp.ControlParts.User
{
    class UserStatusCells : UserStatusCellSize
    {
        /// <summary>
        /// レイアウトの配置を取得
        /// </summary>
        /// <param name="maxWidth">最大幅</param>
        /// <param name="icon">アイコン</param>
        /// <param name="users">ユーザー情報</param>
        /// <param name="name">名前フォント</param>
        /// <param name="screen_name">スクリーンネームフォント</param>
        /// <param name="bio">bioフォント</param>
        public void getLayout(int maxWidth, Bitmap icon, TwitterUserStatus users)
        {
            if (users == null)
                return;
            this.icon = new Cell(new Point(5, 5), icon.Size, null);
            this.name = new Cell(new Point(this.icon.Rect.Width + 5, this.icon.Position.Y),
                                    DrawTextUtil.GetDrawTextSize(users.name, Setting.Fonts.NameFont, maxWidth, false, true),
                                    users.name);
            this.screen_name = new Cell(new Point(this.name.Position.X, this.name.Rect.Bottom + 5),
                                    DrawTextUtil.GetDrawTextSize("@" + users.screen_name, Setting.Fonts.TweetFont, maxWidth, false, true),
                                    "@" + users.screen_name);

            this.tweet_count = new Cell(new Point(this.icon.Position.X, this.screen_name.Rect.Bottom + 5),
                                    DrawTextUtil.GetDrawTextSize("ツイート数:" + users.statuses_count + "", Setting.Fonts.NameFont, maxWidth, false, true),
                                    "ツイート数:" + users.statuses_count + "");

            this.following_count = new Cell(new Point(this.icon.Position.X, this.tweet_count.Rect.Bottom),
                                    DrawTextUtil.GetDrawTextSize("フォロー数:" + users.friends_count + "", Setting.Fonts.NameFont, maxWidth, false, true),
                                    "フォロー数:" + users.friends_count + "");

            this.follower_count = new Cell(new Point(this.icon.Position.X, this.following_count.Rect.Bottom),
                                    DrawTextUtil.GetDrawTextSize("フォロワー数:" + users.followers_count + "", Setting.Fonts.NameFont, maxWidth, false, true),
                                    "フォロワー数:" + users.followers_count + "");

            this.favorites_count = new Cell(new Point(this.icon.Position.X, this.follower_count.Rect.Bottom),
                                    DrawTextUtil.GetDrawTextSize("ふぁぼ数:" + users.favourites_count + "", Setting.Fonts.NameFont, maxWidth, false, true),
                                    "ふぁぼ数:" + users.favourites_count + "");

            this.listed_count = new Cell(new Point(this.icon.Position.X, this.favorites_count.Rect.Bottom),
                        DrawTextUtil.GetDrawTextSize("リスト数:" + users.listed_count + "", Setting.Fonts.NameFont, maxWidth, false, true),
                        "リスト数:" + users.listed_count + "");

            this.AboutUser = new Cell(new Point(this.icon.Position.X, this.listed_count.Rect.Bottom),
                        DrawTextUtil.GetDrawTextSize("このユーザについて", Setting.Fonts.NameFont, maxWidth, false, true),
                        "このユーザについて");

            this.bio = new Cell(new Point(this.icon.Position.X, this.AboutUser.Rect.Bottom + 5),
                                    DrawTextUtil.GetOwnerDrawTextSize(users.description, Setting.Fonts.RetweetNotify, this.icon.Position.X, maxWidth),
                                    users.description);

            this.created_at = new Cell(new Point(this.icon.Position.X, this.bio.Rect.Bottom + 5),
                        DrawTextUtil.GetDrawTextSize("Twitter開始時期:" + users.created_at.ToLongDateString() + "", Setting.Fonts.NameFont, maxWidth, false, true),
                        "Twitter開始時期:" + users.created_at.ToLongDateString() + "");
        }

    }
}
