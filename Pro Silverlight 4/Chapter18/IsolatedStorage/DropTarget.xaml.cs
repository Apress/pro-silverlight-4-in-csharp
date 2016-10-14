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
using System.IO;
using System.Windows.Media.Imaging;

namespace IsolatedStorage
{
    public partial class DropTarget : UserControl
    {
        public DropTarget()
        {
            InitializeComponent();
        }

        private void rectDropSurface_DragEnter(object sender, DragEventArgs e)
        {
            lblResults.Text = "You are dragging an object over the drop area.";
        }

        private void rectDropSurface_DragLeave(object sender, DragEventArgs e)
        {
            lblResults.Text = "";
        }

        private void rectDropSurface_Drop(object sender, DragEventArgs e)
        {
            // Currently FileDrop is the only supported format.
            // However, it's good form to test the type of format, in case more
            // formats are supported in the future.
            if ((e.Data != null) &&
              (e.Data.GetDataPresent(DataFormats.FileDrop)))
            {  
                // Get all the files that were dropped.                
                FileInfo[] files = (FileInfo[])e.Data.GetData(DataFormats.FileDrop);
                                
                foreach (FileInfo file in files)
                {
                    // If you aren't running in an eleveated trust, you can't access
                    // the full path information exposed by the properties of FileInfo.
                    // However, the no-path file name (the Name property) is accessible.
                    string ext = System.IO.Path.GetExtension(file.Name);

                    // Check if it's an image.
                    switch (ext.ToLower())
                    {
                        case ".jpg":
                        case ".gif":
                        case ".png":
                        case ".bmp":
                            try
                            {
                                // Read the image, wrap it in a BitmapImage, and
                                // add it to the list.
                                using (FileStream fs = file.OpenRead())
                                {
                                    BitmapImage source = new BitmapImage();
                                    source.SetSource(fs);
                                    lstImages.Items.Add(source);                                    
                                }
                            }
                            catch (Exception err)
                            {
                                lblResults.Text = "Error reading file: " + err.Message;
                            }
                            break;
                        default:
                            lblResults.Text = "The dropped file was not recognized as a supported image type.";
                            break;
                    }
                                        
                    lblResults.Text = files.Count().ToString() + " files successfully dropped.";
                }            
            }   
        }

    }
}
