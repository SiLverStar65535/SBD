using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SBD.Domain.Interface;
using SBD.Domain.Models;

namespace SBD.Infrastructure
{
    public class DataProvider: IDataProvider
    {
        public Flight GetFlightDetail(string flightNumber)
        {
            var fightList = new List<Flight>()
            {
                new Flight
                {
                    FlightNumber = "AE0381",
                    Carrier = "",
                    Class = null,
                    Gate = null
                }
            };


           var flight = fightList.SingleOrDefault(x => x.FlightNumber == flightNumber);

            return flight;
        }
    }
}
