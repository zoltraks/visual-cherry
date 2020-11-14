using System;
using System.Runtime.InteropServices;

namespace Cherry.Desktop
{
    public class WinApi
    {
        public const int EM_SETTABSTOPS = 0x00CB;

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, ref int lParam);

        [DllImport("user32.dll")]
        public static extern bool SetProcessDPIAware();
    }
}
