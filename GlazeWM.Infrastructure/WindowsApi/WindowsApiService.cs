using System.Runtime.InteropServices;
using System.Text;
using GlazeWM.Infrastructure.WindowsApi.Enums;

namespace GlazeWM.Infrastructure.WindowsApi;

public static partial class WindowsApiService
{
  [Flags]
  public enum SetWindowPosFlags : uint
  {
    NoSize = 0x0001,
    NoMove = 0x0002,
    NoZOrder = 0x0004,
    NoRedraw = 0x0008,
    NoActivate = 0x0010,
    FrameChanged = 0x0020,
    ShowWindow = 0x0040,
    HideWindow = 0x0080,
    NoCopyBits = 0x0100,
    NoOwnerZOrder = 0x0200,
    NoSendChanging = 0x0400,
    DeferErase = 0x2000,
    AsyncWindowPos = 0x4000
  }

  /// <summary>
  /// Flags that can be passed as `hWndInsertAfter` to `SetWindowPos`.
  /// </summary>
  public enum ZOrderFlags
  {
    /// <summary>
    /// Places the window above all non-topmost windows (that is, behind all topmost
    /// windows). This flag has no effect if the window is already a non-topmost window.
    /// </summary>
    NoTopMost = -2,
    /// <summary>
    /// Places the window above all non-topmost windows. The window maintains its
    /// topmost position even when it is deactivated.
    /// </summary>
    TopMost = -1,
    /// <summary>
    /// Places the window at the top of the Z order.
    /// </summary>
    Top = 0,
    /// <summary>
    /// Places the window at the bottom of the Z order.
    /// </summary>
    Bottom = 1,
  }

  /// <summary>
  /// Window styles
  /// </summary>
  [Flags]
  public enum WindowStyles : uint
  {
    Overlapped = 0x00000000,
    Tiled = Overlapped,
    TabStop = 0x00010000,
    MaximizeBox = 0x00010000,
    Group = 0x00020000,
    MinimizeBox = 0x00020000,
    ThickFrame = 0x00040000,
    SizeBox = ThickFrame,
    SysMenu = 0x00080000,
    HScroll = 0x00100000,
    VScroll = 0x00200000,
    DlgFrame = 0x00400000,
    Border = 0x00800000,
    Capion = Border | DlgFrame,
    TiledWindow = OverlappedWindow,
    OverlappedWindow = Overlapped | Capion | SysMenu | ThickFrame | MinimizeBox | MaximizeBox,
    Maximize = 0x01000000,
    ClipChildren = 0x02000000,
    ClipSiblings = 0x04000000,
    Disabled = 0x08000000,
    Visible = 0x10000000,
    Minimize = 0x20000000,
    Iconic = Minimize,
    Child = 0x40000000,
    ChildWindow = Child,
    Popup = 0x80000000,
    PopupWindow = Popup | Border | SysMenu
  }

  /// <summary>
  /// Extended window styles
  /// </summary>
  [Flags]
  public enum WindowStylesEx : uint
  {
    Left = 0x0000,
    LtrReading = 0x0000,
    RightScrollbar = 0x0000,
    DlgModalFrame = 0x0001,
    NoParentNotify = 0x0004,
    TopMost = 0x0008,
    AcceptFiles = 0x0010,
    Transparent = 0x0020,
    MdiChild = 0x0040,
    ToolWindow = 0x0080,
    WindowEdge = 0x0100,
    PaletteWindow = WindowEdge | ToolWindow | TopMost,
    ClientEdge = 0x0200,
    OverlappedWindow = WindowEdge | ClientEdge,
    ContextHelp = 0x0400,
    Right = 0x1000,
    RtlReading = 0x2000,
    LeftScrollbar = 0x4000,
    ControlParent = 0x10000,
    StaticEdge = 0x20000,
    AppWindow = 0x40000,
    Layered = 0x00080000,
    NoInheritLayout = 0x00100000,
    LayoutRtl = 0x00400000,
    Composited = 0x02000000,
    NoActivate = 0x08000000
  }

  [Flags]
  public enum DwmWindowAttribute : uint
  {
    NcRenderingEnabled = 1,
    NcRenderingPolicy,
    TransitionsForceDisabled,
    AllowNcPaint,
    CaptionButtonBounds,
    NonClientRtlLayout,
    ForceIconicRepresentation,
    Flip3DPolicy,
    ExtendedFrameBounds,
    HasIconicBitmap,
    DisallowPeek,
    ExcludedFromPeek,
    Cloak,
    Cloaked,
    FreezeRepresentation,
    Last
  }

  public const int GWLSTYLE = -16;
  public const int GWLEXSTYLE = -20;

  public enum GW : uint
  {
    Owner = 4,
  }

  public delegate bool EnumWindowsDelegate(IntPtr hWnd, int lParam);

