using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Codeplex.Data;

namespace Shrimp.Update
{
    class CheckUpdate
    {
        private const string UpdateUrl = "http://shrimp.ga/update/update?version=";

        public static void CheckUpdateSync(Shrimp.OnCreatingUpdateFormDelegate OnCreatingDel)
        {
            // TODO: ファイルの取得、取得+JSONの解析用のコードに分離したほうがよさそう
            Task.Factory.StartNew(() =>
            {
                string result;
                var vi = FileVersionInfo.GetVersionInfo(Application.ExecutablePath);
                var ver = vi.FileVersion.Replace(".", "");
                var webreq = (HttpWebRequest)WebRequest.Create(UpdateUrl + ver);
                var webres = webreq.GetResponse();

                // 文字コード(EUC)を指定する
                // FIXME: EUC?
                var enc = Encoding.UTF8;

                using (var st = webres.GetResponseStream())
                using (var sr = new StreamReader(st, enc))
                    // 受信して表示
                    result = sr.ReadToEnd();

                if (!string.IsNullOrWhiteSpace(result))
                {
                    var res = DynamicJson.Parse(result);
                    var patch = new PatchInfo(res.download_url, res.md5, res.modify);

                    if (!res.isModify)
                    {
                        // アプデなし
                        Setting.Update.isIgnoreUpdate = false;
                    }
                    else
                    {
                        OnCreatingDel.Invoke(patch.Log);
                    }
                }
            });
        }
    }
}
