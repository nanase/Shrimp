using System.Windows.Forms;

namespace Shrimp.Module.FormUtil
{
    class MessageBoxEX
    {
        /// <summary>
        /// エラーメッセージ
        /// </summary>
        /// <param name="text"></param>
        public static void ShowErrorMessageBox(string text)
        {
            MessageBox.Show(text, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
