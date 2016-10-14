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
using System.Windows.Navigation;

namespace CustomContentLoader
{
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        private void cmdLogin_Click(object sender, RoutedEventArgs e)
        {
            // Use a hard-coded password. A more realistic application would call a remote
            // authentication service that runs on an ASP.NET website.
            if (txtPassword.Text == "secret")
            {
                App.UserIsAuthenticated = true;
                this.NavigationService.Refresh();                
            }
        }

    }
}
