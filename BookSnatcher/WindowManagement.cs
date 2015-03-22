using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

public static class WindowManagement
{
    public static Rectangle GetWindowRect(IntPtr handle)
    {
        RECT rect = new RECT();

        GetWindowRect(handle, out rect);

        return new Rectangle(
            rect.Left,
            rect.Top,
            rect.Right - rect.Left + 1,
            rect.Bottom - rect.Top + 1
        );
    }

    public static string GetWindowTitle(IntPtr handle)
    {
        int title_size = GetWindowTextLength(handle);

        if (title_size++ > 0)
        {
            StringBuilder buffer = new StringBuilder(title_size);

            GetWindowText(handle, buffer, buffer.Capacity);

            return buffer.ToString();
        }

        return "**GetWindowTitle Failed**";
    }

    public static List<IntPtr> FindWindowsWithText(string title_text, bool ignore_case)
    {
        IntPtr found = IntPtr.Zero;
        List<IntPtr> windows = new List<IntPtr>();

        EnumWindows(delegate(IntPtr handle, IntPtr param)
        {
            if (ignore_case)
            {
                if (GetWindowTitle(handle).Contains(title_text, StringComparison.OrdinalIgnoreCase))
                {
                    windows.Add(handle);
                }
            }
            else
            {
                if (GetWindowTitle(handle).Contains(title_text))
                {
                    windows.Add(handle);
                }
            }
            return true;
        },
            IntPtr.Zero
        );

        return windows.ToList();
    }

    public static bool ChangeWindowStatus(IntPtr handle, WindowShowStyle style)
    {
        return ShowWindow(handle, style);
    }

    public static bool HideWindow(IntPtr handle)
    {
        return ChangeWindowStatus(handle, WindowShowStyle.Hide);
    }

    public static bool ShowWindow(IntPtr handle)
    {
        return ChangeWindowStatus(handle, WindowShowStyle.Show);
    }

    public static Size GetWindowSize(IntPtr handle)
    {
        Rectangle rect = GetWindowRect(handle);

        return new Size(rect.Width, rect.Height);
    }

    public static Point GetWindowPosition(IntPtr handle)
    {
        Rectangle rect = GetWindowRect(handle);

        return new Point(rect.X, rect.Y);
    }

    public static bool MoveWindow(IntPtr handle, Point new_postion)
    {
        Point old_postion = GetWindowPosition(handle);

        return SetWindowPos(handle, IntPtr.Zero, 0, 0, 0, 0, SetWindowPosFlags.IgnoreResize);
    }

    public static void Animate(IntPtr handle, int time, AnimateWindowFlags a_flags)
    {
        if (handle == IntPtr.Zero)
            throw new NullReferenceException("The given handle " + handle.ToString() + " is invalid");

        if (time <= 0)
            time = 1000;

        AnimateWindow(handle, time, a_flags);
    }

    public static void CloseWindow(IntPtr handle)
    {
        SendMessage(handle, 0x0010, IntPtr.Zero, IntPtr.Zero);
    }

    public static void SetTextBoxText(IntPtr handle)
    {
        SendMessage(handle, 0x00F5, IntPtr.Zero, IntPtr.Zero);
    }

    public static string GetTextBoxText(IntPtr handle)
    {
        // Gets the length of the text inside the textbox
        int bufferSize = SendMessage(handle, 0x000E, IntPtr.Zero, IntPtr.Zero);

        if (bufferSize <= 0)
            return "";

        // Create a buffer
        StringBuilder buffer = new StringBuilder(bufferSize);

        // Grab the text, insert into buffer
        SendMessage(handle, 0x000C, bufferSize, buffer);

        return bufferSize.ToString();
    }

    public static void ClickButton(IntPtr handle)
    {
        SendMessage(handle, 0x00F5, IntPtr.Zero, IntPtr.Zero);
    }

    #region Extensions
    // Stolen
    public static bool Contains(this string source, string toCheck, StringComparison comp)
    {
        return source.IndexOf(toCheck, comp) >= 0;
    }
    #endregion

    #region Win32
    delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
    [DllImport("user32")]
    static extern bool AnimateWindow(IntPtr hwnd, int time, AnimateWindowFlags flags);

    [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
    static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
    static extern IntPtr FindWindowByCaption(IntPtr makeMeZero, string lpWindowName);

    [DllImport("user32.dll", EntryPoint = "FindWindowEx", SetLastError = true)]
    static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    static extern bool SendMessage(IntPtr hWnd, uint Msg, int bufferLen, StringBuilder buffer);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    static extern int GetWindowText(IntPtr hWnd, StringBuilder strText, int maxCount);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    static extern int GetWindowTextLength(IntPtr hWnd);

    [DllImport("user32.dll")]
    static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);

