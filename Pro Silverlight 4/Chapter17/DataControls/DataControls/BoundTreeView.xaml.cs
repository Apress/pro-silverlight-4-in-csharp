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
using System.ServiceModel;
using System.Windows.Browser;
using System.ServiceModel.Description;
using DataControls.DataService;

namespace DataControls
{
    public partial class BoundTreeView : UserControl
    {
        public BoundTreeView()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            StoreDbClient client = new StoreDbClient();
            EndpointAddress address = new EndpointAddress("http://localhost:" +
                HtmlPage.Document.DocumentUri.Port + "/DataControls.Web/StoreDb.svc");
            client.Endpoint.Address = address;

            client.GetCategoriesWithProductsCompleted += client_GetCategoriesWithProductsCompleted;
            client.GetCategoriesWithProductsAsync();

            lblStatus.Text = "Contacting service ...";
        }

        private void client_GetCategoriesWithProductsCompleted(object sender, GetCategoriesWithProductsCompletedEventArgs e)
        {
            try
            {
                treeCategories.ItemsSource = e.Result;
                lblStatus.Text = "Received results from web service.";
            }
            catch (Exception err)
            {
                lblStatus.Text = "An error occured: " + err.Message;
            }
        }
    }
}
