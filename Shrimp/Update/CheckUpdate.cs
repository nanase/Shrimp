using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Codeplex.Data;

namespace Shrimp.Update
{
    class CheckUpdate
    {
        public static void CheckUpdateSync ( Shrimp.OnCreatingUpdateFormDelegate OnCreatingDel )
        {
            string result = null;
            var task = Task.Factory.StartNew ( () =>
            {
                FileVersionInfo vi = FileVersionInfo.GetVersionInfo ( Application.ExecutablePath );
                var ver = vi.FileVersion.Replace ( ".", "" );
                StreamReader sr = null;
                try
                {
                    HttpWebRequest webreq =
                        (HttpWebRequest)
                            WebRequest.Create ( "http://shrimp.ga/update/update?version=" + ver + "" );

                    WebResponse webres = webreq.GetResponse ();

                    //文字コード(EUC)を指定する
                    Encoding enc = Encoding.UTF8;
                    //応答データを受信するためのStreamを取得
                    Stream st = webres.GetResponseStream ();
                    sr = new StreamReader ( st, enc );
                    //受信して表示
                    string html = sr.ReadToEnd ();
                    result = html;

                }
                catch ( Exception )
                {
                }
                finally
                {
                    if ( sr != null )
                        sr.Close ();
                }

                if ( result != null )
                {
                    var res = DynamicJson.Parse ( result );
                    var patch = new PatchInfo ( res.download_url, res.md5, res.modify );
                    if ( !res.isModify )
                    {
                        //  アプデなし
                        Setting.Update.isIgnoreUpdate = false;
                    }
                    else
                    {
                        OnCreatingDel.Invoke ( patch.log );
                    }
                }
            } );
        }
    }
}
