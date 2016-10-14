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
using SilverlightUploader.Services;
using System.IO;
using System.ServiceModel;
using System.Windows.Browser;

namespace SilverlightUploader
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private FileServiceClient client = new FileServiceClient();
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Make sure the dyanmic URL is set (for testing).
            EndpointAddress address = new EndpointAddress("http://localhost:" +
               HtmlPage.Document.DocumentUri.Port + "/SilverlightUploader.Web/FileService.svc");
            client.Endpoint.Address = address;

            // Attach these event handlers for uploads and downloads.
            client.DownloadFileCompleted += client_DownloadFileCompleted;
            client.UploadFileCompleted += client_UploadFileCompleted;

            // Get the initial file list.
            client.GetFileListCompleted += client_GetFileListCompleted;
            client.GetFileListAsync();
        }               
                
        private void client_GetFileListCompleted(object sender, GetFileListCompletedEventArgs e)
        {
            try
            {
                lstFiles.ItemsSource = e.Result;
            }
            catch
            {
                lblStatus.Text = "Error contacting web service.";
            }
        }

        private void cmdDownload_Click(object sender, RoutedEventArgs e)
        {
            if (lstFiles.SelectedIndex != -1)
            {    
                // We must show SaveFileDialog now. We are not allowed to show it
                // when the web service returns the data, because the
                // DownloadFileCompleted event is not user-initiated.
                SaveFileDialog saveDialog = new SaveFileDialog();
                if (saveDialog.ShowDialog() == true)
                {
                    // The second arugment passes the SaveFileDialog, so the
                    // stream can be opened when the DownloadFileCompleted event
                    // fires.
                    // A nice side effect of this approach is that this code
                    // allows the user to start multiple simultaneous downloads.
                    client.DownloadFileAsync(lstFiles.SelectedItem.ToString(),
                        saveDialog);
                    lblStatus.Text = "Download started.";
                }
            }
        }

        private void client_DownloadFileCompleted(object sender, DownloadFileCompletedEventArgs e)
        {
            if (e.Error == null)
            {   
                lblStatus.Text = "Download completed.";

                // Get the SaveFileDialog that was passed in with the call.
                SaveFileDialog saveDialog = (SaveFileDialog)e.UserState;
                
                using (Stream stream = saveDialog.OpenFile())
                {
                    stream.Write(e.Result, 0, e.Result.Length);                    
                }
                lblStatus.Text = "File saved to " + saveDialog.SafeFileName;
            }            
            else
            {
                lblStatus.Text = "Download failed.";
            }
        }

        private void cmdUpload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();

            if (openDialog.ShowDialog() == true)
            {
                try
                {
                    using (Stream stream = openDialog.File.OpenRead())
                    {
                        // Don't allow big files.
                        if (stream.Length > 5000000)
                        {
                            MessageBox.Show("Files must be less than 5 Megabytes.");
                        }
                        else
                        {
                            byte[] data = new byte[stream.Length];
                            stream.Read(data, 0, (int)stream.Length);

                            client.UploadFileAsync(openDialog.File.Name, data);
                            lblStatus.Text = "Upload started.";
                        }
                    }
                }
                catch
                {
                    lblStatus.Text = "Error reading file.";
                }
            }            
        }

        private void client_UploadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                lblStatus.Text = "Upload succeeded.";

                // Refresh the file list.
                client.GetFileListAsync();
            }
            else
            {
                lblStatus.Text = "Upload failed.";
            }
        }
    }
}
