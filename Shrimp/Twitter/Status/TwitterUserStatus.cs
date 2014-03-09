using System;
using System.Net;
using Shrimp.Twitter.Entities;

namespace Shrimp.Twitter.Status
{
    public class TwitterUserStatus : ICloneable
    {
        public TwitterUserStatus()
        {
        }

        public TwitterUserStatus(string[] sqlData, int offset)
        {
            //  offset = 19
            this.created_at = DateTime.ParseExact(
                sqlData[offset],
                "ddd MMM dd HH:mm:ss K yyyy",
                System.Globalization.DateTimeFormatInfo.InvariantInfo);
            this.id = Decimal.Parse(sqlData[offset + 1]);
            this.name = (string)sqlData[offset + 2].Clone();
            this.screen_name = (string)sqlData[offset + 3].Clone();
            this.location = (string)sqlData[offset + 4].Clone();
            this.url = (string)sqlData[offset + 5].Clone();
            this.description = (string)sqlData[offset + 6].Clone();
            this.entities = new TwitterEntities(this.description);
            this.profile_image_url = (string)sqlData[offset + 7].Clone();
            this.favourites_count = Decimal.Parse(sqlData[offset + 8]);
            this.followers_count = Decimal.Parse(sqlData[offset + 9]);
            this.friends_count = Decimal.Parse(sqlData[offset + 10]);
            this.listed_count = Decimal.Parse(sqlData[offset + 11]);
            this.protected_account = (int.Parse(sqlData[offset + 12]) == 0 ? false : true);
            this.statuses_count = Decimal.Parse(sqlData[offset + 13]);
        }

        public TwitterUserStatus(dynamic raw_data)
        {
            this.created_at = DateTime.ParseExact(
                                        raw_data.created_at,
                                        "ddd MMM dd HH:mm:ss K yyyy",
                                        System.Globalization.DateTimeFormatInfo.InvariantInfo);
            this.id = Decimal.Parse(raw_data.id_str);
            this.name = raw_data.name;
            this.screen_name = raw_data.screen_name;
            this.location = raw_data.location;
            this.url = raw_data.url;
            this.description = WebUtility.HtmlDecode((raw_data.description == null ? "" : raw_data.description));
            this.entities = new TwitterEntities(this.description);
            this.profile_image_url = raw_data.profile_image_url;
            this.favourites_count = (decimal)raw_data.favourites_count;
            this.followers_count = (decimal)raw_data.followers_count;
            this.friends_count = (decimal)raw_data.friends_count;
            this.listed_count = (decimal)(raw_data.listed_count == null ? 0 : raw_data.listed_count);
            this.protected_account = (bool)raw_data["protected"];
            this.statuses_count = (decimal)raw_data.statuses_count;
        }


        public static string sqlCreate
        {
            get
            {
                return @"CREATE TABLE IF NOT EXISTS user (created_at, id primary key ON CONFLICT ignore, name, screen_name, location, url, description, profile_image_url, favorite_count, followers_count, friend_count, listed_count, protected, statuses_count );";
            }
        }

        public virtual string sqlInsert
        {
            get
            {
                /*
                string tmpLocation = WebUtility.HtmlEncode ( ( this.location == null ? "" : this.location ) ).Replace ( "'", "''" );
                string tmpDescription = WebUtility.HtmlEncode ( ( this.description == null ? "" : this.description ) ).Replace ( "'", "''" );
                string tmpName = WebUtility.HtmlEncode ( ( this.name == null ? "" : this.name ) ).Replace ( "'", "''" );
                string tmpURL = WebUtility.HtmlEncode ( ( this.url == null ? "" : this.url ) ).Replace ( "'", "''" );
                return @"insert into user (created_at, id, name, screen_name, location, url, description, profile_image_url, favorite_count, followers_count, friend_count, listed_count, protected, statuses_count ) values ( '" + created_at.ToString ( "ddd MMM dd HH:mm:ss K yyyy", new CultureInfo ( "en-US" ) ) + "', '" + id + "', '" + tmpName + "', '" + screen_name + "', '" + tmpLocation + "', '" + tmpURL + "', '" + tmpDescription + "', '" + profile_image_url + "', '" + favourites_count + "', '" + followers_count + "', '" + friends_count + "', '" + listed_count + "', '" + protected_account + "', '" + statuses_count + "' );";
                */
                return @"insert into user (created_at, id, name, screen_name, location, url, description, profile_image_url, favorite_count, followers_count, friend_count, listed_count, protected, statuses_count ) "
                + "values ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ? )";
            }
        }

