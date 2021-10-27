using System;

namespace MonitorThing.Core
{
    public class WindowsApiException : Exception
    {
        public int ErrorCode { get; private set; }

        public WindowsApiException() : base()
        {
        }

        public WindowsApiException(int errorCode) : base()
        {
            ErrorCode = errorCode;
        }

        public WindowsApiException(string message) : base (message)
        {
        }

        public WindowsApiException(int errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
