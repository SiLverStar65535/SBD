using SBD.Infrastructure.Internel.Interface;

namespace SBD.Infrastructure.Internel.Service
{
    public class QRScanerService(IWMIService wmiService) : IQRScanerService
    {
        public string DeviceID { get; } = "QRScanerID";

        public bool IsConnected()
        {
            var devie = wmiService.QueryDevice<KeyboardQuery>(DeviceID);
            return devie != null;
        }
        public object GetDeviceInformation()
        {
            return string.Empty;
        }
    }
}