        public virtual string AnySqlInsertFirst
        {
            get
            {
                return @"insert into user (created_at, id, name, screen_name, location, url, description, profile_image_url, favorite_count, followers_count, friend_count, listed_count, protected, statuses_count ) "
                + @"SELECT ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?";
            }
        }

        public virtual string AnySqlInsertEnd
        {
            get
            {
                return @" UNION"
                + @" SELECT ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?";
            }
        }

        /// <summary>
        /// 作成日
        /// </summary>
        public virtual DateTime created_at
        {
            get;
            set;
        }

        /// <summary>
        /// ユーザーID
        /// </summary>
        public virtual decimal id
        {
            get;
            set;
        }

        /// <summary>
        /// 名前
        /// </summary>
        public virtual string name
        {
            get;
            set;
        }

        /// <summary>
        /// スクリーンネーム
        /// </summary>
        public virtual string screen_name
        {
            get;
            set;
        }

        /// <summary>
        /// location
        /// </summary>
        public virtual string location
        {
            get;
            set;
        }

        /// <summary>
        /// url
        /// </summary>
        public virtual string url
        {
            get;
            set;
        }

        /// <summary>
        /// bio
        /// </summary>
        public virtual string description
        {
            get;
            set;
        }

        /// <summary>
        /// bioのエンティティ
        /// </summary>
        public virtual TwitterEntities entities
        {
            get;
            set;
        }


        /// <summary>
        /// アイコン画像パス
        /// </summary>
        public virtual string profile_image_url
        {
            get;
            set;
        }

        /// <summary>
        /// ふぁぼ数
        /// </summary>
        public virtual decimal favourites_count
        {
            get;
            set;
        }

        /// <summary>
        /// フォロワー数
        /// </summary>
        public virtual decimal followers_count
        {
            get;
            set;
        }

        /// <summary>
        /// フォロー数
        /// </summary>
        public virtual decimal friends_count
        {
            get;
            set;
        }

        /// <summary>
        /// リスト数
        /// </summary>
        public virtual decimal listed_count
        {
            get;
            set;
        }

        /// <summary>
        /// ツイート数
        /// </summary>
        public virtual decimal statuses_count
        {
            get;
            set;
        }

        /// <summary>
        /// カギ垢ですか？
        /// </summary>
        public virtual bool protected_account
        {
            get;
            set;
        }

        public object Clone()
        {
            var dest = new TwitterUserStatus();
            dest.created_at = this.created_at;
            dest.description = (string)this.description.Clone();
            dest.favourites_count = this.favourites_count;
            dest.followers_count = this.followers_count;
            dest.friends_count = this.friends_count;
            dest.id = this.id;
            dest.listed_count = this.listed_count;
            dest.location = (this.location != null ? (string)this.location.Clone() : "");
            dest.name = (string)this.name.Clone();
            dest.profile_image_url = (string)this.profile_image_url.Clone();
            dest.protected_account = this.protected_account;
            dest.screen_name = (string)this.screen_name.Clone();
            dest.statuses_count = this.statuses_count;
            dest.url = (this.url != null ? (string)this.url.Clone() : "");
            dest.entities = new TwitterEntities(dest.description);
            return dest;
        }
    }
}
