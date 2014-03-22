using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Codeplex.Data;
using Shrimp.Log;

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
				var ver = getShrimpVersion();
				var result = checkUpdate(ver);
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

		/// <summary>
		/// Shrimpのバージョンを取得
		/// </summary>
		/// <returns></returns>
		public static string getShrimpVersion()
		{
			var vi = FileVersionInfo.GetVersionInfo(Application.ExecutablePath);
			return vi.FileVersion.Replace(".", "");
		}

		/// <summary>
		/// アップデート情報を取得するために、URLを叩く
		/// </summary>
		/// <returns>取得した結果/returns>
		public static string checkUpdate( string shrimpVersion )
		{
			string result = null;
			try
			{
				var webreq = (HttpWebRequest)WebRequest.Create(UpdateUrl + shrimpVersion);
				var webres = webreq.GetResponse();

				// 文字コード(EUC)を指定する
				// FIXME: EUC?
				var enc = Encoding.UTF8;

				using (var st = webres.GetResponseStream())
				using (var sr = new StreamReader(st, enc))
					// 受信して表示
					result = sr.ReadToEnd();
			}
			catch (Exception e)
			{
				LogControl.AddLogs("アップデートの取得に失敗しました。"+ e.Message +"");
			}
			return result;
		}
    }
}
