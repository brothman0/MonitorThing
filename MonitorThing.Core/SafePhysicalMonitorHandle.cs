using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace MonitorThing.Core
{
    internal class SafePhysicalMonitorHandle : SafeHandle
    {
        public override bool IsInvalid => false;

        internal SafePhysicalMonitorHandle(IntPtr handle) : base(IntPtr.Zero, true)
        {
            this.handle = handle;
        }

        protected override bool ReleaseHandle()
        {
            if (!DestroyPhysicalMonitor(handle))
            {
                WindowsApiErrorHandler.HandleError();
            }
            return true;
        }

        #region Windows API
        [DllImport("Dxva2.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool DestroyPhysicalMonitor(IntPtr monitorHandle);
        #endregion
    }
}
