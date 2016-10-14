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

namespace Navigation
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void cmdNavigate_Click(object sender, RoutedEventArgs e)
        {            
            mainFrame.Navigate(new Uri("/Page1.xaml", UriKind.Relative));            
        }

    }
}
