using SBD.Domain.Models;
using System;

namespace SBD.Provider
{
    public static class DesignTimeData
    {
        public static BoardingPass BoardingPass { get; set; }
        public static  Flight Flight { get; set; }
        public static LuggageSize LuggageSize { get; set; }
        
        static DesignTimeData()
        {
            if (App.IsDesignTime)
            {
                BoardingPass = GenerateBoardingPass(); Flight = GenerateFlight(); LuggageSize= GenerateLuggageSize();
            }
        }

        private static LuggageSize GenerateLuggageSize()
        {
            return new LuggageSize
            {
                Length = 33,
                Width = 45,
                Height = 65
            };
        }

        private static Flight GenerateFlight()
        {  
            return new Flight
            {
                Route = "七美\u2192澎湖",
                Airline = "德安",
                FlightNumber = "DA7016",
                DepartureTime = DateTime.Parse( "14:25"),
                ArrivalTime = DateTime.Parse("14:45"),
                FlightDays = "一二三四五六日",
                AircraftType = "DHC6-400",
                Remarks = ""
            };
        }

        private static BoardingPass GenerateBoardingPass()
        {
          

            return new BoardingPass
            {
                DepartureAirportENG = "TSA",
                DepartureAirport = DataList.AirportNameDictionary["TSA"],
                ArrivalAirportENG = "MZG",
                ArrivalAirport = DataList.AirportNameDictionary["MZG"],
                FlightNumber = "AE0381",
                SeatNumber = "26A",
                TicketNumber = "016",
                PassengerName = "測式FromDesignTime",
            };
        }
    }
}
