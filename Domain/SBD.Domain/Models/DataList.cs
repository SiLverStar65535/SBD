using System.Collections.Generic;

namespace SBD.Domain.Models
{
    public static class DataList
    {
        //取得機場中文用
        public static Dictionary<string, string>  AirportNameDictionary = new Dictionary<string, string>()
        {
            { "TPE", "中正"},
            { "CYI", "嘉義"},
            { "CMJ", "七美"},
            { "GNI", "綠島"},
            { "HUN", "花蓮"},
            { "KHH", "高雄"},
            { "KNH", "金門"},
            { "MZG", "澎湖"},
            { "MFK", "馬祖"},
            { "KYD", "蘭嶼"},
            { "PIF", "屏東"},
            { "WOT", "望安"},
            { "SAM", "松山"},
            { "TXG", "台中"},
            { "TTT", "台東"},
            { "TNN", "台南"},
        };

    }
}
