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
    public partial class PrintPreview : ChildWindow
    {
        private Point originalSize = new Point();

        public PrintPreview(WriteableBitmap printPreviewBitmap)
        {
            InitializeComponent();
                        
            imgPreview.Source = printPreviewBitmap;
            imgPreview.Height = printPreviewBitmap.PixelHeight;
            imgPreview.Width = printPreviewBitmap.PixelWidth;

            originalSize.X = imgPreview.Width;
            originalSize.Y = imgPreview.Height;            
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void sliderZoom_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (sliderZoom == null) return;
            
            imgPreview.Height = originalSize.Y * sliderZoom.Value;
            imgPreview.Width = originalSize.X * sliderZoom.Value;
        }

        private void ChildWindow_LayoutUpdated(object sender, EventArgs e)
        {
            // Make sure window won't collapse below its original bounds
            // when the slider is used.
            scrollContainer.MinWidth = scrollContainer.ActualWidth;
            scrollContainer.MinHeight = scrollContainer.ActualHeight;
        }
    }
}
