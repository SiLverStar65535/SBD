using System.Collections.Generic;
using System.Threading.Tasks;

namespace SBD.Infrastructure.Interface
{
    public interface IStickerService : IDevice
    {
        public Task<bool?> PrintListString(List<string> InputTex);
    }
}