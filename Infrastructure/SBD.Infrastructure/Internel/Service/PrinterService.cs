using SBD.Infrastructure.Internel.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using Thermal_Printer;

namespace SBD.Infrastructure.Internel.Service
{
    public class PrinterService(IWMIService wmiService) : IPrinterService
    {
        public string DeviceID { get; } = "PrintID";
       
        public bool IsConnected()
        {
            var devie = wmiService.QueryDevice<PrinterQuery>(DeviceID);
            return devie != null;
        }
        public object GetDeviceInformation()
        {
            return string.Empty;
        }

        public async Task<bool?> PrintListString(List<string> InputTex)
        {
            await Task.Run(() => { Post.CallByCommandCode(null, InputTex); });
            return true;
        }
    }
}
