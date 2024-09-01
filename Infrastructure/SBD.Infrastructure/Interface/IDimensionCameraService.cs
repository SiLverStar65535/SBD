using System.Threading.Tasks;
using SBD.Domain.Interface;

namespace SBD.Infrastructure.Interface
{
    public interface IDimensionCameraService : IDevice
    {
        Task<string> GetSize();
        Task<int?> GetWieght();
    }
}
