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
    public partial class GenerateBitmap : UserControl
    {
        public GenerateBitmap()
        {
            InitializeComponent();
        }

        private void cmdGenerate_Click(object sender, RoutedEventArgs e)
        {            
            // Create the bitmap, with the dimensions of the image placeholder.
            WriteableBitmap wb = new WriteableBitmap((int)img.Width, 
                (int)img.Height);
                        
            Random rand = new Random();
            for (int x = 0; x < wb.PixelWidth; x++)
            {
                for (int y = 0; y < wb.PixelHeight; y++)
                {
                    int alpha = 255;
                    int red = 0;
                    int green = 0;
                    int blue = 0;
                    
                    // Determine the pixel's color.
                    if ((x % 5 == 0) || (y % 7 == 0))
                    {
                        red = (int)((double)y / wb.PixelHeight * 255);
                        green = rand.Next(100, 255);
                        blue = (int)((double)x / wb.PixelWidth * 255);
                    }
                    else
                    {                        
                        red = (int)((double)x / wb.PixelWidth * 255);
                        green = rand.Next(100, 255);
                        blue = (int)((double)y / wb.PixelHeight * 255);
                    }

                    // Set the pixel value.
                    int pixelColorValue = (alpha << 24) | (red << 16) |
                     (green << 8) | (blue << 0);
                                        
                    wb.Pixels[y * wb.PixelWidth + x] = pixelColorValue;
                }
            }
                        
            // Show the bitmap in an Image element.
            img.Source = wb;                        
        }
    }
}
