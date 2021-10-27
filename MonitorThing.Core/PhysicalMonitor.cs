using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorThing.Core
{
    internal class PhysicalMonitor
    {
        internal string Description { get; set; }
        internal SafePhysicalMonitorHandle Handle { get; set; }
        internal string CurrentInput { get; set; }
        internal List<string> PossibleInputs { get; set; }

        internal PhysicalMonitor(SafePhysicalMonitorHandle handle, string description)
        {
            Handle = handle;
            Description = description;
        }
    }
}
