using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary
{
    public class BoardingPassData
    {
        public BoardingPassData()
        {
            if (this.LuggageList == null)
            {
                this.LuggageList = new List<Luggage>();
            }
        }
        /// <summary>
        /// 乘客姓名
        /// </summary>
        public string PassengerName { get; set; }

        /// <summary>
        /// 航班號
        /// </summary>
        public string FlightNumber { get; set; }

        /// <summary>
        /// 座位號
        /// </summary>
        public string SeatNumber { get; set; }

        /// <summary>
        /// 出發機場
        /// </summary>
        public string DepartureAirport { get; set; }

        /// <summary>
        /// 到達機場
        /// </summary>
        public string ArrivalAirport { get; set; }

        /// <summary>
        /// 出發時間
        /// </summary>
        public string DepartureTime { get; set; }

        /// <summary>
        /// 到達時間
        /// </summary>
        public string ArrivalTime { get; set; }

        /// <summary>
        /// 登機門
        /// </summary>
        public string BoardingGate { get; set; }

        /// <summary>
        /// 票號
        /// </summary>
        public string TicketNumber { get; set; }

        ///// <summary>
        ///// 建構函式，用於初始化 BoardingPass 類的實例
        ///// </summary>
        ///// <param name="passengerName">乘客姓名</param>
        ///// <param name="flightNumber">航班號</param>
        ///// <param name="seatNumber">座位號</param>
        ///// <param name="departureAirport">出發機場</param>
        ///// <param name="arrivalAirport">到達機場</param>
        ///// <param name="departureTime">出發時間</param>
        ///// <param name="arrivalTime">到達時間</param>
        ///// <param name="boardingGate">登機門</param>
        ///// <param name="ticketNumber">票號</param>
        //public BoardingPass(string passengerName, string flightNumber, string seatNumber, string departureAirport, string arrivalAirport, DateTime departureTime, DateTime arrivalTime, string boardingGate, string ticketNumber)
        //{
        //    PassengerName = passengerName;
        //    FlightNumber = flightNumber;
        //    SeatNumber = seatNumber;
        //    DepartureAirport = departureAirport;
        //    ArrivalAirport = arrivalAirport;
        //    DepartureTime = departureTime;
        //    ArrivalTime = arrivalTime;
        //    BoardingGate = boardingGate;
        //    TicketNumber = ticketNumber;
        //}

        /// <summary>
        /// 顯示登機牌資訊
        /// </summary>
        public void DisplayBoardingPassInfo()
        {
            Console.WriteLine("----- Boarding Pass -----");
            Console.WriteLine($"乘客姓名: {PassengerName}");
            Console.WriteLine($"航班號: {FlightNumber}");
            Console.WriteLine($"座位號: {SeatNumber}");
            Console.WriteLine($"出發機場: {DepartureAirport}");
            Console.WriteLine($"到達機場: {ArrivalAirport}");
            Console.WriteLine($"出發時間: {DepartureTime}");
            Console.WriteLine($"到達時間: {ArrivalTime}");
            Console.WriteLine($"登機門: {BoardingGate}");
            Console.WriteLine($"票號: {TicketNumber}");
            Console.WriteLine("-------------------------");
        }
        public List<Luggage> LuggageList { get; set; }
    }



    public class BoardingPass
    {   
        /// <summary>
        /// 出發機場
        /// </summary>
        public string DepartureAirport { get; set; }
        
        /// <summary>
        /// 到達機場
        /// </summary>
        public string ArrivalAirport { get; set; }

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