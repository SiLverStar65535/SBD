using SBD.Infrastructure.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SBD.Infrastructure.Service
{
    public class StickerService(IWMIService wmiService) : IStickerService
    {
        public string ID { get; } = Config.StickerID;
        public bool IsConnected() => GetDeviceInformation() != null;
        public object GetDeviceInformation()
        {
            var result = wmiService.QueryDevice(typeof(WMIQuery.PrinterQuery), ID);
            return !string.IsNullOrEmpty(result.Key) ? result : null;
        }
        public async Task<bool?> PrintListString(List<string> InputTex)
        {
            return null;
        }
    }
}