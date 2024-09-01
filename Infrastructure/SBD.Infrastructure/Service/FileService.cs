using System;
using System.Data;
using System.IO;
using System.Linq;
using Aspose.Cells;
using SBD.Infrastructure.Interface;

namespace SBD.Infrastructure.Service
{
    public class FileService : IFileService
    {
        public FileService()
        {
            var LData = "DQo8TGljZW5zZT4NCjxEYXRhPg0KPExpY2Vuc2VkVG8+VGhlIFdvcmxkIEJhbms8L0xpY2Vuc2VkVG8+DQo8RW1haWxUbz5ra3VtYXIzQHdvcmxkYmFua2dyb3VwLm9yZzwvRW1haWxUbz4NCjxMaWNlbnNlVHlwZT5EZXZlbG9wZXIgU21hbGwgQnVzaW5lc3M8L0xpY2Vuc2VUeXBlPg0KPExpY2Vuc2VOb3RlPjEgRGV2ZWxvcGVyIEFuZCAxIERlcGxveW1lbnQgTG9jYXRpb248L0xpY2Vuc2VOb3RlPg0KPE9yZGVySUQ+MjEwMzE2MTg1OTU3PC9PcmRlcklEPg0KPFVzZXJJRD43NDQ5MTY8L1VzZXJJRD4NCjxPRU0+VGhpcyBpcyBub3QgYSByZWRpc3RyaWJ1dGFibGUgbGljZW5zZTwvT0VNPg0KPFByb2R1Y3RzPg0KPFByb2R1Y3Q+QXNwb3NlLlRvdGFsIGZvciAuTkVUPC9Qcm9kdWN0Pg0KPC9Qcm9kdWN0cz4NCjxFZGl0aW9uVHlwZT5Qcm9mZXNzaW9uYWw8L0VkaXRpb25UeXBlPg0KPFNlcmlhbE51bWJlcj4wM2ZiMTk5YS01YzhhLTQ4ZGItOTkyZS1kMDg0ZmYwNjZkMGM8L1NlcmlhbE51bWJlcj4NCjxTdWJzY3JpcHRpb25FeHBpcnk+MjAyMjA1MTY8L1N1YnNjcmlwdGlvbkV4cGlyeT4NCjxMaWNlbnNlVmVyc2lvbj4zLjA8L0xpY2Vuc2VWZXJzaW9uPg0KPExpY2Vuc2VJbnN0cnVjdGlvbnM+aHR0cHM6Ly9wdXJjaGFzZS5hc3Bvc2UuY29tL3BvbGljaWVzL3VzZS1saWNlbnNlPC9MaWNlbnNlSW5zdHJ1Y3Rpb25zPg0KPC9EYXRhPg0KPFNpZ25hdHVyZT5XbkJYNnJOdHpCclNMV3pBdFlqOEtkdDFLSUI5MlFrL2xEbFNmMlM1TFRIWGdkcS9QQ2NqWHVORmp0NEJuRmZwNFZLc3VsSjhWeFExakIwbmM0R1lWcWZLek14SFFkaXFuZU03NTJaMjlPbmdyVW40Yk0rc1l6WWVSTE9UOEpxbE9RN05rRFU0bUk2Z1VyQ3dxcjdnUVYxbDJJWkJxNXMzTEFHMFRjQ1ZncEE9PC9TaWduYXR1cmU+DQo8L0xpY2Vuc2U+DQo=";
            var stream = new MemoryStream(Convert.FromBase64String(LData));
            stream.Seek(0, SeekOrigin.Begin);
            var license = new License();
            license.SetLicense(stream);
        }
        public DataTable GetExcelSheetData(int sheetIndex, int firstRow, string filePath)
        {
            // 加載Excel文件
            var workbook = new Workbook(filePath);
            var worksheet = workbook.Worksheets[sheetIndex];

            var dataTable1 =  worksheet.Cells.ExportDataTable(firstRow, 0, worksheet.Cells.MaxDataRow + 1, worksheet.Cells.MaxDataColumn + 1, true);
            // 剃除 dataTable1 中沒有資料的行
            for (int i = dataTable1.Rows.Count - 1; i >= 0; i--)  // 需要倒序遍歷，因為移除行時會改變索引
            {
                var row = dataTable1.Rows[i];
                if (row.ItemArray.All(field => field == null || string.IsNullOrWhiteSpace(field.ToString())))
                {
                    dataTable1.Rows.RemoveAt(i);
                }
            }

            return dataTable1;
        }
        public string FindFilePath(string directoryPath, string searchPattern)
        {
            // 搜尋符合條件的檔案
            var files = Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories)
                .Where(file => Path.GetFileName(file).Contains(searchPattern))
                .ToList();

            // 如果找到檔案，返回第一個檔案的完整路徑
            return files.Count > 0 ? files[0] : null;
        }
    }
}