using System;
using Shrimp.Twitter.Entities;
using Shrimp.Module;
using System.Net;
using System.Globalization;

namespace Shrimp.Twitter.Status
{
    /// <summary>
    /// ツイートクラス
    /// </summary>
    public class TwitterStatus : IComparable, ICloneable
    {
        public readonly static DateTime dtUnixEpoch = new DateTime ( 1970, 1, 1, 0, 0, 0, DateTimeKind.Utc );

        public TwitterStatus ()
        {
        }

        public TwitterStatus ( bool isDirectMessage )
        {
            this.isDirectMessage = isDirectMessage;
        }

        public void SetReply ( decimal user_id )
        {
            this.isReply = true;
            this.replyID = user_id;
        }

        public TwitterStatus ( dynamic raw_data )
        {
            this.GenerateTwitterStatus ( raw_data );
        }

        /// <summary>
        /// SQLを変換する
        /// </summary>
        /// <param name="sqlData"></param>
        public TwitterStatus ( string[] sqlData )
        {
            // user_id, is_retweeted_status, retweeted_by_user_id, retweeted_by_screen_name, retweeted_by_name, retweeted_by_id, retweeted_by_created_at 
            var is_RetweetedStatus = (int.Parse ( sqlData[13] ) == 0 ? false : true );
            if ( is_RetweetedStatus )
            {
                this.retweeted_status = new TwitterStatus ();

                this.retweeted_status.created_at = DateTime.ParseExact (
                                sqlData[0],
                                "ddd MMM dd HH:mm:ss K yyyy",
                                System.Globalization.DateTimeFormatInfo.InvariantInfo );
                this.retweeted_status.id = Decimal.Parse ( sqlData[1] );
                this.retweeted_status.in_reply_to_status_id = Decimal.Parse ( sqlData[2] );
                this.retweeted_status.in_reply_to_user_id = Decimal.Parse ( sqlData[3] );
                this.retweeted_status.text = (string)sqlData[4].Clone ();
                this.retweeted_status.source = (string)sqlData[5].Clone ();
                this.retweeted_status.source_url = (string)sqlData[6].Clone ();
                this.retweeted_status.retweet_count = Decimal.Parse ( sqlData[7] );
                this.retweeted_status.favorite_count = Decimal.Parse ( sqlData[8] );

                this.retweeted_status.isReply = ( int.Parse ( sqlData[9] ) == 0 ? false : true );
                this.retweeted_status.replyID = Decimal.Parse ( sqlData[10] );
                this.retweeted_status.isDirectMessage = ( int.Parse ( sqlData[11] ) == 0 ? false : true );

                //  作成
                this.retweeted_status.entities = new TwitterEntities ( this.retweeted_status.text );
                this.retweeted_status.user = new TwitterUserStatus ( sqlData, 19 );
                this.user = new TwitterUserStatus ();
                this.user.id = Decimal.Parse ( sqlData[14] );
                this.user.screen_name = (string)sqlData[15].Clone ();
                this.user.name = (string)sqlData[16].Clone ();
                this.id = Decimal.Parse ( sqlData[17] );
                this.text = (string)this.retweeted_status.text.Clone ();
                this.created_at = DateTime.ParseExact (
                                sqlData[18],
                                "ddd MMM dd HH:mm:ss K yyyy",
                                System.Globalization.DateTimeFormatInfo.InvariantInfo );
            }
            else
            {
                this.created_at = DateTime.ParseExact (
                                sqlData[0],
                                "ddd MMM dd HH:mm:ss K yyyy",
                                System.Globalization.DateTimeFormatInfo.InvariantInfo );
                this.id = Decimal.Parse ( sqlData[1] );
                this.in_reply_to_status_id = Decimal.Parse ( sqlData[2] );
                this.in_reply_to_user_id = Decimal.Parse ( sqlData[3] );
                this.text = (string)sqlData[4].Clone ();
                this.source = (string)sqlData[5].Clone ();
                this.source_url = (string)sqlData[6].Clone ();
                this.retweet_count = Decimal.Parse ( sqlData[7] );
                this.favorite_count = Decimal.Parse ( sqlData[8] );
                this.entities = new TwitterEntities ( this.text );

                this.isReply = ( int.Parse ( sqlData[9] ) == 0 ? false : true );
                this.replyID = Decimal.Parse ( sqlData[10] );
                this.isDirectMessage = ( int.Parse ( sqlData[11] ) == 0 ? false : true );

                this.user = new TwitterUserStatus ( sqlData, 19 );
            }
            //  ユーザー
        }

