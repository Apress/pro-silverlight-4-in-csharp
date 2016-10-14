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
using System.Windows.Printing;
using System.Windows.Media.Imaging;

namespace BrushesAndTransforms
{
    public partial class Printing : UserControl
    {
        public Printing()
        {
            InitializeComponent();

            for (int i = 0; i < 50; i++)
            {
                lst.Items.Add("This is line number " + i.ToString());
            }
        }

        private void cmdPrintImage_Click(object sender, RoutedEventArgs e)
        {
            PrintDocument document = new PrintDocument();
            document.PrintPage += documentImage_PrintPage;
            document.Print("Image Document");
        }

        private void documentImage_PrintPage(object sender, PrintPageEventArgs e)
        {               
            // Stretch the image to the size of the printed page.
            Image imgToPrint = new Image();
            imgToPrint.Source = imgInWindow.Source;
            imgToPrint.Width = e.PrintableArea.Width;
            imgToPrint.Height = e.PrintableArea.Height;

            // Choose to print the image.
            e.PageVisual = imgToPrint;

            // Do not fire this event again.
            e.HasMorePages = false;
        }

        // Keep track of the position in the list.
        private int listPrintIndex;
                
        private void cmdPrintList_Click(object sender, RoutedEventArgs e)
        {
            // Reset the position, in case a previous printout has changed it.
            listPrintIndex = 0;
            
            PrintDocument document = new PrintDocument();
            document.PrintPage += documentList_PrintPage;
            document.Print("List Document");            
        }


        // Add some extra margin space.
        private int extraMargin = 50;

        private void documentList_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Use a Canvas for the printing surface.
            Canvas printSurface = new Canvas();
            e.PageVisual = printSurface;

            e.HasMorePages = GeneratePage(printSurface, e.PageMargins, e.PrintableArea);
        }

        private bool GeneratePage(Canvas printSurface,
           Thickness pageMargins, Size pageSize)
        {
            // Find the starting coordinate.
            double topPosition = pageMargins.Top + extraMargin;
                        
            // Begin looping through the list.
            while (listPrintIndex < lst.Items.Count)
            {
                // Create a TextBlock for each line, with a 30-pixel font.
                TextBlock txt = new TextBlock();
                txt.FontSize = 30;                
                txt.Text = lst.Items[listPrintIndex].ToString();

                // If the new line doesn't fit, stop printing this page,
                // but request another page.
                double measuredHeight = txt.ActualHeight;
                if (measuredHeight > (pageSize.Height - topPosition - extraMargin))
                {                    
                    return true;
                }

                // Place the TextBlock on the Canvas.
                txt.SetValue(Canvas.TopProperty, topPosition);
                txt.SetValue(Canvas.LeftProperty, pageMargins.Left + extraMargin);
                printSurface.Children.Add(txt);
                
                // Move to the next line.
                listPrintIndex++;
                topPosition = topPosition + measuredHeight;
            }

            // The printing code has reached the end of the list.
            // No more pages are needed.
            return false;
        }

        private void cmdPreviewPrintList_Click(object sender, RoutedEventArgs e)
        {
            // Reset the position, in case a previous printout has changed it.
            listPrintIndex = 0;

            Canvas printSurface = new Canvas();
            // The page information isn't available without starting a real printout,
            // so we hard-code a typical page size.
            int width = 816;
            int height = 1056;
            printSurface.Width = width;
            printSurface.Height = height;
            GeneratePage(printSurface, new Thickness(0), new Size(width, height));            
            
            WriteableBitmap printPreviewBitmap = new WriteableBitmap(printSurface, null);

            PrintPreview preview = new PrintPreview(printPreviewBitmap);            
            preview.Show();
        }

    }
}
