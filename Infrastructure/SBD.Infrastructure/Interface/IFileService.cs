using System.Data;

namespace SBD.Infrastructure.Interface
{
    public interface IFileService
    {
        string FindFilePath(string directoryPath, string searchPattern);
        DataTable GetExcelSheetData(int sheetIndex, int firstRow, string filePath);
    }
}
