using System;
using System.Globalization;

namespace SBD.Converters
{
    public class MultiValueComparisonConverter : MultiValueConverterBase<MultiValueComparisonConverter>
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2 && System.Convert.ToDouble(values[0] ) is var value1 && System.Convert.ToDouble(values[1] ) is var value2)
            {
                return value1 > value2;
            }
            return false;
        }

        public override object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}