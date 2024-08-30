using SBD.Domain.Interface;
using SBD.Domain.Models;
using SBD.Infrastructure.Interface;
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
        public readonly IScaneService _scaneService;
        public readonly IPrintService _printService;
        public readonly IFileService _fileService;

        public SBDService(IScaneService scaneService, IPrintService printService, IFileService fileService)
        {
            _scaneService = scaneService;
            _printService = printService;
            _fileService = fileService;
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
        public BoardingPass CreateFakeBoardingPassData()
        {
            var BoardingPass = new BoardingPass();
            BoardingPass.DepartureAirportENG = "TSA";
            BoardingPass.ArrivalAirportENG = "MZG";
            BoardingPass.FlightNumber = "AE0381";
            BoardingPass.SeatNumber = "26A";
            BoardingPass.TicketNumber = "016";
            BoardingPass.PassengerName = "假資料FromDesignTime";
            BoardingPass.DepartureAirport = DataList.AirportNameDictionary[BoardingPass.DepartureAirportENG];
            BoardingPass.ArrivalAirport = DataList.AirportNameDictionary[BoardingPass.ArrivalAirportENG];
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
        public async Task<int?> GetAirlineLuggageSize(string Airline)
        {
            //模擬取得航空公司尺寸限制
            var result = new int?(158);

            await Task.Delay(3000);

            return result;
        }
        public async Task<int?> GetAirlineLuggageWeight(string Airline)
        {
            //模擬取得航空公司重量限制
            var result = new int?(20);
            await Task.Delay(3000);
            return result;
        }
        public async Task<LuggageSize> GetLuggageSize()
        {
            //模擬取得尺寸
            return await _scaneService.GetSize(); ;
        }
        public async Task<int?> GetLuggageWieght()
        {
            //模擬取得重量 
            return await _scaneService.GetWieght();
        }

        public async Task<bool?>  PrintLuggageSticker()
        {
            await Task.Delay(1000);
            _printService.PrintListString(null);
            return true;
        }

        public Task<bool?> PrintReceipt()
        {
            _printService.PrintListString();
            return null;
        }

        public async Task<bool?>  PrintCoupon()
        {
            await Task.Delay(1000);
            _printService.PrintReceipt(null);
            return true;
        }
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