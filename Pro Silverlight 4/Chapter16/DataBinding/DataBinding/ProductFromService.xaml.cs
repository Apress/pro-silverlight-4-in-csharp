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
using DataBinding.DataService;
using System.Windows.Browser;
using System.ServiceModel;

namespace DataBinding
{
    public partial class ProductFromService : UserControl
    {
        public ProductFromService()
        {
            InitializeComponent();
        }

        private void cmdGetProduct_Click(object sender, RoutedEventArgs e)
        {
            EndpointAddress address = new EndpointAddress("http://localhost:" +
               HtmlPage.Document.DocumentUri.Port + "/DataBinding.Web/StoreDb.svc");
            StoreDbClient client = new StoreDbClient(new BasicHttpBinding(), address);

            client.GetProductCompleted += client_GetProductCompleted;
            client.GetProductAsync(356);            
        }
                
        private void client_GetProductCompleted(object sender, GetProductCompletedEventArgs e)
        {
            try
            {
                gridProductDetails.DataContext = e.Result;
            }
            catch (Exception err)
            {
                lblError.Text = "Failed to contact service.";
            }
        }
    }
}
