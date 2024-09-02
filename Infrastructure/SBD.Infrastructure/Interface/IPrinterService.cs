using System.Collections.Generic;
using System.Threading.Tasks;

namespace SBD.Infrastructure.Interface
{
    public interface IPrinterService : IDevice
    {
        Task<bool> PrintListString(List<string> InputTex);
    }
}
