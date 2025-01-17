﻿using System;
using System.Globalization;
using System.Windows;

namespace SBD.Converters
{
    public class InversBooleanToVisibilityConverter : ValueConverterBase<InversBooleanToVisibilityConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? Visibility.Collapsed : Visibility.Visible;
            }

            return Visibility.Collapsed;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is not Visibility;
        }
    }
}