using DataLibrary;

namespace SBD.Provider
{
    public static class DesignTimeData
    {
      
        public static BoardingPass BoardingPass { get; set; }

        static DesignTimeData()
        {
            if (App.IsDesignTime)
            {
                BoardingPass = GenerateBoardingPass();
            }
        }

        private static BoardingPass GenerateBoardingPass()
        {
            return new BoardingPass
            {
                DepartureAirport = "TSA",
                ArrivalAirport = "MZG",
                FlightNumber = "AE0381",
                SeatNumber = "26A",
                TicketNumber = "016",
                PassengerName = "測式FromDesignTime",
            };
        }
    }
}
