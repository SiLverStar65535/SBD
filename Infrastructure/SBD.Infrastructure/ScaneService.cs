using SBD.Domain.Interface;
using SBD.Domain.Models;

namespace SBD.Infrastructure
{
    public class ScaneService : IScaneService
    {
        public LuggageSize GetLuggageSize()
        {
            return new LuggageSize();
        }

        public int GetLuggageWieght()
        {
           return 0;
        }
    }
}
