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
using AutoCompleteTest.AutoCompleteService;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.Windows.Browser;

namespace AutoCompleteTest
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void acbProducts_Populating(object sender, PopulatingEventArgs e)
        {            
            // Signal that the task is being performed asynchronously.
            e.Cancel = true;

            // Create the web service object.
            ProductAutoCompleteClient service = new ProductAutoCompleteClient();

            // Make sure the dyanmic URL is set (for testing).
            EndpointAddress address = new EndpointAddress("http://localhost:" +
               HtmlPage.Document.DocumentUri.Port + "/AutoCompleteTest.Web/ProductAutoComplete.svc");
            service.Endpoint.Address = address;

            // Attach an event handler to the completion event.
            service.GetProductMatchesCompleted += GetProductMatchesCompleted;
            
            // Call the web service (asynchronously).
            service.GetProductMatchesAsync(e.Parameter);
            lblStatus.Text = "Calling web service ...";
        }

        private void GetProductMatchesCompleted(object sender, GetProductMatchesCompletedEventArgs e)
        {
            // Check for a web service error.
            if (e.Error != null)
            {
                lblStatus.Text = e.Error.Message;
                return;
            }

            lblStatus.Text = "Response received.";            

            // Set the suggestions.
            acbProducts.ItemsSource = e.Result;

            // Notify the control that the data has arrived.
            acbProducts.PopulateComplete();          
        }
    }
}
