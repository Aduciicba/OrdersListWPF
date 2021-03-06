﻿using System;
using System.Globalization;
using System.Windows.Data;
using OrderListWPF.GUI.ViewModel;

namespace OrderListWPF.GUI.Common.Converters
{
    /// <summary>
    /// Convert OrdersViewStatusType enum value to int value
    /// </summary>
    [ValueConversion(typeof(OrdersViewStatusType), typeof(int))]
    public class OrderViewStatusToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is OrdersViewStatusType)
            {
                return (int)value;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
            {
                return (OrdersViewStatusType)value;
            }
            return value;
        }

    }
}
