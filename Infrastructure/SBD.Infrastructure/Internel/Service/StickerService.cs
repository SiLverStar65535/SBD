using SBD.Infrastructure.Internel.Interface;

namespace SBD.Infrastructure.Internel.Service
{
    public class StickerService(IWMIService wmiService) : IStickerService
    {
        public string DeviceID { get; } = Config.StickerID;

        public bool IsConnected()
        {
            return wmiService.QueryDevice<PrinterQuery>(DeviceID) != null;
        }
        public object GetDeviceInformation()
        {
            return wmiService.QueryDevice<PrinterQuery>(DeviceID);
        }
    }
}