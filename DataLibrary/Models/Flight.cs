namespace SBD.Domain.Models
{
    public class Flight
    {
        /// <summary>
        /// 航班編號
        /// </summary>
        public string FlightNumber { get; set; }

        /// <summary>
        /// 航空公司
        /// </summary>
        public string Carrier { get; set; }

        /// <summary>
        /// 艙級
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// 登機門
        /// </summary>
        public string Gate { get; set; }
    }
}