  [LibraryImport("user32.dll", EntryPoint = "EnumWindows", SetLastError = true)]
  [return: MarshalAs(UnmanagedType.Bool)]
  public static partial bool EnumWindows(EnumWindowsDelegate enumCallback, IntPtr lParam);

  [LibraryImport("user32.dll")]
  [return: MarshalAs(UnmanagedType.Bool)]
  public static partial bool IsWindowVisible(IntPtr hWnd);

  [LibraryImport("user32.dll", EntryPoint = "GetWindowLong")]
  private static partial IntPtr GetWindowLongPtr32(IntPtr hWnd, int index);

  [LibraryImport("user32.dll", EntryPoint = "GetWindowLongPtr")]
  private static partial IntPtr GetWindowLongPtr64(IntPtr hWnd, int index);

  public static IntPtr GetWindowLongPtr(IntPtr hWnd, int index)
  {
    return Environment.Is64BitProcess
      ? GetWindowLongPtr64(hWnd, index)
      : GetWindowLongPtr32(hWnd, index);
  }

  [LibraryImport("user32.dll", EntryPoint = "SetWindowLong")]
  private static partial IntPtr SetWindowLongPtr32(IntPtr hWnd, int index, IntPtr newLong);

  [LibraryImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
  private static partial IntPtr SetWindowLongPtr64(IntPtr hWnd, int index, IntPtr newLong);

  public static IntPtr SetWindowLongPtr(IntPtr hWnd, int index, IntPtr newLong)
  {
    return Environment.Is64BitProcess
      ? SetWindowLongPtr64(hWnd, index, newLong)
      : SetWindowLongPtr32(hWnd, index, newLong);
  }

  [LibraryImport("user32.dll", SetLastError = true)]
  [return: MarshalAs(UnmanagedType.Bool)]
  public static partial bool SetWindowPos(
    IntPtr hWnd,
    IntPtr hWndInsertAfter,
    int x,
    int y,
    int cx,
    int cy,
    SetWindowPosFlags uFlags);

  [LibraryImport("user32.dll")]
  public static partial IntPtr BeginDeferWindowPos(int nNumWindows);

  [LibraryImport("user32.dll")]
  public static partial IntPtr DeferWindowPos(
    IntPtr hWinPosInfo,
    IntPtr hWnd,
    [Optional] IntPtr hWndInsertAfter,
    int x,
    int y,
    int cx,
    int cy,
    SetWindowPosFlags uFlags);

  [LibraryImport("user32.dll")]
  [return: MarshalAs(UnmanagedType.Bool)]
  public static partial bool EndDeferWindowPos(IntPtr hWinPosInfo);

  [LibraryImport("user32.dll")]
  public static partial IntPtr GetDesktopWindow();

  [LibraryImport("user32.dll")]
  public static partial IntPtr GetForegroundWindow();

  [LibraryImport("user32.dll")]
  [return: MarshalAs(UnmanagedType.Bool)]
  public static partial bool SetForegroundWindow(IntPtr hWnd);

  [LibraryImport("user32.dll")]
  [return: MarshalAs(UnmanagedType.Bool)]
  public static partial bool MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, [MarshalAs(UnmanagedType.Bool)] bool bRepaint);

  [LibraryImport("user32.dll")]
  [return: MarshalAs(UnmanagedType.Bool)]
  public static partial bool SetFocus(IntPtr hWnd);

  [LibraryImport("user32.dll")]
  public static partial IntPtr WindowFromPoint(Point Point);

  [LibraryImport("user32.dll")]
  public static partial IntPtr GetParent(IntPtr hWnd);

  [LibraryImport("user32.dll")]
  [return: MarshalAs(UnmanagedType.Bool)]
  public static partial bool SetCursorPos(int x, int y);

  /// <summary>
  /// Params that can be passed to `ShowWindow`. Only the subset of flags relevant to
  /// this application are included.
  /// </summary>
  public enum ShowWindowFlags : uint
  {
    Minimize = 2,
    Maximize = 3,
    ShowNoActivate = 8,
    Restore = 9,
    ShowDefault = 10,
  }

  [LibraryImport("user32.dll")]
  [return: MarshalAs(UnmanagedType.Bool)]
  public static partial bool ShowWindowAsync(IntPtr hWnd, ShowWindowFlags flags);

  [LibraryImport("user32.dll", SetLastError = true)]
  public static partial int GetWindowTextLength(IntPtr hWnd);

