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
using DataBinding.DataService;
using System.Windows.Browser;
using System.Collections.ObjectModel;

namespace DataBinding
{
    public partial class BindToCollection : UserControl
    {
        public BindToCollection()
        {
            InitializeComponent();
        }

        //private List<Product> products = new List<Product>();
        private ObservableCollection<Product> products = new ObservableCollection<Product>();
        
        private void cmdGetProducts_Click(object sender, RoutedEventArgs e)
        {
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
                foreach (Product product in e.Result) products.Add(product);

                lstProducts.ItemsSource = products;
            }
            catch (Exception err)
            {
                lblError.Text = "Failed to contact service.";
            }
        }
               

        private void cmdDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            products.Remove((Product)lstProducts.SelectedItem);
        }

        private void cmdAddProduct_Click(object sender, RoutedEventArgs e)
        {
            Product p = new Product();
            p.ModelName = "New Product";
            products.Add(p);
            lstProducts.SelectedItem = p;
            lstProducts.UpdateLayout();
            lstProducts.ScrollIntoView(p);
        }

        private void lstProducts_SelectionChanged(object sender, RoutedEventArgs e)
        {
            gridProductDetails.DataContext = lstProducts.SelectedItem;
        }


    }
}
