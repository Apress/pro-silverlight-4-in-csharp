using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Net;
using System.Windows.Browser;
using System.Text.RegularExpressions;

namespace Networking
{
    public partial class ReadHtmlPage : UserControl
    {
        public ReadHtmlPage()
        {
            InitializeComponent();
        }

        private void cmdGetData_Click(object sender, RoutedEventArgs e)
        {
            lblResult.Text = "";

            WebClient client = new WebClient();            
            Uri address = new Uri("http://localhost:" +
                HtmlPage.Document.DocumentUri.Port + "/ASPWebSite/PopulationTable.html");

            client.DownloadStringCompleted += client_DownloadStringCompleted;
            client.DownloadStringAsync(address);
        }

        private void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            string pageHtml = "";

            try
            {
                pageHtml = e.Result;                
            }
            catch
            {
                lblResult.Text = "Error contacting service.";
                return;
            }

            // Match in form <th>500 BCE</th><td>100,000</td>
            string pattern = @"<th>" + txtYear.Text + "</th>" + @"\s*" + "<td>" + "(?<population>.*)" + @"</td>";

            Regex regex = new Regex(pattern);
            Match match = regex.Match(pageHtml);
            string people = match.Groups["population"].Value;
            if (people == "")
                lblResult.Text = "Year not found.";
            else
                lblResult.Text = match.Groups["population"].Value + " people.";
        }
    }
}
