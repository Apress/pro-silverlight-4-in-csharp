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
using System.Xml;

namespace Resources
{
    public partial class DownloadResource : UserControl
    {
        public DownloadResource()
        {
            InitializeComponent();
        }

        private void cmdDownload_Click(object sender, RoutedEventArgs e)
        {
            string uri = App.Current.Host.Source.AbsoluteUri;
            int index = uri.IndexOf("/ClientBin");
            uri = uri.Substring(0, index) + "/ProductList.xml";
            
            WebClient webClient = new WebClient();            
            webClient.OpenReadCompleted += webClient_OpenReadCompleted;
            webClient.DownloadProgressChanged += webClient_DownloadProgressChanged;
            webClient.OpenReadAsync(new Uri(uri));
        }

        private void webClient_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            Stream stream = (Stream)e.Result;
            XmlReader reader = XmlReader.Create(stream, new XmlReaderSettings());

            txt.Text = "Here's a dump of the retrieved data:\n\n";
            while (reader.Read())
            {
                txt.Text += reader.Value;
            }
            reader.Close();
        }

        private void webClient_DownloadProgressChanged(object sender,
  DownloadProgressChangedEventArgs e)
        {
            lblProgress.Text = e.ProgressPercentage.ToString() + " % downloaded.";
            progressBar.Value = e.ProgressPercentage;
        }

    }
}
