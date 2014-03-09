using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Shrimp.Log;

namespace Shrimp
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            LogControl.AddLogs("Shrimpが起動しました");
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Shrimp());
            LogControl.AddLogs("Shrimpが終了しました");
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            try
            {
                //エラーメッセージを表示する
                StreamWriter sw = new StreamWriter("errlog_" + DateTime.Now.ToString("yyyy年MM月dd日tthh時mm分ss秒") + ".txt", false, Encoding.UTF8);
                sw.Write("" + e.Exception.Message + "\n" + e.Exception.StackTrace + "");
                sw.Close();
                MessageBox.Show("Shrimp実行中にエラーが発生しました。プログラムを終了します。\n" + e.Exception.Message + "\n" + e.Exception.StackTrace + "", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //アプリケーションを終了する
                Environment.Exit(0);
            }
        }
    }
}
