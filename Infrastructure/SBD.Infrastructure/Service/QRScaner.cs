using SBD.Infrastructure.Interface;

namespace SBD.Infrastructure.Service
{
    public class QRScanerService(IWMIService wmiService) : IQRScanerService
    {
        #region IDevice
        public string ID { get; } = Config.QRScanerID;
        public bool IsConnected() => GetDeviceInformation() != null;
        public object GetDeviceInformation()
        {
            var result = wmiService.QueryDevice(typeof(WMIQuery.KeyboardQuery), ID);
            return !string.IsNullOrEmpty(result.Key) ? result : null;
        }
        #endregion

    }
}