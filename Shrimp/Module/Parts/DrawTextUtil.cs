using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Shrimp.Module.Parts
{
    class DrawTextUtil
    {
        static Graphics g;

        /// <summary>
        /// 静的コンストラクタ
        /// </summary>
        static DrawTextUtil ()
        {
            g = Graphics.FromImage ( new Bitmap ( 640, 480 ) );
        }

        /// <summary>
        /// テキストの大きさを取得する
        /// </summary>
        /// <param name="text">テキスト</param>
        /// <param name="font">フォント</param>
        /// <param name="MaxWidth">最大幅</param>
        /// <param name="isTrimSpace">マージンを削除する場合は指定</param>
        /// <param name="without_wrap">折り返しを考慮しない場合は指定</param>
        /// <returns></returns>
        public static Size GetDrawTextSize ( string text, Font font, int MaxWidth, bool isTrimSpace = false, bool without_wrap = false )
        {
            TextFormatFlags strfmt = TextFormatFlags.Default;
            if ( isTrimSpace )
                strfmt |= TextFormatFlags.NoPadding;
            StringFormat fmt = ( isTrimSpace ? StringFormat.GenericTypographic : StringFormat.GenericDefault );
            fmt.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;
            if ( without_wrap )
               fmt.FormatFlags |= StringFormatFlags.NoWrap;
            return g.MeasureString ( text, font, MaxWidth, fmt ).ToSize ();
        }

        /// <summary>
        /// テキストの大きさを取得する(文字が超えそうなら、自動的にきりとります)
        /// </summary>
        /// <param name="text">テキスト</param>
        /// <param name="font">フォント</param>
        /// <param name="MaxWidth">最大幅</param>
        /// <param name="isTrimSpace">マージンを削除する場合は指定</param>
        /// <param name="without_wrap">折り返しを考慮しない場合は指定</param>
        /// <returns></returns>
        public static Size GetDrawTextSizeTrim ( string text, Font font, int MaxWidth, bool isTrimSpace = false, bool without_wrap = false )
        {
            TextFormatFlags strfmt = TextFormatFlags.Default;
            if ( isTrimSpace )
                strfmt |= TextFormatFlags.NoPadding;
            StringFormat fmt = ( isTrimSpace ? StringFormat.GenericTypographic : StringFormat.GenericDefault );
            fmt.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;
            if ( without_wrap )
                fmt.FormatFlags |= StringFormatFlags.NoWrap;
            fmt.Trimming = StringTrimming.EllipsisCharacter;
            return g.MeasureString ( text, font, MaxWidth, fmt ).ToSize ();
        }

        /// <summary>
        /// セルの大きさをAPI利用して得る
        /// 古い関数です。Layout～を利用してください
        /// </summary>
        /// <param name="name">名前</param>
        /// <param name="nameFont">フォント</param>
        /// <param name="tweet">ツイート</param>
        /// <param name="tweetFont">フォント</param>
        /// <param name="via">via</param>
        /// <param name="viafont">フォント</param>
        /// <param name="MaxWidth">最大横幅</param>
        /// <returns></returns>
        public static DrawCellSize GetDrawCellSize ( string name, Font nameFont, string tweet, Font tweetFont, string via, Font viafont, int MaxWidth )
        {
            DrawCellSize result = new DrawCellSize ();
            result.Name.Size = GetDrawTextSize ( name, nameFont, MaxWidth, false );
            result.Tweet.Size = GetDrawTextSize ( tweet, tweetFont, MaxWidth, false );
            result.Via.Size = GetDrawTextSize ( via, viafont, MaxWidth, false );
            return result;
        }

        /// <summary>
        /// オーナードローした場合のテキスト高さ
        /// </summary>
        /// <param name="text">テキスト</param>
        /// <param name="font">フォント</param>
        /// <param name="offsetX">オフセットX位置</param>
        /// <param name="MaxWidth">最大幅</param>
        /// <returns></returns>
        public static Size GetOwnerDrawTextSize ( string text, Font font, int offsetX, int MaxWidth )
        {
            if ( text != null )
            {
                int strX = offsetX, strY = 0, cnt = 0, len = text.Length;
                bool isWrap = false;
                foreach ( char t in text )
                {
                    var one_size = DrawTextUtil.GetDrawTextSize ( t.ToString (), font, MaxWidth, true );
                    strX += one_size.Width + Setting.Timeline.TweetPadding;
                    if ( strY == 0 )
                        strY = one_size.Height;

                    cnt ++;
                    if ( strX + one_size.Width >= MaxWidth || t == '\n' )
                    {
                        if ( t != '\n' && cnt >= len )
                            break;
                        //
                        strX = offsetX;
                        strY += one_size.Height;
                        isWrap = true;
                    }
                }
                //return new Size ( MaxWidth, wrapCalcY );
                return new Size ( ( isWrap ? MaxWidth : strX ), strY );
            }
            return new Size ();
        }

        /// <summary>
        /// 選択中のテキストがどこからどこまでなのか計算する
        /// </summary>
        /// <param name="text"></param>
        /// <param name="f"></param>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        /// <param name="MaxWidth"></param>
        /// <param name="startSel"></param>
        /// <param name="EndSel"></param>
        /// <returns></returns>
        public static int[] GetSelectTextlen ( string text, Font f, int offsetX, int offsetY, int MaxWidth, Point startSel, Point EndSel )
        {
            int x = offsetX, y = offsetY;
            int i = 0;
            int[] result = new int[2];
            result[0] = result[1] = -1;
            foreach ( char t in text )
            {
                var one_size = DrawTextUtil.GetDrawTextSize ( t.ToString (), f, MaxWidth, true );
                Rectangle r = new Rectangle ( x, y, one_size.Width + 1, one_size.Height );
                if ( r.Contains ( startSel ) && result[0] < 0 )
                {
                    result[0] = i;
                }

                if ( r.Contains ( EndSel ) && result[1] < 0 )
                {
                    result[1] = i;
                    break;
                }

                x += one_size.Width + Setting.Timeline.TweetPadding;
                if ( x + one_size.Width >= MaxWidth || t == '\n' )
                {
                    //
                    x = offsetX;
                    y += one_size.Height;
                }

                i++;
            }

            return result;
        }
    }
}
