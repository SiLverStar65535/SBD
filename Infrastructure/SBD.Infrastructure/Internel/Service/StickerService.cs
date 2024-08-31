using SBD.Infrastructure.Internel.Interface;

namespace SBD.Infrastructure.Internel.Service
{
    public class StickerService(IWMIService wmiService) : IStickerService
    {
        public string DeviceID { get; } = "StickerID";

        public bool IsConnected()
        {
            var devie = wmiService.QueryDevice<PrinterQuery>(DeviceID);
            return devie != null;
        }
        public object GetDeviceInformation()
        {
            return string.Empty;
        }
    }
}