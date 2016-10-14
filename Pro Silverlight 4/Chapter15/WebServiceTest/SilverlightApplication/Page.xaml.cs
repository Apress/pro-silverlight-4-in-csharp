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
using SilverlightApplication.ServiceReference1;
using System.Windows.Browser;
using System.Net.NetworkInformation;

namespace SilverlightApplication
{
    public partial class Page : UserControl
    {        
        public Page()
        {
            InitializeComponent();

            // Watch for network changes.
            NetworkChange.NetworkAddressChanged += NetworkChanged;

            // Set up the initial user interface
            CheckNetworkState();            
        }

        private void NetworkChanged(object sender, EventArgs e)
        {
            // Adjust the user interface to match the network state.
            CheckNetworkState();
        }

        private void CheckNetworkState()
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                // Currently online.
                cmdCallCachedService.IsEnabled = true;
                cmdCallService.IsEnabled = true;
            }
            else
            {
                // Currently offline.
                cmdCallCachedService.IsEnabled = false;
                cmdCallService.IsEnabled = false;
            }
        }

        private void callService_Click(object sender, RoutedEventArgs e)
        {
            System.ServiceModel.EndpointAddress address = new System.ServiceModel.EndpointAddress("http://localhost:" +
                HtmlPage.Document.DocumentUri.Port + "/SilverlightApplication.Web/TestService.svc");
            TestServiceClient proxy = new TestServiceClient();
            proxy.Endpoint.Address = address;
            
            proxy.GetServerTimeCompleted += new EventHandler<GetServerTimeCompletedEventArgs>(GetServerTimeCompleted);
            proxy.GetServerTimeAsync();
        }
        private void GetServerTimeCompleted(object sender, GetServerTimeCompletedEventArgs e)
        {
            try
            {
                lblTime.Text = e.Result.ToLongTimeString();
            }
            catch (Exception err)
            {
                lblTime.Text = err.Message;
                if (err.InnerException != null) lblTime.Text += "\n" + err.InnerException.Message;
            }
        }

        private void callCachedService_Click(object sender, RoutedEventArgs e)
        {
            System.ServiceModel.EndpointAddress address = new System.ServiceModel.EndpointAddress("http://localhost:" +
                HtmlPage.Document.DocumentUri.Port + "/SilverlightApplication.Web/TestService.svc");
            TestServiceClient proxy = new TestServiceClient();
            proxy.Endpoint.Address = address;

            proxy.GetCachedServerTimeCompleted += new EventHandler<GetCachedServerTimeCompletedEventArgs>(GetCachedServerTimeCompleted);
            proxy.GetCachedServerTimeAsync();
            
        }
        private void GetCachedServerTimeCompleted(object sender, GetCachedServerTimeCompletedEventArgs e)
        {
            try
            {
                lblTime.Text = e.Result.ToLongTimeString();
            }
            catch (Exception err)
            {
                lblTime.Text = err.Message;
                if (err.InnerException != null) lblTime.Text += "\n" + err.InnerException.Message;
            }
        }

        private void cmdCallSlowService_Click(object sender, RoutedEventArgs e)
        {
            System.ServiceModel.EndpointAddress address = new System.ServiceModel.EndpointAddress("http://localhost:" +
                HtmlPage.Document.DocumentUri.Port + "/SilverlightApplication.Web/TestService.svc");
            TestServiceClient proxy = new TestServiceClient();
            proxy.Endpoint.Address = address;

            cmdCallSlowService.IsEnabled = false;
            lblTime.Text = "";
            busy.IsBusy = true;

            proxy.GetServerTimeSlowCompleted += new EventHandler<GetServerTimeSlowCompletedEventArgs>(GetServerTimeSlowCompleted);
            proxy.GetServerTimeSlowAsync();
        }

        private void GetServerTimeSlowCompleted(object sender, GetServerTimeSlowCompletedEventArgs e)
        {
            try
            {
                lblTime.Text = e.Result.ToLongTimeString();
            }
            catch (Exception err)
            {
                lblTime.Text = err.Message;
                if (err.InnerException != null) lblTime.Text += "\n" + err.InnerException.Message;
            }
            finally
            {
                busy.IsBusy = false;
                cmdCallSlowService.IsEnabled = true;
            }
        }
    }
}
