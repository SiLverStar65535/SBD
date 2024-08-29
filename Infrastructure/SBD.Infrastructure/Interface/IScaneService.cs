using System.Threading.Tasks;
using SBD.Domain.Models;

namespace SBD.Infrastructure.Interface
{

    public interface IScaneService
    {
        Task<LuggageSize> GetSize();
        Task<int?> GetWieght();
    }
}
