using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SBD.Converters
{
    public class MultiValueToVisibilityConverter : MultiValueConverterBase<MultiValueToVisibilityConverter>
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2 && values[0] != null && values[1] != null)
            {
                string selectedItemGroupName = values[0].ToString();
                string tagValue = values[1].ToString();

                return selectedItemGroupName == tagValue ? Visibility.Visible : Visibility.Collapsed;
            }

            return Visibility.Collapsed;
        }

        public override object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        public class ToBooleanConverter : ValueConverterBase<ToBooleanConverter>
        {
            public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return value.Equals(parameter);
            }

            public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return ((bool)value) ? parameter : Binding.DoNothing;
            }
        }
    }
}