  [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
  public static extern int GetWindowText(IntPtr hWnd, [Out] StringBuilder lpString, int nMaxCount);

  [LibraryImport("user32.dll", SetLastError = true)]
  public static partial uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

  [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
  public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

  [LibraryImport("user32.dll")]
  [return: MarshalAs(UnmanagedType.Bool)]
  public static partial bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);

  /// <summary>
  /// Contains information about the placement of a window on the screen.
  /// </summary>
  [StructLayout(LayoutKind.Sequential)]
  public struct WindowPlacement
  {
    /// <summary>
    /// The length of the structure, in bytes. Before calling the GetWindowPlacement or SetWindowPlacement functions, set this member to sizeof(WINDOWPLACEMENT).
    /// </summary>
    public int Length;

    /// <summary>
    /// Specifies flags that control the position of the minimized window and the method by which the window is restored.
    /// </summary>
    public int Flags;

    /// <summary>
    /// The current show state of the window.
    /// </summary>
    public ShowWindowFlags ShowCommand;

    /// <summary>
    /// The coordinates of the window's upper-left corner when the window is minimized.
    /// </summary>
    public Point MinPosition;

    /// <summary>
    /// The coordinates of the window's upper-left corner when the window is maximized.
    /// </summary>
    public Point MaxPosition;

    /// <summary>
    /// The window's coordinates when the window is in the restored position.
    /// </summary>
    public Rect NormalPosition;
  }

  [LibraryImport("user32.dll", SetLastError = true)]
  [return: MarshalAs(UnmanagedType.Bool)]
  public static partial bool GetWindowPlacement(IntPtr hWnd, ref WindowPlacement windowPlacement);

  [LibraryImport("user32.dll")]
  public static partial IntPtr GetWindow(IntPtr hWnd, GW uCmd);

  [LibraryImport("dwmapi.dll")]
  public static partial int DwmGetWindowAttribute(IntPtr hwnd, DwmWindowAttribute dwAttribute, [MarshalAs(UnmanagedType.Bool)] out bool pvAttribute, int cbAttribute);

  [LibraryImport("user32.dll")]
  public static partial IntPtr GetShellWindow();

  public delegate IntPtr HookProc(int code, IntPtr wParam, IntPtr lParam);

  public enum HookType
  {
    KeyboardLowLevel = 13,
    MouseLowLevel = 14
  }

  /// <summary>
  /// Contains information about a low-level keyboard input event.
  /// </summary>
  [StructLayout(LayoutKind.Sequential)]
  public struct LowLevelKeyboardInputEvent
  {
    /// <summary>
    /// A virtual-key code. The code must be a value in the range 1 to 254.
    /// </summary>
    public int VirtualCode;

    /// <summary>
    /// The `VirtualCode` converted to `Keys` for better usability.
    /// </summary>
    public readonly Keys Key => (Keys)VirtualCode;

    /// <summary>
    /// A hardware scan code for the key.
    /// </summary>
    public int HardwareScanCode;

    /// <summary>
    /// The extended-key flag, event-injected Flags, context code, and transition-state flag. This member is specified as follows. An application can use the following values to test the keystroke Flags. Testing LLKHF_INJECTED (bit 4) will tell you whether the event was injected. If it was, then testing LLKHF_LOWER_IL_INJECTED (bit 1) will tell you whether or not the event was injected from a process running at lower integrity level.
    /// </summary>
    public int Flags;

    /// <summary>
    /// The time stamp for this message, equivalent to what GetMessageTime would return for this message.
    /// </summary>
    public int TimeStamp;

    /// <summary>
    /// Additional information associated with the message.
    /// </summary>
    public IntPtr AdditionalInformation;
  }

  [LibraryImport("user32.dll")]
  public static partial IntPtr SetWindowsHookEx(HookType hookType, [MarshalAs(UnmanagedType.FunctionPtr)] HookProc lpfn, IntPtr hMod, int dwThreadId);

  [LibraryImport("user32.dll")]
  public static partial IntPtr CallNextHookEx([Optional] IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

  [LibraryImport("user32.dll")]
  [return: MarshalAs(UnmanagedType.Bool)]
  public static partial bool UnhookWindowsHookEx(IntPtr hhk);

  [LibraryImport("user32.dll")]
  public static partial int GetKeyboardState(byte[] pbKeyState);

  [LibraryImport("user32.dll")]
  public static partial short GetKeyState(Keys nVirtKey);

  [LibraryImport("user32.dll", EntryPoint = "keybd_event")]
  public static partial void KeybdEvent(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

  [LibraryImport("User32.dll")]
  public static partial short GetAsyncKeyState(Keys key);

  public delegate void WindowEventProc(IntPtr hWinEventHook, EventConstant eventType, IntPtr hwnd, ObjectIdentifier idObject, int idChild, uint dwEventThread, uint dwmsEventTime);

  [LibraryImport("user32.dll")]
  public static partial IntPtr SetWinEventHook(EventConstant eventMin, EventConstant eventMax, IntPtr hmodWinEventProc, WindowEventProc lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);

  /// <summary>
  /// Message types that can be passed to `SendMessage`. Only the subset of types relevant to
  /// this application are included.
  /// </summary>
  public enum SendMessageType : uint
  {
    Close = 0x0010,
  }

  [LibraryImport("user32.dll")]
  public static partial IntPtr SendNotifyMessage(IntPtr hWnd, SendMessageType msg, IntPtr wParam, IntPtr lParam);

  [LibraryImport("user32.dll", SetLastError = true)]
  [return: MarshalAs(UnmanagedType.Bool)]
  public static partial bool IsProcessDPIAware();

  public enum DpiAwarenessContext
  {
    UnawareGdiScaled = -5,
    PerMonitorAwareV2 = -4,
    PerMonitorAware = -3,
    SystemAware = -2,
    Unaware = -1,
    Undefined = 0
  }

  [LibraryImport("user32.dll", SetLastError = true)]
  public static partial int SetProcessDpiAwarenessContext(DpiAwarenessContext value);

  [LibraryImport("user32.dll", SetLastError = true)]
  public static partial uint GetDpiForWindow(IntPtr hWnd);

  [LibraryImport("user32.dll", SetLastError = true)]
  [return: MarshalAs(UnmanagedType.Bool)]
  public static partial bool AdjustWindowRectEx(ref Rect lpRect, WindowStyles dwStyle, [MarshalAs(UnmanagedType.Bool)] bool bMenu, WindowStylesEx dwExStyle);

  [LibraryImport("user32.dll", SetLastError = true)]
  [return: MarshalAs(UnmanagedType.Bool)]
  public static partial bool AdjustWindowRect(ref Rect lpRect, WindowStyles dwStyle, [MarshalAs(UnmanagedType.Bool)] bool bMenu);

  [LibraryImport("user32.dll", SetLastError = true)]
  [return: MarshalAs(UnmanagedType.Bool)]
  public static partial bool AdjustWindowRectExForDpi(ref Rect lpRect, WindowStyles dwStyle, [MarshalAs(UnmanagedType.Bool)] bool bMenu, WindowStylesEx dwExStyle, uint dpi);

  public enum DpiType
  {
    Effective = 0,
    Angular = 1,
    Raw = 2,
  }

  [LibraryImport("Shcore.dll")]
  public static partial IntPtr GetDpiForMonitor(IntPtr hmonitor, DpiType dpiType, out uint dpiX, out uint dpiY);

  public enum MonitorFromPointFlags : uint
  {
    DefaultToNearest = 2,
  }

  [LibraryImport("User32.dll")]
  public static partial IntPtr MonitorFromPoint(Point pt, MonitorFromPointFlags dwFlags);

  [LibraryImport("kernel32.dll")]
  [return: MarshalAs(UnmanagedType.Bool)]
  public static partial bool GetSystemPowerStatus(out SystemPowerStatus lpSystemPowerStatus);

  [StructLayout(LayoutKind.Sequential)]
  public struct SystemPowerStatus
  {
    public byte ACLineStatus;
    public byte BatteryFlag;
    public byte BatteryLifePercent;
    public byte SystemStatusFlag;
    public uint BatteryLifeTime;
    public uint BatteryFullLifeTime;
  }

  [LibraryImport("dwmapi.dll")]
  public static partial int DwmSetWindowAttribute(IntPtr handle, uint attribute, ref uint value, uint size);

  [StructLayout(LayoutKind.Sequential)]
  public struct AnimationInfo
  {
    public uint CallbackSize;
    public int MinAnimate;

    public bool IsEnabled
    {
      readonly get => MinAnimate != 0;
      set => MinAnimate = value ? 1 : 0;
    }

    public static AnimationInfo Create(bool isEnabled)
    {
      return new()
      {
        IsEnabled = isEnabled,
        CallbackSize = (uint)Marshal.SizeOf(typeof(AnimationInfo))
      };
    }
  }

  public enum SystemParametersInfoFlags : uint
  {
    GetAnimation = 72,
    SetAnimation = 73,
  }

  [LibraryImport("User32.dll", SetLastError = true)]
  [return: MarshalAs(UnmanagedType.Bool)]
  public static partial bool SystemParametersInfo(SystemParametersInfoFlags uiAction, uint uiParam, ref AnimationInfo pvParam, uint fWinIni);

  [LibraryImport("kernel32.dll")]
  [return: MarshalAs(UnmanagedType.Bool)]
  internal static partial bool AttachConsole(uint dwProcessId);

  public static bool AttachConsoleToParentProcess()
  {
    const uint AttachParentProcess = 0x0ffffffff;
    var result = AttachConsole(AttachParentProcess);

    var streamWriter = new StreamWriter(Console.OpenStandardOutput())
    {
      AutoFlush = true
    };
    Console.SetOut(streamWriter);
    Console.SetError(streamWriter);

    return result;
  }
}
