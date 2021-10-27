using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using static MonitorThing.Core.WindowsApiUtility;

namespace MonitorThing.Core
{
    internal static class PhysicalMonitorAgent
    {
        internal static void GetPhysicalMonitorCollection(DisplayMonitor displayMonitor)
        {
            var physicalMonitorCount = GetPhysicalHMonitorCount(displayMonitor.Handle);
            var physicalHMonitors = GetPhysicalHMonitors(displayMonitor.Handle, physicalMonitorCount);

            foreach(var physicalHMonitor in physicalHMonitors)
            {
                var handle = new SafePhysicalMonitorHandle(displayMonitor.Handle);
                var physicalMonitor = new PhysicalMonitor(handle, physicalHMonitor.Description);
                displayMonitor.PhysicalMonitorCollection.Add(physicalMonitor);
            }
        }

        private static PhysicalHMonitor[] GetPhysicalHMonitors(IntPtr displayMonitorHandle, uint physicalMonitorCount)
        {
            var physicalHMonitors = new PhysicalHMonitor[physicalMonitorCount];
            
            if (!GetPhysicalMonitorsFromHMONITOR(displayMonitorHandle, physicalMonitorCount, physicalHMonitors))
            {
                WindowsApiErrorHandler.HandleError();
            }

            return physicalHMonitors;
        }

        private static uint GetPhysicalHMonitorCount(IntPtr displayMonitorHandle)
        {
            if (!GetNumberOfPhysicalMonitorsFromHMONITOR(displayMonitorHandle, out uint physicalMonitorCount))
            {
                WindowsApiErrorHandler.HandleError();
            }
            return physicalMonitorCount;
        }

        #region Windows API
        [DllImport("Dxva2.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetNumberOfPhysicalMonitorsFromHMONITOR(IntPtr displayMonitorHandle, out uint monitorCount);

        [DllImport("Dxva2.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetPhysicalMonitorsFromHMONITOR(IntPtr displayMonitorHandle, uint monitorCount, [Out] PhysicalHMonitor[] physicalMonitors);
        #endregion
    }
}
