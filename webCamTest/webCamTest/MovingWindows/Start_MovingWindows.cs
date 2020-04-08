using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;


namespace webCamTest.MovingWindows
{
    class Start_MovingWindows
    {

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        //[DllImport("user32.dll", SetLastError = true)]
        //static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, SetWindowPosFlags uFlags);

        internal struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }
        //[DllImport("USER32.DLL")]
        //public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);



        // Call this way:
        private void MoveWindowsIfHasFocus()
        {
            while (true)
            {
                try
                {
                    int chars = 256;
                    StringBuilder buff = new StringBuilder(chars);
                    IntPtr handle = GetForegroundWindow();

                    string RET = "";
                    if (handle != null)
                    {
                        if (GetWindowText(handle, buff, chars) > 0)
                        {
                            RET = buff.ToString() + "\n";
                            //listBox2.Items.Add(handle.ToString());                   
                        }
                    }

                    //if (RET.Contains("desktop-head-unit"))
                    //{
                    //    ShowWindow(handle, 2);

                    //}

                    //if (RET.Contains("Android Auto - Desktop Head Unit"))
                    //{
                    //    Rectangle workingRectangle = Screen.PrimaryScreen.WorkingArea;
                    //    MoveWindow(handle, -20, -30, (workingRectangle.Width + 40), workingRectangle.Height + 50, false);
                    //}

                    else
                    {
                        Screen[] allScreens = Screen.AllScreens;
                        Rectangle workingRectangle = new Rectangle();

                        Process[] localAll1 = Process.GetProcesses();
                        IntPtr a = new IntPtr();
                        foreach (Process proc in localAll1)
                        {
                            a = proc.MainWindowHandle;

                            //if (proc.MainWindowTitle == "Android Auto - Desktop Head Unit")
                            //{
                            //    foreach (Screen x in allScreens)
                            //    {
                            //        if (x.Primary == false)
                            //        {
                            //            workingRectangle = x.WorkingArea;
                            //            break;
                            //        }
                            //    }
                            //    MoveWindow(a, -3850, -35, workingRectangle.Width + 30, workingRectangle.Height + 50, true);
                            //}

                            workingRectangle = Screen.PrimaryScreen.WorkingArea;

                            if (proc.MainWindowTitle.Contains("Chrome"))
                            {
                                MoveWindow(a, 295, 0, (workingRectangle.Width - 275), workingRectangle.Height + 10, true);
                            }

                            //if (proc.MainWindowTitle.Contains("Camera"))
                            //{
                            //    MoveWindow(a, 295, -10, (workingRectangle.Width - 275), workingRectangle.Height + 20, true);
                            //}

                            if (proc.MainWindowTitle.Contains("Spotify"))
                            {
                                MoveWindow(a, 304, 0, (workingRectangle.Width - 275), workingRectangle.Height + 0, true);
                            }
                            if (proc.MainWindowTitle.Contains("Macro"))
                            {
                                MoveWindow(a, -10, 0, (325), workingRectangle.Height + 10, true);
                            }

                        }
                    }
                    //Console.Write(RET);
                }
                catch
                {
                    Console.Write("NO Windows Active");
                }
                Thread.Sleep(1000);
            }
        }



