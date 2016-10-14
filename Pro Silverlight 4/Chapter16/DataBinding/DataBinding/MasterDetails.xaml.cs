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
using System.ServiceModel;
using System.Windows.Browser;
using DataBinding.DataService;

namespace DataBinding
{
    public partial class MasterDetails : UserControl
    {
        public MasterDetails()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            EndpointAddress address = new EndpointAddress("http://localhost:" +
               HtmlPage.Document.DocumentUri.Port + "/DataBinding.Web/StoreDb.svc");
            StoreDbClient client = new StoreDbClient(new BasicHttpBinding(), address);

            client.GetCategoriesWithProductsCompleted += client_GetCategoriesWithProductsCompleted;
            client.GetCategoriesWithProductsAsync();
        }

        private void client_GetCategoriesWithProductsCompleted(object sender, GetCategoriesWithProductsCompletedEventArgs e)
        {
            try
            {
                lstCategories.ItemsSource = e.Result;
            }
            catch (Exception err)
            {
                lblError.Text = "Failed to contact service.";
            }
        }

        private void lstCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lstProducts.ItemsSource = ((Category)lstCategories.SelectedItem).Products;
        }
    }
}
