using SBD.Domain;
using SBD.Domain.Interface;
using SBD.Domain.Models;
using SBD.Infrastructure.Internel.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SBD.Infrastructure.Service
{
    public class SBDService : ISBDService
    {
        private readonly IFileService _fileService;
        private readonly IQRScanerService _qrScanerService;
        private readonly IDimensionCameraService _dimensionCameraService;
        private readonly IPrinterService _printerService;
        private readonly IStickerService _stickerService;

        public SBDService(IFileService fileService,
            IWMIService wmiService,
            IQRScanerService qrScanerService,
            IDimensionCameraService dimensionCameraService,
            IPrinterService printerService,
            IStickerService stickerService)
        {
            _fileService = fileService;
            _qrScanerService = qrScanerService;
            _dimensionCameraService = dimensionCameraService;
            _printerService = printerService;
            _stickerService = stickerService;
        }

        public IDevice GetDevice(eDevice device)
        {
            return device switch
            {
                eDevice.QRScaner => _qrScanerService,
                eDevice.DemensionCamera => _dimensionCameraService,
                eDevice.Printer => _stickerService,
                eDevice.Sticker => _printerService,
                _ => throw new ArgumentOutOfRangeException(nameof(device), device, null)
            };
        }

        public BoardingPass GetBoardingPassData(string scaneString)
        {
            var ScandedStringList = scaneString.Split(',');
            var boardingPassInfo = ScandedStringList[0];
            var boardingPassPassengerName = ScandedStringList[1];
            var BoardingPass = new BoardingPass();
            BoardingPass.PassengerName = boardingPassPassengerName;
            BoardingPass.DepartureAirportENG = boardingPassInfo.Substring(31, 3);
            BoardingPass.DepartureAirport = DataList.AirportNameDictionary[BoardingPass.DepartureAirportENG];
            BoardingPass.ArrivalAirportENG = boardingPassInfo.Substring(33, 3);
            BoardingPass.ArrivalAirport = DataList.AirportNameDictionary[BoardingPass.ArrivalAirportENG];
            BoardingPass.FlightNumber = boardingPassInfo.Substring(36, 7);
            BoardingPass.SeatNumber = boardingPassInfo.Substring(49, 3);
            BoardingPass.TicketNumber = boardingPassInfo.Substring(53, 3);
            return BoardingPass;
        }
        public Flight GetFlightDetail(string flightNumber)
        {
            var directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\Resources");
            var fileName = DateTime.Today.Month + "月正班時刻表";
            var filePath = _fileService.FindFilePath(directoryPath, fileName);
            var fightList = GetFlightList(filePath);
            //前面2字英文
            var temp0 = flightNumber.Substring(0, 2);
            //沒有前面2字英文的航班號//去前面的0
            var temp1 = int.Parse(flightNumber.Substring(2)).ToString();
            var fixedFlightNumber = temp0 + temp1;

            var flight = fightList.SingleOrDefault(x => x.FlightNumber == fixedFlightNumber);

            return flight;
        }
        public async Task<int?> GetAirlineLuggageSize(string airline)
        {
            //模擬取得航空公司尺寸限制
            var result = new int?(158);

            await Task.Delay(3000);

            return result;
        }
        public async Task<int?> GetAirlineLuggageWeight(string airline)
        {
            //模擬取得航空公司重量限制
            var result = new int?(20);
            await Task.Delay(3000);
            return result;
        }
        
        public async Task<LuggageSize> GetLuggageSize()
        { 
            await _dimensionCameraService.GetSize(); 
            return new LuggageSize();
        }
        
        public async Task<int?> GetLuggageWieght() => await _dimensionCameraService.GetWieght();
        public async Task<bool?> PrintLuggageSticker(BoardingPass boardingPass, Luggage luggage)
        {
           return await _printerService.PrintListString(null);
        }
        public async Task<bool?> PrintReceipt() => await _printerService.PrintListString(null);
        public async Task<bool?> PrintCoupon() => await _printerService.PrintListString(null);
        private List<Flight> GetFlightList(string filePath)
        {
            var dataTable = _fileService.GetExcelSheetData(sheetIndex: 0, firstRow: 4, filePath);
            var flightList = new List<Flight>();
            foreach (DataRow row in dataTable.Rows)
            {
                var flightInfo = new Flight();
                flightInfo.Route = row[0].ToString();
                flightInfo.Airline = row[1].ToString();
                flightInfo.FlightNumber = row[2].ToString();
                flightInfo.DepartureTime = Convert.ToDateTime(row[3]);
                flightInfo.ArrivalTime = Convert.ToDateTime(row[4]);
                flightInfo.FlightDays = row[5].ToString();
                flightInfo.AircraftType = row[6].ToString();
                flightInfo.Remarks = row[7].ToString();

                flightList.Add(flightInfo);
            }
            return flightList;
        }
    }
}


//// 使用WMI來搜索系統中的所有設備，篩選出含有COM端口描述的設備
//var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Caption LIKE '%(COM%'");
////var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity ");
//var managmentObjects = searcher.Get();
//// 正則表達式，用於從設備描述中提取COM端口編號
//var comRegex = new Regex(@"\(COM(\d+)\)");

//try
//{
//    var comCount = managmentObjects.Count;
//    // 遍歷搜索結果
//    foreach (var managmentObject in managmentObjects)
//    {
//        // 獲取設備描述信息
//        string caption = managmentObject["Caption"].ToString();

//        // 使用正則表達式匹配COM端口
//        var matchCom = comRegex.Match(caption);

//        if (matchCom.Success)
//        {
//            // 成功匹配後提取COM端口號碼
//            string comPortNumberStr = matchCom.Groups[1].Value;
//            // 嘗試將提取的端口號碼字符串轉換為整數
//            if (int.TryParse(comPortNumberStr, out int comPortNumber))
//            {
//                // 提取設備ID，用於進一步匹配VID和PID
//                string deviceId = managmentObject["DeviceID"].ToString();
//                // 正則表達式，用於從設備ID中提取VID和PID
//                var vidPidRegex = new Regex(@"VID_([0-9A-F]+)&PID_([0-9A-F]+)");
//                var matchVidPid = vidPidRegex.Match(deviceId);

//                var isMatched = matchVidPid.Success;
//                var PosVID = matchVidPid.Groups[1].Value;
//                var PosPID = matchVidPid.Groups[2].Value;

//                // 檢查VID和PID是否與配置文件中設定的相符
//                if (isMatched && PosVID == Config.PosVID && PosPID == Config.PosPID)
//                {
//                    // 組合顯示文字並更新界面上的顯示
//                    string displayText = $"COM{comPortNumber} (VID={Config.PosVID}, PID={Config.PosPID}) detected and saved.";
//                    DeviceString += displayText + "\n";
//                }
//            }
//        }
//    }
//}
//catch (ManagementException ex)
//{
//    // 處理可能的異常，並在異常發生時彈出提示
//    MessageBox.Show("An error occurred while querying for WMI data: " + ex.Message);
//}