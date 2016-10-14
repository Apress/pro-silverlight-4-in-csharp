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
using System.Runtime.Serialization.Json;
using System.IO;
using System.Net;
using System.Windows.Browser;

namespace Networking
{
    public partial class CallJsonService : UserControl
    {
        public CallJsonService()
        {
            InitializeComponent();
        }

         private void cmdGetData_Click(object sender, RoutedEventArgs e)
        {
            WebClient client = new WebClient();
            Uri address = new Uri(
                "http://search.yahooapis.com/ImageSearchService/V1/imageSearch?appid=YahooDemo&query=" + 
                HttpUtility.UrlEncode(txtSearchKeyword.Text) + "&output=json");

            client.OpenReadCompleted += client_OpenReadCompleted;
            client.OpenReadAsync(address);
        }

         private void client_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
         {             
             DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(SearchResults));
             SearchResults results = (SearchResults)serializer.ReadObject(e.Result);
             
             lblResultsTotal.Text = results.ResultSet.totalResultsAvailable + " total results.";
             lblResultsReturned.Text = results.ResultSet.totalResultsReturned + " results returned.";
             gridResults.ItemsSource = results.ResultSet.Result;
         }
    }

    public class SearchResults
    {
        public SearchResultSet ResultSet;
    }

    public class SearchResultSet
    {
        public int totalResultsAvailable { get; set; }
        public int totalResultsReturned { get; set; }
        public SearchResult[] Result { get; set; }
    }

    public class SearchResult
    {
        public string Title {get; set;}
        public string Summary { get; set; }
        public string Url { get; set; }
    }
}
