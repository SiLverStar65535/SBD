using DataLibrary;

namespace SBD.Provider
{
    public static class DesignTimeData
    {
        public static BoardingPassData BoardingPassData { get; set; }

        static DesignTimeData()
        {
            bool isDesignTime = true;

            if (isDesignTime)
            {
                BoardingPassData = GenerateBoardingPassData();

            }
        }

        private static BoardingPassData GenerateBoardingPassData()
        {
            return new BoardingPassData
            {
                PassengerName = null,
                FlightNumber = null,
                SeatNumber = null,
                DepartureAirport = null,
                ArrivalAirport = null,
                DepartureTime = null,
                ArrivalTime = null,
                BoardingGate = null,
                TicketNumber = null,
                LuggageList = null
            };
        }
    }
}
