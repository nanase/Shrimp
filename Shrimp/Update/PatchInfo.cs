using System;
using System.Text;

namespace Shrimp.Update
{
    class PatchInfo
    {
        public string file_url = "";
        public string md5 = "";
        public string log = "";

        public PatchInfo(string file_url, string md5, string log)
        {
            this.file_url = file_url;
            this.md5 = MD5Decrypt(md5);
            this.log = log;
        }

        public string MD5Decrypt(string md5)
        {
            if (!string.IsNullOrEmpty(md5))
            {
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
            return "";
        }
    }
}
