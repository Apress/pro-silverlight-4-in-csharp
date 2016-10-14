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

namespace SilverlightApplication
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private LocalMessageSender messageSender = new LocalMessageSender("EavesdropperReceiver");

        private void txt_KeyUp(object sender, KeyEventArgs e)
        {
            messageSender.SendAsync(txt.Text);
        }
    }
}
