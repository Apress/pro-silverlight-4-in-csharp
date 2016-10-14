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
using DuplexService.Service;
using System.ServiceModel;
using System.Windows.Browser;

namespace DuplexService
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
                        
            EndpointAddress address = new EndpointAddress("http://localhost:" +
               HtmlPage.Document.DocumentUri.Port + "/DuplexService.Web/AsyncTask.svc");
            PollingDuplexHttpBinding binding = new PollingDuplexHttpBinding();
            
            client = new AsyncTaskServiceClient(binding, address);
            client.ReturnResultReceived += client_ReturnResultReceived;
        }

        private AsyncTaskServiceClient client;

        private void cmdSubmit_Click(object sender, RoutedEventArgs e)
        {   
            TaskDescription taskDescription = new TaskDescription();
            taskDescription.DataToProcess = txtTextToProcess.Text;
            client.SubmitTaskAsync(taskDescription);
            lblStatus.Text = "Asynchronous request sent to server.";
        }

        private void client_ReturnResultReceived(object sender, ReturnResultReceivedEventArgs e)
        {
            lblStatus.Text = "Response received: " + e.result.ProcessedData;
        }
    }
}
