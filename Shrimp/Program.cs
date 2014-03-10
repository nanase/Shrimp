using System;
using System.IO;
using System.Text;
using System.Threading;
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
        public static void Main()
        {
            LogControl.AddLogs("Shrimpが起動しました");

            Application.ThreadException += ApplicationThreadException;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Shrimp());

            LogControl.AddLogs("Shrimpが終了しました");
        }

        private static void ApplicationThreadException(object sender, ThreadExceptionEventArgs e)
        {
            try
            {
                string path = string.Format("errlog_{0:yyyy年MM月dd日tthh時mm分ss秒}.txt", DateTime.Now);
                string errorContent = e.Exception.Message + Environment.NewLine + e.Exception.StackTrace;

                // エラーメッセージを表示する
                using (StreamWriter sw = new StreamWriter(path, false, Encoding.UTF8))
                    sw.Write(errorContent);

                MessageBox.Show("Shrimp実行中にエラーが発生しました。プログラムを終了します。" +
                                Environment.NewLine + errorContent, "エラー",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            finally
            {
                // アプリケーションを終了する
                // FIXME: 本当に 0 でいいのか？
                Environment.Exit(0);
            }
        }
    }
}
