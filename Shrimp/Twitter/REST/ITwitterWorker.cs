using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shrimp.Twitter.REST
{
    interface ITwitterWorker
    {
        object workerResult ( dynamic data );
    }
}
