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
using System.Text.RegularExpressions;
using System.Windows.Browser;

namespace Networking
{
    public class HtmlCleanUpConverter : IValueConverter
    {
        private int maxCharacterLength = 200;
        public int MaxCharacterLength
        {
            get { return maxCharacterLength; }
            set { maxCharacterLength = value; }
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // Remove tags, newlines, and spaces.
            string returnString = Regex.Replace(value as string, "<.*?>", "");
            returnString = Regex.Replace(returnString, @"\n+\s+", "\n\n");

            // Decode HTML entities.
            returnString = HttpUtility.HtmlDecode(returnString);

            // Shorten.
            if (returnString.Length > MaxCharacterLength)
                return returnString.Substring(0, MaxCharacterLength) + "...";
            else
                return returnString;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