        private void GenerateTwitterStatus ( dynamic raw_data )
        {
            this.user = new TwitterUserStatus ( raw_data.user );
            this.created_at = DateTime.ParseExact (
                            raw_data.created_at,
                            "ddd MMM dd HH:mm:ss K yyyy",
                            System.Globalization.DateTimeFormatInfo.InvariantInfo );
            this.id = Decimal.Parse(raw_data.id_str);
            this.text = WebUtility.HtmlDecode(raw_data.text);
            string[] parse_res = RegexUtil.ParseVia ( raw_data.source );
            if ( parse_res != null )
            {
                this.source_url = parse_res[0];
                this.source = ""+ parse_res[1] +"";
            }
            else
            {
                this.source = "Web";
                this.source_url = "http://www.twitter.com";
            }
            this.favorite_count = (decimal)raw_data.favorite_count;
            this.favorited = (bool)raw_data.favorited;
            this.in_reply_to_status_id = ( raw_data.in_reply_to_status_id == null ? 0 : Decimal.Parse(raw_data.in_reply_to_status_id_str) );
            this.in_reply_to_user_id = ( raw_data.in_reply_to_user_id == null ? 0 : Decimal.Parse ( raw_data.in_reply_to_user_id_str ) );
            this.retweet_count = (decimal)raw_data.retweet_count;
            this.retweeted = (bool)raw_data.retweeted;
            this.retweeted_status = ( raw_data.IsDefined ( "retweeted_status" ) ? new TwitterStatus ( raw_data.retweeted_status ) : null );
            if ( raw_data.IsDefined ( "entities" ) )
            {
                this.text = TwitterEntitiesUtil.getCorrectURL ( this.text, raw_data.entities );
            }
            this.entities = new TwitterEntities ( this.text );
        }

        private void GenerateDummyTwitterStatus ()
        {
            this.created_at = DateTime.Now;
            this.id = System.DateTime.Now.Ticks;
            this.source = "ShrimpMan";
            this.source_url = "http://www.twitter.com";
        }

        public TwitterStatus ( TwitterNotifyStatus status )
        {
            //  通知をツイート化します
            this.NotifyStatus = status;
            if ( status.notify_event == "favorite" || status.notify_event == "unfavorite" )
            {
                GenerateTwitterStatus ( status.target_object );
                status.target_object = new TwitterStatus ( status.target_object );
                this.id = System.DateTime.Now.Ticks;
                if ( status.isFav )
                {
                    this.text = "【お気に入り通知】@" + status.source.screen_name + "がお気に入り追加しました\n" + this.text + "";
                }
                if ( status.isFaved )
                {
                    this.text = "【お気に入り通知】@" + status.source.screen_name + "にお気に入り追加されました\n" + this.text + "";
                }
                if ( status.isUnFav )
                {
                    this.text = "【お気に入り削除通知】@" + status.source.screen_name + "がお気に入りから削除しました\n" + this.text + "";
                }
                if ( status.isUnFaved )
                {
                    this.text = "【お気に入り削除通知】お気に入りから削除しました\n" + this.text + "";
                }
                this.entities = new TwitterEntities ( this.text );
                //this.text += ( status.notify_event == "favorite" ? "がふぁぼられました" : "があんふぁぼられました" );
            }
            else if ( status.notify_event == "follow" || status.notify_event == "unfollow" )
            {
                GenerateDummyTwitterStatus ();
                var s = status.source as TwitterUserStatus;
                var t = status.target as TwitterUserStatus;
                this.user = s;
                this.text = "【フォロー通知】\n@" + t.screen_name + "("+ t.name +")を" + ( status.notify_event == "follow" ? "フォロー" : "アンフォロー" ) + "しました";
                this.entities = new TwitterEntities ( this.text );
            }
        }

        /// <summary>
        /// リツイートかどうかを考慮せずに、ツイートを拾える
        /// </summary>
        public virtual TwitterStatus DynamicTweet
        {
            get {
                return ( this.retweeted_status != null ? this.retweeted_status : this );
            }
        }

