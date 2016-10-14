using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Data;
using System.Globalization;

namespace DataBinding
{    
    public class PriceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
          CultureInfo culture)
        {
            double price = (double)value;
            return price.ToString("C", culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter,
          CultureInfo culture)
        {
            string price = value.ToString();

            double result;
            if (Double.TryParse(price, NumberStyles.Any, culture, out result))
            {
                return result;
            }
            return value;
        }
    }

}
