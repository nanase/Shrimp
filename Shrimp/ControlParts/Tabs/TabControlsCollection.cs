using System.Collections;
using System.Collections.Generic;
using Shrimp.ControlParts.Timeline;
using Shrimp.ControlParts.User;
using Shrimp.Query;
using Shrimp.SQL;
using Shrimp.Twitter.Status;

namespace Shrimp.ControlParts.Tabs
{
    class TabControlsCollection : List<TabControls>
    {
        /// <summary>
        /// タブの保存
        /// </summary>
        /// <returns></returns>
        public List<TabManager> Save(DBControl db)
        {
            List<TabManager> destTabs = new List<TabManager>();
            lock (((ICollection)this).SyncRoot)
            {
                foreach (TabControls t in this)
                {
                    destTabs.Add(t.save(db));
                }
            }
            return destTabs;
        }

        /// <summary>
        /// アイテムをスレッドセーフな状態で追加します
        /// </summary>
        /// <param name="data"></param>
        public new void Add(TabControls data)
        {
            lock (((ICollection)this).SyncRoot)
            {
                base.Add(data);
            }
        }

        /// <summary>
        /// アイテムをスレッドセーフな状態で削除
        /// </summary>
        /// <param name="data"></param>
        public new void RemoveAt(int data)
        {
            lock (((ICollection)this).SyncRoot)
            {
                base.RemoveAt(data);
            }
        }

        public bool isLocked(int num)
        {
            bool res = false;
            lock (((ICollection)this).SyncRoot)
            {
                res = this[num].isLock;
            }
            return res;
        }

        /// <summary>
        /// ツイートを挿入する
        /// </summary>
        /// <param name="queryParser"></param>
        /// <param name="tweet"></param>
        /// <param name="user_id"></param>
        /// <param name="destCategories"></param>
        /// <param name="obj"></param>
        public void InsertTweet(QueryParser queryParser, TwitterStatus tweet, decimal user_id, TimelineCategories destCategories, object obj)
        {
            lock (((ICollection)this).SyncRoot)
            {
                foreach (TabControls tabctl in this)
                {
                    if (tabctl.tabDelivery.isMatch(user_id, destCategories, obj))
                    {
                        if (!string.IsNullOrEmpty(Setting.Timeline.GlobalMuteString))
                        {
                            if (queryParser.isMatch(Setting.Timeline.GlobalMuteString, tweet))
                                continue;
                        }
                        tabctl.InsertTweet(tweet);
                    }
                }
            }
        }

        public int InsertTweetRange(QueryParser queryParser, List<TwitterStatus> tweets, decimal user_id, TimelineCategories destCategories, object obj)
        {
            var newTweetNum = 0;
            lock (((ICollection)this).SyncRoot)
            {
                foreach (TabControls tabctl in this)
                {
                    if (tabctl.tabDelivery.isMatch(user_id, destCategories, obj))
                    {
                        if (!string.IsNullOrEmpty(Setting.Timeline.GlobalMuteString))
                        {
                            bool isContinueFlag = false;
                            foreach (TwitterStatus tweet in tweets)
                            {
                                if (queryParser.isMatch(Setting.Timeline.GlobalMuteString, tweet))
                                {
                                    isContinueFlag = true;
                                    break;
                                }
                            }
                            if (isContinueFlag)
                                continue;
                        }
                        newTweetNum = tabctl.InsertTimelineRange(tweets);
                    }
                }
            }
            return newTweetNum;
        }

        /// <summary>
        /// 変更があった
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public object SelectedChange(int num)
        {
            object selectedForm = null;

            lock (((ICollection)this).SyncRoot)
            {
                int i = 0;
                foreach (TabControls tb in this)
                {
                    if (num == i)
                    {
                        //  タイムライン復帰
                        if (tb.TimelineObject is TimelineControl)
                        {
                            var res = tb.TimelineObject as TimelineControl;
                            selectedForm = res;
                            res.Resume();
                        }
                        else
                        {
                            var res = tb.TimelineObject as UserStatusControl;
                            selectedForm = res;
                            res.Resume();
                        }

                        tb.isVisible = true;
                        //visibleControl = tb;
                        if (tb.OnReload != null && !tb.isFirstView)
                            tb.OnReload.BeginInvoke(tb, null, null);
                        tb.isFirstView = true;
                    }
                    else
                    {
                        if (tb.TimelineObject is TimelineControl)
                        {
                            var res = tb.TimelineObject as TimelineControl;
                            res.Suspend();
                        }
                        else
                        {
                            var res = tb.TimelineObject as UserStatusControl;
                            res.Suspend();
                        }
                        //invisibleControls.Add ( tb );
                    }
                    i++;
                }
            }
            return selectedForm;
        }
    }
}
