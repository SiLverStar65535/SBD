using System.Collections.Generic;
using SBD.Infrastructure.Internel.Interface;

namespace SBD.Infrastructure.Internel.Service
{
    public class WMIService : IWMIService
    {
        public IEnumerable<Dictionary<string, object>> QueryWMI(string query)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Dictionary<string, object>> GetProcessorInfo()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Dictionary<string, object>> GetPhysicalMemoryInfo()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Dictionary<string, object>> GetDiskDriveInfo()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Dictionary<string, object>> GetNetworkAdapterInfo()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Dictionary<string, object>> GetPnPDeviceInfo()
        {
            throw new System.NotImplementedException();
        }
    }
}