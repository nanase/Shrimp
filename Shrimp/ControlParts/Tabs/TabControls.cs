using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using Shrimp.ControlParts.Timeline;
using Shrimp.ControlParts.User;
using Shrimp.Query;
using Shrimp.SQL;
using Shrimp.Twitter.Status;

namespace Shrimp.ControlParts.Tabs
{
    public class TabControls : TabPage, IDisposable
    {
        #region 定義
        public string TabID = "";
        private object timeline;

        public TabDeliveryCollection tabDelivery;
        public bool isDefaultTab = false;
        public bool isLock = false;
        private string sourceTabName = "";

        private bool _isVisible = false;
        public bool isFlash = false;
        /// ツイートを除外する条件式
        /// </summary>
        public string ignoreTweet = "";
        //  未読数
        private int unreadNum = 0;

        private DBControl db;
        private QueryParser shrimpQueryParser = new QueryParser();
        /// <summary>
        /// 読み込む際のデリゲート
        /// </summary>
        public delegate void OnReloadDelegate(TabControls tab);
        public OnReloadDelegate OnReload;
        /// <summary>
        /// タブを最初に開いたときにtrueになる
        /// </summary>
        public bool isFirstView = false;
        private bool isDisposeTabControls = false;

        /// <summary>
        /// FlashWindowを動かすためのデリゲート
        /// </summary>
        public delegate void FlashWindowDelegate();
        public FlashWindowDelegate FlashWindowHandler;
        #endregion

        #region コンストラクタ
        public TabControls()
        {
        }

        /// <summary>
        /// 未読があるかどうかを調べます
        /// </summary>
        public bool isContainUnRead
        {
            get { return (unreadNum != 0); }
        }

        /// <summary>
        /// クエリと比較して、エラーや、ただしければtrueが変える
        /// </summary>
        /// <param name="tweet"></param>
        /// <returns></returns>
        public bool isMatchQuery(TwitterStatus tweet)
        {
            //
            if (this.ignoreTweet == null || String.IsNullOrEmpty(this.ignoreTweet) || tweet == null)
                return false;
            return shrimpQueryParser.isMatch(this.ignoreTweet, tweet);
        }

        /// <summary>
        /// 一番上にスクロールします
        /// </summary>
        public void SetFirstView(bool isSQL)
        {
            //  一番最初のところを表示する
            if (this.tabDelivery != null)
            {
                if (this.timeline is TimelineControl)
                {
                    var tl = this.timeline as TimelineControl;
                    tl.SetFirstView();
                    if (isSQL)
                        tl.SetSQL();
                }
            }
        }

        /// <summary>
        /// コントロールを撮影します
        /// </summary>
        /// <returns></returns>
        public Bitmap CaptureControl()
        {
            if (this.tabDelivery != null)
            {
                if (this.timeline is TimelineControl)
                {
                    var tl = this.timeline as TimelineControl;
                    return tl.CaptureControl();
                }
                else
                {
                    var tl = this.timeline as UserStatusControl;
                    return tl.CaptureControl();
                }
            }
            return null;
        }

        /// <summary>
        /// コントロールをスクロール
        /// </summary>
        public void ScrollUp()
        {
            if (this.tabDelivery != null)
            {
                if (this.timeline is TimelineControl)
                {
                    var tl = this.timeline as TimelineControl;
                    tl.ScrollUp();
                }
            }
        }

        /// <summary>
        /// スクロール
        /// </summary>
        public void ScrollDown()
        {
            if (this.tabDelivery != null)
            {
                if (this.timeline is TimelineControl)
                {
                    var tl = this.timeline as TimelineControl;
                    tl.ScrollDown();
                }
            }
        }

        /// <summary>
        /// タブコントロールで得たキーを、タイムライン下にも配る
        /// </summary>
        /// <param name="ke"></param>
        public void OnKeyDownFromParent(KeyEventArgs ke)
        {
            if (this.tabDelivery != null)
            {
                if (this.timeline is TimelineControl)
                {
                    var tl = this.timeline as TimelineControl;
                    tl.TimelineControl_KeyDown(this, ke);
                }
            }
        }

