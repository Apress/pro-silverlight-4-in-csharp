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

namespace DataBinding
{
    public class PriceToBackgroundConverter : IValueConverter
    {
        public double MinimumPriceToHighlight
        {
            get;
            set;
        }

        public Brush HighlightBrush
        {
            get;
            set;
        }

        public Brush DefaultBrush
        {
            get;
            set;
        }

        public object Convert(object value, Type targetType, object parameter,
          System.Globalization.CultureInfo culture)
        {            
            double price = (double)value;
            if (price >= MinimumPriceToHighlight)
                return HighlightBrush;
            else
                return DefaultBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
          System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

}
