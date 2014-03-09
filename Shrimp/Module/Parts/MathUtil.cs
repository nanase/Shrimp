using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shrimp.Module.Parts
{
    class MathUtil
    {
        /// <summary>
        /// limit
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int limit ( int value, int min, int max )
        {
            if ( value < min )
                value = min;
            if ( value > max )
                value = max;
            return value;
        }
    }
}
