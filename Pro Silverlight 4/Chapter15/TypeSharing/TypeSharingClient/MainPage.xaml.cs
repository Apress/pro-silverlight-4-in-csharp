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
using System.Windows.Browser;
using TypeSharingClient.Service;
using DataClasses;

namespace TypeSharingClient
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void cmdCallService_Click(object sender, RoutedEventArgs e)
        {
            System.ServiceModel.EndpointAddress address = new System.ServiceModel.EndpointAddress("http://localhost:" +
                HtmlPage.Document.DocumentUri.Port + "/TypeSharing.Web/TestService.svc");
            TestServiceClient proxy = new TestServiceClient();
            proxy.Endpoint.Address = address;

            proxy.GetCustomerCompleted += GetCustomerCompleted;
            proxy.GetCustomerAsync(0);
        }

        private void GetCustomerCompleted(object sender, GetCustomerCompletedEventArgs e)
        {
            Customer customer = e.Result;
            MessageBox.Show(customer.GetFullName());
        }
        
    }
}
