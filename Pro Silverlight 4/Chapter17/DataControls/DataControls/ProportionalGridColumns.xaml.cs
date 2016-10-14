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
using DataControls.DataService;
using System.ServiceModel;
using System.Windows.Browser;

namespace DataControls
{
    public partial class ProportionalGridColumns : UserControl
    {
        public ProportionalGridColumns()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            EndpointAddress address = new EndpointAddress(
                "http://localhost:" +
               HtmlPage.Document.DocumentUri.Port + "/DataControls.Web/StoreDb.svc");
            StoreDbClient client = new StoreDbClient();
            client.Endpoint.Address = address;

            client.GetProductsCompleted += client_GetProductsCompleted;
            client.GetProductsAsync();
        }

        private void client_GetProductsCompleted(object sender, GetProductsCompletedEventArgs e)
        {
            try
            {
                gridProducts.ItemsSource = e.Result;
            }
            catch (Exception err)
            {
                lblInfo.Text = "Failed to contact service.";
            }
        }


    }
}
