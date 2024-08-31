using System.Data;

namespace SBD.Infrastructure.Internel.Interface
{
    public interface IFileService
    {
        DataTable GetExcelSheetData(int sheetIndex, int firstRow, string filePath);
        string FindFilePath(string directoryPath, string searchPattern);
    }
}
