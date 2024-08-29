using System.Threading.Tasks;
using SBD.Domain.Models;
using SBD.Infrastructure.Interface;

namespace SBD.Infrastructure.Service
{
    public class ScaneService : IScaneService
    {


        //模擬取得尺寸
        private LuggageSize CustomLuggageSize { get; set; } 
        //模擬取得重量
        private int? CustomLuggageWeight { get; set; }
        


        public ScaneService()
        {
            switch (true)
            {
                case true:
                    CustomLuggageSize = new LuggageSize
                    {
                        Length = 33,
                        Width = 45,
                        Height = 65
                    };
                    
                    CustomLuggageWeight = 18;
                    break;
                case false:
                    CustomLuggageSize = new LuggageSize
                    {
                        Length = 53,
                        Width = 55,
                        Height = 65
                    };
       
                    CustomLuggageWeight = 28;
                    break;
            }
        }



        public async Task<LuggageSize> GetSize()
        {
            
            
            await Task.Delay(5000);
            return CustomLuggageSize;
        }

        public async Task<int?> GetWieght()
        {
           
           
            await Task.Delay(1000);
            return CustomLuggageWeight;
        }
    }
}
