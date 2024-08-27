using System;

namespace SBD.Domain.Models
{
    public class Flight
    {
        public string Route { get; set; } // 航線
        public string Airline { get; set; } // 公司
        public string FlightNumber { get; set; } // 班次
        public DateTime DepartureTime { get; set; } // 離場時間
        public DateTime ArrivalTime { get; set; } // 到場時間
        public string FlightDays { get; set; } // 飛行日期
        public string AircraftType { get; set; } // 機型
        public string Remarks { get; set; } // 備註
    }
}
