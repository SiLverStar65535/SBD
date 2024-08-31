using System.Collections.Generic;
using System.Threading.Tasks;
using SBD.Infrastructure.Internel.Interface;
using Thermal_Printer;

namespace SBD.Infrastructure.Internel.Service
{
    public class PrinterService : IPrinterService
    {
        public string GetDeviceInfo()
        {
            return string.Empty;
        }
        public async Task<bool?> PrintListString(List<string> InputTex)
        {
            await Task.Run(() => { Post.CallByCommandCode(null, InputTex); });
            return true;
        }

        public string GetPrinterInfo()
        {
            return string.Empty;
        }
    }
}