        public void StartMovingWindows()
        {
            Thread a = new Thread(MoveWindowsIfHasFocus);
            a.Start();

            StartProgramAndMoveIt("chrome");
            StartProgramAndMoveIt("Spotify");


            //while (true)
            //{
            //    string val;
            //    Console.Write("Enter integer: ");
            //    val = Console.ReadLine();

            //    //Check for chrome
            //    if (val == "c")
            //    {
            //        Process[] localAll1 = Process.GetProcesses();

            //        foreach (Process proc in localAll1)
            //        {
            //            if (proc.ProcessName == "chrome")
            //            {
            //                IntPtr x = proc.MainWindowHandle;
            //                SetForegroundWindow(x);
            //                isChromeStartedAlready = true;
            //            }
            //        }
            //        if(isChromeStartedAlready == false)
            //        {
            //            StartProgramAndMoveIt("chrome");
            //        }
            //    }

            //    //Check for Spotify
            //    if (val == "s")
            //    {
            //        Process[] localAll1 = Process.GetProcesses();

            //        foreach (Process proc in localAll1)
            //        {
            //            //Process is already created, bring it to the front
            //            if (proc.ProcessName == "Spotify")
            //            {
            //                IntPtr x = proc.MainWindowHandle;
            //                SetForegroundWindow(x);
            //                isSpotifyStartedAlready = true;
            //            }
            //        }
            //        if (isSpotifyStartedAlready == false)
            //        {
            //            StartProgramAndMoveIt("Spotify");
            //        }
            //    }
            //}
        }

        private static void StartProgramAndMoveIt(string program)
        {
            //if (program == "chrome")
            //{
            //    var chromeProcess = new Process
            //    {
            //        StartInfo =
            //        {
            //        //Arguments = "https://www.google.com --new-window --start-fullscreen",
            //        FileName = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe",
            //        WindowStyle = ProcessWindowStyle.Normal
            //        }
            //    };

            //    chromeProcess.Start();

            //    // Needed to move the the process.
            //    Thread.Sleep(2000);
            //}

            //if (program == "Spotify")
            //{
            //    var spotifyProcess = new Process
            //    {
            //        StartInfo =
            //        {
            //        //Arguments = "https://www.google.com --new-window --start-fullscreen",
            //        FileName = @"C:\Users\chris\AppData\Roaming\Spotify\Spotify.exe",
            //        WindowStyle = ProcessWindowStyle.Normal
            //        }
            //    };

            //    spotifyProcess.Start();

            //    // Needed to move the the process.
            //    Thread.Sleep(2000);
            //}


            ////Get all the Screens, then Find the first screen thats not the primary screen
            ////the other screens will have the same workingArea 
            //Screen[] allScreens = Screen.AllScreens;
            //foreach(Screen x in allScreens)
            //{
            //    if (x.Primary == false)
            //    {
            //        workingRectangle = x.WorkingArea;
            //        break;
            //    }
            //}

            //Process[] localAll = Process.GetProcesses();
            //foreach (Process proc in localAll)
            //{
            //    MoveChromeAndSpotify(proc);
            //}

        }

        //private static void MoveChromeAndSpotify(Process proc)
        //{
        //    System.Drawing.Rectangle workingRectangle = Screen.PrimaryScreen.WorkingArea;
        //    if (proc.ProcessName == "chrome")
        //    {
        //        IntPtr x = proc.MainWindowHandle;
        //        // Call this way:
        //        //MoveWindow(x, 300, 0, (workingRectangle.Width / 2) + 420, workingRectangle.Height, false);

        //        bool a = SetWindowPos(x, (IntPtr)SpecialWindowHandles.HWND_TOP, 300, 0, (workingRectangle.Width / 2) + 420, workingRectangle.Height, SetWindowPosFlags.SWP_SHOWWINDOW);

        //        Thread.Sleep(10);
        //    }
        //    if (proc.ProcessName == "Spotify")
        //    {
        //        IntPtr x = proc.MainWindowHandle;
        //        // Call this way:
        //       // MoveWindow(x, 300, 0, (workingRectangle.Width / 2) + 420, workingRectangle.Height, false);
        //        bool a = SetWindowPos(x, (IntPtr)SpecialWindowHandles.HWND_TOP, 300, 0, (workingRectangle.Width / 2) + 420, workingRectangle.Height, SetWindowPosFlags.SWP_SHOWWINDOW);

