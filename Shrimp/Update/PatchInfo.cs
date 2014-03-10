using System;
using System.Text;

namespace Shrimp.Update
{
    class PatchInfo
    {
        public string Url { get; private set; }

        public string DecryptedMD5 { get; private set; }

        public string Log { get; private set; }

        public PatchInfo(string url, string md5, string log)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException("url");

            if (string.IsNullOrWhiteSpace(md5))
                throw new ArgumentNullException("md5");

            this.Url = url;
            this.DecryptedMD5 = DecryptMD5(md5);
            this.Log = log;
        }

        private static string DecryptMD5(string md5)
        {
            if (string.IsNullOrWhiteSpace(md5))
                throw new ArgumentNullException("md5");
            
            var decString = Encoding.ASCII.GetString(Convert.FromBase64String(md5));
            var decbytes = Encoding.ASCII.GetBytes(decString);
            //decbytes = Encoding.ASCII.GetBytes ( Encoding.ASCII.GetString ( decbytes ) );
            var tmp_rev = Encoding.ASCII.GetBytes("shrimpshrimpshrimpshrimpshrimpshrimpshrimpshrimp");
            var dest_md5 = new byte[decbytes.Length];
            var i = 0;
            var num = 0;

            foreach (var tmp_md5_str in decbytes)
            {
                var dest_tmp_md5 = (byte)(tmp_md5_str ^ tmp_rev[i]);
                dest_md5[num] = dest_tmp_md5;
                i++; num++;

                if (i >= tmp_rev.Length - 1)
                    i = 0;
            }

            return Encoding.ASCII.GetString(dest_md5);
        }
    }
}
