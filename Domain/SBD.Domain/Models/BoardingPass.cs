namespace SBD.Domain.Models
{
    public class BoardingPass
    {   
        /// <summary>
        /// 出發機場
        /// </summary>
        public string DepartureAirport { get; set; }

        /// <summary>
        /// 出發機場
        /// </summary>
        public string DepartureAirportENG { get; set; }

        /// <summary>
        /// 到達機場
        /// </summary>
        public string ArrivalAirport { get; set; }

        /// <summary>
        /// 到達機場
        /// </summary>
        public string ArrivalAirportENG { get; set; }

        /// <summary>
        /// 航班號
        /// </summary>
        public string FlightNumber { get; set; }

        /// <summary>
        /// 座位號
        /// </summary>
        public string SeatNumber { get; set; }

        /// <summary>
        /// 票號
        /// </summary>
        public string TicketNumber { get; set; }

        /// <summary>
        /// 乘客姓名
        /// </summary>
        public string PassengerName { get; set; }
    }
}