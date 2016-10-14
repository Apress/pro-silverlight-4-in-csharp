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

namespace ElevatedTrust
{
    public partial class WebBrowserTest : UserControl
    {
        public WebBrowserTest()
        {
            InitializeComponent();
            
            browser.NavigateToString("<h1>Welcome to the WebBrowser Test</h1>");
        }

        private void cmdGo_Click(object sender, RoutedEventArgs e)
        {
            try
            {                
                browser.Navigate(new Uri(txtUrl.Text));
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, err.GetType().Name, MessageBoxButton.OK);
            }
        }
    }
}
