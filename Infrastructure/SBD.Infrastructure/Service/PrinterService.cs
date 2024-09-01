using SBD.Infrastructure.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using Thermal_Printer;

namespace SBD.Infrastructure.Service
{
    public class PrinterService(IWMIService wmiService) : IPrinterService
    {
        public string ID { get; } = Config.PrinterID;

        public bool IsConnected() => GetDeviceInformation() != null;

        public object GetDeviceInformation()
        {
            var result = wmiService.QueryDevice(typeof(WMIQuery.PrinterQuery), ID);
            return !string.IsNullOrEmpty(result.Key) ? result : null;
        }

        public async Task<bool?> PrintListString(List<string> InputTex)
        {
            await Task.Run(() => { Post.CallByCommandCode(null, InputTex); });
            return true;
        }
    }
}
