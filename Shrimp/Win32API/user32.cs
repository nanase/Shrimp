using System;
using System.Runtime.InteropServices;

namespace Shrimp.Win32API
{
    public class user32
    {
        [DllImport("user32.dll")]
        static extern Int32 FlashWindowEx(ref FLASHWINFO pwfi);

        [StructLayout(LayoutKind.Sequential)]
        public struct FLASHWINFO
        {
            public UInt32 cbSize;    // FLASHWINFO構造体のサイズ
            public IntPtr hwnd;      // 点滅対象のウィンドウ・ハンドル
            public UInt32 dwFlags;   // 以下の「FLASHW_XXX」のいずれか
            public UInt32 uCount;    // 点滅する回数
            public UInt32 dwTimeout; // 点滅する間隔（ミリ秒単位）
        }


        public const UInt32 FLASHW_STOP = 0;        // 点滅を止める
        public const UInt32 FLASHW_CAPTION = 1;     // タイトルバーを点滅させる
        public const UInt32 FLASHW_TRAY = 2;        // タスクバー・ボタンを点滅させる
        public const UInt32 FLASHW_ALL = 3;         // タスクバー・ボタンとタイトルバーを点滅させる
        public const UInt32 FLASHW_TIMER = 4;       // FLASHW_STOPが指定されるまでずっと点滅させる
        public const UInt32 FLASHW_TIMERNOFG = 12;  // ウィンドウが最前面に来るまでずっと点滅させる

        public static void FlashWindow(IntPtr hWnd, bool isStop = false)
        {
            FLASHWINFO fInfo = new FLASHWINFO();
            fInfo.cbSize = Convert.ToUInt32(Marshal.SizeOf(fInfo));
            fInfo.hwnd = hWnd;
            fInfo.dwFlags = (isStop ? FLASHW_STOP : FLASHW_ALL);
            fInfo.uCount = 5; // 点滅回数
            fInfo.dwTimeout = 0;

            FlashWindowEx(ref fInfo);
        }
    }
}
