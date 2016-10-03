using System;
using System.Globalization;
using System.Windows.Data;

namespace OrderListWPF.GUI.Common.Converters{

    /// <summary>
    /// Convert item index in Datagrid to row index by incrementing
    /// </summary>
    [ValueConversion(typeof(int), typeof(int))]
    public class ItemIndexToRowIndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
            {
                return (int)value + 1;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
            {
                return (int)value - 1;
            }
            return value;
        }

    }
}
