using SBD.Infrastructure.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SBD.Infrastructure.Service
{
    public class StickerService(IWMIService wmiService) : IStickerService
    {
        #region IDevice
        public string ID { get; } = Config.StickerID;
        public bool IsConnected() => GetDeviceInformation() != null;
        public object GetDeviceInformation()
        {
            var result = wmiService.QueryDevice(typeof(WMIQuery.PrinterQuery), ID);
            return !string.IsNullOrEmpty(result.Key) ? result : null;
        }
        #endregion

        public async Task<bool?> PrintListString(List<string> InputTex)
        {
            await Task.Delay(1000);
            return null;
        }
    }
}