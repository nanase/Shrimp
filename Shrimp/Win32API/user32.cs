using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Shrimp.Win32API
{
    public class User32
    {
        private const UInt32 FLASHW_STOP = 0;        // 点滅を止める
        private const UInt32 FLASHW_CAPTION = 1;     // タイトルバーを点滅させる
        private const UInt32 FLASHW_TRAY = 2;        // タスクバー・ボタンを点滅させる
        private const UInt32 FLASHW_ALL = 3;         // タスクバー・ボタンとタイトルバーを点滅させる
        private const UInt32 FLASHW_TIMER = 4;       // FLASHW_STOPが指定されるまでずっと点滅させる
        private const UInt32 FLASHW_TIMERNOFG = 12;  // ウィンドウが最前面に来るまでずっと点滅させる
        
        [StructLayout(LayoutKind.Sequential)]
        private struct FLASHWINFO
        {
            public UInt32 cbSize;    // FLASHWINFO構造体のサイズ
            public IntPtr hwnd;      // 点滅対象のウィンドウ・ハンドル
            public UInt32 dwFlags;   // 以下の「FLASHW_XXX」のいずれか
            public UInt32 uCount;    // 点滅する回数
            public UInt32 dwTimeout; // 点滅する間隔（ミリ秒単位）
        }
        
        public static void FlashWindow(Form form, bool isStop = false)
        {
            FLASHWINFO fInfo = new FLASHWINFO()
            {
                cbSize = (uint)Marshal.SizeOf(typeof(FLASHWINFO)),
                hwnd = form.Handle,
                dwFlags = (isStop ? FLASHW_STOP : FLASHW_ALL),
                uCount = 5, // 点滅回数
                dwTimeout = 0,
            };

            FlashWindowEx(ref fInfo);
        }

        [DllImport("user32.dll")]
        private static extern Int32 FlashWindowEx(ref FLASHWINFO pwfi);
    }
}
