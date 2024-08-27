using SBD.Domain.Models;

namespace SBD.Domain.Interface
{
    public interface IDataProvider
    {

        BoardingPass GetBoardingPassData(string scaneString);
        BoardingPass CreateFakeBoardingPassData();
        Flight GetFlightDetail(string flightNumber);
        LuggageSize GetAirlineLuggageSize(string Airline);
        int GetAirlineLuggageWeight(string Airline);
    }
}