    [DllImport("user32.dll")]
    static extern bool ShowWindow(IntPtr hWnd, WindowShowStyle nCmdShow);

    [DllImport("user32.dll")]
    static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

    [DllImport("user32.dll")]
    static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, SetWindowPosFlags uFlags);

    [StructLayout(LayoutKind.Sequential)]
    struct RECT
    {
        public int Left;        // x position of upper-left corner
        public int Top;         // y position of upper-left corner
        public int Right;       // x position of lower-right corner
        public int Bottom;      // y position of lower-right corner
    }

    public enum WindowShowStyle : uint
    {
        /// <summary>Hides the window and activates another window.</summary>
        /// <remarks>See SW_HIDE</remarks>
        Hide = 0,
        /// <summary>Activates and displays a window. If the window is minimized 
        /// or maximized, the system restores it to its original size and 
        /// position. An application should specify this flag when displaying 
        /// the window for the first time.</summary>
        /// <remarks>See SW_SHOWNORMAL</remarks>
        ShowNormal = 1,
        /// <summary>Activates the window and displays it as a minimized window.</summary>
        /// <remarks>See SW_SHOWMINIMIZED</remarks>
        ShowMinimized = 2,
        /// <summary>Activates the window and displays it as a maximized window.</summary>
        /// <remarks>See SW_SHOWMAXIMIZED</remarks>
        ShowMaximized = 3,
        /// <summary>Maximizes the specified window.</summary>
        /// <remarks>See SW_MAXIMIZE</remarks>
        Maximize = 3,
        /// <summary>Displays a window in its most recent size and position. 
        /// This value is similar to "ShowNormal", except the window is not 
        /// actived.</summary>
        /// <remarks>See SW_SHOWNOACTIVATE</remarks>
        ShowNormalNoActivate = 4,
        /// <summary>Activates the window and displays it in its current size 
        /// and position.</summary>
        /// <remarks>See SW_SHOW</remarks>
        Show = 5,
        /// <summary>Minimizes the specified window and activates the next 
        /// top-level window in the Z order.</summary>
        /// <remarks>See SW_MINIMIZE</remarks>
        Minimize = 6,
        /// <summary>Displays the window as a minimized window. This value is 
        /// similar to "ShowMinimized", except the window is not activated.</summary>
        /// <remarks>See SW_SHOWMINNOACTIVE</remarks>
        ShowMinNoActivate = 7,
        /// <summary>Displays the window in its current size and position. This 
        /// value is similar to "Show", except the window is not activated.</summary>
        /// <remarks>See SW_SHOWNA</remarks>
        ShowNoActivate = 8,
        /// <summary>Activates and displays the window. If the window is 
        /// minimized or maximized, the system restores it to its original size 
        /// and position. An application should specify this flag when restoring 
        /// a minimized window.</summary>
        /// <remarks>See SW_RESTORE</remarks>
        Restore = 9,
        /// <summary>Sets the show state based on the SW_ value specified in the 
        /// STARTUPINFO structure passed to the CreateProcess function by the 
        /// program that started the application.</summary>
        /// <remarks>See SW_SHOWDEFAULT</remarks>
        ShowDefault = 10,
        /// <summary>Windows 2000/XP: Minimizes a window, even if the thread 
        /// that owns the window is hung. This flag should only be used when 
        /// minimizing windows from a different thread.</summary>
        /// <remarks>See SW_FORCEMINIMIZE</remarks>
        ForceMinimized = 11
    }

    public enum SetWindowPosFlags : uint
    {
        /// <summary>If the calling thread and the thread that owns the window are attached to different input queues, 
        /// the system posts the request to the thread that owns the window. This prevents the calling thread from 
        /// blocking its execution while other threads process the request.</summary>
        /// <remarks>SWP_ASYNCWINDOWPOS</remarks>
        AsynchronousWindowPosition = 0x4000,
        /// <summary>Prevents generation of the WM_SYNCPAINT message.</summary>
        /// <remarks>SWP_DEFERERASE</remarks>
        DeferErase = 0x2000,
        /// <summary>Draws a frame (defined in the window's class description) around the window.</summary>
        /// <remarks>SWP_DRAWFRAME</remarks>
        DrawFrame = 0x0020,
        /// <summary>Applies new frame styles set using the SetWindowLong function. Sends a WM_NCCALCSIZE message to 
        /// the window, even if the window's size is not being changed. If this flag is not specified, WM_NCCALCSIZE 
        /// is sent only when the window's size is being changed.</summary>
        /// <remarks>SWP_FRAMECHANGED</remarks>
        FrameChanged = 0x0020,
        /// <summary>Hides the window.</summary>
        /// <remarks>SWP_HIDEWINDOW</remarks>
        HideWindow = 0x0080,
        /// <summary>Does not activate the window. If this flag is not set, the window is activated and moved to the 
        /// top of either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter 
        /// parameter).</summary>
        /// <remarks>SWP_NOACTIVATE</remarks>
        DoNotActivate = 0x0010,
        /// <summary>Discards the entire contents of the client area. If this flag is not specified, the valid 
        /// contents of the client area are saved and copied back into the client area after the window is sized or 
        /// repositioned.</summary>
        /// <remarks>SWP_NOCOPYBITS</remarks>
        DoNotCopyBits = 0x0100,
        /// <summary>Retains the current position (ignores X and Y parameters).</summary>
        /// <remarks>SWP_NOMOVE</remarks>
        IgnoreMove = 0x0002,
        /// <summary>Does not change the owner window's position in the Z order.</summary>
        /// <remarks>SWP_NOOWNERZORDER</remarks>
        DoNotChangeOwnerZOrder = 0x0200,
        /// <summary>Does not redraw changes. If this flag is set, no repainting of any kind occurs. This applies to 
        /// the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent 
        /// window uncovered as a result of the window being moved. When this flag is set, the application must 
        /// explicitly invalidate or redraw any parts of the window and parent window that need redrawing.</summary>
        /// <remarks>SWP_NOREDRAW</remarks>
        DoNotRedraw = 0x0008,
        /// <summary>Same as the SWP_NOOWNERZORDER flag.</summary>
        /// <remarks>SWP_NOREPOSITION</remarks>
        DoNotReposition = 0x0200,
        /// <summary>Prevents the window from receiving the WM_WINDOWPOSCHANGING message.</summary>
        /// <remarks>SWP_NOSENDCHANGING</remarks>
        DoNotSendChangingEvent = 0x0400,
        /// <summary>Retains the current size (ignores the cx and cy parameters).</summary>
        /// <remarks>SWP_NOSIZE</remarks>
        IgnoreResize = 0x0001,
        /// <summary>Retains the current Z order (ignores the hWndInsertAfter parameter).</summary>
        /// <remarks>SWP_NOZORDER</remarks>
        IgnoreZOrder = 0x0004,
        /// <summary>Displays the window.</summary>
        /// <remarks>SWP_SHOWWINDOW</remarks>
        ShowWindow = 0x0040,
    }

    public enum AnimateWindowFlags : uint
    {
        /// <summary>
        /// Animates the window from left to right. This flag can be used with roll or slide animation. It is ignored when used with AW_CENTER or AW_BLEND.
        /// </summary>
        AW_HOR_POSITIVE = 0x00000001,
        /// <summary>
        /// Animates the window from right to left. This flag can be used with roll or slide animation. It is ignored when used with AW_CENTER or AW_BLEND.
        /// </summary>
        AW_HOR_NEGATIVE = 0x00000002,
        /// <summary>
        /// Animates the window from top to bottom. This flag can be used with roll or slide animation. It is ignored when used with AW_CENTER or AW_BLEND. 
        /// </summary>
        AW_VER_POSITIVE = 0x00000004,
        /// <summary>
        /// Animates the window from bottom to top. This flag can be used with roll or slide animation. It is ignored when used with AW_CENTER or AW_BLEND. 
        /// </summary>
        AW_VER_NEGATIVE = 0x00000008,
        /// <summary>
        /// Makes the window appear to collapse inward if AW_HIDE is used or expand outward if the AW_HIDE is not used. The various direction flags have no effect. 
        /// </summary>
        AW_CENTER = 0x00000010,
        /// <summary>
        /// Hides the window. By default, the window is shown. 
        /// </summary>
        AW_HIDE = 0x00010000,
        /// <summary>
        /// Activates the window. Do not use this value with AW_HIDE. 
        /// </summary>
        AW_ACTIVATE = 0x00020000,
        /// <summary>
        /// Uses slide animation. By default, roll animation is used. This flag is ignored when used with AW_CENTER. 
        /// </summary>
        AW_SLIDE = 0x00040000,
        /// <summary>
        /// Uses a fade effect. This flag can be used only if hwnd is a top-level window.
        /// </summary>
        AW_BLEND = 0x00080000
    }
    #endregion

}
