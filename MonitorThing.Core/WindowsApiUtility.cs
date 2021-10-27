using System;
using System.Runtime.InteropServices;

namespace MonitorThing.Core
{
    internal static class WindowsApiUtility
    {
        internal struct Rectangle
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        internal struct MonitorInfoEx
        {
            public uint Size;
            public Rectangle MonitorArea;
            public Rectangle WorkArea;
            public uint Flags;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string MonitorName;
        }

        internal struct PhysicalHMonitor
        {
            public IntPtr Handle;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string Description;
        }

        internal enum VcpCodeType
        {
            None = -1,
            Momentary,
            SetParameter
        }
    }
}