        /// <summary>
        /// 通知かどうかを考慮せずに、ツイートを拾える
        /// </summary>
        public virtual TwitterStatus DynamicNotifyTweet
        {
            get
            {
                if ( this.NotifyStatus != null && this.NotifyStatus.target_object != null && this.NotifyStatus.target_object is TwitterStatus )
                {
                    var tweet = this.NotifyStatus.target_object as TwitterStatus;
                    return ( tweet.retweeted_status != null ? tweet.retweeted_status : tweet );
                }
                else
                {
                    return ( this.retweeted_status != null ? this.retweeted_status : this );
                }
            }
        }

        /// <summary>
        /// フォロー通知ですか？
        /// </summary>
        public virtual bool isFollowing
        {
            get
            {
                return ( this.NotifyStatus != null && this.NotifyStatus.isFollow );
            }
        }

        /// <summary>
        /// イメージ数
        /// </summary>
        public virtual int media_count
        {
            get
            {
                if ( this.entities != null && this.entities.media != null )
                {
                    return this.entities.media.Count;
                }
                else
                {
                    return 0;
                }
            }
        }

        public static string sqlCreate
        {
            get
            {
                return @"CREATE TABLE IF NOT EXISTS tweet(created_at, id primary key ON CONFLICT ignore, in_reply_to_status_id, in_reply_to_user_id, text, source, source_url, retweet_count, favorite_count, isReply, ReplySourceUserID, isDirectMessage, user_id, is_retweeted_status, retweeted_by_user_id, retweeted_by_screen_name, retweeted_by_name, retweeted_by_id, retweeted_by_created_at );";
            }
        }

        public virtual string sqlInsert
        {
            get
            {
                /*
                var t = this.DynamicTweet;
                string text_tmp = ( t.text == null ? "" : WebUtility.HtmlEncode ( t.text ).Replace("'","''") );
                string via_tmp = ( t.source == null ? "" : WebUtility.HtmlEncode ( t.source ).Replace ( "'", "''" ) );
                string user_name_tmp = ( t.user.name == null ? "" : WebUtility.HtmlEncode ( t.user.name ).Replace ( "'", "''" ) );
                 return @"insert into tweet (created_at, id, in_reply_to_status_id, in_reply_to_user_id, text, source, source_url, retweet_count, favorite_count, user_id, is_retweeted_status, retweeted_by_user_id, retweeted_by_screen_name, retweeted_by_name, retweeted_by_id, retweeted_by_created_at )"
                + @" values ( '" + t.created_at.ToString ( "ddd MMM dd HH:mm:ss K yyyy", new CultureInfo ( "en-US" ) ) + "', '" + t.id + "',"
                + @"'"+ t.in_reply_to_status_id +"', '"+ t.in_reply_to_user_id +"', '"+ text_tmp +"', '"+ via_tmp +"', '"+ t.source_url +"', "+ t.retweet_count +", "+ t.favorite_count +", '"+ t.user.id +"', '"+ ( this.retweeted_status != null ) +"', '"+ ( this.retweeted_status != null ? this.user.id : 0 ) +"', '"+ this.user.screen_name +"', '"+ user_name_tmp +"', '"+ this.id +"', '"+ this.created_at.ToString ( "ddd MMM dd HH:mm:ss K yyyy", new CultureInfo ( "en-US" ) ) +"' );";
                */

                return @"insert into tweet (created_at, id, in_reply_to_status_id, in_reply_to_user_id, text, source, source_url, retweet_count, favorite_count, isReply, ReplySourceUserID, isDirectMessage, user_id, is_retweeted_status, retweeted_by_user_id, retweeted_by_screen_name, retweeted_by_name, retweeted_by_id, retweeted_by_created_at )"
                + @" values ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ? )";
            }
        }

        public virtual string AnySqlInsertFirst
        {
            get
            {
                return @"insert into tweet (created_at, id, in_reply_to_status_id, in_reply_to_user_id, text, source, source_url, retweet_count, favorite_count, isReply, ReplySourceUserID, isDirectMessage, user_id, is_retweeted_status, retweeted_by_user_id, retweeted_by_screen_name, retweeted_by_name, retweeted_by_id, retweeted_by_created_at )"
                + @" SELECT ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?";
            }
        }

