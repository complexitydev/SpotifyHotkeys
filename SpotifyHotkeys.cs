using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;

namespace SpotifyHotkeys
{
    class Program
    {
        /// <summary>
        /// Gets Spotify's main window handle, set it to foreground
        /// </summary>
        /// <returns>IntPtr WindowHandle</returns>
        static IntPtr SetSpotifyForeground()
        {
            Process proc = Process.GetProcessesByName("Spotify")[0];
            IntPtr spotify_window = proc.MainWindowHandle;
            Win32.SetForegroundWindow(spotify_window);
            return spotify_window;
        }
        /// <summary>
        /// Sets foreground window, takes handle as argument
        /// </summary>
        static void ResetForegroundProcess(IntPtr handle)
        {
            Win32.SetForegroundWindow(handle);
        }
        static void Main(string[] args)
        {
            const int WM_KEYDOWN = 0x0100;
            const int WM_KEYUP = 0x0101;
            const int VK_CTRL = 0x11;
            const int VK_RIGHT = 0x27;
            const int VK_SPACE = 0x20;
            const int VK_LEFT = 0x25;

            //not using any sort of special timer tick nonsense
            while (true)
            {

                var current_handle = Win32.GetForegroundWindow();
                //CTRL + LEFT
                if ((Win32.GetAsyncKeyState(VK_CTRL) & 0x8000) > 0
                     && (Win32.GetAsyncKeyState(VK_LEFT) & 0x8000) > 0)
                {
                    var spotify_window = SetSpotifyForeground();
                    Win32.SendMessage(spotify_window, WM_KEYDOWN, (IntPtr)VK_CTRL, IntPtr.Zero);
                    Win32.SendMessage(spotify_window, WM_KEYDOWN, (IntPtr)VK_LEFT, IntPtr.Zero);
                    Win32.SendMessage(spotify_window, WM_KEYUP, (IntPtr)VK_CTRL, IntPtr.Zero);
                    Win32.SendMessage(spotify_window, WM_KEYUP, (IntPtr)VK_LEFT, IntPtr.Zero);
                    ResetForegroundProcess(current_handle);
                }
                //CTRL + RIGHT
                if ((Win32.GetAsyncKeyState(VK_CTRL) & 0x8000) > 0
                    && (Win32.GetAsyncKeyState(VK_RIGHT) & 0x8000) > 0)
                {
                    var spotify_window = SetSpotifyForeground();
                    Win32.SendMessage(spotify_window, WM_KEYDOWN, (IntPtr)VK_CTRL, IntPtr.Zero);
                    Win32.SendMessage(spotify_window, WM_KEYDOWN, (IntPtr)VK_RIGHT, IntPtr.Zero);
                    Win32.SendMessage(spotify_window, WM_KEYUP, (IntPtr)VK_CTRL, IntPtr.Zero);
                    Win32.SendMessage(spotify_window, WM_KEYUP, (IntPtr)VK_RIGHT, IntPtr.Zero);
                    ResetForegroundProcess(current_handle);
                }
                //CTRL + SPACE
                if ((Win32.GetAsyncKeyState(VK_CTRL) & 0x8000) > 0
                     && (Win32.GetAsyncKeyState(VK_SPACE) & 0x8000) > 0)
                {
                    var spotify_window = SetSpotifyForeground();
                    Win32.SendMessage(spotify_window, WM_KEYDOWN, (IntPtr)VK_CTRL, IntPtr.Zero);
                    Win32.SendMessage(spotify_window, WM_KEYDOWN, (IntPtr)VK_SPACE, IntPtr.Zero);
                    Win32.SendMessage(spotify_window, WM_KEYUP, (IntPtr)VK_CTRL, IntPtr.Zero);
                    Win32.SendMessage(spotify_window, WM_KEYUP, (IntPtr)VK_SPACE, IntPtr.Zero);
                    ResetForegroundProcess(current_handle);
                }
                Thread.Sleep(275);
            }
        }

    }
    class Win32
    {

        [DllImport("User32.dll")]
        public static extern short GetAsyncKeyState(System.Int32 vKey);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
    }
}
