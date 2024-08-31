using SBD.Infrastructure.Internel.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SBD.Infrastructure.Internel.Interface
{
    public interface IWMIService
    {
        IDictionary<string,object> QueryWMI(string query);
        Task<IDictionary<string, object>> QueryWMIAsync(string query);
        IDictionary<string, object> QueryDevices<T>() where T : WMIQuery, new();
        Task<IDictionary<string, object>> QueryDevicesAsync<T>( ) where T : WMIQuery, new();
        object QueryDevice<T>(string deviceID) where T : WMIQuery, new();
        Task<object> QueryDeviceAsync<T>(string deviceID) where T : WMIQuery, new();
    }
}
