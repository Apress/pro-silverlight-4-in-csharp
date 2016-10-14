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

namespace DataControls
{    
    public class DateOnlyConverter : IValueConverter
    {
        public bool ShortDate {get; set;}
        
        public object Convert(object value, Type targetType, object parameter,
          CultureInfo culture)
        {
            DateTime date = (DateTime)value;
            if (ShortDate)
            {
                return date.ToShortDateString();
            }
            else
            {
                return date.ToLongDateString();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter,
          CultureInfo culture)
        {
            string date = value.ToString();

            DateTime result;
            if (DateTime.TryParse(date, out result))
            {
                return result;
            }
            return value;
        }
    }

}
