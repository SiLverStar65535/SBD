using System.Threading.Tasks;
using SBD.Domain.Models;

namespace SBD.Domain.Interface
{
    public interface ISBDService
    {


        /// <summary>
        /// 根據 QR 掃碼器取得的字串，創建登機證物件
        /// </summary>
        /// <param name="scanString">QR 掃碼取得的字串</param>
        /// <returns>登機證(BoardingPass)物件，若格式不符則返回 null</returns>
        BoardingPass CreateBoardingPassData(string scanString);

        ///<summary>
        ///取得航班資訊
        ///</summary>
        ///<param name="flightNumber">航班編號ex:AE0381</param>
        ///<returns>航班資訊(Flight)物件，若格式不符則返回 null</returns>
        Flight GetFlightDetail(string flightNumber);

        ///<summary>
        ///取得航空公司規定的行李尺寸(總和)
        ///</summary>
        ///<param name="airline">航空公司名稱(正班時刻表內顯示的名稱)ex:華信/立榮/德安</param>
        ///<returns>返回航空公司規定的行李尺寸(總和)，若取得異常返回 null</returns>
        Task<int?> GetAirlineLuggageSize(string airline);

        ///<summary>
        ///取得航空公司規定的行李重量
        ///</summary>
        ///<param name="airline">航空公司名稱(正班時刻表內顯示的名稱)ex:華信</param>
        ///<returns>返回航空公司規定的行李尺寸(總和)，若取得異常返回 null</returns>
        Task<int?> GetAirlineLuggageWeight(string airline);

        /// <summary>
        /// 取的指定設備的是否連接
        /// </summary>
        /// <param name="device">欲查詢的設備列舉</param>
        /// <returns>返回是否連接中</returns>
        bool IsDeviceConnected(eDevice device);

        ///<summary>
        ///取得乘客託運行李尺寸
        ///</summary>
        ///<returns>返回乘客託運行李尺寸(長寬高)，若取得異常返回 null</returns>
        Task<LuggageSize> GetPassengerLuggageSize();

        ///<summary>
        ///取得乘客託運行李重量
        ///</summary>
        ///<returns>返回乘客託運行李重量，若取得異常返回 null</returns>
        Task<int?> GetPassengerLuggageWieght();

        ///<summary>
        ///列印行李條貼紙
        ///</summary>
        ///<returns>列印成功回傳true，列印失敗回傳false</returns>
        Task<bool> PrintLuggageSticker(BoardingPass boardingPass, Luggage luggage);

        ///<summary>
        ///列印收據
        ///</summary>
        ///<returns>列印成功回傳true，列印失敗回傳false</returns>
        Task<bool> PrintReceipt();

        ///<summary>
        ///列印優惠券
        ///</summary>
        ///<returns>列印成功回傳true，列印失敗回傳false</returns>
        Task<bool> PrintCoupon();
    }
}