        public TabControls(DBControl db, bool isDefaultTab, bool isLock, TimelineCategory category, OnReloadDelegate onReload, TimelineControl.TabControlOperationgDelegate TabControlOperation,
                             TimelineControl.OnUseTwitterAPIDelegate OnUseTwitterAPIHandler,
                             FlashWindowDelegate FlashWindowEventHandler, Shrimp.OnUserStatusControlAPIDelegate OnUserStatusControlAPI)
        {
            this.db = db;

            this.isDefaultTab = isDefaultTab;
            if (category.category == TimelineCategories.UserInformation)
            {
                timeline = (UserStatusControl)new UserStatusControl(null, TabControlOperation, OnUseTwitterAPIHandler, OnUserStatusControlAPI) { Dock = DockStyle.Fill };
            }
            else
            {
                timeline = (TimelineControl)new TimelineControl() { Dock = DockStyle.Fill };
            }
            this.tabParent = (Control)timeline;
            this.isLock = isLock;
            this.tabDelivery = new TabDeliveryCollection();
            this.tabDelivery.AddDelivery(new TabDelivery(category, null));
            this.OnReload = onReload;
            this.FlashWindowHandler = FlashWindowEventHandler;
        }

        /// <summary>
        /// テーブルを作成するSQL
        /// </summary>
        public string tableCreateSQL
        {
            get
            {
                return "CREATE TABLE IF NOT EXISTS " + TabID + " (id primary key ON CONFLICT ignore)";
            }
        }

        /// <summary>
        /// テーブルを破棄するSQL
        /// </summary>
        public string destroyTableSQL
        {
            get
            {
                return "DROP TABLE " + TabID + "";
            }
        }

        /// <summary>
        /// ツイートを挿入するSQL
        /// </summary>
        public string insertTimelineSQL
        {
            get { return "INSERT INTO " + TabID + " (id) values (?)"; }
        }

        /// <summary>
        /// ツイートを一気に挿入するSQL
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public string insertTimelineSQLRange(int num)
        {
            if (num == 0)
                return null;
            string f = "INSERT INTO " + TabID + " (id) ";
            //bool isFirst = false;
            for (int i = 0; i < num; i++)
            {
                if (i % 2 == 0)
                {
                    f += " select ?";
                }
                else
                {
                    if (i < num - 1)
                        f += " UNION";
                }
            }
            return f;
        }

        /// <summary>
        /// 保存時に利用します。
        /// 保存するときに、データベースにタイムラインのツイートID一式をすべて保存します
        /// </summary>
        /// <param name="db"></param>
        public void SetTimelineToDatabase(DBControl db)
        {
            if (db == null)
                return;
            if (this.timeline is TimelineControl)
            {
                //  TimelineControlならば、直接データひろってきていいかなぁ。
                var tl = this.timeline as TimelineControl;
                tl.Invoke((MethodInvoker)delegate()
                {
                    var gen = tl.GenerateStatusIDs;
                    if (gen.Count != 0)
                    {
                        int count = gen.Count;
                        int MaxInsert = 499;
                        for (int i = 0; i < (gen.Count / MaxInsert) + 1; i++)
                        {
                            var range = gen.GetRange((i * MaxInsert), count > MaxInsert ? MaxInsert : count);
                            db.InsertTimeline(insertTimelineSQLRange(range.Count), range);
                            count -= MaxInsert;
                        }
                    }
                });
            }
        }

        /// <summary>
        /// タイムラインSQLを取得する
        /// </summary>
        public string getTimelineSQL
        {
            get
            {
                return "SELECT * FROM tweet AS tweets cross join user on tweets.user_id = user.id WHERE EXISTS ( SELECT * FROM " + TabID + " WHERE tweets.id = id ) order by id desc limit 1000";
                //return "SELECT * FROM " + TabID + " AS STATUSES WHERE EXISTS ( SELECT * FROM tweet WHERE STATUSES.id = id )";
            }
        }

