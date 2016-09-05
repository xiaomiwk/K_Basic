using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Utility.Windows
{
    public class ConsoleWindow
    {
        public static IntPtr CreateConsole()
        {
            var console = new ConsoleWindow();
            return console.Hwnd;
        }

        public IntPtr Hwnd { get; private set; }

        public ConsoleWindow()
        {
            Initialize();
        }

        public void Initialize()
        {
            Hwnd = GetConsoleWindow();

            // Console app
            if (Hwnd != IntPtr.Zero)
            {
                return;
            }

            // Windows app
            AllocConsole();
            Hwnd = GetConsoleWindow();
        }

        #region Win32

        [DllImport("kernel32")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("kernel32")]
        static extern bool AllocConsole();


        #endregion

        #region Windows API

        /// <summary>
        /// Flashes the specified window. It does not change the active state of the window.
        /// </summary>
        /// <param name="info">A pointer to a FLASHWINFO structure.</param>
        /// <remarks>
        /// Typically, you flash a window to inform the user that the window requires attention,
        /// but does not currently have the keyboard focus. 
        /// When a window flashes, it appears to change from inactive to active status. 
        /// An inactive caption bar changes to an active caption bar; 
        /// an active caption bar changes to an inactive caption bar.
        /// </remarks>
        //[DllImport("user32")]
        //static extern void FlashWindowEx(ref FlashWInfo info);

        /// <summary>
        /// lays a waveform sound. The waveform sound for each sound type is identified by an entry in the registry.
        /// </summary>
        /// <param name="type">ref http://msdn.microsoft.com/en-us/library/ms680356(VS.85).aspx</param>
        [DllImport("user32")]
        static extern void MessageBeep(int type);

        /// <summary>
        /// The SetWindowLong function changes an attribute of the specified window
        /// </summary>
        /// <param name="hWnd">Handle to the window and, indirectly, the class to which the window belongs.</param>
        /// <param name="nIndex">
        /// Specifies the zero-based offset to the value to be set. 
        /// Valid values are in the range zero through the number of bytes of extra window memory, 
        /// minus the size of an integer. To set any other value, specify one of the following values.
        /// ref : http://msdn.microsoft.com/en-us/library/ms633591(VS.85).aspx</param>
        /// <param name="newValue">Specifies the replacement value</param>
        /// <returns>
        /// If the function succeeds, the return value is the previous value of the specified 32-bit integer.
        /// If the function fails, the return value is zero
        /// </returns>
        [DllImport("user32")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int newValue);

        /// <summary>
        /// The GetWindowLong function retrieves information about the specified window
        /// </summary>
        /// <param name="hWnd">Handle to the window and, indirectly, the class to which the window belongs.</param>
        /// <param name="nIndex">
        /// Specifies the zero-based offset to the value to be set. 
        /// Valid values are in the range zero through the number of bytes of extra window memory, 
        /// minus the size of an integer. To set any other value, specify one of the following values.
        /// ref : http://msdn.microsoft.com/en-us/library/ms633591(VS.85).aspx</param>
        /// <param name="newValue">Specifies the replacement value</param>
        /// <returns>
        /// If the function succeeds, the return value is the requested 32-bit value.
        /// If the function fails, the return value is zero.
        ///</returns>
        [DllImport("user32")]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        /// <summary>
        /// Allocates a new console for the calling process
        /// </summary>
        /// <returns>If the function succeeds, return true</returns>
        //[DllImport("kernel32")]
        //static extern bool AllocConsole();

        /// <summary>
        /// Detaches the calling process from its console.
        /// </summary>
        /// <returns>If the function succeeds, return true.</returns>
        [DllImport("kernel32")]
        static extern bool FreeConsole();

        /// <summary>
        /// Retrieves information about the specified console screen buffer
        /// </summary>
        /// <param name="consoleOutput">
        /// A handle to the console screen buffer. 
        /// The handle must have the GENERIC_READ access right. 
        /// For more information, see Console Buffer Security and Access Rights.</param>
        /// <param name="info">A pointer to a CONSOLE_SCREEN_BUFFER_INFO structure that receives the console screen buffer information</param>
        /// <returns>If the function succeeds, return true.</returns>
        //[DllImport("kernel32")]
        //static extern bool GetConsoleScreenBufferInfo(IntPtr consoleOutput, out ConsoleScreenBufferInfo info);

        /// <summary>
        /// Retrieves the title for the current console window.
        /// </summary>
        /// <param name="text">
        /// (out) A pointer to a buffer that receives a null-terminated string containing the title. 
        /// The total size of the buffer required will be less than 64K.
        /// </param>
        /// <param name="size">The size of the buffer pointed to by the lpConsoleTitle parameter, in characters.</param>
        /// <returns>If the function succeeds, return true</returns>
        [DllImport("kernel32")]
        static extern bool GetConsoleTitle(StringBuilder text, int size);

        /// <summary>
        /// Retrieves the window handle used by the console associated with the calling process.
        /// more info : http://msdn.microsoft.com/en-us/library/ms683175(VS.85).aspx
        /// </summary>
        /// <returns>the window handle used by the console associated with the calling process.</returns>
        //[DllImport("kernel32")]
        //static extern IntPtr GetConsoleWindow();

        /// <summary>
        /// Retrieves a handle to the specified standard device (standard input, standard output, or standard error).
        /// </summary>
        /// <param name="handle">
        /// STD_INPUT_HANDLE, (DWORD)-10
        ///     The standard input device. Initially, this is the console input buffer, CONIN$.
        /// STD_OUTPUT_HANDLE, (DWORD)-11
        ///     The standard output device. Initially, this is the active console screen buffer, CONOUT$.
        /// STD_ERROR_HANDLE, (DWORD)-12
        ///     The standard error device. Initially, this is the active console screen buffer, CONOUT$.
        /// </param>
        /// <returns>The handle to the specified standard device </returns>
        [DllImport("kernel32")]
        static extern IntPtr GetStdHandle(int handle);

        /// <summary>
        /// Sets the cursor position in the specified console screen buffer.
        /// </summary>
        /// <param name="buffer">A handle to the console screen buffer.</param>
        /// <param name="position">
        /// A COORD structure that specifies the new cursor position, in characters.
        /// The coordinates are the column and row of a screen buffer character cell.
        /// The coordinates must be within the boundaries of the console screen buffer.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero.
        ///</returns>
        //[DllImport("kernel32")]
        //static extern int SetConsoleCursorPosition(IntPtr buffer, Coord position);

        /// <summary>
        /// Writes a character to the console screen buffer a specified number of times,
        /// beginning at the specified coordinates.
        /// </summary>
        /// <param name="buffer">
        /// A handle to the console screen buffer.
        /// The handle must have the GENERIC_WRITE access right</param>
        /// <param name="character">The character to be written to the console screen buffer</param>
        /// <param name="length">The number of character cells to which the character should be written.</param>
        /// <param name="position">A COORD structure that specifies the character coordinates of the first cell to which the character is to be written</param>
        /// <param name="written">A pointer to a variable that receives the number of characters actually written to the console screen buffer</param>
        /// <returns>If the function succeeds, the return value is nonzero.</returns>
        //[DllImport("kernel32")]
        //static extern int FillConsoleOutputCharacter(IntPtr buffer, char character, int length, Coord position, out int written);

        /// <summary>
        /// Sets the attributes of characters written to the console screen buffer by the WriteFile or WriteConsole function, 
        /// or echoed by the ReadFile or ReadConsole function. 
        /// This function affects text written after the function call.
        /// </summary>
        /// <param name="hConsoleOutput">A handle to the console screen buffer. The handle must have the GENERIC_READ access right</param>
        /// <param name="wAttributes">The character attributes.</param>
        /// <returns>If the function succeeds, the return value is True.</returns>
        [DllImport("kernel32")]
        static extern bool SetConsoleTextAttribute(IntPtr hConsoleOutput, ConsoleColor wAttributes);

        /// <summary>
        /// Sets the title for the current console window.
        /// </summary>
        /// <param name="lpConsoleTitle">The string to be displayed in the title bar of the console window. The total size must be less than 64K</param>
        /// <returns>If the function succeeds, the return value is True.</returns>
        [DllImport("kernel32")]
        static extern bool SetConsoleTitle(string lpConsoleTitle);

        /// <summary>
        /// Adds or removes an application-defined HandlerRoutine function from the list of handler functions for the calling process.
        /// </summary>
        /// <param name="routine"></param>
        /// <param name="add">
        /// If this parameter is TRUE, the handler is added; if it is FALSE, the handler is removed. 
        /// If the HandlerRoutine parameter is NULL, 
        /// a TRUE value causes the calling process to ignore CTRL+C input, 
        /// and a FALSE value restores normal processing of CTRL+C input. 
        /// This attribute of ignoring or processing CTRL+C is inherited by child processes.
        /// </param>
        /// <returns>If the function succeeds, the return value is nonzero.</returns>
        //[DllImport("kernel32")]
        //static extern bool SetConsoleCtrlHandler(HandlerRoutine routine, bool add);

        /// <summary>
        /// The ShowWindow function sets the specified window's show state.
        /// </summary>
        /// <param name="hwnd">Handle to the window</param>
        /// <param name="nCmdShow">
        /// Specifies how the window is to be shown.
        /// This parameter is ignored the first time an application calls ShowWindow,
        /// if the program that launched the application provides a STARTUPINFO structure.
        /// Otherwise, the first time ShowWindow is called, 
        /// the value should be the value obtained by the WinMain function in its nCmdShow parameter.
        /// In subsequent calls, this parameter can be one of the following values.
        /// ref http://msdn.microsoft.com/en-us/library/ms633548(VS.85).aspx</param>
        /// <returns>If the window was previously visible, the return value is True</returns>
        [DllImport("user32")]
        static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);

        /// <summary>
        /// Retrieves the visibility state of the specified window.
        /// </summary>
        /// <param name="hwnd">Handle to the window to test</param>
        [DllImport("user32")]
        static extern bool IsWindowVisible(IntPtr hwnd);

        /// <summary>
        /// Creates a console screen buffer.
        /// </summary>
        /// <param name="access">The access to the console screen buffer.</param>
        /// <param name="share">
        /// This parameter can be zero, indicating that the buffer cannot be shared, 
        /// or it can be one or more of the following values.
        /// </param>
        /// <param name="security">
        /// A pointer to a SECURITY_ATTRIBUTES structure that determines whether the returned handle can be inherited by child processes.
        /// If lpSecurityAttributes is NULL, the handle cannot be inherited.
        /// The lpSecurityDescriptor member of the structure specifies a security descriptor for the new console screen buffer.
        /// If lpSecurityAttributes is NULL, the console screen buffer gets a default security descriptor.
        /// The ACLs in the default security descriptor for a console screen buffer come from the primary or impersonation token of the creator.
        /// </param>
        /// <param name="flags">The type of console screen buffer to create. The only supported screen buffer type is CONSOLE_TEXTMODE_BUFFER</param>
        /// <param name="reserved">Reserved; should be NULL.</param>
        /// <returns>If the function succeeds, the return value is a handle to the new console screen buffer.</returns>
        [DllImport("kernel32")]
        static extern IntPtr CreateConsoleScreenBuffer(int access, int share, IntPtr security, int flags, IntPtr reserved);

        /// <summary>
        /// Sets the specified screen buffer to be the currently displayed console screen buffer.
        /// </summary>
        /// <param name="handle">A handle to the console screen buffer.</param>
        /// <returns>If the function succeeds, the return value is True.</returns>
        [DllImport("kernel32")]
        static extern bool SetConsoleActiveScreenBuffer(IntPtr handle);

        /// <summary>
        /// Writes a character string to a console screen buffer beginning at the current cursor location.
        /// </summary>
        /// <param name="handle">A handle to the console screen buffer. The handle must have the GENERIC_WRITE access right</param>
        /// <param name="s">A pointer to a buffer that contains characters to be written to the console screen buffer.</param>
        /// <param name="length">
        /// The number of TCHARs to write. 
        /// If the total size of the specified number of characters exceeds 64 KB, 
        /// the function fails with ERROR_NOT_ENOUGH_MEMORY.</param>
        /// <param name="written">A pointer to a variable that receives the number of TCHARs actually written.</param>
        /// <param name="reserved">Reserved; must be NULL.</param>
        /// <returns>If the function succeeds, the return value is True.</returns>
        [DllImport("kernel32")]
        static extern bool WriteConsole(IntPtr handle, string s, int length, out int written, IntPtr reserved);

        /// <summary>
        /// Retrieves the input code page used by the console associated with the calling process. 
        /// A console uses its input code page to translate keyboard input into the corresponding character value.
        /// </summary>
        /// <returns>
        /// The return value is a code that identifies the code page.
        /// </returns>
        [DllImport("kernel32")]
        static extern int GetConsoleCP();

        /// <summary>
        /// Retrieves the output code page used by the console associated with the calling process. 
        /// A console uses its output code page to translate the character values written by the various output functions into the images displayed in the console window
        /// </summary>
        /// <returns>The return value is a code that identifies the code page</returns>
        [DllImport("kernel32")]
        static extern int GetConsoleOutputCP();

        /// <summary>
        /// Retrieves the current input mode of a console's input buffer 
        /// or the current output mode of a console screen buffer.
        /// </summary>
        /// <param name="handle">
        /// A handle to the console input buffer or the console screen buffer. 
        /// The handle must have the GENERIC_READ access right
        /// </param>
        /// <param name="flags">
        /// A pointer to a variable that receives the current mode of the specified buffer.
        /// ref : http://msdn.microsoft.com/en-us/library/ms683167(VS.85).aspx
        /// </param>
        /// <returns>If the function succeeds, the return value is True.</returns>
        [DllImport("kernel32")]
        static extern bool GetConsoleMode(IntPtr handle, out int flags);

        /// <summary>
        /// Sets the handle for the specified standard device 
        /// (standard input, standard output, or standard error).
        /// </summary>
        /// <param name="handle1">T
        /// he standard device for which the handle is to be set
        /// (STD_INPUT_HANDLE,STD_OUTPUT_HANDLE,STD_ERROR_HANDLE)
        /// </param>
        /// <param name="handle2">The handle for the standard device.</param>
        /// <returns>If the function succeeds, the return value is True.</returns>
        [DllImport("kernel32")]
        static extern bool SetStdHandle(int handle1, IntPtr handle2);

        /// <summary>
        /// The SetParent function changes the parent window of the specified child window.
        /// </summary>
        /// <param name="hwnd">Handle to the child window.</param>
        /// <param name="hwnd2">
        /// Handle to the new parent window. 
        /// If this parameter is NULL, the desktop window becomes the new parent window
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is a handle to the previous parent window.
        /// If the function fails, the return value is NULL.
        /// </returns>
        [DllImport("user32")]
        static extern IntPtr SetParent(IntPtr hwnd, IntPtr hwnd2);

        /// <summary>
        /// The GetParent function retrieves a handle to the specified window's parent or owner.
        /// </summary>
        /// <param name="hwnd">Handle to the window whose parent window handle is to be retrieved</param>
        /// <returns>
        /// If the window is a child window, the return value is a handle to the parent window. 
        /// If the window is a top-level window, the return value is a handle to the owner window. 
        /// If the window is a top-level unowned window or if the function fails, the return value is NULL
        /// </returns>
        [DllImport("user32")]
        static extern IntPtr GetParent(IntPtr hwnd);

        /// <summary>
        /// Changes the size, position, and Z order of a child, pop-up, or top-level window. 
        /// These windows are ordered according to their appearance on the screen. 
        /// The topmost window receives the highest rank and is the first window in the Z order
        /// </summary>
        /// <param name="hWnd"> A handle to the window.</param>
        /// <param name="hWndInsertAfter">
        /// A handle to the window to precede the positioned window in the Z order.
        /// This parameter must be a window handle or one of the following values:
        /// HWND_BOTTOM, HWND_NOTOPMOST,HWND_TOP,HWND_TOPMOST
        /// </param>
        /// <param name="x">Specifies the new position of the left side of the window, in client coordinates.</param>
        /// <param name="y">Specifies the new position of the top of the window, in client coordinates.</param>
        /// <param name="cx">Specifies the new width of the window, in pixels.</param>
        /// <param name="cy">Specifies the new height of the window, in pixels.</param>
        /// <param name="flags">
        /// Specifies the window sizing and positioning flags. 
        /// This parameter can be a combination of the following values:
        /// SWP_ASYNCWINDOWPOS,SWP_DEFERERASE,SWP_DRAWFRAME,SWP_FRAMECHANGED,
        /// SWP_HIDEWINDOW,SWP_NOACTIVATE,SWP_NOCOPYBITS,SWP_NOMOVE,
        /// SWP_NOOWNERZORDER,SWP_NOREDRAW,SWP_NOREPOSITION,SWP_NOSENDCHANGING,
        /// SWP_NOSIZE,SWP_NOZORDER,SWP_SHOWWINDOW
        /// </param>
        /// <returns></returns>
        [DllImport("user32")]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, int flags);

        //[DllImport("user32")]
        //static extern bool GetClientRect(IntPtr hWnd, ref RECT rect);

        #endregion

        #region Constants

        //about WS_XXX, ref： http://msdn.microsoft.com/en-us/library/czada357(VS.80).aspx
        const int WS_POPUP = unchecked((int)0x80000000);
        const int WS_OVERLAPPED = 0x0;
        const int WS_CHILD = 0x40000000;
        const int WS_MINIMIZE = 0x20000000;
        const int WS_VISIBLE = 0x10000000;
        const int WS_DISABLED = 0x8000000;
        const int WS_CLIPSIBLINGS = 0x4000000;
        const int WS_CLIPCHILDREN = 0x2000000;
        const int WS_MAXIMIZE = 0x1000000;
        /// <summary>
        ///  Creates a window that has a title bar (implies the WS_BORDER style)
        /// </summary>
        const int WS_CAPTION = 0xC00000;                  //  WS_BORDER | WS_DLGFRAME
        const int WS_BORDER = 0x800000;
        const int WS_DLGFRAME = 0x400000;
        const int WS_VSCROLL = 0x200000;
        const int WS_HSCROLL = 0x100000;
        const int WS_SYSMENU = 0x80000;
        const int WS_THICKFRAME = 0x40000;
        const int WS_GROUP = 0x20000;
        const int WS_TABSTOP = 0x10000;

        const int WS_MINIMIZEBOX = 0x20000;
        const int WS_MAXIMIZEBOX = 0x10000;

        const int WS_TILED = WS_OVERLAPPED;
        const int WS_ICONIC = WS_MINIMIZE;
        const int WS_SIZEBOX = WS_THICKFRAME;
        const int WS_OVERLAPPEDWINDOW = (WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX);
        const int WS_TILEDWINDOW = WS_OVERLAPPEDWINDOW;


        const int GWL_STYLE = (-16);

        const int SW_HIDE = 0;
        const int SW_SHOWNORMAL = 1;
        const int SW_NORMAL = 1;
        const int SW_SHOWMINIMIZED = 2;
        const int SW_SHOWMAXIMIZED = 3;
        const int SW_MAXIMIZE = 3;
        const int SW_SHOWNOACTIVATE = 4;
        const int SW_SHOW = 5;
        const int SW_MINIMIZE = 6;
        const int SW_SHOWMINNOACTIVE = 7;
        const int SW_SHOWNA = 8;
        const int SW_RESTORE = 9;
        const int SW_SHOWDEFAULT = 10;
        const int SW_MAX = 10;

        const int EMPTY = 32;
        const int CONSOLE_TEXTMODE_BUFFER = 1;

        /// <summary>
        /// Stop flashing. The system restores the window to its original state.
        /// </summary>
        const int FLASHW_STOP = 0;
        /// <summary>
        /// Flash the window caption.
        /// </summary>
        const int FLASHW_CAPTION = 1;
        /// <summary>
        /// Flash the taskbar button.
        /// </summary>
        const int FLASHW_TRAY = 2;
        /// <summary>
        /// Flash both the window caption and taskbar button. 
        /// This is equivalent to setting the FLASHW_CAPTION | FLASHW_TRAY flags.
        /// </summary>
        const int FLASHW_ALL = 3;
        /// <summary>
        /// Flash continuously, until the FLASHW_STOP flag is set.
        /// </summary>
        const int FLASHW_TIMER = 4;
        /// <summary>
        /// Flash continuously until the window comes to the foreground.
        /// </summary>
        const int FLASHW_TIMERNOFG = 0xc;

        const int DefaultConsoleBufferSize = 256;

        const int FOREGROUND_BLUE = 0x1;
        const int FOREGROUND_GREEN = 0x2;
        const int FOREGROUND_RED = 0x4;
        const int FOREGROUND_INTENSITY = 0x8;
        const int BACKGROUND_BLUE = 0x10;
        const int BACKGROUND_GREEN = 0x20;
        const int BACKGROUND_RED = 0x40;
        const int BACKGROUND_INTENSITY = 0x80;
        const int COMMON_LVB_REVERSE_VIDEO = 0x4000;
        const int COMMON_LVB_UNDERSCORE = 0x8000;

        /// <summary>
        /// Requests read access to the console screen buffer, 
        /// enabling the process to read data from the buffer.
        /// </summary>
        const int GENERIC_READ = unchecked((int)0x80000000);
        /// <summary>
        /// Requests write access to the console screen buffer,
        /// enabling the process to write data to the buffer.
        /// </summary>
        const int GENERIC_WRITE = 0x40000000;

        /// <summary>
        /// Other open operations can be performed on the console screen buffer for read access.
        /// </summary>
        const int FILE_SHARE_READ = 0x1;
        /// <summary>
        /// Other open operations can be performed on the console screen buffer for write access.
        /// </summary>
        const int FILE_SHARE_WRITE = 0x2;

        /// <summary>
        /// The standard input device. Initially, this is the console input buffer
        /// </summary>
        const int STD_INPUT_HANDLE = -10;

        /// <summary>
        /// The standard output device. Initially, this is the active console screen buffer
        /// </summary>
        const int STD_OUTPUT_HANDLE = -11;

        /// <summary>
        /// The standard error device. Initially, this is the active console screen buffer
        /// </summary>
        const int STD_ERROR_HANDLE = -12;

        /// <summary>
        /// Retains the current size (ignores the cx and cy parameters).
        /// </summary>
        const int SWP_NOSIZE = 0x1;
        /// <summary>
        /// Retains the current position (ignores X and Y parameters).
        /// </summary>
        const int SWP_NOMOVE = 0x2;
        /// <summary>
        /// Retains the current Z order (ignores the hWndInsertAfter parameter).
        /// </summary>
        const int SWP_NOZORDER = 0x4;
        /// <summary>
        /// Does not redraw changes. 
        /// If this flag is set, no repainting of any kind occurs. 
        /// This applies to the client area, 
        /// the nonclient area (including the title bar and scroll bars), 
        /// and any part of the parent window uncovered as a result of the window being moved. 
        /// When this flag is set, 
        /// the application must explicitly invalidate or redraw any parts of the window and parent window that need redrawing.
        /// </summary>
        const int SWP_NOREDRAW = 0x8;
        /// <summary>
        /// Does not activate the window. 
        /// If this flag is not set, the window is activated and moved to the top of either the topmost or non-topmost group 
        /// (depending on the setting of the hWndInsertAfter parameter).
        /// </summary>
        const int SWP_NOACTIVATE = 0x10;

        #endregion
    }
}
