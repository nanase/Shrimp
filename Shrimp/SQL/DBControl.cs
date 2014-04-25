using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Globalization;
using Shrimp.Twitter.Status;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace Shrimp.SQL
{
    /// <summary>
    /// Shrimpのデータベース操作関数軍です
    /// </summary>
    public class DBControl : IDisposable
    {
        private SQLiteConnection sql;
        private SQLiteCommand command;
        private Queue<SQLiteCommand> commandStack = new Queue<SQLiteCommand>();
        private Queue<SQLiteCommand> commandQueue = new Queue<SQLiteCommand> ();
        private System.Timers.Timer queueTimer = new System.Timers.Timer ();
        private object lockObj = new object();
        private object lockTransaction = new object();
        private object lockQueue = new object ();
		private object lockReader = new object();

        public DBControl(string fileName)
        {
            SQLiteConnectionStringBuilder connBuilder = new SQLiteConnectionStringBuilder();
            connBuilder.DataSource = fileName;
            connBuilder.Version = 3;
            //Set page size to NTFS cluster size = 4096 bytes
            connBuilder.PageSize = 4096;
            connBuilder.CacheSize = 1024*1024;
            connBuilder.JournalMode = SQLiteJournalModeEnum.Wal;
            connBuilder.Pooling = true;
            this.sql = new SQLiteConnection(connBuilder.ToString());
            this.sql.Open();
            this.command = this.sql.CreateCommand();
            this.queueTimer.Interval = 30 * 1000;
            this.queueTimer.Elapsed += new System.Timers.ElapsedEventHandler ( queueTimer_Elapsed );
            this.queueTimer.Start ();
        }

        void queueTimer_Elapsed ( object sender, System.Timers.ElapsedEventArgs e )
        {
            this.ProcessQueue (false);
        }

        private void ProcessQueue ( bool wait )
        {
            Task task = Task.Factory.StartNew ( () =>
            {
                SQLiteCommand[] list = null;

                lock ( lockQueue )
                {
                    if ( commandQueue.Count == 0 )
                        return;
                    list = commandQueue.ToArray ();
                }

                lock ( lockObj )
                {
                    using ( SQLiteTransaction trans = sql.BeginTransaction () )
                    {
                        try
                        {
                            foreach ( SQLiteCommand cmd in list )
                            {
                                cmd.ExecuteNonQuery ();
                                cmd.Dispose ();
                            }
                            trans.Commit ();
                        }
                        catch ( Exception err )
                        {
                            //  ?
                            Console.WriteLine ( err.Message );
                        }
                    }
                }

                lock ( lockQueue )
                {
                    commandQueue.Clear ();
                }

            } );
            if ( wait )
                task.Wait ();
        }

        /// <summary>
        /// 破棄
        /// </summary>
        public void Dispose()
        {
            this.queueTimer.Dispose ();
            this.commandStack.Clear();
            this.commandStack = null;
            this.sql.Close();
            this.sql.Dispose();
            this.sql = null;
        }

        /// <summary>
        /// テーブルを作成します
        /// </summary>
        /// <param name="com"></param>
        public void CreateTable(string com)
        {
            var dest = this.sql.CreateCommand ();
            dest.CommandText = com;
            this.InsertData ( dest );
        }

        /// <summary>
        /// フォローしているユーザテーブルを作成します
        /// </summary>
        /// <param name="com"></param>
        public void CreateUserTable ( decimal user_id )
        {
            var com = this.sql.CreateCommand ();
            com.CommandText = "CREATE TABLE IF NOT EXISTS '" + user_id + "_FRIENDS' (id primary key ON CONFLICT ignore)";
            this.InsertData ( com );

            com = this.sql.CreateCommand ();
            com.CommandText = "CREATE TABLE IF NOT EXISTS '" + user_id + "_FOLLOWER' (id primary key ON CONFLICT ignore)";
            this.InsertData ( com );
        }

        /// <summary>
        /// テーブルを削除します
        /// </summary>
        /// <param name="user_id"></param>
        public void DestroyUserTabel ( decimal user_id )
        {
            var com = this.sql.CreateCommand ();
            com.CommandText = "DROP TABLE " + user_id + "_FRIENDS";
            this.InsertData ( com );

            com = this.sql.CreateCommand ();
            com.CommandText = "DROP TABLE " + user_id + "_FOLLOWER";
            this.InsertData ( com );
        }

        /// <summary>
        /// 処理を閉じます
        /// </summary>
        public void Close()
        {
            queueTimer.Stop ();
            this.ProcessQueue (true);
            this.sql.Close();
        }

        /// <summary>
        /// ツイートを取得する
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="offsetValue"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<TwitterStatus> GetlistTweets(string tableName, decimal offsetValue, decimal count)
        {
			var sql = tableName;
			try
			{
				lock (this.lockReader)
				{
					this.command.CommandText = sql;
					using (SQLiteDataReader sdr = this.command.ExecuteReader())
					{
						List<TwitterStatus> tuples = new List<TwitterStatus>();
						for (int i = 0; sdr.Read(); i++)
						{
							string[] column = new string[sdr.FieldCount];
							for (int j = 0; j < sdr.FieldCount; j++)
							{
								column[j] = sdr[j].ToString();
							}
							tuples.Add(new TwitterStatus(column));
						}

						//リストを配列に変換して返す
						return tuples;
					}
				}
			}
			catch (Exception err)
			{
				//  ?
				Console.WriteLine(err.Message);
			}
            return null;
        }

        /// <summary>
        /// DBのツイート数を取得する
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="offsetValue"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public decimal GetDatabasesTweets ( string tableName )
        {
            try
            {
				lock (this.lockReader)
				{
					this.command.CommandText = "select count(*) from " + tableName + "";
					object sdr = this.command.ExecuteScalar();
					return Convert.ToDecimal(sdr); // val1 = 0.4
				}
            }
            catch ( Exception err )
            {
                //  ?
                Console.WriteLine ( err.Message );
            }
            return 0;
        }

        /// <summary>
        /// ユーザのツイートを取得する
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="offsetValue"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<TwitterStatus> GetTweetByUserID ( string sql, decimal offsetValue, decimal count )
        {
            try
            {
				lock (this.lockReader)
				{
					this.command.CommandText = sql;
					using (SQLiteDataReader sdr = this.command.ExecuteReader())
					{
						List<TwitterStatus> tuples = new List<TwitterStatus>();
						for (int i = 0; sdr.Read(); i++)
						{
							string[] column = new string[sdr.FieldCount];
							for (int j = 0; j < sdr.FieldCount; j++)
							{
								column[j] = sdr[j].ToString();
							}
							tuples.Add(new TwitterStatus(column));
						}

						//リストを配列に変換して返す
						return tuples;
					}
				}
            }
            catch ( Exception err )
            {
                //  ?
                Console.WriteLine ( err.Message );
            }
            return null;
        }

        /// <summary>
        /// DMを取得する
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="offsetValue"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<TwitterDirectMessageStatus> GetlistDirectMessages(string tableName, decimal offsetValue, decimal count)
        {
            var sql = tableName;
            try
            {
				lock (this.lockReader)
				{
					this.command.CommandText = sql;
					using (SQLiteDataReader sdr = this.command.ExecuteReader())
					{
						List<TwitterDirectMessageStatus> tuples = new List<TwitterDirectMessageStatus>();
						for (int i = 0; sdr.Read(); i++)
						{
							string[] column = new string[sdr.FieldCount];
							for (int j = 0; j < sdr.FieldCount; j++)
							{
								column[j] = sdr[j].ToString();
							}
							tuples.Add(new TwitterDirectMessageStatus(column));
						}

						//リストを配列に変換して返す
						return tuples;
					}
				}
            }
            catch ( Exception err )
            {
                //  ?
                Console.WriteLine ( err.Message );
            }
            return null;
        }

        /// <summary>
        /// タイムラインを挿入する
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="id"></param>
        public void InsertTimeline(string sql, decimal id)
        {
            var com = this.sql.CreateCommand();
            com.CommandText = sql;
            com.Parameters.Add(new SQLiteParameter(DbType.Decimal) { Value = id });
            com.Prepare();
            this.InsertData(com);
        }

        public void InsertTimeline(string sql, List<TwitterStatus> ids)
        {
            var com = this.sql.CreateCommand();
            com.CommandText = sql;
            foreach (TwitterStatus id in ids)
                com.Parameters.Add(new SQLiteParameter(DbType.Decimal) { Value = id.id });

            com.Prepare();
            this.InsertData(com);
        }

        /// <summary>
        /// タイムラインに追加
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="ids"></param>
        public void InsertTimeline(string sql, List<decimal> ids)
        {
            var com = this.sql.CreateCommand();
            com.CommandText = sql;
            foreach (decimal id in ids)
            {
                com.Parameters.Add(new SQLiteParameter(DbType.Decimal) { Value = id });
            }
            com.Prepare();
            this.InsertData(com);
        }

		/// <summary>
		/// ユーザ情報をデータベースに書き込む
		/// </summary>
		/// <param name="user"></param>
        public void InsertUser(TwitterUserStatus user)
        {
            var t = user;

            var com = this.sql.CreateCommand();
            com.CommandText = user.sqlInsert;
            com.Parameters.Add(new SQLiteParameter(DbType.String) { Value = t.created_at.ToString("ddd MMM dd HH:mm:ss K yyyy", new CultureInfo("en-US")) });
            com.Parameters.Add(new SQLiteParameter(DbType.Decimal) { Value = t.id });
            com.Parameters.Add(new SQLiteParameter(DbType.String) { Value = t.name });
            com.Parameters.Add(new SQLiteParameter(DbType.String) { Value = t.screen_name });
            com.Parameters.Add(new SQLiteParameter(DbType.String) { Value = t.location });
            com.Parameters.Add(new SQLiteParameter(DbType.String) { Value = t.url });
            com.Parameters.Add(new SQLiteParameter(DbType.String) { Value = t.description });
            com.Parameters.Add(new SQLiteParameter(DbType.String) { Value = t.profile_image_url });
            com.Parameters.Add(new SQLiteParameter(DbType.Decimal) { Value = t.favourites_count });
            com.Parameters.Add(new SQLiteParameter(DbType.Decimal) { Value = t.followers_count });
            com.Parameters.Add(new SQLiteParameter(DbType.Decimal) { Value = t.friends_count });
            com.Parameters.Add(new SQLiteParameter(DbType.Decimal) { Value = t.listed_count });
            com.Parameters.Add(new SQLiteParameter(DbType.Boolean) { Value = t.protected_account });
            com.Parameters.Add(new SQLiteParameter(DbType.Decimal) { Value = t.statuses_count });
            com.Prepare();

            this.InsertData(com);
        }

		/// <summary>
		/// 画像をデータベースに書き込む
		/// </summary>
		/// <param name="user"></param>
		public void InsertImage(string command, string url, Bitmap image)
		{
			var com = this.sql.CreateCommand();
			com.CommandText = command;
			MemoryStream stream = new MemoryStream();
			image.Save(stream, ImageFormat.Bmp);
			Byte[] bytes = stream.ToArray();
			stream.Dispose();

			com.Parameters.Add(new SQLiteParameter(DbType.String) { Value = url });
			com.Parameters.Add(new SQLiteParameter(DbType.Binary) { Value = bytes });
			com.Prepare();

			this.InsertData(com);
		}

		public Bitmap BytesToBitmap(byte[] byteArray)
		{
			using (MemoryStream ms = new MemoryStream(byteArray))
			{
				Bitmap img = (Bitmap)Image.FromStream(ms);
				return img;
			}
		}

		/// <summary>
		/// 画像をデータベースに書き込む
		/// </summary>
		/// <param name="user"></param>
		public Bitmap GetImage(string command, string url)
		{
            var sql = command;
            try
            {
				lock (this.lockReader)
				{
					this.command.CommandText = sql;
					using (SQLiteDataReader sdr = this.command.ExecuteReader())
					{
						if (sdr.Read())
						{
							var data = (byte[])sdr.GetValue(0);
							return BytesToBitmap(data);
						}
					}
				}
            }
            catch ( Exception err )
            {
                //  ?
                Console.WriteLine ( err.Message );
            }
            return null;
		}

		/// <summary>
		/// ユーザ一覧をデータベースにいっきに書き込む
		/// </summary>
		/// <param name="users"></param>
        public void InsertUserRange(List<TwitterUserStatus> users)
        {
            if ( users == null || users.Count == 0 )
                return;

            var com = this.sql.CreateCommand();
            var isFirst = false;
            const int ParamNum = 14;
            int i = 0;

            foreach (var user in users)
            {
                if ((i * ParamNum) + ParamNum >= 999)
                {
                    com.Prepare();
                    this.InsertData(com);
                    com = this.sql.CreateCommand();
                    isFirst = false;
                    i = 0;
                }

                var t = user;
                if (!isFirst)
                {
                    com.CommandText = user.AnySqlInsertFirst;
                    isFirst = true;
                }
                else
                {
                    com.CommandText += user.AnySqlInsertEnd;
                }
                com.Parameters.Add(new SQLiteParameter(DbType.String) { Value = t.created_at.ToString("ddd MMM dd HH:mm:ss K yyyy", new CultureInfo("en-US")) });
                com.Parameters.Add(new SQLiteParameter(DbType.Decimal) { Value = t.id });
                com.Parameters.Add(new SQLiteParameter(DbType.String) { Value = t.name });
                com.Parameters.Add(new SQLiteParameter(DbType.String) { Value = t.screen_name });
                com.Parameters.Add(new SQLiteParameter(DbType.String) { Value = t.location });
                com.Parameters.Add(new SQLiteParameter(DbType.String) { Value = t.url });
                com.Parameters.Add(new SQLiteParameter(DbType.String) { Value = t.description });
                com.Parameters.Add(new SQLiteParameter(DbType.String) { Value = t.profile_image_url });
                com.Parameters.Add(new SQLiteParameter(DbType.Decimal) { Value = t.favourites_count });
                com.Parameters.Add(new SQLiteParameter(DbType.Decimal) { Value = t.followers_count });
                com.Parameters.Add(new SQLiteParameter(DbType.Decimal) { Value = t.friends_count });
                com.Parameters.Add(new SQLiteParameter(DbType.Decimal) { Value = t.listed_count });
                com.Parameters.Add(new SQLiteParameter(DbType.Boolean) { Value = t.protected_account });
                com.Parameters.Add(new SQLiteParameter(DbType.Decimal) { Value = t.statuses_count });
                i += ParamNum;
            }
            com.Prepare();
            this.InsertData(com);
        }

		/// <summary>
		/// ツイートをデータベースに書き込む
		/// </summary>
		/// <param name="tweet"></param>
        public void InsertTweet(TwitterStatus tweet)
        {
            var t = tweet.DynamicTweet;

            var com = this.sql.CreateCommand();
            com.CommandText = tweet.sqlInsert;
            com.Parameters.Add(new SQLiteParameter(DbType.String) { Value = t.created_at.ToString("ddd MMM dd HH:mm:ss K yyyy", new CultureInfo("en-US")) });
            com.Parameters.Add(new SQLiteParameter(DbType.Decimal) { Value = t.id });
            com.Parameters.Add(new SQLiteParameter(DbType.Decimal) { Value = t.in_reply_to_status_id });
            com.Parameters.Add(new SQLiteParameter(DbType.Decimal) { Value = t.in_reply_to_user_id });
            com.Parameters.Add(new SQLiteParameter(DbType.String) { Value = t.text });
            com.Parameters.Add(new SQLiteParameter(DbType.String) { Value = t.source });
            com.Parameters.Add(new SQLiteParameter(DbType.String) { Value = t.source_url });
            com.Parameters.Add(new SQLiteParameter(DbType.Decimal) { Value = t.retweet_count });
            com.Parameters.Add(new SQLiteParameter(DbType.Decimal) { Value = t.favorite_count });
            com.Parameters.Add(new SQLiteParameter(DbType.Boolean) { Value = t.isReply });
            com.Parameters.Add(new SQLiteParameter(DbType.Decimal) { Value = t.replyID });
            com.Parameters.Add(new SQLiteParameter(DbType.Boolean) { Value = t.isDirectMessage });
            com.Parameters.Add(new SQLiteParameter(DbType.Decimal) { Value = t.user.id });
            com.Parameters.Add(new SQLiteParameter(DbType.Boolean) { Value = t.retweeted_status != null });
            com.Parameters.Add(new SQLiteParameter(DbType.Decimal) { Value = (t.retweeted_status != null ? t.user.id : 0) });
            com.Parameters.Add(new SQLiteParameter(DbType.String) { Value = t.user.screen_name });
            com.Parameters.Add(new SQLiteParameter(DbType.String) { Value = t.user.name });
            com.Parameters.Add(new SQLiteParameter(DbType.Decimal) { Value = t.id });
            com.Parameters.Add(new SQLiteParameter(DbType.String) { Value = t.created_at.ToString("ddd MMM dd HH:mm:ss K yyyy", new CultureInfo("en-US")) });
            com.Prepare();

            this.InsertData(com);
        }

        /// <summary>
        /// DMを一括で入れる
        /// </summary>
        /// <param name="tweets"></param>
        public void InsertDMRange(List<TwitterDirectMessageStatus> tweets)
        {
            if ( tweets == null || tweets.Count == 0 )
                return;
            var com = this.sql.CreateCommand();
            bool isFirst = false;
            const int ParamNum = 19;
            int i = 0;

            foreach (var tweet in tweets)
            {
                if (i + ParamNum >= 999)
                {
                    com.Prepare();
                    this.InsertData(com);
                    com = this.sql.CreateCommand();
                    isFirst = false;
                    i = 0;
                }

                var t = tweet.DynamicTweet;
                if (!isFirst)
                {
                    com.CommandText = tweet.AnySqlInsertFirst;
                    isFirst = true;
                }
                else
                {
                    com.CommandText += tweet.AnySqlInsertEnd;
                }
                com.Parameters.Add(new SQLiteParameter(DbType.String) { Value = t.created_at.ToString("ddd MMM dd HH:mm:ss K yyyy", new CultureInfo("en-US")) });
                com.Parameters.Add(new SQLiteParameter(DbType.Decimal) { Value = t.id });
                com.Parameters.Add(new SQLiteParameter(DbType.String) { Value = t.text });
                com.Parameters.Add(new SQLiteParameter(DbType.String) { Value = t.source });
                com.Parameters.Add(new SQLiteParameter(DbType.String) { Value = t.source_url });
                com.Parameters.Add(new SQLiteParameter(DbType.Decimal) { Value = t.user.id });
                i += ParamNum;
            }

            com.Prepare();
            this.InsertData(com);
        }

        /// <summary>
        /// ツイートを一括で入れる
        /// </summary>
        /// <param name="tweets"></param>
        public void InsertTweetRange(List<TwitterStatus> tweets)
        {
            if ( tweets == null || tweets.Count == 0 )
                return;

            var com = this.sql.CreateCommand();
            bool isFirst = false;
            const int ParamNum = 19;
            int i = 0;

            foreach (var tweet in tweets)
            {
                if (i + ParamNum >= 999)
                {
                    com.Prepare();
                    this.InsertData(com);
                    com = this.sql.CreateCommand();
                    isFirst = false;
                    i = 0;
                }

                var t = tweet.DynamicTweet;
                if (!isFirst)
                {
                    com.CommandText = tweet.AnySqlInsertFirst;
                    isFirst = true;
                }
                else
                {
                    com.CommandText += tweet.AnySqlInsertEnd;
                }
                com.Parameters.Add(new SQLiteParameter(DbType.String) { Value = t.created_at.ToString("ddd MMM dd HH:mm:ss K yyyy", new CultureInfo("en-US")) });
                com.Parameters.Add(new SQLiteParameter(DbType.Decimal) { Value = t.id });
                com.Parameters.Add(new SQLiteParameter(DbType.Decimal) { Value = t.in_reply_to_status_id });
                com.Parameters.Add(new SQLiteParameter(DbType.Decimal) { Value = t.in_reply_to_user_id });
                com.Parameters.Add(new SQLiteParameter(DbType.String) { Value = t.text });
                com.Parameters.Add(new SQLiteParameter(DbType.String) { Value = t.source });
                com.Parameters.Add(new SQLiteParameter(DbType.String) { Value = t.source_url });
                com.Parameters.Add(new SQLiteParameter(DbType.Decimal) { Value = t.retweet_count });
                com.Parameters.Add(new SQLiteParameter(DbType.Decimal) { Value = t.favorite_count });
                com.Parameters.Add(new SQLiteParameter(DbType.Boolean) { Value = t.isReply });
                com.Parameters.Add(new SQLiteParameter(DbType.Decimal) { Value = t.replyID });
                com.Parameters.Add(new SQLiteParameter(DbType.Boolean) { Value = t.isDirectMessage });
                com.Parameters.Add(new SQLiteParameter(DbType.Decimal) { Value = t.user.id });
                com.Parameters.Add(new SQLiteParameter(DbType.Boolean) { Value = t.retweeted_status != null });
                com.Parameters.Add(new SQLiteParameter(DbType.Decimal) { Value = (t.retweeted_status != null ? t.user.id : 0) });
                com.Parameters.Add(new SQLiteParameter(DbType.String) { Value = t.user.screen_name });
                com.Parameters.Add(new SQLiteParameter(DbType.String) { Value = t.user.name });
                com.Parameters.Add(new SQLiteParameter(DbType.Decimal) { Value = t.id });
                com.Parameters.Add(new SQLiteParameter(DbType.String) { Value = t.created_at.ToString("ddd MMM dd HH:mm:ss K yyyy", new CultureInfo("en-US")) });

                i += ParamNum;
            }

            com.Prepare();
            this.InsertData(com);
        }

		/// <summary>
		/// データを書き込む
		/// </summary>
		/// <param name="com"></param>
        public void InsertData(SQLiteCommand com)
        {
            lock ( lockQueue )
            {
                commandQueue.Enqueue ( com );
            }
        }

        /// <summary>
        /// SQLへの処理を「直ちに」行う
        /// </summary>
        /// <param name="com"></param>
        public void DoRequest ( SQLiteCommand com )
        {
            try
            {
                com.ExecuteNonQuery ();
                com.Dispose ();
            }
            catch ( Exception err )
            {
                //  ?
                Console.WriteLine ( err.Message );
            }
        }

		/*
		/// <summary>
		/// データをデータベースに書き込む
		/// </summary>
		/// <param name="com"></param>
		public void InsertData(SQLiteCommand com)
		{
			Task.Factory.StartNew(() =>
			{
				lock (lockObj)
				{
					try
					{
						com.ExecuteNonQuery();
						com.Dispose();
					}
					catch (Exception e)
					{
						//  ?
						Console.WriteLine(e.Message);
					}
				}
			});
		}
		*/
		/*
        public void InsertData(SQLiteCommand com)
        {
            lock (lockObj)
            {
                this.commandStack.Enqueue(com);
                if (this.commandStack.Count >= 250)
                {
                    //Console.WriteLine ( "バッファがたくさんたまったので、書き込みます" );
                    lock (lockObj)
                    {
                        try
                        {
                            using (DbTransaction transaction = this.sql.BeginTransaction())
                            {
                                for (int i = 0; i < this.commandStack.Count; i++)
                                {
                                    var cmd = this.commandStack.Dequeue();
                                    if (cmd == null || cmd.CommandText == null)
                                        continue;

                                    cmd.ExecuteNonQuery();
                                    cmd.Dispose();
                                }
                                transaction.Commit();
                            }
                        }
                        catch (Exception e)
                        {
                            //  ?
                            Console.WriteLine(e.Message);
                        }
                        this.commandStack.Clear();
                    }
                    //Console.WriteLine ( "バッファを消去しました" );
                }
            }
        }
		*/
    }
}