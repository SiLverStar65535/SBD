using SBD.Infrastructure.Internel.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SBD.Infrastructure.Internel.Service
{
    public class StickerService : IStickerService
    {
        public string GetDeviceInfo()
        {
            return string.Empty;
        }
        public async Task<bool?> PrintListString(List<string> InputTex)
        {
            await Task.Delay(1000);
            return null;
        }
    }
}