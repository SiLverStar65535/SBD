using System.Collections.Generic;

namespace SBD.Infrastructure.Interface
{
    internal interface IWMIService
    {
        IEnumerable<Dictionary<string, object>> QueryWMI(string query);
        IEnumerable<Dictionary<string, object>> GetProcessorInfo();
        IEnumerable<Dictionary<string, object>> GetPhysicalMemoryInfo();
        IEnumerable<Dictionary<string, object>> GetDiskDriveInfo();
        IEnumerable<Dictionary<string, object>> GetNetworkAdapterInfo();
        IEnumerable<Dictionary<string, object>> GetPnPDeviceInfo();
    }
}
