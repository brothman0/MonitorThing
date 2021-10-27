using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using static MonitorThing.Core.WindowsApiUtility;
using System.Windows;

namespace MonitorThing.Core
{
    internal class DisplayMonitor
    {
        private MonitorInfoEx _monitorinfoEx;

        internal IntPtr Handle { get; private set; }

        internal int Size => (int)_monitorinfoEx.Size;
        
        internal Rect Area => MapRect(_monitorinfoEx.MonitorArea);

        internal Rect WorkArea => MapRect(_monitorinfoEx.WorkArea);

        internal int Flags => (int)_monitorinfoEx.Flags;

        internal string Name => _monitorinfoEx.MonitorName;

        internal List<PhysicalMonitor> PhysicalMonitorCollection { get; set; } = new List<PhysicalMonitor>();

        internal DisplayMonitor(IntPtr handle, MonitorInfoEx monitorInfoEx)
        {
            Handle = handle;
            _monitorinfoEx = monitorInfoEx;
        }
        
        private Rect MapRect(Rectangle rectangle)
        {
            double x = rectangle.Left;
            double y = rectangle.Top;
            double width = rectangle.Right - rectangle.Left;
            double height = rectangle.Bottom - rectangle.Top;
            return new Rect(x, y, width, height);
        }
    }
}
