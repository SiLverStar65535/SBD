using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SBD.Converters
{
    public class BooleanToVisibilityConverter : ValueConverterBase<BooleanToVisibilityConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? Visibility.Visible : Visibility.Collapsed;
            }

            return Visibility.Collapsed;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is Visibility;
        }
    }
}