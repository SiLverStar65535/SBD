using System;

namespace SBD.Domain.Models
{
    /// <summary>
    /// 正班時刻表物件
    /// </summary>
    public class Flight
    {
        /// <summary>
        /// 航線
        /// </summary>
        public string Route { get; set; }

        /// <summary>
        /// 公司
        /// </summary>
        public string Airline { get; set; }

        /// <summary>
        /// 班次
        /// </summary>
        public string FlightNumber { get; set; }

        /// <summary>
        /// 離場時間
        /// </summary>
        public DateTime DepartureTime { get; set; }

        /// <summary>
        /// 到場時間
        /// </summary>
        public DateTime ArrivalTime { get; set; }

        /// <summary>
        /// 飛行日期
        /// </summary>
        public string FlightDays { get; set; }

        /// <summary>
        /// 機型
        /// </summary>
        public string AircraftType { get; set; }

        /// <summary>
        /// 備註
        /// </summary>
        public string Remarks { get; set; }
    }
}
