using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Shrimp.Setting.Forms
{
	class SettingUtils
	{
		/// <summary>
		/// 色画像作成
		/// </summary>
		/// <param name="c"></param>
		/// <returns></returns>
		public static Image CreateImageColor(Brush c)
		{
			var dest = new Bitmap(24, 24);
			using (Graphics g = Graphics.FromImage(dest))
			{
				g.FillRectangle(c, 0, 0, dest.Width, dest.Height);
			}
			return dest;
		}

	}
}
