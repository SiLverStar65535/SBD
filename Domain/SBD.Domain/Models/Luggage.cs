namespace SBD.Domain.Models
{





    /// <summary>
    /// 代表乘客的行李詳情。
    /// </summary>
    public class Luggage
    {
        /// <summary>
        /// 登機證資訊
        /// </summary>
        public BoardingPassData boardingPassData { get; set; }
        /// <summary>
        /// 獲取或設置行李的重量，單位為公斤。
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// 獲取或設置行李的長度，單位為厘米。
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// 獲取或設置行李的寬度，單位為厘米。
        /// </summary>
        public int Width { get; set; }

        /// <text>
        /// 獲取或設置行李的高度，單位為厘米。
        /// </text>
        public int Height { get; set; }

        /// <summary>
        /// 獲取或設置行李的類型，例如「隨身攜帶」或「托運」。
        /// </summary>
        public string LuggageType { get; set; }

        /// <summary>
        /// 獲取或設置用於追踪和識別行李的標籤號碼。
        /// </summary>
        public string TagNumber { get; set; }

        /// <summary>
        /// 英文航班標籤號碼。
        /// </summary>
        public string FlightTagNumber { get; set; }

    }
}

