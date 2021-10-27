using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using static MonitorThing.Core.WindowsApiUtility;

namespace MonitorThing.Core
{
    public class DisplayMonitorAgent
    {
        private List<DisplayMonitor> _displayMonitorCollection = new List<DisplayMonitor>();
        
        public DisplayMonitorAgent()
        {
            GetDisplayMonitorCollection();
            GetPhysicalMonitorCollection();
        }

        #region GetDisplayMonitorCollection()
        private void GetDisplayMonitorCollection()
        {
            if(!EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, GetDisplayMonitor, IntPtr.Zero))
            {
                throw new WindowsApiException("Unable to retrieve display monitor collection!");
            }
        }

        private bool GetDisplayMonitor(IntPtr monitorHandle, IntPtr deviceContextHandle, IntPtr monitorRect, IntPtr appData)
        {
            MonitorInfoEx monitorInfoEx = new MonitorInfoEx { Size = (uint)Marshal.SizeOf<MonitorInfoEx>() };
            if (GetMonitorInfo(monitorHandle, ref monitorInfoEx))
            {
                DisplayMonitor displayMonitor = new DisplayMonitor(monitorHandle, monitorInfoEx);
                _displayMonitorCollection.Add(displayMonitor);
            }
            return true;
        }
        #endregion

        private void GetPhysicalMonitorCollection()
        {
            foreach(var displayMonitor in _displayMonitorCollection)
            {
                PhysicalMonitorAgent.GetPhysicalMonitorCollection(displayMonitor);
            }
        }

        #region Windows API
        [DllImport("User32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool EnumDisplayMonitors(IntPtr monitorHandle, IntPtr pointer, EnumDisplayMonitorsCallBack callback, IntPtr appData);

        [return: MarshalAs(UnmanagedType.Bool)]
        internal delegate bool EnumDisplayMonitorsCallBack(IntPtr monitorHandle, IntPtr deviceContextHandle, IntPtr monitorRect, IntPtr appData);

        [DllImport("User32.dll", EntryPoint = "GetMonitorInfoW")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetMonitorInfo(IntPtr monitorHandle, ref MonitorInfoEx monitorInfo);
        #endregion
    }
}
