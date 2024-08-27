using SBD.Domain.Models;

namespace SBD.Domain.Interface
{
    public interface IDataProvider
    {
        Flight GetFlightDetail(string flightNumber);
    }
}

