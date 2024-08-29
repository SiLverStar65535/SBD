using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace SBD.Converters
{
    public abstract class ValueConverterBase<ConverterType> : MarkupExtension, IValueConverter where ConverterType : class, new()
    {    
        //直接利用擴充標記，無須在放入Resources了
        //ex:
        //xmlns:converters="clr-namespace:ASI.Wanda.CMFT.WPF.Core.Converters"
        //Converter={converters:StationIDToStationNameConverter}}"
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);
        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);
        public static readonly ConverterType Instance = new ConverterType();
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Instance;
        }
    }
}