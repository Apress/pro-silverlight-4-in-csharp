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
using System.Windows.Browser;
using System.Globalization;
using System.Collections.ObjectModel;
using System.ServiceModel;
using DataControls.DataService;
using StoreDbDataClasses;

namespace DataControls
{
    public partial class DataGridTest : UserControl
    {
        
        public DataGridTest()
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

        // Reuse brush objects for efficiency in large data displays.
        private SolidColorBrush highlightBrush = new SolidColorBrush(Colors.Orange);
        private SolidColorBrush normalBrush = new SolidColorBrush(Colors.White);

        private void gridProducts_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            Product product = (Product)e.Row.DataContext;
            if (product.UnitCost > 100)
                e.Row.Background = highlightBrush;
            else
                e.Row.Background = normalBrush;
          
        }

        private void FormatRow(DataGridRow row)
        {
            Product product = (Product)row.DataContext;
            if (product.UnitCost > 100)
                row.Background = highlightBrush;                
            else
                row.Background = normalBrush;        
        }   
 
    }
}
