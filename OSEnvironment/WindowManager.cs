using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using Utils;

namespace OSEnvironment
{
    public class WindowManager
    {
        //[DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        //public static extern IntPtr FindWindow(String lpClassName, String lpWindowName);

        //[DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        //static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        //private IntPtr u32FindWindow(string caption)
        //{
        //    return FindWindow(null, caption);
        //}

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);
        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        public WindowManager()
        {

        }

        private Process[] _windows;

        public Process[] ListWindows()
        {
            return ListWindows((Process p) => { return true; });
        }

        public Process[] ListWindows(Func<Process, bool> filter)
        {
            return Procedural.Filter((Process p) =>
            {
                return p.MainWindowTitle != null && !p.MainWindowTitle.Equals("") && filter(p);
            },
            Process.GetProcesses());
        }

        public int FindWindows(string[] nameFragments)
        {
            _windows = ListWindows((Process p) =>
            {
                var t = p.MainWindowTitle.ToLower();

                foreach(string nf in nameFragments) {
                    if (t.Contains(nf)) return true;
                }

                return false;
            });

            return _windows.Length;
        }

        public bool BringToFront(int i)
        {
            if (_windows == null || i < 0 || i >= _windows.Length) return false;

            SetForegroundWindow(_windows[i].MainWindowHandle);
            return true;
        }

        
        public Bitmap CaptureWindow(int i)
        {
            if (!BringToFront(i)) return new Bitmap(0, 0);

            const int DELAY = 250;
            Thread.Sleep(DELAY);

            RECT bounds = new RECT();
            GetWindowRect(_windows[i].MainWindowHandle, ref bounds);
            if (bounds.Left == bounds.Right && bounds.Top == bounds.Bottom) return new Bitmap(0, 0);

            var gc = new GraphicsCapture(bounds.Left, bounds.Top, bounds.Right - bounds.Left, bounds.Bottom - bounds.Top);
            var img = gc.Img;
            return img;
        }
    }
}
