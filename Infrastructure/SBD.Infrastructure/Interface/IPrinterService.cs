using System.Collections.Generic;
using System.Threading.Tasks;
using SBD.Domain.Interface;

namespace SBD.Infrastructure.Interface
{
    public interface IPrinterService : IDevice
    {
        Task<bool> PrintListString(List<string> InputTex);
    }
}