        public virtual string AnySqlInsertEnd
        {
            get
            {
                return @" UNION"
                + @" SELECT ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?";
            }
        }

        /// <summary>
        /// ユーザ
        /// </summary>
        public virtual TwitterUserStatus user
        {
            get;
            set;
        }

        /// <summary>
        /// 通知ツイートなの？
        /// 
        /// </summary>
        public virtual TwitterNotifyStatus NotifyStatus
        {
            get; set;
        }

        /// <summary>
        /// 通知ツイート？
        /// </summary>
        public virtual bool isNotify
        {
            get
            {
                return this.NotifyStatus != null;
            }
        }

        /// <summary>
        /// ダイレクトメッセージ？
        /// </summary>
        public virtual bool isDirectMessage
        {
            get;
            set;
        }


        /// <summary>
        /// 作成日時
        /// </summary>
        public virtual DateTime created_at
        {
            get;
            set;
        }

        /// <summary>
        /// ツイートID
        /// </summary>
        public virtual decimal id
        {
            get;
            set;
        }

        /// <summary>
        /// entities
        /// </summary>
        public virtual TwitterEntities entities
        {
            get;
            set;
        }

        /// <summary>
        /// in_reply_to_status_id
        /// </summary>
        public virtual decimal in_reply_to_status_id
        {
            get;
            set;
        }

        /// <summary>
        /// in_reply_to_user_id
        /// </summary>
        public virtual decimal in_reply_to_user_id
        {
            get;
            set;
        }

        /// <summary>
        /// 独自パラメータ。タイムラインによって異なるが、リプライである場合は、これをtrueにする
        /// </summary>
        public virtual bool isReply
        {
            get;
            set;
        }
        public virtual decimal replyID
        {
            get;
            set;
        }

        /// <summary>
        /// テキスト
        /// </summary>
        public virtual string text
        {
            get;
            set;
        }

        /// <summary>
        /// via
        /// </summary>
        public virtual string source
        {
            get;
            set;
        }

        /// <summary>
        /// via URL
        /// </summary>
        public virtual string source_url
        {
            get;
            set;
        }

        /// <summary>
        /// リツイート回数
        /// </summary>
        public virtual decimal retweet_count
        {
            get; set;
        }

        /// <summary>
        /// ふぁぼ数
        /// </summary>
        public virtual decimal favorite_count
        {
            get; set;
        }

        /// <summary>
        /// すでにふぁぼられてる？
        /// </summary>
        public virtual bool favorited
        {
            get; set;
        }

        /// <summary>
        /// すでにリツイートされてる？
        /// </summary>
        public virtual bool retweeted
        {
            get; set;
        }

        /// <summary>
        /// リツイートされた内容
        /// </summary>
        public virtual TwitterStatus retweeted_status
        {
            get; set;
        }

        /// <summary>
        /// リツイートされたもの？
        /// </summary>
        public virtual bool isRetweet
        {
            get
            {
                return this.retweeted_status != null;
            }
        }

        public int CompareTo ( object obj )
        {
            if ( obj == null )
                return 1;

            TwitterStatus a = obj as TwitterStatus;
            if ( a.id > this.id )
                return 0;
            else
                return 1;
        }

        public object Clone ()
        {
            var dest = new TwitterStatus ();
            dest.id = this.id;
            dest.created_at = this.created_at;
            dest.favorite_count = this.favorite_count;
            dest.favorited = this.favorited;
            dest.in_reply_to_status_id = this.in_reply_to_status_id;
            dest.in_reply_to_user_id = this.in_reply_to_user_id;
            dest.isDirectMessage = this.isDirectMessage;
            dest.isReply = this.isReply;
            dest.NotifyStatus = ( this.NotifyStatus != null ? (TwitterNotifyStatus)this.NotifyStatus.Clone () : null );
            dest.replyID = this.replyID;
            dest.retweet_count = this.retweet_count;
            dest.retweeted = this.retweeted;
            dest.retweeted_status = (this.retweeted_status != null ? (TwitterStatus)this.retweeted_status.Clone () : null );
            dest.source = (string)this.source.Clone ();
            dest.source_url = (string)this.source_url.Clone ();
            dest.text = (string)this.text.Clone ();
            dest.entities = new TwitterEntities ( dest.text );
            dest.user = (TwitterUserStatus)this.user.Clone ();
            return dest;
        }
    }
}
