using System.Threading.Tasks;
using SBD.Domain.Models;

namespace SBD.Domain.Interface
{
    public interface ISBDService
    {
        IDevice GetDevice(eDevice device);

        /// <summary>根據 QR 掃碼器取得的字串，創建登機證物件</summary>
        /// <param name="scanString">QR 掃碼取得的字串</param>
        /// <returns>登機證(BoardingPass)物件，若格式不符則返回 null</returns>
        BoardingPass CreateBoardingPassData(string scanString);

        ///<summary>取得航班資訊</summary>
        ///<param name="flightNumber">航班編號ex:AE0381</param>
        ///<returns>航班資訊(Flight)物件，若格式不符則返回 null</returns>
        Flight GetFlightDetail(string flightNumber);

        ///<summary>取得航空公司規定的行李尺寸</summary>
        Task<int?> GetAirlineLuggageSize(string airline);

        ///<summary>取得航空公司規定的行李重量</summary>
        Task<int?> GetAirlineLuggageWeight(string airline);
        
        ///<summary>取得乘客託運行李尺寸</summary>
        Task<LuggageSize> GetPassengerLuggageSize();
        
        ///<summary>取得乘客託運行李重量</summary>
        Task<int?> GetPassengerLuggageWieght();
        
        ///<summary>列印行李條貼紙</summary>
        Task<bool> PrintLuggageSticker(BoardingPass boardingPass, Luggage luggage);
        
        ///<summary>列印收據</summary>
        Task<bool> PrintReceipt();
        
        ///<summary>列印優惠券</summary>
        Task<bool> PrintCoupon();
    }
}