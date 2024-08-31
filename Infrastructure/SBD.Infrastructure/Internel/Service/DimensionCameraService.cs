using System.Threading.Tasks;
using SBD.Infrastructure.Internel.Interface;

namespace SBD.Infrastructure.Internel.Service
{
    public class DimensionCameraService(IWMIService wmiService) : IDimensionCameraService
    {
        public string DeviceID { get; } = "DimensionCameraID";

        public bool IsConnected()
        {
            var devie = wmiService.QueryDevice<SerialPortQuery>(DeviceID);
            return devie != null;
        }
        public object GetDeviceInformation()
        {
            return string.Empty;
        }
        public async Task<string> GetSize()
        {   
            await Task.Delay(5000);
            return string.Empty;
        }

        public async Task<int?> GetWieght()
        {
            await Task.Delay(1000);
            return 18;
        }
    }
}
