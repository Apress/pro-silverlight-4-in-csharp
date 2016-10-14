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
using System.Windows.Messaging;

namespace Eavesdropper
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private LocalMessageReceiver receiver = new LocalMessageReceiver("EavesdropperReceiver");
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            receiver.MessageReceived += receiver_MessageReceived;
            receiver.Listen();
        }

        private void receiver_MessageReceived(object sender, MessageReceivedEventArgs e)
        {   
            lblDisplay.Text = "The user of Main Application typed: \"" + e.Message + "\"";
        }
    
    }
}
