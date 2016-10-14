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
using DataControls.DataService;
using System.Windows.Browser;
using System.Windows.Data;
using System.ComponentModel;
using StoreDbDataClasses;

namespace DataControls
{
    public partial class DataGridPaging : UserControl
    {
        public DataGridPaging()
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
                PagedCollectionView view = new PagedCollectionView(e.Result);
                // Sort by category and price.
                view.SortDescriptions.Add(new SortDescription("CategoryName", ListSortDirection.Ascending ));
                view.SortDescriptions.Add(new SortDescription("UnitCost", ListSortDirection.Ascending));

                gridProducts.ItemsSource = view;

                pager.Source = view;
            }
            catch (Exception err)
            {
                lblInfo.Text = "Failed to contact service.";
            }
        }
    }
}
