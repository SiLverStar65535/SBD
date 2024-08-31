using System.Threading.Tasks;
using SBD.Infrastructure.Internel.Interface;

namespace SBD.Infrastructure.Internel.Service
{
    public class DimensionCameraService : IDimensionCameraService
    {
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

        public string GetDeviceInfo()
        {
            return string.Empty;
        }
    }
}
