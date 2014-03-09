using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Shrimp.Setting.ObjectXML
{
    [DataContract]
    public class TabColorManager
    {
        [DataMember]
        public BrushEX BackgroundColor, StringColor;
        [DataMember]
        public bool isBold;

        public TabColorManager ( BrushEX BackgroundColor, BrushEX StringColor, bool isBold )
        {
            this.BackgroundColor = BackgroundColor;
            this.StringColor = StringColor;
            this.isBold = isBold;
        }
    }
}
