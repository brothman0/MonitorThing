using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using static MonitorThing.Core.WindowsApiUtility;

namespace MonitorThing.Core
{
    internal static class WindowsApiCalls
    {
        [DllImport("User32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool EnumDisplayMonitors(IntPtr monitorHandle, IntPtr pointer, EnumDisplayMonitorsCallBack callback, IntPtr appData);

        [return: MarshalAs(UnmanagedType.Bool)]
        internal delegate bool EnumDisplayMonitorsCallBack(IntPtr monitorHandle, IntPtr deviceContextHandle, IntPtr monitorRect, IntPtr appData);

        [DllImport("User32.dll", EntryPoint = "GetMonitorInfoW")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetMonitorInfo(IntPtr monitorHandle, ref MonitorInfoEx monitorInfo);
    }
}
