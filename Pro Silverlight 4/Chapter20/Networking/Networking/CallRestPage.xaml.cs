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
using System.Net;
using System.IO;
using System.Windows.Browser;

namespace Networking
{
    public partial class CallRestPage : UserControl
    {
        public CallRestPage()
        {
            InitializeComponent();
        }
                
        private string searchYear;

        private void cmdGetData_Click(object sender, RoutedEventArgs e)
        {             
            Uri address = new Uri("http://localhost:" +
                HtmlPage.Document.DocumentUri.Port + "/ASPWebSite/PopulationService.ashx");

            WebRequest request = WebRequest.Create(address);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            
            // Store the year you want to use.
            searchYear = txtYear.Text;

            // Prepare the request asynchronously.
            request.BeginGetRequestStream(CreateRequest, request);
        }

        private void CreateRequest(IAsyncResult asyncResult)
        {
            WebRequest request = (WebRequest)asyncResult.AsyncState;
            
            // Write the year information in the name-value format "year=1985".
            Stream requestStream = request.EndGetRequestStream(asyncResult);
            StreamWriter writer = new StreamWriter(requestStream);
            writer.Write("year=" + searchYear);
            
            // Clean up (required).
            writer.Close();
            requestStream.Close();
            
            // Read the response asynchronously.
            request.BeginGetResponse(ReadResponse, request);
        }

        private void ReadResponse(IAsyncResult asyncResult)
        {
            string result;
            WebRequest request = (WebRequest)asyncResult.AsyncState;

            // Get the respone stream.
            WebResponse response = request.EndGetResponse(asyncResult);
            Stream responseStream = response.GetResponseStream();

            try
            {
                // Read the returned text.
                StreamReader reader = new StreamReader(responseStream);
                string population = reader.ReadToEnd();
                result = population + " people.";
            }
            catch (Exception err)
            {
                result = "Error contacting service.";
            }
            finally
            {
                response.Close();
            }

            // Update the display.
            Dispatcher.BeginInvoke(
                    delegate()
                    {
                        lblResult.Text = result;
                    });
        }
    }
}
