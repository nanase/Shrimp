using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shrimp.ControlParts.Tabs
{
    /// <summary>
    /// 保存・読み込みようのタブマネージャー
    /// </summary>
    public class TabManager
    {
        public bool isDefaultTab = false;
        public bool isLock = false;
        public string sourceTabName = "";
        public string tabID = "";
        public string ignoreTweet = "";
        public bool isFlash = false;
        public TabDeliveryCollection TabDelivery;
    }
}
