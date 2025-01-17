﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace SBD.Converters
{
    public class IsEqualityConverter : ValueConverterBase<IsEqualityConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null && value.Equals(parameter);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null && ((bool)value) ? parameter : Binding.DoNothing;
        }
    }
}