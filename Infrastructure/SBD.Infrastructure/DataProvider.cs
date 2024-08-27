using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SBD.Domain.Interface;
using SBD.Domain.Models;
using Aspose.Cells;
using System.Data;

namespace SBD.Infrastructure
{
    public class DataProvider: IDataProvider
    {
        public DataProvider()
        {
            var LData = "DQo8TGljZW5zZT4NCjxEYXRhPg0KPExpY2Vuc2VkVG8+VGhlIFdvcmxkIEJhbms8L0xpY2Vuc2VkVG8+DQo8RW1haWxUbz5ra3VtYXIzQHdvcmxkYmFua2dyb3VwLm9yZzwvRW1haWxUbz4NCjxMaWNlbnNlVHlwZT5EZXZlbG9wZXIgU21hbGwgQnVzaW5lc3M8L0xpY2Vuc2VUeXBlPg0KPExpY2Vuc2VOb3RlPjEgRGV2ZWxvcGVyIEFuZCAxIERlcGxveW1lbnQgTG9jYXRpb248L0xpY2Vuc2VOb3RlPg0KPE9yZGVySUQ+MjEwMzE2MTg1OTU3PC9PcmRlcklEPg0KPFVzZXJJRD43NDQ5MTY8L1VzZXJJRD4NCjxPRU0+VGhpcyBpcyBub3QgYSByZWRpc3RyaWJ1dGFibGUgbGljZW5zZTwvT0VNPg0KPFByb2R1Y3RzPg0KPFByb2R1Y3Q+QXNwb3NlLlRvdGFsIGZvciAuTkVUPC9Qcm9kdWN0Pg0KPC9Qcm9kdWN0cz4NCjxFZGl0aW9uVHlwZT5Qcm9mZXNzaW9uYWw8L0VkaXRpb25UeXBlPg0KPFNlcmlhbE51bWJlcj4wM2ZiMTk5YS01YzhhLTQ4ZGItOTkyZS1kMDg0ZmYwNjZkMGM8L1NlcmlhbE51bWJlcj4NCjxTdWJzY3JpcHRpb25FeHBpcnk+MjAyMjA1MTY8L1N1YnNjcmlwdGlvbkV4cGlyeT4NCjxMaWNlbnNlVmVyc2lvbj4zLjA8L0xpY2Vuc2VWZXJzaW9uPg0KPExpY2Vuc2VJbnN0cnVjdGlvbnM+aHR0cHM6Ly9wdXJjaGFzZS5hc3Bvc2UuY29tL3BvbGljaWVzL3VzZS1saWNlbnNlPC9MaWNlbnNlSW5zdHJ1Y3Rpb25zPg0KPC9EYXRhPg0KPFNpZ25hdHVyZT5XbkJYNnJOdHpCclNMV3pBdFlqOEtkdDFLSUI5MlFrL2xEbFNmMlM1TFRIWGdkcS9QQ2NqWHVORmp0NEJuRmZwNFZLc3VsSjhWeFExakIwbmM0R1lWcWZLek14SFFkaXFuZU03NTJaMjlPbmdyVW40Yk0rc1l6WWVSTE9UOEpxbE9RN05rRFU0bUk2Z1VyQ3dxcjdnUVYxbDJJWkJxNXMzTEFHMFRjQ1ZncEE9PC9TaWduYXR1cmU+DQo8L0xpY2Vuc2U+DQo=";
            MemoryStream stream = new MemoryStream(Convert.FromBase64String(LData));
            stream.Seek(0, SeekOrigin.Begin);
            License license = new License();
            license.SetLicense(stream);
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
            var filePath = FindFilePath(directoryPath, fileName);
            var flightDataTable = GetFlightDataTable(filePath);
            var fightList = MapToFlightInfo(flightDataTable);


            //前面2字英文
            var temp0 = flightNumber.Substring(0, 2);
            //沒有前面2字英文的航班號//去前面的0
            var temp1 = int.Parse(flightNumber.Substring(2)).ToString();
            var fixedFlightNumber = temp0 + temp1;



            var flight = fightList.SingleOrDefault(x => x.FlightNumber == fixedFlightNumber);

            return flight;
        }

        public LuggageSize GetAirlineLuggageSize(string Airline)
        {
            return new LuggageSize();
        }

        public int GetAirlineLuggageWeight(string Airline)
        {
            return 0;
        }

        private DataTable GetFlightDataTable(string filePath)
        {
            // 加載Excel文件
            Workbook workbook = new Workbook(filePath);
            Worksheet worksheet = workbook.Worksheets[0];

            // 初始化DataTable
            DataTable dataTable = new DataTable();

            // 添加列到DataTable
            dataTable.Columns.Add("Route", typeof(string));
            dataTable.Columns.Add("Airline", typeof(string));
            dataTable.Columns.Add("FlightNumber", typeof(string));
            dataTable.Columns.Add("DepartureTime", typeof(DateTime));
            dataTable.Columns.Add("ArrivalTime", typeof(DateTime));
            dataTable.Columns.Add("FlightDays", typeof(string));
            dataTable.Columns.Add("AircraftType", typeof(string));
            dataTable.Columns.Add("Remarks", typeof(string));

            // 從Excel中提取資料
            for (int i = 4; i < worksheet.Cells.MaxDataRow + 1; i++)
            {
                DataRow row = dataTable.NewRow();

                row["Route"] = worksheet.Cells[i, 0].StringValue;
                row["Airline"] = worksheet.Cells[i, 1].StringValue;
                row["FlightNumber"] = worksheet.Cells[i, 2].StringValue;
                row["DepartureTime"] = DateTime.Parse(worksheet.Cells[i, 3].StringValue);
                row["ArrivalTime"] = DateTime.Parse(worksheet.Cells[i, 4].StringValue);
                row["FlightDays"] = worksheet.Cells[i, 5].StringValue;
                row["AircraftType"] = worksheet.Cells[i, 6].StringValue;
                row["Remarks"] = worksheet.Cells[i, 7].StringValue;

                dataTable.Rows.Add(row);
            }

            return dataTable;
        }
        private string FindFilePath(string directoryPath, string searchPattern)
        {
            // 搜尋符合條件的檔案
            var files = Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories)
                .Where(file => Path.GetFileName(file).Contains(searchPattern))
                .ToList();
           
            // 如果找到檔案，返回第一個檔案的完整路徑
            return files.Count > 0 ? files[0] : null;
        }
        private List<Flight> MapToFlightInfo(DataTable dataTable)
        {
            List<Flight> flightInfoList = new List<Flight>();

            foreach (DataRow row in dataTable.Rows)
            {
                Flight flightInfo = new Flight
                {
                    Route = row["Route"].ToString(),
                    Airline = row["Airline"].ToString(),
                    FlightNumber = row["FlightNumber"].ToString(),
                    DepartureTime = Convert.ToDateTime(row["DepartureTime"]),
                    ArrivalTime = Convert.ToDateTime(row["ArrivalTime"]),
                    FlightDays = row["FlightDays"].ToString(),
                    AircraftType = row["AircraftType"].ToString(),
                    Remarks = row["Remarks"].ToString()
                };

                flightInfoList.Add(flightInfo);
            }

            return flightInfoList;
        }
    }
}