        //        Thread.Sleep(10);
        //    }
        //}
    }
    /// <summary>
	///     Special window handles
	/// </summary>
	public enum SpecialWindowHandles
    {
        // ReSharper disable InconsistentNaming
        /// <summary>
        ///     Places the window at the top of the Z order.
        /// </summary>
        HWND_TOP = 0,
        /// <summary>
        ///     Places the window at the bottom of the Z order. If the hWnd parameter identifies a topmost window, the window loses its topmost status and is placed at the bottom of all other windows.
        /// </summary>
        HWND_BOTTOM = 1,
        /// <summary>
        ///     Places the window above all non-topmost windows. The window maintains its topmost position even when it is deactivated.
        /// </summary>
        HWND_TOPMOST = -1,
        /// <summary>
        ///     Places the window above all non-topmost windows (that is, behind all topmost windows). This flag has no effect if the window is already a non-topmost window.
        /// </summary>
        HWND_NOTOPMOST = -2
        // ReSharper restore InconsistentNaming
    }
    [Flags]
    public enum SetWindowPosFlags : uint
    {
        // ReSharper disable InconsistentNaming

        /// <summary>
        ///     If the calling thread and the thread that owns the window are attached to different input queues, the system posts the request to the thread that owns the window. This prevents the calling thread from blocking its execution while other threads process the request.
        /// </summary>
        SWP_ASYNCWINDOWPOS = 0x4000,

        /// <summary>
        ///     Prevents generation of the WM_SYNCPAINT message.
        /// </summary>
        SWP_DEFERERASE = 0x2000,

        /// <summary>
        ///     Draws a frame (defined in the window's class description) around the window.
        /// </summary>
        SWP_DRAWFRAME = 0x0020,

        /// <summary>
        ///     Applies new frame styles set using the SetWindowLong function. Sends a WM_NCCALCSIZE message to the window, even if the window's size is not being changed. If this flag is not specified, WM_NCCALCSIZE is sent only when the window's size is being changed.
        /// </summary>
        SWP_FRAMECHANGED = 0x0020,

        /// <summary>
        ///     Hides the window.
        /// </summary>
        SWP_HIDEWINDOW = 0x0080,

        /// <summary>
        ///     Does not activate the window. If this flag is not set, the window is activated and moved to the top of either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter parameter).
        /// </summary>
        SWP_NOACTIVATE = 0x0010,

        /// <summary>
        ///     Discards the entire contents of the client area. If this flag is not specified, the valid contents of the client area are saved and copied back into the client area after the window is sized or repositioned.
        /// </summary>
        SWP_NOCOPYBITS = 0x0100,

        /// <summary>
        ///     Retains the current position (ignores X and Y parameters).
        /// </summary>
        SWP_NOMOVE = 0x0002,

        /// <summary>
        ///     Does not change the owner window's position in the Z order.
        /// </summary>
        SWP_NOOWNERZORDER = 0x0200,

        /// <summary>
        ///     Does not redraw changes. If this flag is set, no repainting of any kind occurs. This applies to the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent window uncovered as a result of the window being moved. When this flag is set, the application must explicitly invalidate or redraw any parts of the window and parent window that need redrawing.
        /// </summary>
        SWP_NOREDRAW = 0x0008,

        /// <summary>
        ///     Same as the SWP_NOOWNERZORDER flag.
        /// </summary>
        SWP_NOREPOSITION = 0x0200,

        /// <summary>
        ///     Prevents the window from receiving the WM_WINDOWPOSCHANGING message.
        /// </summary>
        SWP_NOSENDCHANGING = 0x0400,

        /// <summary>
        ///     Retains the current size (ignores the cx and cy parameters).
        /// </summary>
        SWP_NOSIZE = 0x0001,

        /// <summary>
        ///     Retains the current Z order (ignores the hWndInsertAfter parameter).
        /// </summary>
        SWP_NOZORDER = 0x0004,

        /// <summary>
        ///     Displays the window.
        /// </summary>
        SWP_SHOWWINDOW = 0x0040,

        // ReSharper restore InconsistentNaming
    }

}
