using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Xml;
using System.ServiceModel.Syndication;
using System.Net;
using System.Windows.Browser;

namespace Networking
{
    public partial class ReadRssFeed : UserControl
    {
        public ReadRssFeed()
        {
            InitializeComponent();
        }

        
        private void cmdGetData_Click(object sender, RoutedEventArgs e)
        {
            WebClient client = new WebClient();
            

            Uri address = new Uri(
        "http://feeds.wired.com/wired/index");
            client.OpenReadCompleted += client_OpenReadCompleted;
            client.OpenReadAsync(address);                 
        }
                
        private void client_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            try
            {
                XmlReader reader = XmlReader.Create(e.Result);
                SyndicationFeed feed = SyndicationFeed.Load(reader);
                gridFeed.ItemsSource = feed.Items;
                reader.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void gridFeed_SelectionChanged(object sender, EventArgs e)
        {
            HtmlElement element = HtmlPage.Document.GetElementById("rssFrame");
            SyndicationItem selectedItem = (SyndicationItem)gridFeed.SelectedItem;
            element.SetAttribute("src", selectedItem.Links[0].Uri.ToString());
        }

        

    }
}
