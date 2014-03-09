using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace Shrimp.Setting.ObjectXML
{
	/// <summary>
	/// ブラシデータを保存・読み込みを可能にするクラス
	/// </summary>
	[DataContract]
	public class BrushEX
	{
		[DataMember]
		public byte a, r, g, b;
        [DataMember]
        public string profileName;

		public BrushEX(Brush b)
		{
			SolidBrush source = (SolidBrush)b;
			this.a = source.Color.A;
			this.r = source.Color.R;
			this.g = source.Color.G;
			this.b = source.Color.B;
		}

		[XmlIgnore]
		public Brush Generate
		{
			get
			{
				var dest = new SolidBrush(Color.FromArgb(
					this.a,
					this.r,
					this.g,
					this.b
					));
				return (Brush)dest;
			}
		}

        [XmlIgnore]
        public Color GenerateColor
        {
            get
            {
                var dest = Color.FromArgb (
                    this.a,
                    this.r,
                    this.g,
                    this.b
                    );
                return dest;
            }
        }

        [XmlIgnore]
        public byte Alpha
        {
            get { return this.a; }
        }
	}
}
