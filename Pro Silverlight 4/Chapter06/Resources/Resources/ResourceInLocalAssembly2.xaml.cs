using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Resources;
using System.Windows.Media.Imaging;

namespace Resources
{
    public partial class ResourceInLocalAssembly2 : UserControl
    {
        public ResourceInLocalAssembly2()
        {
            InitializeComponent();

            StreamResourceInfo sri = Application.GetResourceStream(
                new Uri("Resources;component/harpsichord_resource.jpg", UriKind.Relative));

            BitmapImage bitmap = new BitmapImage();
            bitmap.SetSource(sri.Stream);

            img.Source = bitmap;                
        }
    }
}
