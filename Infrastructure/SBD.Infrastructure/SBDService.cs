using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SBD.Domain;
using SBD.Domain.Interface;
using SBD.Domain.Models;
using SBD.Infrastructure.Interface;

namespace SBD.Infrastructure
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

        public BoardingPass CreateBoardingPassData(string scanString)
        {
            try
            {
                var scannedStringList = scanString.Split(',');
                var boardingPassInfo = scannedStringList[0];
                var boardingPassPassengerName = scannedStringList[1];
                var boardingPass = new BoardingPass
                {
                    PassengerName = boardingPassPassengerName,
                    DepartureAirportENG = boardingPassInfo.Substring(31, 3),
                    DepartureAirport = DataList.AirportNameDictionary[boardingPassInfo.Substring(31, 3)],
                    ArrivalAirportENG = boardingPassInfo.Substring(33, 3),
                    ArrivalAirport = DataList.AirportNameDictionary[boardingPassInfo.Substring(33, 3)],
                    FlightNumber = boardingPassInfo.Substring(36, 7),
                    SeatNumber = boardingPassInfo.Substring(49, 3),
                    TicketNumber = boardingPassInfo.Substring(53, 3)
                };
                return boardingPass;
            }
            catch(Exception ex) 
            {
                Console.WriteLine($"Error:CreateBoardingPassData: {ex.Message}");
            }
            return null;
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
        
        public async Task<LuggageSize> GetPassengerLuggageSize()
        { 
            await _dimensionCameraService.GetSize(); 
            return new LuggageSize();
        }
        
        public async Task<int?> GetPassengerLuggageWieght() => await _dimensionCameraService.GetWieght();
        public async Task<bool> PrintLuggageSticker(BoardingPass boardingPass, Luggage luggage)
        {
           return await _printerService.PrintListString(null);
        }
        public async Task<bool> PrintReceipt() => await _printerService.PrintListString(null);
        public async Task<bool> PrintCoupon() => await _printerService.PrintListString(null);
        private List<Flight> GetFlightList(string filePath)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error:GetFlightList: {ex.Message}");
            }
            return null;
        }
    }
}