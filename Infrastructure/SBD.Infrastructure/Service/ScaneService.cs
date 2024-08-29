using System.Threading.Tasks;
using SBD.Domain.Models;
using SBD.Infrastructure.Interface;

namespace SBD.Infrastructure.Service
{
    public class ScaneService : IScaneService
    {
        public async Task<LuggageSize> GetSize()
        {
            //模擬取得尺寸
            var result = new LuggageSize
            {
                Length = 45,
                Width = 22,
                Height = 68
            };
            await Task.Delay(5000);
            return result;
        }

        public async Task<int?> GetWieght()
        {
            //模擬取得重量
            var result = new int?(2);
            await Task.Delay(1000);
            return result;
        }
    }
}