        /// <summary>
        /// DMを取得するSQL
        /// </summary>
        public string getDMTimelineSQL
        {
            get
            {
                return "SELECT * FROM directMessages AS tweets cross join user on tweets.user_id = user.id WHERE EXISTS ( SELECT * FROM " + TabID + " WHERE tweets.id = id ) order by id desc limit 1000";
                //return "SELECT * FROM " + TabID + " AS STATUSES WHERE EXISTS ( SELECT * FROM tweet WHERE STATUSES.id = id )";
            }
        }

        /// <summary>
        /// このタブを保存する
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public TabManager save(DBControl db)
        {
            TabManager dest = new TabManager();

            dest.isDefaultTab = this.isDefaultTab;
            dest.isLock = this.isLock;
            dest.sourceTabName = (string)this.sourceTabName.Clone();
            dest.ignoreTweet = (string)this.ignoreTweet.Clone();
            dest.tabID = (string)this.TabID.Clone();
            dest.isFlash = this.isFlash;
            dest.TabDelivery = (TabDeliveryCollection)this.tabDelivery.Clone();
            if (db != null)
                this.SetTimelineToDatabase(db);

            return dest;
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (!this.isDisposeTabControls)
            {
                if (!this.tabDelivery.isTimeline)
                {
                    var tl = this.timeline as UserStatusControl;
                    if (tl != null)
                    {
                        tl.Dispose();
                        tl = null;
                    }
                }
                else
                {
                    var tl = this.timeline as TimelineControl;
                    if (tl != null)
                    {
                        tl.Dispose();
                        tl = null;
                    }
                }
                this.timeline = null;
                this.tabDelivery = null;
                base.Dispose(disposing);
                this.isDisposeTabControls = true;
            }
        }

        #endregion

        /// <summary>
        /// ロードが終わったら実行してください
        /// </summary>
        public void SetFinishedLoading()
        {
            if (this.tabDelivery != null)
            {
                if (!this.tabDelivery.isTimeline)
                {
                    var res = this.timeline as UserStatusControl;
                    res.isLoadingFinished = true;
                }
                else
                {
                    var res = this.timeline as TimelineControl;
                    res.isLoadingFinished = true;
                }
            }
        }

