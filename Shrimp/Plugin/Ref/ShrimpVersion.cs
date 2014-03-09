using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shrimp.Plugin.Ref
{
    /// <summary>
    /// Shrimpのバージョン
    /// </summary>
    public class ShrimpVersion
    {
        public readonly uint version;
        public readonly double version_str;

        public ShrimpVersion ()
        {
            this.version = 110;
            this.version_str = 1.10;
        }
    }
}
