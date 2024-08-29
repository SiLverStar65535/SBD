using System.Threading.Tasks;
using SBD.Domain.Models;

namespace SBD.Domain.Interface
{
    public interface ISBDService
    {
        BoardingPass GetBoardingPassData(string scaneString);
        BoardingPass CreateFakeBoardingPassData();
        Flight GetFlightDetail(string flightNumber);
        Task<int?> GetAirlineLuggageSize(string Airline);
        Task<int?> GetAirlineLuggageWeight(string Airline);
        Task<LuggageSize> GetLuggageSize();
        Task<int?> GetLuggageWieght();
    }
}