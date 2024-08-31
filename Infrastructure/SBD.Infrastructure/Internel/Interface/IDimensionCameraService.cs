using System.Threading.Tasks;

namespace SBD.Infrastructure.Internel.Interface
{
    public interface IDimensionCameraService : IDevice
    {
        Task<string> GetSize();
        Task<int?> GetWieght();
    }
}