        /// <summary>
        /// タブ名変更
        /// </summary>
        [XmlIgnore]
        public string tabText
        {
            get { return this.sourceTabName; }
            set
            {
                Task.Factory.StartNew(() =>
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke((MethodInvoker)delegate()
                        {
                            this.Text = value;
                        });
                    }
                    else
                    {
                        this.Text = value;
                    }
                });
                this.sourceTabName = value;
            }
        }

        [XmlIgnore]
        public bool isVisible
        {
            get { return this._isVisible; }
            set
            {
                if (_isVisible != value)
                {
                    _isVisible = value;
                    if (this.tabDelivery != null)
                    {
                        if (!this.tabDelivery.isTimeline)
                        {
                            UserStatusControl u = this.timeline as UserStatusControl;
                            u.Visible = value;
                        }
                        else
                        {
                            TimelineControl t = this.timeline as TimelineControl;
                            t.Visible = value;
                        }
                    }
                    if ( value )
                        SetTextReset ();
                }
            }
        }

        /// <summary>
        /// テキスト
        /// </summary>
        public void SetTextReset()
        {
            //  元に戻す
            this.tabText = this.sourceTabName;
            this.unreadNum = 0;
        }

        /// <summary>
        /// percentage
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public int CalcPercentage(int num)
        {
            if (this.tabDelivery == null || !this.tabDelivery.isTimeline)
                return 0;
            var res = this.timeline as TimelineControl;
            if (res == null)
                return 0;
            int per = (int)(((double)num / res.TweetCount) * 100);
            return per;
        }

        /// <summary>
        /// ツイートを挿入
        /// </summary>
        /// <param name="tweet"></param>
        public void InsertTweet(TwitterStatus tweet)
        {
            if (this.tabDelivery == null || !this.tabDelivery.isTimeline)
                return;
            var res = this.timeline as TimelineControl;
            if (res == null)
                return;
            if (!this.isMatchQuery(tweet))
            {
                res.InsertTimeline(tweet);
                this.unreadNum++;
                if (this.isFlash)
                {
                    //  ウィンドウフラッシュ
                    FlashWindow();
                }

                Task.Factory.StartNew(() =>
                {
                    if (!isVisible)
                    {
                        if (this.InvokeRequired)
                        {
                            this.Invoke((MethodInvoker)delegate()
                            {
                                this.Text = "*" + this.sourceTabName + "(" + this.unreadNum + ")";
                            });
                        }
                        else
                        {
                            this.Text = "*" + this.sourceTabName + "(" + this.unreadNum + ")";
                        }
                    }
                });
            }
        }

        /// <summary>
        /// ツイートをいっきに挿入
        /// </summary>
        /// <param name="tweet"></param>
        public int InsertTimelineRange(List<TwitterStatus> tweet, bool isSQL = false)
        {
            if (this.tabDelivery == null || !this.tabDelivery.isTimeline || tweet.Count == 0)
                return 0;
            var res = this.timeline as TimelineControl;
            if (res == null)
                return 0;
            int newTweetNum = res.InsertTimelineRange(tweet.FindAll((t) => !this.isMatchQuery(t)));
            if (!isSQL)
            {
                this.unreadNum += newTweetNum;
                if (this.isFlash)
                {
                    //  ウィンドウフラッシュ
                    FlashWindow();
                }

                Task.Factory.StartNew(() =>
                {
                    if (!isVisible && newTweetNum != 0)
                    {
                        if (this.InvokeRequired)
                        {
                            this.Invoke((MethodInvoker)delegate()
                            {
                                this.Text = "*" + this.sourceTabName + "(" + this.unreadNum + ")";
                            });
                        }
                        else
                        {
                            this.Text = "*" + this.sourceTabName + "(" + this.unreadNum + ")";
                        }
                    }
                });

            }
            return newTweetNum;
            /*
            if ( db != null && !isSQL )
            {
                foreach ( TwitterStatus tt in tweet )
                {
                    //db.InsertData ( this.insertTimelineSQL ( tt.id ) );
                }
            }
            */
        }

        /// <summary>
        /// ユーザー情報を変更する
        /// </summary>
        /// <param name="tweet"></param>
        public void ChangeUserStatus(TwitterUserStatus user)
        {
            if (this.tabDelivery != null && !this.tabDelivery.isTimeline)
            {
                var res = this.timeline as UserStatusControl;
                res.ChangeUserStatus(user);
            }
        }

        /// <summary>
        /// タブの親を変更
        /// </summary>
        private Control tabParent
        {
            get { return this.Parent; }
            set
            {
                if (this.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate()
                    {
                        if (value.InvokeRequired)
                        {
                            this.Invoke((MethodInvoker)delegate()
                            {
                                value.Dock = DockStyle.Fill;
                            });
                        }
                        else
                        {
                            value.Dock = DockStyle.Fill;
                        }
                        this.Controls.Add(value);
                    });
                }
                else
                {
                    if (value.InvokeRequired)
                    {
                        this.Invoke((MethodInvoker)delegate()
                        {
                            value.Dock = DockStyle.Fill;
                        });
                    }
                    else
                    {
                        value.Dock = DockStyle.Fill;
                    }
                    this.Controls.Add(value);
                }
            }
        }

        [XmlIgnore]
        public object TimelineObject
        {
            get { return this.timeline; }
        }

        public void FlashWindow()
        {
            if (this.FlashWindowHandler != null)
                this.FlashWindowHandler.Invoke();
        }
    }
}
