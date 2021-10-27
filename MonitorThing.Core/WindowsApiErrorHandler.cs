using System;
using System.Text;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace MonitorThing.Core
{
    internal static class WindowsApiErrorHandler
    {
        private const uint _formatOption = 0x00001000;
        private const uint _languageId = 0x0409;

        internal static void HandleError()
        {
            int errorCode = Marshal.GetLastWin32Error();
            StringBuilder buffer = new StringBuilder(512);
            if (TryRetrieveErrorMessage(errorCode, buffer))
            {
                throw new WindowsApiException(errorCode, buffer.ToString());
            }
            throw new WindowsApiException(errorCode);
        }

        [DllImport("Kernel32.dll", SetLastError = true)]
        private static extern uint FormatMessage(uint formatOption, IntPtr sourceLocation, uint messageId, uint languageId, StringBuilder buffer, int size, IntPtr arguments);
        private static bool TryRetrieveErrorMessage(int errorCode, StringBuilder buffer)
        {
            return FormatMessage(_formatOption, IntPtr.Zero, (uint)errorCode, _languageId, buffer, buffer.Capacity, IntPtr.Zero) > 0;
        }
    }
}
