using System;
using System.Net;
using Shrimp.Twitter.Entities;

namespace Shrimp.Twitter.Status
{
    /// <summary>
    /// ダイレクトメッセージのデータ
    /// </summary>
    public class TwitterDirectMessageStatus : TwitterStatus
    {
        public TwitterDirectMessageStatus(dynamic data)
            : base(true)
        {
            this.id = Decimal.Parse(data.id_str);
            this.created_at = DateTime.ParseExact(
                                data.created_at,
                                "ddd MMM dd HH:mm:ss K yyyy",
                                System.Globalization.DateTimeFormatInfo.InvariantInfo);
            this.user = new TwitterUserStatus(data.sender);
            this.text = WebUtility.HtmlDecode(data.text);
            this.entities = new Entities.TwitterEntities(this.text);
            this.source = "Shrimp Man";
            this.source_url = "http://google.co.jp";
        }

        public TwitterDirectMessageStatus(string[] sqlData)
            : base(true)
        {
            this.created_at = DateTime.ParseExact(
                                sqlData[0],
                                "ddd MMM dd HH:mm:ss K yyyy",
                                System.Globalization.DateTimeFormatInfo.InvariantInfo);
            this.id = Decimal.Parse(sqlData[1]);
            this.text = (string)sqlData[2].Clone();
            this.entities = new TwitterEntities(this.text);
            this.source = (string)sqlData[3].Clone();
            this.source_url = (string)sqlData[4].Clone();
            this.user = new TwitterUserStatus(sqlData, 6);

        }

        public static string DBsqlCreate
        {
            get
            {
                return @"CREATE TABLE IF NOT EXISTS directMessages(created_at, id primary key ON CONFLICT ignore, text, source, source_url, user_id );";
            }
        }

        public override string sqlInsert
        {
            get
            {
                return @"insert into directMessages (created_at, id, text, source, source_url, user_id)"
                + @" values ( ?, ?, ?, ?, ?, ? )";
            }
        }

        public override string AnySqlInsertFirst
        {
            get
            {
                return @"insert into directMessages ( created_at, id, text, source, source_url, user_id)"
                + @" SELECT ?, ?, ?, ?, ?, ?";
            }
        }

        public override string AnySqlInsertEnd
        {
            get
            {
                return @" UNION"
                + @" SELECT ?, ?, ?, ?, ?, ?";
            }
        }
    }
}
