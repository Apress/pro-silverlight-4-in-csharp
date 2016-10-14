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
using System.Windows.Media.Imaging;

namespace BrushesAndTransforms
{
    public partial class ScreenCapture : UserControl
    {
        public ScreenCapture()
        {
            InitializeComponent();
        }

        private void cmdCapture_Click(object sender, RoutedEventArgs e)
        {
            UserControl mainPage = (UserControl)Application.Current.RootVisual;
                        
            WriteableBitmap wb = new WriteableBitmap((int)mainPage.ActualWidth, 
                (int)mainPage.ActualHeight);

            wb.Render(mainPage, null);            
            wb.Invalidate();

            // Show the bitmap in an Image element.
            img.Source = wb;
        }
    }
}
