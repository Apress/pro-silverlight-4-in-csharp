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

namespace BrowserInteraction
{
    public partial class BrowserDetails : UserControl
    {
        public BrowserDetails()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {            
            BrowserInformation b = HtmlPage.BrowserInformation;
            lblInfo.Text = "Name: " + b.Name;
            lblInfo.Text += "\nBrowser Version: " + b.BrowserVersion;
            lblInfo.Text += "\nPlatform: " + b.Platform;            
            lblInfo.Text += "\nCookies Enabled: " + b.CookiesEnabled;
            lblInfo.Text += "\nUser Agent: " + b.UserAgent;
        }
    }
}
