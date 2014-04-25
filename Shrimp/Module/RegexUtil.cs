using System;
using System.Text.RegularExpressions;

namespace Shrimp.Module
{
    class RegexUtil
    {
        #region 定義
        private static Regex via_re = new Regex("<a href=\"(?<url>.*?)\".*?>(?<text>.*?)</a>", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
        private static Regex url_re = new Regex(@"(https?|ftp)(:\/\/[-_.!~*\'()a-zA-Z0-9;\/?:\@&=+\$,%#]+)", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
        private static Regex mention_re = new Regex(@"@+([_A-Za-z0-9-]+)", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
        private static Regex hashtag_re = new Regex(@"(?:^|[^ーー゛゜々ヾヽぁ-ヶ一-龠ａ-ｚＡ-Ｚ０-９a-zA-Z0-9&_/>\u3041-\u3094\u3099-\u309C\u30A1-\u30FA\u3400-\uD7FF\uFF10-\uFF19\uFF20-\uFF3A\uFF41-\uFF5A\uFF66-\uFF9E]+)[#＃]([ー゛゜々ヾヽぁ-ヶ一-龠ａ-ｚＡ-Ｚ０-９a-zA-Z0-9_\u3041-\u3094\u3099-\u309C\u30A1-\u30FA\u3400-\uD7FF\uFF10-\uFF19\uFF20-\uFF3A\uFF41-\uFF5A\uFF66-\uFF9E]*[ー゛゜々ヾヽぁ-ヶ一-龠ａ-ｚＡ-Ｚ０-９a-zA-Z\u3041-\u3094\u3099-\u309C\u30A1-\u30FA\u3400-\uD7FF\uFF10-\uFF19\uFF20-\uFF3A\uFF41-\uFF5A\uFF66-\uFF9E]+[ー゛゜々ヾヽぁ-ヶ一-龠ａ-ｚＡ-Ｚ０-９a-zA-Z0-9_\u3041-\u3094\u3099-\u309C\u30A1-\u30FA\u3400-\uD7FF\uFF10-\uFF19\uFF20-\uFF3A\uFF41-\uFF5A\uFF66-\uFF9E]*)", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
        private static Regex twitpic_re = new Regex(@"http:\/\/twitpic[.]com\/(?<id>\w+)", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
        private static Regex yfrog_re = new Regex(@"http:\/\/yfrog[.]com\/(?<id>\w+)", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
        private static Regex movepic_re = new Regex(@"http:\/\/movapic[.]com\/pic\/(?<id>\w+)", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
        private static Regex img_re = new Regex(@"http:\/\/img[.]ly\/(?<id>\w+)", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
        private static Regex youtube_re = new Regex(@"http:\/\/(?:www[.]youtube[.]com\/watch(?:\?|#!)v=|youtu[.]be\/)(?<id>[\w\-]+)(?:[-_.!~*\'()a-zA-Z0-9;\/?:@&=+$,%#]*)", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
        private static Regex inst_re = new Regex(@"http:\/\/instagr[.]am\/p\/(?<id>[\w\-]+)\/", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
        private static Regex photo_zou_re = new Regex(@"http:\/\/photozou[.]jp\/photo\/show\/\d+\/(?<id>[\d]+)", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
        private static Regex twipple_re = new Regex(@"http:\/\/p[.]twipple[.]jp\/(?<id>[\w]+)", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
        private static Regex nico_re = new Regex(@"http:\/\/(?:www\.nicovideo\.jp\/watch|nico\.ms)\/(?<id>[\w]+)", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);

        #endregion

        static RegexUtil()
        {
            //
        }

        /// <summary>
        /// Viaをパースする
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string[] ParseVia(string source)
        {
            string[] result = null;
            if (source == null)
                return null;
            Match m = via_re.Match(source);
            if (m.Success)
            {
                result = new string[2] { m.Groups["url"].Value, m.Groups["text"].Value };
                return result;
            }

            return null;
        }

        /// <summary>
        /// 正規表現でURLをみつける
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static MatchCollection ParseURL(string text)
        {
            if (text == null)
                return null;
            MatchCollection m = url_re.Matches(text);
            return m;
        }

        /// <summary>
        /// 正規表現で@をみつける
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static MatchCollection ParseMention(string text)
        {
            if (text == null)
                return null;
            MatchCollection m = mention_re.Matches(text);
            return m;
        }

        /// <summary>
        /// 正規表現でタグをみつける
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static MatchCollection ParseHashTag(string text)
        {
            if (text == null)
                return null;
            return hashtag_re.Matches(text);
        }

        public static string ReplaceThumbURL(string url, out bool isThumb)
        {
            isThumb = false;
            if (twitpic_re.IsMatch(url))
            {
                url = twitpic_re.Replace(url, "http://twitpic.com/show/thumb/${id}");
                isThumb = true; return url;
            }
            if (yfrog_re.IsMatch(url))
            {
                url = yfrog_re.Replace(url, "http://yfrog.com/${id}.th.jpg");
                isThumb = true; return url;
            }
            if (movepic_re.IsMatch(url))
            {
                url = movepic_re.Replace(url, "http://image.movapic.com/pic/t_${id}.jpeg");
                isThumb = true; return url;
            }
            if (img_re.IsMatch(url))
            {
                url = img_re.Replace(url, "http://img.ly/show/thumb/${id}");
                isThumb = true; return url;
            }
            if (youtube_re.IsMatch(url))
            {
                url = youtube_re.Replace(url, "http://i.ytimg.com/vi/${id}/default.jpg");
                isThumb = true; return url;
            }
            if (inst_re.IsMatch(url))
            {
                url = inst_re.Replace(url, "http://instagr.am/p/${id}/media/?size=t");
                isThumb = true; return url;
            }
            if (photo_zou_re.IsMatch(url))
            {
                url = photo_zou_re.Replace(url, "http://photozou.jp/p/thumb/${id}");
                isThumb = true; return url;
            }
            if (twipple_re.IsMatch(url))
            {
                url = twipple_re.Replace(url, "http://p.twipple.jp/show/thumb/${id}");
                isThumb = true; return url;
            }
            if (nico_re.IsMatch(url))
            {
                var t = nico_re.Match(url);
                string id = t.Groups["id"].Value;
                if (id != null && id.Substring ( 0, 1 ) != "l")
                {
                    int id_int = Int32.Parse(id.Substring(2));
                    isThumb = true;
                    int pID = (id_int % 4) + 1;
                    return "http://tn-skr" + pID + ".smilevideo.jp/smile?i=" + id_int + "";
                }
            }
            return url;
        }
    }
}
