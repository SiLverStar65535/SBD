using SBD.Infrastructure.Internel.Interface;

namespace SBD.Infrastructure.Internel.Service
{
    public class QRScanerService(IWMIService wmiService) : IQRScanerService
    {
        public string DeviceID { get; } = Config.QRScanerID;

        public bool IsConnected()
        {
            return wmiService.QueryDevice<KeyboardQuery>(DeviceID) != null;
        }
        public object GetDeviceInformation()
        {
            return  wmiService.QueryDevice<KeyboardQuery>(DeviceID);
        }
    }
}