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

namespace OutOfBrowserApplication
{
    public partial class InstallPage : UserControl
    {
        public InstallPage()
        {
            InitializeComponent();

            if (Application.Current.InstallState == InstallState.Installed)
            {
                lblMessage.Text = "This application is already installed. " +
                    "You cannot use the browser to run it. " +
                    "Instead, use the shortcut on your computer.";
                cmdInstall.IsEnabled = false;
            }
        }

        private void cmdInstall_Click(object sender, RoutedEventArgs e)
        {
            // Make sure that the application is not already installed.
            if (Application.Current.InstallState != InstallState.Installed)
            {
                // Attempt to install it.
                bool installAccepted = Application.Current.Install();

                if (!installAccepted)
                {
                    lblMessage.Text =
                      "You declined the install. Click Install to try again.";
                }
                else
                {
                    cmdInstall.IsEnabled = false;
                    lblMessage.Text = "The application is installing... ";
                }
            }
        }

        public void DisplayInstalled()
        {
            lblMessage.Text =
              "The application installed and launched. You can close this page.";
        }

        public void DisplayFailed()
        {
            lblMessage.Text = "The application failed to install.";
            cmdInstall.IsEnabled = true;
        }


    }
}
