using SBD.Infrastructure.Internel.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using Thermal_Printer;

namespace SBD.Infrastructure.Internel.Service
{
    public class PrinterService(IWMIService wmiService) : IPrinterService
    {
        public string DeviceID { get; } = Config.PrinterID;
       
        public bool IsConnected() => wmiService.QueryDevice<PrinterQuery>(DeviceID) != null;

        public object GetDeviceInformation() => wmiService.QueryDevice<PrinterQuery>(DeviceID);

        public async Task<bool?> PrintListString(List<string> InputTex)
        {
            await Task.Run(() => { Post.CallByCommandCode(null, InputTex); });
            return true;
        }
    }
}
