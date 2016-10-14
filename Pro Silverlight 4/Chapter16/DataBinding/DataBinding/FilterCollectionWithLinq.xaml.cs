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
using System.Collections.ObjectModel;

namespace DataBinding
{
    public partial class FilterCollectionWithLinq : UserControl
    {
        public FilterCollectionWithLinq()
        {
            InitializeComponent();
        }

        private List<Product> products = new List<Product>();


        private double minCost;
        private void cmdGetProducts_Click(object sender, RoutedEventArgs e)
        {
            if (!Double.TryParse(txtMinimumCost.Text, out minCost))
            {
                lblError.Text = "The minimum cost is not a valid number.";
                return;
            }

            EndpointAddress address = new EndpointAddress("http://localhost:" +
               HtmlPage.Document.DocumentUri.Port + "/DataBinding.Web/StoreDb.svc");
            StoreDbClient client = new StoreDbClient(new BasicHttpBinding(), address);

            client.GetProductsCompleted += client_GetProductsCompleted;
            client.GetProductsAsync();
        }

        private void client_GetProductsCompleted(object sender, GetProductsCompletedEventArgs e)
        {
            try
            {                  
                products.Clear();
                IEnumerable<Product> matches = from product in e.Result 
                                               where product.UnitCost >= minCost
                                               select product;

                lstProducts.ItemsSource = matches;
            }
            catch (Exception err)
            {
                lblError.Text = "Failed to contact service.";
            }
        }

      

        private void lstProducts_SelectionChanged(object sender, RoutedEventArgs e)
        {
            gridProductDetails.DataContext = lstProducts.SelectedItem;
        }

    }
}
