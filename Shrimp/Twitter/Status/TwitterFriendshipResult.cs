using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shrimp.Twitter.Status
{
    class TwitterFriendshipResult : List<TwitterUserStatus>
    {
        public decimal next_cursor;
    }
}
