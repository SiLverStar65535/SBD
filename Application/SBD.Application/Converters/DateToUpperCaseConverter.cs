using System;
using System.Globalization;

namespace SBD.Converters
{
    public class DateToUpperCaseConverter : ValueConverterBase<DateToUpperCaseConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dateTime)
            {
                // 將日期格式化為 "ddMMM" 並轉換為大寫
                return dateTime.ToString("ddMMM", CultureInfo.InvariantCulture).ToUpper();
            }

            return string.Empty;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

