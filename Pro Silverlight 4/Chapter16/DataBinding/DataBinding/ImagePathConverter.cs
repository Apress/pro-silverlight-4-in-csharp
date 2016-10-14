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
using System.Windows.Media.Imaging;
using System.Windows.Browser;
using System.IO;

namespace DataBinding
{
    public class ImagePathConverter : IValueConverter
    {
        private string rootUri;
        public string RootUri
        {
            get { return rootUri; }
            set { rootUri = value; }
        }

        public ImagePathConverter()
        {        
            string uri = HtmlPage.Document.DocumentUri.ToString();
            rootUri = uri.Remove(uri.LastIndexOf('/'), uri.Length - uri.LastIndexOf('/'));
        }

        public object Convert(object value, Type targetType, object parameter,
          System.Globalization.CultureInfo culture)
        {            
            string imagePath = RootUri + "/" + (string)value;
            
            // Hack for GIFs.
            // (The database expect GIF files, but Silverlight only supports PNG and JPEG.)
            imagePath = imagePath.ToLower().Replace(".gif", ".png");

            return new BitmapImage(new Uri(imagePath));
        }

        public object ConvertBack(object value, Type targetType, object parameter,
          System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

}
