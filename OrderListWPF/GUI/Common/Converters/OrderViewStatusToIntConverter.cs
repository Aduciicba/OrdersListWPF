using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Globalization;
using System.Windows.Data;
using OrderListWPF.GUI.ViewModel;

namespace OrderListWPF.GUI.Common.Converters
{

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
